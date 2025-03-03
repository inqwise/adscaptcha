using System;

namespace Inqwise.AdsCaptcha.Common.Data
{
    public class VisitorArgs
    {
        public int CountOfTouches { get; set; }
        public string IpAddress { get; set; }
        public long CaptchaId { get; set; }
        public DateTime InsertDate { get; set; }
        public string CountryCode { get; set; }
        public string CaptchaIdAndIpAddress {
            get { return (string.IsNullOrEmpty(IpAddress) ? null : string.Concat("cip", CaptchaId,"_", IpAddress)); }
        }

        public string VisitorUid { get; set; }

        public int DifficultyLevelId { get; set; }

        public string CaptchaIdAndVisitorId
        {
            get { return null == VisitorUid ? null : (string.Concat("cvid", CaptchaId, "_", VisitorUid.Replace("-",""))); }
        }

        public CaptchaStatistics Statistics { get; set; }
    }

    public class CaptchaStatistics
    {
        private const int MINIMUM_INSTANCES_BY_TEN_SEC = 40;
        public double LastMinByTenSecAvg { get; set; }
        public long LastMinByTenSecMax { get; set; }

        public double LastTenMinByMinAvg { get; set; }
        public long LastTenMinByMinMax { get; set; }

        public double LastHourByTenMinAvg { get; set; }
        public long LastHourByTenMinMax { get; set; }

        public double LastDayByHourAvg { get; set; }
        public long LastDayByHourMax { get; set; }

        public double LastTenDaysByDayAvg { get; set; }
        public long LastTenDaysByDayMax { get; set; }


        public double DiffLastMinByTenSec {
            get { return (MINIMUM_INSTANCES_BY_TEN_SEC > LastMinByTenSecAvg || LastMinByTenSecMax < MINIMUM_INSTANCES_BY_TEN_SEC ? 0 : LastMinByTenSecMax / LastMinByTenSecAvg); }
        }

        public double DiffLastTenMinByMin
        {
            get { return (1 > LastTenMinByMinAvg || MINIMUM_INSTANCES_BY_TEN_SEC > LastMinByTenSecAvg || LastTenMinByMinCountOfZero > 1 ? 0 : LastMinByTenSecAvg * 6 / LastTenMinByMinAvg); }
        }

        public double DiffHourByTenMin
        {
            get { return (1 > LastHourByTenMinAvg || MINIMUM_INSTANCES_BY_TEN_SEC > LastMinByTenSecAvg || LastHourByTenMinCountOfZero > 1 ? 0 : LastMinByTenSecAvg * 60 / LastHourByTenMinAvg); }
        }

        public double DiffDayByHour
        {
            get { return (1 > LastDayByHourAvg || MINIMUM_INSTANCES_BY_TEN_SEC > LastMinByTenSecAvg || LastDayByHourCountOfZero > 0 ? 0 : LastMinByTenSecAvg * 60 * 6 / LastDayByHourAvg); }
        }

        public double DiffTenDaysByDay
        {
            get { return (1 > LastTenDaysByDayAvg || MINIMUM_INSTANCES_BY_TEN_SEC > LastMinByTenSecAvg || LastTenDaysByDayCountOfZero > 0 ? 0 : LastMinByTenSecAvg * 60 * 6 * 24 / LastTenDaysByDayAvg); }
        }

        public int? LastTenMinByMinCountOfZero { get; set; }

        public int? LastHourByTenMinCountOfZero { get; set; }

        public int? LastDayByHourCountOfZero { get; set; }

        public int? LastTenDaysByDayCountOfZero { get; set; }
    }
}