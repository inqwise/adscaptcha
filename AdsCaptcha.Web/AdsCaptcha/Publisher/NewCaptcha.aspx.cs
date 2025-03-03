using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class NewCaptcha : System.Web.UI.Page, IPublisherNewCaptcha
    {
        protected IWebsite Website { get; private set; }

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
                int publisherId = Convert.ToInt16(Session["PublisherId"]);
                int websiteId = Convert.ToInt16(Page.Request.QueryString["WebsiteId"]);

                var websiteResult = WebsitesManager.Get(websiteId, publisherId);

                if (websiteResult.HasError)
                {
                    // TODO: Handle website not exists
                    throw new Exception("Website not exists");
                }
                else
                {
                    Website = websiteResult.Value;
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
            // Set navigation path.
            labelNavigationPath.Text = "<a href=\"ManageWebsites.aspx\">Websites</a>" + " &gt; " +
                                       "Website: " + "<a href=\"ManageCaptchas.aspx?WebsiteId=" + _website.Website_Id.ToString() + "\">" + _website.Url + "</a>" + " &gt; " +
                                       "New Captcha";

            // Fill captcha type list.
            listCaptchaType.DataSource = DictionaryBLL.GetCaptchaTypeList();
            listCaptchaType.DataBind();
            
            // Set captcha type default value.
            listCaptchaType.ClearSelection();
            int defaultCaptchaType = (int)CaptchaType.SlideToFit;
            listCaptchaType.Items.FindByValue(defaultCaptchaType.ToString()).Selected = true;

            listCaptchaType.Attributes.Add("onChange", "javascript:ChangeCaptchaType();");
            listThemes.Attributes.Add("onChange", "javascript:OnThemeChange();");
            checkAllowVideo.Attributes.Add("onClick", "javascript:toggleAllowContent();");
            checkAllowImage.Attributes.Add("onClick", "javascript:toggleAllowContent();");
            checkAllowSlogan.Attributes.Add("onClick", "javascript:toggleAllowContent();");
            radioAllowClick.Attributes.Add("onClick", "javascript:toggleAllowContent();");
            rblCommercial.Attributes.Add("onClick", "javascript:ChangeCaptchaType();");

            // Set captcha type restrictions.
            checkAllowPopup.Checked = false;
            checkAllowVideo.Checked = false;
            checkAllowImage.Checked = true;
            checkAllowSlogan.Checked = true;
            radioAllowClick.SelectedIndex = 0;

            // Fill captcha dims list.
            listCaptchaDims.DataSource = DictionaryBLL.GetImageDimsList();
            listCaptchaDims.DataTextField = "Name";
            listCaptchaDims.DataValueField = "Name";
            listCaptchaDims.DataBind();
            listCaptchaDims.ClearSelection();
            listCaptchaDims.Items.FindByValue("300x250").Selected = true;

            // Fill slider dims list.
            listSliderDims.DataSource = DictionaryBLL.GetSliderDimsList();
            listSliderDims.DataTextField = "Name";
            listSliderDims.DataValueField = "Name";
            listSliderDims.DataBind();
            listSliderDims.ClearSelection();
            listSliderDims.Items.FindByValue("300x250").Selected = true;

            // Fill themes list.
            listThemes.DataSource = DictionaryBLL.GetThemeList();
            listThemes.DataTextField = "Theme_Name";
            listThemes.DataValueField = "Theme_Id";
            listThemes.DataBind();
            listThemes.ClearSelection();
            listThemes.SelectedIndex = 0;
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
            tableSliderDims.Style.Add("display", "block");

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
            int publisherId = _website.Publisher_Id;
            int websiteId = _website.Website_Id;
            string captchaName = textCaptchaName.Text.Trim();
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
            int securityLevelId;
            if (captchaTypeId == (int)CaptchaType.SecurityOnly)
                securityLevelId = (int)SecurityLevel.Medium;
            else
                securityLevelId = (int)SecurityLevel.VeryLow;
           
            // Create new captcha.
            int captchaId = CaptchaBLL.Add(publisherId,
                                           websiteId,
                                           captchaName,
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
                        houseAds.Add( Convert.ToInt32(temppairs[i].Split('|')[0]));
                        clickUrls.Add(temppairs[i].Split('|')[1]);
                    }
                }


                CaptchaBLL.AddHouseAds(captchaId, houseAds.ToArray(), clickUrls.ToArray());
            }

            string publisherEmail = PublisherBLL.GetPublisher(publisherId).Email;
            string websiteUrl = WebsiteBLL.GetWebsite(publisherId, websiteId).Url;

            // Send mail to administrator.
            Mail.SendNewCaptchaAdminMail(publisherId, publisherEmail, websiteId, websiteUrl, captchaId, captchaTypeId);

            // Redirect to get code page.
            Response.Redirect("GetCode.aspx?WebsiteId=" + _website.Website_Id.ToString() + "&CaptchaId=" + captchaId.ToString());
           
        }

        #region Validation

        protected void checkCaptchaExist_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check and return if captcha already exists (by name).
            e.IsValid = (CaptchaBLL.IsExist(_website.Publisher_Id, _website.Website_Id, textCaptchaName.Text) ? false : true);
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

        #endregion
        
        */
    }
}