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
using Root.Reports;
using Inqwise.AdsCaptcha.DAL;

namespace Inqwise.AdsCaptcha.Async.Press
{
    public partial class NewsPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    int newsID = Convert.ToInt32(Request["id"]);

                    AdsCaptcha_DevEntities1 ent = new AdsCaptcha_DevEntities1();
                    var news = ent.T_PRESS.Where(n => n.PressID == newsID).FirstOrDefault();
                    if (news != null)
                    {

                        PdfReport<NewsReport> pdfReport = new PdfReport<NewsReport>();
                        
                        pdfReport.Response(this);

                       
                    }
                }
        }

        private class NewsReport : Report
        {
            private T_PRESS news;

            public NewsReport() : base()
            {

                PdfFormatter pf = (PdfFormatter)formatter;
                pf.sTitle = "Inqwise PDF";
                pf.sAuthor = "Inqwise";
                //pf.sSubject = "Sample of some PDF features";
                pf.sKeywords = "";
                pf.sCreator = "Inqwise PDF";
                pf.dt_CreationDate = DateTime.Now;
                pf.pageLayout = PageLayout.OneColumn;
                
                pf.bHideToolBar = true;
                pf.bHideMenubar = false;
                pf.bHideWindowUI = true;
                pf.bFitWindow = true;
                pf.bCenterWindow = true;
                pf.bDisplayDocTitle = true;

                if (!string.IsNullOrEmpty(HttpContext.Current.Request["id"]))
                {
                    int newsID = Convert.ToInt32(HttpContext.Current.Request["id"]);

                    AdsCaptcha_DevEntities1 ent = new AdsCaptcha_DevEntities1();
                    var news = ent.T_PRESS.Where(n => n.PressID == newsID).FirstOrDefault();
                    if (news != null)
                    {
                        pf.sSubject = news.Header;
                        this.news = news;

                    }
                }
            }

            protected override void Create()
            {
                //FontDef fontDef = FontDef.fontDef_FromName(this, FontDef.StandardFont.Helvetica);
                //FontProp fontProp = new FontPropMM(fontDef, 20);
                //new Root.Reports.Page(this);
                //page_Cur.AddCB_MM(80, new RepString(fontProp, "Hello World"));

                //PdfFormatter pf = new PdfFormatter();
                //pf.sSubject = news.Header;
                //pf.sTitle = news.Header;
                //this.formatter = pf;

                FontDef fd = new FontDef(this, FontDef.StandardFont.Helvetica);
                FontProp fp = new FontPropMM(fd, 4);
                FontProp fp_Title = new FontPropMM(fd, 11);
                fp_Title.bBold = true;

                //Root.Reports.Page page = new Root.Reports.Page(this);
                page_Cur = new Root.Reports.Page(this);
                page_Cur.AddCB_MM(40, new RepString(fp_Title, news.Header));
                page_Cur.AddCB_MM(80, new RepString(fp, news.Body));

            }
        }
    }
}
