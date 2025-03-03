<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="Admin.Master" ValidateRequest="false" CodeFile="Presskit.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.Presskit" %>

<%@ Register src="UserControls/PressInquires.ascx" tagname="PressInquires" tagprefix="uc1" %>

<%@ Register src="UserControls/PressAddNews.ascx" tagname="PressAddNews" tagprefix="uc2" %>

<%@ Register src="UserControls/News.ascx" tagname="News" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>

<asp:content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
            <li><asp:LinkButton ID="btnMenu1" runat="server" 
                    onclick="btnMenu1_Click" CommandArgument="1" CausesValidation="false">Inquires</asp:LinkButton></li>
            <li><asp:LinkButton ID="btnMenu2" runat="server" onclick="btnMenu1_Click" CommandArgument="2" CausesValidation="false">News</asp:LinkButton></li>
            <li><asp:LinkButton ID="btnMenu3" runat="server" onclick="btnMenu1_Click" CommandArgument="3" CausesValidation="false">Add News</asp:LinkButton></li>
                
            </ul>
        </div>
    </div>
    
        <div class="warp">
        <div id="content">

        <div id="breadCrambs">
            <asp:Label ID="labelBreadCrambs" runat="server" />
        </div>

        <asp:ScriptManager ID="ScriptManager" runat="server" OnInit="scriptManagerOnInit" EnablePartialRendering="true" />
            
            
            
            
            
            
            
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate> 
            
            <uc1:PressInquires ID="ctrlPressInquires" runat="server" />
            <uc2:PressAddNews ID="ctrlPressAddNews" runat="server" Visible="false" />
            <uc3:News ID="ctrlNews" runat="server" />
             </ContentTemplate>        
        </asp:UpdatePanel>
    
        </div> 
    </div> 
</asp:Content>