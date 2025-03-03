using System;
using Inqwise.AdsCaptcha.ImageProcessWCF.Utils;
using Inqwise.AdsCaptcha_ImagesModel;

namespace Inqwise.AdsCaptcha.ImageProcessWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class ImageProcessing : IImageProcessing
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        public int CreateImageDeformations(string imgUrl, int imageType, int width, int height)
        {
            DeformedImage img = new DeformedImage();
            try
            {
                ImageWorker w = new ImageWorker();

                using (AdsCaptcha_ImagesEntities ent = new AdsCaptcha_ImagesEntities())
                {
                    int imgID = -1;
                    byte[] imgStream = null;
                    w.LoadImage(ent, imgUrl, imageType, width, height, ref imgID, imgStream);
                    img.ImgID = imgID; 
                    img.ImageStream = imgStream;
                }

            }
            catch(Exception ex)
            {
                Log.ErrorException("CreateImageDeformations : Unexpected error occured", ex);
                img = new DeformedImage();
            }
            return img.ImgID;
        }

        public int CreateImageDeformationsFromStream(byte[] imageStream, int imageType, int width, int height)
        {
            DeformedImage img = new DeformedImage();
            try
            {
                ImageWorker w = new ImageWorker();

                using (AdsCaptcha_ImagesEntities ent = new AdsCaptcha_ImagesEntities())
                {
                    int imgID = -1;
                    //byte[] imgStream = null;
                    w.LoadImageFromStream(ent, imageStream, imageType, width, height, ref imgID);
                    img.ImgID = imgID;
                    img.ImageStream = imageStream;
                }

            }
            catch(Exception ex)
            {
                Log.ErrorException("CreateImageDeformationsFromStream : Unexpected error occured", ex);
                img = new DeformedImage();
            }
            return img.ImgID;
        }
    }
}
