using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;
using System.IO;
using System.Xml;
using CountryDetection;

namespace Inqwise.AdsCaptcha.API
{
    public partial class Click : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                // Get challange guid.
                string cid = HttpUtility.HtmlEncode(Request.QueryString["cid"]);

                #region Get User Data

                string userSession = null;
                string IPAddress = null;
                string reffererUrl = null;
                string userAgent = null;

                try
                {
                    userSession = Session.SessionID;
                }
                catch { }

                string countryPrefix = string.Empty;
                try
                {
                    IPAddress = detectIPAdress();
                    countryPrefix = CaptchaServerBLL.Ip2country(IPAddress);                        
                }
                catch { }

                try
                {
                    reffererUrl = (Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : (Request.ServerVariables["HTTP_REFERER"] != null ? Request.ServerVariables["HTTP_REFERER"] : ""));
                }
                catch { }

                try
                {
                    userAgent = Request.ServerVariables["HTTP_USER_AGENT"];
                }
                catch { }
                
                #endregion

                // Get link URL and update user information and statistics.
                //string linkURL = CaptchaServerBLL.Click(cid, userSession, IPAddress, reffererUrl, countryPrefix);

                string linkURL = null;

                using (DAL.AdsCaptcha_RequestsEntities ent = new AdsCaptcha.DAL.AdsCaptcha_RequestsEntities())
                {
                    var request = ent.T_REQUESTS.Where(r => r.Request_Guid == cid).FirstOrDefault();
                    if (request != null)
                    {
                        linkURL = request.Link_Url;
                        if (!linkURL.ToLower().StartsWith("http"))
                            linkURL = "http://" + linkURL;

                        if (request.Is_Clicked == 0)
                        {
                            request.Is_Clicked = 1;
                            if (request.Revenue_Share_Pct == 0)
                                request.Revenue_Share_Pct = 50;
                            if (request.Cpt_Bid == null)
                                request.Cpt_Bid = (decimal)0.005;
                            request.Revenue_Share = (double)request.Cpt_Bid * (double)request.Revenue_Share_Pct / 100.0;
                            ent.SaveChanges();
                        }

                        //request.ci
                    }
                }

                if (linkURL == null)
                {
                    Response.Write("Sorry, unable to redirect.");
                    return;
                }
                else
                {
                    // Redirect to link URL.
                    Response.Redirect(linkURL, false);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Sorry, unable to redirect.");
            }
        }
        
        private string detectIPAdress()
        {
            string strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string proxyIPaddress = strIpAddress;
            if (string.IsNullOrEmpty(strIpAddress))
            {
                strIpAddress = (Request.UserHostAddress != null ? Request.UserHostAddress : (Request.ServerVariables["REMOTE_ADDR"] != null ? Request.ServerVariables["REMOTE_ADDR"] : ""));
            }            
            return CaptchaServerBLL.ChooseFirstIP(strIpAddress);
        }
    }
}