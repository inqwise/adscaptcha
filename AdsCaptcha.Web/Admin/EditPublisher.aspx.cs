using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class EditPublisher : System.Web.UI.Page
    {
        private TP_PUBLISHER _publisher;
        private int _developerId = 0;
        private int publisherId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                _developerId = int.Parse(Page.Request.QueryString["DeveloperId"].ToString());
            }
            catch { }

            try
            {
                publisherId = int.Parse(Page.Request.QueryString["publisherId"].ToString());

                _publisher = PublisherBLL.GetPublisher(publisherId);

                if (_publisher == null)
                {
                    throw new Exception("Publisher not exists");
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
            if (_developerId == 0)
            {
                // Set "bread-crambs" text.
                labelBreadCrambs.Text = "<a href='ManagePublishers.aspx'>" + "Site Owners" + "</a>" + " » " +
                                        "Site Owner: " + "<a href='ManageWebsites.aspx?PublisherId=" + _publisher.Publisher_Id.ToString() + "'>" + _publisher.Email + "</a>" + " » " +
                                        "Edit";
            }
            else
            {
                TD_DEVELOPER developer = DeveloperBLL.GetDeveloper(_developerId);

                // Set "bread-crambs" text.
                labelBreadCrambs.Text = "<a href='ManageDevelopers.aspx'>" + "Developers" + "</a>" + " » " +
                                        "Developer: " + "<a href='ManagePublishers.aspx?DeveloperId=" + _developerId.ToString() + "'>" + developer.Email + "</a> (<a href='EditDeveloper.aspx?DeveloperId=" + _developerId.ToString() + "'>edit</a>)" + " » " +
                                        "Site Owner: " + "<a href='ManageWebsites.aspx?DeveloperId=" + _developerId.ToString() + "&PublisherId=" + _publisher.Publisher_Id.ToString() + "'>" + _publisher.Email + "</a> (<a href='EditPublisher.aspx?DeveloperId=" + _developerId.ToString() + "&PublisherId=" + _publisher.Publisher_Id.ToString() + "'>edit</a>)" + " » " +
                                        "Edit";
            }

            // Fill statuses list.
            listStatus.DataSource = DictionaryBLL.GetStatusList();
            listStatus.DataBind();
            listStatus.ClearSelection();
            listStatus.Items.FindByValue(_publisher.Status_Id.ToString()).Selected = true;

            // Fill country list.
            listCountry.DataSource = DictionaryBLL.GetCountryList();
            listCountry.DataBind();

            // Fill credit method list.
            listCreditMethod.DataSource = DictionaryBLL.GetCreditMethodList();
            listCreditMethod.DataBind();

            if (_publisher.Country_Id == null)
            {
                listCountry.ClearSelection();
                listCountry.Items.Insert(0, new ListItem("-- Select --", "0"));
                listCountry.SelectedIndex = 0;
            }
            else
            {
                listCountry.Items.FindByValue(_publisher.Country_Id.ToString()).Selected = true;
            }

            // Set minimum check amount.
            validatorMinCheckAmount2.Type = ValidationDataType.Integer;
            validatorMinCheckAmount2.MinimumValue = ApplicationConfiguration.DEFAULT_MIN_CHECK_AMOUNT.ToString();
            validatorMinCheckAmount2.MaximumValue = "99999";
            validatorMinCheckAmount2.ErrorMessage = "* Min. $" + ApplicationConfiguration.DEFAULT_MIN_CHECK_AMOUNT.ToString();

            // Fill data from db.
            labelEmail.Text = _publisher.Email;
            textFirstName.Text = _publisher.First_Name;
            textLastName.Text = _publisher.Last_Name;
            textCompanyName.Text = _publisher.Company_Name;
            textAddress.Text = _publisher.Address;
            textCity.Text = _publisher.City;
            textState.Text = _publisher.State;
            textZipCode.Text = _publisher.Zip_Code;
            textPhone.Text = _publisher.Phone;
            textPhone2.Text = _publisher.Phone_2;
            if (_publisher.Credit_Method_Id > 0)
                listCreditMethod.Items.FindByValue(_publisher.Credit_Method_Id.ToString()).Selected = true;
            textMinCheckAmount.Text = _publisher.Min_Check_Amount.ToString();
            textPubRevenueSharePct.Text = _publisher.Revenue_Share_Pct.ToString();

            if (_publisher.Developer_Id != null)
            {
                TD_DEVELOPER developer = DeveloperBLL.GetDeveloper((int)_publisher.Developer_Id);
                textDevRevenueSharePct.Text = developer.Revenue_Share_Pct.ToString();
            }
            else
            {
                validatorDevShareRevenue1.Enabled = false;
                validatorDevShareRevenue2.Enabled = false;
                textDevRevenueSharePct.Enabled = false;
            }

            // Add country change script.
            listCountry.Attributes["onChange"] = "changeCountry();";

            // Branded.
            checkBranded.Checked = (_publisher.Is_Branded == 1 ? true : false);
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
            int pubRevenueSharePct = Convert.ToInt16(textPubRevenueSharePct.Text.Trim());
            bool isBranded = checkBranded.Checked;

            // Update publisher data.
            PublisherBLL.Update(_publisher.Publisher_Id,
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
                                pubRevenueSharePct,
                                minCheckAmount,
                                isBranded,
                                _publisher.Get_Email_Announcements == 1 ? true : false,
                                _publisher.Get_Email_Newsletters == 1 ? true : false
                                );

            // Update developer's revenue share pct.
            if (_publisher.Developer_Id != null)
            {
                int devRevenueSharePct = Convert.ToInt16(textDevRevenueSharePct.Text.Trim());
                
                DeveloperBLL.Update((int)_publisher.Developer_Id, devRevenueSharePct);
            }

            // Change password
            if (textPassword.Text != "" && textPasswordConfirm.Text == textPassword.Text)
            {
                PublisherBLL.ChangePassword(_publisher.Email, textPassword.Text, false);
            }

            // Redirect to manage publishers page.
            if (_developerId == 0)
            {
                Response.Redirect("ManagePublishers.aspx");
            }
            else
            {
                Response.Redirect("ManagePublishers.aspx?DeveloperId=" + _developerId.ToString());
            }
        }
    }
}
