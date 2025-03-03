using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections.Generic;
using System.Drawing.Imaging;
using Inqwise.AdsCaptcha.DAL;

namespace Inqwise.AdsCaptcha.HouseAdsSlider
{
    /// <summary>
    /// Summary description for $CodeFileclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = " image/jpeg";

            int houseId = Convert.ToInt32(context.Request.QueryString["img"]);

            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                int imageId = dataContext.TP_CAPTCHA_HOUSEADs.Where(h => h.HouseAd_Id == houseId).FirstOrDefault().DeformedImageID;
                using (AdsCaptcha_ImagesEntities ent = new AdsCaptcha_ImagesEntities())
                {
                    var image = ent.T_IMAGES.Where(i => i.ImageID == imageId).FirstOrDefault();
                    if (image != null)
                    {
                        ImageCodecInfo codec = GetEncoderInfo("image/jpeg");

                        // Configure to encode at high quality
                        using (EncoderParameters ep = new EncoderParameters())
                        {
                            ep.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

                            // Encode the image
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                            {
                                ms.Write(image.ImageStream, 0, image.ImageStream.Length);
                                //imgTemp.Save(ms, codec, ep);

                                // Send the encoded image to the browser
                                HttpContext.Current.Response.Clear();
                                HttpContext.Current.Response.ContentType = "image/jpeg";
                                ms.WriteTo(HttpContext.Current.Response.OutputStream);
                                //ms.Close();
                            }
                        }
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// A quick lookup for getting image encoders
        /// </summary>
        private Dictionary<string, ImageCodecInfo> encoders = null;

        /// <summary>
        /// A quick lookup for getting image encoders
        /// </summary>
        public Dictionary<string, ImageCodecInfo> Encoders
        {
            //get accessor that creates the dictionary on demand
            get
            {
                //if the quick lookup isn't initialised, initialise it
                if (encoders == null)
                {
                    encoders = new Dictionary<string, ImageCodecInfo>();
                }

                //if there are no codecs, try loading them
                if (encoders.Count == 0)
                {
                    //get all the codecs
                    foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
                    {
                        //add each codec to the quick lookup
                        encoders.Add(codec.MimeType.ToLower(), codec);
                    }
                }

                //return the lookup
                return encoders;
            }
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        public ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            //do a case insensitive search for the mime type
            string lookupKey = mimeType.ToLower();

            //the codec to return, default to null
            ImageCodecInfo foundCodec = null;

            //if we have the encoder, get it to return
            if (Encoders.ContainsKey(lookupKey))
            {
                //pull the codec from the lookup
                foundCodec = Encoders[lookupKey];
            }

            return foundCodec;
        }
    }
}
