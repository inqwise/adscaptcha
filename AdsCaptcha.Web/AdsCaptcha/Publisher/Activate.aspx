<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Publisher.Master" AutoEventWireup="true" CodeFile="Activate.aspx.cs" Inherits="Inqwise.AdsCaptcha.Publisher.Activate" %>

<asp:content ContentPlaceHolderID="MainContent" runat="server">
    <div class="warp">
        <div id="messageHolder">
            <asp:Panel ID="panelError" runat="server" Visible="false">
                Activation code not found or already activated.
                <br />
                <br />                
                If you already activated your account, click <a href="Login.aspx" target="_self">here</a> to login.
            </asp:Panel>
            <asp:Panel ID="panelActivated" runat="server" Visible="true">
                <b>Welcome to AdsCaptcha!</b>
                <br />
                <br />
                You successfully activated your account. 
                <br />
                <br />
                Now, sign your website and add an AdsCaptcha to your website!
                <br />
                <br />
                <br />
                To continute, click <a href="Login.aspx?Activation=1" target="_self">here</a>.
            </asp:Panel>
        </div>
    </div>
</asp:content>