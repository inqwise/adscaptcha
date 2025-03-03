using System;

namespace Inqwise.AdsCaptcha.SystemFramework.GeoIp
{
    public static class LookupServiceFactory
    {
        private static readonly Lazy<LookupService> LookupService = new Lazy<LookupService>(() => new LookupService(ApplicationConfiguration.GeoIpPath.Value, SystemFramework.GeoIp.LookupService.GEOIP_MEMORY_CACHE));

        public static LookupService GetLookupService()
        {
            return LookupService.Value;
        }
    }
}