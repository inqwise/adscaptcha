<%@ Page EnableViewStateMac="false" EnableViewState="false" Language="C#" MasterPageFile="AdsCaptcha.Master" AutoEventWireup="true" CodeFile="Resources.aspx.cs" Inherits="Inqwise.AdsCaptcha.Resources" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">  
	
	<link type="text/css" rel="stylesheet" href="Resources/Styles/shCore.css"/>
	<link type="text/css" rel="stylesheet" href="Resources/Styles/shThemeDefault.css"/>
	
	
	<style type="text/css">
	h5 {
   		clear:both;
   		display:block;
   	}
	</style>

    <script type="text/javascript" charset="utf-8">
        $(function() {
            var tabContainers = $('#resourcesContent > .tab');
            var tabTabs = $('#resourcesNavigation ul a')
            tabContainers.hide().filter(':first').show();

            $('#resourcesNavigation ul a, a.link').click(function() {
                tabContainers.hide();
                tabContainers.filter(this.hash).show();
                tabTabs.removeClass('selected');
                tabTabs.filter(this.hash).addClass('selected');
                return false;
            }).filter(':first').click();

            var url = window.location.href;
            var index = url.indexOf("#");
            if (index >= 0) {
                var filterName = url.substring(index, url.length).trim();
                tabTabs.removeClass('selected').filter(filterName).addClass('selected');                
                tabContainers.hide().filter(filterName).show();
            }
        });
    </script>
    
</asp:Content>


<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginContent" runat="server">
	<div>
		<ul class="menu-top">
			<li><a href="aboutus.aspx" title="About Us">About Us</a></li>
			<li><a href="products.aspx" title="Products">Products</a></li>
		</ul>
	</div>
	<div class="menu-lobby-container">	
		<ul class="menu-lobby">
			<li><a href="advertiser/StartPage.aspx" title="Advertisers" class="button-green"><span>Advertisers</span></a></li>
			<li><a href="publisher/StartPage.aspx" title="Site Owners" class="button-green"><span>Site Owners</span></a></li>
		</ul>
	</div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
id="body" class="page"
</asp:Content>

<asp:content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




	<div class="content">
		<div class="breadcrumbs">
			<a href="/" title="Home">Home</a>&nbsp;&rsaquo;&nbsp;Resources
		</div>
		<div class="inner-wrapper">
			<div class="inner-content">
			
			
				<div class="page-heading-wrapper">
					<h1>Resources</h1>
				</div>


     

        <div class="resourcesTabs">
            <table width="100%">
                <tr>
                    <td id="resourcesNavigation">
                        <ul style="margin:0 20px 20px 0;">
                            <li><a id="overview" href="#overview">Overview</a></li>
                            <li><a id="display" href="#display">Display the CAPTCHA</a></li>
                            <li><a id="validate" href="#validate">Validate the CAPTCHA</a></li>
                            <li><a id="keys" href="#keys">Your API Keys</a></li>
                            <!--<li><a id="troubleshooting" href="#troubleshooting">Troubleshooting</a></li>-->
                        </ul>
                        
                        <h3>Plugins</h3>
                        <ul style="margin:0;">
                            <li><a id="csharp" href="#csharp">C# ASP.NET</a></li>
                            <li><a id="vb" href="#vb">VB ASP.NET</a></li>
                            <li><a id="php" href="#php">PHP</a></li>
                            <li><a id="asp" href="#asp">ASP</a></li>
                            <li><a id="wordpress" href="#wordpress">WordPress Plugin</a></li>
                            <li><a id="joomla" href="#joomla">Joomla Plugin</a></li>
                            <li><a id="drupal" href="#drupal">Drupal Plugin</a></li>
                            <li><a id="phpBB" href="#phpBB">phpBB Plugin</a></li>
                            <li><a id="vBulletin" href="#vBulletin">vBulletin Plugin</a></li>
                            <li><a id="java" href="#java">Java</a></li>
                            <li><a id="python" href="#python">Python</a></li>
                            <li><a id="perl" href="#perl">Perl</a></li>
                            <li><a id="ruby" href="#ruby">Ruby</a></li>
                            <li><a id="dotnetnuke" href="#dotnetnuke">DotNetNuke Plugin</a></li>
                        </ul>
                    </td>
                    <td id="resourcesContent">
                        <!-- Overview -->
                        <div id="overview" class="tab">
                            <div class="tabContent">
                                <h4>Inqwise API Overview</h4>
                                Welcome to our resources page! Here you'll find documentation and examples of how to embed an <b>Inqwise</b> in your websites.<br />
                                This documentation is designed for people who are familiar with HTML and server-side processing.<br />

                               <h5>API keys</h5>
                                In order to add <b>Inqwise</b> to your website, you'll need to get unique API keys.<br />
                                Using the API keys, we identify your website on each CAPTCHA request/validation.<br />
                                <a href="Publisher/SignUp.aspx">Sign up</a> now for your API keys. It's FREE!<br />
                                
                                <h5>Getting started</h5>
                                Once you've got your API keys, all you have to do are simply two steps:<br />
                                1. Display the <b>Inqwise</b> on your form (client-side).<br />
                                2. Validate the user's answer when form is being processed (server-side).<br />
                                <br />
                                We recommend you check out our plugins before doing it yourself.<br />
                                
                                <h5>How the Inqwise API works</h5>
                                <img src="Resources/adscaptcha_service.jpg" alt="Inqwise integration"/><br />
                                <ul style="list-style-type:decimal;">
                                    <li>The user loads your website form with the <b>Inqwise</b> widget (JavaScript code).</li>
                                    <li>The JavaScript code requests an <b>Inqwise</b> challenge. The user is exposed to the <b>Inqwise</b>.</li>
                                    <li>The user fills out your website form (includes <b>Inqwise</b> answer) and clicks submit.</li>
                                    <li>Your server sends the user's answer to <b>Inqwise</b>'s servers, which checks the user's answer and sends back a response.</li>
                                    <li>If correct, proceed with your process. If incorrect, display an error to the users and generate a new <b>Inqwise</b> challenge.</li>
                                </ul>
                                
                                <h5>Important connection issue</h5>
                                Make sure your server can establish a connection to <b>Inqwise</b>'s servers.<br />
                                Some application servers define outbound firewall restriction which could cause connectivity faults.<br />                                

                                <h5>Related posts</h5>
                                <a href="http://blog.Inqwise.com/category/interesting-captcha/">Interesting CAPTCHA</a><br />
                                <a href="http://blog.Inqwise.com/category/captcha-news/">CAPTCHA news</a>
                            </div>
                        </div>

                        <!-- Display -->
                        <div id="display" class="tab">
                            <div class="tabContent">
                                We recommend you check out our plugins before doing it yourself.<br />

                                <h4>Display the CAPTCHA</h4>
                                Displaying <b>Inqwise</b> on your website is <b>extremely simple!</b><br />
                                All you have to do is add this code in your &lt;form&gt; where you want it to be displayed.<br />
                                Replace <span class="blue">your_captcha_id</span> and <span class="blue">your_public_key</span> with your API codes.<br />
                                Replace <span class="blue">random_dummy</span> with a random number (generated in each load of your page).
                                <pre class="brush: html;">
