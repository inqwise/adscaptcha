using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Admin.Components
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            MeasureBLL.StartMeasureProcess();
            base.OnPreInit(e);
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);
            MeasureBLL.EndMeasureProcess();
        }
    }
}
