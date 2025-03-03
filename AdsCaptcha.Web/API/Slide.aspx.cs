using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;

namespace Inqwise.AdsCaptcha.API
{
    public partial class Slide : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string AD_URL = ConfigurationSettings.AppSettings["AWSCloudFront"];

                // Get challange guid.
                string cid = Request.QueryString["cid"];

                //using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                //{
                //    TCS_REQUEST challenge = dataContext.TCS_REQUESTs.Where(i => i.Request_Guid == cid).SingleOrDefault();

                //    // Get slider parameters.
                //    string[] args = challenge.TCS_REQUEST_CHALLENGE.Params.Split(',');
                //    int sliderType = Convert.ToInt16(args[0]);
                //    int sliderWidth = Convert.ToInt16(args[1]);
                //    int sliderHeight = Convert.ToInt16(args[2]);
                //    int sliderX = Convert.ToInt16(args[3]);
                //    int sliderY = Convert.ToInt16(args[4]);
                //    int sliderW = Convert.ToInt16(args[5]);
                //    int sliderH = Convert.ToInt16(args[6]);
                //    int sliderMin = Convert.ToInt16(args[7]);
                //    int sliderMax = Convert.ToInt16(args[8]);

                //    string imageFileName;
                //    string imageURL;

                //    int captchaType = challenge.TCS_REQUEST_PUBLISHER.Type_Id;
                //    bool isCommercial = (challenge.TCS_REQUEST_PUBLISHER.TP_CAPTCHA.IsCommercial == null) ? true : (bool)challenge.TCS_REQUEST_PUBLISHER.TP_CAPTCHA.IsCommercial;
                //    if ((captchaType == (int)CaptchaType.RandomImage) ||
                //        (captchaType == (int)CaptchaType.SecurityOnly))
                //        isCommercial = false;

                //    bool isAdImages = false;
                //    if ((captchaType == (int)CaptchaType.Slider) ||
                //        (isCommercial && (captchaType == (int)CaptchaType.SlideToFit)))
                //        isAdImages = true;

                //    bool isDefaultResize = false;

                //    if (isAdImages)
                //        imageFileName = challenge.TCS_REQUEST_ADVERTISER.TA_AD.Ad_Image;
                //    else
                //        imageFileName = CaptchaBLL.GetRandomImage(sliderWidth, sliderHeight, ref isDefaultResize);

                //    //switch (captchaType)
                //    //{
                //    //    case (int)CaptchaType.Slider:
                //    //    case (int)CaptchaType.SlideToFit:
                //    //        imageFileName = challenge.TCS_REQUEST_ADVERTISER.TA_AD.Ad_Image;
                //    //        break;
                //    //    case (int)CaptchaType.RandomImage:
                //    //    default:
                //    //        imageFileName = CaptchaBLL.GetRandomImage(sliderWidth, sliderHeight);
                //    //        break;
                //    //}

                //    imageURL = AD_URL + imageFileName;

                //    string contentType;
                //    ImageFormat imageFormat;

                //    WebResponse wr = WebRequest.Create(imageURL).GetResponse();
                //    contentType = wr.ContentType;

                //    switch (contentType.ToLower())
                //    {
                //        case "image/bmp":
                //            imageFormat = ImageFormat.Bmp;
                //            break;
                //        case "image/gif":
                //            imageFormat = ImageFormat.Gif;
                //            break;
                //        case "image/jpg":
                //        case "image/jpeg":
                //        default:
                //            imageFormat = ImageFormat.Jpeg;
                //            break;
                //    }

                //    // Clear and set response content type.
                //    this.Response.Clear();
                //    this.Response.ContentType = contentType;

                //    // Read and save bitmap.
                //    StreamReader reader = new StreamReader(wr.GetResponseStream());
                //    Bitmap bitmap = new Bitmap(reader.BaseStream);

