using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Inqwise.AdsCaptcha.API.App_Code
{
    public class YBrantResponce
    {
        private string id;
        private string imgUrl;
        private string clickUrl;

        public string ID { get{return id;} set{id = value;}}
        public string ImgUrl { get { return imgUrl; } set { imgUrl = value; } }
        public string ClickUrl { get { return clickUrl; } set { clickUrl = value; } }

    }
}
