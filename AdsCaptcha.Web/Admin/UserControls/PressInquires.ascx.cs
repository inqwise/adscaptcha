using System;
using System.Linq;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;

namespace Inqwise.AdsCaptcha.Admin.UserControls
{
    public partial class PressInquires : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData(0);
        }

        private void LoadData(int page)
        {
            AdsCaptcha_DevEntities1 ent = new AdsCaptcha_DevEntities1();
            var inquires = ent.T_PRESS_INQUIRES.OrderByDescending(n=>n.InsertDate).Skip(page * 10).Take(10).ToList();

            gvInquires.DataSource = inquires;
            gvInquires.DataBind();

        }


        protected void gvInquires_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadData(e.NewPageIndex);
        }

    }
}