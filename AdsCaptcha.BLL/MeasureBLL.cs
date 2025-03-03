using System;
using System.Linq;
using CodeSmith.Data.Linq;
using AdsCaptcha.Model;
using Inqwise.AdsCaptcha.BLL.Common;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.BLL
{
    public class MeasureBLL : ContextConnector
    {
        private static int cacheDuration = 300; // 5 minutes
        private static int bigCacheDuration = 21600; // 5 minutes
        

        #region Public Methods

        /* Finance */

        

        public static decimal GetTotalIncome(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);

            try
            {
                return DataContext.TA_CHARGE_AGGs.Where(i => i.Date >= from && i.Date < to).Sum(s => s.Charges);
            }
            catch
            {
                return 0;
            }

        }

        public static decimal GetTotalOutcomeDev(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);


            try
            {
                return DataContext.TD_EARNING_AGGs.Where(i => i.Date >= from && i.Date < to).Sum(s => s.Earnings);
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalOutcomePub(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);


            try
            {
                return DataContext.TP_EARNING_AGGs.Where(i => i.Date >= from && i.Date < to).Sum(s => s.Earnings);
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalProfit(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);

            try
            {
                return DataContext.TM_PROFIT_AGGs.Where(i => i.Date >= from && i.Date < to).Sum(s => s.Profits);
            }
            catch
            {
                return 0;
            }
        }


        /* Publishers */

        public static decimal GetTotalNewPublishers(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);

            try
            {
                return DataContext.TP_PUBLISHERs.Where(i => i.Join_Date >= from && i.Join_Date < to).Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalPublishers()
        {
            try
            {
                return DataContext.TP_PUBLISHERs.Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalActivePublishers()
        {
            try
            {
                return DataContext.TP_PUBLISHERs.Where(i => i.Status_Id == (int)Status.Running).Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalPublishersToBePaid()
        {
            try
            {
                return DataContext.Admin_GetPublishersToBePaidList().Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalWebsites()
        {
            try
            {
                return DataContext.TP_WEBSITEs.Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalActiveWebsites()
        {
            try
            {
                return DataContext.TP_WEBSITEs.Where(i => i.Status_Id == (int)Status.Running).Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalPendingWebsites()
        {
            try
            {
                return DataContext.TP_WEBSITEs.Where(i => i.Status_Id == (int)Status.Pending).Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetWebsiteAvgValue()
        {
            try
            {
                return (DataContext.TP_EARNINGs.Sum(s => s.Earnings) / DataContext.TP_WEBSITEs.Count());
            }
            catch
            {
                return 0;
            }
        }


        /* Developers */

        public static decimal GetTotalNewDevelopers(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);

            try
            {
                return DataContext.TD_DEVELOPERs.Where(i => i.Join_Date >= from && i.Join_Date < to).Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalDevelopers()
        {
            try
            {
                return DataContext.TD_DEVELOPERs.Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalActiveDevelopers()
        {
            try
            {
                return DataContext.TD_DEVELOPERs.Where(i => i.Status_Id == (int)Status.Running).Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalDevelopersToBePaid()
        {
            try
            {
                return DataContext.Admin_GetDevelopersToBePaidList().Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalNewWebsites(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);

            try
            {
                return DataContext.TP_WEBSITEs.Where(i => i.Add_Date >= from && i.Add_Date < to).Count();
            }
            catch
            {
                return 0;
            }
        }


        /* Advertisers */

        public static decimal GetTotalNewAdvertisers(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);

            try
            {
                return DataContext.TA_ADVERTISERs.Where(i => i.Join_Date >= from && i.Join_Date < to).Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalAdvertisers()
        {
            try
            {
                return DataContext.TA_ADVERTISERs.Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalActiveAdvertisers()
        {
            try
            {
                return DataContext.TA_ADVERTISERs.Where(i => i.Status_Id == (int)Status.Running).Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetAdvertiserAvgValue()
        {
            try
            {
                return (DataContext.TA_CHARGEs.Sum(s => s.Charges) / DataContext.TA_ADVERTISERs.Count());
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalNewAds(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);

            try
            {
                return DataContext.TA_ADs.Where(i => i.Add_Date >= from && i.Add_Date < to).Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalAds()
        {
            try
            {
                return DataContext.TA_ADs.Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalActiveAds()
        {
            try
            {
                return DataContext.TA_ADs.Where(i => i.Status_Id == (int)Status.Running).Count();
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetAvgBid()
        {
            try
            {
                return DataContext.TA_ADs.Average(a => a.Max_Cpt);
            }
            catch
            {
                return 0;
            }
        }


        /* Requests */

        public static decimal GetTotalServed()
        {
            try
            {
                return DataContext.TCS_REQUEST_AGGs.FromCache(bigCacheDuration).Sum(s => s.Served);
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalServed(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);

            try
            {
                return DataContext.TCS_REQUEST_AGGs.Where(i => i.Date >= from && i.Date < to).Sum(s => s.Served);
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalTyped(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);

            try
            {
                return DataContext.TCS_REQUEST_AGGs.Where(i => i.Date >= from && i.Date < to).Sum(s => s.Typed);
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetTotalTypedCorrect(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);

            try
            {
                return DataContext.TCS_REQUEST_AGGs.Where(i => i.Date >= from && i.Date < to).Sum(s => s.Correct);
            }
            catch
            {
                return 0;
            }
        }

        [Obsolete("Not in use" ,true)]
        public static decimal GetTotalErrors(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);

            try
            {
                return DataContext.TCS_ERRORs.Where(i => i.Timestamp >= from && i.Timestamp < to && i.Error_Type == (int)Constants.ErrorType.Error).Count();
            }
            catch
            {
                return 0;
            }
        }

        [Obsolete("Not in use", true)]
        public static decimal GetTotalWarnings(DateTime from, DateTime to)
        {
            // Check if specific date selected.
            if (from == to) to = to.AddDays(1);

            try
            {
                return DataContext.TCS_ERRORs.Where(i => i.Timestamp >= from && i.Timestamp < to && i.Error_Type == (int)Constants.ErrorType.Warning).Count();
            }
            catch
            {
                return 0;
            }
        }


        /* Support */

        public static decimal GetTotalNewRequests()
        {
            try
            {
                return DataContext.TC_REQUESTs.Where(i => i.Status_Id == (int)CrmStatus.New).Count();
            }
            catch
            {
                return 0;
            }
        }


        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods
    }
}
