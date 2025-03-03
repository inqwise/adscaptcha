<%@ Page EnableViewStateMac="false" Title="Site Owners | Inqwise" Language="C#" MasterPageFile="~/AdsCaptcha.Master" AutoEventWireup="true" CodeFile="PreRollSkipAd.aspx.cs" Inherits="Inqwise.AdsCaptcha.Publisher.PreRollSkipAd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
id="body" class="page"
</asp:Content> 

<asp:Content ID="Content2" ContentPlaceHolderID="LoginContent" runat="server">

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="LoginScriptContent" runat="server"> 
	  
</asp:Content>

<asp:content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

   <div class="content">
		<div class="breadcrumbs">
			<a href="/" title="Home">Home</a>&nbsp;&rsaquo;&nbsp;Pre-Roll Skip Ad&trade;&nbsp;&rsaquo;&nbsp;Site Owners
		</div>
		<div class="inner-wrapper">
			<div class="inner-content">
				<div class="page-heading-wrapper">
					<h1>Pre-Roll Skip Ad&trade;</h1>
					<p>Enhance engagement. Increase ROI.</p>
					<a href="/gallery/?auid=f76e3b3f-b532-4e2b-bd98-633d60b0523d" title="Gallery" class="header-gallery-link">Gallery<i class="icon-link margin-left">&nbsp;</i></a>
				</div>
				<div class="inner-content-left-side">
					<div>
						<b>Flash version</b> - VAST / VPAID support<br/><br/>
					</div>
					<div id="thumb_image_container_slider" class="mid">
						<div id="thumb_image_slider"></div>
						<div id="thumb_image_play_slider"></div>
						<div id="thumb_image_stop_slider"></div>
					</div>
					<br/>
					<div class="color-green">Try it now</div>
					<br/>
					<br/>
					<div>
						<b>HTML5 version</b> - Optimized for mobile and touch screens. Full MRAID / VPAID support<br/><br/>
					</div>
					<div id="thumb_image_container_push" class="mid">
						<div id="thumb_image_push"></div>
						<div id="thumb_image_play_push"></div>
						<div id="thumb_image_stop_push"></div>
					</div>
					<br/>
					<div class="color-green">Try it now</div>
					<br/>
					<a href="/casestudy.aspx" title="View our Case Study"><i class="icon-link margin-right">&nbsp;</i>View our Case Study &gt;&gt;</a>
				</div>
				<div class="inner-content-right-side">
					<h4>Site Owners</h4>
					<div class="desc">
					<h3>Lower bounce rates.</h3><br/>
					<p>Research demonstrates that as many as 25% of site visitors click away from sites when forced to watch a pre-roll video ad*.<br/>
					Inqwise's Pre-Roll Skip Ad&trade; offers your site's visitors the choice to either complete the pre-roll or skip it by actively engaging with the advertiser's branded experience.<br/>
					Inqwise's Pre Roll Skip Ad&trade; empowers you to:
					</p><br/>
					<ul style="margin: 0 20px;text-align: justify;" class="list-square-green margin-bottom-14">
						<li>Improve user experience and reduce bounce rate: 38% on average have chosen to "slide and skip"</li>
						<li>Monetize each page-view and impression even when site visitors skip the pre-roll</li>
						<li>Enhance measurable engagement with an advertiser's message and call-to-action</li>
						<li>Generate positive perception of your site and advertisers</li>
					</ul>
					<br/>
					<p><a href="mailto: support@Inqwise.com">Learn more</a> about how you can seamlessly integrate Inqwise units on your web and mobile properties!</p><br/>
					<p>We'd love to hear from you - <a title="contact us" href="/contactus.aspx">contact us</a> for more information!</p>
					</div>
					<div style="border: 1px solid #ccc; padding: 20px;width: 458px;">
						<div class="sub-heading">
							<p><b>Inqwise's</b> Pre-Roll Skip Ad&trade; was <a href="http://blog.aol.com/2013/09/06/aol-and-Inqwise-partner-to-create-new-video-ad-unit/" target="_blank" title="AOL and Inqwise Partner to Create New Video Ad Unit">selected By AOL</a> as the SKIP solution for <a href="http://ads.aolonnetwork.com/solutions/ad-formats" target="_blank" title="Ad Formats">AOL's sites and video ad network publishers</a>.</p><br/>
						</div>
						<div>
							<div>
								<div style="background: url(/images/aol-logo.jpg) no-repeat 0 0 #fff; background-position: 100% 100%"><b>Follow in AOL's footsteps</b> and partner with Inqwise to revolutionize the way in which you monetize your video content. <a href="http://blog.aol.com/2013/09/06/aol-and-Inqwise-partner-to-create-new-video-ad-unit/" target="_blank" title="AOL and Inqwise Partner to Create New Video Ad Unit">Read more</a> to learn why AOL chose to offer its site visitors the ability to skip its own video ads to both boost engagement and increase monetization.<br/><br/><br/></div>
							</div>
						</div>
					</div>
					<br/>
					<span class="small">*Adage</span>
					<br/>
					<br/>
					<a href="/contactus.aspx" title="Contact Sales" class="button-green"><span>Contact Sales</span></a>
					<br/>
					<br/>
					<div class="text-align-right">
					Pre-Roll Skip Ad&trade; <a href="/advertiser/prerollskipad.aspx" title="Pre-Roll Skip Ad&trade; for advertisers">for advertisers &gt;&gt;</a>
					</div>
					<br/>
					<br/>
				</div>
    		</div>
    	</div>
    </div>
	
	<script type="text/javascript">
	$(function() {
		
		// slider
		
		$("#thumb_image_play_slider").click(function() {
			$(this).hide();
			$("#thumb_image_slider").empty();
			$("<iframe src=\"http://skipad.test.Inqwise.com/FW/SkipRollTagDemo_512x288.html?srqatnocache=true&pid=91A3-883&tagurl=http%3A%2F%2Fskipc1.Inqwise.com%2Ftag%3Fauid%3D7e7f3be9-9717-4289-9094-b77eedc4086a%26otp%3Dvast&width=640&height=360\" width=\"512\" height=\"288\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#thumb_image_slider");
			$("#thumb_image_stop_slider").show();
		});
		
		$("#thumb_image_stop_slider").click(function() {
			$(this).hide();
			$("#thumb_image_slider").empty();
			$("#thumb_image_play_slider").show();
		});
		
		
		// push
		
		$("#thumb_image_play_push").click(function() {
			$(this).hide();
			$("#thumb_image_push").empty();
			
			$("<iframe src=\"http://skipad.resources.s3.amazonaws.com/mobile/template/admanager.512.html?tagurl=http://skipc1.Inqwise.com/tag?auid=7bd00c88-6707-4a68-a06d-56258ffbed75&otp=mraid&demo=1\" width=\"512\" height=\"288\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#thumb_image_push");
			
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