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

namespace Inqwise.AdsCaptcha
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.ContactUs, Master.Page.Header);
                
                InitControls();
            }
        }

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            bool underMaintenance = (ConfigurationSettings.AppSettings["UnderMaintenance"] == "true" ? true : false);

            // If site under maintenance, don't show contact form.
            if (underMaintenance)
            {
                panelContactForm.Visible = false;
                panelMessageSent.Visible = false;
            }
            else
            {
                panelContactForm.Visible = true;
                panelMessageSent.Visible = false;

                // If user is logged in, get his email.
                if (Session["AdvertiserEmail"] != null)
                {
                    textEmail.Text = Session["AdvertiserEmail"].ToString();
                }
                else if (Session["PublisherEmail"] != null)
                {
                    textEmail.Text = Session["PublisherEmail"].ToString();
                }

                // Get subject.
                string subject = "";
                if (Request.QueryString["s"] != null)
                {
                    subject = Request.QueryString["s"].ToString();
                }

                // Fill subject list.

                listSubject.DataSource = DictionaryBLL.GetCrmSubjectList();
                listSubject.DataBind();
                listSubject.Items.Insert(0, new ListItem("Select subject", "-1"));

                if (listSubject.Items.FindByText(subject) != null)
                    listSubject.Items.FindByText(subject).Selected = true;

                string lengthFunction = "function isMaxLength(txtBox) {";
                    lengthFunction += " if(txtBox) { ";
                    lengthFunction += "     return ( txtBox.value.length <" + "1000" + ");";
                    lengthFunction += " }";
                    lengthFunction += "}";

                // Add message body max length restriction.
                textMessage.Attributes.Add("onkeypress", "return isMaxLength(this);");
                ClientScript.RegisterClientScriptBlock(
                    this.GetType(),
                    "txtLength",
                    lengthFunction, true);
            }
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
            string phone = textPhone.Text;
            int subjectId = Convert.ToInt16(listSubject.SelectedValue);
            string message = textMessage.Text;

            // Write new message to DB.
            int messageId = CrmBLL.Add(name,
                                       email,
                                       phone,
                                       subjectId,
                                       message);

            // Send notifier to administrator.
            Mail.SendNewRequestAdminMail(messageId, name, email, phone, subjectId, message);

            // TODO: Redirect to previous page.
            panelContactForm.Visible = false;
            panelMessageSent.Visible = true;
        }
    }
}
