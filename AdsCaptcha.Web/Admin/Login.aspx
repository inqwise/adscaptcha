<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.Login" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">
    <div class="gradient_bg">
        <div id="loginFrame">
            <table cellspacing="5" cellpadding="0" align="center" style="margin-left:30px;">
                <tr>
                    <td align="left" colspan="2"><div class="text">To access this site, you need to login:</div></td>
                </tr>
                <tr>
                    <td>E-mail:</td>
                    <td><asp:textbox id="textEmail" runat="server" MaxLength="100" Width="200px"></asp:textbox></td>
                </tr>
                <tr>
                    <td>Password:</td>
                    <td><asp:textbox id="textPassword" runat="server" MaxLength="50" Width="200px" TextMode="Password"></asp:textbox></td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:label id="labelMessage" runat="server" CssClass="errorMessage"></asp:label>
                    </td>
                </tr>
            </table>
            <table cellspacing="5" cellpadding="0" align="center" style="margin-left:30px;">
                <tr>
                    <td valign="top" align="left">
                        <asp:LinkButton id="buttonLogin" runat="server" CssClass="buttonSignIn" onclick="buttonLogin_Click"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>        
    </div>
</asp:content>