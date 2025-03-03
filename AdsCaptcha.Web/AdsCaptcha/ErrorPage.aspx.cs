using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Inqwise.AdsCaptcha
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set metadata (title, keywords and description).
            Metadata.SetMetadata(Metadata.Pages.General, Master.Page.Header);

            int errorCode = 0;
            string errorTitle = "";
            string errorMessage = "";

            // Get custom error code.
            try
            {
                errorCode = Convert.ToInt16(Request.QueryString["code"].ToString());
            }
            catch {}

            // Check error code and set suitable error message.
            switch (errorCode)
            {
                case 404:
                    errorTitle = "Page not found";
                    errorMessage = "The page you've been reached is unavailable.";
                    AutoRedirect();
                    break;
                default:
                    errorTitle = "Sorry, an error occured";
                    //errorMessage = "Sorry, an error occured.";
                    break;
            }

            if (errorMessage.Length > 0)
                errorMessage = errorMessage + "<br/><br/>";

            // Display error message.
            labelErrorTitle.Text = errorTitle;
            labelErrorMessage.Text = errorMessage;
        }

        private void AutoRedirect()
        {
            string ADSCAPTCHA_URL = ConfigurationSettings.AppSettings["URL"];

            // Set meta data.
            HtmlMeta meta = new HtmlMeta();
            meta.Attributes.Add("http-equiv", "refresh");
            meta.Attributes.Add("content", "7;url=" + ADSCAPTCHA_URL);
            Page.Header.Controls.Add(meta);

            labelRedirection.Visible = true;
        }
    }
}
