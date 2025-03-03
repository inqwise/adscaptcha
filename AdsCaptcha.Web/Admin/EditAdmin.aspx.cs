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
    public partial class EditAdmin : System.Web.UI.Page
    {
        private TM_ADMIN _admin;
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

                _admin = AdminBLL.GetAdmin(adminId);

                if (_admin == null)
                {
                    throw new Exception("Admin not exists");
                }
            }
            catch
            {
                Response.Redirect("StartPage.aspx");
            }

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
                                    "Admin: " + _admin.Email + " » " +
                                    "Edit";

            // Set values from DB.
            textName.Text = _admin.Name;
            textEmail.Text = _admin.Email;
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

            // Update admin.
            AdminBLL.Update(_admin.Admin_Id, email, name);

            // Redirect to manage admins page.
            Response.Redirect("ManageAdmins.aspx");
        }

        #region Validation Controls

        /// <summary>
        /// Validates that the admin is exsists.
        /// </summary>
        protected void checkEmailExsists_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check if admin already exists (by email).
            e.IsValid = (AdminBLL.IsDuplicateAdmin(_admin.Admin_Id, textEmail.Text) ? false : true);
        }

        #endregion Validation Controls
    }
}
