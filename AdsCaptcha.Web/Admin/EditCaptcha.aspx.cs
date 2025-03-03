using System;
using System.Linq;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Managers;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class EditCaptcha : System.Web.UI.Page
    {
        private ICaptcha _captcha;
        private int publisherId;
        private int websiteId;
        private int captchaId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                publisherId = Convert.ToInt16(Page.Request.QueryString["PublisherId"]);
                websiteId = Convert.ToInt16(Page.Request.QueryString["WebsiteId"]);
                captchaId = Convert.ToInt16(Page.Request.QueryString["CaptchaId"]);

                _captcha = CaptchasManager.Get(captchaId).Value;
            }
            catch
            {
                Response.Redirect("StartPage.aspx");
            }

            if (_captcha == null)
            {
                Response.Redirect("ManageCaptchas.aspx?PublisherId=" + _captcha.PublisherId + "&WebsiteId=" + _captcha.WebsiteId);
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
            string publisher = PublishersManager.Get(_captcha.PublisherId).Value.Email;
            IWebsite website = WebsitesManager.Get(_captcha.WebsiteId).Value;

            {
                // Set "bread-crambs" text.
                labelBreadCrambs.Text = "<a href='ManagePublishers.aspx'>" + "Site Owners" + "</a>" + " » " +
                                        "Site Owner: " + "<a href='ManageWebsites.aspx?PublisherId=" + _captcha.PublisherId + "'>" + publisher + "</a> (<a href='EditPublisher.aspx?PublisherId=" + _captcha.PublisherId + "'>edit</a>)" + " » " +
                                        "Website: " + "<a href='ManageCaptchas.aspx?PublisherId=" + _captcha.PublisherId + "&WebsiteId=" + _captcha.WebsiteId + "'>" + website.Url + "</a> (<a href='EditWebsite.aspx?PublisherId=" + _captcha.PublisherId + "&WebsiteId=" + _captcha.WebsiteId + "'>edit</a>)" + " » " +
                                        "Captcha: " + _captcha.Name + " » " +
                                        "Edit";
            }
            
            // Set IDs.
            labelCaptchaId.Text = _captcha.Id.ToString();
            labelPublicKey.Text = website.PublicKey;
            labelPrivateKey.Text = website.PrivateKey;

            // Fill status list.
            listStatus.DataSource = DictionaryBLL.GetStatusList();
            listStatus.DataBind();
            listStatus.ClearSelection();
            
            // Fill captcha type list.
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

            
            if (listSliderDims.Items.FindByValue(_captcha.MaxWidth.ToString() + "x" + _captcha.MaxHeight.ToString()) != null)
            {
                listSliderDims.SelectedValue = listSliderDims.Items.FindByValue(_captcha.MaxWidth.ToString() + "x" + _captcha.MaxHeight.ToString()).Value;
            }
            
            int statusId = (int)_captcha.Status;
            
            // Set values from db.
            textCaptchaName.Text = _captcha.Name;
            listStatus.Items.FindByValue(statusId.ToString()).Selected = true;
            rblCommercial.SelectedIndex = 1;
            sliderSecurityLevel.Value = 1;
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

            holderSlider = "block";
            holderSliderDims = "block";
            displaySlider = "block";
            
            // Set clickable options display.
            
            // Set dimensions display.
            tableCaptchaDims.Style.Add("display", holderCaptchaDims);
            tableSliderDims.Style.Add("display", holderSliderDims);

            // Set displayed preview.
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
            long captchaId = _captcha.Id;
            string captchaName = textCaptchaName.Text.Trim();
            int statusId = Convert.ToInt16(listStatus.SelectedValue);
            
            // Get dimensions.
            int width = Convert.ToInt16(listSliderDims.SelectedValue.Split('x').First());
            int height = Convert.ToInt16(listSliderDims.SelectedValue.Split('x').Last());
            
            // Update captcha.
            CaptchasManager.Update(new UpdateCaptchaArgs(captchaId)
                {
                    Height = height,
                    Name = captchaName,
                    Width = width,
                    SourceType = (rblCommercial.SelectedValue == "1" ? CaptchaSourceType.Commercial : CaptchaSourceType.NonCommercial),
                    Status = (Status)statusId,
                });

            // Redirect to manage websites page.
            Response.Redirect("ManageCaptchas.aspx?PublisherId=" + _captcha.PublisherId.ToString() + "&WebsiteId=" + _captcha.WebsiteId.ToString());
            
        }
        
        /// <summary>
        /// Validates that the captcha doesn't already exist.
        /// </summary>
        protected void checkCaptchaExist_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check and return if captcha already exists (by name) in the same website.
            e.IsValid = (CaptchaBLL.IsDuplicateNameForWebsite(_captcha.PublisherId, _captcha.WebsiteId, Convert.ToInt32(_captcha.Id), textCaptchaName.Text) ? false : true);
        }

        protected void checkCaptchaDims_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            e.IsValid = true;
        }

        protected void checkSliderDims_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            e.IsValid = true;
        }
    }
}
