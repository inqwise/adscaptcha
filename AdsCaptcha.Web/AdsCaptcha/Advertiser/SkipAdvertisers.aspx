<%@ Page EnableViewStateMac="false" Title="Pre-Roll SKIP ad - Advertisers | Inqwise" Language="C#" MasterPageFile="~/AdsCaptcha.Master" AutoEventWireup="true" CodeFile="SkipAdvertisers.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.SkipAdvertisers" %>
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
			<li><a href="StartPage.aspx" title="Advertisers" class="button-green"><span>Advertisers</span></a></li>
			<li><a href="/publisher/StartPage.aspx" title="Site Owners" class="button-green"><span>Site Owners</span></a></li>
		</ul>
	</div> 
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

	<div class="content">
		<div class="middle-content">
			<div class="inner-middle-content">
				<div class="middle-status"><h2>Pre-Roll SKIP ad - Advertisers</h2></div>
			</div>
		</div>
		<div class="inner-wrapper">
			<div class="inner-content">
			
				<div class="inner">
					<div class="inner-left">

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
<p>Inqwise's ad units seamlessly fit as a layer on existing pre-roll video ads and are platform, device, and player agnostic.<br/>
Learn more about Inqwise's robust advertising platform.</p>
<br/>
<p>We'd love to hear from you! <a title="Contact us" href="/contactus.aspx">Contact us</a> for more information about how Inqwise's Pre Roll Skip Ad&trade;!</p><br/>
<p><b>Inqwise's</b> Pre-Roll Skip Ad was <a href="http://blog.aol.com/2013/09/06/aol-and-Inqwise-partner-to-create-new-video-ad-unit/" target="_blank" title="AOL and Inqwise Partner to Create New Video Ad Unit">selected By AOL</a> as the SKIP solution for <a href="http://ads.aolonnetwork.com/solutions/ad-formats" target="_blank" title="Ad Formats">AOL's sites and video ad network publishers</a>.</p>
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
							<div>
								<ul class="list-square margin-bottom-14 font-size-16 green-dots">
									<li class="square-grey-20x20">Ensure your ad exposure<br/> - through Slide to Skip or Pre-Roll</li>
									<li class="square-green-20x20 color-green">Deliver innovative video<br/> advertising online</li>
									<li class="square-grey-20x20"><strong>38% of the users choose to Slide and Skip the Pre Roll Ad</strong></li>
									<li class="square-green-20x20 color-green"><strong>9.3 Second engagement time with the Skip Ad</strong></li>
									<li class="square-grey-20x20"><strong>3.5% Click Through Rate on Skip Ad</strong></li>
									<li class="square-green-20x20 color-green">Give your audience a choice and<br/> create a positive brand attitude</li>
								</ul>
							</div>
							<div class="padding-top-24">
								<div class="box-green">
									<h3>Optional Services:</h3>
									<ul class="list-square-white padding-top-16">
										<li>Real time A/B Testing analytic and optimization</li>
										<li>Ads Creative and Landing pages</li>
									</ul>
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





