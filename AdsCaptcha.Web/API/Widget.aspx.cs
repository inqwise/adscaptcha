using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.API
{
    public partial class Widget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            const int DFAULT_IMAGE_HEIGHT = 30;

            int width = 180;
            string slogan = "AdsCaptcha";
            bool rtl = false;
            string colorBack = "FFFFFF";
            string colorText = "000000";

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            try
            {
                slogan = HttpUtility.HtmlEncode(Request.QueryString["s"]).ToString();
            } catch {}

            try
            {
                width = int.Parse(HttpUtility.HtmlEncode(Request.QueryString["w"]));
            } catch {}

            try
            {
                colorBack = HttpUtility.HtmlEncode(Request.QueryString["bc"]);
            }
            catch { }

            try
            {
                colorText = HttpUtility.HtmlEncode(Request.QueryString["tc"]);
            }
            catch { }

            try
            {
                string dir = HttpUtility.HtmlEncode(Request.QueryString["d"]);
                if (dir.ToUpper() == "RTL")
                    rtl = true;
            }
            catch {}

            colorBack = "#" + colorBack;
            colorText = "#" + colorText;

            try
            {
                // warp - distort - ellipse - arc - beizer - curve
                int[] severity = new int[] { 2, 2, 4, 0, 0, 0 };

                ChallengeHandler challenge = new ChallengeHandler();

                // Write the image to the response stream.
                challenge.GenerateImage(slogan, width, DFAULT_IMAGE_HEIGHT, severity, colorBack, colorText, rtl).Save(this.Response.OutputStream, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                NLogManager.logger.TraceException("Widget challange preview error", ex);

                if (width < 180) width = 180;

                // Generate "empty" image.
                Bitmap b = new Bitmap(width, DFAULT_IMAGE_HEIGHT);                
                Graphics g = Graphics.FromImage(b);

                // Fill background.
                Brush white = new SolidBrush(Color.White);
                g.FillRectangle(white, 0, 0, width, DFAULT_IMAGE_HEIGHT);
                
                // Draw text.
                Font f = new Font("Arial", 10);
                Brush black = new SolidBrush(Color.Black);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                g.DrawString("AdsCaptcha", f, black, width / 2, DFAULT_IMAGE_HEIGHT / 2, format);

                // Write the image to the response stream.
                b.Save(this.Response.OutputStream, ImageFormat.Jpeg);
            }
        }
    }
}
