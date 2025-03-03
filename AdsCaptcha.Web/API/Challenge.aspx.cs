using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.DAL;

namespace Inqwise.AdsCaptcha.API
{
    [Obsolete("Not in use", true)]
    public partial class Challenge : System.Web.UI.Page
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {
            const int DFAULT_IMAGE_HEIGHT = 30;

            int width = 200;
            bool rtl = false;

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Get width.
            try
            {
                width = int.Parse(HttpUtility.HtmlEncode(Request.QueryString["w"]));
            } catch {}

            // Get challange guid.
            string cid = HttpUtility.HtmlEncode(Request.QueryString["cid"]);

            //CaptchaServerBLL.ChallengeData cd = CaptchaServerBLL.GetChallengeData(cid);

            ChallengeData cd = null;

            try
            {
                if (!String.IsNullOrEmpty(cid))
                {
                    using (DAL.AdsCaptcha_RequestsEntities dataContext = new DAL.AdsCaptcha_RequestsEntities())
                    {
                        //var request = dataContext.T_REQUESTS.Where(i => i.Request_Guid == cid).FirstOrDefault();
                        var request = CacheManager.RequestsCache.GetOrAddCachedItem(cid, () => new Tuple<T_REQUESTS, TimeSpan>(dataContext.T_REQUESTS.FirstOrDefault(i => i.Request_Guid == cid), CacheManager.DefaultRequestsCacheExpiration));

                        if (request != null)
                        {
                            cd = new ChallengeData();
                            cd.Challenge = request.Challenge;
                            cd.SecurityLevel = (int)AdsCaptcha.ApplicationConfiguration.SecurityLevel.Medium;

                            int themeId = (int)request.ThemeId;
                            T_THEME theme = DictionaryBLL.GetThemeById(themeId);
                            cd.BackColor = theme.Captcha_Background_Color;
                            cd.ForeColor = theme.Captcha_Text_Color;

                            cd.Direction = (request.Direction == 1) ? ApplicationConfiguration.Direction.RightToLeft : ApplicationConfiguration.Direction.LeftToRight;
                        }
                    } 
                }
            }
            catch(Exception ex)
            {
                Log.ErrorException("Challenge:AdsCaptcha_RequestsEntities: Unexpected error occured", ex);
                cd = null;
            }


            // If challange not exists or already archived, throw exception.
            if (null == cd)
            {
                Log.Warn("Challenge: Challange not exists or already archived. Requested cid: '{0}'", cid);
            }
            else
            {
                int[] severity = new int[] {};

                // warp - distort - ellipse - arc - beizer - curve
                switch (cd.SecurityLevel)
                {
                    case (int) AdsCaptcha.ApplicationConfiguration.SecurityLevel.VeryLow:
                        severity = new int[] {2, 3, 3, 1, 0, 0};
                        break;
                    case (int) AdsCaptcha.ApplicationConfiguration.SecurityLevel.Low:
                        severity = new int[] {2, 3, 3, 1, 1, 0};
                        break;
                    case (int) AdsCaptcha.ApplicationConfiguration.SecurityLevel.Medium:
                        severity = new int[] {2, 3, 3, 1, 1, 0};
                        break;
                    case (int) AdsCaptcha.ApplicationConfiguration.SecurityLevel.High:
                        severity = new int[] {2, 3, 3, 1, 1, 0};
                        break;
                    case (int) AdsCaptcha.ApplicationConfiguration.SecurityLevel.VeryHigh:
                        severity = new int[] {2, 3, 3, 1, 1, 0};
                        break;
                }

                if (cd.Direction == ApplicationConfiguration.Direction.RightToLeft)
                    rtl = true;

                ChallengeHandler challenge = new ChallengeHandler();

                // Write the image to the response stream.
                challenge.GenerateImage(cd.Challenge, width, DFAULT_IMAGE_HEIGHT, severity, cd.BackColor, cd.ForeColor,
                                        rtl).Save(this.Response.OutputStream, ImageFormat.Jpeg);
            }
        }
    }
}
