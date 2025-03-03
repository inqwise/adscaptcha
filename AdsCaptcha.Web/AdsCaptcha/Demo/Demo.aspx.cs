using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace LiveDemo
{
    public partial class Demo : System.Web.UI.Page
    {        
        // Set your identifiers
        public int CaptchaId = 0;
        public string PublicKey = null;
        public string PrivateKey = null;

        private bool IsSSL = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.ToString().ToLower().StartsWith("https://"))
                IsSSL = true;

            try
            {
                int getId = 0;

                try
                {
                    getId = Convert.ToInt16(Page.Request.QueryString["id"].ToString());
                }
                catch { }

                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TP_CAPTCHA captcha = new TP_CAPTCHA();
                    captcha = dataContext.GetTable<TP_CAPTCHA>().Where(i => i.Captcha_Id == getId).SingleOrDefault();

                    if (captcha == null)
                    {
                        Response.Redirect("Default.aspx");
                    }

                    CaptchaId = getId;
                    PublicKey = captcha.TP_WEBSITE.Public_Key;
                    PrivateKey = captcha.TP_WEBSITE.Private_Key;
                }

            }
            catch { }

            if (!Page.IsPostBack)
            {
                labelAdsCaptcha.Text = AdsCaptchaAPI.CAPTCHA.GetCaptcha(CaptchaId, PublicKey, IsSSL);
            }
        }

        protected void Submit_Click1(object sender, EventArgs e)
        {
            // Get posted AdsCaptcha values
            string Challenge = Request.Form["adscaptcha_challenge_field"].ToString();
            string Response = Request.Form["adscaptcha_response_field"].ToString();
            string RemoteAddress = Request.ServerVariables["REMOTE_ADDR"];

            // Check validation
            string result = AdsCaptchaAPI.CAPTCHA.ValidateCaptcha(CaptchaId, PrivateKey, Challenge, Response, RemoteAddress, IsSSL);

            if (result == "true")
            {
                panelForm.Visible = false;
                panelCorrect.Visible = true;

                RunProcesses();
            }
            else
            {
                panelForm.Visible = true;
                labelWrong.Visible = true;
                panelCorrect.Visible = false;

                // Display new AdsCaptcha
                labelAdsCaptcha.Text = AdsCaptchaAPI.CAPTCHA.GetCaptcha(CaptchaId, PublicKey, IsSSL);
            }
        }

        private void RunProcesses()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Archive request and calculate balance;
                dataContext.Process_ArchiveCaptchaRequests();
                //dataContext.Process_CalcAdvertiserBalance();
            }
        }
    }
}
