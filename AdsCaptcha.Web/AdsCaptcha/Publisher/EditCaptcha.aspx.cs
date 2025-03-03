using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class EditCaptcha : System.Web.UI.Page, IPublisherEditCaptcha
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        protected ICaptcha Captcha { get; private set; }
        
        protected string ApiUrl
        {
            get { return ApplicationConfiguration.ApiUrl.Value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["PublisherLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["PublisherId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                int publisherId = Convert.ToInt32(Page.Session["PublisherId"]);
                long captchaId = Convert.ToInt64(Page.Request.QueryString["CaptchaId"]);

                var captchaResult = CaptchasManager.Get(captchaId, publisherId);

                if (captchaResult.HasError)
                {
                    // TODO: Handle captcha not exsists
                    throw new Exception("Captcha not exists");
                }
                else
                {
                    Captcha = captchaResult.Value;
                }
            }
            catch(Exception ex)
            {
                Log.ErrorException("Page_Load: Unexpected error occured", ex);
                Response.Redirect(null == Request.UrlReferrer ? "StartPage.aspx" : Request.UrlReferrer.ToString());
            }
        

            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Publisher, Master.Page.Header);

                //InitControls();
            }

            //ShowHide();
        }
        
        
		/*
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
            TP_WEBSITE website = new TP_WEBSITE();
            website = WebsiteBLL.GetWebsite(_captcha.Publisher_Id, _captcha.Website_Id);

            // Set navigation path.
            labelNavigationPath.Text = "<a href=\"ManageWebsites.aspx\">Websites</a>" + " &gt; " +
                                       "Website: " + "<a href=\"ManageCaptchas.aspx?WebsiteId=" + _captcha.Website_Id.ToString() + "\">" + website.Url + "</a>" + " &gt; " +
                                       "Captcha: " + _captcha.Captcha_Name + " &gt; " +
                                       "Edit";

            // Fill captcha type list.
            listCaptchaType.DataSource = DictionaryBLL.GetCaptchaTypeList();
            listCaptchaType.DataBind();
            listCaptchaType.ClearSelection();

            listCaptchaType.Attributes.Add("onChange", "javascript:ChangeCaptchaType();");
            listThemes.Attributes.Add("onChange", "javascript:OnThemeChange();");
            checkAllowVideo.Attributes.Add("onClick", "javascript:toggleAllowContent();");
            checkAllowImage.Attributes.Add("onClick", "javascript:toggleAllowContent();");
            checkAllowSlogan.Attributes.Add("onClick", "javascript:toggleAllowContent();");
            radioAllowClick.Attributes.Add("onClick", "javascript:toggleAllowContent();");
            rblCommercial.Attributes.Add("onClick", "javascript:ChangeCaptchaType();");

            // Fill captcha dims list.
            listCaptchaDims.DataSource = DictionaryBLL.GetImageDimsList();
            listCaptchaDims.DataTextField = "Name";
            listCaptchaDims.DataValueField = "Name";
            listCaptchaDims.DataBind();
            listCaptchaDims.ClearSelection();
            //listCaptchaDims.Items.FindByValue("200x200").Selected = true;

            // Fill slider dims list.
            listSliderDims.DataSource = DictionaryBLL.GetSliderDimsList();
            listSliderDims.DataTextField = "Name";
            listSliderDims.DataValueField = "Name";
            listSliderDims.DataBind();
            listSliderDims.ClearSelection();
            //listSliderDims.Items.FindByValue("200x200").Selected = true;
            
            // Select dims.
            if (_captcha.Type_Id == (int)CaptchaType.Slider ||
                _captcha.Type_Id == (int)CaptchaType.SlideToFit ||
                _captcha.Type_Id == (int)CaptchaType.RandomImage)
            {
                if (listSliderDims.Items.FindByValue(_captcha.Max_Width.ToString() + "x" + _captcha.Max_Height.ToString()) != null)
                {
                    listSliderDims.SelectedValue = listSliderDims.Items.FindByValue(_captcha.Max_Width.ToString() + "x" + _captcha.Max_Height.ToString()).Value;
                }
            }
            else
            {
                if (listCaptchaDims.Items.FindByValue(_captcha.Max_Width.ToString() + "x" + _captcha.Max_Height.ToString()) != null)
                {
                    listCaptchaDims.SelectedValue = listCaptchaDims.Items.FindByValue(_captcha.Max_Width.ToString() + "x" + _captcha.Max_Height.ToString()).Value;
                }
            }

            int captchaType = _captcha.Type_Id;
            
            // Set captcha type restrictions values from DB.
            if (_captcha.Type_Id == (int)CaptchaType.PayPerType ||
                (_captcha.Type_Id == (int)CaptchaType.TypeWords && (bool)_captcha.IsCommercial))
            {
                checkAllowVideo.Checked = (_captcha.Allow_Video == 1) ? true : false;
                checkAllowImage.Checked = (_captcha.Allow_Image == 1) ? true : false;
                checkAllowSlogan.Checked = (_captcha.Allow_Slogan == 1) ? true : false;
                radioAllowClick.SelectedIndex = (_captcha.Allow_Click == 1) ? 0 : 1;
            }
            else
            {
                checkAllowVideo.Checked = false;
                checkAllowImage.Checked = true;
                checkAllowSlogan.Checked = true;
                radioAllowClick.SelectedIndex = 0;
            }

            // Set allow popup if not security only
            //if (_captcha.Type_Id != (int)CaptchaType.SecurityOnly)
            //{
            //    checkAllowPopup.Checked = (_captcha.Allow_Popup == 1) ? true : false;
            //}
            //else
            //{
            //    checkAllowPopup.Checked = false;
            //}
            checkAllowPopup.Checked = (_captcha.Allow_Popup == 1) ? true : false;

            // Check if captcha is rejected or pending.
            if (website.Status_Id != (int)Status.Running ||
                _captcha.Status_Id == (int)Status.Rejected ||
                _captcha.Status_Id == (int)Status.Pending)
            {
                // Fill statuses list.
                listStatus.Items.Add(new ListItem(DictionaryBLL.GetNameById(_captcha.Status_Id), _captcha.Status_Id.ToString()));

                listStatus.Visible = listStatus.Enabled = false;
                labelStatus.Visible = true;
                labelStatus.CssClass = listStatus.SelectedItem.Text;

                labelStatus.Text = listStatus.SelectedItem.Text;
            }
            else
            {
                // Fill statuses list.
                listStatus.Items.Add(new ListItem(Status.Running.ToString(), ((int)Status.Running).ToString()));
                listStatus.Items.Add(new ListItem(Status.Paused.ToString(), ((int)Status.Paused).ToString()));

                listStatus.Visible = listStatus.Enabled = true;
                labelStatus.Visible = false;
            }

            listStatus.ClearSelection();

            // If captcha is pending, display explanation message.
            if (_captcha.Status_Id == (int)Status.Pending)
                labelStatusPending.Visible = true;
            else
                labelStatusPending.Visible = false;

            // Fill themes list.
            listThemes.DataSource = DictionaryBLL.GetThemeList();
            listThemes.DataTextField = "Theme_Name";
            listThemes.DataValueField = "Theme_Id";
            listThemes.DataBind();
            listThemes.ClearSelection();
            listThemes.SelectedValue = listThemes.Items.FindByValue(_captcha.Theme_Id.ToString()).Value;

            // Set values from db.
            textCaptchaName.Text = _captcha.Captcha_Name;
            listStatus.Items.FindByValue(_captcha.Status_Id.ToString()).Selected = true;

            CaptchaType capType = CaptchaType.SlideToFit;
            switch ((CaptchaType)captchaType)
            {
                case CaptchaType.Slider:
                    capType = CaptchaType.SlideToFit;
                    rblCommercial.SelectedIndex = 0;
                    break;
                case CaptchaType.RandomImage:
                    capType = CaptchaType.SlideToFit;
                    rblCommercial.SelectedIndex = 2;
                    break;
                case CaptchaType.PayPerType:
                case CaptchaType.SecurityOnly:
                    capType = CaptchaType.TypeWords;
                    rblCommercial.SelectedIndex = 0;
                    break;
                default:
                    capType = (CaptchaType)captchaType;
                    if ((bool)_captcha.IsCommercial) rblCommercial.SelectedIndex = 0;
                    else rblCommercial.SelectedIndex = 2;
                    break;
            }

            if (_captcha.IsHouseAds)
            {
                rblCommercial.SelectedIndex = 0;
                //LoadHouseAds(_captcha.Captcha_Id);
            }

            listCaptchaType.Items.FindByValue(Convert.ToInt32(capType).ToString()).Selected = true;

            int securityLevelId;

            // Set captcha security level.
            if (_captcha.Type_Id == (int)CaptchaType.Slider ||
                _captcha.Type_Id == (int)CaptchaType.SlideToFit ||
                _captcha.Type_Id == (int)CaptchaType.RandomImage)
            {
                int defaultSecurityLevel = 1;
                securityLevelId = defaultSecurityLevel;
            }
            else
            {
                securityLevelId = _captcha.Security_Level_Id - (int)DecodeTables.SecurityLevel * 1000;
            }

            sliderSecurityLevel.Value = securityLevelId;
            imageSecurityLevel.ImageUrl = "../Images/Preview/SecurityLevel_" + securityLevelId.ToString() + ".jpg";

            //if ((bool)_captcha.IsCommercial) rblCommercial.SelectedIndex = 0;
            //else rblCommercial.SelectedIndex = 1;
        }

        public string HouseAds = string.Empty;
        public void LoadHouseAds(int captchaId)
        {
            List<TP_CAPTCHA_HOUSEAD> ads = CaptchaBLL.GetHouseAds(captchaId);
            foreach (TP_CAPTCHA_HOUSEAD ad in ads)
            {
                if (HouseAds != string.Empty) HouseAds += ",";
                HouseAds += "{";
                HouseAds += "adid:" + ad.HouseAd_Id.ToString();
                HouseAds += ",imgid:" + ad.DeformedImageID.ToString() ;
                HouseAds += ",clickurl:'" + ad.ClickUrl + "'";
                HouseAds += "}";
            }
        }

        /// <summary>
        /// Show/hide dynamic content.
        /// </summary>
        private void ShowHide()
        {
            string holderPayPerType = "none";
            string holderSecurityOnly = "none";
            string holderSlider = "none";
            string holderSecuritySlider = "none";
            string holderCaptchaDims = "none";
            string holderSliderDims = "none";
            string displayVideo = "none";
            string displayImageClickable = "none";
            string displayImage = "none";
            string displaySlogan = "none";
            string displaySecurity = "none";
            string displaySlider = "none";
            string displaySecuritySlider = "none";

            int captchaType = int.Parse(listCaptchaType.SelectedValue);
            bool isCommercial = rblCommercial.SelectedValue == "1" ? true : false;

            if ((captchaType == (int)CaptchaType.SecurityOnly) ||
                    (captchaType == (int)CaptchaType.TypeWords && !isCommercial))
            {
                holderSecurityOnly = "block";
                displaySecurity = "block";
                holderCaptchaDims = "block";
            }
            else if ((captchaType == (int)CaptchaType.PayPerType) ||
                    (captchaType == (int)CaptchaType.TypeWords && isCommercial))
            {
                holderPayPerType = "block";
                holderCaptchaDims = "block";

                if (checkAllowVideo.Checked == true)
                    displayVideo = "block";
                //else if (radioAllowClick.SelectedIndex == 0)
                //    displayImageClickable = "block";
                else if (checkAllowImage.Checked == true)
                    displayImage = "block";
                else if (checkAllowSlogan.Checked == true)
                    displaySlogan = "block";
                else
                    displayImage = "block";
            }
            else if ((captchaType == (int)CaptchaType.Slider) ||
                    (captchaType == (int)CaptchaType.SlideToFit && isCommercial))
            {
                holderSlider = "block";
                holderSliderDims = "block";
                displaySlider = "block";
            }
            else if ((captchaType == (int)CaptchaType.RandomImage) ||
                    (captchaType == (int)CaptchaType.SlideToFit && !isCommercial))
            {
                holderSecuritySlider = "block";
                holderSliderDims = "block";
                displaySlider = "block";
            }

            // Set clickable options display.
            bool showClick = (captchaType == (int)CaptchaType.PayPerType || captchaType == (int)CaptchaType.Slider || isCommercial);
            //AllowClickOptions.Style.Add("display", showClick ? "block" : "none");

            // Set dimensions display.
            tableCaptchaDims.Style.Add("display", holderCaptchaDims);
            tableSliderDims.Style.Add("display", holderSliderDims);

            // Set displayed preview.
            CaptchaTypePayPerTypeHolder.Style.Add("display", holderPayPerType);
            CaptchaTypeSecurityOnlyHolder.Style.Add("display", holderSecurityOnly);
            CaptchaTypeSliderHolder.Style.Add("display", holderSlider);
            CaptchaTypeSecuritySliderHolder.Style.Add("display", holderSecuritySlider);
            //CaptchaTypeHolder0.Style.Add("display", displaySecurity);
            //CaptchaTypeHolder1.Style.Add("display", displaySlogan);
            //CaptchaTypeHolder2.Style.Add("display", displayImage);
            //CaptchaTypeHolder3.Style.Add("display", displayImageClickable);
            //CaptchaTypeHolder4.Style.Add("display", displayVideo);
            //CaptchaTypeHolder5.Style.Add("display", displaySlider);
            //CaptchaTypeHolder6.Style.Add("display", displaySecuritySlider);
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit
            if (!Page.IsValid)
            {
                if (!validatorCaptchaName1.IsValid || !validatorCaptchaName2.IsValid)
                    textCaptchaName.Focus();
                else if (!validatorStatus.IsValid)
                    listStatus.Focus();
                else if (!validatorCaptchaType.IsValid)
                    listCaptchaType.Focus();
                else if (!validatorAllowContent.IsValid)
                    checkAllowVideo.Focus();
                else if (!validatorCaptchaDims.IsValid)
                    listCaptchaDims.Focus();
                else if (!validatorSliderDims.IsValid)
                    listSliderDims.Focus();

                return;
            }
            
            // Get values.
            int publisherId = _captcha.Publisher_Id;
            int websiteId = _captcha.Website_Id;
            int captchaId = _captcha.Captcha_Id;
            string captchaName = textCaptchaName.Text.Trim();
            int statusId;
            if (_captcha.Status_Id == (int)Status.Pending || _captcha.Status_Id == (int)Status.Rejected)
            {
                statusId = _captcha.Status_Id;
            }
            else
            {
                statusId = int.Parse(listStatus.SelectedItem.Value);
            }
            int captchaTypeId = Convert.ToInt16(listCaptchaType.SelectedValue);
            bool allowPopup = checkAllowPopup.Checked;
            bool allowVideo = checkAllowVideo.Checked;
            bool allowImage = checkAllowImage.Checked;
            bool allowSlogan = checkAllowSlogan.Checked;
            bool allowClick = radioAllowClick.Items[0].Selected;
            int themeId = Convert.ToInt16(listThemes.SelectedValue);

            // Get dimensions.
            int width = 0;
            int height = 0;
            switch (captchaTypeId)
            {
                case (int)CaptchaType.SlideToFit:
                case (int)CaptchaType.Slider:
                case (int)CaptchaType.RandomImage:
                    width = Convert.ToInt16(listSliderDims.SelectedValue.Split('x').First());
                    height = Convert.ToInt16(listSliderDims.SelectedValue.Split('x').Last());
                    break;
                case (int)CaptchaType.PayPerType:
                case (int)CaptchaType.TypeWords:
                    width = Convert.ToInt16(listCaptchaDims.SelectedValue.Split('x').First());
                    height = Convert.ToInt16(listCaptchaDims.SelectedValue.Split('x').Last());
                    break;
                case (int)CaptchaType.SecurityOnly:
                default:
                    width = ApplicationConfiguration.SECURITY_ONLY_WIDTH;
                    height = ApplicationConfiguration.SECURITY_ONLY_HEIGHT;
                    break;
            }

            // Calculate security level id.
            int securityLevelId = Convert.ToInt16(sliderSecurityLevel.Value);
            securityLevelId = (securityLevelId < 1) ? 1 : (securityLevelId > 5) ? 5 : securityLevelId;
            securityLevelId += (int)DecodeTables.SecurityLevel * 1000;

            // Get old Captcha type.
            int oldCaptchaTypeId = _captcha.Type_Id;

            // Update captcha.
            CaptchaBLL.Update(publisherId,
                              websiteId,
                              captchaId,
                              captchaName,
                              statusId,
                              captchaTypeId, 
                              securityLevelId,
                              width,
                              height,
                              allowPopup,
                              allowVideo,
                              allowImage,
                              allowSlogan,
                              allowClick,
                              themeId,
                              rblCommercial.SelectedValue == "1" ? true : false);

            if (rblCommercial.SelectedValue == "2")
            {
                string tempHouseAds = hiddenHouseAdsAndLinks.Value;
                if (width == 180) tempHouseAds = hiddenHouseAdsAndLinks180.Value;
                string[] temppairs = tempHouseAds.Split(';');
                List<int> houseAds = new List<int>();
                List<string> clickUrls = new List<string>();
                for (int i = 0; i < temppairs.Length; i++)
                {
                    if (temppairs[i].Split('|')[0].Trim() != string.Empty)
                    {
                        houseAds.Add(Convert.ToInt32(temppairs[i].Split('|')[0]));
                        clickUrls.Add(temppairs[i].Split('|')[1]);
                    }
                }


                CaptchaBLL.UpdateHouseAds(captchaId, houseAds.ToArray(), clickUrls.ToArray());
            }

            // Check if Captcha type changed.
            bool isTypeChanged = _captcha.Type_Id != captchaTypeId;
            if (isTypeChanged)
                Mail.SendCaptchaTypeChangedAdminMail(publisherId, websiteId, captchaId, oldCaptchaTypeId, captchaTypeId);

            // Redirect to manage websites page.
            Response.Redirect("ManageCaptchas.aspx?WebsiteId=" + _captcha.Website_Id.ToString());
        }
        
        /// <summary>
        /// Validates that the captcha doesn't already exist.
        /// </summary>
        protected void checkCaptchaExist_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check and return if captcha already exists (by name) in the same website.
            e.IsValid = (CaptchaBLL.IsDuplicateNameForWebsite(_captcha.Publisher_Id, _captcha.Website_Id, _captcha.Captcha_Id, textCaptchaName.Text) ? false : true);
        }

        protected void checkCaptchaDims_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            int captchaTypeId = Convert.ToInt16(listCaptchaType.SelectedValue);

            if (captchaTypeId == (int)CaptchaType.PayPerType ||
                captchaTypeId == (int)CaptchaType.SecurityOnly)
            {
                // Check if empty.
                if (String.IsNullOrEmpty(listCaptchaDims.SelectedValue))
                {
                    validatorCaptchaDims.ErrorMessage = "* Required";
                    e.IsValid = false;
                }

                int width = Convert.ToInt16(listCaptchaDims.SelectedValue.Split('x').First());
                int height = Convert.ToInt16(listCaptchaDims.SelectedValue.Split('x').Last());

                // Check if in range.
                if (!CaptchaBLL.IsDimensionValid(captchaTypeId, width, height))
                {
                    validatorCaptchaDims.ErrorMessage = "* Dimension not allowed";
                    e.IsValid = false;
                }
            }

            e.IsValid = true;
        }

        protected void checkSliderDims_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            int captchaTypeId = Convert.ToInt16(listCaptchaType.SelectedValue);

            if (captchaTypeId == (int)CaptchaType.Slider ||
                captchaTypeId == (int)CaptchaType.RandomImage)
            {
                // Check if empty.
                if (String.IsNullOrEmpty(listSliderDims.SelectedValue))
                {
                    validatorSliderDims.ErrorMessage = "* Required";
                    e.IsValid = false;
                }

                int width = Convert.ToInt16(listSliderDims.SelectedValue.Split('x').First());
                int height = Convert.ToInt16(listSliderDims.SelectedValue.Split('x').Last());

                // Check if in range.
                if (!CaptchaBLL.IsDimensionValid(captchaTypeId, width, height))
                {
                    validatorSliderDims.ErrorMessage = "* Dimension not allowed";
                    e.IsValid = false;
                }
            }

            e.IsValid = true;
        }
        
        */
    }
}