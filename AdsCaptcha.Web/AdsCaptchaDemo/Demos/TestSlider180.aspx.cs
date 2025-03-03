using System;
using System.Drawing;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Managers;

public partial class TestSlider180 : System.Web.UI.Page
{
    private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {

    }


	/*
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblResult.Visible = true;
        try
        {
            int sliderRes = Convert.ToInt32(Request["adscaptcha_response_field"]);
            string cid = Request["adscaptcha_challenge_field"];
            // Get request
            var actualRequestResult = RequestsManager.Get(cid, isDemo: true);
            if (actualRequestResult.HasError)
            {
                // Cache timeout or not found
                lblResult.Text = "Wrong. Please try again...    <br />";
                lblResult.ForeColor = Color.Red;
            }
            else
            {
                IRequest captchaRequest = actualRequestResult.Value;
                if (sliderRes == captchaRequest.CorrectIndex)
                {
                    lblResult.Text = "Correct     <br />";
                    lblResult.ForeColor = Color.Green;
                }
                else
                {
                    lblResult.Text = "Wrong. Please try again...    <br />";
                    lblResult.ForeColor = Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            Log.ErrorException("btnSubmit_Click: Unexpected error occured", ex);
            lblResult.Text = "Error    <br />";
        }
    }
    */

    private void SaveToFileSystem()
    {
        /*
        if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
        {
            string fn = Guid.NewGuid().ToString() + ".jpg";// System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            string SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder180"] + "\\" + fn;
            try
            {
                FileUpload1.PostedFile.SaveAs(SaveLocation);

                string imageDirName = Guid.NewGuid().ToString();

                string imagesDir = ConfigurationManager.AppSettings["RandomImagesFolder180"] + imageDirName + "\\";
                Directory.CreateDirectory(imagesDir);

                System.Drawing.Image coreimage = Bitmap.FromFile(SaveLocation);

                ImageUtilities utilities = new ImageUtilities();

                var resizedCoreBmp = utilities.ResizeImage(coreimage, 180, 150);
                utilities.SaveJpeg(imagesDir + "30.jpg", resizedCoreBmp, 90);


                double xStep = 0.02;

                Random rand = new Random(999);
                double yStep = (double)(rand.Next(999) * DateTime.Now.Millisecond % 13) / (double)20 / (double)30;

                double amountStep = 1.1;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 25)) / (double)30 ;
                //double sizeStep = 0.035;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 5) / (double)10) / (double)30;
                TwistEffect effect = new TwistEffect();

                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    double offFirst = xStep * (double)(i + 1);
                    double offSecond = yStep * (double)(i + 1);
                    double amount = amountStep * (double)(i + 1) + 5.0;
                    double size = 0.50;// -sizeStep * (double)(i + 1);

                    var destBMP = effect.ConvertImage(tempBmp, amount, size, 2, offFirst, offSecond);
                    //var destBMP2 = effect.ConvertImage(tempBmp, amount, size, 2, (-1.0) * offFirst, (-1.0) * offSecond);

                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    //jpgQuality = 90 - Convert.ToInt32((double)i * 0.4);
                    utilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, jpgQuality);
                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, 90 - Convert.ToInt32((double)i * 1.1));

                    tempBmp = null;
                    destBMP = null;
                    //destBMP2 = null;
                }

                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    double offFirst = xStep * (double)(i + 1);
                    double offSecond = yStep * (double)(i + 1);
                    double amount = amountStep * (double)(i + 1) + 5.0;
                    double size = 0.50;// 3.0 - sizeStep * (double)(i + 1);

                    //var destBMP = effect.ConvertImage(tempBmp, amount, size, 2, offFirst, offSecond);
                    var destBMP2 = effect.ConvertImage(tempBmp, (-1.0) * amount, size, 2, (-1.0) * offFirst, (-1.0) * offSecond);

                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, 90 - Convert.ToInt32((double)i * 1.1));
                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    utilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, jpgQuality);

                    tempBmp = null;
                    //destBMP = null;
                    destBMP2 = null;
                }

                //string imagesUrl = ConfigurationManager.AppSettings["Images180Url"] + imageDirName + "/";
                //int startimageIndex = 2 + Math.Abs(((rand.Next(999, 9999) * DateTime.Now.Millisecond) % 26));

                //string jsArray = "var AdscaptchaBgImages = [";
                //for (int i = 0; i < 30; i++)
                //{
                //    jsArray += "{ src: '" + imagesUrl + Convert.ToString(startimageIndex + i) + ".jpg' },";
                //    if ((startimageIndex + i) == 30)
                //        Session["Result"] = i;
                //}
                //jsArray = jsArray.Substring(0, jsArray.Length - 1);
                //jsArray += " ];";

                //AdscaptchaBgImages = jsArray;

                Response.Redirect("~/Demos/TestSlider180.aspx");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        else
        {

        }
         */
    }

