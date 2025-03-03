using System;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class EditAd : System.Web.UI.Page
    {
        protected TA_AD _ad;
        private int advertiserId;
        private int campaignId;
        private int adId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                advertiserId = Convert.ToInt16(Page.Request.QueryString["AdvertiserId"].ToString());
                campaignId = Convert.ToInt16(Page.Request.QueryString["CampaignId"].ToString());
                adId = Convert.ToInt16(Page.Request.QueryString["AdId"].ToString());

                _ad = AdBLL.GetAd(advertiserId, campaignId, adId);
            }
            catch
            {
                Response.Redirect("StartPage.aspx");
            }
                
            if (_ad == null)
            {
                Response.Redirect("ManageAds.aspx?AdvertiserId=" + advertiserId.ToString() + "&CampaignId=" + campaignId.ToString());
            }
            
            // Check if ad type not supported
            if (DictionaryBLL.GetAdTypeList().Where(t => t.Item_Id == _ad.Type_Id).Count() == 0)
            {
                Response.Redirect("ManageAds.aspx?AdvertiserId=" + advertiserId.ToString() + "&CampaignId=" + campaignId.ToString());
            }

            if (!IsPostBack)
            {
                InitControls();
            }

            ShowHide();
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
            string advertiser = AdvertiserBLL.GetAdvertiser(_ad.Advertiser_Id).Email;
            string campaign = CampaignBLL.GetCampaign(_ad.Advertiser_Id, _ad.Campaign_Id).Campaign_Name;

            // Set "bread-crambs" text.
            labelBreadCrambs.Text = "<a href='ManageAdvertisers.aspx'>" + "Advertisers" + "</a>" + " » " +
                                    "Advertiser: " + "<a href='ManageCampaigns.aspx?AdvertiserId=" + _ad.Advertiser_Id.ToString() + "'>" + advertiser + "</a> (<a href='EditAdvertiser.aspx?AdvertiserId=" + _ad.Advertiser_Id.ToString() + "'>edit</a>)" + " » " +
                                    "Campaign: " + "<a href='ManageAds.aspx?AdvertiserId=" + _ad.Advertiser_Id.ToString() + "&CampaignId=" + _ad.Campaign_Id.ToString() + "'>" + campaign + "</a> (<a href='EditCampaign.aspx?AdvertiserId=" + _ad.Advertiser_Id.ToString() + "&CampaignId=" + _ad.Campaign_Id.ToString() + "'>edit</a>)" + " » " +
                                    "Ad: " + _ad.Ad_Name + " » " + 
                                    "Edit";

            // Set image restrictions.
            labelImageFormat.Text = ApplicationConfiguration.ALLOWED_IMAGE_TYPE;
            labelImageFileSize.Text = (ApplicationConfiguration.ALLOWED_IMAGE_SIZE / 1024).ToString();

            // Set video restrictions.
            //labelVideoFormat.Text = ApplicationConfiguration.ALLOWED_VIDEO_TYPE;
            //labelVideoFileSize.Text = (ApplicationConfiguration.ALLOWED_VIDEO_SIZE / 1024).ToString();
            //labelVideoDimensions.Text = null; // TODO: Set video dimensions restrictions.

            // Set max slogan length.
            textAdSlogan.MaxLength = ApplicationConfiguration.MAX_SLOGAN_LENGTH;

            // Fill ad type list.
            listAdType.DataSource = DictionaryBLL.GetAdTypeList();
            listAdType.DataBind();
            listAdType.ClearSelection();

            listAdType.Attributes.Add("onChange", "javascript:OnAdTypeChange();");

            // Fill statuses list.
            listStatus.DataSource = DictionaryBLL.GetStatusList();
            listStatus.DataBind();
            listStatus.ClearSelection();

            // Set ad values from DB.
            textAdName.Text = _ad.Ad_Name;
            listStatus.Items.FindByValue(_ad.Status_Id.ToString()).Selected = true;
            textAdSlogan.Text = _ad.Ad_Slogan;
            checkRtl.Checked = _ad.Rtl.GetValueOrDefault();
            textLinkUrl.Text = _ad.Ad_Url;
            listAdType.Items.FindByValue(_ad.Type_Id.ToString()).Selected = true;
            textMaxPpt.Text = _ad.Max_Cpt.ToString();
            textLinkUrl.Text = _ad.Ad_Url;
            txtImageClickURL.Text = _ad.Ad_Url;
            txtLikeURL.Text = _ad.Ad_Like_Url;

            // Hide/show image preview.
            if (_ad.Ad_Image != null)
            {
                int PREVIEW_WIDTH = 250;
                float ratio = 1;

                int width = Convert.ToInt16(_ad.Width);
                int height = Convert.ToInt16(_ad.Height);

                AdWidth.Value = width.ToString();
                AdHeight.Value = height.ToString();

                if (width > PREVIEW_WIDTH)
                {
                    ratio = (float)width / (float)PREVIEW_WIDTH;
                    height = (int)((float)height / ratio);
                }
                else
                {
                    ratio = (float)PREVIEW_WIDTH / (float)width;
                    height = (int)((float)height * ratio);
                }

                width = PREVIEW_WIDTH;

                UploadedImageHolder.Style.Add("display", "block");
                UploadPreview.Src = "/handlers/Image.ashx?AdId=" + _ad.Ad_Id;
                UploadPreview.Width = width / 2;
                UploadPreview.Height = height / 2;
                UploadWidth.Value = width.ToString();
                UploadHeight.Value = height.ToString();                
            }
            else
            {
                UploadedImageHolder.Style.Add("display", "none");
                UploadPreview.Src = "";
                UploadWidth.Value = "0";
                UploadHeight.Value = "0";
                AdWidth.Value = "0";
                AdHeight.Value = "0";
            }

            // Hide image uploader validation message.
            labelImageValidation.Style.Add("display", "none");
        }

        /// <summary>
        /// Show/hide dynamic content.
        /// </summary>
        private void ShowHide()
        {
            string displaySlogan = "none";
            string displayImage = "none";
            string displayVideo = "none";
            string displayCoupon = "none";
            string displayAdGame = "none";
            string displayTextMessageInput = "none";
            string displayImageInput = "none";
            string displayVideoInput = "none";
            string displayCouponInput = "none";

            // Set ad type attributes.
            switch (int.Parse(listAdType.SelectedValue))
            {
                case (int)AdType.SloganOnly:
                    displaySlogan = "block";
                    displayTextMessageInput = "table-row";
                    AdTypeClickURLHolder.Style.Add("display", "none");
                    AdTypeClickLikeHolder.Style.Add("display", "none");
                    break;
                case (int)AdType.SloganAndImage:
                    displayImage = "block";
                    displayTextMessageInput = "table-row";
                    displayImageInput = "table-row";
                    AdTypeClickURLHolder.Style.Add("display", "block");
                    AdTypeClickURLHolder.Style.Add("display", "table-row");
                    AdTypeClickLikeHolder.Style.Add("display", "block");
                    AdTypeClickLikeHolder.Style.Add("display", "table-row");
                    break;
                case (int)AdType.SloganAndVideo:
                    displayVideo = "block";
                    displayTextMessageInput = "table-row";
                    displayVideoInput = "table-row";
                    break;
                case (int)AdType.Slide2Fit:
                    displayAdGame = "block";
                    displayImageInput = "table-row";
                    AdTypeClickURLHolder.Style.Add("display", "block");
                    AdTypeClickURLHolder.Style.Add("display", "table-row");
                    AdTypeClickLikeHolder.Style.Add("display", "block");
                    AdTypeClickLikeHolder.Style.Add("display", "table-row");
                    break;
            }

            AdTypeHolder0.Style.Add("display", displaySlogan); // Slogan Only
            AdTypeHolder1.Style.Add("display", displayImage);  // Slogan & Image
            AdTypeHolder2.Style.Add("display", displayAdGame); // Image Only
            AdTypeHolder3.Style.Add("display", displayCoupon); // Slogan & Coupon
            AdTypeHolder4.Style.Add("display", displayVideo);  // Slogan & Video
            TextMessageHolder.Style.Add("display", displayTextMessageInput);
            AdTypeImageHolder.Style.Add("display", displayImageInput);
            AdTypeVideoHolder.Style.Add("display", displayVideoInput);
            AdTypeCouponHolder.Style.Add("display", displayCouponInput);
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // TODO: Validate upload image/video.

            // If form is not valid, exit
            if (!Page.IsValid)
            {
                return;
            }

            // Check upload validation.
            if (!IsUploadImageExists())
            {
                return;
            }

            // Check image dimension validation.
            if (!IsDimensionValid())
            {
                UploadHidden.Focus();
                return;
            }

            TA_CAMPAIGN campaign = new TA_CAMPAIGN();
            campaign = CampaignBLL.GetCampaign(_ad.Advertiser_Id, _ad.Campaign_Id);

            // Get values.
            string adName = textAdName.Text.Trim();
            int statusId = int.Parse(listStatus.SelectedItem.Value);
            int adType = int.Parse(listAdType.SelectedValue);
            string adSlogan = textAdSlogan.Text.Trim();
            bool rtl = checkRtl.Checked;
            decimal maxCpt = decimal.Parse(textMaxPpt.Text.Trim());

            string adImage = null;
            string adVideo = null;
            string adUrl = null;
            string adLikeUrl = null;
            Nullable<int> width = null;
            Nullable<int> height = null;

            // Handle ad type attributes.
            switch (int.Parse(listAdType.SelectedValue))
            {
                case (int)AdType.SloganOnly:
                    break;
                case (int)AdType.Slide2Fit:
                case (int)AdType.SloganAndImage:
                    // image click url support - START
                    // image click url support - START
                    if (!string.IsNullOrEmpty(txtImageClickURL.Text))
                    {
                        adUrl = txtImageClickURL.Text.Trim();
                    }

                    if (!string.IsNullOrEmpty(txtLikeURL.Text))
                    {
                        adLikeUrl = txtLikeURL.Text.Trim();
                    }
                    // image click url support - END
                    if (UploadHidden.Value != "")
                    {
                        adImage = UploadHidden.Value;
                        width = Convert.ToInt16(UploadWidth.Value);
                        height = Convert.ToInt16(UploadHeight.Value);
                    }
                    else
                    {
                        // Get current/original ad image attributes.
                        adImage = _ad.Ad_Image;
                        width = _ad.Width;
                        height = _ad.Height;
                    }
                    break;

                case (int)AdType.SloganAndVideo:
                    break;
            }

            // Update ad.
            AdBLL.Update(_ad.Advertiser_Id,
                         _ad.Campaign_Id,
                         _ad.Ad_Id, 
                         adName,
                         statusId,
                         adType,
                         width,
                         height,
                         adSlogan,
                         adImage,
                         adVideo,
                         adUrl,
                         adLikeUrl,
                         maxCpt,
                         rtl);

            // Redirect to manage ads page.
            Response.Redirect("ManageAds.aspx?AdvertiserId=" + _ad.Advertiser_Id.ToString() + "&CampaignId=" + _ad.Campaign_Id.ToString());
        }

        /// <summary>
        /// Validates that the ad doesn't already exist.
        /// </summary>
        protected void checkAdExist_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check and return if ad already exists (by name).
            e.IsValid = (AdBLL.IsDuplicateNameForCampaign(_ad.Advertiser_Id, _ad.Campaign_Id, _ad.Ad_Id, textAdName.Text) ? false : true);
        }

        /// <summary>
        /// Check if text message entered only if not slider.
        /// </summary>
        protected void checkTextMessage_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            int adType = int.Parse(listAdType.SelectedValue);
            string textMessage = textAdSlogan.Text.Trim();

            if (adType != (int)AdType.Slide2Fit)
            {
                if (string.IsNullOrEmpty(textMessage))
                {
                    e.IsValid = false;
                    validatorAdSlogan.ErrorMessage = "* Required";
                    return;
                }
                // TODO: Check english letters only
            }

            e.IsValid = true;
        }

        /// <summary>
        /// Validates upload image file.
        /// </summary>
        private bool IsUploadImageExists()
        {
            // First of all, if selected ad type is not image, we can skip the check (actually, Valid=True).
            if (int.Parse(listAdType.SelectedValue) != (int)AdType.SloganAndImage &&
                int.Parse(listAdType.SelectedValue) != (int)AdType.Slide2Fit)
            {
                return true;
            }

            // Check if already image and the image didn't change.
            if (_ad.Ad_Image != null && (UploadHidden.Value == null || UploadHidden.Value == ""))
            {
                return true;
            }

            //bool urlCheck = General.IsUrlExists(ConfigurationSettings.AppSettings["URL"] + ApplicationConfiguration.UPLOADS_DIRECTORY + UploadHidden.Value);
            //bool urlCheck = General.IsUrlExists(ConfigurationSettings.AppSettings["AWSBucketUrl"] + UploadHidden.Value);

            // Check if temporary (uploaded file) exists.
            //if (!urlCheck)
            //{
            //    labelImageValidation.Text = "* Required";
            //    labelImageValidation.Style["display"] = "block";
            //    return false;
            //}
            //else
            //{
            //    labelImageValidation.Style["display"] = "none";
                return true;
            //}
        }

        private bool IsDimensionValid()
        {
            return true;
            int adType = int.Parse(listAdType.SelectedValue);

            // First of all, if selected ad type is not image, we can skip the check (actually, Valid=True).
            if (adType != (int)AdType.SloganAndImage &&
                adType != (int)AdType.Slide2Fit)
            {
                return true;
            }

            int width = int.Parse(AdWidth.Value);
            int height = int.Parse(AdHeight.Value);

            if (!AdBLL.IsDimensionValidForAdmin(adType, width, height))
            {
                labelImageValidation.Text = "* Image dimension is not allowed for ad type";
                labelImageValidation.Style["display"] = "block";
                return false;
            }
            else
            {
                labelImageValidation.Style["display"] = "none";
                return true;
            }
        }
    }
}
