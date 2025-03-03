using System;

public partial class gallery_Default : System.Web.UI.Page
{
    protected string Auid { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        Auid = Request["auid"];
    }
}