<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="AdsCaptcha.Master" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="Inqwise.AdsCaptcha.ErrorPage" %>
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

<asp:content ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="content">
		<div class="middle-content">
			<div class="inner-middle-content">
				<div class="middle-status"><h2>Some error occurs</h2></div>
			</div>
		</div>
		<div class="inner-wrapper">
			<div class="inner-content">


        		<div class="description" style="float:left;width:570px;margin:30px 0 0 0;">
        			<img src="css/Inqwise/images/404.jpeg" alt="Error" />
        		</div>
        		<div class="description" style="float:left;width:360px;margin:60px 0 0 0;">
            		<h4 style="line-height: 55px;"><asp:Label ID="labelErrorTitle" runat="server" CssClass="errorTitle"></asp:Label></h4>
            		<div id="errorLinks">
                		<asp:Label ID="labelErrorMessage" runat="server"></asp:Label>
                		You may choose one of the following links:
                		<ul>
                    		<li>AdsCaptcha - <a href="Index.aspx">Homepage</a></li>
                    		<li>AdsCaptcha - <a href="Advertiser/StartPage.aspx">For Advertisers</a></li>
                    		<li>AdsCaptcha - <a href="Publisher/StartPage.aspx">For Site Owners</a></li>
                		</ul>
                		<asp:Label ID="labelRedirection" runat="server">AdsCaptcha homepage will load in a few seconds</asp:Label>
                		<br />
                		<br />
                		<div class="report"><a href="ContactUs.aspx">Report To Webmaster</a></div>
            		</div>
        		</div>
        		    
        
        	</div>
        </div>
	</div>
        
</asp:content>