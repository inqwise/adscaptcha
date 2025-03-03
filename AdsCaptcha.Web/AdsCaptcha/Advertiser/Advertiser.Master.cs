using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inqwise.AdsCaptcha.Advertiser.Master
{
    public partial class Advertiser_Master : MasterPage
    {
        public string APIUrl = System.Configuration.ConfigurationManager.AppSettings["API"];

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if Advertiser is not logged in.
            if (Session["AdvertiserEmail"] == null)
            {
                panelGuest.Visible = true;
                panelUser.Visible = false;
            }
            else
            {
                panelGuest.Visible = false;
                panelUser.Visible = true;
                labelLoginStatus.Text = Session["AdvertiserEmail"].ToString();
            }
        }

        void Page_Init()
        {
            this.ID = "MasterPage";

            bool underMaintenance = (ConfigurationSettings.AppSettings["UnderMaintenance"] == "true" ? true : false);
            if (underMaintenance)
                Response.Redirect("../UnderMaintenance.aspx");
        }
    }
}
