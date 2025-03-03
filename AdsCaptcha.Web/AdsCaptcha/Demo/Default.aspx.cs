using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiveDemo
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonGenerate_Click(object sender, EventArgs e)
        {
            Response.Redirect("Demo.aspx?id=" + textCaptchaId.Text);
        }
    }
}
