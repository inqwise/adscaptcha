<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="AdsCaptcha.Master" AutoEventWireup="true" CodeFile="Download.aspx.cs" Inherits="Inqwise.AdsCaptcha.Download" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function getHead() {
            var a = document.getElementsByTagName("head");
            a = !a || a.length < 1 ? document.body : a[0];
            return a;
        }

        function autoRedirect(url) {
            var meta = document.createElement("meta");
            meta.setAttribute('http-equiv', 'refresh');
            meta.setAttribute('content', '3; url='+url);
            
            getHead().appendChild(meta);
        }
    </script>
</asp:Content>


<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <div class="warp">
        <div id="messageHolder">

<% 
string file = "";
string version = "";
string url = "";
string env = ConfigurationSettings.AppSettings["Environment"];
string srv = ConfigurationSettings.AppSettings["URL"] + "Resources/Downloads/";

// Get download file request.
if (!string.IsNullOrEmpty(Request.QueryString["file"]))
{
    file = Request.QueryString["file"].ToLower();
    version = Request.QueryString["ver"];
}

// Check file request.
switch (file)
{
    case "wordpress_plugin":
        url = srv + "WordPress/adscaptcha.zip";
        break;
        
    case "wordpress_plugin_lp_top":
    case "wordpress_plugin_lp_bottom":
        url = srv + "WordPress/adscaptcha.zip";

        %>
        <!-- Google AdWord for WordPress plugin -->
        <%
        if (env == "Prod")
        {
        %>
            <script type="text/javascript">
                /* <![CDATA[ */
                var google_conversion_id = 1005684884;
                var google_conversion_language = "en";
                var google_conversion_format = "2";
                var google_conversion_color = "ffffff";
                var google_conversion_label = "dyLDCJSL8gEQlJHG3wM";
                var google_conversion_value = 0;
                /* ]]> */
            </script>
            <script type="text/javascript" src="http://www.googleadservices.com/pagead/conversion.js"></script>
            <noscript>
                <div style="display:inline;">
                    <img height="1" width="1" style="border-style:none;" alt="" src="http://www.googleadservices.com/pagead/conversion/1005684884/?label=dyLDCJSL8gEQlJHG3wM&amp;guid=ON&amp;script=0"/>
                </div>
            </noscript>
        <%
        }     
        break;
    case "vb_example":
        url = srv + "VBNET/adscaptcha_vbnet.zip";
        break;
    case "vb_api":
        url = srv + "VBNET/AdsCaptchaAPI.zip";
        break;
    case "php_example":
        url = srv + "PHP/adscaptcha_php.zip";
        break;
    case "php_library":
        url = srv + "PHP/adscaptchalib.zip";
        break;
    case "joomla_plugin":
        url = srv + "Joomla/joomla-adscaptcha-1_5.zip";
        break;
    case "joomla_plugin_2_5":
        url = srv + "Joomla/joomla-adscaptcha-2_5.zip";
        break;
    case "java_example":
        url = srv + "Java/adscaptcha_java.zip";
        break;
    case "java_library":
        url = srv + "Java/adscaptchalib.zip";
        break;
    case "drupal5_plugin":
        url = srv + "Drupal/drupal5-adscaptcha.zip";
        break;
    case "drupal6_plugin":
        url = srv + "Drupal/drupal6-adscaptcha.zip";
        break;
    case "drupal7_plugin":
        url = srv + "Drupal/drupal7-adscaptcha.zip";
        break;
    case "phpbb3_plugin":
        url = srv + "phpBB/adscaptcha_phpbb3.zip";
        break;
    case "csharp_example":
        url = srv + "CSharp/adscaptcha_csharp.zip";
        break;
    case "csharp_api":
        url = srv + "CSharp/AdsCaptchaAPI.zip";
        break;
    case "asp_example":
        url = srv + "ASP/adscaptcha_asp.zip";
        break;
    case "asp_library":
        url = srv + "ASP/adscaptchalib.zip";
        break;
    case "python_module":
        url = srv + "Python/AdsCaptcha-1.0.tar.gz";
        break;
    case "perl_module":
        url = srv + "Perl/adsCAPTCHA-1.0.tar.gz";
        break;
    case "vbulletin_plugin":
        switch (version)
        {
            case "5":
                url = srv + "vBulletin/adscaptcha-vbulletin-5.x.zip";
                break;
            default:
                url = srv + "vBulletin/adscaptcha-vbulletin-4.x.zip";
                break;
        }
        break;
    case "ruby_package":
        url = srv + "Ruby/adscaptcha-0.0.1.tar.gz";
        break;
    case "ruby_sample":
        url = srv + "Ruby/sample.tar.gz";
        break;
    default:            
        break;
}

try
{
    if (string.IsNullOrEmpty(url))
    {
        throw new Exception("File not found.");
    }
    else
    {
        Response.Redirect(url);
        %>
            <b>Thank you for choosing AdsCaptcha!</b>
            <br />
            <br />
            <br />
            Your download will begin in a moment...
            <br />
            <br />
            <b>No download?</b> Check for your browser's security bar at the top of the page, or click <a href="<%=url%>">here</a>.
            <script type="text/javascript">
                autoRedirect('<%=url%>');
            </script>
        <%
    }
}
catch 
{ 
    %>
            Sorry, file not found.
    <%
}
%>       
            <br />
            <br />
            <br />
            For more details, visit our <a href="Resources.aspx">page</a>.
        </div>
    </div>

</asp:content>