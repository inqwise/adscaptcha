<%@ Page EnableViewStateMac="false" Title="About Us | Inqwise" Language="C#" MasterPageFile="AdsCaptcha.Master" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="Inqwise.AdsCaptcha.AboutUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
id="body" class="page"
</asp:Content> 
<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginContent" runat="server">
	
</asp:Content>
<asp:content ContentPlaceHolderID="MainContent" runat="server">
	
	<div class="content">
		<div class="breadcrumbs">
			<a href="/" title="Home">Home</a>&nbsp;&rsaquo;&nbsp;About Us
		</div>
		<div class="inner-wrapper">
			<div class="inner-content">
				<div class="page-heading-wrapper">
					<h1>About Us</h1>
				</div>
				<div class="inner-content-left-side">
					<div>
						<b>Pre-roll Skip Ad&trade;</b><br/><br/>
					</div>
					<div id="thumb_image_container_push" class="mid">
						<div id="thumb_image_push"></div>
						<div id="thumb_image_play_push"></div>
						<div id="thumb_image_stop_push"></div>
					</div>
					<br/>
					<div class="color-green">Try it now</div>
					<br/>
					<br/>
					<div>
						<b>Sliding Captcha&trade;</b><br/><br/>
					</div>
					<div>
						<script type="text/javascript" src="<%=ApiUrl%>slider/get.ashx?imageTypeId=5&w=300&h=250"></script>
					</div>
					<br/>
					<div class="color-green">Try it now</div>
				</div>
				<div class="inner-content-right-side">
			    	<h4>WHAT WE DO</h4>
					<div class="desc">
			        	<p>We are an innovative advertising technology company that develops non-intrusive advertising products that address the shortcomings of pre-roll video ads and captchas. Inqwise's Pre Roll Skip Ad&trade; and Sliding Captcha&trade; units guarantee engagement, improve user experience, lower bounce rate, and increase revenue for web and mobile publishers and advertisers. Inqwise's products use the company's proprietary slide-bar technology, which turns engagement with a brand into a fun, game-like experience.<br/>Learn more about the benefits of Inqwise's revolutionary products for site owners and advertisers.</p>
					</div>
					<h4>THE TEAM</h4>
					<div class="desc">
						<strong>Chief Executive Officer - Gadi Hadar</strong>
						<p>Gadi is renowned in Israel as one of the country's internet pioneers. Gadi founded <a href="http://www.walla.co.il" target="_blank" title="Walla">Walla</a>, Israels first and most popular internet portal, and served as its CEO from 1994-2001. Walla was also the first internet IPO on the Tel Aviv Stock Exchange. Gadi subsequently served as Founder and Managing Director of <a href="http://www.easy-forex.com.au" target="_blank" title="Easy Forex&reg; Asia Pacific">Easy Forex&reg; Asia Pacific</a>, from 2003-2010. Gadi earned a BA and an MBA from Tel Aviv University.</p>
						<br/>
						<strong>Founder &amp; Chief Product Officer - Shay Inbar</strong>
						<p>Shay Inbar is an Internet sector veteran, with extensive experience in business and product development of online games and online classifieds. Prior to Inqwise, Shay co-founded <a href="http://www.megavendo.es" target="_blank" title="Megavendo">Megavendo</a>, a leader in online local advertising/classifieds aggregation and discovery.</p>
						<br/>
						<strong>VP Marketing &amp; Business Development - Tal Even</strong>
						<p>Tal brings 13 years of technology and media sector experience as both a former corporate lawyer and a business development, marketing, and sales professional. Tal previously held key positions at leading internet companies such as SupersonicAds, Dynamix, and ooVoo. Tal holds an MBA from the NYU Stern School of Business and a law degree from Bar Ilan University.</p>
					</div>
					<h4>Advisory board</h4>
					<div class="desc">
						<p><strong><a href="http://www.cs.tau.ac.il/~dcor/" title="Prof. Daniel Cohen-Or" target="_blank" style="color: #333">Prof. Daniel Cohen-Or</a></strong> is an expert in the fields of Computer Graphics, Visual Computing and Geometric Modeling, including rendering and modeling techniques. He is a senior member of the Department of Computer Science at <a href="http://www.cs.tau.ac.il/~dcor/" target="_blank">Tel Aviv University</a>. Prof. Cohen-Or earned a B.Sc. cum laude in Mathematics and Computer Science (1985) and an  M.Sc. cum laude in Computer Science (1986) from Ben-Gurion University, and a Ph.D. from the Department of Computer Science (1991) at State University of New York at Stony Brook.</p>
						<br/>
						<p><strong><a href="http://www.wisdom.weizmann.ac.il/~naor/" title="Prof. Moni Naor" target="_blank" style="color: #333">Prof. Moni Naor</a></strong>, of the <a href="http://www.wisdom.weizmann.ac.il/~naor/" target="_blank">Weizmann Institute of Science</a>, is renowned for his work in non-malleable cryptography, visual cryptography and for his contribution to the field that laid the foundations for CAPTCHA technology. <a href="http://en.wikipedia.org/wiki/Moni_Naor" target="_blank">http://en.wikipedia.org/wiki/Moni_Naor</a>
						</p>
					</div>
				</div>
			</div>
		</div>
	</div>
	
	
	<script type="text/javascript">
	$(function() {
		
		// slider
		/*
		$("#thumb_image_play_slider").click(function() {
			$(this).hide();
			$("#thumb_image_slider").empty();
			$("<iframe src=\"http://skipad.test.Inqwise.com/FW/SkipRollTagDemo_512x288.html?srqatnocache=true&pid=91A3-883&tagurl=http%3A%2F%2Fskipc1.Inqwise.com%2Ftag%3Fauid%3D7de074e7-b973-4f19-a15e-93d14388927b%26otp%3Dvast&width=640&height=360\" width=\"512\" height=\"288\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#thumb_image_slider");
			$("#thumb_image_stop_slider").show();
		});
		
		$("#thumb_image_stop_slider").click(function() {
			$(this).hide();
			$("#thumb_image_slider").empty();
			$("#thumb_image_play_slider").show();
		});
		*/
		
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