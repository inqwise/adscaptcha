using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using CoreWrapper;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Dal;
using Inqwise.AdsCaptcha.Dal.Cache;
using Inqwise.AdsCaptcha.Entities;
using Inqwise.AdsCaptcha.SystemFramework;
using NLog;
using Encoder = System.Drawing.Imaging.Encoder;

namespace Inqwise.AdsCaptcha.Managers
{
    public static class ImagesManager
    {
        private const long DEFAULT_SPRITE_QUALITY = 70L;
        private const long DEFAULT_SPRITE_LOW_QUALITY = 0L;        
        private const bool DEFAULT_SPRITE_MAKE_LOW_QUALITY = true;
        private static readonly Lazy<string> _hostBaseUrl = new Lazy<string>(() => ConfigurationManager.AppSettings["AWSBucketUrl"]);

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private static readonly Lazy<AmazonHelper> _amazon = new Lazy<AmazonHelper>();

        public const string JPEG_CONTENT_TYPE = "image/jpeg";
        public const string PNG_CONTENT_TYPE = "image/png";
        public const string GIF_CONTENT_TYPE = "image/gif";

        public static readonly Lazy<ImageCodecInfo> JpegEncoder = new Lazy<ImageCodecInfo>(() => GetEncoder(ImageFormat.Jpeg));
        public static readonly Lazy<ImageCodecInfo> GifEncoder = new Lazy<ImageCodecInfo>(() => GetEncoder(ImageFormat.Gif));
        public static readonly Lazy<ImageCodecInfo> PngEncoder = new Lazy<ImageCodecInfo>(() => GetEncoder(ImageFormat.Png));
        //public static readonly Lazy<EncoderParameters> EncoderParameters = new Lazy<EncoderParameters>(() => new EncoderParameters { Param = new[] { new EncoderParameter(Encoder.Quality, SpriteQuality) } });
        public static readonly Lazy<EncoderParameters> LowQualityEncoderParameters = new Lazy<EncoderParameters>(() => new EncoderParameters { Param = new[] { new EncoderParameter(Encoder.Quality, SpriteLowQuality) } });
        public static readonly Lazy<string[]> AllowedContextTypes = new Lazy<string[]>(() => new[] { JPEG_CONTENT_TYPE });
        public static readonly Lazy<Tuple<int, int>[]> AllowedDimensions = new Lazy<Tuple<int, int>[]>(() => new[] { new Tuple<int, int>(300, 250) });

        private static readonly Lazy<ConcurrentDictionary<long, EncoderParameters>> EncoderParametersSet = new Lazy<ConcurrentDictionary<long, EncoderParameters>>();

        private static EncoderParameters GetEncoderParameters(long? quality)
        {
            if (null == quality) quality = SpriteQuality;

            return EncoderParametersSet.Value.GetOrAdd(quality.Value, (q) =>
                                                       new EncoderParameters
                                                           {
                                                               Param = new[]
                                                                       {
                                                                           new EncoderParameter(Encoder.Quality, q)
                                                                       }
                                                           });
        }

        private static readonly TimeSpan DEFAULT_TMP_IMAGE_CACHE_TIMEOUT = TimeSpan.FromMinutes(30);
        private static readonly TimeSpan DEFAULT_BINARY_CACHE_TIMEOUT = TimeSpan.FromSeconds(30);

        private static readonly Lazy<CacheManager<NewImageArgs>> _cachedTmpImages =
            new Lazy<CacheManager<NewImageArgs>>(
                () => new CacheManager<NewImageArgs>(DEFAULT_TMP_IMAGE_CACHE_TIMEOUT, ExpirationType.Sliding, CachedTmpImageInfoRemoved));

        private static readonly Lazy<CacheManager<byte[]>> _cachedBinary =
            new Lazy<CacheManager<byte[]>>(
                () => new CacheManager<byte[]>(DEFAULT_BINARY_CACHE_TIMEOUT));

