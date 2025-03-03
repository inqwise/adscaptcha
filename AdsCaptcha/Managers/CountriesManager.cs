using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Dal;
using Inqwise.AdsCaptcha.Entities;
using Inqwise.AdsCaptcha.SystemFramework;
using Inqwise.AdsCaptcha.SystemFramework.GeoIp;

namespace Inqwise.AdsCaptcha.Managers
{
    public class CountriesManager
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        private static readonly TimeSpan DEFAULT_COUNTRIES_CACHE_TIMEOUT = TimeSpan.FromDays(1);
        private static DateTime? cachedFrom;
        private static Dictionary<int, ICountry> countriesById;
        private static Dictionary<string, ICountry> countriesByPrefix;
        private static List<ICountry> countries;
        public static ICountry DefaultCountry = new CountryEntity {Id = 0, Name = "All", Prefix = "All"};
        

        public static IEnumerable<ICountry> GetList()
        {
            CheckCountries();
            IEnumerable<ICountry> result = countries;

            if (IsExpired())
            {
                cachedFrom = null;
            }

            return result;
        }

        public static AdsCaptchaOperationResult<ICountry> Get(int id, bool defaultNotExist = false)
        {
            ICountry country;
            CheckCountries();

            countriesById.TryGetValue(id, out country);

            if (IsExpired())
            {
                if (Monitor.TryEnter(typeof(CountriesManager)))
                {
                    try
                    {
                        if (IsExpired())
                        {
                            cachedFrom = null;
                        }
                    }
                    finally
                    {
                        Monitor.Exit(typeof(CountriesManager));
                    }
                }
            }

            if (null == country && defaultNotExist)
            {
                country = DefaultCountry;
            }

            return AdsCaptchaOperationResult<ICountry>.ToValueOrNotExist(country);
        }

        private static bool IsExpired()
        {
            return null != cachedFrom && cachedFrom + DEFAULT_COUNTRIES_CACHE_TIMEOUT < DateTime.Now;
        }

        public static AdsCaptchaOperationResult<ICountry> Get(string prefix, bool defaultNotExist = false)
        {
            ICountry country;
            CheckCountries();

            countriesByPrefix.TryGetValue(prefix, out country);

            if (IsExpired())
            {
                if(Monitor.TryEnter(typeof (CountriesManager)))
                {
                    try
                    {
                        if (IsExpired())
                        {
                            cachedFrom = null;
                        }
                    }
                    finally
                    {
                        Monitor.Exit(typeof (CountriesManager));
                    }
                }
            }

            if (null == country && defaultNotExist)
            {
                country = DefaultCountry;
            }

           
            
            return AdsCaptchaOperationResult<ICountry>.ToValueOrNotExist(country);
        }

        private static void CheckCountries()
        {
            if (null == cachedFrom)
            {
                lock (typeof (CountriesManager))
                {
                    if (null == cachedFrom)
                    {
                        var tmpCountriesById = new Dictionary<int, ICountry>();
                        var tmpCountriesByPrefix = new Dictionary<string, ICountry>();
                        var tmpCountries = new List<ICountry>();
                        FillCountries(tmpCountriesById, tmpCountriesByPrefix, tmpCountries);
                        countriesById = tmpCountriesById;
                        countriesByPrefix = tmpCountriesByPrefix;
                        countries = tmpCountries;
                        cachedFrom = DateTime.Now;
                    }
                }
            }
        }

        private static void FillCountries(Dictionary<int, ICountry> countriesById, IDictionary<string, ICountry> countriesByPrefix, List<ICountry> countries)
        {
            using (var reader = CountriesDataAccess.GetDataReader())
            {
                while (reader.Read())
                {
                    var c = new CountryEntity(reader);
                    countriesById.Add(c.Id, c);
                    countriesByPrefix.Add(c.Prefix, c);
                    countries.Add(c);
                }
            }
        }

        public static AdsCaptchaOperationResult<ICountry> GetByIp(string ipAddress)
        {
            Stopwatch sw = Stopwatch.StartNew();
            AdsCaptchaOperationResult<ICountry> result = null;
            var lookupCountry = LookupServiceFactory.GetLookupService().getCountry(ipAddress);
            sw.Stop();
            if (sw.ElapsedMilliseconds > 100)
            {
                Log.Warn("GetByIp: 1: {0}", sw.ElapsedMilliseconds);
            }

            if (null == lookupCountry.getCode())
            {
                result = AdsCaptchaOperationResult<ICountry>.ToError(AdsCaptchaErrors.NoResults);
            }
            else
            {
                sw.Restart();
                result = Get(lookupCountry.getCode());
                sw.Stop();
                if (sw.ElapsedMilliseconds > 100)
                {
                    Log.Warn("GetByIp: 2: {0}", sw.ElapsedMilliseconds);
                }
            }

            return result;
        }
    }
}