using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class SkipSiteOwners : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                // Metadata.SetMetadata(Metadata.Pages.SkipSiteOwners, Master.Page.Header);

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
