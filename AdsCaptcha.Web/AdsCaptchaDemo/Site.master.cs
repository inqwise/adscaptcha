﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    public string BaseUrl = ConfigurationManager.AppSettings["Demo"];
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
