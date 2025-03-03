<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaymentPreferencesBank.ascx.cs"
    Inherits="Inqwise.AdsCaptcha.Controls.PaymentPreferences.PaymentPreferencesBank" %>
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
                                Bank Name:
                            </td>
                            <td>
                                <asp:TextBox ID="textBankName" runat="server" CssClass="InputField" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">
                                Branch Name:
                            </td>
                            <td>
                                <asp:TextBox ID="textBranchName" runat="server" CssClass="InputField" MaxLength="50" />
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
                                Account Holder:
                            </td>
                            <td>
                                <asp:TextBox ID="textHolderName" runat="server" CssClass="InputField" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">
                                Account Number:
                            </td>
                            <td>
                                <asp:TextBox ID="textAccountNumber" runat="server" CssClass="InputField" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">
                                IBAN / SWIFT / Sort code:
                            </td>
                            <td>
                                <asp:TextBox ID="textCode" runat="server" CssClass="InputField" MaxLength="40" />
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
