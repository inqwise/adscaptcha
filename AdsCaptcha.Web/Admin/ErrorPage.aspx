<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.ErrorPage" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <!--<meta http-equiv="refresh" content="10;url=StartPage.aspx" />-->
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">
    <div class="gradient_bg"> 
        <div id="loginFrame">
            <div class="errorIcon"><asp:Label ID="labelErrorTitle" runat="server" CssClass="errorTitle"></asp:Label></div>
            <div id="errorLinks">
                <asp:Label ID="labelErrorMessage" runat="server"></asp:Label>
                You may choose one of the following links:
                <ul>
                    <li>AdsCaptcha - <a href="StartPage.aspx">Administration Panel</a></li>
                    <li>AdsCaptcha - <a href="<%=ConfigurationSettings.AppSettings["URL"]%>Advertiser/StartPage.aspx">For Advertisers</a></li>
                    <li>AdsCaptcha - <a href="<%=ConfigurationSettings.AppSettings["URL"]%>Publisher/StartPage.aspx">For Site Owners</a></li>
                    <li>AdsCaptcha - <a href="<%=ConfigurationSettings.AppSettings["URL"]%>Developer/StartPage.aspx">For Developers</a></li>
                </ul>
            </div>
        </div> 
    </div>
</asp:content>