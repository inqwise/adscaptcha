using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Advertiser, Master.Page.Header);
                
                InitControls();
            }
        }

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            // Set panels state.
            panelContactForm.Visible = true;
            panelMessageSent.Visible = false;

            string lengthFunction = "function isMaxLength(txtBox) {";
                lengthFunction += " if(txtBox) { ";
                lengthFunction += "     return ( txtBox.value.length <" + "1000" + ");";
                lengthFunction += " }";
                lengthFunction += "}";

            // Add message body max length restriction.
            textNotes.Attributes.Add("onkeypress", "return isMaxLength(this);");
            ClientScript.RegisterClientScriptBlock(
                this.GetType(),
                "txtLength",
                lengthFunction, true);
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit
            if (!Page.IsValid)
            {
                return;
            }

            // Get values.
            string name = textName.Text;
            string email = textEmail.Text;
            string type = listType.SelectedItem.Text;
            string agency = textAgencyBrandName.Text;
            string website = textWebsite.Text;
            string phone = textPhone.Text;
            string country = textCountry.Text;
            string notes = textNotes.Text;

            string recipients;
            string subject;
            string body;

            /*
            // Write new message to DB.
            int messageId = CrmBLL.Add(name,
                                       email,
                                       phone,
                                       (int)Constants.CrmSubject.Advertiser,
                                       "Type: " + type + " | " + "Agency: " + agency + " | " + "Website: " + " | " + "Country: " + country + " | " + "Message: " + notes);
            */

            // Send mail to advertiser.
            recipients = email;
            subject = "Your message recieved | AdsCaptcha";
            body =
                "Hello " + name + "," + "<br /><br />" + 
                "We've received your message. You can expect a response within 72 hours." + "<br /><br />" + 
                "Thanks," + "<br />" +
                "The AdsCaptcha team";

            Mail.SendMail(recipients, subject, body);

            // Send mail to administrator.
            recipients = ConfigurationSettings.AppSettings["SupportEmail"];
            subject = "New advertiser interests in AdsCaptcha";
            body = @"
                <table>
                    <tr><td><b>Name</b></td><td>" + name + @"</td></tr>
                    <tr><td><b>Email</b></td><td>" + email + @"</td></tr>
                    <tr><td><b>Type</b></td><td>" + type + @"</td></tr>
                    <tr><td><b>Agency/Brand</b></td><td>" + agency + @"</td></tr>
                    <tr><td><b>Website</b></td><td>" + website + @"</td></tr>
                    <tr><td><b>Phone</b></td><td>" + phone + @"</td></tr>
                    <tr><td><b>Country</b></td><td>" + country + @"</td></tr>
                    <tr><td><b>Notes</b></td><td>" + notes + @"</td></tr>
                </table>
                ";

            Mail.SendMail(recipients, subject, body);

            panelContactForm.Visible = false;
            panelMessageSent.Visible = true;
        }
    }
}
