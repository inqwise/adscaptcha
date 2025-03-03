using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class Publisher : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if publisher is not logged in.
            if (Session["PublisherEmail"] == null)
            {
                panelGuest.Visible = true;
                panelUser.Visible = false;
            }
            else
            {
                panelGuest.Visible = false;
                panelUser.Visible = true;
                labelLoginStatus.Text = Session["PublisherEmail"].ToString();
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
