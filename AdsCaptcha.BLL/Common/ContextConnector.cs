using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeSmith.Data.Linq;
using Inqwise.AdsCaptcha.DAL;
using System.Threading;

namespace Inqwise.AdsCaptcha.BLL.Common
{
    public class ContextConnector
    {
        protected static AdsCaptchaDataContext dataContext = null;
        private static Object mutexObj = new object();

        public static void StartMeasureProcess()
        {
            lock (mutexObj)
            {
                if (dataContext == null)
                    dataContext = new AdsCaptchaDataContext();
            }
        }

        public static void EndMeasureProcess()
        {
            lock (mutexObj)
            {
                if (dataContext != null)
                {
                    dataContext.Dispose();
                    dataContext = null;
                }
            }
        }

        public static AdsCaptchaDataContext DataContext
        {
            get
            {
                if (dataContext == null)
                    StartMeasureProcess();
                return dataContext;
            }
        }
    }
}
