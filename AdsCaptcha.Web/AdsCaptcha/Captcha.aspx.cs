using System;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha
{
    public partial class Captcha : System.Web.UI.Page
    {
        protected string ApiUrl {
            get { return ApplicationConfiguration.ApiUrl.Value; }
        }
        
        protected string CaptchaId {
        	get {
        		string captchaId = Request.QueryString["captcha_id"]; 
        		return !string.IsNullOrEmpty(captchaId) ? captchaId : "315"; 
        	}
        }
        
        protected string PublicKey {
        	get { 
        		string publicKey = Request.QueryString["public_key"];
        		return !string.IsNullOrEmpty(publicKey) ? publicKey : "d9f2c620583e40b0a1939efb74ded9fc"; 
        	}
        }
        
        protected string PrivateKey {
        	get {
        		string privateKey = Request.QueryString["private_key"];
        		return !string.IsNullOrEmpty(privateKey) ? privateKey : "9638418d23124febb1b0499a76c6a48c"; 
        	}
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}