<%@ Page EnableViewStateMac="false" Title="Advertisers | Inqwise" Language="C#" MasterPageFile="~/AdsCaptcha.Master" AutoEventWireup="true" CodeFile="SlidingCaptcha.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.SlidingCaptcha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
id="body" class="page"
</asp:Content> 

<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginContent" runat="server"> 
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="LoginScriptContent" runat="server"> 

</asp:Content>
<asp:content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	
	<div class="content">
		<div class="breadcrumbs">
			<a href="/" title="Home">Home</a>&nbsp;&rsaquo;&nbsp;Sliding Captcha&trade;&nbsp;&rsaquo;&nbsp;Advertisers
		</div>
		<div class="inner-wrapper">
			<div class="inner-content">
				<div class="page-heading-wrapper">
					<h1>Sliding Captcha&trade;</h1>
				</div>
				<div class="inner-content-left-side">
					<div>
						<script type="text/javascript" src="<%=ApiUrl%>slider/get.ashx?imageTypeId=5&w=300&h=250" ></script>
					</div>
					<br/>
					<div class="color-green">Try it now</div>
					<br/>
					<br/>
				</div>
				<div class="inner-content-right-side">
					<h4>Advertisers</h4>
					<div class="desc">
					<h3 style="line-height: 28px;">Guarantee consumer engagement and enhance performance. <br>Make your ads fun and memorable!</h3><br/>
					<p>Research demonstrates that fewer than 14% of consumers remember the last display ad they saw, failing to recall the product, company, or brand.** Banner blindness has plagued online advertising for years, as millions of dollars go to waste.</p><br/>
					<p>Click-through-rates continue to decline, whereas hackers continue to activate automated bots that click on your ad. Standard captchas designed to secure sites and protect them from hackers comprise prime, highly targeted advertising inventory, but have failed to create a positive user experience.</p><br/>
					<p>How can you guarantee consumer engagement in a lean-in experience that combats banner blindness but ensures a positive user experience?</p><br/>					
					<p>With Inqwise's Sliding Captcha&trade;! Inqwise's Sliding Captcha ad units empower advertisers to:</p><br/>
					<ul style="margin: 0 20px;text-align: justify;" class="list-square-green margin-bottom-14">
						<li>Guarantee consumer engagement and fight banner blindness with a fun, lean-in experience</li>
						<li>Improve user experience on a myriad of websites, promoting a positive perception of your brand</li>
						<li>Drive performance with average CTR of 1-3%, 10-30 times the industry average</li>
						<li>Target ads and place them in contextually and geographically relevant spots</li>
					</ul>
					<br/>
					<p>We'd love to hear from you! <a title="Contact us" href="/contactus.aspx">Contact us</a> for more information about how Inqwise's Sliding Captcha&trade;</p>
					</div>
					<span class="small">**Infolinks</span>
					<br/>
					<br/>
					<a href="/advertiser/signup.aspx" title="Start Campaign" class="button-green"><span>Start Campaign</span></a>
					<br/>
					<br/>
					<div class="text-align-right">
					Sliding Captcha&trade; <a href="/publisher/slidingcaptcha.aspx" title="Sliding Captcha&trade; for site owners">for site owners &gt;&gt;</a>
					</div>
					<br/>
					<br/>
				</div>
    		</div>
		</div>
	</div>
    
</asp:content>