using System;
using System.IO;
using System.Web;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Managers;

namespace Inqwise.AdsCaptcha.Handlers
{
    public class ImageHandler : BaseHandler, IHttpHandler
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;
            var server = context.Server;
            AdsCaptchaOperationResult result = null;
            try
            {
                string imageId = null;
                long? adId = null;
                ImageType? imageType = null;

                if (null == result)
                {
                    long tmpAdId;
                    if (long.TryParse(request["adId"], out tmpAdId))
                    {
                        adId = tmpAdId;
                    }

                    imageId = request["imageId"];
                    if (string.IsNullOrEmpty(imageId))
                    {
                        imageId = null;
                    }

                    int imageTypeId;
                    if (int.TryParse(request["imageTypeId"], out imageTypeId))
                    {
                        imageType = (ImageType) imageTypeId;
                    }
                     
	            }

                string filePath = null;
                string contentType = null;

                if (imageType == ImageType.Temp)
                {
                    result = GetTmpImagePath(server.UrlDecode(imageId), out filePath, out contentType);
                }
                else
                {
                    if (null == AdvertiserId && null == PublisherId)
                    {
                        result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.NotLoggedIn);
                    }

                    if (null == result)
                    {
                        result = GetImagePath(imageId, adId, out filePath, out contentType); 
                    }
                }

                if (null == result)
                {
                    response.Clear();
                    response.ContentType = contentType;
                    response.WriteFile(filePath);
                }
                else
                {
                    response.Clear();
                    response.ContentType = "application/text";
                    response.StatusCode = 500;
                    response.StatusDescription = result.Error.ToString();
                }

            }
            catch (Exception ex)
            {
                Log.ErrorException("Image : Unexpected error occured", ex);
                response.Clear();
                response.StatusCode = 500;
                //response.StatusDescription = GetGeneralErrorString(ex.ToString());
            }

            response.End();
        }

        private AdsCaptchaOperationResult GetTmpImagePath(string imageId, out string filePath, out string contentType)
        {
            AdsCaptchaOperationResult result = null;

            var imageArgs = ImagesManager.GetTmp(imageId);
            if (null == imageArgs)
            {
                result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.NoResults);
                filePath = null;
                contentType = null;
            }
            else
            {
                filePath = imageArgs.AbsoluteFilePath;
                contentType = imageArgs.ContentType;
            }

            return result;
        }

        private AdsCaptchaOperationResult GetImagePath(string strImageId, long? adId, out string filePath, out string contentType)
        {
            AdsCaptchaOperationResult result = null;
            IImage image = null;
            long? imageId = null;
            
            long tmpImageId;
            if (long.TryParse(strImageId, out tmpImageId))
            {
                imageId = tmpImageId;
            }

	        if (null == imageId && null == adId)
	        {
                result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.NoResults);
	        }
            if (null == result)
            {
                AdsCaptchaOperationResult<IImage> imageResult = ImagesManager.Get(imageId, adId, AdvertiserId);
                if (imageResult.HasError)
                {
                    result = imageResult;
                }
                else
                {
                    image = imageResult.Value;
                }
            }

            if (null == result)
            {
                filePath = Path.Combine(ImagesManager.ImagesBaseFolder, image.Path);
                contentType = image.ContentType;
            }
            else
            {
                filePath = null;
                contentType = null;
            }

            return result;
        }

        public bool IsReusable {
            get { return false; }
        }
    }
}