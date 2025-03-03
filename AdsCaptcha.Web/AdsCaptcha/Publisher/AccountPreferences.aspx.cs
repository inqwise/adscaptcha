using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using AjaxControlToolkit;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class AccountPreferences : System.Web.UI.Page, IPublisherAccountPreferences
    {
        private TP_PUBLISHER _publisher;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["PublisherLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["PublisherId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                int publisherId = Convert.ToInt16(Session["PublisherId"]);

                _publisher = PublisherBLL.GetPublisher(publisherId);

                if (_publisher == null)
                {
                    // TODO: Handle publisher not exsists
                    throw new Exception("Publisher not exists");
                }
            }
            catch 
            {
                Response.Redirect("ManageWebsites.aspx");
            }

            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Publisher, Master.Page.Header);

                // If saved, display saved message.
                string isSaved = (Request.QueryString["saved"] == null ? "false" : Request.QueryString["saved"]);
                if (isSaved == "true")
                    labelChangesSaved.Visible = true;

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

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            // Set navigation path.
            labelNavigationPath.Text = "<a href=\"AccountPreferences.aspx\">My Account</a>" + " &gt; " +
                                       "Account Preferences";

            // Fill country list.
            listCountry.DataSource = DictionaryBLL.GetCountryList();
            listCountry.DataBind();

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
            checkGetEmailAnnouncements.Checked = (_publisher.Get_Email_Announcements == 1) ? true : false;
            checkGetEmailNewsletters.Checked = (_publisher.Get_Email_Newsletters == 1) ? true : false;

            // Add country change script.
            listCountry.Attributes["onChange"] = "changeCountry();";
        }

        /// <summary>
        /// Submit sign up form.
        /// </summary>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                if (!validatorFirstName.IsValid)
                    textFirstName.Focus();
                else if (!validatorLastName.IsValid)
                    textLastName.Focus();
                else if (!validatorCountry.IsValid)
                    listCountry.Focus();
                else if (!validatorAddress.IsValid)
                    textAddress.Focus();
                else if (!validatorCity.IsValid)
                    textCity.Focus();
                else if (!validatorPhone.IsValid)
                    textPhone.Focus();

                // Hide changes saved status.
                labelChangesSaved.Visible = false;

                return;
            }

            // Get form data.
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
            bool getEmailAnnouncements = checkGetEmailAnnouncements.Checked;
            bool getEmailNewsletters = checkGetEmailNewsletters.Checked;

            // Update publisher data.
            PublisherBLL.Update(_publisher.Publisher_Id,
                                _publisher.Status_Id,
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
                                _publisher.Credit_Method_Id, 
                                _publisher.Revenue_Share_Pct, 
                                _publisher.Min_Check_Amount,
                                (_publisher.Is_Branded == 1 ? true : false),
                                getEmailAnnouncements,
                                getEmailNewsletters
                                );

            // Redirect to page.
            Response.Redirect("AccountPreferences.aspx?saved=true");
        }
    }
}
