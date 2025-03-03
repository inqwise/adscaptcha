using System;
using System.Drawing;

public partial class LandingDemo_LandingPage : System.Web.UI.Page
{
    public string DemoUrl = string.Empty;
    public string DemoWidth = "300";
    public string DemoHeight = "250";
    protected void Page_Load(object sender, EventArgs e)
    {
        string demourl = Page.RouteData.Values["demo"] as string;
        if (!string.IsNullOrEmpty(demourl))
        {

            DemoUrl = demourl.Replace("_", " ");
            lblHeader.Text = DemoUrl;
        }
        else
        {
            //lblTest.Text = "Wrong!";
        }

        btnEdit.Visible = User.Identity.IsAuthenticated;
        btnSend.Visible = User.Identity.IsAuthenticated;


        if (!Page.IsPostBack)
        {
            if (User.Identity.IsAuthenticated)
            {
                /*
                using (AdsCaptcha_ImagesEntities ent = new AdsCaptcha_ImagesEntities())
                {

                    var demo = ent.T_DEMOS.Where(d => d.DemoUrlName == demourl).FirstOrDefault();
                    btnEdit.CommandArgument = demo.DemoID.ToString();
                    var demoimg = demo.T_DEMOS_IMAGES.FirstOrDefault();
                    if (demoimg != null)
                    {
                       demoimg.T_IMAGESReference.Load();
                       DemoWidth = demoimg.T_IMAGES.Width.ToString();
                       DemoHeight = demoimg.T_IMAGES.Height.ToString();
                    }
                }
                 */
            }

        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblResult.Visible = true;
        try
        {
            int sliderRes = Convert.ToInt32(Request["adscaptcha_response_field"]);
            /*
            int? sliderSaved = ImagesDAL.GetInstance().GetCorrectAnswer(Context.Session.SessionID);
            if (sliderSaved != null)
            {

                if ((sliderRes >= (sliderSaved - 2)) && (sliderRes <= (sliderSaved + 2)))
                {
                    lblResult.Text = "Correct     <br />";
                    lblResult.ForeColor = Color.Green;
                }
                else
                {
                    lblResult.Text = "Wrong. Please try again...    <br />";
                    lblResult.ForeColor = Color.Red;
                }
            }
            else
             */
            {
                lblResult.Text = "Error    <br />";
            }
        }
        catch
        {
            lblResult.Text = "Error    <br />";
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LandingDemo/Edit.aspx?did=" + btnEdit.CommandArgument);
    }
}