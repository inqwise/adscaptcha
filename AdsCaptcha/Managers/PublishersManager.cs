using System;
using System.Data;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Dal;
using Inqwise.AdsCaptcha.Entities;
using NLog;

namespace Inqwise.AdsCaptcha.Managers
{
    public class PublishersManager
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static AdsCaptchaOperationResult<IPublisher> Get(int publisherId)
        {
            AdsCaptchaOperationResult<IPublisher> result = null;
            try
            {
                using (IDataReader reader = PublishersDataAccess.GetReader(publisherId))
                {
                    if (reader.Read())
                    {
                        result = new PublisherEntity(reader);
                    }
                    else
                    {
                        result = AdsCaptchaOperationResult<IPublisher>.ToError(AdsCaptchaErrors.NoResults);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Get: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult<IPublisher>.ToError(AdsCaptchaErrors.GeneralError,
                                                                              description: ex.ToString());
            }

            return result;
        }
    }
}