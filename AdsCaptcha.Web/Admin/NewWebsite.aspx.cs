using System;
using System.Collections.Generic;
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
    public partial class NewWebsite : System.Web.UI.Page
    {
        private TP_PUBLISHER _publisher;
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
                publisherId = int.Parse(Page.Request.QueryString["PublisherId"].ToString());

                _publisher = PublisherBLL.GetPublisher(publisherId);
            }
            catch
            {
                Response.Redirect("StartPage.aspx");
            }

            if (_publisher == null)
            {
                Response.Redirect("ManageWebsites.aspx?PublisherId=" + publisherId.ToString());
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
            {
                // Set "bread-crambs" text.
                labelBreadCrambs.Text = "<a href='ManagePublishers.aspx'>" + "Site Owners" + "</a>" + " » " +
                                        "Site Owner: " + "<a href='ManageWebsites.aspx?PublisherId=" + _publisher.Publisher_Id.ToString() + "'>" + _publisher.Email + "</a> (<a href='EditPublisher.aspx?PublisherId=" + _publisher.Publisher_Id.ToString() + "'>edit</a>)" + " » " +
                                        "New Website";
            }
            
            // Fill languages list.
            listLanguage.DataSource = DictionaryBLL.GetLanguageList();
            listLanguage.DataBind();
            listLanguage.ClearSelection();
            listLanguage.Items.Insert(0, new ListItem("-- Select --", "0"));
            listLanguage.SelectedIndex = 0;

            // Fill countries list.
            listCountry.DataSource = DictionaryBLL.GetCountryList();
            listCountry.DataBind();
            listCountry.ClearSelection();
            listCountry.Items.Insert(0, new ListItem("-- Select --", "0"));
            listCountry.SelectedIndex = 0;

            // Fill categories boxes.
            foreach (T_CATEGORY category in DictionaryBLL.GetCategoryList())
            {
                ListItem item = new ListItem(category.Category_Desc, category.Category_Id.ToString());
                checkCategory.Items.Add(item);
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

            // Set bonus.
            checkBonus.Checked = false;//true;
            checkBonus.Visible = false;
            textBonusLimit.Text = ApplicationConfiguration.DEFAULT_BONUS_LIMIT.ToString();
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
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                return;
            }

            List<int> languagesList = new List<int>();
            List<int> countriesList = new List<int>();
            List<int> categoriesList = new List<int>();
            List<string> keywordsList = new List<string>();

            // Get values.
            int publisherId = _publisher.Publisher_Id;
            string url = textUrl.Text.Trim();
            languagesList.Add(Convert.ToInt16(listLanguage.SelectedValue));
            countriesList.Add(Convert.ToInt16(listCountry.SelectedValue));

            // Build selected categories list.
            foreach (ListItem category in checkCategory.Items)
            {
                // Check if current category is selected.
                if (category.Selected)
                {
                    categoriesList.Add(int.Parse(category.Value));
                }
            }

            // Get values.
            string captchaName = textCaptchaName.Text;
            int captchaTypeId = Convert.ToInt16(listCaptchaType.SelectedValue);
            
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


            // Add website.
            INewWebsiteResult newWebsiteData;
            var websiteResult = WebsitesManager.Add(new NewWebsiteArgs
                {
                    NewCaptcha = new NewCaptchaArgs
                        {
                            Height = height,
                            Width = width,
                            Name = captchaName,
                            PublisherId = publisherId,
                            SourceType = (rblCommercial.SelectedValue == "1" ? CaptchaSourceType.Commercial : CaptchaSourceType.NonCommercial),
                        },
                    PublisherId = publisherId,
                    WebsiteUrl = url,
                    Status = Status.Running,
                    SendPublisherEmail = true,
                });

            if (websiteResult.HasError)
            {
                throw new Exception(websiteResult.Error.ToString());
            }
            

            // Redirect to manage websites page.
            
            Response.Redirect("ManageWebsites.aspx?PublisherId=" + _publisher.Publisher_Id.ToString());
        }

        /// <summary>
        /// Validates that the website does not already exist.
        /// </summary>
        protected void checkWebsiteExist_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check and return if website already exists (by url).
            e.IsValid = (WebsiteBLL.IsExist(_publisher.Publisher_Id, textUrl.Text) ? false : true);
        }

        /// <summary>
        /// Validates that the all keywords are is the right length.
        /// </summary>
        protected void checkKeywordsLength_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            List<string> keywordsList = new List<string>();

            // Build selected keywords list.
            if (textKeywords.Text != "")
            {
                keywordsList = textKeywords.Text.Split(',').ToList();
            }

            foreach (string keyword in keywordsList)
            {
                if (keyword.Trim().Length > 50)
                {
                    e.IsValid = false;
                    return;
                }
            }

            // Check and return if website already exists (by url).
            e.IsValid = true;
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