using System;
using System.Linq;
using Inqwise.AdsCaptcha.DAL;

namespace Inqwise.AdsCaptcha.Admin.UserControls
{
    public partial class PressAddNews : System.Web.UI.UserControl
    {

        private int newsToEdit;
        public int NewsToEdit { 
            get{return newsToEdit;}
            set{newsToEdit= value;} 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["edit"]))
            {
                NewsToEdit = Convert.ToInt32(Request["edit"]);
                if (!Page.IsPostBack)
                {
                    FillForEdit();
                }
            }
        }

        public void FillForEdit()
        {
            AdsCaptcha_DevEntities1 ent = new AdsCaptcha_DevEntities1();
            T_PRESS press = ent.T_PRESS.Where(n => n.PressID == newsToEdit).FirstOrDefault();
            if (press != null)
            {
                txtTitle.Text = press.Header;
                txtSummary.Value = press.Summary;
                FreeTextBox1.Text = press.Body;
                txtSource.Text = press.PressSource;
                txtSourceUrl.Text = press.PressSourceUrl;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                AdsCaptcha_DevEntities1 ent = new AdsCaptcha_DevEntities1();
                T_PRESS press = new T_PRESS();
                if (!string.IsNullOrEmpty(Request["edit"]))
                {
                    press = ent.T_PRESS.Where(n => n.PressID == newsToEdit).FirstOrDefault();
                }

                press.Header = txtTitle.Text;
                press.Body = FreeTextBox1.Text;
                press.Summary = txtSummary.Value;
                press.PressSource = txtSource.Text;
                press.PressSourceUrl = txtSourceUrl.Text;
                
                press.LastUpdateDate = DateTime.Now;

                //if (chkIsPDF.Checked)
                //{
                //    PdfFormatter pf = new PdfFormatter();
                //    pf.sSubject = txtTitle.Text;
                //    pf.sTitle = txtTitle.Text;
                //    Report report = new Report(pf);
                //    FontDef fd = new FontDef(report, "Helvetica");
                //    FontProp fp = new FontPropMM(fd, 25);
                //    Root.Reports.Page page = new Root.Reports.Page(report);
                //    page.AddCenteredMM(80, new RepString(fp, "Hello World!"));
                //    RT.ViewPDF(report, "HelloWorld.pdf");
                //}

                if (string.IsNullOrEmpty(Request["edit"]))
                {
                    press.InsertDate = DateTime.Now;
                    ent.AddToT_PRESS(press);
                }
                ent.SaveChanges();
                lblMessage.Text = "News is saved successfully";
            }
            catch (Exception exc)
            {
                lblMessage.Text = "Error: " + exc.ToString();
            }

        }
    }
}