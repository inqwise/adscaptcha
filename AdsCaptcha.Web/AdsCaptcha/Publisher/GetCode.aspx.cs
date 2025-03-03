using System;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Managers;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class GetCode : System.Web.UI.Page
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        private ICaptcha _captcha;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["PublisherLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["PublisherId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                int publisherId = Convert.ToInt16(Page.Session["PublisherId"]);
                int websiteId = Convert.ToInt16(Page.Request.QueryString["WebsiteId"]);
                int captchaId = Convert.ToInt16(Page.Request.QueryString["CaptchaId"]);

                var captchaResult = CaptchasManager.Get(captchaId, publisherId);
                if (captchaResult.HasError)
                {
                    throw new Exception("Captcha not exists");
                }
                else
                {
                    _captcha = captchaResult.Value;
                }

            }
            catch(Exception ex)
            {
                Log.ErrorException("Page_Load: Unexpected error occured", ex);
                Response.Redirect("ManageWebsites.aspx");
            }

            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Publisher, Master.Page.Header);

                InitControls();
            }
        }

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            IWebsite website = WebsitesManager.Get(_captcha.WebsiteId, _captcha.PublisherId).Value;

            // Set navigation path.
            labelNavigationPath.Text = "<a href=\"ManageWebsites.aspx\">Websites</a>" + " &gt; " +
                                       "Website: " + "<a href=\"ManageCaptchas.aspx?WebsiteId=" + _captcha.WebsiteId.ToString() + "\">" + website.Url + "</a>" + " &gt; " +
                                       "Captcha: " + _captcha.Name + " &gt; " +
                                       "Get Code";

            // Display code.
            labelCaptchaID.Text = _captcha.Id.ToString();
            labelPublicKey.Text = website.PublicKey;
            labelPrivateKey.Text = website.PrivateKey;
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // Redirect to manage websites page.
            Response.Redirect("ManageCaptchas.aspx?WebsiteId=" + _captcha.WebsiteId.ToString());
        }
    }
}
