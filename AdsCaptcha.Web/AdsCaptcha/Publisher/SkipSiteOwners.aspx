<%@ Page EnableViewStateMac="false" Title="Pre-Roll SKIP ad - Site Owners | Inqwise" Language="C#" MasterPageFile="~/AdsCaptcha.Master" AutoEventWireup="true" CodeFile="SkipSiteOwners.aspx.cs" Inherits="Inqwise.AdsCaptcha.Publisher.SkipSiteOwners" %>
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
			<!-- <li><a id="loginBox" href="#login-box" class="login-window">SIGN IN</a></li> -->
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

<asp:content ContentPlaceHolderID="MainContent" runat="server">

	<div class="content">
		<div class="middle-content">
			<div class="inner-middle-content">
				<div class="middle-status"><h2>Pre-Roll SKIP ad - Site Owners</h2></div>
			</div>
		</div>
		<div class="inner-wrapper">
			<div class="inner-content">
			
				<div class="inner">
					<div class="inner-left">
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
<p>Learn more about how you can seamlessly integrate Inqwise units on your web and mobile properties!</p><br/>
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
						<p><b>Inqwise's</b> ad units seamlessly fit as a layer  on existing pre-roll video ads and are platform, device, and player agnostic:</p>
						<br/>
						<ul class="list-square margin-bottom-14" style="margin-left: 20px;">
							<li>Flash and HTML5</li>
							<li>On any player that runs VAST / VPAID</li>
							<li>iOS and Android</li>
						</ul>
						<br/>
						<p>For more information about Pre-Roll SKIP ad, please <a href="/contactus.aspx" title="contact">contact</a> our sales team.</p>
						
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
							<div class="padding-top-24">
								<ul class="list-square margin-bottom-14 font-size-16 green-dots">
									<li class="square-grey-20x20">Reduce your site<br/> bouncerate/click aways</li>
									<li class="square-green-20x20 color-green">Give your audience a choice<br/> - they can Slide and skip or<br/> watch a video ad</li>
									<li class="square-grey-20x20">Present your audience with<br/> an innovative and alternative<br/> video ad</li>
									<li class="square-green-20x20 color-green">Increase your revenue by<br/> creating a true and full<br/> guaranteed impression for<br/> your advertiser</li>
								</ul>
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