&lt;script type='text/javascript'
    src='http://api.Inqwise.com/Get.aspx?CaptchaId=your_captcha_id&PublicKey=your_public_key&Dummy=random_dummy'&gt;&lt;/script&gt;
&lt;noscript&gt;
    &lt;iframe src='http://api.Inqwise.com/NoScript.aspx?CaptchaId=your_captcha_id&PublicKey=your_public_key&Dummy=random_dummy'
        width='300' height='100' frameborder='0'&gt;&lt;/iframe&gt;
    &lt;table&gt;
    &lt;tr&gt;&lt;td&gt;Type challenge here:&lt;/td&gt;&lt;td&gt;&lt;input type='text' name='adscaptcha_response_field' value='' /&gt;&lt;/td&gt;&lt;/tr&gt;
    &lt;tr&gt;&lt;td&gt;Paste code here:&lt;/td&gt;&lt;td&gt;&lt;input type='text' name='adscaptcha_challenge_field' value='' /&gt;&lt;/td&gt;&lt;/tr&gt;
    &lt;/table&gt;
&lt;/noscript&gt;
                                </pre>
                                <!--
                                <h5>Muli-language support</h5>
                                UTF-8?
                                -->
									
								<h4>For better User Experience</h4>
								<p>For better User Experience it is recommended to reload the CAPTCHA when negative response is received from "validate"</p>
								<pre style="width:824px;" class="brush: javascript;">
									var reloadCaptcha = function(params) {

										jQuery.ajax({
											url: "//api.Inqwise.com/slider/get.ashx?callback=?",
											data: {
												captchaId : params.captchaId,
											    publicKey : params.publicKey,
											    responseType : "json"
											},
											dataType: "jsonp",
											jsonp: "callback",
											success: function (data, b) {        
												if(Inqwise != undefined) {
											    	Inqwise.render(data);
											    }    	
											},
											error: function (a, b, c) {
												//
											}
										});
		
									};
		
									// Call this function when you need to reload captcha
									reloadCaptcha({
										captchaId : "your_captcha_id",
										publicKey : "your_public_key"
									});
								</pre>
									
									
                                <h5>Error codes</h5>
                                Most likely, an error occurred when the API keys weren't set correctly.<br />
                                Please check that you copied the exact key.<br />
                                Make sure you didn't get confused between your public key and your private key.<br />
                                <br />
                                Anyway, this is the full list of error codes:
                                <table cellpadding="0" cellspacing="0">
                                    <tr><th>Code</th><th>Description</th></tr>
                                    <tr><td class="errorCode">captchaid-not-set</td><td>You didn't set your Captcha ID. Please set it.</td></tr>
                                    <tr><td class="errorCode">captchaid-invalid</td><td>Your Captcha ID is invalid. Please check it again.</td></tr>
                                    <tr><td class="errorCode">publickey-not-set</td><td>You didn't set your public key. Please set it.</td></tr>
                                    <tr><td class="errorCode">publickey-invalid</td><td>Your public key is invalid. Please check it again.</td></tr>
                                    <tr><td class="errorCode">publickey-not-exists</td><td>We couldn't verify your public key. Please check it again.</td></tr>
                                    <tr><td class="errorCode">publickey-not-match-captchaid</td><td>Your Captcha ID and public key don't match. Please check them again.</td></tr>
                                    <!--<tr><td class="errorCode">invalid-captcha-type</td><td>API does not support this kind of CAPTCHA type. Check you CAPTCHA settings</td></tr>-->
                                    <tr><td class="errorCode">unexpected-error</td><td>An unexpected error occured.</td></tr>
                                </table>
                            </div>
                        </div>

                        <!-- Validate -->
                        <div id="validate" class="tab">
                            <div class="tabContent">
                                We offer a variety of plugins and code examples for different programming languages and platforms.<br />
                                We recommend you check out our plugins before doing it yourself.<br />
                                <h4>Validate the CAPTCHA</h4>
                                Validate your <b>Inqwise</b> on your server-side processing by doing a POST request to our servers.<br />
                                <h5>Send API request</h5>
                                <b>URL:</b> http://api.Inqwise.com/Validate.aspx
                                <table cellpadding="0" cellspacing="0">
                                    <tr><th>Parameter</th><th>Description</th></tr>
                                    <tr><td>CaptchaId</td><td>Your Captcha ID.</td></tr>
                                    <tr><td>PrivateKey</td><td>Your private key.</td></tr>
                                    <tr><td>ChallengeCode</td><td>Challenge code. The value of "adscaptcha_challenge_field" (form variable).</td></tr>
                                    <tr><td>UserResponse</td><td>User's response. The value of "adscaptcha_response_field" (form variable).</td></tr>
                                    <tr><td>RemoteAddress</td><td>User's IP address. Used for security reasons.</td></tr>
                                </table>                                                                
                                <h5>Get API response</h5>
                                The validation response is a string: "true" for correct answer and "false" for wrong answer.<br />
                                Otherwise, most likely the request parameters didn't set correctly. Please check them out.<br />
                                <br />
                                Anyway, this is list of response strings:
                                <table cellpadding="0" cellspacing="0">
                                    <tr><th>Return Value</th><th>Description</th></tr>
                                    <tr><td class="errorCode">true</td><td>User's answer was <b>correct</b>.</td></tr>
                                    <tr><td class="errorCode">false</td><td>User's answer was incorrect.</td></tr>
                                    <tr><td class="errorCode">already-checked</td><td>CAPTCHA challenge already checked before.</td></tr>
                                    <tr><td class="errorCode">captchaid-not-set</td><td>You didn't set your Captcha ID. Please set it.</td></tr>
                                    <tr><td class="errorCode">captchaid-invalid</td><td>Your Captcha ID is invalid. Please check it again.</td></tr>
                                    <tr><td class="errorCode">privatekey-not-set</td><td>You didn't set your private key. Please set it.</td></tr>
                                    <tr><td class="errorCode">privatekey-invalid</td><td>Your private key is invalid. Please check it again.</td></tr>
                                    <tr><td class="errorCode">challenge-not-set</td><td>You didn't send or we couldn't get the challenge code (POST field). Please set it.</td></tr>
                                    <tr><td class="errorCode">challenge-invalid</td><td>Challenge code (POST field) is invalid.</td></tr>
                                    <tr><td class="errorCode">response-not-set</td><td>You didn't send or we couldn't get user's response (POST field). Please set it.</td></tr>
                                    <tr><td class="errorCode">response-invalid</td><td>User's response (POST field) is invalid.</td></tr>
                                    <tr><td class="errorCode">remoteaddress-not-set</td><td>You didn't set the remote address. Remote address is needed for security reasons. Please set it.</td></tr>
                                    <tr><td class="errorCode">remoteaddress-invalid</td><td>Remote address is invalid. Please check it. Required format: 0.0.0.0.</td></tr>
                                    <tr><td class="errorCode">privatekey-not-exists</td><td>We couldn't verify your private key. Please check it again.</td></tr>
                                    <tr><td class="errorCode">privatekey-not-match-captchaid</td><td>Your Captcha ID and private key don't match. Please check them again.</td></tr>
                                    <tr><td class="errorCode">unexpected-error</td><td>An unexpected error occured.</td></tr>
                                </table>
                            </div>
                        </div>

                        <!-- Keys -->
                        <div id="keys" class="tab">
                            <div class="tabContent">
                                <h4>Your API Keys</h4>
                                In order to add <b>Inqwise</b> to your website, you'll need to get unique API keys.<br />
                                Using the API keys, we identify your website on each CAPTCHA request/validation.<br />
                                <a href="Publisher/SignUp.aspx">Sign up</a> now for your API keys. It's FREE!<br />                                
                            </div>
                        </div>

                        <!-- Troubleshooting -->
                        <!--
                        <div id="troubleshooting" class="tab">
                            <div class="tabContent">
                                <h4>Troubleshooting</h4>
                                &lt;meta http-equiv="Content-Type" content="text/html; charset=utf-8" /&gt;
                                <br />
                                accept-charset="UTF-8"
                            </div>
                        </div>
                        -->

                        <!-- C# ASP.NET -->
                        <div id="csharp" class="tab">
                            <div class="tabContent"> 
                                <h4><b>Inqwise</b> for C#/ASP.NET</h4>
                                <h5><b>Inqwise</b> C#/ASP.NET class</h5>
                                <p>
                                To use <b>Inqwise</b> with C#/ASP.NET, download <a href="Download.aspx?file=csharp_api"><b>Inqwise</b> C#/ASP.NET class</a>, just to make things easier for you.<br />
                                Extract and save <i>AdsCaptchaAPI.cs</i> on your website directory.
                                </p>
                                
                                <p>
                                In order to use the C#/ASP.NET class, you'll need to add the class in your project/solution.
                                </p>

                                <h5>Display your CAPTCHA</h5>
                                <p>
                                Now you're ready to display your CAPTCHA.<br />
                                On your form page (.aspx), place a label where the <b>Inqwise</b> will be displayed:
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

                                <h5>Validate your CAPTCHA</h5>
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
    // Wrong answer, you may display a new AdsCaptcha and add an error args 
}
                                </pre>
                                
                                <!--
                                <h5>Troubleshooting</h5>
                                <p>
                                For security reasones and multi-lingual support: Use UTF-8
                                </p>
                                -->

                                <div id="buttonHolder">
                                    <a href="Download.aspx?file=csharp_api" class="button" style="width:220px;"><span>Download C#/ASP.NET Class</span></a>
                                    <a href="Download.aspx?file=csharp_example" class="button"><span>Download Example</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- VB ASP.NET -->
                        <div id="vb" class="tab">
                            <div class="tabContent"> 
                                <h4><b>Inqwise</b> for VB.NET</h4>
                                <h5><b>Inqwise</b> VB.NET class</h5>
                                <p>
                                To use <b>Inqwise</b> with VB.NET, download <a href="Download.aspx?file=vb_api"><b>Inqwise</b> VB.NET class</a>, just to make things easier for you.<br />
                                Extract and save <i>AdsCaptchaAPI.vb</i> on your website directory.
                                </p>
                                
                                <p>
                                In order to use the VB.NET class, you'll need to add the class in your project/solution.
                                </p>

                                <h5>Display your CAPTCHA</h5>
                                <p>
                                Now you're ready to display your CAPTCHA.<br />
                                On your form page (.aspx), place a label where the <b>Inqwise</b> will be displayed:
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

                                <h5>Validate your CAPTCHA</h5>
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
    ' Wrong answer, you may display a new AdsCaptcha and add an error args 
