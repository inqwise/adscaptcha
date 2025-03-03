using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class SkipAdvertisers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                // Metadata.SetMetadata(Metadata.Pages.SkipAdvertisers, Master.Page.Header);

                InitControls();
            }
        }        

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
        }
    }
}
