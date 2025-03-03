/*
using System;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace Slider
{
    public class Image : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = " image/jpeg";

            //int challenge = Convert.ToInt32(HttpContext.Current.Session["Correct"]);
            string imagePath = string.Empty;

            string width = context.Request.QueryString["w"];
            string height = context.Request.QueryString["h"];

            string reqid = context.Request.QueryString["reqid"];
            int imageNo = Convert.ToInt32(context.Request.QueryString["img"]);


            //imagePath = ConfigurationManager.AppSettings["RandomImagesFolder300"] + imgdir + @"\";
            //if (width == "180")
            //    imagePath = ConfigurationManager.AppSettings["RandomImagesFolder180"] + imgdir + @"\";

            //imagePath += Convert.ToString(imageNo + (30 - challenge) - 1).ToString() + ".jpg";
            //System.Drawing.Image imgTemp = System.Drawing.Image.FromFile(imagePath);

            //var image = (from d in ent.T_IMAGES_DEFORMATIONS
            //             where d.ImageID == imgid && d.DeformationID == (imageNo + (30 - challenge) - 1)
            //             select d).SingleOrDefault();
            //try
            {

                Request cacherequest = (Request)CacheManager.Instance.GetCachedItem(reqid);
                if (Log.IsDebugEnabled)
                {
                    Log.Debug("Image: received request #{0} from cache, type: '{1}'", reqid, cacherequest);
                }
                int imageid = cacherequest.ImageID; //ImagesDAL.GetInstance().GetImageIDByRequest(reqid, ref challenge);
                int challenge = cacherequest.Challenge;
                imageNo = Math.Abs(imageNo) % 31;

                var def = CacheImagesManager.GetDeformations(imageid).Where(d => d.DeformationID == imageNo + 30 - challenge - 1).FirstOrDefault();
                int maxLength = CacheImagesManager.GetMaxDefLength(imageid);
                //if ((image == null) || (image.ImageID <= 0)) image = ImagesDAL.GetInstance().GetImageDeformation(reqid, imageNo);
                //if ((image == null) || (image.Length <= 0)) image = ImagesDAL.GetInstance().GetImageDeformationStreamEnt(reqid, imageNo);

                if ((def != null) && (def.ImageStream.Length > 0))
                {
                    ImageCodecInfo codec = GetEncoderInfo("image/jpeg");

                    // Configure to encode at high quality
                    using (EncoderParameters ep = new EncoderParameters())
                    {
                        ep.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

                        // Encode the image
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        {
                            if (maxLength == 0) maxLength = 1024 * 30;

                            byte[] tempbytes = new byte[Math.Max(maxLength, def.ImageStream.Length)];
                            tempbytes.Initialize();
                            for (int i = 0; i < def.ImageStream.Length - 1; i++)
                                tempbytes[i] = def.ImageStream[i];
                            ms.Write(tempbytes, 0, tempbytes.Length);
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
            //catch (Exception exc)
            //{
            //    throw exc;
            //}


            if (imagePath != string.Empty)
            {

            }

            context.Response.End();
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
*/