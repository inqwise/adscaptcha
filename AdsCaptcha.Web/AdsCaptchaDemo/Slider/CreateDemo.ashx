<%@ WebHandler Language="C#" Class="CreateDemo" %>

using System;
using System.Web;

public class CreateDemo : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string demo = string.Empty;

        string width = context.Request.QueryString["w"];
        string height = context.Request.QueryString["h"];
        
        


        context.Response.Write(demo);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    /*
    private void SaveToDB(AdsCaptcha_DemoModel.AdsCaptcha_ImagesEntities ent, ref int imageID)
    {
        imageID= -1;
        //if ((uploadImage.PostedFile != null) && (uploadImage.PostedFile.ContentLength > 0))
        //{
        //    string fn = Guid.NewGuid().ToString() + ".jpg";// System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
        //    string SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder300"] + "\\" + fn;
        //    try
        //    {
        //        ImageUtilities utilities = new ImageUtilities();

        //        uploadImage.PostedFile.SaveAs(SaveLocation);

        //        string imageDirName = Guid.NewGuid().ToString();

        //        //string imagesDir = ConfigurationManager.AppSettings["RandomImagesFolder300"] + imageDirName + "\\";
        //        //Directory.CreateDirectory(imagesDir);


        //        System.Drawing.Image coreimage = Bitmap.FromFile(SaveLocation);


        //        var resizedCoreBmp = utilities.ResizeImage(coreimage, 300, 250);
        //        FileInfo fi = new FileInfo(SaveLocation);

        //        var mainImage = ent.P_Images_Insert(2, utilities.SaveJpegToStream(resizedCoreBmp, 90).ToArray(),
        //                                            fi.Extension.Replace(".", ""), 300, 250, null, null, null).FirstOrDefault();


        //        imageID = mainImage.ImageID;

        //        AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS defMain = new T_IMAGES_DEFORMATIONS();
        //        defMain.DeformationID = 30;
        //        //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
        //        //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
        //        defMain.ImageID = mainImage.ImageID;
        //        defMain.DeformationType = 1;
        //        defMain.ImageStream = mainImage.ImageStream;
        //        ent.AddToT_IMAGES_DEFORMATIONS(defMain);

        //        //ent.P_Images_Deformations_Insert(30, 1, mainImage.ImageID, mainImage.ImageStream);


        //        double xStep = 0.02;

        //        Random rand = new Random(999);
        //        double yStep = (double)(rand.Next(999) * DateTime.Now.Millisecond % 13) / (double)20 / (double)30;

        //        double amountStep = 1.1;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 25)) / (double)30 ;
        //        double sizeStep = 0.035;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 5) / (double)10) / (double)30;
        //        TwistEffect effect = new TwistEffect();

        //        for (int i = 0; i < 30; i++)
        //        {

        //            var tempBmp = new Bitmap(resizedCoreBmp);

        //            double offFirst = xStep * (double)(i + 1);
        //            double offSecond = yStep * (double)(i + 1);
        //            double amount = amountStep * (double)(i + 1) + 5.0;
        //            double size = 0.50;// -sizeStep * (double)(i + 1);

        //            var destBMP = effect.ConvertImage(tempBmp, amount, size, 2, offFirst, offSecond);
        //            //var destBMP2 = effect.ConvertImage(tempBmp, amount, size, 2, (-1.0) * offFirst, (-1.0) * offSecond);

        //            int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
        //            jpgQuality = 90;
        //            //jpgQuality = 90 - Convert.ToInt32((double)i * 0.4);
        //            //utilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, jpgQuality);
        //            //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, 90 - Convert.ToInt32((double)i * 1.1));

        //            AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
        //            def.DeformationID = 30 + i + 1;
        //            //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
        //            //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
        //            def.ImageID = mainImage.ImageID;
        //            def.DeformationType = 1;
        //            def.ImageStream = utilities.SaveJpegToStream(destBMP, 90).ToArray();
        //            ent.AddToT_IMAGES_DEFORMATIONS(def);

        //            tempBmp = null;
        //            destBMP = null;
        //            //destBMP2 = null;
        //        }

        //        for (int i = 0; i < 30; i++)
        //        {

        //            var tempBmp = new Bitmap(resizedCoreBmp);

        //            double offFirst = xStep * (double)(i + 1);
        //            double offSecond = yStep * (double)(i + 1);
        //            double amount = amountStep * (double)(i + 1) + 5.0;
        //            double size = 0.50;// 3.0 - sizeStep * (double)(i + 1);

        //            //var destBMP = effect.ConvertImage(tempBmp, amount, size, 2, offFirst, offSecond);
        //            var destBMP2 = effect.ConvertImage(tempBmp, (-1.0) * amount, size, 2, (-1.0) * offFirst, (-1.0) * offSecond);

        //            //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, 90 - Convert.ToInt32((double)i * 1.1));
        //            int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
        //            jpgQuality = 90;
        //            // utilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, jpgQuality);

        //            AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
        //            def.DeformationID = 30 - i - 1;
        //            //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
        //            //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
        //            def.ImageID = mainImage.ImageID;
        //            def.DeformationType = 1;
        //            def.ImageStream = utilities.SaveJpegToStream(destBMP2, 90).ToArray();
        //            ent.AddToT_IMAGES_DEFORMATIONS(def);

        //            tempBmp = null;
        //            //destBMP = null;
        //            destBMP2 = null;
        //        }

        //        ent.SaveChanges();

        //        CacheImagesManager.AddToCache(mainImage.ImageID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //        imageID = -1;
        //    }
        //}
        //else
        //{
        //    imageID = -1;
        //}
    }
     */

}