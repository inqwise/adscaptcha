using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if admin is not logged in.
            if (Session["AdminEmail"] == null)
            {
                labelLoginStatus.Text = "<a href=\"Login.aspx\">Login</a>";
            }
            else
            {
                labelLoginStatus.Text = "<b>" + Session["AdminEmail"].ToString() + "</b>" + "<br/>" + "<a href=\"Logout.aspx\">Logout</a>";
            }
        }

        void Page_Init()
        {
            this.ID = "MasterPage";
        }

         
    }
}
