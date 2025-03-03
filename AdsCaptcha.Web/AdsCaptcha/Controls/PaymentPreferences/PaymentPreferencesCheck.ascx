<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaymentPreferencesCheck.ascx.cs"
    Inherits="Inqwise.AdsCaptcha.Controls.PaymentPreferences.PaymentPreferencesCheck" %>
<%@ Import Namespace="Inqwise.AdsCaptcha.SystemFramework" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
    
<div class="warp">
    <div id="content-form">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <div class="Section">
                    Choose where you want us to send you your revenue payments.
                    <br />
                    If you do not have this information at the moment, you may enter it later.
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td class="FieldHeader">
                                Payee Name:
                            </td>
                            <td>
                                <asp:TextBox ID="textPayeeName" runat="server" CssClass="InputField" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">
                                Recipient Name:
                            </td>
                            <td>
                                <asp:TextBox ID="textRecipientName" runat="server" CssClass="InputField" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">
                                Address:
                            </td>
                            <td>
                                <asp:TextBox ID="textAddress" runat="server" CssClass="InputField" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">
                                City:
                            </td>
                            <td>
                                <asp:TextBox ID="textCity" runat="server" CssClass="InputField" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">
                                Country:
                            </td>
                            <td>
                                <asp:TextBox ID="textCountry" runat="server" CssClass="InputField" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">
                                State:
                            </td>
                            <td>
                                <asp:TextBox ID="textState" runat="server" CssClass="InputField" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">
                                Zip/Postal Code:
                            </td>
                            <td>
                                <asp:TextBox ID="textZipCode" runat="server" CssClass="InputField" MaxLength="10"
                                    Text="" />
                                <AjaxControlToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    TargetControlID="textZipCode" ValidChars="0123456789" />
                            </td>
                        </tr>
                    </table>
                    Your payment will be delivered monthly as long as your balance is over the $<%=ApplicationConfiguration.DEFAULT_MIN_CHECK_AMOUNT.ToString()%>
                    minimum.
                </div>
                <div class="Section">
                    <asp:Label ID="labelChangesSaved" runat="server" CssClass="ChangesSaved" Text="Changes saved successfully."
                        Visible="false" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
