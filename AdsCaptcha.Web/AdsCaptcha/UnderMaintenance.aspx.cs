using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Inqwise.AdsCaptcha
{
    public partial class UnderMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set metadata (title, keywords and description).
            Metadata.SetMetadata(Metadata.Pages.General, Master.Page.Header);
        }
    }
}
