using System;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class EditDeveloper : System.Web.UI.Page
    {
        private TD_DEVELOPER _developer;
        private int developerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                developerId = int.Parse(Page.Request.QueryString["DeveloperId"].ToString());

                _developer = DeveloperBLL.GetDeveloper(developerId);

                if (_developer == null)
                {
                    throw new Exception("Developer not exists");
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
            labelBreadCrambs.Text = "<a href='ManageDevelopers.aspx'>" + "Developers" + "</a>" + " » " +
                                    "Developer: " + "<a href='ManagePublishers.aspx?DeveloperId=" + _developer.Developer_Id.ToString() + "'>" + _developer.Email + "</a>" + " » " +
                                    "Edit";

            // Fill statuses list.
            listStatus.DataSource = DictionaryBLL.GetStatusList();
            listStatus.DataBind();
            listStatus.ClearSelection();
            listStatus.Items.FindByValue(_developer.Status_Id.ToString()).Selected = true;

            // Fill country list.
            listCountry.DataSource = DictionaryBLL.GetCountryList();
            listCountry.DataBind();

            // Fill credit method list.
            listCreditMethod.DataSource = DictionaryBLL.GetCreditMethodList();
            listCreditMethod.DataBind();

            // Set minimum check amount.
            validatorMinCheckAmount2.Type = ValidationDataType.Integer;
            validatorMinCheckAmount2.MinimumValue = ApplicationConfiguration.DEFAULT_MIN_CHECK_AMOUNT.ToString();
            validatorMinCheckAmount2.MaximumValue = "99999";
            validatorMinCheckAmount2.ErrorMessage = "* Min. $" + ApplicationConfiguration.DEFAULT_MIN_CHECK_AMOUNT.ToString();

            // Fill data from db.
            labelEmail.Text = _developer.Email;
            textFirstName.Text = _developer.First_Name;
            textLastName.Text = _developer.Last_Name;
            textCompanyName.Text = _developer.Company_Name;
            textAddress.Text = _developer.Address;
            textCity.Text = _developer.City;
            listCountry.Items.FindByValue(_developer.Country_Id.ToString()).Selected = true;
            textState.Text = _developer.State;
            textZipCode.Text = _developer.Zip_Code;
            textPhone.Text = _developer.Phone;
            textPhone2.Text = _developer.Phone_2;
            listCreditMethod.Items.FindByValue(_developer.Credit_Method_Id.ToString()).Selected = true;
            textMinCheckAmount.Text = _developer.Min_Check_Amount.ToString();
            textRevenueSharePct.Text = _developer.Revenue_Share_Pct.ToString();

            // Add country change script.
            listCountry.Attributes["onChange"] = "changeCountry();";
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
            int statusId = int.Parse(listStatus.SelectedItem.Value);
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

            // Update developer data.
            DeveloperBLL.Update(_developer.Developer_Id,
                                statusId,
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
                                _developer.Get_Email_Announcements == 1 ? true : false,
                                _developer.Get_Email_Newsletters == 1 ? true : false
                                );

            // Change password
            if (textPassword.Text != "" && textPasswordConfirm.Text == textPassword.Text)
            {
                DeveloperBLL.ChangePassword(_developer.Email, textPassword.Text, false);
            }

            // Redirect to manage developers page.
            Response.Redirect("ManageDevelopers.aspx");
        }
    }
}
