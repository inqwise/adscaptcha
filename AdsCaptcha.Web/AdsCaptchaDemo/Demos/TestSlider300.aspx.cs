using System;
using System.Drawing;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Managers;

public partial class TestSlider300 : System.Web.UI.Page
{
    private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


	/*
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblResult.Visible = true;
        try
        {
            int sliderRes = Convert.ToInt32(Request["adscaptcha_response_field"]);
            string cid = Request["adscaptcha_challenge_field"];
            // Get request
            var actualRequestResult = RequestsManager.Get(cid, isDemo:true);
            if (actualRequestResult.HasError)
            {
                // Cache timeout or not found
                lblResult.Text = "Wrong. Please try again...    <br />";
                lblResult.ForeColor = Color.Red;
            }
            else
            {
                IRequest captchaRequest = actualRequestResult.Value;
                if (sliderRes == captchaRequest.CorrectIndex)
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
        }
        catch(Exception ex)
        {
            Log.ErrorException("btnSubmit_Click: Unexpected error occured", ex);
            lblResult.Text = "Error    <br />";
        }
    }
    
    */
}