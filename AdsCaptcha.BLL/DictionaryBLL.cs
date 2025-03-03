using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inqwise.AdsCaptcha.Cache;
using Inqwise.AdsCaptcha.DAL;

namespace Inqwise.AdsCaptcha.BLL
{
    public enum DecodeTables
    {
        Status = 10,
        SecurityLevel = 11,
        PaymentMethod = 12,
        CaptchaType = 13,
        CaptchaTypeOptions = 15,
        AdType = 16,
        TransactionType = 17,
        CrmSubject = 18,
        CrmStatus = 19,
        CreditCard = 20,
        BillingMethod = 21,
        ErrorType = 22,
        CreditMethod = 23
    }

    public static class DictionaryBLL
    {
        #region Public Methods

        public static string GetNameById(int itemId)
        {
            return ((DecodeCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Decode)).GetDecode(itemId).Item_Desc;
        }

        public static List<T_DECODE> GetStatusList()
        {
            int tableId = (int)DecodeTables.Status;
            return GetDecodeList(tableId);
        }

        public static List<T_DECODE> GetCaptchaTypeList()
        {
            int tableId = (int)DecodeTables.CaptchaType;
            return GetDecodeList(tableId);
        }

        public static List<T_DECODE> GetAdTypeList()
        {
            int tableId = (int)DecodeTables.AdType;
            return GetDecodeList(tableId);
            //return ((DecodeCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Decode)).GetDecodeGroup(tableId).Where(i => i.Is_Deleted == 0).ToList<T_DECODE>();
        }

        public static List<T_DECODE> GetAdTypeListForAdmin()
        {
            int tableId = (int)DecodeTables.AdType;
            return ((DecodeCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Decode)).GetDecodeGroup(tableId).ToList<T_DECODE>(); ;
            //return 
        }

        public static List<T_DECODE> GetSecurityLevelList()
        {
            int tableId = (int)DecodeTables.SecurityLevel;
            return GetDecodeList(tableId);
        }

        public static List<T_DECODE> GetTransactionTypeList()
        {
            int tableId = (int)DecodeTables.TransactionType;
            return GetDecodeList(tableId);
        }

        public static List<T_DECODE> GetCrmSubjectList()
        {
            int tableId = (int)DecodeTables.CrmSubject;
            return GetDecodeList(tableId);
        }

        public static List<T_DECODE> GetCrmStatusList()
        {
            int tableId = (int)DecodeTables.CrmStatus;
            return GetDecodeList(tableId);
        }

        public static List<T_DECODE> GetCreditCardList()
        {
            int tableId = (int)DecodeTables.CreditCard;
            return GetDecodeList(tableId);
        }

        public static List<T_DECODE> GetPaymentMethodList()
        {
            int tableId = (int)DecodeTables.PaymentMethod;
            return GetDecodeList(tableId);
        }

        public static List<T_DECODE> GetCreditMethodList()
        {
            int tableId = (int)DecodeTables.CreditMethod;
            return GetDecodeList(tableId);
        }

        public static List<T_DECODE> GetBillingMethodList()
        {
            int tableId = (int)DecodeTables.BillingMethod;
            return GetDecodeList(tableId);
        }

        
        public static string GetLanguageById(int languageId)
        {
            return ((LanguageCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Language)).GetLanguage(languageId).Language_Name;
        }
        
        public static List<T_LANGUAGE> GetLanguageList()
        {
            return ((LanguageCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Language)).GetAllLanguages();
        }

        
        public static string GetCategoryById(int categoryId)
        {
            return ((CategoryCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Category)).GetCategory(categoryId).Category_Desc;
        }

        public static List<T_CATEGORY> GetCategoryList()
        {
            return ((CategoryCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Category)).GetAllCategories();
        }

        
        public static string GetCountryById(int countryId)
        {
            return ((CountryCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Country)).GetCountry(countryId).Country_Name;
        }

        public static List<T_COUNTRY> GetCountryList()
        {
            return ((CountryCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Country)).GetAllCountries();
        }

