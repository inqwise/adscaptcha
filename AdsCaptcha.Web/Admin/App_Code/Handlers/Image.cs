using System;
using System.IO;
using System.Web;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Managers;

namespace Admin.Handlers
{
    public class Image : BaseHandler, IHttpHandler
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
                IImage image = null;
                long? imageId = null;
                long tmpImageId;
                if (long.TryParse(request["imageId"], out tmpImageId))
                {
                    imageId = tmpImageId;
                }

                long? adId = null;
                long tmpAdId;
                if (long.TryParse(request["adId"], out tmpAdId))
                {
                    adId = tmpAdId;
                }
                
                if(null == adId && null == imageId)
                {
                    result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.NoResults);
                }


                if (null == result)
                {
                    AdsCaptchaOperationResult<IImage> imageResult = ImagesManager.Get(imageId, adId, null);
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
                    var filePath = Path.Combine(ImagesManager.ImagesBaseFolder, image.Path);

                    response.Clear();
                    response.ContentType = image.ContentType;
                    response.WriteFile(filePath);
                }

            }
            catch (Exception ex)
            {
                Log.ErrorException("Image : Unexpected error occured", ex);
                response.Clear();
                response.StatusCode = 500;
                response.StatusDescription = GetGeneralErrorString(ex.ToString());
            }

            response.End();
        }

        public bool IsReusable {
            get { return false; }
        }
    }
}