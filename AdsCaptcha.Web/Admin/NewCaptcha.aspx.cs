using System;
using System.Linq;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class NewCaptcha : System.Web.UI.Page
    {
        private TP_WEBSITE _website;
        private int publisherId;
        private int websiteId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null)
                Response.Redirect("Login.aspx");

            try
            {
                publisherId = int.Parse(Page.Request.QueryString["PublisherId"].ToString());
                websiteId = Convert.ToInt16(Page.Request.QueryString["WebsiteId"].ToString());

                _website = WebsiteBLL.GetWebsite(publisherId, websiteId);
            }
            catch
            {
                Response.Redirect("StartPage.aspx");
            }

            if (_website == null)
            {
                Response.Redirect("ManageWebsites.aspx?PublisherId=" + _website.Publisher_Id.ToString());
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
            string publisher = PublisherBLL.GetPublisher(_website.Publisher_Id).Email;

            {
                // Set "bread-crambs" text.
                labelBreadCrambs.Text = "<a href='ManagePublishers.aspx'>" + "Site Owners" + "</a>" + " » " +
                                        "Site Owner: " + "<a href='ManageWebsites.aspx?PublisherId=" + _website.Publisher_Id.ToString() + "'>" + publisher + "</a> (<a href='EditPublisher.aspx?PublisherId=" + _website.Publisher_Id.ToString() + "'>edit</a>)" + " » " +
                                        "Website: " + "<a href='ManageCaptchas.aspx?PublisherId=" + _website.Publisher_Id.ToString() + "&WebsiteId=" + _website.Website_Id.ToString() + "'>" + _website.Url + "</a> (<a href='EditWebsite.aspx?PublisherId=" + _website.Publisher_Id.ToString() + "&WebsiteId=" + _website.Website_Id.ToString() + "'>edit</a>)" + " » " +
                                        "New Captcha";
            }
            
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
            rblCommercial.Attributes.Add("onChange", "javascript:ChangeCaptchaType();");

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
            //listCaptchaDims.Items.FindByValue("200x200").Selected = true;

            // Fill slider dims list.
            listSliderDims.DataSource = DictionaryBLL.GetSliderDimsList();
            listSliderDims.DataTextField = "Name";
            listSliderDims.DataValueField = "Name";
            listSliderDims.DataBind();
            listSliderDims.ClearSelection();
            //listSliderDims.Items.FindByValue("200x200").Selected = true;

            // Set captcha security level default value.
            int defaultSecurityLevel = 1;
            sliderSecurityLevel.Value = defaultSecurityLevel;

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
            tableSliderDims.Style.Add("display", holderSliderDims);

            // Set displayed preview.
            CaptchaTypePayPerTypeHolder.Style.Add("display", holderPayPerType);
            CaptchaTypeSecurityOnlyHolder.Style.Add("display", holderSecurityOnly);
            CaptchaTypeSliderHolder.Style.Add("display", holderSlider);
            CaptchaTypeSecuritySliderHolder.Style.Add("display", holderSecuritySlider);
            CaptchaTypeHolder0.Style.Add("display", displaySecurity);
            CaptchaTypeHolder1.Style.Add("display", displaySlogan);
            CaptchaTypeHolder2.Style.Add("display", displayImage);
            CaptchaTypeHolder3.Style.Add("display", displayImageClickable);
            CaptchaTypeHolder4.Style.Add("display", displayVideo);
            CaptchaTypeHolder5.Style.Add("display", displaySlider);
            CaptchaTypeHolder6.Style.Add("display", displaySecuritySlider);
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit
            if (!Page.IsValid)
            {
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
            int securityLevelId = Convert.ToInt16(sliderSecurityLevel.Value);
            securityLevelId = (securityLevelId < 1) ? 1 : (securityLevelId > 5) ? 5 : securityLevelId;
            securityLevelId += (int)DecodeTables.SecurityLevel * 1000;

            // Create new captcha.
            var captchaResult =
                CaptchasManager.Add(new NewCaptchaArgs
                    {
                        Height = height,
                        Name = captchaName,
                        PublisherId = publisherId,
                        SourceType =
                            (rblCommercial.SelectedValue == "1"
                                 ? CaptchaSourceType.Commercial
                                 : CaptchaSourceType.NonCommercial),
                        WebsiteId = websiteId,
                        Width = width
                    });

            if (captchaResult.HasError)
            {
                throw new Exception(captchaResult.Error.ToString());
            }

            // Redirect to manage websites page.
           
            {
                Response.Redirect("ManageCaptchas.aspx?PublisherId=" + _website.Publisher_Id.ToString() + "&WebsiteId=" + _website.Website_Id.ToString());
            }
        }
        
        /// <summary>
        /// Validates that the captcha doesn't already exist.
        /// </summary>
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
    }
}