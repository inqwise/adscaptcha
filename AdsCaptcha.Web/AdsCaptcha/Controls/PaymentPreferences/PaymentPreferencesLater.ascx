<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaymentPreferencesLater.ascx.cs"
    Inherits="Inqwise.AdsCaptcha.Controls.PaymentPreferences.PaymentPreferencesLater" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<div class="warp">
    <div id="content-form">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <div class="Section">
                    <asp:Label ID="labelChangesSaved" runat="server" CssClass="ChangesSaved" Text="Changes saved successfully."
                        Visible="false" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>