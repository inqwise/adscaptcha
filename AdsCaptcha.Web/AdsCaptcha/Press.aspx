<%@ Page Language="C#" AutoEventWireup="true" Title="Press kit | Inqwise"   MasterPageFile="AdsCaptcha.Master" CodeFile="Press.aspx.cs" Inherits="Inqwise.AdsCaptcha.Press" %>
<%@ Register src="UserControls/Press/LatestNews.ascx" tagname="LatestNews" tagprefix="uc1" %>
<%@ Register src="UserControls/Press/ContactForm.ascx" tagname="ContactForm" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 

	<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>
	
	<!--
	<style type="text/css">
	h5 {
   		clear:both;
   		display:block;
   		margin-top: 10px;
   		line-height: 26px;
   	}
	</style>
	-->
		
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
id="body" class="page"
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


<asp:content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

	<div class="content">
		<div class="breadcrumbs">
			<a href="/" title="Home">Home</a>&nbsp;&rsaquo;&nbsp;Press
		</div>
		<div class="inner-wrapper">
			<div class="inner-content">
				<div class="page-heading-wrapper">
					<h1>Latest News</h1>
				</div>
				<div class="inner-content-left-side">
					<uc1:LatestNews ID="LatestNews1" runat="server" />
				</div>
				<div class="inner-content-right-side">
             		<uc2:ContactForm ID="ContactForm1" runat="server" />
		             <div style="clear:both;"></div>
		             <br />
		             <h4>Videos</h4>
		             <br />
		             <div style="clear: both;">
		             	<div style="padding: 10px; float: left">
		             		<a href="" target="_blank" ><img src="" alt="Inqwise video" /></a>
		             	</div>
		             	<div style="padding: 10px; float: left">
		             		<a href="" target="_blank" ><img src="" alt="Inqwise video" /></a>
		             	</div>
		             	<div style="padding: 10px; float: left">
		             		<a href="" target="_blank" ><img src="" alt="Inqwise video" /></a>
		             	</div>
		             </div>
		             
		             <div style="clear:both;"></div>
		             <br /><br />
		             <h4>Logo</h4><br />
		             <div>
		             	<a href="css/Inqwise/images/logo.png" target="_blank"><img src="css/Inqwise/images/logo.png" alt="Inqwise logo" /></a>
		             	<br />
		             	<a href="css/Inqwise/images/Logo.png" target="_blank"><img src="css/Inqwise/images/Logo.png" alt="Inqwise logo" /></a>
		             </div>
				</div>
            </div>
		</div>
		
		
		
		
		
		
		<!--
		<div class="coa_footer">
			<div class="coa_container">
		    	<a href="publisher/signup.aspx" class="fl_right_a"><img src="css/Inqwise/images/singup_btn.jpg" width="200px" /></a>
		  		<div class="fl_right" style="padding-left:26px">
					<h4>FOR <span class="uppercase">SITE OWNERS</span></h4>
					<div class="description">More revenue, Lower Bounce Rate with Pre-Roll SKIP ad and Sliding Captcha</div>
				</div>
				<a href="advertiser/signup.aspx" class="fl_right_a"><img src="css/Inqwise/images/singup_btn.jpg" width="200px" /></a>
		    	<div class="fl_right">
					<h4>FOR <span class="uppercase">ADVERTISERS</span></h4>
					<div class="description">Interactive advertising the user can't disregard!</div>
		  		</div>
		  	</div>
		</div>
		-->
		
		
		 
	</div>
     
</asp:content>
