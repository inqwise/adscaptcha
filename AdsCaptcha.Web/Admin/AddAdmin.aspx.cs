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
    public partial class AddAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                InitControls();
            }
        }

        /// <summary>
        /// Check if browser is Chrome or Safari.
        /// If so, on pages with Grid - Disable partial page rendering of the ScriptManager.
        /// </summary>
        protected void scriptManagerOnInit(object sender, EventArgs e)
        {
            if (Request.Browser.Browser.ToUpper().Contains("SAFARI") || Request.Browser.Browser.ToUpper().Contains("CHROME"))
            {
                ScriptManager.EnablePartialRendering = false;
            }
        }

        private void InitControls()
        {
            // Set "bread-crambs" text.
            labelBreadCrambs.Text = "<a href='ManageAdmins.aspx'>" + "Admins" + "</a>" + " » " + 
                                    "Add Admin";
        }

        /// <summary>
        /// Submit sign up form.
        /// </summary>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                return;
            }

            // Get values.
            string name = textName.Text.Trim();
            string email = textEmail.Text.Trim();
            string password = textPassword.Text;

            // Add admin.
            int adminId = AdminBLL.Add(email, password, name);

            // Redirect to manage admins page.
            Response.Redirect("ManageAdmins.aspx");
        }

        #region Validation Controls

        /// <summary>
        /// Validates that the user is exsists.
        /// </summary>
        protected void checkEmailExsists_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check if admin already exists (by email).
            if (AdminBLL.GetAdminIdByEmail(textEmail.Text) == 0)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }

        #endregion Validation Controls
    }
}
