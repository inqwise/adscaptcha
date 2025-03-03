using System;
using System.Web.UI.WebControls;

public partial class LandingDemo_Demos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated)
            Response.Redirect("~/Account/Login?returnUrl=" + Request.Url.LocalPath);
    }
    protected void gvDemos_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void gvDemos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Select":
                
                Response.Redirect("~/LandingDemo/Edit.aspx?did=" + gvDemos.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString());
                break;
            case "Preview":
                Response.Redirect("~/lp/" + Server.UrlEncode(gvDemos.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString()));
                break;
            case "DeleteDemo":
                /*
                using (AdsCaptcha_ImagesEntities ent = new AdsCaptcha_ImagesEntities())
                {
                    int demoid = Convert.ToInt32(gvDemos.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString());
                    var demo = ent.T_DEMOS.Where(d => d.DemoID == demoid).FirstOrDefault();
                    if(demo != null)
                    {
                        demo.IsDeleted = true;
                        ent.SaveChanges();
                        Response.Redirect("~/LandingDemo/Demos.aspx");
                    }
                }
                 * 
                 */
                break;
                
        }
    }
}