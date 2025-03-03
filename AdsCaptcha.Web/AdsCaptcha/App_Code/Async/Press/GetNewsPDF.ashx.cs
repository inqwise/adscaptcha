using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Inqwise.AdsCaptcha.DAL;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Collections.Generic;


namespace Inqwise.AdsCaptcha.Async.Press
{
    /// <summary>
    /// Summary description for $CodeFileclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetNewsPDF : IHttpHandler
    {
       

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "application/pdf";

            try
            {
                if (!string.IsNullOrEmpty(context.Request["id"]))
                {
                    int newsID = Convert.ToInt32(context.Request["id"]);

                    AdsCaptcha_DevEntities1 ent = new AdsCaptcha_DevEntities1();
                    var news = ent.T_PRESS.Where(n => n.PressID == newsID).FirstOrDefault();
                    if (news != null)
                    {

                        HttpResponse Response = context.Response;

                        Response.Clear();

                        Response.ContentType = "application/octet-stream";

                        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + news.Header.Replace(" ", "_") + ".pdf\"");

                        // step 1: creation of a document-object

                        Document document = new Document(PageSize.A4, 10, 10, 90, 10);

                        // step 2: we create a writer that listens to the document

                        PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

                        //set some header stuff

                        document.AddTitle(news.Header);
                        document.AddSubject(news.Header);
                        document.AddCreator("Inqwise");
                        document.AddAuthor("Inqwise");


                        document.Open();

                        // step 4: we add content to the document

                        
                        //document.Add(FormatHeaderPhrase(news.Header));
                        string logoHtml = "<a class='logo' href='http://www.Inqwise.com'><img width='189' height='100' src='http://www.Inqwise.com/css/Inqwise/images/Inqwise-Logo.png'/></a>";
                        logoHtml += "<br /><br /><br /><br /><br /><br />";

                        string dateHtml = "<div>" + news.LastUpdateDate.ToLongDateString() + "</div>";

                        string html = "<html><head></head><body>" + logoHtml + "<h1>" + news.Header + "</h1><br /><br />" + dateHtml + "<br /><br /><br />" + news.Body + "</body></html>";
                        TextReader tr = new StringReader(html);

                        StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();

                        IDictionary<string, string> h1style = new Dictionary<string, string>();
                        h1style.Add("width", "100%");
                        h1style.Add("text-align", "center");
                        h1style.Add("margin-bottom", "100px");
                        h1style.Add("margin-left", "0 auto");
                        h1style.Add("font-weight", "bold");
                        //h1style.Add("text-decoration", "underline");

                        styles.LoadTagStyle("h1", h1style);

                        IDictionary<string, string> logostyle = new Dictionary<string, string>();
                        logostyle.Add("width", "100%");
                        logostyle.Add("margin-bottom", "100px");
                        logostyle.Add("margin-left", "0 auto");

                        styles.LoadTagStyle(".logo", logostyle);


                        var objects = HTMLWorker.ParseToList(tr, styles);

                        //document.Add(FormatPhrase(news.Body));

                        for (int k = 0; k < objects.Count; k++)
                        {

                            document.Add((IElement)objects[k]);

                        }

                        tr.Close();

                        document.Close();

                       
                    }
                }
            }
            catch(Exception exc)
            {
                throw exc;
            }
          

            
           // context.Response.Write("Hello World");
        }


        private static Phrase FormatPhrase(string value)
        {
            Chunk chk = new Chunk();
           

            return new Phrase(value, FontFactory.GetFont(FontFactory.HELVETICA, 8));
        }

        private static Phrase FormatHeaderPhrase(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD, new BaseColor(255, 0, 0)));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
   
    }
}
