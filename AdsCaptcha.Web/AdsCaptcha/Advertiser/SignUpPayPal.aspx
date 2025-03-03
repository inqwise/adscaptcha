<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Advertiser/Advertiser.Master" AutoEventWireup="true" CodeFile="SignUpPayPal.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.SignUpPayPal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<asp:content ContentPlaceHolderID="MainContent" runat="server">
    <div class="signupInfo">
        <div class="warp">
            In order to activate your campaign you need add funds to your AdsCaptcha account using PayPal.
            <br />
            Please choose the amount you wish to add and 
        </div>
    </div>

    <div class="warp">
        <div id="content-form">
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
        
            <p>
            Follow the prompts to pay at the Paypal site. Once you have completed the payment, you will be redirected automatically back to your AdsCaptcha account.
            </p>

            <h2>PayPal</h2>
            <div class="Section">
                <table>
                    <tr>
                        <td class="FieldHeader">Amount To Charge:*</td>
                        <td>
                            $ <asp:textbox ID="textAmount" runat="server" CssClass="InputField Width60" MaxLength="6" AutoCompleteType="None" />
                            <asp:RequiredFieldValidator ID="validatorAmount1"
                                runat="server" ControlToValidate="textAmount" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                SetFocusOnError="true" />
                            <asp:CustomValidator ID="validatorAmount2"
                                runat="server" ControlToValidate="textAmount" Display="Dynamic"
                                OnServerValidate="checkAmount_ServerValidate" ValidationGroup="Form"
                                CssClass="ValidationMessage" SetFocusOnError="true" />
                            <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textAmount" ValidChars="0123456789" />
                        </td>
                    </tr>
                </table>
            </div>
                            
            <div id="buttonHolder">                
                <asp:LinkButton id="buttonSubmit" runat="server" CssClass="button" OnClick="buttonSubmit_Click" CausesValidation="true" ValidationGroup="Form"><span>Pay Now</span></asp:LinkButton>
                <asp:LinkButton id="buttonCancel" runat="server" CssClass="button" OnClientClick="javascript:history.go(-1);"><span>Cancel</span></asp:LinkButton>
            </div>
            
            </ContentTemplate>
        </asp:UpdatePanel>    
        </div>
    </div>
</asp:content>