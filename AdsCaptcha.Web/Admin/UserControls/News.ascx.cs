using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Inqwise.AdsCaptcha.DAL;

namespace Inqwise.AdsCaptcha.Admin.UserControls
{
    public partial class News : System.Web.UI.UserControl
    {
         protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillData();
            }
        }

        private void FillData()
        {

            AdsCaptcha_DevEntities1 ent = new AdsCaptcha_DevEntities1();
            var news = ent.T_PRESS.OrderBy(n => n.LastUpdateDate).ToList();

            rptNews.DataSource = news;
            rptNews.DataBind();
        }

        protected void rptNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) ||
                (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                T_PRESS press = e.Item.DataItem as T_PRESS;
                if (press != null)
                {
                    Literal lblHeader = e.Item.FindControl("lblHeader") as Literal;
                    if (lblHeader != null)
                    {
                        lblHeader.Text = press.Header;
                    }

                    Literal lblSummary = e.Item.FindControl("lblSummary") as Literal;
                    if (lblSummary != null)
                    {
                        lblSummary.Text = press.Summary;
                    }

                    HtmlAnchor aPDF = e.Item.FindControl("aPDF") as HtmlAnchor;
                    if (aPDF != null)
                    {
                        aPDF.HRef = ConfigurationManager.AppSettings["URL"] + "async/press/GetNewsPDF.ashx?id=" + press.PressID.ToString();
                    }

                    HtmlAnchor aSource = e.Item.FindControl("aSource") as HtmlAnchor;
                    if (aSource != null)
                    {
                        if (press.PressSource.Trim() == string.Empty)
                        {
                            aSource.Visible = false;
                        }
                        else
                        {
                            aSource.InnerHtml = press.PressSource;
                            aSource.HRef = press.PressSourceUrl;
                        }
                    }

                    HtmlAnchor aEdit = e.Item.FindControl("aEdit") as HtmlAnchor;
                    if (aEdit != null)
                    {
                        aEdit.HRef = ConfigurationManager.AppSettings["Admin"] + "Presskit.aspx?edit=" + press.PressID.ToString();
                    }
                }

            }
        }
    }
}
