<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaymentPreferencesPayPal.ascx.cs"
    Inherits="Inqwise.AdsCaptcha.Controls.PaymentPreferences.PaymentPreferencesPayPal" %>
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
                                Payee Account:
                            </td>
                            <td>
                                <asp:TextBox ID="textPayeeAccount" runat="server" CssClass="InputField" MaxLength="100" />
                                <asp:RegularExpressionValidator ID="validatorPayeeAccount" runat="server" ControlToValidate="textPayeeAccount"
                                    ValidationGroup="Form" SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    CssClass="ValidationMessage" ErrorMessage="* Not a valid e-mail" Display="dynamic" />
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
