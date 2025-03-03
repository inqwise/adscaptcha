using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demos_Videos : System.Web.UI.Page
{
    public string StartVideoNo = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["n"]))
        {
            try
            {
                StartVideoNo = Convert.ToInt32(Request["n"]).ToString();
            }
            catch
            {
                StartVideoNo = "0";
            }

        }
    }
}