        public static List<string> GetCountryBlockedList()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.T_COUNTRies.Where(i => !i.Is_Accessible).Select(i => i.Country_Name.ToLower()).ToList();
            }
        }


        public static List<T_AD_DIMENSION> GetImageDimsList()
        {
            return ((DimensionCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Dimension)).GetAllDimensions().Where(i => i.Is_Image == 1).ToList();
        }

        public static List<T_AD_DIMENSION> GetSliderDimsList()
        {
            return ((DimensionCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Dimension)).GetAllDimensions().Where(i => i.Is_Slider == 1).ToList();
        }

        public static List<T_AD_DIMENSION> GetVideoDimsList()
        {
            return ((DimensionCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Dimension)).GetAllDimensions().Where(i => i.Is_Video == 1).ToList();
        }


        public static T_THEME GetThemeById(int themeId)
        {
            return ((ThemeCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Theme)).GetTheme(themeId);
        }

        public static List<T_THEME> GetThemeList()
        {
            return ((ThemeCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Theme)).GetAllThemes();
        }

        /*
        public static List<T_AD_DIMENSION> GetAdDimensionList(bool image, bool video)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.T_AD_DIMENSIONs.Where(i => i.Is_Deleted == 0 && (i.Is_Image == (image ? 1 : 0) || i.Is_Video == (video ? 1 : 0)) ).ToList();
            }
        }

        public static List<int> GetWidthList()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.T_AD_DIMENSIONs.Where(i => i.Is_Deleted == 0).OrderBy(i => i.Width).Select(i => i.Width).Distinct().Distinct().ToList();
            }
        }

        public static List<int> GetHeightList()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.T_AD_DIMENSIONs.Where(i => i.Is_Deleted == 0).OrderBy(i => i.Height).Select(i => i.Height).Distinct().Distinct().ToList();
            }
        }

        public static List<string> GetAdDimensionAllowedList(bool image, bool video)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.T_AD_DIMENSIONs.Where(i => i.Is_Deleted == 0 && (i.Is_Image == (image ? 1 : 0) || i.Is_Video == (video ? 1 : 0)) ).Select(i => i.Width.ToString() + "x" + i.Height.ToString()).ToList<string>();
            }
        }

        public static string GetAdDimensionAllowedString(bool image, bool video)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                string list = "";

                // Create comma seperated allowed sizes list.
                foreach (T_AD_DIMENSION size in dataContext.T_AD_DIMENSIONs.Where(i => i.Is_Deleted == 0 && (i.Is_Image == (image ? 1 : 0) || i.Is_Video == (video ? 1 : 0)) ) )
                {
                    list += ("(" + size.Width.ToString() + "x" + size.Height.ToString() + ")" + ", ");
                }

                // Remove last comma.
                if (list.Length > 0)
                    list = list.Substring(0, list.Length - 2);

                return list;
            }
        }

        public static Nullable<int> IsSizeExists(int width, int height, bool image, bool video)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                var query = (
                    from size in dataContext.T_AD_DIMENSIONs
                    where size.Is_Deleted == 0 &&
                         (size.Is_Image == (image ? 1 : 0) || size.Is_Video == (video ? 1 : 0)) &&
                          size.Width == width &&
                          size.Height == height
                    select new { Size_Id = size.Size_Id } );

                return query.SingleOrDefault().Size_Id;
            }
        }

        public static string GetDimensionById(Nullable<int> sizeId)
        {
            if (sizeId == null)
                return null;

            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.T_AD_DIMENSIONs.Where(i => i.Size_Id == sizeId).Select(i => i.Width + "x" + i.Height).SingleOrDefault();
            }
        }        
        */

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Gets decodes list by table id.
        /// </summary>
        /// <param name="tableId">Table id.</param>
        /// <returns>Requested decode (code and value) list.</returns>
        private static List<T_DECODE> GetDecodeList(int tableId)
        {
            return ((DecodeCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Decode)).GetDecodeGroup(tableId).Where(i => i.Is_Deleted == 0).ToList<T_DECODE>();
        }

        #endregion Private Methods
    }
}
