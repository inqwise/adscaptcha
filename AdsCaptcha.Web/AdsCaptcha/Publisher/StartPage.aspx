<%@ Page EnableViewStateMac="false" Title="Site Owners | Inqwise" Language="C#" MasterPageFile="~/AdsCaptcha.Master" AutoEventWireup="true" CodeFile="StartPage.aspx.cs" Inherits="Inqwise.AdsCaptcha.Publisher.StartPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
id="body" class="page"
</asp:Content> 

<asp:Content ID="Content2" ContentPlaceHolderID="LoginContent" runat="server">
    <div>
		<ul class="menu-top">
			<li><a href="/aboutus.aspx" title="About Us">About Us</a></li>
			<li><a href="/products.aspx" title="Products">Products</a></li>
			<li><a id="loginBox" href="#login-box" class="login-window">SIGN IN</a></li>
			<li><a id="signBox" href="SignUp.aspx">SIGN UP</a></li>
		</ul>
	</div>
	<div class="menu-lobby-container">	
		<ul class="menu-lobby">
			<li><a href="/advertiser/StartPage.aspx" title="Advertisers" class="button-green"><span>Advertisers</span></a></li>
			<li><a href="StartPage.aspx" title="Site Owners" class="button-green"><span>Site Owners</span></a></li>
		</ul>
	</div>        
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="LoginScriptContent" runat="server"> 
  <div id="login-box" class="login-popup">
            <div class="signin">
                <fieldset class="textbox">
    	<label class="username">
        <span>Email</span>
        <asp:textbox id="textEmail" CssClass="logininput" runat="server" MaxLength="50"></asp:textbox>
        </label>
        <label class="password">
        <span>Password</span>
        <asp:textbox id="textPassword" CssClass="logininput" runat="server" MaxLength="20" TextMode="Password"></asp:textbox>
        </label>
        <asp:CheckBox id="checkRememberMe" runat="server" Checked="true" style="display:inline;" Text="Remember me?" />
        <asp:Button id="buttonLogin" runat="server" CssClass="submit btn signinbutton" onclick="buttonLogin_Click" Text="Sign In" />
        <p>
        <asp:LinkButton id="linkForgotPassword" CssClass="forgot" runat="server" PostBackUrl="Forgot.aspx">Forgot your password?</asp:LinkButton>
        </p>
        <p><asp:label id="labelMessage" runat="server" CssClass="errorMessage"></asp:label></p>
        </fieldset>
            </div>
  </div>
  
  <script type="text/javascript" charset="utf-8">
      $(document).ready(function() {
          $('a.login-window').click(function() {

              //Getting the variable's value from a link
              var loginBox = $(this).attr('href');

              //Fade in the Popup
              $(loginBox).fadeIn(300);

              //Set the center alignment padding + border see css style
              var popMargTop = ($(loginBox).height() + 24) / 2;
              var popMargLeft = ($(loginBox).width() + 24) / 2;

              $(loginBox).css({
                  'margin-top': -popMargTop,
                  'margin-left': -popMargLeft
              });

              // Add the mask to body
              $('body').append('<div id="mask"></div>');
              $('#mask').fadeIn(300);

              return false;
          });

          // When clicking on the button close or the mask layer the popup closed
          $('a.close, #mask').live('click', function() {
              $('#mask , .login-popup').fadeOut(300, function() {
                  $('#mask').remove();
              });
              return false;
          });
      });
	</script>
   
   
   <div id="divLoginClick" runat="server" visible="false" enableviewstate="false">        
   <script type="text/javascript">
       $(document).ready(function() {
           $("#loginBox").click();
       });
   </script>
   
   </div> 

</asp:Content>

<asp:content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

   <div class="content">
		<div class="middle-content">
			<div class="inner-middle-content">
				<div class="middle-status"><h2>Site Owners</h2></div>
			</div>
		</div>
		<div class="inner-wrapper">
			<div class="inner-content">
			
			
				<div class="inner">
					<div class="inner-left">
						
						
						