End If
                                </pre>
                                
                                <!--
                                <h5>Troubleshooting</h5>
                                <p>
                                For security reasones and multi-lingual support: Use UTF-8
                                </p>
                                -->

                                <div id="buttonHolder">
                                    <a href="Download.aspx?file=vb_api" class="button"><span>Download VB.NET Class</span></a>
                                    <a href="Download.aspx?file=vb_example" class="button"><span>Download Example</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- PHP -->
                        <div id="php" class="tab">
                            <div class="tabContent">                               
                                <h4><b>Inqwise</b> for PHP</h4>
                                <h5><b>Inqwise</b> PHP library</h5>
                                <p>
                                To use <b>Inqwise</b> with PHP, download <a href="Download.aspx?file=php_library"><b>Inqwise</b> PHP library</a>, just to make things easier for you.<br />
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

                                <h5>Client Side - Display your CAPTCHA</h5>
                                <p>
                                Now you're ready to display your CAPTCHA.<br />
                                Place this code inside your &lt;form&gt; where the Inqwise will be placed:
                                <pre class="brush: php;">
$captchaId  = '';   // Set your captcha id here
$publicKey  = '';   // Set your public key here
echo GetCaptcha($captchaId, $publicKey);
                                </pre>
                                Don't forget to set your Captcha ID and public key values.
                                </p>

                                <h5>Server Side - Validate your CAPTCHA</h5>
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
    // Wrong answer, you may display a new AdsCaptcha and add an error args 
}
                                </pre>
                                
                                <h5>Troubleshooting</h5>
                                <p>
                                    <b>Q: Fatal error: require_once() [function.require]: Failed opening required 'adscaptchalib.php'</b><br />
                                    A: Make sure you set the currect path to the <b>Inqwise</b>'s library file.
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
                                
                                <div id="buttonHolder">
                                    <a href="Download.aspx?file=php_library" class="button"><span>Download PHP Library</span></a>
                                    <a href="Download.aspx?file=php_example" class="button"><span>Download Example</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Java -->
                        <div id="java" class="tab">
                            <div class="tabContent">
                                <h4><b>Inqwise</b> for JAVA</h4>
                                <h5><b>Inqwise</b> JAVA library</h5>
                                <p>
                                To use <b>Inqwise</b> with Java, download <a href="Download.aspx?file=java_library"><b>Inqwise</b> Java library</a>, just to make things easier for you.<br />
                                Unzip and put the <i>adscaptcha.jar</i> in the classpath of your web application. For example, if you are using Tomcat to run JSP, you may copy the jar file to the <i>WEB-INF/lib/</i> directory.
                                </p>
                                
                                <p>
                                In order to use the Java library, you'll need to import the <b>Inqwise</b> classes. In JSP, add this line in the page where the <b>Inqwise</b> will be displayed:
                                <pre class="brush: java;">
