using System;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class SignUp2 : System.Web.UI.Page, IPublisherSignUp2
    {
        private TP_PUBLISHER _publisher;
        
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

                _publisher = PublisherBLL.GetPublisher(publisherId);

                if (_publisher == null)
                {
                    // TODO: Handle publisher not exists
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
                Metadata.SetMetadata(Metadata.Pages.Publisher, this.Page.Master.Page.Header);

                FillControls();
            }

            //ShowHide();
        }

        private void FillControls()
        {
            rpLanguages.DataSource = DictionaryBLL.GetLanguageList();
            rpLanguages.DataBind();

            rpCategories.DataSource = DictionaryBLL.GetCategoryList();
            rpCategories.DataBind();
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
                                       "New Website";

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
            // rblCommercial.Attributes.Add("onChange", "javascript:ChangeCaptchaType();");


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
            listSliderDims.SelectedIndex = 1;
            //listSliderDims.Items.FindByValue("200x200").Selected = true;

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

            if (captchaType == (int)CaptchaType.SecurityOnly)
            {
                holderSecurityOnly = "block";
                displaySecurity = "block";
                holderCaptchaDims = "block";
            }
            else if (captchaType == (int)CaptchaType.PayPerType)
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
            else if (captchaType == (int)CaptchaType.Slider)
            {
                holderSlider = "block";
                holderSliderDims = "block";
                displaySlider = "block";
            }
            else if (captchaType == (int)CaptchaType.RandomImage)
            {
                holderSecuritySlider = "block";
                holderSliderDims = "block";
                displaySlider = "block";
            }

            // Set clickable options display.
            bool showClick = (captchaType == (int)CaptchaType.PayPerType || captchaType == (int)CaptchaType.Slider);
            AllowClickOptions.Style.Add("display", showClick ? "block" : "none");

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
                if (!validatorUrl1.IsValid || !validatorUrl2.IsValid)
                    textUrl.Focus();
                else if (!validatorLanguage.IsValid)
                    listLanguage.Focus();
                //else if (!validatorRegion.IsValid)
                //    listCountry.Focus();
                else if (!validatorKeywords.IsValid)
                    textKeywords.Focus();
                else if (!validatorCaptchaName.IsValid)
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

            List<int> languagesList = new List<int>();
            List<int> countriesList = new List<int>();
            List<int> categoriesList = new List<int>();
            List<string> keywordsList = new List<string>();

            // Get values.
            int publisherId = Publisher.Publisher_Id;
            string url = textUrl.Text.Trim();
            bool allowBonus = false;
            int bonusLimit = ApplicationConfiguration.DEFAULT_BONUS_LIMIT;
            int languageId = Convert.ToInt16(listLanguage.SelectedValue);
            int regionId = Convert.ToInt16(listCountry.SelectedValue);
            languagesList.Add(Convert.ToInt16(listLanguage.SelectedValue));
            //countriesList.Add(Convert.ToInt16(listCountry.SelectedValue));

            // Build selected categories list.
            foreach (ListItem category in checkCategory.Items)
            {
                // Check if current category is selected.
                if (category.Selected)
                {
                    categoriesList.Add(int.Parse(category.Value));
                }
            }

            // Build selected keywords list.
            if (textKeywords.Text != "")
            {
                keywordsList = textKeywords.Text.Split(',').ToList();
            }

            // Add website.
            int websiteId = WebsiteBLL.Add(publisherId,
                                           url,
                                           languagesList,
                                           countriesList,
                                           categoriesList,
                                           keywordsList,
                                           allowBonus,
                                           bonusLimit);

            TP_WEBSITE website = WebsiteBLL.GetWebsite(Publisher.Publisher_Id, websiteId);

            // Send notifier to administrator.
            Mail.SendNewWebsiteAdminMail(Publisher.Publisher_Id, Publisher.Email, websiteId, website.Url);

            // Get values.
            string captchaName = textCaptchaName.Text;
            int captchaTypeId = Convert.ToInt16(listCaptchaType.SelectedValue);
            bool allowPopup = checkAllowPopup.Checked;
            bool allowVideo = checkAllowVideo.Checked;
            bool allowClick = radioAllowClick.Items[0].Selected;
            bool allowImage = checkAllowImage.Checked;
            bool allowSlogan = checkAllowSlogan.Checked;
            int themeId = Convert.ToInt16(listThemes.SelectedValue);

            // Get dimensions.
            int width = 0;
            int height = 0;
            switch (captchaTypeId)
            {
                case (int)CaptchaType.Slider:
                case (int)CaptchaType.SlideToFit:
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
                        houseAds.Add(Convert.ToInt32(temppairs[i].Split('|')[0]));
                        clickUrls.Add(temppairs[i].Split('|')[1]);
                    }
                }


                CaptchaBLL.AddHouseAds(captchaId, houseAds.ToArray(), clickUrls.ToArray());
            }

            string publisherEmail = PublisherBLL.GetPublisher(publisherId).Email;

            // Send mail to administrator.
            Mail.SendNewCaptchaAdminMail(publisherId, publisherEmail, websiteId, url, captchaId, captchaTypeId);

            // Send mail to publisher.
            if (captchaTypeId == (int)CaptchaType.SecurityOnly)
            {
                Mail.SendNewWebsiteSecurityOnlyMail(Publisher.Email, url);
            }
            else
            {
                Mail.SendNewWebsiteMail(Publisher.Email, url);
            }

            // Redirect to get code page.
            Response.Redirect("GetCode.aspx?WebsiteId=" + websiteId.ToString() + "&CaptchaId=" + captchaId.ToString());
        }

        /// <summary>
        /// Validates that the website does not already exist.
        /// </summary>
        protected void checkWebsiteExist_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check and return if website already exists (by url).
            e.IsValid = (WebsiteBLL.IsExist(Publisher.Publisher_Id, textUrl.Text) ? false : true);
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
        */
    }
}