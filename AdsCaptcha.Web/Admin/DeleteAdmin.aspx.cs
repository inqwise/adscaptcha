using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class DeleteAdmin : System.Web.UI.Page
    {
        private int adminId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                adminId = int.Parse(Page.Request.QueryString["AdminId"].ToString());

                // TODO: Handle - admin not exists, admin tries to delete himself

                // Delete admin.
                AdminBLL.Delete(adminId);
            }
            catch
            {
                Response.Redirect("ManageAdmins.aspx");
            }

            // Return to manage admins page.
            Response.Redirect("ManageAdmins.aspx");
        }

        private void InitControls()
        {
        }
    }
}