<h3 class="font-size-36-new" style="color: #333">PRE-ROLL SKIP AD&trade;</h3><br/>
						
						
						
												<h1 class="oswald"><span class="green font-size-48">Enhance engagement. Increase ROI.</span><br> <span class="font-size-60 top-minus-18">Lower bounce rates.</span></h1><br/>
						
												<p>Research demonstrates that as many as 25% of site visitors click away from sites when forced to watch a pre-roll video ad*.<br/>
						Inqwise's Pre-Roll Skip Ad&trade; offers your site's visitors the choice to either complete the pre-roll or skip it by actively engaging with the advertiser's branded experience.<br/>
						Inqwise's Pre Roll Skip Ad&trade; empowers you to:
						</p><br/>
						<ul class="list-square margin-bottom-14" style="margin-left: 20px;">
							<li>Improve user experience and reduce bounce rate: 38% on average have chosen<br/> to "slide and skip"</li>
							<li>Monetize each page-view and impression even when site visitors skip the pre-roll</li>
							<li>Enhance measurable engagement with an advertiser's message and call-to-action</li>
							<li>Generate positive perception of your site and advertisers</li>
						</ul>
						<br/>
						<p><a href="mailto: support@Inqwise.com">Learn more</a> about how you can seamlessly integrate Inqwise units on your web and mobile properties!</p><br/>
						<p>We'd love to hear from you - <a title="contact us" href="/contactus.aspx">contact us</a> for more information!</p>
												<br/>
												<br/>
												<h3 class="font-size-36"><span>Try it out</span> now</h3>
						
						
												<div id="thumb_image_container" style="margin-top: 42px">
													<div id="thumb_image"></div>
													<div id="thumb_image_play"></div>
													<div id="thumb_image_stop"></div>
												</div>
												<p><br/><b>Flash version</b> - VAST / VPAID support</p>
						
						
												<div style="padding-top: 40px;">
						            				<div id="thumb_image_container_push">
														<div id="thumb_image_push"></div>
														<div id="thumb_image_play_push"></div>
														<div id="thumb_image_stop_push"></div>
													</div>
												</div>
												<p><br/><b>HTML5 version</b> - Optimized for mobile and touch screens. Full MRAID support</p>
						
												<br/>
												<br/>
												
												<h3 class="font-size-36-new" style="color: #333">SLIDING CAPTCHA&trade;</h3><br/>
												
												
												<h1 class="oswald"><span class="green font-size-48">Secure your site. Improve user experience.</span><br/> <span class="font-size-60 top-minus-18">Generate new revenue</span></h1><br/>
						
												
												<p>Captchas serve as vital tools to secure your site and protect it from hackers. Too often, however, they are difficult to read and can harm your site's user expirience, deterring visitors and posing an obstacle to conversion. Until now!</p><br/>
												<p>Inqwise's Sliding Captcha&trade; empowers you to:</p><br/>
												<ul class="list-square margin-bottom-14" style="margin-left: 20px;">
													<li>Enhance your site's security and protection from hackers</li>
													<li>Improve user experience by replacing standard captchas that so many find annoying and difficult to read</li>
													<li>Increase conversion - 94% on average, 24% higher than the industry average</li>
													<li>Generate new revenue by monetizing our Sliding Captcha&trade; with engaging, lean-in brand advertising</li>
													<li>Offer your advertisers a high performance advertising platform: 1-3% CTR, 10-30x average CTR generated by banners</li>
												</ul>
												<br/>
												<p><a href="/resources.aspx">Learn how easy</a> it is to integrate Inqwise's Sliding Captcha&trade; platform and how you can monetize each and every captcha!</p><br/>  
												<p>We'd love to hear from you - <a title="Contact us" href="/contactus.aspx">contact us</a> anytime to learn more about Sliding Captcha&trade;!</p>
												<br/>
												<br/>
												<h3 class="font-size-36"><span>Try it out</span> now</h3>
												<div style="padding-top: 24px;">
													<script type="text/javascript" src="<%=ApiUrl%>slider/get.ashx?imageTypeId=5&w=300&h=250"></script>
												</div>
						
							
																		<br/>
																		<br/>
																		<br/>
																		<div style="text-align: center;">
																			<a href="SignUp.aspx" title="Sign Up" class="button-black"><span class="sign-up">Sign Up</span></a>
																		</div>
																		
																		<br/>
																		<br/>
																		*Adage
												
												
						
						
							
							&nbsp;
							
					</div>
					<div class="inner-right">
						
						<div style="margin-left: 34px">
							<div style="padding-top: 14px;">
								<div class="box-green">
									<div style="display: block; padding: 10px; background: #fff;">
						<div style="background: url(/images/aol-logo.jpg) no-repeat 0 0 #fff; background-position: 100% 100%"><b>Follow in AOL's footsteps</b> and partner with Inqwise to revolutionize the way in which you monetize your video content. <a href="http://blog.aol.com/2013/09/06/aol-and-Inqwise-partner-to-create-new-video-ad-unit/" target="_blank" title="AOL and Inqwise Partner to Create New Video Ad Unit">Read more</a> to learn why AOL chose to offer its site visitors the ability to skip its own video ads to both boost engagement and increase monetization.<br/><br/><br/></div>
									</div>
								</div>
							</div>
						</div>
			            
			            
					</div>
					
				</div>

    
    		</div>
    	</div>
    	
    	
    </div>
	
	<script type="text/javascript">
	$(function() {
		
		$("#thumb_image_play").click(function() {
			
			$(this).hide();
			
			$("#thumb_image").empty();
			
			$("<iframe src=\"http://skipad.test.Inqwise.com/FW/UnitTestSkipRoll_640x360.html\" width=\"640\" height=\"360\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#thumb_image");
			
			$("#thumb_image_stop").show();
			
		});
		
		$("#thumb_image_stop").click(function() {
			$(this).hide();
			
			$("#thumb_image").empty();
			
			$("#thumb_image_play").show();
			
		});
		
		
		// push
		
		$("#thumb_image_play_push").click(function() {
			
			$(this).hide();
			
			$("#thumb_image_push").empty();
			
			
			
			$("<iframe src=\"http://skipad.resources.s3.amazonaws.com/mobile/template/admanager.html?tagurl=http://skipc1.Inqwise.com/tag?auid=7bd00c88-6707-4a68-a06d-56258ffbed75&otp=mraid&demo=1\" width=\"640\" height=\"360\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#thumb_image_push");
			
			$("#thumb_image_stop_push").show();
			
		});
		
		$("#thumb_image_stop_push").click(function() {
			$(this).hide();
			
			$("#thumb_image_push").empty();

			$("#thumb_image_play_push").show();
			
		});
	
	});
	</script>
    
</asp:content>