&lt;@ page import="net.adscaptcha.AdsCaptchaAPI"&gt;
                                </pre>
                                </p>

                                <h5>Client Side - Display your CAPTCHA</h5>
                                <p>
                                Now you're ready to display your <b>Inqwise</b>.<br />
                                Place this code inside your &lt;form&gt; where the Inqwise will be placed:
                                <pre class="brush: java;">
final String captchaId  = "your_captcha_id";
final String publicKey  = "your_public_key";

AdsCaptchaAPI adscaptcha = new AdsCaptchaAPI();

out.print(adscaptcha.getCaptcha(captchaId, publicKey));
                                </pre>
                                Don't forget to set your Captcha ID and Public Key values.
                                </p>

                                <h5>Server Side - Validate your CAPTCHA</h5>
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
	// Wrong answer, you may display a new Inqwise and add an error args
}
                                </pre>
                                Don't forget to set your Captcha ID and Private Key values.
                                </p>
                                
                                <div id="buttonHolder">
                                    <a href="Download.aspx?file=java_library" class="button"><span>Download Java Library</span></a>
                                    <a href="Download.aspx?file=java_example" class="button"><span>Download Example</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- ASP -->
                        <div id="asp" class="tab">
                            <div class="tabContent">
                                <h4><b>Inqwise</b> for ASP</h4>
                                <h5><b>Inqwise</b> ASP library</h5>
                                <p>
                                To use <b>Inqwise</b> with ASP, download <a href="Download.aspx?file=asp_library"><b>Inqwise</b> ASP library</a>, just to make things easier for you.<br />
                                Extract and save <i>adscaptchalib.asp</i> on your website directory.
                                </p>
                                
                                <p>
                                In order to use the ASP library, you'll need to include the library in the page/s which use it:
                                <pre class="brush: xml;">
