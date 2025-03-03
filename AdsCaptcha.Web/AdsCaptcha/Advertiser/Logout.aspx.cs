using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set metadata (title, keywords and description).
            Metadata.SetMetadata(Metadata.Pages.Advertiser, Master.Page.Header);

            // Clear session login values.
            Session["AdvertiserId"] = null;
            Session["AdvertiserEmail"] = null;

            // Redirect to start page.
            Response.Redirect("StartPage.aspx");
        }
    }
}
