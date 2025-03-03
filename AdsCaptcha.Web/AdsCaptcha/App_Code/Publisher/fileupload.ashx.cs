using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Configuration;
using System.IO;
using System.Drawing;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;
using System.Drawing.Imaging;
using Inqwise.AdsCaptcha.Common;


namespace Inqwise.AdsCaptcha.Publisher
{
    /// <summary>
    /// Summary description for $CodeFileclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class fileupload : IHttpHandler
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }

        [Obsolete("Refactoring", true)]
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var request = context.Request;
            var formUpload = request.Files.Count > 0;

            // find filename
            var xFileName = request.Headers["X-File-Name"];
            var qqFile = request["qqfile"];
            var formFilename = formUpload ? request.Files[0].FileName : null;

            if (!string.IsNullOrEmpty(formFilename))
            {
                string adId = string.Empty;

                int width = 0;
                int height = 0;
                int imgID = -1;

                string fileUrl = ConfigurationSettings.AppSettings["AWSBucketUrl"];
                string fileName = string.Empty;

                string strResponse = "error";
                string strMIMEType = string.Empty;
                try
                {
                    bool isError = false;

                    string strExtension = Path.GetExtension(formFilename).ToLower();
                    switch (strExtension)
                    {
                        case ".gif":
                            strMIMEType = "image/gif";
                            break;

                        case ".jpg":
                            strMIMEType = "image/jpeg";
                            break;

                        case ".jpeg":
                            strMIMEType = "image/jpeg";
                            break;
                    }


                    if (strMIMEType == string.Empty)
                    {
                        strResponse = "File type not supported";
                        isError = true;
                    }

                    try
                    {
                        Image bmp = Image.FromStream(context.Request.Files[0].InputStream);
                        if (!AdBLL.IsDimensionValid((int)AdType.Slide2Fit, bmp.Width, bmp.Height))
                        {
                            strResponse = "File dimensions need to be 300x250 or 180x150 (WxH)";
                            isError = true;
                        }
                    }
                    catch(Exception ex)
                    {
                        Log.ErrorException("fileupload : Unexpected error occured", ex);
                        strResponse = "Error occured, please try again";
                        isError = true;
                    }


                    if (context.Request.Files[0].InputStream.Length > Constants.ALLOWED_IMAGE_SIZE)
                    {
                        strResponse = "File size is too big";
                        isError = true;
                    }


                    if (!isError)
                    {
                        // Generate image file name.
                        fileName = (DateTime.UtcNow.Ticks.ToString() + General.GenerateGuid().Replace("-", "")).Substring(0, 45) +
                            System.IO.Path.GetExtension(context.Request.Files[0].FileName);
                        using (Bitmap bitmap = new Bitmap(context.Request.Files[0].InputStream, false))
                        {
                            // Get image dimensions.
                            width = bitmap.Width;
                            height = bitmap.Height;

                            float newWidth = width;
                            float newHeight = height;


                            // Create new Bitmap at new dimensions
                            Bitmap result = new Bitmap((int)newWidth, (int)newHeight);

                            using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
                            {
                                g.DrawImage(bitmap, 0, 0, (int)newWidth, (int)newHeight);
                            }

                            string contentType = "image/jpg";
                            ImageCodecInfo imageCodec = GetEncoder(ImageFormat.Jpeg);
                            Encoder encoder = Encoder.Quality;
                            EncoderParameters encoderParams = new EncoderParameters(1);
                            EncoderParameter encoderParam = new EncoderParameter(encoder, 100L);
                            encoderParams.Param[0] = encoderParam;

                            MemoryStream ms = new MemoryStream();

                            width = (int)newWidth;
                            height = (int)newHeight;

                            result.Save(ms, imageCodec, encoderParams);
                            //AmazonAWS aws = new AmazonAWS();
                            //aws.Upload(ms, fileName, width, height, contentType);

                            //ImageProcessingClient uploader = new ImageProcessingClient();
                            //var uploader = new ImageProcessing.ImageProcessingClient();
                            bool tempImgBool;
                            width = string.IsNullOrEmpty(context.Request["w"]) ? width : Convert.ToInt32(context.Request["w"]);
                            height = string.IsNullOrEmpty(context.Request["h"]) ? height : Convert.ToInt32(context.Request["h"]);

                            //imgID = uploader.CreateImageDeformationsFromStream(ms.ToArray(), 10, width, height);


                            ms.Close();
                        }

                        // Calculate preview dimensions.
                        int PREVIEW_WIDTH = 250;
                        float ratio;

                        int previewWidth = width;
                        int previewHeight = height;

                        if (width > PREVIEW_WIDTH)
                        {
                            ratio = (float)width / (float)PREVIEW_WIDTH;
                            previewHeight = (int)((float)height / ratio);
                        }
                        else
                        {
                            ratio = (float)PREVIEW_WIDTH / (float)width;
                            previewWidth = (int)((float)height * ratio);
                        }
                        previewWidth = PREVIEW_WIDTH;



                        //byte[] docBytes = new byte[context.Request.Files[0].InputStream.Length];
                        //context.Request.Files[0].InputStream.Read(docBytes, 0, docBytes.Length);
                        //UploadFile(docBytes, strMIMEType, strFileName);
                        if (imgID > 0)
                            strResponse = "{ \"success\": \"success\", \"imgID\":\"" + imgID + "\", \"thumb\":\"" +
                                          Path.Combine(fileUrl, fileName) + "\" }";
                        else
                        {
                            strResponse = "{ \"error\": \"Error occured, please try again\" }";
                            Log.Warn("fileupload: imgID is {0}", imgID);
                        }
                    }
                    else
                    {
                        strResponse = "{ \"error\": \"" + strResponse + "\" }";
                    }


                }
                catch(Exception ex)
                {
                    Log.ErrorException("fileupload : Unexpected error occured", ex);
                    strResponse = "{ \"error\": \"Error occured, please try again\" }";
                }

                

                //UploadResult upresult = new UploadResult();
                //upresult.Result = strResponse;
                //if (strResponse == "success")
                //{
                //    upresult.Message = "Image successfully uploaded";
                //    upresult.AdId = adId;
                //    upresult.Width = width;
                //    upresult.Height = height;
                //    upresult.FileName = fileName;
                //    upresult.FileUrl = fileUrl;
                //}


                //var json = new JavaScriptSerializer();
                context.Response.ContentType = "text/plain";
                context.Response.Write(strResponse);

            }
        


           
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
