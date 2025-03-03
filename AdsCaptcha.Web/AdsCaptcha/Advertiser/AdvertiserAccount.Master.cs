using System;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.BLL;
using Advertiser;

public partial class Advertiser_AdvertiserAccount : System.Web.UI.MasterPage
{
    public string BaseUrl = "/";

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["AdvertiserId"] == null)
        //{
        //    Response.Redirect("~/Advertiser/Login.aspx");
        //}
        int advertiserId = Convert.ToInt16(Session["AdvertiserId"]);
        decimal totalCharges = AdvertiserBLL.GetTotalCharges(advertiserId);
        labelChargesSum.Text = String.Format("${0:#,##0.00}", totalCharges);

        if ((this.MainContent.Page is IAdvertiserManageCampaigns) ||
            (this.MainContent.Page is IAdvertiserNewAd) ||
            (this.MainContent.Page is IAdvertiserManageAds) ||
            (this.MainContent.Page is IAdvertiserEditCampaign) ||
            (this.MainContent.Page is IAdvertiserEditAd))
        {
            aMenuManageCampaigns.Attributes["class"] = "activeitem";
        }
        else if ((this.MainContent.Page is IAdvertiserBillingSummary) ||
                    (this.MainContent.Page is IAdvertiserBillingCreditCard) ||
                    (this.MainContent.Page is IAdvertiserBillingPayPal))
        {
            aMenuBillingSummary.Attributes["class"] = "activeitem";
        }
        else if ((this.MainContent.Page is IAdvertiserAccountPreferences) ||
                    (this.MainContent.Page is IAdvertiserChangePassword))
        {
            aMenuAccountPreferences.Attributes["class"] = "activeitem";
        }
        else if ((this.MainContent.Page is IAdvertiserNewCampaign))
        {
            aMenuNewCampaign.Attributes["class"] = "activeitem";
        }


    }
}


