<%@ Page EnableViewStateMac="false" Title="Advertisers | Inqwise" Language="C#" MasterPageFile="~/AdsCaptcha.Master" AutoEventWireup="true" CodeFile="StartPage.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.StartPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
id="body" class="page"
</asp:Content> 

<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginContent" runat="server"> 
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
			<li><a href="StartPage.aspx" title="Advertisers" class="button-green"><span>Advertisers</span></a></li>
			<li><a href="/publisher/StartPage.aspx" title="Site Owners" class="button-green"><span>Site Owners</span></a></li>
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
  
  <script type="text/javascript"charset="utf-8">
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
				<div class="middle-status"><h2>Advertisers</h2></div>
			</div>
		</div>
		<div class="inner-wrapper">
			<div class="inner-content">
			
				<div class="inner">
					<div class="inner-left">
			
			
<h3 class="font-size-36-new" style="color: #333">PRE-ROLL SKIP AD&trade;</h3><br/>
			
						<h1 class="oswald"><span class="green font-size-48">Let consumers choose how they engage with your ads.</span><br> <span class="font-size-60 top-minus-18">Don't compromise on engagement or performance.</span></h1><br/>
					
						<p>Few pre-roll ads achieve their targets. Consumers often click away, open new tabs, or simply leave their seats - and automated "bots" are programmed to click play - all while your advertising dollars go to waste.<br/><br/></p>
						<p>Pre-roll advertising doesn't need to be a zero-sum game.
						<strong>Inqwise's</strong> Pre-Roll Skip Ad&trade; gives consumers the choice to "Slide and Skip" - or "Push to Skip" on mobile devices - directly to your marketing message and call to action in a "lean-in" game-like experience.</p><br/>
						<ul class="list-square margin-bottom-14" style="margin-left: 20px;">
							<li>Guarantee active and measurable branded engagement, averaging 9.3 seconds</li>
							<li>Enhance positive brand association, balancing consumer choice with effective message delivery</li>
							<li>Drive higher performance: 3.5% average click-through</li>
						</ul>
						<br/>
						<p>Inqwise's ad units seamlessly fit as a layer on existing pre-roll video ads and are platform, device, and player agnostic.<br/><br/>
						<a href="mailto: support@Inqwise.com">Learn more</a> about Inqwise's robust advertising platform.</p>
						<br/>
						<p>We'd love to hear from you! <a title="Contact us" href="/contactus.aspx">Contact us</a> for more information about how Inqwise's Pre Roll Skip Ad&trade;!</p><br/>
						<p><b>Inqwise's</b> Pre-Roll Skip Ad&trade; was <a href="http://blog.aol.com/2013/09/06/aol-and-Inqwise-partner-to-create-new-video-ad-unit/" target="_blank" title="AOL and Inqwise Partner to Create New Video Ad Unit">selected By AOL</a> as the SKIP solution for <a href="http://ads.aolonnetwork.com/solutions/ad-formats" target="_blank" title="Ad Formats">AOL's sites and video ad network publishers</a>.</p>
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
												
												<h1 class="oswald"><span class="green font-size-60">Guarantee consumer engagement and enhance performance.</span><br> <span class="font-size-72 top-minus-26">Make your ads fun and memorable!</span></h1><br/>
						
												<p>Research demonstrates that fewer than 14% of consumers remember the last display ad they saw, failing to recall the product, company, or brand.** Banner blindness has plagued online advertising for years, as millions of dollars go to waste.</p><br/>
												<p>Click-through-rates continue to decline, whereas hackers continue to activate automated bots that click on your ad. Standard captchas designed to secure sites and protect them from hackers comprise prime, highly targeted advertising inventory, but have failed to create a positive user experience.</p><br/>
												<p>How can you guarantee consumer engagement in a lean-in experience that combats banner blindness but ensures a positive user experience?</p><br/>					
												<p>With Inqwise's Sliding Captcha&trade;! Inqwise's Sliding Captcha ad units empower advertisers to:</p><br/>
												<ul class="list-square margin-bottom-14" style="margin-left: 20px;">
													<li>Guarantee consumer engagement and fight banner blindness with a fun, lean-in experience</li>
													<li>Improve user experience on a myriad of websites, promoting a positive perception of your brand</li>
													<li>Drive performance with average CTR of 1-3%, 10-30 times the industry average</li>
													<li>Target ads and place them in contextually and geographically relevant spots</li>
												</ul>
												<br/>
												<p>We'd love to hear from you! <a title="Contact us" href="/contactus.aspx">Contact us</a> for more information about how Inqwise's Sliding Captcha&trade;</p>
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
																		*Adage<br/>
																		**Infolinks
						
						
					</div>
					<div class="inner-right">
			            
						
						
			            
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