    private void SaveToDB()
    {
        /*
        if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
        {
            string fn = Guid.NewGuid().ToString() + ".jpg";// System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            string SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder180"] + "\\" + fn;
            try
            {
                ImageUtilities utilities = new ImageUtilities();

                FileUpload1.PostedFile.SaveAs(SaveLocation);

                string imageDirName = Guid.NewGuid().ToString();

                //string imagesDir = ConfigurationManager.AppSettings["RandomImagesFolder300"] + imageDirName + "\\";
                //Directory.CreateDirectory(imagesDir);

                using (AdsCaptcha_ImagesEntities ent = new AdsCaptcha_ImagesEntities())
                {
                    System.Drawing.Image coreimage = Bitmap.FromFile(SaveLocation);


                    var resizedCoreBmp = utilities.ResizeImage(coreimage, 180, 150);
                    FileInfo fi = new FileInfo(SaveLocation);

                    var mainImage = ent.P_Images_Insert(1, utilities.SaveJpegToStream(resizedCoreBmp, 90).ToArray(),
                                                        fi.Extension.Replace(".", ""), 180, 150, null, null, null).FirstOrDefault();


                    AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS defMain = new T_IMAGES_DEFORMATIONS();
                    defMain.DeformationID = 30;
                    //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                    //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                    defMain.ImageID = mainImage.ImageID;
                    defMain.DeformationType = 1;
                    defMain.ImageStream = mainImage.ImageStream;
                    ent.AddToT_IMAGES_DEFORMATIONS(defMain);

                    //ent.P_Images_Deformations_Insert(30, 1, mainImage.ImageID, mainImage.ImageStream);


                    double xStep = 0.02;

                    Random rand = new Random(999);
                    double yStep = (double)(rand.Next(999) * DateTime.Now.Millisecond % 13) / (double)20 / (double)30;

                    double amountStep = 1.1;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 25)) / (double)30 ;
                    //double sizeStep = 0.035;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 5) / (double)10) / (double)30;
                    TwistEffect effect = new TwistEffect();

                    for (int i = 0; i < 30; i++)
                    {

                        var tempBmp = new Bitmap(resizedCoreBmp);

                        double offFirst = xStep * (double)(i + 1);
                        double offSecond = yStep * (double)(i + 1);
                        double amount = amountStep * (double)(i + 1) + 5.0;
                        double size = 0.50;// -sizeStep * (double)(i + 1);

                        var destBMP = effect.ConvertImage(tempBmp, amount, size, 2, offFirst, offSecond);
                        //var destBMP2 = effect.ConvertImage(tempBmp, amount, size, 2, (-1.0) * offFirst, (-1.0) * offSecond);

                        int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                        jpgQuality = 90;
                        //jpgQuality = 90 - Convert.ToInt32((double)i * 0.4);
                        //utilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, jpgQuality);
                        //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, 90 - Convert.ToInt32((double)i * 1.1));

                        AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                        def.DeformationID = 30 + i + 1;
                        //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                        //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                        def.ImageID = mainImage.ImageID;
                        def.DeformationType = 1;
                        def.ImageStream = utilities.SaveJpegToStream(destBMP, 90).ToArray();
                        ent.AddToT_IMAGES_DEFORMATIONS(def);

                        tempBmp = null;
                        destBMP = null;
                        //destBMP2 = null;
                    }

                    for (int i = 0; i < 30; i++)
                    {

                        var tempBmp = new Bitmap(resizedCoreBmp);

                        double offFirst = xStep * (double)(i + 1);
                        double offSecond = yStep * (double)(i + 1);
                        double amount = amountStep * (double)(i + 1) + 5.0;
                        double size = 0.50;// 3.0 - sizeStep * (double)(i + 1);

                        //var destBMP = effect.ConvertImage(tempBmp, amount, size, 2, offFirst, offSecond);
                        var destBMP2 = effect.ConvertImage(tempBmp, (-1.0) * amount, size, 2, (-1.0) * offFirst, (-1.0) * offSecond);

                        //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, 90 - Convert.ToInt32((double)i * 1.1));
                        int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                        jpgQuality = 90;
                        // utilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, jpgQuality);

                        AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                        def.DeformationID = 30 - i - 1;
                        //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                        //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                        def.ImageID = mainImage.ImageID;
                        def.DeformationType = 1;
                        def.ImageStream = utilities.SaveJpegToStream(destBMP2, 90).ToArray();
                        ent.AddToT_IMAGES_DEFORMATIONS(def);

                        tempBmp = null;
                        //destBMP = null;
                        destBMP2 = null;
                    }

                    //string imagesUrl = ConfigurationManager.AppSettings["Images300Url"] + imageDirName + "/";
                    //int startimageIndex = 2 + rand.Next(999) * DateTime.Now.Millisecond % 26;

                    //string jsArray = "var AdscaptchaBgImages = [";
                    //for (int i = 0; i < 30; i++)
                    //{
                    //    jsArray += "{ src: '" + imagesUrl + Convert.ToString(startimageIndex + i) + ".jpg' },";
                    //}
                    //jsArray = jsArray.Substring(0, jsArray.Length - 1);
                    //jsArray += " ];";

                    //AdscaptchaBgImages = jsArray;

                    ent.SaveChanges();

                    CacheImagesManager.AddToCache(mainImage.ImageID);

                    Response.Redirect("~/Demos/TestSlider180.aspx");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        else
        {

        }
         */
    }
}