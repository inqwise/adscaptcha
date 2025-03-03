using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using System.Net;
using System.IO;
using System.Text;
using System.Drawing;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha
{
    public partial class Products : System.Web.UI.Page
    {
    
    	protected string ApiUrl {
            get { return ApplicationConfiguration.ApiUrl.Value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Products, Master.Page.Header);

                //InitControls();
            }
        }        


		/*
        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
        }
		*/
		
		/*
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            Stream receiveStream = null;
            WebResponse response = null;
            StreamReader readStream = null;
            try
            {
                int sliderRes = Convert.ToInt32(Request["adscaptcha_response_field"]);
                string sliderRequest = Request["adscaptcha_challenge_field"];
                WebRequest req = HttpWebRequest.Create("http://demo.Inqwise.com/slider/validate.ashx?r=" + sliderRequest + "&a=" + sliderRes.ToString());
                response = req.GetResponse();
                receiveStream = response.GetResponseStream();
                readStream = new StreamReader(receiveStream, Encoding.UTF8);

                string answer = readStream.ReadToEnd();

                if (answer == "1")
                {
                    lblResult.Text = "Yuuuuup! You're a human being...";
                    lblResult.ForeColor = Color.Green;
                    lblResult.Visible = true;
                }
                else
                {
                    lblResult.Text = "No, Mr. Robot, you shall not pass!";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Visible = true;
                }

            }
            catch (Exception exc)
            {
                lblResult.Text = "Error! Try again later";
                lblResult.ForeColor = Color.Black;
                lblResult.Visible = true;
            }
            finally
            {
                if (response != null) response.Close();
                if (receiveStream != null) receiveStream.Close();
                if (readStream != null) readStream.Close();
            }

        }
        */
    }
}
