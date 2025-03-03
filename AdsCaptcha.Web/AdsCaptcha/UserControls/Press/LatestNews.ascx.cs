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

namespace Inqwise.AdsCaptcha.UserControls.Press
{
    public partial class LatestNews : System.Web.UI.UserControl
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
            var news = ent.T_PRESS.OrderByDescending(n => n.LastUpdateDate).ToList();

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

                    HtmlGenericControl h5Header = e.Item.FindControl("h5Header") as HtmlGenericControl;
                    if (h5Header != null)
                    {
                        if (!string.IsNullOrEmpty(press.PressSourceUrl.Trim()))
                        {
                            h5Header.Attributes["onclick"] = "window.open('" + press.PressSourceUrl + "');";
                            h5Header.Attributes["style"] = "cursor:pointer;";
                        }

                    }

                    Literal lblDate = e.Item.FindControl("lblDate") as Literal;
                    if (lblDate != null)
                    {
                        lblDate.Text = press.LastUpdateDate.ToLongDateString();
                    }

                    Label lblSummary = e.Item.FindControl("lblSummary") as Label;
                    if (lblSummary != null)
                    {
                        lblSummary.Text = press.Summary;
                        if (!string.IsNullOrEmpty(press.PressSourceUrl.Trim()))
                        {
                            lblSummary.Attributes["onclick"] = "window.open('" + press.PressSourceUrl + "');";
                            lblSummary.Attributes["style"] = "cursor:pointer;";
                        }
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
                }

            }
        }
    }
}