&lt;!-- #include virtual="/your_path/adscaptchalib.asp" --&gt;
                                </pre>
                                Important: set the path according to your server path.
                                </p>

                                <h5>Client Side - Display your CAPTCHA</h5>
                                <p>
                                Now you're ready to display your CAPTCHA.<br />
                                Place this code inside your &lt;form&gt; where the Inqwise will be placed:
                                <pre class="brush: vb;">
captchaId = "your captcha id"  ' Set your captcha id
publicKey = "your public key"  ' Set your private key
Response.Write GetCaptcha(captchaId, publicKey)
                                </pre>
                                Don't forget to set your Captcha ID and public key values.
                                </p>

                                <h5>Server Side - Validate your CAPTCHA</h5>
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
    ' Wrong answer, you may display a new Inqwise and add an error args
End If
                                </pre>
                                Don't forget to set your Captcha ID and Private Key values.
                                </p>
                                
                                <!--
                                <h5>Troubleshooting</h5>
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
                                
                                <div id="buttonHolder">
                                    <a href="Download.aspx?file=asp_library" class="button"><span>Download ASP Library</span></a>
                                    <a href="Download.aspx?file=asp_example" class="button"><span>Download Example</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- WordPress Plugin -->
                        <div id="wordpress" class="tab">
                            <div class="tabContent">
                                <h4><b>Inqwise</b> for WordPress</h4>
                                
                                <h5>Install Plugin</h5>
                                <ul class="numericList">
                                    <li>Download our <a href="Download.aspx?file=wordpress_plugin">WordPress plugin</a>.</li>
                                    <li>If you already have an installed <b>Inqwise</b> plugin, please deactivate & delete it first.</li>
                                    <li>On the administration panel, open <i>Appearance</i> > <i>Plugins</i> menu and click on <i>Add New</i>.</li>
                                    <li>Click on the <i>Upload</i> tab. Use the <i>browse</i> button to select the plugin zip file, then click <i>Install Now</i>.</li>
                                    <li>Activate the plugin.</li>
                                </ul>
                                
                                <h5>Configure Plugin</h5>
                                Open <i>Settings</i> > <i><b>Inqwise</b></i> to configure your CAPTCHA:
                                <ul>
                                    <li>Keys - Unique identifiers (Captcha ID, Public Key and Private Key).<br />After you enter your keys, test your CAPTCHA.</li>
                                    <li>You can choose whether to display an <b>Inqwise</b> on the registration form and/or the comments form.</li>
                                    <li>If you want to use different CAPTCHAs on the registration form and/or the comments, you may enter a different Captcha ID.</li>
                                    <li>You can hide the CAPTCHA for registered users (by their permission level).</li>
                                    <li>Error argss - You can modify the error argss text according to your own language.</li>
                                </ul>
                                
                                <h5>That's it!</h5>
                                The <b>Inqwise</b> is now set and displayed in your blog... Congratulations!
                                
                                <h5>Requirements</h5>
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

                                <h5>Troubleshooting</h5>
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
                                
                                <div id="buttonHolder">
                                    <a href="Download.aspx?file=wordpress_plugin" class="button" style="width:220px;"><span>Download WordPress Plugin</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Joomla Plugin -->
                        <div id="joomla" class="tab">
                            <div class="tabContent">
                                <h4><b>Inqwise</b> for Joomla</h4>

                                <h5>Install 1.5.x Plugin</h5>
                                <ul class="numericList">
                                    <li>Download our <a href="Download.aspx?file=joomla_plugin">Joomla 1.5.x plugin</a>.</li>
                                    <li>On the administration panel, from the <i>Extensions</i> menu select <i>Install/Uninstall</i>.</li>
                                    <li>Under <i>Upload Package File</i>, click on <i>Choose File</i> and selected the downloaded plugin file. Than, click on <i>Upload File & Install</i>.</li>
                                    <li>From the <i>Extensions</i> menu select <i>Plugin Manager</i>.</li>
                                    <li>Click on <i>System - <b>Inqwise</b></i> plugin (you can use the filter to find it quicker).</li>
                                    <li>Enter your captcha id, public and private keys.</li>
                                    <li>Click on <i>Save</i>.</li>
                                    <li>Check <i>System - <b>Inqwise</b></i> plugin and click on <i>Enable</i>.</li>
                                </ul>
                                <h5>Requirements for 1.5.x</h5>
                                <ul>
                                    <li>Joomla! 1.5.x</li>
                                    <li>PHP 4.3+</li>
                                    <li><a href="http://php.net/manual/en/book.mbstring.php" target="_blank">Multibyte String</a> functions installed and enabled in your PHP server.</li>
                                </ul>
                                <h5>Install 2.5.x Plugin</h5>
                                <ul class="numericList">
                                    <li>Download our <a href="Download.aspx?file=joomla_plugin">Joomla 2.5.x plugin</a>.</li>
                                    <li>On the administration panel, from the Extensions menu select Extension Manager.</li>
                                    <li>In the Install tab, Under Upload Package File, click on Choose File and select the downloaded plugin file. Than, click on Upload & Install.</li>
                                    <li>From the Extensions menu select Plug-in Manager.</li>
                                    <li>Click on Captcha - AdsCaptcha plugin (you can use the filter to find it quicker).</li>
                                    <li>Set your AdsCaptcha keys (captcha Id, public and private keys), change status to Enabled and click on Save & Close.</li>
                                </ul>
                                <h5>How to use</h5>
                                <ul>
                                    <li>On the administration panel, from the Site menu select Global Configuration.</li>
                                    <li>Under Default Captcha attribute, select Captcha - AdsCaptcha.</li>
                                </ul>
                                <h5>Requirements for 2.5.x</h5>
                                <ul>
                                    <li>Joomla! 2.5.x</li>
                                    <li>PHP 4.3+</li>
                                    <li><a href="http://php.net/manual/en/book.mbstring.php" target="_blank">Multibyte String</a> functions installed and enabled in your PHP server.</li>
                                </ul>
                                
                                <h5>That's it!</h5>
                                The <b>Inqwise</b> is now set and displayed in your Joomla website... Congratulations!



                                <div id="buttonHolder">
                                    <a href="Download.aspx?file=joomla_plugin" class="button" style="width:220px;"><span>Download Joomla 1.5.x Plugin</span></a>
                                    <a href="Download.aspx?file=joomla_plugin_2_5" class="button" style="width:220px;"><span>Download Joomla 2.5.x Plugin</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Drupal Plugin -->
                        <div id="drupal" class="tab">
                            <div class="tabContent">
                                <h4><b>Inqwise</b> for Drupal</h4>

                                <h5>Install Plugin</h5>
                                <ul class="numericList">
                                    <li>Download our Drupal plugin (<a href="Download.aspx?file=drupal5_plugin">5.x</a> or <a href="Download.aspx?file=drupal6_plugin">6.x</a> or <a href="Download.aspx?file=drupal7_plugin">7.x</a>).</li>                                    
                                    <li>Upload the plugin to your Drupal server. Extract the files to your <i>modules</i> folder.</li>
                                    <li>Verify that the <a href="http://drupal.org/project/captcha" target="_blank">CAPTCHA module</a> is installed</li>
                                    <li>On your Drupal administration page go to <i>Site Building</i> > <i>Modules</i>.</li>
                                    <li>Under the <i>Spam Control</i> group, enable the <i>CAPTCHA</i> module and the <i><b>Inqwise</b></i> module.</li>
                                </ul>
                                
                                <h5>Configure Plugin</h5>
                                <ul>
                                    <li>Goto <i>User Management</i> > <i>CAPTCHA</i> > <i><b>Inqwise</b></i> tab and set your <b>Inqwise</b> keys.</li>
                                    <li>Goto <i>User Management</i> > <i>CAPTCHA</i>. Here you can choose the forms you want to use <b>Inqwise</b> by selecting <b>Inqwise</b> as the challenge type for each form you'd like to use <b>Inqwise</b> (e.g., select <b>Inqwise</b> for comment_form to enable <b>Inqwise</b> on comments).</li>
                                </ul>

                                <h5>That's it!</h5>
                                The <b>Inqwise</b> is now set and displayed in your Drupal website... Congratulations!
            
                                <h5>Requirements</h5>
                                <ul>
                                    <li>Drupal 5.x / 6.x / 7.x</li>
                                    <li><a href="http://drupal.org/project/captcha" target="_blank">CAPTCHA module</a></li>
                                    <li>PHP 4.3+</li>
                                    <li><a href="http://php.net/manual/en/book.mbstring.php" target="_blank">Multibyte String</a> functions installed and enabled in your PHP server.</li>
                                </ul>

                                <h5>Troubleshooting</h5>
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
                                    It should display examples of all your enabled CAPTCHAs. Check if you see the <b>Inqwise</b> example.<br />
                                    If an error args appears instead, please re-check your settings.
                                </p>

                                <div id="buttonHolder">
                                    <a href="Download.aspx?file=drupal6_plugin" class="button" style="width:220px;"><span>Download Drupal 6.x Plugin</span></a>
                                    <a href="Download.aspx?file=drupal5_plugin" class="button" style="width:220px;"><span>Download Drupal 5.x Plugin</span></a>
                                    <a href="Download.aspx?file=drupal7_plugin" class="button" style="width:220px;"><span>Download Drupal 7.x Plugin</span></a>
                                </div>
                            </div>
                        </div>

                        <!-- phpBB Plugin -->
                        <div id="phpBB" class="tab">
                            <div class="tabContent">
                                <h4><b>Inqwise</b> for phpBB 3.x</h4>
                                
                                <h5>Install Plugin</h5>
                                <ul class="numericList">
                                    <li>Download our <a href="Download.aspx?file=phpbb3_plugin">phpBB3 plugin</a>.</li>
                                    <li>Unzip the adscaptcha_phpbb3.zip file, and upload the sub-directories into your root forum directory.</li>
                                    <li>On the administration control panel (ACP), under the <i>General</i> tab, select <i>Spambot countermeasures</i>.</li>
                                    <li>On the available plugins, select the <i><b>Inqwise</b></i> plugin, then click <i>Configure</i>.</li>
                                    <li>Enter your Captcha ID, Public and Private keys.</li>
                                    <li>Click on <i>Submit</i>.</li>
                                </ul>
                                
                                <h5>That's it!</h5>
                                The <b>Inqwise</b> is now set and displayed in your phpBB forum... Congratulations!
                                
                                <h5>Requirements</h5>
                                <ul>
                                    <li>phpBB 3.x</li>
                                    <li>Styles: prosilver or subsilver2</li>
                                    <li>PHP 4.3+</li>
                                    <li><a href="http://php.net/manual/en/book.mbstring.php" target="_blank">Multibyte String</a> functions installed and enabled in your PHP server.</li>
                                </ul>

                                <div id="buttonHolder">
                                    <a href="Download.aspx?file=phpbb3_plugin" class="button" style="width:220px;"><span>Download phpBB 3.x Plugin</span></a>
                                </div>
                            </div>
                        </div>
                        
                       <!-- vBulletin Plugin -->
                        <div id="vBulletin" class="tab">
                            <div class="tabContent">
                                <h4><b>Inqwise</b> for vBulletin</h4>
                                
                                <h5>Install Plugin</h5>
                                <ul class="numericList">
                                    <li>Download our <a href="Download.aspx?file=vBulletin_plugin">vBulletin 4.x plugin</a> or <a href="Download.aspx?file=vBulletin_plugin&ver=5">vBulletin 5.x plugin</a>.</li>
                                    <li>Unzip it and copy adscaptchalib.php and class_humanverify_adscaptcha.php to your vBulletin /includes/ directory.</li>
                                    <li>Open your vBulletin Administrator Control Panel (ACP).</li>
                                    <li>Open Manage Products under Plugins &amp; Products from the navigation menu.</li>
                                    <li>Click Add/Import Product and import product-adscaptcha.xml.</li>
                                </ul>
                                
                                <h5>Configure Plugin</h5>
                               <ul>
                                    <li>Open Human Verification Manager under Settings from the navigation menu.</li>
                                    <li>Choose AdsCaptcha and save.</li>
                                    <li>Set your AdsCaptcha keys and save.</li>
                                </ul>
                                
                                <h5>That's it!</h5>
                                <b>Inqwise</b>'s captcha is now set and displayed in your vBulleting registration form... Congratulations!
                                
                                <h5>Requirements</h5>
                                <ul>
                                    <li>vBulleting 4.x. or vBulleting 5.x</li>
                                    <li>PHP 4.3+</li>
                                    <li>For multilanguage support, use UTF-8 encoding.</li>

                                </ul>

                                <div id="buttonHolder">
                                    <a href="Download.aspx?file=vBulletin_plugin" class="button" style="width:220px;"><span>Download vBulletin 4.x Plugin</span></a> or <a href="Download.aspx?file=vBulletin_plugin&ver=5" class="button" style="width:220px;"><span>Download vBulletin 5.x Plugin</span></a>
                                </div>
                            </div>
                        </div>

                        <!-- Ptyhon -->
                        <div id="python" class="tab">
                            <div class="tabContent">
                                <h4><b>Inqwise</b> for Python</h4>
                                 <h5>Install Module</h5>
                                <ul class="numericList">
                                    <li>Download our <a href="Download.aspx?file=python_module">Python module</a>.</li>
                                    <li>Extract the archive: tar zxvf AdsCaptcha-1.0.tar.gz</li>
                                    <li>Change directory: cd AdsCaptcha-1.0 </li>
                                    <li>Build the module: python setup.py build </li>
                                    <li>Install the module: python setup.py install </li>
                                    <li>Use the module: from adscaptcha import captcha | captcha.get_html(); | captcha.validate();</li>
                                </ul>
                                
                                <h5>That's it!</h5>
                                The <b>Inqwise</b> is now set and displayed in your forum... Congratulations!
                                <br />
                                See README.txt for instructions and docs/example.py for sample code with usage
                                <!--h5>Requirements</h5>
                                <ul>
                                    <li>phpBB 3.x</li>
                                    <li>Styles: prosilver or subsilver2</li>
                                    <li>PHP 4.3+</li>
                                    <li><a href="http://php.net/manual/en/book.mbstring.php" target="_blank">Multibyte String</a> functions installed and enabled in your PHP server.</li>
                                </ul-->

                                <div id="buttonHolder">
                                    <a href="Download.aspx?file=python_module" class="button" style="width:220px;"><span>Download Python module</span></a>
                                </div>
                            </div>
                        </div>

                        <!-- Perl -->
                        <div id="perl" class="tab">
                            <div class="tabContent">
                                <h4><b>Inqwise</b> for Perl</h4>
                                <h5>Install Module</h5>
                                <ul class="numericList">
                                    <li>Download our <a href="Download.aspx?file=perl_module">Perl module</a>.</li>
                                    <li>Extract the package: tar zxvf Captcha-adsCAPTCHA-1.0.tar.gz</li>
                                    <li>Change directory: cd Captcha-adsCAPTCHA-1.0 </li>
                                    <li>Run the Makefile.PL: perl Makefile.PL </li>
                                    <li>Build the package: make </li>
                                    <li>Test the package: make test </li>
                                    <li>Install the package: make install </li>
                                    <li>See the man page for package use information: man Captcha::adsCAPTCHA</li>
                                </ul>
                                
                                <h5>That's it!</h5>
                                The <b>Inqwise</b> is now set and displayed in your forum... Congratulations!
                                <br />
                                An example CGI script using this module can be found in the examples folder: Captcha-adsCAPTCHA-1.0/examples/captcha.pl
                                <div id="buttonHolder">
                                    <a href="Download.aspx?file=perl_module" class="button"><span>Download Perl module</span></a>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Ruby -->
                        <div id="ruby" class="tab">
                            <div class="tabContent">
                                <h4><b>Inqwise</b> for Ruby</h4>
                                <h5>Install package</h5>
                                <ul class="numericList">
                                    <li>Download our <a href="Download.aspx?file=ruby_package">Ruby package</a>.</li>
                                    <li>Extract the package: tar zxvf adscaptcha-0.0.1.tar.gz</li>
                                    <li>Change directory: cd adscaptcha-0.0.1 </li>
                                    <li>Build the gem module: gem build gem build adscaptcha.gemspec </li>
                                    <li>Install the gem: gem install adscaptcha-0.0.1.gem </li>
                                </ul>
                                
                                <h5>Test the package</h5>
                                <ul class="numericList">
                                    <li>Download our <a href="Download.aspx?file=ruby_sample">Ruby sample</a>.</li>
                                    <li>Extract the sample: tar zxvf sample.tar.gz</li>
                                    <li>Change directory: cd sample </li>
                                    <li>Start the rails server: ./script/server </li>
                                    <li>Take a browser and go to: http://localhost:3000 </li>
                                </ul>
                                
                                <h5>That's it!</h5>
                                The <b>Inqwise</b> is now set and displayed in your forum... Congratulations!
                                <br />
                 
                                <h5>Requirements</h5>
                                <ul>
                                    <li>ruby 1.8 and rails</li>
                                </ul>

                                <div id="buttonHolder">
                                    <a href="Download.aspx?file=ruby_package" style="width:220px;" class="button"><span>Download Ruby Package</span></a>
                                </div>
                            </div>
                        </div>

                        <!-- DotNetNuke Plugin -->
                        <div id="dotnetnuke" class="tab">
                            <div class="tabContent">
                                <h4><b>Inqwise</b> for DotNetNuke</h4>
                                Coming soon...
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    
        
        
        
        


			</div>
		</div>
   </div>


    <script type="text/javascript" src="Resources/scripts/shCore.js"></script>
    <script type="text/javascript" src="Resources/scripts/shBrushCSharp.js"></script>
	<script src="Resources/scripts/shBrushJScript.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript" src="Resources/scripts/shBrushPhp.js"></script>
    <script type="text/javascript" src="Resources/scripts/shBrushVb.js"></script>
    <script type="text/javascript" src="Resources/scripts/shBrushJava.js"></script>
    <script type="text/javascript" src="Resources/scripts/shBrushXml.js"></script>
    <script type="text/javascript">
        SyntaxHighlighter.config.clipboardSwf = 'Resources/scripts/clipboard.swf';
        SyntaxHighlighter.all();
    </script>
    
    
    
</asp:content>