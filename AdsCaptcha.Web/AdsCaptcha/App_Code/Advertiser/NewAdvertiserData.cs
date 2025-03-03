using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advertiser
{
    /// <summary>
    /// Summary description for NewAdvertiserData
    /// </summary>
    [Serializable]
    public class NewAdvertiserData
    {
        public string Email = null;
        public string Password = null;
        public int BillingMethod = 0;
        public int PaymentMethod = 0;
        public bool GetEmailAnnouncements = false;
        public bool GetEmailNewsletters = false;
        public string FirstName;
        public string LastName;
        public string CompanyName = null;
        public string Address = null;
        public string City = null;
        public string State = null;
        public int Country = 0;
        public string ZipCode = null;
        public string Phone = null;
        public string Fax = null;
    } 
}