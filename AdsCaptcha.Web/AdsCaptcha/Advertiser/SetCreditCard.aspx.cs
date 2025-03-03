using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;
using AjaxControlToolkit;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class SetCreditCard : System.Web.UI.Page
    {
        private TA_ADVERTISER _advertiser;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdvertiserLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdvertiserId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                int advertiserId = Convert.ToInt16(Session["AdvertiserId"]);

                _advertiser = AdvertiserBLL.GetAdvertiser(advertiserId);

                if (_advertiser == null)
                {
                    // TODO: Handle advertiser not exsists
                    throw new Exception("Advertiser not exists");
                }
            }
            catch 
            {
                Response.Redirect(Page.Request.Url.ToString());
            }

            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Advertiser, Master.Page.Header);

                if (_advertiser.Payment_Method_Id != (int)PaymentMethod.CreditCard)
                {
                    panelForm.Visible = false;
                    labelMessage.Text = "Your payment method is not credit card.";
                    panelMessage.Visible = true;
                }
                else
                {
                    InitControls();
                }
            }
        }

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            // Set navigation path.
            labelNavigationPath.Text = "<a href=\"AccountPreferences.aspx\">My Account</a>" + " &gt; " +
                                       "Set Credit Card";

            // Fill credit cards list.
            listCreditCard.DataTextField = "Item_Desc";
            listCreditCard.DataValueField = "Item_Id";
            listCreditCard.DataSource = DictionaryBLL.GetCreditCardList();
            listCreditCard.DataBind();
            listCreditCard.ClearSelection();
            listCreditCard.Items.Insert(0, new ListItem("-- Select --", "0"));
            listCreditCard.SelectedIndex = 0;
        
            // Expiration date.
            for (int i = 1; i <= 12; i++)
            {
                int month = i;
                listExpirationMonth.Items.Add(month.ToString().PadLeft(2, '0'));
            }
            for (int i = 0; i < 10; i++)
            {
                int year = DateTime.Today.Year + i;
                listExpirationYear.Items.Add(year.ToString());
            }
        }

        /// <summary>
        /// Submit sign up form.
        /// </summary>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                if (!validatorCreditCard.IsValid)
                    listCreditCard.Focus();
                else if (!validatorCardNumber.IsValid)
                    textCardNumber.Focus();

                return;
            }

            // Get data.
            int creditCard = Convert.ToInt16(listCreditCard.SelectedValue);
            string cardNumber = textCardNumber.Text.Trim();
            string cardExpiration = listExpirationMonth.SelectedValue + listExpirationYear.SelectedValue.Substring(2, 2);

            // Validate credit card and get card id.
            CreditCardBLL.CreditCardResult result = CreditCardBLL.IsValid(cardNumber, cardExpiration, ApplicationConfiguration.DEFAULT_MIN_BILLING_AMOUNT);
            
            // Check if card is valid.
            if (!result.Status)
            {
                labelError.Text = result.ErrorMessage;
                labelError.Visible = true;
                return;
            }

            // Get card id.
            string cardId = result.CardID;

            // Update credit card data.
            AdvertiserBLL.UpdateCreditCard(_advertiser.Advertiser_Id, cardId, cardExpiration);

            // Redirect to account preferences page.
            Response.Redirect("AccountPreferences.aspx");
        }
    }
}
