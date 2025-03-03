<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainTop.ascx.cs" Inherits="Inqwise.AdsCaptcha.UserControls.Menu.MainTop" %>
<ul id="menu-top" class="fr">
    <li><a href="<%# System.Configuration.ConfigurationManager.AppSettings["Url"]%>aboutus.aspx">ABOUT US</a></li>
    <li>|</li>
    <li><a href="<%# System.Configuration.ConfigurationManager.AppSettings["Url"]%>products.aspx">PRODUCTS</a></li>
    <!--li>|</li>
    <li><a href="#login-box" class="login-window">SIGN IN</a></li>
    <li>|</li>
    <li><a href="#login-box" class="login-window">SIGN IN</a></li-->
</ul>