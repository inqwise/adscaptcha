using System;
using System.Data;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Dal;
using Inqwise.AdsCaptcha.Entities;
using NLog;

namespace Inqwise.AdsCaptcha.Managers
{
    public class AdvertisersManager
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static AdsCaptchaOperationResult<IAdvertiser> Get(int advertiserId)
        {
            AdsCaptchaOperationResult<IAdvertiser> result = null;
            try
            {
                using (IDataReader reader = AdvertisersDataAccess.GetReader(advertiserId))
                {
                    if (reader.Read())
                    {
                        result = new AdvertiserEntity(reader);
                    }
                    else
                    {
                        result = AdsCaptchaOperationResult<IAdvertiser>.ToError(AdsCaptchaErrors.NoResults);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Get: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult<IAdvertiser>.ToError(AdsCaptchaErrors.GeneralError,
                                                                              description: ex.ToString());
            }

            return result;
        }
    }
}