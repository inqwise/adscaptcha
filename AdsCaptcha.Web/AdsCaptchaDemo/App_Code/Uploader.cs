/*
using System;
using System.Linq;
using System.Web.Services;
using Inqwise.AdsCaptcha_DemoModel;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for UploadImage
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Uploader : System.Web.Services.WebService {

    private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

    public Uploader()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public bool UploadImage(int advertiserId, int campaignId, int adId, int width, int height, byte[] stream) {



            try
            {
                ImageUtilities utilities = new ImageUtilities();

                using (AdsCaptcha_ImagesEntities ent = new AdsCaptcha_ImagesEntities())
                {
                    MemoryStream mstream = new MemoryStream(stream);
                    System.Drawing.Image coreimage = Bitmap.FromStream(mstream);


                    var resizedCoreBmp = utilities.ResizeImage(coreimage, width, height);

                    Log.Debug("UploadImage: advertiserId: '{0}', campaignId: '{1}', adId: '{2}'", advertiserId, campaignId, adId);
                    var mainImage = ent.P_Images_Insert(2, utilities.SaveJpegToStream(resizedCoreBmp, 90).ToArray(),
                                                        "jpg", width, height, advertiserId, campaignId, adId).FirstOrDefault();


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

                    mstream.Close();
                    ent.SaveChanges();

                    CacheImagesManager.RemoveFromCache(mainImage.ImageID);
                    CacheImagesManager.AddToCache(mainImage.ImageID);
                    


                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }


       
    }

    [WebMethod]
    public bool UpdateAd(int advertiserId, int campaignId, int adId, int previousAdId)
    {
        try
        {
            using (AdsCaptcha_ImagesEntities ent = new AdsCaptcha_ImagesEntities())
            {
                var img = ent.T_IMAGES.Where(m => m.Ad_Id == previousAdId).FirstOrDefault();
                if (img != null)
                {
                    img.Ad_Id = adId;
                    img.Advertiser_Id = advertiserId;
                    img.Campaign_Id = campaignId;

                    ent.SaveChanges();
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
    
}
*/