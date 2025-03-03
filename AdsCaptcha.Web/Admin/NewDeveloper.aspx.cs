using System;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class NewDeveloper : System.Web.UI.Page
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
            labelBreadCrambs.Text = "<a href='ManageDevelopers.aspx'>" + "Developers" + "</a>" + " » " +
                                    "New Developer";

            // Set defaults.
            textMinCheckAmount.Text = ApplicationConfiguration.DEFAULT_MIN_CHECK_AMOUNT.ToString();
            textRevenueSharePct.Text = ApplicationConfiguration.DEFAULT_REVENUE_SHARE_DEVELOPER.ToString();

            // Fill country list.
            listCountry.DataSource = DictionaryBLL.GetCountryList();
            listCountry.DataBind();
            listCountry.ClearSelection();
            listCountry.Items.Insert(0, new ListItem("-- Select --", "0"));
            listCountry.SelectedIndex = 0;

            // Add country change script.
            listCountry.Attributes["onChange"] = "changeCountry();";

            // Fill credit method list.
            listCreditMethod.DataSource = DictionaryBLL.GetCreditMethodList();
            listCreditMethod.DataBind();
            listCreditMethod.ClearSelection();
            listCreditMethod.Items.Insert(0, new ListItem("-- Select --", "0"));
            listCreditMethod.SelectedIndex = 0;

            // Set minimum check amount.
            validatorMinCheckAmount2.Type = ValidationDataType.Integer;
            validatorMinCheckAmount2.MinimumValue = ApplicationConfiguration.DEFAULT_MIN_CHECK_AMOUNT.ToString();
            validatorMinCheckAmount2.MaximumValue = "99999";
            validatorMinCheckAmount2.ErrorMessage = "* Min. $" + ApplicationConfiguration.DEFAULT_MIN_CHECK_AMOUNT.ToString();
        }

        /// <summary>
        /// Submit form.
        /// </summary>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                return;
            }

            // Get form data.
            string email = textEmail.Text.Trim();
            string password = textPassword.Text;
            string firstName = textFirstName.Text.Trim();
            string lastName = textLastName.Text.Trim();
            string companyName = textCompanyName.Text.Trim();
            string address = textAddress.Text.Trim();
            string city = textCity.Text.Trim();
            string state = textState.Text.Trim();
            int country = Convert.ToInt16(listCountry.SelectedValue);
            string zipCode = textZipCode.Text.Trim();
            string phone = textPhone.Text.Trim();
            string phone2 = textPhone2.Text.Trim();
            int creditMethod = Convert.ToInt16(listCreditMethod.SelectedValue);
            int minCheckAmount = Convert.ToInt16(textMinCheckAmount.Text.Trim());
            int revenueSharePct = Convert.ToInt16(textRevenueSharePct.Text.Trim());
            bool getEmailAnnouncements = checkGetEmailAnnouncements.Checked;
            bool getEmailNewsletters = checkGetEmailNewsletters.Checked;

            // Activate publisher.
            int developerId = DeveloperBLL.Add(email,
                                               password,
                                               firstName,
                                               lastName,
                                               companyName,
                                               address,
                                               city,
                                               state,
                                               country,
                                               zipCode,
                                               phone,     
                                               phone2,
                                               creditMethod,
                                               revenueSharePct,
                                               minCheckAmount,
                                               getEmailAnnouncements,
                                               getEmailNewsletters
                                               );

            // Send mail to administrator.
            Mail.SendNewDeveloperAdminMail(developerId, email);

            // Redirect to manage developers page.
            Response.Redirect("ManageDevelopers.aspx");
        }

        /// <summary>
        /// Validates that the user is exsists.
        /// </summary>
        protected void checkEmailExsists_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            string email = textEmail.Text.Trim();

            // Check if developer already exists (by email).
            if (DeveloperBLL.GetDeveloper(email) == null)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }

        /// <summary>
        /// Validates that the user is not waiting for activation.
        /// </summary>
        protected void checkWaitForActivation_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            string email = textEmail.Text.Trim();

            // Check if developer already waiting for activation.
            e.IsValid = (DeveloperBLL.IsWaitingForActivation(email) == false);
        }
    }
}
