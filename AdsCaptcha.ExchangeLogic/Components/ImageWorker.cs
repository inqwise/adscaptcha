using System;
using System.Linq;
using AdsCaptchaEffects;
using System.Drawing;
using System.Net;
using System.IO;

namespace Inqwise.AdsCaptcha.ExchangeLogic.Components
{
    public class ImageWorker
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private const string JPG_CONTENT_TYPE = "image/jpg";
        private const string IMAGE_PATH_FORMAT = "images/{0}/{1}_{2}/{3}.jpg";

        public int LoadImage(AdsCaptcha_ImagesEntities ent, string imagePath, int AdvertiserID, int CampaignID, int AdID, int width, int height)
        {
            Console.WriteLine("LoadImage: '{0}'", imagePath);
            MemoryStream ms = null;
            BinaryWriter bw = null;

            try
            {
                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(imagePath);
                Console.WriteLine("LoadImage: download complete '{0}' bytes", imageBytes.Length);
                ms = new MemoryStream();
                bw = new BinaryWriter(ms);
                bw.Write(imageBytes);
                Image img = Image.FromStream(ms);

                int id;
                if (width == 180)
                    id = SaveImage(ent, img, AdvertiserID, CampaignID, AdID, 180, 150);
                else
                    id = SaveImage(ent, img, AdvertiserID, CampaignID, AdID, 300, 250);

                Console.WriteLine("LoadImage: save complete. Id: '{0}'", id);

                return id;

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                throw exc;

            }
            finally
            {
                if (bw != null) bw.Close();
                if (ms != null) ms.Close();
            }
          
        }
        private int SaveImage(AdsCaptcha_ImagesEntities ent, System.Drawing.Image coreimage, int AdvertiserID, int CampaignID, int AdID, int width, int height)
        {
            ImageUtilities utilities = new ImageUtilities();



            var resizedCoreBmp = utilities.ResizeImage(coreimage, width, height);

            var mainImage = ent.P_Images_Insert(101, utilities.SaveJpegToStream(resizedCoreBmp, 90).ToArray(),
                                                ".jpg", width, height, AdvertiserID, CampaignID, AdID).FirstOrDefault();

            Random rand = new Random(999);
            TwistEffect effect = new TwistEffect();

            T_IMAGES_DEFORMATIONS defMain = new T_IMAGES_DEFORMATIONS();
            defMain.DeformationID = 30;
            //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
            //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
            defMain.ImageID = mainImage.ImageID;
            defMain.DeformationType = 1;
            //defMain.ImageStream = mainImage.ImageStream;

            int next = rand.Next(999) * DateTime.Now.Millisecond % 2;
            double offFirst1 = 0.01 * ((next == 0) ? 1 : -1);

            next = rand.Next(999) * DateTime.Now.Millisecond % 2;
            double offSecond1 = 0.01 * ((next == 0) ? 1 : -1);

            next = rand.Next(999) * DateTime.Now.Millisecond % 2;
            double amount1 = 1.0 * ((next == 0) ? 1 : -1);
            double size1 = 0.01;// -sizeStep * (double)(i + 1);

            var tempBmp1 = new Bitmap(resizedCoreBmp);
            var destBMP1 = effect.ConvertImage(tempBmp1, amount1, size1, 2, offFirst1, offSecond1);

            defMain.ImageStream = utilities.SaveJpegToStream(destBMP1, 90).ToArray();

            ent.AddToT_IMAGES_DEFORMATIONS(defMain);

            try
            {
                // Upload image to S3 main deformation
                AmazonAWS.Upload(new MemoryStream(defMain.ImageStream),
                                 string.Format(IMAGE_PATH_FORMAT, defMain.ImageID, width, height, defMain.DeformationID), width,
                                 height, JPG_CONTENT_TYPE);
            }
            catch (Exception ex)
            {
                Log.ErrorException("SaveImage: Failed to save image deformation", ex);
            }

            //ent.P_Images_Deformations_Insert(30, 1, mainImage.ImageID, mainImage.ImageStream);


            double xStep = 0.02;


            double yStep = (double)(rand.Next(999) * DateTime.Now.Millisecond % 13) / (double)20 / (double)30;

            double amountStep = 1.1;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 25)) / (double)30 ;
            double sizeStep = 0.035;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 5) / (double)10) / (double)30;
            

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

                T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                def.DeformationID = 30 + i + 1;
                //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                def.ImageID = mainImage.ImageID;
                def.DeformationType = 1;
                def.ImageStream = utilities.SaveJpegToStream(destBMP, 90).ToArray();

                try
                {
                    // Upload image to S3 deformation
                    AmazonAWS.Upload(new MemoryStream(def.ImageStream),
                                     string.Format(IMAGE_PATH_FORMAT, defMain.ImageID, width, height, def.DeformationID), width,
                                     height, JPG_CONTENT_TYPE);
                }
                catch (Exception ex)
                {
                    Log.ErrorException("SaveImage: Failed to save image deformation", ex);
                }

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

                T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                def.DeformationID = 30 - i - 1;
                //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                def.ImageID = mainImage.ImageID;
                def.DeformationType = 1;
                def.ImageStream = utilities.SaveJpegToStream(destBMP2, 90).ToArray();
                try
                {
                    // Upload image to S3 deformation
                    AmazonAWS.Upload(new MemoryStream(def.ImageStream),
                                     string.Format(IMAGE_PATH_FORMAT, defMain.ImageID, width, height, def.DeformationID), width,
                                     height, JPG_CONTENT_TYPE);
                }
                catch (Exception ex)
                {
                    Log.ErrorException("SaveImage: Failed to save image deformation", ex);
                }
                ent.AddToT_IMAGES_DEFORMATIONS(def);

                tempBmp = null;
                //destBMP = null;
                destBMP2 = null;
            }



            ent.SaveChanges();

            return mainImage.ImageID;

        }
    }
}
