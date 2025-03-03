using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeSmith.Data.Linq;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.BLL
{
    public static class CaptchaBLL
    {
        #region Data Members

        private static readonly Random random = new Random();

        #endregion Data Members

        #region Public Methods

        /// <summary>
        /// Check if a captcha exists.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaId">Captcha id.</param>
        /// <returns>Returns whether captcha exists or not.</returns>
        public static bool IsExist(int publisherId, int websiteId, int capthcaId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TP_CAPTCHAs.Where(item =>
                                                      item.Publisher_Id == publisherId &&
                                                      item.Website_Id == websiteId &&
                                                      item.Captcha_Id == capthcaId).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Check if a captcha exists.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaName">Captcha Name.</param>
        /// <returns>Returns whether captcha exists or not.</returns>
        public static bool IsExist(int publisherId, int websiteId, string capthcaName)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TP_CAPTCHAs.Where(item =>
                                                      item.Publisher_Id == publisherId &&
                                                      item.Website_Id == websiteId &&
                                                      item.Captcha_Name.ToLower() == capthcaName.ToLower()).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Check if captcha name already exists in the same website.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaId">Captcha id.</param>
        /// <param name="capthcaName">Captcha name.</param>
        /// <returns>Returns whether captcha exists or not.</returns>
        public static bool IsDuplicateNameForWebsite(int publisherId, int websiteId, int captchaId, string capthcaName)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TP_CAPTCHAs.Where(item =>
                                                      item.Publisher_Id == publisherId &&
                                                      item.Website_Id == websiteId &&
                                                      item.Captcha_Id != captchaId && 
                                                      item.Captcha_Name.ToLower() == capthcaName.ToLower()).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Get captcha.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaId">Captcha id.</param>
        /// <returns>Returns a specific captcha of a publisher and website.</returns> 
        internal static TP_CAPTCHA GetCaptcha(int publisherId, int websiteId, int capthcaId)
        {
            // Check if captcha exists.
            if (IsExist(publisherId, websiteId, capthcaId) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return captcha.
                    return dataContext.TP_CAPTCHAs.SingleOrDefault(item =>
                                                                   item.Publisher_Id == publisherId &&
                                                                   item.Website_Id == websiteId &&
                                                                   item.Captcha_Id == capthcaId);
                }
            }
            else
            {
                // Captcha does not exist.
                return null;
            }
        }

        /// <summary>
        /// Get captchas list.
        /// </summary>
        /// <returns>Returns all captchas.</returns>
        public static List<TP_CAPTCHA> GetCaptchas()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return captchas list.
                return dataContext.TP_CAPTCHAs.ToList<TP_CAPTCHA>();
            }
        }
        
        /// <summary>
        /// Get captchas list.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <returns>Returns all captchas of a specific publisher.</returns>
        public static List<TP_CAPTCHA> GetCaptchas(int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return captchas list.
                return dataContext.TP_CAPTCHAs.Where(item => item.Publisher_Id == publisherId).ToList<TP_CAPTCHA>();
            }
        }

        /// <summary>
        /// Get captchas list.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <returns>Returns all captchas of a specific publisher and website.</returns>
        public static List<TP_CAPTCHA> GetCaptchas(int publisherId, int websiteId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return captchas list.
                return dataContext.TP_CAPTCHAs.Where(item =>
                                                     item.Publisher_Id == publisherId &&
                                                     item.Website_Id == websiteId).ToList<TP_CAPTCHA>();
            }
        }

        /// <summary>
        /// Adds new captcha.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="captchaId">Captcha id.</param>
        /// <param name="captchaName">Captcha name.</param>
        /// <param name="typeId">Type id.</param>
        /// <param name="securityLevelId">Security level id.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="allowPopup">Allow popup?</param>
        /// <param name="allowVideo">Allow video?</param>
        /// <param name="allowImage">Allow image?</param>
        /// <param name="allowSlogan">Allow slogan?</param>
        /// <param name="allowClick">Allow clickable?</param>
        /// <param name="themeId">Theme Id</param>
        /// <returns>New captcha id.</returns>
        [Obsolete("Use AdsCaptcha.Managers.CaptchasManager.Add", true)]
        public static int Add(int publisherId, int websiteId, string captchaName, int typeId, int securityLevelId, int width, int height, bool allowPopup, bool allowVideo, bool allowImage, bool allowSlogan, bool allowClick, int themeId, bool isCommercial)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Create captcha.
                TP_CAPTCHA captcha = new TP_CAPTCHA();
                captcha.Publisher_Id = publisherId;
                captcha.Website_Id = websiteId;
                captcha.Captcha_Name = captchaName;
                captcha.Type_Id = typeId;
                captcha.Max_Width = width;
                captcha.Max_Height = height;

                // If slider - security level is very high.
                if (typeId == (int)CaptchaType.Slider ||
                    typeId == (int)CaptchaType.SlideToFit ||
                    typeId == (int)CaptchaType.RandomImage)
                {
                    captcha.Security_Level_Id = (int)CaptchaSecurityLevel.VeryHigh;
                }
                else
                {
                    captcha.Security_Level_Id = securityLevelId;
                }

                // If security - not allow ad content.
                if (typeId == (int)CaptchaType.SecurityOnly ||
                    typeId == (int)CaptchaType.RandomImage)
                {
                    captcha.Allow_Popup = 0;
                    captcha.Allow_Video = 0;
                    captcha.Allow_Image = 0;
                    captcha.Allow_Slogan = 0;
                    captcha.Allow_Click = 0;
                }
                else
                {
                    captcha.Allow_Popup = (allowPopup ? 1 : 0);
                    captcha.Allow_Video = (allowVideo ? 1 : 0);
                    captcha.Allow_Image = (allowImage ? 1 : 0);
                    captcha.Allow_Slogan = (allowSlogan ? 1 : 0);
                    captcha.Allow_Click = (allowClick ? 1 : 0);
                }

                captcha.Status_Id = WebsiteBLL.GetWebsite(publisherId, websiteId).Status_Id; // Set status according to website
                captcha.Last_Status_Id = (int)Status.Running;                      // Set previous status to Running
                captcha.Theme_Id = themeId;
                captcha.Add_Date = DateTime.Today;
                captcha.Modify_Date = DateTime.Today;
                //captcha.IsCommercial = isCommercial;

                // Add captcha.
                dataContext.TP_CAPTCHAs.InsertOnSubmit(captcha);

                // Save changes.
                dataContext.SubmitChanges();

                // Return new captcha id.
                return captcha.Captcha_Id;
            }
        }

        [Obsolete("Not in use", true)]
        public static void AddHouseAds(int captchaId, int[]adsImgIds, string[]clickUrls)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                var captcha = dataContext.TP_CAPTCHAs.Where(c => c.Captcha_Id == captchaId).FirstOrDefault();
                if (captcha != null)
                {
                    //captcha.IsHouseAds = true;

                    for (int i = 0; i < adsImgIds.Length; i++)
                    {
                        TP_CAPTCHA_HOUSEAD h = new TP_CAPTCHA_HOUSEAD();

                        h.Captcha_Id = captchaId;
                        h.DeformedImageID = adsImgIds[i];
                        h.ClickUrl = clickUrls[i];
                        h.InsertDate = DateTime.Now;
                        h.IsDeleted = false;
                        h.Served = 0;

                        dataContext.TP_CAPTCHA_HOUSEADs.InsertOnSubmit(h);
                    }

                        dataContext.SubmitChanges();
                }
            }
        }

        [Obsolete("Not in use", true)]
        public static void UpdateHouseAds(int captchaId, int[] adsImgIds, string[] clickUrls)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                var captcha = dataContext.TP_CAPTCHAs.Where(c => c.Captcha_Id == captchaId).FirstOrDefault();
                if (captcha != null)
                {
                    //captcha.IsHouseAds = true;
                    var hads = dataContext.TP_CAPTCHA_HOUSEADs.Where(c => c.Captcha_Id == captchaId && !c.IsDeleted).ToList();

                    foreach (var had in hads)
                    {
                        if (!adsImgIds.Contains(had.DeformedImageID))
                        {
                            had.IsDeleted = true;
                        }
                    }
                    for (int i = 0; i < adsImgIds.Length; i++)
                    {
                        var had = hads.Where(h => h.DeformedImageID == adsImgIds[i]).FirstOrDefault();
                        if (had != null)
                        {
                            had.ClickUrl = clickUrls[i];
                        }
                        else
                        {
                            TP_CAPTCHA_HOUSEAD h = new TP_CAPTCHA_HOUSEAD();

                            h.Captcha_Id = captchaId;
                            h.DeformedImageID = adsImgIds[i];
                            h.ClickUrl = clickUrls[i];
                            h.InsertDate = DateTime.Now;
                            h.IsDeleted = false;
                            h.Served = 0;

                            dataContext.TP_CAPTCHA_HOUSEADs.InsertOnSubmit(h);
                        }
                    }

                    dataContext.SubmitChanges();
                }
            }
        }

        public static List<TP_CAPTCHA_HOUSEAD> GetHouseAds(int captchaId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_CAPTCHA_HOUSEADs.Where(h => h.Captcha_Id == captchaId && !h.IsDeleted).ToList();
            }
        }

        /// <summary>
        /// Set captcha status to running.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaId">Caotcha id.</param>
        public static void Activate(AdsCaptchaDataContext dataContext, int publisherId, int websiteId, int capthcaId)
        {
            // Change captcha status to running.
            ChangeStatus(dataContext, publisherId, websiteId, capthcaId, (int)Status.Running);
        }

        /// <summary>
        /// Set captcha status to paused.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaId">Caotcha id.</param>
        public static void Pause(AdsCaptchaDataContext dataContext, int publisherId, int websiteId, int capthcaId)
        {
            // Change captcha status to paused.
            ChangeStatus(dataContext, publisherId, websiteId, capthcaId, (int)Status.Paused);
        }

        /// <summary>
        /// Set captcha status to pending.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaId">Caotcha id.</param>
        public static void Pending(AdsCaptchaDataContext dataContext, int publisherId, int websiteId, int capthcaId)
        {
            // Change captcha status to pending.
            ChangeStatus(dataContext, publisherId, websiteId, capthcaId, (int)Status.Pending);
        }

        /// <summary>
        /// Set captcha status to rejected.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaId">Caotcha id.</param>
        public static void Reject(AdsCaptchaDataContext dataContext, int publisherId, int websiteId, int capthcaId)
        {
            // Change captcha status to rejected.
            ChangeStatus(dataContext, publisherId, websiteId, capthcaId, (int)Status.Rejected);
        }

        /*
        /// <summary>
        /// Set captcha status to running.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaId">Caotcha id.</param>
        public static void Activate(int publisherId, int websiteId, int capthcaId)
        {
            // Change captcha status to running.
            ChangeStatus(publisherId, websiteId, capthcaId, (int)Status.Running);
        }

        /// <summary>
        /// Set captcha status to paused.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaId">Caotcha id.</param>
        public static void Pause(int publisherId, int websiteId, int capthcaId)
        {
            // Change captcha status to paused.
            ChangeStatus(publisherId, websiteId, capthcaId, (int)Status.Paused);
        }

        /// <summary>
        /// Set captcha status to pending.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaId">Caotcha id.</param>
        public static void Pending(int publisherId, int websiteId, int capthcaId)
        {
            // Change captcha status to pending.
            ChangeStatus(publisherId, websiteId, capthcaId, (int)Status.Pending);
        }

        /// <summary>
        /// Set captcha status to rejected.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaId">Caotcha id.</param>
        public static void Reject(int publisherId, int websiteId, int capthcaId)
        {
            // Change captcha status to rejected.
            ChangeStatus(publisherId, websiteId, capthcaId, (int)Status.Rejected);
        }
        */

        /// <summary>
        /// Update captcha.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="captchaId">Captcha id.</param>
        /// <param name="captchaName">Captcha name.</param>
        /// <param name="statusId">Status id.</param>
        /// <param name="typeId">Type id.</param>
        /// <param name="securityLevelId">Security level id.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="allowPopup">Allow popup?</param>
        /// <param name="allowVideo">Allow video?</param>
        /// <param name="allowImage">Allow image?</param>
        /// <param name="allowSlogan">Allow slogan?</param>
        /// <param name="allowClick">Allow clickable?</param>
        /// <param name="themeId">Theme id</param>
        [Obsolete("Use AdsCaptcha.Managers.CaptchasManager.Update", true)]
        public static void Update(int publisherId, int websiteId, int captchaId, string captchaName, int statusId, int typeId, int securityLevelId, int width, int height, bool allowPopup, bool allowVideo, bool allowImage, bool allowSlogan, bool allowClick, int themeId, bool isCommercial)
        {
            // Check if captcha exists.
            if (IsExist(publisherId, websiteId, captchaId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TP_CAPTCHA captcha = new TP_CAPTCHA();

                    // Get captcha data.
                    captcha = dataContext.TP_CAPTCHAs.SingleOrDefault(item =>
                                                                      item.Publisher_Id == publisherId &&
                                                                      item.Website_Id == websiteId &&
                                                                      item.Captcha_Id == captchaId);

                    // Update captcha data.
                    captcha.Captcha_Name = captchaName;
                    captcha.Status_Id = statusId;
                    captcha.Type_Id = typeId;
                    captcha.Max_Width = width;
                    captcha.Max_Height = height;
                    captcha.Theme_Id = themeId;
                    captcha.Modify_Date = DateTime.Today;
                    captcha.SourceTypeId = (short)(isCommercial ? 1 : 0);

                    // If slider - security level is very high.
                    if (typeId == (int)CaptchaType.Slider ||
                        typeId == (int)CaptchaType.SlideToFit ||
                        typeId == (int)CaptchaType.RandomImage)
                    {
                        captcha.Security_Level_Id = (int)CaptchaSecurityLevel.VeryHigh;
                    }
                    else
                    {
                        captcha.Security_Level_Id = securityLevelId;
                    }

                    // If security - not allow ad content.
                    if (typeId == (int)CaptchaType.SecurityOnly ||
                        typeId == (int)CaptchaType.RandomImage)
                    {
                        captcha.Allow_Popup = 0;
                        captcha.Allow_Video = 0;
                        captcha.Allow_Image = 0;
                        captcha.Allow_Slogan = 0;
                        captcha.Allow_Click = 0;
                    }
                    else
                    {
                        captcha.Allow_Popup = (allowPopup ? 1 : 0);
                        captcha.Allow_Video = (allowVideo ? 1 : 0);
                        captcha.Allow_Image = (allowImage ? 1 : 0);
                        captcha.Allow_Slogan = (allowSlogan ? 1 : 0);
                        captcha.Allow_Click = (allowClick ? 1 : 0);
                    }                    
                    // Change website status.
                    ChangeStatus(publisherId, websiteId, captchaId, statusId);

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
            else
            {
                // TODO: Handle website not exsists
            }
        }

        /// <summary>
        /// Change specific captcha's status.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaId">Captcha id.</param>
        /// <param name="statusId">New status.</param>
        public static void ChangeStatus(AdsCaptchaDataContext dataContext, int publisherId, int websiteId, int captchaId, int statusId)
        {
            // Get captcha.
            TP_CAPTCHA captcha = new TP_CAPTCHA();
            captcha = GetCaptcha(publisherId, websiteId, captchaId);

            // Check if captcha exists.
            if (captcha == null)
            {
                // TODO: Handle captcha not exsists
            }
            else
            {
                // Check if status changed.
                if (captcha.Last_Status_Id != statusId)
                {
                    if (captcha.Status_Id != (int)Status.Pending &&
                        captcha.Status_Id != (int)Status.Rejected)
                    {
                        // Set previous status.
                        captcha.Last_Status_Id = captcha.Status_Id;
                    }

                    // Change status.
                    captcha.Status_Id = statusId;
                    captcha.Modify_Date = DateTime.Today;
                }
            }
        }

        /// <summary>
        /// Change specific captcha's status.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaId">Captcha id.</param>
        /// <param name="statusId">New status.</param>
        public static void ChangeStatus(int publisherId, int websiteId, int captchaId, int statusId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Change statsus.
                ChangeStatus(dataContext, publisherId, websiteId, captchaId, statusId);

                // Save changes.
                dataContext.SubmitChanges();
            }
        }

        /// <summary>
        /// Check if dimensions are valid for selected Captcha type.
        /// </summary>
        /// <param name="captchaType">Captcha type.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <returns>Wheather dimensions are valid or not.</returns>
        public static bool IsDimensionValid(int captchaType, int width, int height)
        {
            int isPPT = (captchaType == (int)CaptchaType.PayPerType ? 1 : 0);
            int isSlider = (captchaType == (int)CaptchaType.Slider || captchaType == (int)CaptchaType.SlideToFit || captchaType == (int)CaptchaType.RandomImage ? 1 : 0);

            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.T_AD_DIMENSIONs.Where(d => d.Width == width &&
                                                               d.Height == height &&
                                                               ((isPPT == 1 && d.Is_Image == isPPT) || (isSlider == 1 && d.Is_Slider == isSlider) || (isPPT == 1 && d.Is_Video == isPPT))
                                                         ).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Get random images list.
        /// </summary>
        /// <returns>Returns all random images.</returns>
        public static List<TP_RANDOM_IMAGE> GetRandomImages()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return random images list.
                return dataContext.TP_RANDOM_IMAGEs.ToList<TP_RANDOM_IMAGE>();
            }
        }

        /// <summary>
        /// Add random image.
        /// </summary>
        public static void AddRandomImage(string image, int width, int height)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                TP_RANDOM_IMAGE randomImage = new TP_RANDOM_IMAGE();

                // Set values.
                randomImage.Width = width;
                randomImage.Height = height;
                randomImage.Image = image;
                randomImage.Status_Id = (int)Status.Running;

                // Insert.
                dataContext.TP_RANDOM_IMAGEs.InsertOnSubmit(randomImage);
                
                // Submit changes.
                dataContext.SubmitChanges();
            }
        }

        /// <summary>
        /// Get random image.
        /// </summary>
        public static string GetRandomImage(int width, int height, ref bool isDefaultResize)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                var images = from i in dataContext.TP_RANDOM_IMAGEs
                             where i.Status_Id == (int)Status.Running
                             where i.Width == width
                             where i.Height == height
                             select i.Image;

                if (images.Count() == 0)
                {
                    isDefaultResize = true;

                    images = from i in dataContext.TP_RANDOM_IMAGEs
                             where i.Status_Id == (int)Status.Running
                             where i.Width == 300
                             where i.Height == 300
                             select i.Image;
                }


                // Get random images list.
                List<string> listImages = images.FromCache(600).ToList<string>();

               
                // Get number of available random images.
                int numOfImages = listImages.Count();
                
                // Randomize image.
                int index = random.Next(0, numOfImages);

                // Return random image file name.
                return listImages[index];
            }
        }

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods
    }
}
