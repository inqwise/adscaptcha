using System;
using System.Configuration;
using System.Linq;
using System.Web.Services;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.API
{
    [WebService(Namespace = "http://api.com/",
                Description = "AdsCaptcha: Stop SPAM, Make MONEY!")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class FlashService : System.Web.Services.WebService
    {
        [WebMethod]
        public AdChallenge GetAd(int CaptchaId)
        {
            AdChallenge adChallenge = new AdChallenge();

            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                var adsList = (
                    from ad in dataContext.GetTable<TA_AD>()
                    where ad.Status_Id == (int)Status.Running &&
                          ad.TA_CAMPAIGN.Status_Id == (int)Status.Running &&
                          ad.TA_ADVERTISER.Status_Id == (int)Status.Running &&
                          ad.Type_Id == (int)AdType.Slide2Fit &&
                          ad.Width <= 250 && ad.Height == ad.Width
                    select ad
                ).ToList<TA_AD>();             
                
                if (adsList.Count() == 0)
                {
                    return null;
                }
                else
                {
                    Random rnd = new Random();
                    string PATH = ConfigurationSettings.AppSettings["AWSCloudFront"];

                    int index = rnd.Next(0, adsList.Count() - 1);
                    adChallenge.ChallengeCode = General.GenerateGuid();

                    /*** Method A. Take picture from Amazon S3 (need to enable method A. or B.) ***/
                    //adChallenge.ClickUrl = PATH + adsList[index].Ad_Image;

                    /*** Method B. Copy picture to local and take it from there (need to enable A. or B.) ***/
                    System.Net.WebClient wc = new System.Net.WebClient();
                    wc.DownloadFile(PATH + adsList[index].Ad_Image, "//" + System.Environment.MachineName + "/AdsCaptcha/picture" + CaptchaId + ".jpg");
                    adChallenge.AdUrl = System.Configuration.ConfigurationSettings.AppSettings["URL"] + "picture" + CaptchaId + ".jpg";
                }
                return adChallenge;
            }
        }

        [WebMethod]
        public bool ValidateAd(int CaptchaId, string ChallengeCode, bool IsCorrect)
        {
            return true;
        }

        public class AdChallenge
        {
            public string AdUrl;
            public string ChallengeCode;
        }

    }
}