        private static void CachedTmpImageInfoRemoved(NewImageArgs fileInfo)
        {
            if (null == fileInfo.TouchDate)
            {
                Log.Info("Tmp image removed from cache '{0}'", fileInfo.AbsoluteFilePath);
                try
                {
                    File.Delete(fileInfo.AbsoluteFilePath);
                }
                catch (Exception ex)
                {
                    Log.ErrorException("CachedTmpImageInfoRemoved: Unable to delete file: " + fileInfo.AbsoluteFilePath, ex);
                } 
            }
        }

        private static readonly Lazy<string> _imagesBaseFolder = new Lazy<string>(() => ConfigurationManager.AppSettings["ImagesBaseFolder"]);

        private static readonly Lazy<long> _spriteQuality = new Lazy<long>(() =>
            {
                long spriteQuality;
                if (!long.TryParse(ConfigurationManager.AppSettings["Sprite.Quality"], out spriteQuality))
                {
                    spriteQuality = DEFAULT_SPRITE_QUALITY;
                }
                return spriteQuality;
            });

        private static readonly Lazy<long> _spriteLowQuality = new Lazy<long>(() =>
        {
            long spriteLowQuality;
            if (!long.TryParse(ConfigurationManager.AppSettings["Sprite.LowQuality"], out spriteLowQuality))
            {
                spriteLowQuality = DEFAULT_SPRITE_LOW_QUALITY;
            }
            return spriteLowQuality;
        });

        private static readonly Lazy<bool> _spriteMakeLowQuality = new Lazy<bool>(() =>
        {
            bool makeLowQuality;
            if (!bool.TryParse(ConfigurationManager.AppSettings["Sprite.LowQuality.Enable"], out makeLowQuality))
            {
                makeLowQuality = DEFAULT_SPRITE_MAKE_LOW_QUALITY;
            }
            return makeLowQuality;
        });


        public static string ImagesBaseFolder
        {
            get
            {
                if (null == _imagesBaseFolder.Value)
                {
                    throw new ArgumentNullException("ImagesBaseFolder");
                }
                return _imagesBaseFolder.Value;
            }
        }

        public static long SpriteQuality
        {
            get { return _spriteQuality.Value; }
        }

        public static long SpriteLowQuality
        {
            get { return _spriteLowQuality.Value; }
        }

        public static string HostBaseUrl
        {
            get { return _hostBaseUrl.Value; }
        }

        public static AdsCaptchaOperationResult<IImage> Get(long? imageId, long? adId, int? advertiserId)
        {
            AdsCaptchaOperationResult<IImage> result = null;
            using (var reader = ImagesDataAccess.GetReader(imageId, adId, advertiserId))
            {
                result = reader.Read() ? new ImageEntity(reader) : AdsCaptchaOperationResult<IImage>.ToError(AdsCaptchaErrors.NoResults);
                return result;
            }
        }

