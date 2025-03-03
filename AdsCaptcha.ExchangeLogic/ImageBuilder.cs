using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inqwise.AdsCaptcha.ExchangeLogic.Components;
using System.Configuration;

namespace Inqwise.AdsCaptcha.ExchangeLogic
{
    public class ImageBuilder
    {
        public static void RunProcess()
        {
            try
            {
                using (AdsCaptcha_ImagesEntities ent = new AdsCaptcha_ImagesEntities())
                {
                    var excImages = ent.T_EXCHANGE_IMAGES.Where(e => e.ImageID == null).OrderByDescending(e => e.InsertDate).Take(10).ToList();

                    Console.WriteLine("RunProcess: received '{0}' image(s)", excImages.Count);
                    foreach (var img in excImages)
                    {
                        int imageID = CreateImage(ent, img);

                        if (imageID > 0)
                        {
                            img.ImageID = imageID;
                            img.ImageInsertDate = DateTime.Now;
                        }

                        T_EXCHANGE_IMAGES img180 = new T_EXCHANGE_IMAGES();
                        img180.AdID = img.AdID;
                        img180.ExchangeID = img.ExchangeID;
                        img180.ClickUrl = img.ClickUrl;
                        img180.SrcImageUrl = img.SrcImageUrl;
                        img180.AdID = img.AdID;
                        img180.Width = 180;
                        img180.Height = 150;
                        img180.InsertDate = DateTime.Now;

                        ent.AddToT_EXCHANGE_IMAGES(img180);
                        ent.SaveChanges();

                        imageID = CreateImage(ent, img180);

                        if (imageID > 0)
                        {
                            img180.ImageID = imageID;
                            img180.ImageInsertDate = DateTime.Now;
                        }

                    }

                    if (excImages.Count > 0)
                    {
                        Console.WriteLine("ImageBuilder: SaveChanges");
                        ent.SaveChanges();
                        Console.WriteLine("ImageBuilder: SaveChanges complete");
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private static int CreateImage(AdsCaptcha_ImagesEntities ent, T_EXCHANGE_IMAGES img)
        {

            ImageWorker worker = new ImageWorker();
            int advertiserID = Convert.ToInt32(ConfigurationManager.AppSettings["YBrantAdvertiser"].Split(';')[0]);
            int campaignID = Convert.ToInt32(ConfigurationManager.AppSettings["YBrantAdvertiser"].Split(';')[0]);
            int adID = Convert.ToInt32(ConfigurationManager.AppSettings["YBrantAdvertiser"].Split(';')[0]);
            int imageID = worker.LoadImage(ent, img.SrcImageUrl, advertiserID, campaignID, adID, img.Width, img.Height);

            return imageID;

        }


    }
}
