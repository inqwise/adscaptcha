using System;

namespace Inqwise.AdsCaptcha.API
{
    public partial class HelpSlider : System.Web.UI.Page
    {
        string _LANG = null;
        string _DIR = "ltr";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["lang"] != null)
                _LANG = Request.QueryString["lang"].ToLower();

            switch (_LANG)
            {
                case "he":
                    langHE.Visible = true;
                    _DIR = "rtl";
                    break;
                case "fr":
                    langFR.Visible = true;
                    break;
                case "ru":
                    langRU.Visible = true;
                    break;
                case "nl":
                    langNL.Visible = true;
                    break;
                case "de":
                    langDE.Visible = true;
                    break;
                case "pt":
                    langPT.Visible = true;
                    break;
                case "es":
                    langES.Visible = true;
                    break;
                case "it":
                    langIT.Visible = true;
                    break;
                case "hi":
                    langHI.Visible = true;
                    break;
                case "el":
                    langEL.Visible = true;
                    break;
                case "tr":
                    langTR.Visible = true;
                    break;
                case "ro":
                    langRO.Visible = true;
                    break;
                case "en":
                default:
                    langEN.Visible = true;
                    break;
            }
            
            Body.Style.Add("direction", _DIR);
        }
    }
}