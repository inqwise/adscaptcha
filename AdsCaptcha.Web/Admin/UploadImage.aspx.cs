using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;
using System.IO;
using System.Web;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class UploadImage : System.Web.UI.Page
    {
        private int mAdType = 0;
        private const string SCRIPT_TEMPLATE = "<script type='text/javascript'>window.parent.photoUploadComplete('{0}', {1}, '{2}', '{3}', {4}, {5});</script>";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                UploadPhoto();
            }
        }

        private void UploadPhoto()
        {
            string errorMessage = null;
            string script = string.Empty;

            // Check if file to uplad selected.
            if (uploadImage.PostedFile == null || uploadImage.PostedFile.ContentLength == 0)
            {
                script = string.Format(SCRIPT_TEMPLATE, "Requierd", "true", "", "", 0, 0);
            }
            else
            {
                // Check if uploaded file is a valid image.
                if (!IsImageValid(uploadImage.PostedFile, ref errorMessage))
                {
                    script = string.Format(SCRIPT_TEMPLATE, errorMessage, "true", "", "", 0, 0);
                }
            }

            if (string.IsNullOrEmpty(script))
            {
                try
                {
                    int width;
                    int height;

                    // Generate image file name.
                    string fileName = (DateTime.UtcNow.Ticks.ToString() + General.GenerateGuid().Replace("-", "")).Substring(0, 45) + System.IO.Path.GetExtension(uploadImage.PostedFile.FileName);

                    using (Bitmap bitmap = new Bitmap(uploadImage.PostedFile.InputStream, false))
                    {
                        // Get image dimensions.
                        width = bitmap.Width;
                        height = bitmap.Height;

                        float newWidth = width;
                        float newHeight = height;
                        /*
                        float percent = 0;
                        float percentW = 0;
                        float percentH = 0;

                        // Check if image dimensions is larger than maximum allowed dimensions.
                        if (
                            mAdType == (int)AdType.SloganAndImage && (width > ApplicationConfiguration.MAX_IMAGE_WIDTH || height > ApplicationConfiguration.MAX_IMAGE_HEIGHT)
                            ||
                            mAdType == (int)AdType.Slide2Fit && (width > ApplicationConfiguration.MAX_SLIDER_DIM)
                            )
                        {
                            switch (mAdType)
                            {
                                case (int)AdType.Slide2Fit:
                                    int minSize = (width > ApplicationConfiguration.ALLOWED_SLIDER_DIM[1] ? ApplicationConfiguration.ALLOWED_SLIDER_DIM[1] : ApplicationConfiguration.MIN_SLIDER_DIM);
                                    percentW = minSize / (float)width;
                                    percentH = minSize / (float)height;
                                    break;
                                case (int)AdType.SloganAndImage:
                                default:
                                    percentW = ApplicationConfiguration.MAX_IMAGE_WIDTH / (float)width;
                                    percentH = ApplicationConfiguration.MAX_IMAGE_HEIGHT / (float)height;
                                    break;
                            }

                            if (percentH < percentW)
                                percent = percentH;
                            else
                                percent = percentW;

                            newWidth = width * percent;
                            newHeight = height * percent;

                            switch (mAdType)
                            {
                                case (int)AdType.Slide2Fit:
                                    if (newWidth < ApplicationConfiguration.MIN_SLIDER_DIM) newWidth = ApplicationConfiguration.MIN_SLIDER_DIM;
                                    if (newHeight < ApplicationConfiguration.MIN_SLIDER_DIM) newHeight = ApplicationConfiguration.MIN_SLIDER_DIM;
                                    break;
                                case (int)AdType.SloganAndImage:
                                default:
                                    if (newWidth < ApplicationConfiguration.MIN_IMAGE_WIDTH) newWidth = ApplicationConfiguration.MIN_IMAGE_WIDTH;
                                    if (newHeight < ApplicationConfiguration.MIN_IMAGE_HEIGHT) newHeight = ApplicationConfiguration.MIN_IMAGE_HEIGHT;
                                    break;
                            }
                        }
                        */
                        
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

                    string fileUrl = ConfigurationSettings.AppSettings["AWSBucketUrl"];

                    script = string.Format(SCRIPT_TEMPLATE, "Image successfully uploaded", "false", fileUrl, fileName, width, height);
                }
                catch (Exception ex)
                {
                    NLogManager.logger.ErrorException("UploadImage", ex);
                    script = string.Format(SCRIPT_TEMPLATE, "Error occured, please try again", "true", "", "", 0, 0);
                }
            }

            // Inject the script which will fire when the page is refreshed.
            ClientScript.RegisterStartupScript(this.GetType(), "uploadNotify", script);
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

        /// <summary>
        /// Get posted image dimensions.
        /// </summary>
        /// <param name="file">File.</param>
        /// <param name="width">Image width (OUT).</param>
        /// <param name="height">Image height (OUT).</param>
        public void GetImageDimensions(HttpPostedFile file, out Nullable<int> width, out Nullable<int> height)
        {
            using (Bitmap bitmap = new Bitmap(file.InputStream, false))
            {
                width = bitmap.Width;
                height = bitmap.Height;
            }
        }

        /// <summary>
        /// Validates that file type is allowed (images).
        /// </summary>
        /// <param name="file">File.</param>
        /// <returns>Validation result.</returns>
        private bool ValidateImageType(HttpPostedFile file)
        {
            string fileType = file.ContentType.ToString();

            if (!ApplicationConfiguration.ALLOWED_IMAGE_TYPE_LIST.Contains(fileType))
                return false;

            return true;
        }

        /// <summary>
        /// Validates that file size is allowed.
        /// </summary>
        /// <param name="file">File.</param>
        /// <returns>Validation result.</returns>
        private bool ValidateImageSize(HttpPostedFile file)
        {
            int fileSize = file.ContentLength;

            if (fileSize > ApplicationConfiguration.ALLOWED_IMAGE_SIZE)
                return false;

            return true;
        }

        /// <summary>
        /// Validates that file dimensions are allowed (width & height).
        /// </summary>
        /// <param name="file">File.</param>
        /// <returns>Validation result.</returns>
        private bool ValidateImageDimensions(HttpPostedFile file)
        {
            using (Bitmap bitmap = new Bitmap(file.InputStream, false))
            {
                return AdBLL.IsDimensionValidForAdmin(mAdType, bitmap.Width, bitmap.Height);
            }
        }

        /// <summary>
        /// Check ad type.
        /// </summary>
        /// <param name="t">Ad type</param>
        /// <returns>Return if type supports image.</returns>
        private bool ValidateAdType(string t)
        {
            int type = 0;
            Int32.TryParse(t, out type);

            mAdType = (int)AdType.Slide2Fit;
            return true;

            //switch (type)
            //{
            //    case (int)AdType.SloganAndImage:
            //        mAdType = (int)AdType.SloganAndImage;
            //        return true;
            //    case (int)AdType.Slide2Fit:
            //        mAdType = (int)AdType.Slide2Fit;
            //        return true;
            //    default:
            //        return false;
            //}
        }

        /// <summary>
        /// Check if image is valid.
        /// </summary>
        /// <param name="file">Posted file.</param>
        /// <param name="errorMessage">Error message (OUT).</param>
        /// <returns>Whether image is valid or not.</returns>
        private bool IsImageValid(HttpPostedFile file, ref string errorMessage)
        {
            string type = adType.Value;

            // Check ad type.
            if (!ValidateAdType(type))
            {
                errorMessage = "Ad type not support image";
                return false;
            }

            // Check image type.
            if (!ValidateImageType(file))
            {
                errorMessage = "File type not supported";
                return false;
            }

            // Check image size.
            if (!ValidateImageSize(file))
            {
                errorMessage = "File size is too big";
                return false;
            }

            // Check image dimensions.
            if (!ValidateImageDimensions(file))
            {
                errorMessage = "File dimensions does not fit";
                return false;
            }

            return true;
        }
    }
}
