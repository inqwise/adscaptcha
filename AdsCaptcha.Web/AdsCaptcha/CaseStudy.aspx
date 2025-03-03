<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="AdsCaptcha.Master" AutoEventWireup="true" CodeFile="CaseStudy.aspx.cs" Inherits="Inqwise.AdsCaptcha.CaseStudy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
id="supportpage" class="page"
</asp:Content>
<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginContent" runat="server">
	
</asp:Content>
<asp:content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<div class="content">
		<div class="breadcrumbs">
			<a href="/" title="Home">Home</a>&nbsp;&rsaquo;&nbsp;Case Study
		</div>
		<div class="inner-wrapper">
			<div class="inner-content">
				<div class="page-heading-wrapper">
					<h1>Case Study</h1>
					<a href="/gallery/?auid=f76e3b3f-b532-4e2b-bd98-633d60b0523d" title="Gallery" class="header-gallery-link">Gallery<i class="icon-link margin-left">&nbsp;</i></a>
				</div>
				<div class="inner-content-left-side">
					<div>
						<b>Pre-roll Skip Ad&trade;</b><br/><br/>
					</div>
					<div id="thumb_image_container_slider" class="empty">
						<div id="thumb_image_slider"></div>
						<div id="thumb_image_play_slider"></div>
						<div id="thumb_image_stop_slider"></div>
					</div>
					<!--
					<div id="thumb_image_container_push" class="empty">
						<div id="thumb_image_push"></div>
						<div id="thumb_image_play_push"></div>
						<div id="thumb_image_stop_push"></div>
					</div>
					-->
					<br/>
					<div class="color-green">Try it now</div>
					<br/>
					<br/>
				</div>	
				<div class="inner-content-right-side">
	            	<h4 style="line-height: 32px;">Comcast Xfinity Campaign on AOL Properties Powered by Inqwise</h4>
					<div class="desc">
						Slide, skip... and click? Indeed, 3.85 times as much - just ask AOL and Comcast!
					</div>
					<b>Campaign Background and Objectives:</b>
					<div class="desc">Comcast desired to raise awareness for its Xfinity high speed Internet service and ran a video campaign on AOL "owned and operated" and network properties. AOL proposed that Comcast use Inqwise's innovative Pre Roll Skip Ad&trade; in order to guarantee interactive engagement and drive traffic to Comcast's Xfinity landing page. </div>
					<b>Campaign Overview:</b>
					<div class="desc">
						<ul class="list-square-green margin-bottom-14" style="margin: 0 20px;text-align: justify;">
							<li>The campaign ran on over 550 domains across AOL's network for one month</li>
							<li>AOL built the unit immediately using Inqwise's self-serve management system. A video asset (the same used as with any standard pre-roll), together with a branded image, were simply uploaded, generating  the campaign's VAST Tag that was served by FreeWheel. AOL was able to customize the player's design, the timing when users can opt to <b>"Slide to Skip"</b> and other parameters and features.</li>
							<li>AOL continuously monitored the campaign's performance using the Inqwise backend management system</li>
							<li>Neither Comcast nor AOL actively promoted the Inqwise unit or explained to site visitors how it works. </li>
						</ul>
					</div>
					<b>Campaign Results</b>
					<div class="desc">
						Monetization: AOL was able to monetize each and every impression, regardless of whether the pre-roll was skipped, without compromising on the engagement it promises its advertisers.
					</div>
					<b>User interaction:</b>
					<div class="desc">
						The number of users who decided to "slide and skip" the pre-roll and thereby actively assemble the branded Comcast Pre Roll Skip Ad&trade; powered by Inqwise increased throughout the duration of the campaign, as users became more familiar with it.<br/><br/>
						<ul class="list-square-green margin-bottom-14" style="margin: 0 20px;text-align: justify;">
							<li>On premium sites, such as TechCrunch and The Huffington Post, 35-50% of site visitors opted to "slide and skip" the pre-roll and assembled a branded Comcast unit powered by Inqwise</li>
							<li>On average, approximately 15% of site visitors opted to "slide and skip"</li>
						</ul>
					</div>
					<b>User Engagement:</b>
					<div class="desc">
						<ul class="list-square-green margin-bottom-14" style="margin: 0 20px;text-align: justify;">
							<li>Inqwise recorded over 199 hours of active user engagement with the Xfinity brand - i.e. site visitors who "slided to skip" the pre-roll but in doing so assembled a branded Comcast unit and engaged with the Xfinity brand</li>
							<li>This engagement was guaranteed and measured, whereas standard pre-roll units cannot measure whether the user actually viewed the video or just let it run in the background.</li>
						</ul>
					</div>
					<b>Performance</b>
					<div class="desc">
						Site visitors who "slided to skip" using the Inqwise Pre Roll Skip Ad&trade; clicked through to Comcast's landing page at a rate of 3.12% - 3.85 times more than visitors who let the pre-roll run entirely
					</div>
				</div>
			</div>
        	</div>
        </div>
    </div>
	
	
	<script type="text/javascript">
	$(function() {
		
		// slider
		$('#thumb_image_container_slider').css({
			'background' : 'url(http://skipad.resources.s3.amazonaws.com/video/comcast/Screen-Shot-2014-07-08-at-14.32.48.jpg)',
			'background-size' : '512px 288px'
		});
		$("#thumb_image_play_slider").click(function() {
			$(this).hide();
			$("#thumb_image_slider").empty();
			$("<iframe src=\"http://skipad.test.Inqwise.com/FW/SkipRollTagDemo_512x288.html?srqatnocache=true&pid=91A3-883&tagurl=http%3A%2F%2Fskipc1.Inqwise.com%2Ftag%3Fauid%3De2d09913-af40-4761-9229-955c1df44410%26otp%3Dvast&width=640&height=360\" width=\"512\" height=\"288\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#thumb_image_slider");
			$("#thumb_image_stop_slider").show();
		});
		
		$("#thumb_image_stop_slider").click(function() {
			$(this).hide();
			$("#thumb_image_slider").empty();
			$("#thumb_image_play_slider").show();
		});
		
		// push
		
		
		/*
		$('#thumb_image_container_push').css({
			'background' : 'url(http://skipad.resources.s3.amazonaws.com/video/comcast/i_1280x720x8d1670d0939f2eb.jpg)',
			'background-size' : '512px 288px'
		});
		$("#thumb_image_play_push").click(function() {
			$(this).hide();
			$("#thumb_image_push").empty();
			
			$("<iframe src=\"http://skipad.resources.s3.amazonaws.com/mobile/template/admanager.512.html?tagurl=http://skipc1.Inqwise.com/tag?auid=e2d09913-af40-4761-9229-955c1df44410&otp=mraid&demo=1\" width=\"512\" height=\"288\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#thumb_image_push");
			
			$("#thumb_image_stop_push").show();
		});
		
		$("#thumb_image_stop_push").click(function() {
			$(this).hide();
			$("#thumb_image_push").empty();
			$("#thumb_image_play_push").show();
		});
		*/
	
	});
	</script>
	
</asp:content>