using System;
using System.Configuration;
using System.Web;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.API
{
    public partial class Viewed : System.Web.UI.Page
    {
        private string challengeCode;
        private string API_URL;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Get ChallengeCode.
                challengeCode = HttpUtility.HtmlEncode(Request.QueryString["ChallengeCode"]);

                // Raise viewed flag.
                CaptchaServerBLL.Viewed(challengeCode);
            }
            catch (Exception ex)
            {
            }
        }
    }
}