                //    if (isDefaultResize)
                //    {
                //        bitmap = ProportionallyResizeBitmap(bitmap, sliderWidth, sliderHeight);
                //        int x = 0, y = 0; 
                //        if (bitmap.Height > sliderHeight)
                //        {
                //            y = (bitmap.Height - sliderHeight) / 2;
                //        }

                //        if (bitmap.Width > sliderWidth)
                //        {
                //            x = (bitmap.Width - sliderWidth) / 2;
                //        }

                //        bitmap = cropImage(bitmap, new Rectangle(new Point(x, y), new Size(sliderWidth, sliderHeight)));
                //    }
                //    // Get slide challange
                //    int slideChallange = Convert.ToInt16(challenge.TCS_REQUEST_CHALLENGE.Challenge_Code);

                //    switch (sliderType)
                //    {
                //        case 2:
                //            slide(bitmap, sliderY, sliderH, slideChallange).Save(this.Response.OutputStream, imageFormat);
                //            break;
                //        case 1:
                //        default:
                //            int radius = sliderW / 2;
                //            rotate(bitmap, radius, slideChallange).Save(this.Response.OutputStream, imageFormat);
                //            break;
                //    }
                    
                //    // Dispose bitmap.
                //    bitmap.Dispose();
                //}

                if (!String.IsNullOrEmpty(cid))
                {
                    using (DAL.AdsCaptcha_RequestsEntities dataContext = new DAL.AdsCaptcha_RequestsEntities())
                    {
                        //var challenge = dataContext.T_REQUESTS.Where(i => i.Request_Guid == cid).FirstOrDefault();
                        var challenge = CacheManager.RequestsCache.GetOrAddCachedItem(cid, () => new Tuple<T_REQUESTS, TimeSpan>(dataContext.T_REQUESTS.FirstOrDefault(i => i.Request_Guid == cid), CacheManager.DefaultRequestsCacheExpiration));

                        // Get slider parameters.
                        string[] args = challenge.ChallengeParams.Split(',');
                        int sliderType = Convert.ToInt16(args[0]);
                        int sliderWidth = Convert.ToInt16(args[1]);
                        int sliderHeight = Convert.ToInt16(args[2]);
                        int sliderX = Convert.ToInt16(args[3]);
                        int sliderY = Convert.ToInt16(args[4]);
                        int sliderW = Convert.ToInt16(args[5]);
                        int sliderH = Convert.ToInt16(args[6]);
                        int sliderMin = Convert.ToInt16(args[7]);
                        int sliderMax = Convert.ToInt16(args[8]);

                        string imageFileName;
                        string imageURL;

                        int captchaType = challenge.Type_Id;
                        bool isAdImages = (challenge.Ad_Id != null);

                        bool isDefaultResize = false;

                        if (isAdImages)
                            imageFileName = challenge.Ad_Image;
                        else
                            imageFileName = CaptchaBLL.GetRandomImage(sliderWidth, sliderHeight, ref isDefaultResize);

                        //switch (captchaType)
                        //{
                        //    case (int)CaptchaType.Slider:
                        //    case (int)CaptchaType.SlideToFit:
                        //        imageFileName = challenge.TCS_REQUEST_ADVERTISER.TA_AD.Ad_Image;
                        //        break;
                        //    case (int)CaptchaType.RandomImage:
                        //    default:
                        //        imageFileName = CaptchaBLL.GetRandomImage(sliderWidth, sliderHeight);
                        //        break;
                        //}

                        imageURL = AD_URL + imageFileName;

                        string contentType;
                        ImageFormat imageFormat;

                        WebResponse wr = WebRequest.Create(imageURL).GetResponse();
                        contentType = wr.ContentType;

                        switch (contentType.ToLower())
                        {
                            case "image/bmp":
                                imageFormat = ImageFormat.Bmp;
                                break;
                            case "image/gif":
                                imageFormat = ImageFormat.Gif;
                                break;
                            case "image/jpg":
                            case "image/jpeg":
                            default:
                                imageFormat = ImageFormat.Jpeg;
                                break;
                        }

                        // Clear and set response content type.
                        this.Response.Clear();
                        this.Response.ContentType = contentType;

                        // Read and save bitmap.
                        StreamReader reader = new StreamReader(wr.GetResponseStream());
                        Bitmap bitmap = new Bitmap(reader.BaseStream);

                        if (isDefaultResize)
                        {
                            bitmap = ProportionallyResizeBitmap(bitmap, sliderWidth, sliderHeight);
                            int x = 0, y = 0;
                            if (bitmap.Height > sliderHeight)
                            {
                                y = (bitmap.Height - sliderHeight) / 2;
                            }

                            if (bitmap.Width > sliderWidth)
                            {
                                x = (bitmap.Width - sliderWidth) / 2;
                            }

                            bitmap = cropImage(bitmap, new Rectangle(new Point(x, y), new Size(sliderWidth, sliderHeight)));
                        }
                        // Get slide challange
                        int slideChallange = Convert.ToInt16(challenge.Challenge);

                        switch (sliderType)
                        {
                            case 2:
                                slide(bitmap, sliderY, sliderH, slideChallange).Save(this.Response.OutputStream, imageFormat);
                                break;
                            case 1:
                            default:
                                int radius = sliderW / 2;
                                rotate(bitmap, radius, slideChallange).Save(this.Response.OutputStream, imageFormat);
                                break;
                        }

                        // Dispose bitmap.
                        bitmap.Dispose();
                    } 
                }
            }
            catch (Exception ex)
            {
                this.Response.Clear();
                this.Response.ContentType = "image/jpeg";
            }
        }

        private static Bitmap slide(Bitmap img, int y, int h, int slidePixels)
        {
            // Calculate left part attributes.
            int ax1 = 0;
            int ay1 = y;
            int ax2 = img.Width - slidePixels;
            int ay2 = y + h;
            int aw = ax2 - ax1;
            int ah = ay2 - ay1;

            // Calculate right part attributes.
            int bx1 = img.Width - slidePixels;
            int by1 = y;
            int bx2 = img.Width;
            int by2 = y + h;
            int bw = bx2 - bx1;
            int bh = by2 - by1;

            // Calculate "new part" coordinates.
            int cx = slidePixels;
            int cy = y;

            // Crop left part.
            Bitmap cropA = cropImage(img, new Rectangle(ax1, ay1, aw, ah));

            // Crop right part.
            Bitmap cropB = cropImage(img, new Rectangle(bx1, by1, bw, bh));

            using (Graphics g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                GraphicsPath gp = new GraphicsPath();
                //gp.AddRectangle(new Rectangle(bw, y, aw, ah));
                gp.AddRectangle(new Rectangle(bw, y, aw, ah));
                g.SetClip(gp);
                g.DrawImage(cropA, cx, cy);
            }

            using (Graphics g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                GraphicsPath gp = new GraphicsPath();
                gp.AddRectangle(new Rectangle(0, y, bw, bh));
                g.SetClip(gp);
                g.DrawImage(cropB, ax1, ay1);
            }

            // Dispose temporary croped bitmaps.
            cropA.Dispose();
            cropB.Dispose();

            return img;
        }

        private static Bitmap rotate(Bitmap img, int r, int rotateDegree)
        {
            int x = img.Width / 2 - r;
            int y = img.Height / 2 - r;
            int w = 2 * r;
            int h = 2 * r;

            // Rotate original image.
            Bitmap rotate = rotateCenter(img, rotateDegree);

            // Crop rotated image.
            rotate = cropImage(rotate, new Rectangle(rotate.Width / 2 - r, rotate.Height / 2 - r, 2 * r, 2 * r));

            // Extract rotated ellipse into original image.
            using (Graphics g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                GraphicsPath gp = new GraphicsPath();
                gp.AddEllipse(x, y, w, h);
                g.SetClip(gp);
                g.DrawImage(rotate, x, y);
            }

            // Dispose temporary rotated bitmaps.
            rotate.Dispose();

            return img;
        }

        private static Bitmap rotate(Bitmap img, int rotateDegree)
        {
            int width = img.Width;
            int height = img.Height;

            // Rotate original image.
            Bitmap rotate = rotateCenter(img, rotateDegree);

            // Crop rotated image.
            int x = rotate.Width / 2 - width / 2;
            int y = rotate.Height / 2 - height / 2;
            Rectangle rect = new Rectangle(x, y, width, height);
            rotate = cropImage(rotate, rect);

            // Extract rotated ellipse into original image.
            using (Graphics g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                GraphicsPath gp = new GraphicsPath();
                gp.AddEllipse(0, 0, width, height);
                g.SetClip(gp);
                g.DrawImage(rotate, 0, 0);
            }

            // Dispose temporary rotated bitmaps.
            rotate.Dispose();

            return img;
        }

        private static Bitmap cropImage(Bitmap img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return bmpCrop;
        }

        private static Bitmap rotateCenter(Bitmap bmpSrc, int theta)
        {
            Matrix mRotate = new Matrix();
            mRotate.Translate(bmpSrc.Width / -2, bmpSrc.Height / -2, MatrixOrder.Append);
            mRotate.RotateAt(theta, new Point(0, 0), MatrixOrder.Append);
            using (GraphicsPath gp = new GraphicsPath())
            {  // transform image points by rotation matrix
                gp.AddPolygon(new Point[] { new Point(0, 0), new Point(bmpSrc.Width, 0), new Point(0, bmpSrc.Height) });
                gp.Transform(mRotate);
                PointF[] pts = gp.PathPoints;

                // create destination bitmap sized to contain rotated source image
                Rectangle bbox = boundingBox(bmpSrc, mRotate);
                Bitmap bmpDest = new Bitmap(bbox.Width, bbox.Height);

                using (Graphics gDest = Graphics.FromImage(bmpDest))
                {  // draw source into dest
                    Matrix mDest = new Matrix();
                    mDest.Translate(bmpDest.Width / 2, bmpDest.Height / 2, MatrixOrder.Append);
                    gDest.Transform = mDest;
                    gDest.DrawImage(bmpSrc, pts);
                    gDest.DrawRectangle(Pens.Red, bbox);
                    return bmpDest;
                }
            }
        }

        private static Rectangle boundingBox(Bitmap img, Matrix matrix)
        {
            GraphicsUnit gu = new GraphicsUnit();
            Rectangle rImg = Rectangle.Round(img.GetBounds(ref gu));

            // Transform the four points of the image, to get the resized bounding box.
            Point topLeft = new Point(rImg.Left, rImg.Top);
            Point topRight = new Point(rImg.Right, rImg.Top);
            Point bottomRight = new Point(rImg.Right, rImg.Bottom);
            Point bottomLeft = new Point(rImg.Left, rImg.Bottom);
            Point[] points = new Point[] { topLeft, topRight, bottomRight, bottomLeft };
            GraphicsPath gp = new GraphicsPath(points, new byte[] { (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line });
            gp.Transform(matrix);
            return Rectangle.Round(gp.GetBounds());
        }

        public Bitmap ProportionallyResizeBitmap(Bitmap src, int maxWidth, int maxHeight)
        {

            // original dimensions
            int w = src.Width;
            int h = src.Height;

            // Longest and shortest dimension
            int longestDimension = (w > h) ? w : h;
            int shortestDimension = (w < h) ? w : h;

            // propotionality
            float factor = ((float)longestDimension) / shortestDimension;

            // default width is greater than height
            double newWidth = maxWidth;
            double newHeight = maxWidth / factor;

            // if height greater than width recalculate
            if (w < h)
            {
                newWidth = maxHeight / factor;
                newHeight = maxHeight;
            }

            // Create new Bitmap at new dimensions
            Bitmap result = new Bitmap((int)newWidth, (int)newHeight);
            using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
                g.DrawImage(src, 0, 0, (int)newWidth, (int)newHeight);

            return result;
        }

    }
}
