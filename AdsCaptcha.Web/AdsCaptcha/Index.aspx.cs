using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                
                Metadata.SetMetadata(Metadata.Pages.General, Master.Page.Header);

                InitControls();
            }
        }

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            // Get total served.
            //decimal totalServed = MeasureBLL.GetTotalServed();
            
            //if (ConfigurationSettings.AppSettings["Environment"] == "LiveDemo")
            //    totalServed += 17000000;

            //labelCaptchaServed.Text = String.Format("{0:#,##}", totalServed);
        }
    }
}
