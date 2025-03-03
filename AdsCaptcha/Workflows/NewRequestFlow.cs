using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Dal.Cache;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.Dal.ElasticSearch;

namespace Inqwise.AdsCaptcha.Workflows
{
    public class NewRequestFlow
    {
        private const int DEFAULT_LOCK_TIMEOUT_IN_SECONDS = 10;
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private NewRequestFlow(NewRequestArgs args)
        {
            Args = args;
        }

        public NewRequestArgs Args { get; private set; }

        public static NewRequestFlow Instance(NewRequestArgs args)
        {
            return new NewRequestFlow(args);
        }

        private class ImageArgs
        {
            public string ProviderId { get; set;}
            public string Signature { get; set; }
            public string Url { get; set; }
            public string OwnerId { get; set; }
            public string ImageId { get; set; }
        }

        private class OwnerArgs
        {
            public string RealName { get; set; }
            public string ProfileUrl { get; set; }
        }

        public AdsCaptchaOperationResult Process()
        {
            Stopwatch sw = Stopwatch.StartNew();
            AdsCaptchaOperationResult result = null;
            using(CacheBuilder.GetLocker().AcquireLock(Args.RequestGuid.ToString(), TimeSpan.FromSeconds(10)))
            {
                try
                {
                    if (Args.ImageType == ImageType.Temp || Args.ImageType == ImageType.Ad)
                    {
                        Args.Request = RequestsManager.InsertTemp(Args);
                        sw.Stop();
                        if (sw.ElapsedMilliseconds > 100)
                        {
                            Log.Warn("Process: 1: {0}", sw.ElapsedMilliseconds);
                        }
                        sw.Restart();

                    }
                    else
                    {
                        var countryResult = CountriesManager.GetByIp(Args.ClientIp);
                        if (countryResult.HasValue)
                        {
                            Args.CountryId = countryResult.Value.Id;
                            Args.CountryPrefix = countryResult.Value.Prefix;
                        }

                        sw.Stop();
                        //if (sw.ElapsedMilliseconds > 100)
                        //{
                        //    Log.Error("Process: 2: {0}", sw.ElapsedMilliseconds);
                        //}
                        sw.Restart();

                        // Insert new request
                        AdsCaptchaOperationResult<long> requestIdResult = RequestsManager.Insert(Args);

                        sw.Stop();
                        if (sw.ElapsedMilliseconds > 200)
                        {
                            Log.Warn("Process: 3: {0}", sw.ElapsedMilliseconds);
                        }
                        //sw.Restart();

                        if (requestIdResult.HasError)
                        {
                            result = requestIdResult;
                        }
                        else
                        {
                            Args.RequestId = requestIdResult.Value;
                        }

                        // Get the request from DB
                        if (null == result)
                        {
                            var requestResult = RequestsManager.Get(Args.RequestId);
                            if (requestResult.HasError)
                            {
                                result = requestResult;
                            }
                            else
                            {
                                Args.Request = requestResult.Value;
                            }
                        }

                        //sw.Stop();
                        //if (sw.ElapsedMilliseconds > 200)
                        //{
                        //    Log.Error("Process: 4: {0}", sw.ElapsedMilliseconds);
                        //}

                    }

                    IRequest realRequest = null;
                    if (null == result)
                    {
                        realRequest = Args.Request as IRequest;
                        if (null == Args.Request.SpriteUrl)
                        {
                            Args.FileName = Guid.NewGuid().ToString().ToLower() + ".jpg";
                            string externalFilePath = String.Format(@"cap/{0:yyyyMM}/{0:dd}/{1}", DateTime.Now,
                                                                    Args.FileName);
                            // Generate sprite
                            Args.Request.SpriteFilePath = externalFilePath;

                            try
                            {
                                
                                string spriteBase64LowQuality;

                                if (Args.ImageType == ImageType.Temp)
                                {
                                    ImagesManager.GenerateSpriteFromCachedImage(Args.Request.ImagePath, Args.Request.CountOfFrames,
                                                                    Args.Request.CorrectIndex, Args.EffectType,
                                                                    Args.Request.SpriteFilePath,
                                                                    Args.Width.GetValueOrDefault(),
                                                                    Args.Height.GetValueOrDefault(),
                                                                    out spriteBase64LowQuality);
                                }
                                else
                                {
                                    string imageAbsolutePath = ImagesManager.GetImageAbsolutePath(Args.Request.ImagePath);
                                    ImagesManager.GenerateSprite(imageAbsolutePath, Args.Request.CountOfFrames,
                                                                    Args.Request.CorrectIndex, Args.EffectType,
                                                                    Args.Request.SpriteFilePath,
                                                                    Args.Width.GetValueOrDefault(),
                                                                    Args.Height.GetValueOrDefault(),
                                                                    out spriteBase64LowQuality);
                                    
                                }

                                Args.Request.SpriteUrl = ImagesManager.GetExternalUrl(externalFilePath);
                                Args.Request.SpriteBase64LowQuality = spriteBase64LowQuality;

                                if (null != realRequest)
                                {
                                    ImagesManager.InsertSprite(realRequest.RequestId, realRequest.EffectType,
                                                                realRequest.SpriteUrl,
                                                                spriteBase64LowQuality, realRequest.ImageId,
                                                                realRequest.CorrectIndex,
                                                                realRequest.CountOfFrames, Args.ClientIp,
                                                                Args.Width.GetValueOrDefault(),
                                                                Args.Height.GetValueOrDefault(),
                                                                Args.DifficultyLevelId);
                                }
                            }
                            catch (FileNotFoundException ex)
                            {
                                Args.Request.SpriteFilePath = null;
                                Args.Request.SpriteUrl = null;
                                Log.Warn("Process: Image not found", ex);
                                result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.ImageNotFound);
                            }
                            catch (Exception ex)
                            {
                                Log.ErrorException("Process: ImagesManager.GenerateSprite failed", ex);
                                Args.Request.SpriteFilePath = null;
                                Args.Request.SpriteUrl = null;
                                RequestsManager.RemoveFromCache(Args.RequestGuid.ToString());
                                throw;
                            }
                        }   
                    }

                    if (null == result && null != realRequest && !realRequest.IsDemo && null != realRequest.ImageExternalId)
                    {
                        var image = ElasticSearchFactory.ElasticClient.Get<ImageArgs>(query => query.Index("images").Type("image").Id(realRequest.ImageSource + "_" + realRequest.ImageExternalId));
                        if (null != image)
                        {
                            var owner = ElasticSearchFactory.ElasticClient.Get<OwnerArgs>(query => query.Index("flickr").Type("user").Id(image.OwnerId));
                            if (null != owner)
                            {
                                realRequest.PhotoBy = owner.RealName;
                                realRequest.PhotoByUrl = owner.ProfileUrl;
                            }
                        }
                    }

                    if (null == result && null != realRequest && !realRequest.IsDemo)
                    {
                        Task.Run(() => { new CaptchaAttackDetectionFlow(realRequest).Process(); });
                    }
                }
                catch (Exception ex)
                {
                    Log.ErrorException("Process: Unexpeceted error occured", ex);
                    result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.GeneralError, description: ex.ToString());
                }
            }

            return result;
        }
    }
}