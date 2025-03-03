<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Publisher/PublisherAccount.Master" AutoEventWireup="true" CodeFile="GetCode.aspx.cs" Inherits="Inqwise.AdsCaptcha.Publisher.GetCode" %>
<%@ Register assembly="ComponentArt.Web.UI" namespace="ComponentArt.Web.UI" tagprefix="ComponentArt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">  
	<link type="text/css" rel="stylesheet" href="../Resources/Styles/shCore.css"/>
	<link type="text/css" rel="stylesheet" href="../Resources/Styles/shThemeDefault.css"/>
	
	<link href="<%=ConfigurationSettings.AppSettings["URL"]%>css/colorbox/colorbox.css" type="text/css" rel="stylesheet" />
    <script src="<%=ConfigurationSettings.AppSettings["URL"]%>js/colorbox.js" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
        $(function() {


            var tabTabs = $('#resourcesNavigation ul li a');
            tabTabs.bind('click', function() {

                tabTabs.removeClass('selected');
                $(this).addClass('selected');

                var ahref = $(this).attr("href");
                $.colorbox({ inline: true, href: $(ahref), width:"90%", height:"100%" });

                return false;
            });

        });
    </script>
    <style type="text/css">
    .pluginList li
    
    {
    	float: left;
    	width:150px;
    }

    
    .pluginList li a
    {
    	white-space:nowrap;
    	font-size:13px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Get Code
</asp:Content>
<asp:content ContentPlaceHolderID="MainContent" runat="server">
<div id="content"  class="container">
   <div class="inner-content">
            <br>
        <!--div id="breadCrambs">
            <asp:Label ID="labelNavigationPath" runat="server" />        
        </div-->
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>

            <h4>Website Information</h4>

            <div class="description">
            <table>
                <tr>
                
                    <td>
                     <table>
                    <tr>
                        <td class="CodeHeader">Captcha ID:</td>
                        <td style="padding-bottom:20px;">
                            <b><asp:Label ID="labelCaptchaID" runat="server"></asp:Label></b>
                            <br />
                            <span class="Explanation">
                            This id is unique to this specific Captcha.
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="CodeHeader">Public Key:</td>
                        <td style="padding-bottom:20px;">
                            <b><asp:Label ID="labelPublicKey" runat="server"></asp:Label></b>
                            <br />
                            <span class="Explanation">
                            Use the public key in the client code to receive any CAPTCHA of this website.
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="CodeHeader">Private Key:</td>
                        <td style="padding-bottom:20px;">
                            <b><asp:Label ID="labelPrivateKey" runat="server"></asp:Label></b>
                            <br />
                            <span class="Explanation">
                            Use the private key when validating any CAPTCHA of this website.
                            <br />
                            Be sure to keep it secret.
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="CodeHeader">Resources:</td>                    
                        <td style="padding-bottom:20px;">
                            Check out our <a href="../Resources.aspx" target="_blank">resources page</a>, to find out the method that fits your website.
                        </td>
                    </tr>
                </table>
                
                <p class="Explanation">
                Your keys are saved. You can return to this page and get them any time.<br />
                Make sure to copy & paste them correctly into your code.                
                </p>
                    </td></tr>
                    <tr>
                    <td id="resourcesNavigation" style="padding:10px;">
                     <h4>Plugins</h4>
                        <ul class="pluginList">
                            <li><a id="acsharp" href="#csharp">C# ASP.NET</a></li>
                            <li><a id="avb" href="#vb">VB ASP.NET</a></li>
                            <li><a id="aphp" href="#php">PHP</a></li>
                            <li><a id="aasp" href="#asp">ASP</a></li>
                            <li><a id="awordpress" href="#wordpress">WordPress Plugin</a></li>
                            <li><a id="ajoomla" href="#joomla">Joomla Plugin</a></li>
                            <li><a id="adrupal" href="#drupal">Drupal Plugin</a></li>
                            <li><a id="aphpBB" href="#phpBB">phpBB Plugin</a></li>
                            <li><a id="ajava" href="#java">Java</a></li>
                            <li><a id="apython" href="#python">Python</a></li>
                            <li><a id="aperl" href="#perl">Perl</a></li>
                            <li><a id="adotnetnuke" href="#dotnetnuke">DotNetNuke Plugin</a></li>
                        </ul>
                    </td>
                </tr>
            </table>
            
               
                
            </div>
            
            
                            
            <div id="buttonHolder">
                <asp:LinkButton id="buttonSubmit" runat="server" CssClass="btn" style="color:#FFFFFF;" OnClick="buttonSubmit_Click" CausesValidation="true" ValidationGroup="Form"><span>Got it!</span></asp:LinkButton>
            </div>
                    
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>

    </div>
    <!--Plugin contents-->
    <div style="display:none;">
    
                        <!-- C# ASP.NET -->
                        <div id="csharp" class="resourcesContent tab">
                            <div class="tabContent"> 
                                <h1>Inqwise Captcha for C#/ASP.NET</h1>
                                <h2>Inqwise Captcha C#/ASP.NET class</h2>
                                <p>
                                To use Inqwise Captcha with C#/ASP.NET, download <a href="../Download.aspx?file=csharp_api">Inqwise Captcha C#/ASP.NET class</a>, just to make things easier for you.<br />
                                Extract and save <i>AdsCaptchaAPI.cs</i> on your website directory.
                                </p>
                                
                                <p>
                                In order to use the C#/ASP.NET class, you'll need to add the class in your project/solution.
                                </p>

                                <h2>Display your CAPTCHA</h2>
                                <p>
                                Now you're ready to display your CAPTCHA.<br />
                                On your form page (.aspx), place a label where the Inqwise Captcha will be displayed:
                                <pre class="brush: html;">
                                    &lt;asp:Label ID="AdsCaptchaHolder" runat="server"&gt;&lt;/asp:Label&gt;
                                </pre>
                                On the server side (.aspx.cs), set your API keys and display the CAPTCHA:
                                <pre class="brush: c-sharp;">
int    CaptchaId  = your captcha id;    // Set your captcha id
string PublicKey  = "your public key";  // Set your public key
AdsCaptchaHolder.Text = AdsCaptchaAPI.CAPTCHA.GetCaptcha(CaptchaId, PublicKey);
                                </pre>
                                </p>

                                <h2>Validate your CAPTCHA</h2>
                                On your validation process, place this code:
                                <pre class="brush: c-sharp;">
int    CaptchaId  = your captcha id;    // Set your captcha id
string PrivateKey = "your private key"; // Set your private key
string ChallengeCode = Request.Form["adscaptcha_challenge_field"].ToString();
string UserResponse = Request.Form["adscaptcha_response_field"].ToString();
string RemoteAddress = Request.ServerVariables["REMOTE_ADDR"];

string result = AdsCaptchaAPI.CAPTCHA.ValidateCaptcha(CaptchaId, PrivateKey, ChallengeCode, UserResponse, RemoteAddress);

if (result == "true")
{
    // Corrent answer, continue with your submission process
}
else
{
    // Wrong answer, you may display a new Inqwise Captcha and add an error args 
}
                                </pre>
                                
                                <!--
                                <h2>Troubleshooting</h2>
                                <p>
                                For security reasones and multi-lingual support: Use UTF-8
                                </p>
                                -->

                                <div id="Div2">
                                    <a href="../Download.aspx?file=csharp_api" class="button" target="_blank"><span>Download C#/ASP.NET Class</span></a>
                                    <a href="../Download.aspx?file=csharp_example" class="button" target="_blank"><span>Download Example</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- VB ASP.NET -->
                        <div id="vb" class="resourcesContent tab">
                            <div class="tabContent"> 
                                <h1>Inqwise Captcha for VB.NET</h1>
                                <h2>Inqwise Captcha VB.NET class</h2>
                                <p>
                                To use Inqwise Captcha with VB.NET, download <a href="../Download.aspx?file=vb_api">Inqwise Captcha VB.NET class</a>, just to make things easier for you.<br />
                                Extract and save <i>AdsCaptchaAPI.vb</i> on your website directory.
                                </p>
                                
                                <p>
                                In order to use the VB.NET class, you'll need to add the class in your project/solution.
                                </p>

                                <h2>Display your CAPTCHA</h2>
                                <p>
                                Now you're ready to display your CAPTCHA.<br />
                                On your form page (.aspx), place a label where the Inqwise Captcha will be displayed:
                                <pre class="brush: xml;">
&lt;asp:Label ID="AdsCaptchaHolder" runat="server"&gt;&lt;/asp:Label&gt;
                                </pre>
                                On the server side (.aspx.vb), set your API keys and display the CAPTCHA:
                                <pre class="brush: vb;">
Dim CaptchaId As Integer = your captcha id    ' Set your captcha id
Dim PublicKey As String  = "your public key"  ' Set your public key
AdsCaptchaHolder.Text = AdsCaptchaAPI.GetCaptcha(CaptchaId, PublicKey)
                                </pre>
                                </p>

                                <h2>Validate your CAPTCHA</h2>
                                On your validation process, place this code:
                                <pre class="brush: vb;">
Dim CaptchaId As Integer = your captcha id     ' Set your captcha id
Dim PrivateKey As String = "your private key"  ' Set your private key
Dim ChallengeCode As String = Request.Form("adscaptcha_challenge_field").ToString()
Dim UserResponse As String  = Request.Form("adscaptcha_response_field").ToString()
Dim RemoteAddress As String = Request.ServerVariables("REMOTE_ADDR")

Dim result As String = AdsCaptchaAPI.ValidateCaptcha(CaptchaId, PrivateKey, ChallengeCode, UserResponse, RemoteAddress)

If (result = "true") Then
    ' Corrent answer, continue with your submission process
Else
    ' Wrong answer, you may display a new Inqwise Captcha and add an error args 
End If
                                </pre>
                                
                                <!--
                                <h2>Troubleshooting</h2>
                                <p>
                                For security reasones and multi-lingual support: Use UTF-8
                                </p>
                                -->

                                <div id="Div4">
                                    <a href="../Download.aspx?file=vb_api" class="button"  target="_blank"><span>Download VB.NET Class</span></a>
                                    <a href="../Download.aspx?file=vb_example" class="button"  target="_blank"><span>Download Example</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- PHP -->
                        <div id="php" class="resourcesContent tab">
                            <div class="tabContent">                               
                                <h1>Inqwise Captcha for PHP</h1>
                                <h2>Inqwise Captcha PHP library</h2>
                                <p>
                                To use Inqwise Captcha with PHP, download <a href="../Download.aspx?file=php_library">Inqwise Captcha PHP library</a>, just to make things easier for you.<br />
                                Extract and save <i>adscaptchalib.php</i> on your website directory.
                                </p>
                                
                                <p>
                                In order to use the PHP library, you'll need to include the library in the page/s which use it:
                                <pre class="brush: php;">
&lt;?php
require_once('adscaptchalib.php');
?&gt;
                                </pre>
                                </p>

                                <h2>Client Side - Display your CAPTCHA</h2>
                                <p>
                                Now you're ready to display your CAPTCHA.<br />
                                Place this code inside your &lt;form&gt; where the Inqwise Captcha will be placed:
                                <pre class="brush: php;">
$captchaId  = '';   // Set your captcha id here
$publicKey  = '';   // Set your public key here
echo GetCaptcha($captchaId, $publicKey);
                                </pre>
                                Don't forget to set your Captcha ID and public key values.
                                </p>

                                <h2>Server Side - Validate your CAPTCHA</h2>
                                On your validation process, place this code:
                                <pre class="brush: php;">
$captchaId  = '';   // Set your captcha id here
$privateKey = '';   // Set your private key here
$challengeValue = $_POST['adscaptcha_challenge_field'];
$responseValue  = $_POST['adscaptcha_response_field'];
$remoteAddress  = $_SERVER["REMOTE_ADDR"];

if ("true" == ValidateCaptcha($captchaId, $privateKey, $challengeValue, $responseValue, $remoteAddress))
{
    // Corrent answer, continue with your submission process
} else {
    // Wrong answer, you may display a new Inqwise Captcha and add an error args 
}
                                </pre>
                                
                                <h2>Troubleshooting</h2>
                                <p>
                                    <b>Q: Fatal error: require_once() [function.require]: Failed opening required 'adscaptchalib.php'</b><br />
                                    A: Make sure you set the currect path to the Inqwise Captcha's library file.
                                </p>
                                <p>
                                    <b>Q: Fatal error:  Call to undefined function mb_detect_encoding()</b><br />
                                    A: Make sure <a href="http://php.net/manual/en/book.mbstring.php" target="_blank">Multibyte String</a> is installed and enabled in your PHP server.
                                </p>
                                
                                <!--
                                <p>
                                Use UTF-8
                                </p>
                                -->
                                
                                <div id="Div6">
                                    <a href="../Download.aspx?file=php_library" class="button" target="_blank"><span>Download PHP Library</span></a>
                                    <a href="../Download.aspx?file=php_example" class="button" target="_blank"><span>Download Example</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Java -->
                        <div id="java" class="resourcesContent tab">
                            <div class="tabContent">
                                <h1>Inqwise Captcha for JAVA</h1>
                                <h2>Inqwise Captcha JAVA library</h2>
                                <p>
                                To use Inqwise Captcha with Java, download <a href="../Download.aspx?file=java_library">Inqwise Captcha Java library</a>, just to make things easier for you.<br />
                                Unzip and put the <i>adscaptcha.jar</i> in the classpath of your web application. For example, if you are using Tomcat to run JSP, you may copy the jar file to the <i>WEB-INF/lib/</i> directory.
                                </p>
                                
                                <p>
                                In order to use the Java library, you'll need to import the Inqwise Captcha classes. In JSP, add this line in the page where the Inqwise Captcha will be displayed:
                                <pre class="brush: java;">
&lt;@ page import="net.adscaptcha.AdsCaptchaAPI"&gt;
                                </pre>
                                </p>

                                <h2>Client Side - Display your CAPTCHA</h2>
                                <p>
                                Now you're ready to display your Inqwise Captcha.<br />
                                Place this code inside your &lt;form&gt; where the Inqwise Captcha will be placed:
                                <pre class="brush: java;">
final String captchaId  = "your_captcha_id";
final String publicKey  = "your_public_key";

AdsCaptchaAPI adscaptcha = new AdsCaptchaAPI();

out.print(adscaptcha.getCaptcha(captchaId, publicKey));
                                </pre>
                                Don't forget to set your Captcha ID and Public Key values.
                                </p>

                                <h2>Server Side - Validate your CAPTCHA</h2>
                                <p>
                                On your validation process, place this code:
                                <pre class="brush: java;">
final String captchaId  = "your_captcha_id";
final String privateKey = "your_private_key";
final String challengeValue = request.getParameter("adscaptcha_challenge_field");
final String responseValue = request.getParameter("adscaptcha_response_field");		
final String remoteAddress = request.getRemoteAddr();

AdsCaptchaAPI adscaptcha = new AdsCaptchaAPI();

String result = adscaptcha.validateCaptcha(captchaId, privateKey, challengeValue, responseValue, remoteAddress);

if (result.equalsIgnoreCase("true")) 
{
	// Corrent answer, continue with your submission process
} 
else 
{
	// Wrong answer, you may display a new Inqwise Captcha and add an error args
}
                                </pre>
                                Don't forget to set your Captcha ID and Private Key values.
                                </p>
                                
                                <div id="Div8">
                                    <a href="../Download.aspx?file=java_library" class="button" target="_blank"><span>Download Java Library</span></a>
                                    <a href="../Download.aspx?file=java_example" class="button" target="_blank"><span>Download Example</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- ASP -->
                        <div id="asp" class="resourcesContent tab">
                            <div class="tabContent">
                                <h1>Inqwise Captcha for ASP</h1>
                                <h2>Inqwise Captcha ASP library</h2>
                                <p>
                                To use Inqwise Captcha with ASP, download <a href="../Download.aspx?file=asp_library">Inqwise Captcha ASP library</a>, just to make things easier for you.<br />
                                Extract and save <i>adscaptchalib.asp</i> on your website directory.
                                </p>
                                
                                <p>
                                In order to use the ASP library, you'll need to include the library in the page/s which use it:
                                <pre class="brush: xml;">
&lt;!-- #include virtual="/your_path/adscaptchalib.asp" --&gt;
                                </pre>
                                Important: set the path according to your server path.
                                </p>

                                <h2>Client Side - Display your CAPTCHA</h2>
                                <p>
                                Now you're ready to display your CAPTCHA.<br />
                                Place this code inside your &lt;form&gt; where the Inqwise Captcha will be placed:
                                <pre class="brush: vb;">
captchaId = "your captcha id"  ' Set your captcha id
publicKey = "your public key"  ' Set your private key
Response.Write GetCaptcha(captchaId, publicKey)
                                </pre>
                                Don't forget to set your Captcha ID and public key values.
                                </p>

                                <h2>Server Side - Validate your CAPTCHA</h2>
                                <p>
                                On your validation process, place this code:
                                <pre class="brush: vb;">
captchaId     = "your captcha id"   ' Set your captcha id, same value as before
privateKey    = "your private key"  ' Set your private key
challengeCode = Request("adscaptcha_challenge_field")
userResponse  = Request("adscaptcha_response_field")
remoteAddress = Request.ServerVariables("REMOTE_ADDR")
IsCaptchaValid = ValidateCaptcha(captchaId, privateKey, challengeCode, userResponse, remoteAddress)

If IsCaptchaValid = "true" Then
    ' Corrent answer, continue with your submission process
Else
    ' Wrong answer, you may display a new Inqwise Captcha and add an error args
End If
                                </pre>
                                Don't forget to set your Captcha ID and Private Key values.
                                </p>
                                
                                <!--
                                <h2>Troubleshooting</h2>
                                <p>
                                    <b>Q: The include file '/path/adscaptchalib.asp' was not found</b><br />
                                    A: Make sure you set the currect library file path and name.
                                </p>
                                -->
                                <!--
                                <p>
                                Hebrew -> Encoding must be Windows-1255, ISO-8859-8, ISO-8859-8-I (<b>NOT</b> UTF-8)
                                </p>
                                -->
                                
                                <div id="Div10">
                                    <a href="../Download.aspx?file=asp_library" class="button" target="_blank"><span>Download ASP Library</span></a>
                                    <a href="../Download.aspx?file=asp_example" class="button" target="_blank"><span>Download Example</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- WordPress Plugin -->
                        <div id="wordpress" class="resourcesContent tab">
                            <div class="tabContent">
                                <h1>Inqwise Captcha for WordPress</h1>
                                
                                <h2>Install Plugin</h2>
                                <ul class="numericList">
                                    <li>Download our <a href="../Download.aspx?file=wordpress_plugin">WordPress plugin</a>.</li>
                                    <li>If you already have an installed Inqwise Captcha plugin, please deactivate & delete it first.</li>
                                    <li>On the administration panel, open <i>Appearance</i> > <i>Plugins</i> menu and click on <i>Add New</i>.</li>
                                    <li>Click on the <i>Upload</i> tab. Use the <i>browse</i> button to select the plugin zip file, then click <i>Install Now</i>.</li>
                                    <li>Activate the plugin.</li>
                                </ul>
                                
                                <h2>Configure Plugin</h2>
                                Open <i>Settings</i> > <i>Inqwise Captcha</i> to configure your CAPTCHA:
                                <ul>
                                    <li>Keys - Unique identifiers (Captcha ID, Public Key and Private Key).<br />After you enter your keys, test your CAPTCHA.</li>
                                    <li>You can choose whether to display an Inqwise CAPTCHA on the registration form and/or the comments form.</li>
                                    <li>If you want to use different CAPTCHAs on the registration form and/or the comments, you may enter a different Captcha ID.</li>
                                    <li>You can hide the CAPTCHA for registered users (by their permission level).</li>
                                    <li>Error argss - You can modify the error argss text according to your own language.</li>
                                </ul>
                                
                                <h2>That's it!</h2>
                                The Inqwise CAPTCHA is now set and displayed in your blog... Congratulations!
                                
                                <h2>Requirements</h2>
                                <ul>
                                    <li>WordPress 2.6+</li>
                                    <li>PHP 4.3+</li>
                                    <li>
                                        Your theme must have a &lt;?php do_action('comment_form', $post-&gt;ID); ?&gt; tag inside your comments.php form. Most themes do.
                                        <br />
                                        If not, in your comments.php file, put &lt;?php do_action('comment_form', $post-&gt;ID); ?&gt; before &lt;input name="submit"..&gt;.
                                    </li>
                                    <li><a href="http://php.net/manual/en/book.mbstring.php" target="_blank">Multibyte String</a> functions installed and enabled in your PHP server.</li>
                                    <li>For multilanguage support, use UTF-8 encoding.</li>
                                </ul>

                                <h2>Troubleshooting</h2>
                                <p>
                                    <b>Q: I can't see the CAPTCHA</b><br />
                                    A: By default, the CAPTCHA is hidden for registered users (like you, as Administrator). Make sure to uncheck the "Hide CAPTCHA for registered users" or logout before checking your CAPTCHA again.
                                </p>
                                <p>
                                    <b>Q: The CAPTCHA displays AFTER or ABOVE the submit button on the comment form</b><br />
                                    A: You can try to check the "Rearrange CAPTCHA's position on the comment form automatically" option and javascript will attempt to rearrange it for you. If it's not fixing the problem, edit your current theme comments.php file and locate the line: &lt;?php do_action('comment_form', $post->ID); ?&gt;. Move this line to BEFORE the comment textarea, uncheck the rearrange option and the problem should be fixed.
                                </p>
                                <!--
                                <p>
                                Use UTF-8
                                </p>
                                -->
                                
                                <p class="learnMore">
                                Learn more about our <a href="http://blog.Inqwise.com/2010/10/19/adscaptcha-your-free-captcha-plugin-for-wordpress/" target="_blank">captcha plugin for WordPress</a>.
                                </p>
                                
                                <div id="Div12">
                                    <a href="../Download.aspx?file=wordpress_plugin" class="button" target="_blank"><span>Download WordPress Plugin</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Joomla Plugin -->
                        <div id="joomla" class="resourcesContent tab">
                            <div class="tabContent">
                                <h1>Inqwise Captcha for Joomla</h1>

                                <h2>Install Plugin</h2>
                                <ul class="numericList">
                                    <li>Download our <a href="../Download.aspx?file=joomla_plugin">Joomla plugin</a>.</li>
                                    <li>On the administration panel, from the <i>Extensions</i> menu select <i>Install/Uninstall</i>.</li>
                                    <li>Under <i>Upload Package File</i>, click on <i>Choose File</i> and selected the downloaded plugin file. Than, click on <i>Upload File & Install</i>.</li>
                                    <li>From the <i>Extensions</i> menu select <i>Plugin Manager</i>.</li>
                                    <li>Click on <i>System - Inqwise Captcha</i> plugin (you can use the filter to find it quicker).</li>
                                    <li>Enter your captcha id, public and private keys.</li>
                                    <li>Click on <i>Save</i>.</li>
                                    <li>Check <i>System - Inqwise Captcha</i> plugin and click on <i>Enable</i>.</li>
                                </ul>
                                
                                <h2>That's it!</h2>
                                The Inqwise CAPTCHA is now set and displayed in your Joomla website... Congratulations!

                                <h2>Requirements</h2>
                                <ul>
                                    <li>Joomla! 1.5.x</li>
                                    <li>PHP 4.3+</li>
                                    <li><a href="http://php.net/manual/en/book.mbstring.php" target="_blank">Multibyte String</a> functions installed and enabled in your PHP server.</li>
                                </ul>

                                <div id="Div14">
                                    <a href="../Download.aspx?file=joomla_plugin" class="button" target="_blank"><span>Download Joomla Plugin</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Drupal Plugin -->
                        <div id="drupal" class="resourcesContent tab">
                            <div class="tabContent">
                                <h1>Inqwise Captcha for Drupal</h1>

                                <h2>Install Plugin</h2>
                                <ul class="numericList">
                                    <li>Download our Drupal plugin (<a href="../Download.aspx?file=drupal5_plugin">5.x</a> or <a href="../Download.aspx?file=drupal6_plugin">6.x</a>).</li>                                    
                                    <li>Upload the plugin to your Drupal server. Extract the files to your <i>modules</i> folder.</li>
                                    <li>Verify that the <a href="http://drupal.org/project/captcha" target="_blank">CAPTCHA module</a> is installed</li>
                                    <li>On your Drupal administration page go to <i>Site Building</i> > <i>Modules</i>.</li>
                                    <li>Under the <i>Spam Control</i> group, enable the <i>CAPTCHA</i> module and the <i>Inqwise Captcha</i> module.</li>
                                </ul>
                                
                                <h2>Configure Plugin</h2>
                                <ul>
                                    <li>Goto <i>User Management</i> > <i>CAPTCHA</i> > <i>Inqwise Captcha</i> tab and set your Inqwise Captcha keys.</li>
                                    <li>Goto <i>User Management</i> > <i>CAPTCHA</i>. Here you can choose the forms you want to use Inqwise Captcha by selecting Inqwise Captcha as the challenge type for each form you'd like to use Inqwise Captcha (e.g., select Inqwise Captcha for comment_form to enable Inqwise Captcha on comments).</li>
                                </ul>

                                <h2>That's it!</h2>
                                The Inqwise CAPTCHA is now set and displayed in your Drupal website... Congratulations!
            
                                <h2>Requirements</h2>
                                <ul>
                                    <li>Drupal 5.x / 6.x</li>
                                    <li><a href="http://drupal.org/project/captcha" target="_blank">CAPTCHA module</a></li>
                                    <li>PHP 4.3+</li>
                                    <li><a href="http://php.net/manual/en/book.mbstring.php" target="_blank">Multibyte String</a> functions installed and enabled in your PHP server.</li>
                                </ul>

                                <h2>Troubleshooting</h2>
                                <p>
                                    <b>Q: I can't see the CAPTCHA.</b><br />
                                    A: By default, the CAPTCHA is hidden for administrators. You can change it in your CAPTCHA options.
                                </p>
                                <p>
                                    <b>Q: The CAPTCHA's answer is not checked.</b><br />
                                    A: The CAPTCHA ignores administrators. Please re-check it with a "simple" user.
                                </p>
                                <p>
                                    <b>Q: How can I check my CAPTCHA?</b><br />
                                    A: On your Drupal administration page, go to <i>User Management</i> > <i>CAPTCHA</i> and click on the <i>Examples</i> tab.<br />
                                    It should display examples of all your enabled CAPTCHAs. Check if you see the AdsCaptcha example.<br />
                                    If an error args appears instead, please re-check your settings.
                                </p>

                                <div id="Div16">
                                    <a href="../Download.aspx?file=drupal6_plugin" class="button" target="_blank"><span>Download Drupal 6.x Plugin</span></a>
                                    <a href="../Download.aspx?file=drupal5_plugin" class="button" target="_blank"><span>Download Drupal 5.x Plugin</span></a>
                                </div>
                            </div>
                        </div>

                        <!-- phpBB Plugin -->
                        <div id="phpBB" class="resourcesContent tab">
                            <div class="tabContent">
                                <h1>Inqwise Captcha for phpBB 3.x</h1>
                                
                                <h2>Install Plugin</h2>
                                <ul class="numericList">
                                    <li>Download our <a href="../Download.aspx?file=phpbb3_plugin">phpBB3 plugin</a>.</li>
                                    <li>Unzip the adscaptcha_phpbb3.zip file, and upload the sub-directories into your root forum directory.</li>
                                    <li>On the administration control panel (ACP), under the <i>General</i> tab, select <i>Spambot countermeasures</i>.</li>
                                    <li>On the available plugins, select the <i>Inqwise Captcha</i> plugin, then click <i>Configure</i>.</li>
                                    <li>Enter your Captcha ID, Public and Private keys.</li>
                                    <li>Click on <i>Submit</i>.</li>
                                </ul>
                                
                                <h2>That's it!</h2>
                                The Inqwise CAPTCHA is now set and displayed in your phpBB forum... Congratulations!
                                
                                <h2>Requirements</h2>
                                <ul>
                                    <li>phpBB 3.x</li>
                                    <li>Styles: prosilver or subsilver2</li>
                                    <li>PHP 4.3+</li>
                                    <li><a href="http://php.net/manual/en/book.mbstring.php" target="_blank">Multibyte String</a> functions installed and enabled in your PHP server.</li>
                                </ul>

                                <div id="Div18">
                                    <a href="../Download.aspx?file=phpbb3_plugin" class="button" target="_blank"><span>Download phpBB 3.x Plugin</span></a>
                                </div>
                            </div>
                        </div>

                        <!-- Python -->
                        <div id="python" class="resourcesContent tab">
                            <div class="tabContent">
                                <h1>Inqwise Captcha for Python</h1>
                                Coming soon...
                            </div>
                        </div>

                        <!-- Perl -->
                        <div id="perl" class="resourcesContent tab">
                            <div class="tabContent">
                                <h1>Inqwise Captcha for Perl</h1>
                                Coming soon...
                            </div>
                        </div>

                        <!-- DotNetNuke Plugin -->
                        <div id="dotnetnuke" class="resourcesContent tab">
                            <div class="tabContent">
                                <h1>Inqwise Captcha for DotNetNuke</h1>
                                Coming soon...
                            </div>
                        </div>
    
    </div>
           <% 
        try
        {
            string env = System.Configuration.ConfigurationSettings.AppSettings["Environment"];

            if (env == "Prod")
            { 
            %>  
<!-- Google Code for Download Conversion Page -->
<script type="text/javascript">
/* <![CDATA[ */
var google_conversion_id = 994048181;
var google_conversion_language = "en";
var google_conversion_format = "3";
var google_conversion_color = "ffffff";
var google_conversion_label = "6ZdUCLO6yAMQtfH_2QM";
var google_conversion_value = 10;
/* ]]> */
</script>
<script type="text/javascript" src="http://www.googleadservices.com/pagead/conversion.js">
</script>
<noscript>
<div style="display:inline;">
<img height="1" width="1" style="border-style:none;" alt="" src="http://www.googleadservices.com/pagead/conversion/994048181/?value=10&amp;label=6ZdUCLO6yAMQtfH_2QM&amp;guid=ON&amp;script=0"/>
</div>
</noscript>

	            <% 
            }
        }
        catch { }
    %>
</asp:content>