        public static string GetExternalUrl(string filePath)
        {
            return HostBaseUrl + filePath;
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

        public static string GenerateSprite(string imagePath, int countOfFrames, int correctIndex, EffectTypes effectType, string externalFilePath, int width, int height, out string lowQualitySpriteBase64)
        {
            long size;
            Bitmap image;
            
            image = GetImage(imagePath, width, height, true, out size);

            return GenerateSprite(countOfFrames, correctIndex, effectType, externalFilePath, width, height, out lowQualitySpriteBase64, size, image);
        }

        public static string GenerateSpriteFromCachedImage(string key, int countOfFrames, int correctIndex, EffectTypes effectType, string externalFilePath, int width, int height, out string lowQualitySpriteBase64)
        {
            long size;
            Bitmap image;

            image = GetImageFromCache(key, width, height, true, out size);

            return GenerateSprite(countOfFrames, correctIndex, effectType, externalFilePath, width, height, out lowQualitySpriteBase64, size, image);
        }

        public static string GenerateSprite(int countOfFrames, int correctIndex, EffectTypes effectType,
                                             string externalFilePath, int width, int height, out string lowQualitySpriteBase64,
                                             long size, Bitmap image)
        {
            lowQualitySpriteBase64 = null;
            Stopwatch sw = Stopwatch.StartNew();
            long quality = (101 - ((width*height)*3)/size);
            Bitmap sprite;
            try
            {
                switch (effectType)
                {
                    case EffectTypes.Swirl:
                        sprite = GetSwirlSprite(countOfFrames, correctIndex, image);
                        break;
                    default:
                        sprite = GetAccordionChameleonSprite(countOfFrames, correctIndex, image);
                        break;
                }
            }
            finally
            {
                if (null != image) image.Dispose();
            }

            sw.Stop();
            if (sw.ElapsedMilliseconds > 4000)
            {
                if (sw.ElapsedMilliseconds > 6000)
                {
                    Log.Error("GenerateSprite: 1: {0}", sw.ElapsedMilliseconds);
                }
                else
                {
                    Log.Warn("GenerateSprite: 1: {0}", sw.ElapsedMilliseconds);
                }
            }
            sw.Restart();

            var memoryStream = new MemoryStream();
            MemoryStream tinyStream = null;
            if (_spriteMakeLowQuality.Value)
            {
                tinyStream = new MemoryStream();
            }

            try
            {
                Task<string> t2 = null;
                Task t1;
                try
                {
                    sprite.Save(memoryStream, JpegEncoder.Value, GetEncoderParameters(quality));
                    t1 = Task.Factory.StartNew(() =>
                        {
                            _amazon.Value.Upload(memoryStream, externalFilePath, JPEG_CONTENT_TYPE);
                            memoryStream.Close();
                        });

                    if (_spriteMakeLowQuality.Value)
                    {
                        var thumbnail = sprite.GetThumbnailImage(width, height*countOfFrames, null, IntPtr.Zero);
                        t2 = Task<string>.Factory.StartNew(() =>
                            {
                                {
                                    thumbnail.Save(tinyStream, JpegEncoder.Value, LowQualityEncoderParameters.Value);
                                    string result = null;
                                    result = Convert.ToBase64String(tinyStream.ToArray());
                                    tinyStream.Close();
                                    thumbnail.Dispose();
                                    return result;
                                }
                            });
                    }
                }
                finally
                {
                    sprite.Dispose();
                }

                if (t2 != null) t2.Wait();
                t1.Wait();
                if (t2 != null) lowQualitySpriteBase64 = t2.Result;
            }
            finally
            {
                memoryStream.Dispose();
                if (tinyStream != null) tinyStream.Dispose();
            }

            sw.Stop();
            if (sw.ElapsedMilliseconds > 500)
            {
                Log.Warn("GenerateSprite: 2: {0}", sw.ElapsedMilliseconds);
            }

            return GetExternalUrl(externalFilePath);
        }

        private static Bitmap GetImageFromCache(string key, int width, int height, bool fromCache, out long size)
        {
            var hashcode = (key + width + height).GetHashMd5();

            byte[] imageData;
            if (fromCache)
            {
                imageData = _cachedBinary.Value.GetOrAddCachedItem(hashcode, () =>
                {
                    return GetFittedImageDataFromCache(key, width, height);
                });
            }
            else
            {
                imageData = GetFittedImageDataFromCache(key, width, height);
            }

            size = imageData.LongLength;

            Bitmap result;
            using (var stream = new MemoryStream(imageData, false))
            {
                result = new Bitmap(stream);
            }

            return result;
        }

        private static Bitmap GetImage(string imagePath, int width, int height, bool fromCache, out long size)
        {
            var hashcode = (imagePath + width + height).GetHashMd5();
            
            byte[] imageData;
            if (fromCache)
            {
                imageData = _cachedBinary.Value.GetOrAddCachedItem(hashcode, () =>
                        {
                            if (String.IsNullOrEmpty(imagePath))
                            {
                                throw new ArgumentNullException("imagePath");
                            }

                            if (!File.Exists(imagePath))
                            {
                                throw new FileNotFoundException("File not found", imagePath);
                            }

                            return GetFittedImageData(imagePath, width, height);
                        }); 
            }
            else
            {
                imageData = GetFittedImageData(imagePath, width, height);
            }

            size = imageData.LongLength;

            Bitmap result;
            using (var stream = new MemoryStream(imageData, false))
            {
                result = new Bitmap(stream);
            }

            return result;
        }

        private static byte[] GetFittedImageDataFromCache(string key, int width, int height)
        {
            byte[] data;
            using (var ms = new MemoryStream())
            {
                try
                {
                    var originalImageData = CacheBuilder.GetCache().Get<byte[]>(key);
                    if (null == originalImageData)
                    {
                        throw new TimeoutException("Image not found");
                    }
                    using (var originalImage = Image.FromStream(new MemoryStream(originalImageData)))
                    {
                        long quality = (101 - ((originalImage.Width * originalImage.Height) * 3) / originalImageData.Length);
                        if (originalImage.Width > width || originalImage.Height > height)
                        {
                            using (var resizedImage = originalImage.GetThumbnailImage(width, height, null, IntPtr.Zero))
                            {
                                resizedImage.Save(ms, JpegEncoder.Value, GetEncoderParameters(quality));
                            }
                        }
                        else
                        {
                            originalImage.Save(ms, JpegEncoder.Value, GetEncoderParameters(quality));
                        }
                    }

                    data = ms.GetBuffer();
                }
                finally
                {
                    ms.Close();
                }
            }
            return data;
        }

        private static byte[] GetFittedImageData(string imagePath, int width, int height)
        {
            byte[] data;
            using (var ms = new MemoryStream())
            {
                try
                {
                    using (var originalImage = Image.FromFile(imagePath))
                    {
                        var fi = new FileInfo(imagePath);
                        long quality = (101 - ((originalImage.Width * originalImage.Height) * 3) / fi.Length);
                        if (originalImage.Width > width || originalImage.Height > height)
                        {
                            using (var resizedImage = originalImage.GetThumbnailImage(width, height, null, IntPtr.Zero))
                            {
                                resizedImage.Save(ms, JpegEncoder.Value, GetEncoderParameters(quality));
                            }
                        }
                        else
                        {
                            originalImage.Save(ms, JpegEncoder.Value, GetEncoderParameters(quality));
                        }
                    }

                    data = ms.GetBuffer();
                }
                finally
                {
                    ms.Close();
                }
            }
            return data;
        }

        private static Bitmap GetSwirlSprite(int countOfFrames, int correctIndex, string imagePath)
        {
            Bitmap sprite;
            using (var transformator = new ManagedAccordionChameleon(imagePath))
            {
                sprite = GetSwirlSprite(countOfFrames, correctIndex, transformator);
            }

            return sprite;
        }

        private static Bitmap GetSwirlSprite(int countOfFrames, int correctIndex, Bitmap image)
        {
            Bitmap sprite;
            using (var transformator = new ManagedAccordionChameleon(image))
            {
                sprite = GetSwirlSprite(countOfFrames, correctIndex, transformator);
            }
            return sprite;
        }

        private static Bitmap GetSwirlSprite(int countOfFrames, int correctIndex, ManagedAccordionChameleon transformator)
        {
            List<Bitmap> transformations;
            transformator.ComputeTwists(ManagedAccordionChameleon.Difficulty.EASY, countOfFrames, out transformations,
                                        correctIndex);

            var originalImage = transformator.GetImage();
            var heightStep = originalImage.Height;
            var sprite = new Bitmap(originalImage.Width, heightStep*countOfFrames);
            using (Graphics g = Graphics.FromImage(sprite))
            {
                for (var i = 0; i < transformations.Count; i++)
                {
                    var transformation = transformations[i];
                    g.DrawImage(transformation, 0, heightStep*i);
                    transformations[i].Dispose();
                }

                transformations.Clear();
            }

            transformator.Dispose();
            return sprite;
        }

        private static Bitmap GetAccordionChameleonSprite(int countOfFrames, int correctIndex, Bitmap image)
        {
            Bitmap sprite;
            using (var transformator = new ManagedAccordionChameleon(image))
            {
                sprite = GetAccordionChameleonSprite(countOfFrames, correctIndex, transformator);
            }

            return sprite;
        }

        private static Bitmap GetAccordionChameleonSprite(int countOfFrames, int correctIndex, string imagePath)
        {
            Bitmap sprite;
            using (var transformator = new ManagedAccordionChameleon(imagePath))
            {
                sprite = GetAccordionChameleonSprite(countOfFrames, correctIndex, transformator);
            }
            
            return sprite;
        }

        private static Bitmap GetAccordionChameleonSprite(int countOfFrames, int correctIndex,
                                                          ManagedAccordionChameleon transformator)
        {
            Bitmap sprite;
            List<List<int>> accordions;
            transformator.ComputeAccordions(ManagedAccordionChameleon.Difficulty.EASY, countOfFrames, out accordions,
                                            correctIndex, 1, 1);
            using (var originalImage = transformator.GetImage())
            {
                var heightStep = originalImage.Height;
                sprite = new Bitmap(originalImage.Width, heightStep*countOfFrames);

                using (Graphics g = Graphics.FromImage(sprite))
                {
                    for (int i = 0; i < accordions.Count; i++)
                    {
                        var accordion = accordions[i];
                        using (var accordeonImage = transformator.RequestCaptcha(accordion, correctIndex - i))
                        {
                            g.DrawImage(accordeonImage, 0, heightStep*i);
                            accordeonImage.Dispose();
                        }
                    }
                }
            }
            return sprite;
        }

        public static void RemoveHostedImage(string filePath)
        {
            try
            {
               Task.Run(()=> _amazon.Value.Delete(filePath));
            }
            catch (Exception ex)
            {
                Log.ErrorException("RemoveHostedImage : unexpected error occured", ex);
                throw;
            }
        }

        public static DsImages GetMeny(DateTime? fromDate, DateTime? toDate, IEnumerable<ImageType> types, ImageStatus? status, bool deleted = false)
        {
            return ImagesDataAccess.GetMeny(fromDate, toDate, types, deleted, status);
        }

        public static AdsCaptchaOperationResult Delete(long id, int? advertiserId)
        {
            AdsCaptchaOperationResult result = null;
            try
            {
                IImage image;
                AdsCaptchaOperationResult<IImage> imageResult = Get(id, null, advertiserId);
                if (imageResult.HasError)
                {
                    result = imageResult;
                }
                else
                {
                    image = imageResult.Value;
                    ImagesDataAccess.Delete(id);
                    string imageAbsolutePath = GetImageAbsolutePath(image.Path);
                    try
                    {
                        File.Delete(imageAbsolutePath);
                    }
                    catch (Exception ex)
                    {
                        Log.WarnException("Delete: Failed to delete local file: " + imageAbsolutePath, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Delete: Unexpected error occured. Image #" + id, ex);
                result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.GeneralError, description: ex.ToString());
            }

            return result;
        }

        public static AdsCaptchaOperationResult AddTemp(NewImageArgs fileInfo)
        {
            AdsCaptchaOperationResult result = null;
            bool rollback = false;

            try
            {
                IdentifyDetails(fileInfo);

                if (!fileInfo.HasError)
                {
                    Validate(fileInfo);
                }

                if (!fileInfo.HasError)
                {
                    _cachedTmpImages.Value.AddToCache(fileInfo.RelativeFilePath, fileInfo);
                    
                    // Add file to distributed cache
                    CacheBuilder.GetCache().Add(fileInfo.RelativeFilePath, File.ReadAllBytes(fileInfo.AbsoluteFilePath));
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("AddTemp: Unexpected error occured", ex);
                rollback = true;
                result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.GeneralError, description: ex.ToString());
            }

            if (rollback) // ResourceFile not created
            {
                Rollback(fileInfo);
            }

            return result ?? AdsCaptchaOperationResult.Ok;
        }

        public static NewImageArgs RemoveTmp(string cacheKeyName)
        {
            // Remove from distributed cache
            CacheBuilder.GetCache().Remove(cacheKeyName);

            // Remove from local cache
            return _cachedTmpImages.Value.RemoveCachedItem(cacheKeyName);
        }

        public static AdsCaptchaOperationResult<long> Add(NewImageArgs fileInfo)
        {
            AdsCaptchaOperationResult<long> result = null;
            bool rollback = false;

            try
            {
                IdentifyDetails(fileInfo);

                if (!fileInfo.HasError)
                {
                    Validate(fileInfo);
                }

                if (!fileInfo.HasError)
                {
                    Collect(fileInfo);
                }

                // Save
                if (fileInfo.HasError)
                {
                    result = AdsCaptchaOperationResult<long>.ToError(fileInfo.Error.Value);
                }
                else
                {
                    result = ImagesDataAccess.Insert(fileInfo.RelativeFilePath, fileInfo.Width, fileInfo.Height,
                                                   (int) fileInfo.ImageType, fileInfo.Extension, fileInfo.ContentType, fileInfo.AdvertiserId, fileInfo.AdId, fileInfo.CampaignId);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Add: Unexpected error occured", ex);
                rollback = true;
                result = AdsCaptchaOperationResult<long>.ToError(AdsCaptchaErrors.GeneralError, description: ex.ToString());
            }

            if (rollback) // ResourceFile not created
            {
                Rollback(fileInfo);
            }

            return result;
        }

        private static void Validate(NewImageArgs fileInfo)
        {
            if (null != fileInfo.AllowedContentTypes && !fileInfo.AllowedContentTypes.Contains(fileInfo.ContentType))
            {
                Log.Warn("ContentType '{0}' not allowed", fileInfo.ContentType);
                fileInfo.Error = AdsCaptchaErrors.InvalidFormat;
            }

//#if DEBUG
            if (!fileInfo.HasError && null != fileInfo.AllowedDimansions &&
                    !fileInfo.AllowedDimansions.Any(d => d.Item1 == fileInfo.Width && d.Item2 == fileInfo.Height))
            {
                Log.Warn("Dimension '{0}X{1}' not allowed", fileInfo.Width, fileInfo.Height);
                fileInfo.Error = AdsCaptchaErrors.InvalidDimension;
            } 
//#endif

            if (!fileInfo.HasError && fileInfo.CountOfFrames > 1)
            {
                Log.Warn("Multi-frame not allowed");
                fileInfo.Error = AdsCaptchaErrors.InvalidFormat;
            }
        }

        private static void Collect(NewImageArgs fileInfo)
        {
            var fileName = Guid.NewGuid().ToString().ToLower().Replace("-", "") + fileInfo.Extension;
            var subfolder = String.Format("cap\\{0:yyyyMM}", DateTime.Now);

            fileInfo.RelativeFilePath = Path.Combine(subfolder, fileName);
            var filePath = Path.Combine(ImagesBaseFolder, fileInfo.RelativeFilePath);
            // Move file
            var folder = Path.Combine(ImagesBaseFolder, subfolder);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            File.Move(fileInfo.AbsoluteFilePath, filePath);
            fileInfo.AbsoluteFilePath = filePath;
        }

        private static void Rollback(NewImageArgs fileInfo)
        {
            File.Delete(fileInfo.AbsoluteFilePath);
        }

        private static AdsCaptchaOperationResult IdentifyDetails(NewImageArgs fileInfo)
        {
            AdsCaptchaOperationResult result = null;
            try
            {
                if (null == fileInfo.AbsoluteFilePath)
                {
                    fileInfo.AbsoluteFilePath = GetTmpImageAbsolutePath(fileInfo.RelativeFilePath);
                }
                
                using (Image image = Image.FromFile(fileInfo.AbsoluteFilePath))
                {
                    fileInfo.Width = image.Width;
                    fileInfo.Height = image.Height;
                    fileInfo.ContentType = GetMimeType(image);
                    var frameDimensions = new FrameDimension(image.FrameDimensionsList[0]);
                    int frames = image.GetFrameCount(frameDimensions);
                    fileInfo.CountOfFrames = frames;
                }

                fileInfo.Extension = Path.GetExtension(fileInfo.Name);
            }
            catch (Exception ex)
            {
                Log.ErrorException("IdentifyImageDetails : Unexpected error occured", ex);
                fileInfo.Error = AdsCaptchaErrors.InvalidFormat;
                result = AdsCaptchaOperationResult.ToError(fileInfo.Error.Value, description: ex.ToString());
            }

            return result ?? AdsCaptchaOperationResult.Ok;
        }

        private static string GetTmpImageAbsolutePath(string relativeFilePath)
        {
            return Path.Combine(ApplicationConfiguration.TempFolderPath.Value, relativeFilePath);
        }

        public static string GetMimeType(Image i)
        {
            var imgguid = i.RawFormat.Guid;
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
            {
                if (codec.FormatID == imgguid)
                    return codec.MimeType;
            }
            return "image/unknown";
        }

        public static string GetImageAbsolutePath(string relativeFilePath, bool isTemp = false)
        {
            return Path.Combine((isTemp ? ApplicationConfiguration.TempFolderPath.Value : ImagesBaseFolder), relativeFilePath);
        }

        public static NewImageArgs GetTmp(string cacheKeyName)
        {
            return _cachedTmpImages.Value.GetCachedItem(cacheKeyName);
        }

        private const int MILISECONDS_IN_5_MINUTES = 1000 * 60 * 5;
        private static Lazy<Timer> _toBeDeletedImagesChacker = new Lazy<Timer>(()=> new Timer(MILISECONDS_IN_5_MINUTES));
        private static void CheckForToBeDeletedImages(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            try
            {
                // Get all ToBeDeleted images
                var ds = GetMeny(null, null, null, null, true);
                foreach (var image in ds.Images)
                {
                    Delete(image.ImageId, null);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("CheckForDeletedImages : Unexpected error occured", ex);
            }
        }

        public static void StartToBeDeletedImagesJob()
        {
            if (!_toBeDeletedImagesChacker.IsValueCreated)
            {
                _toBeDeletedImagesChacker.Value.Elapsed += CheckForToBeDeletedImages;
                _toBeDeletedImagesChacker.Value.Start();
            }
        }

        public static void StopToBeDeletedImagesJob()
        {
            if (_toBeDeletedImagesChacker.IsValueCreated)
            {
                _toBeDeletedImagesChacker.Value.Elapsed -= CheckForToBeDeletedImages;
                _toBeDeletedImagesChacker.Value.Stop();
                _toBeDeletedImagesChacker.Value.Close();
            }
        }

        public static void InsertSprite(long requestId, EffectTypes effectType, String spriteUrl, String spriteBase64, long imageId, int correctFrameIndex, int framesCount, string clientIp, int width, int height, int? difficultyLevelId)
        {
            Task.Run(() => ImagesDataAccess.InsertSprite(requestId, (int)effectType, spriteUrl, spriteBase64, imageId, correctFrameIndex, framesCount, clientIp, width, height, difficultyLevelId));
        }

        public static void ChangeStatus(long imageId, int imageStatusId)
        {
            ImagesDataAccess.ChangeStatus(imageId, imageStatusId);
        }
    }
}
