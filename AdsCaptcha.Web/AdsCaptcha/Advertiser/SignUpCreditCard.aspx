<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Advertiser.Master" AutoEventWireup="true" CodeFile="SignUpCreditCard.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.SignUpCreditCard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<asp:content ContentPlaceHolderID="MainContent" runat="server">
    <div class="signupInfo">
        <div class="warp">
            In order to activate your campaign you need an active AdsCaptcha account.
        </div>
    </div>

    <div class="warp">
        <div id="content-form">
            <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="false" />        
            <h2>Credit Card Information</h2>
            <div class="Section">
                <asp:Label ID="labelError" runat="server" CssClass="cardError" Visible="false"></asp:Label>

                <table>
                    <tr>
                        <td class="FieldHeader">Type of Card:*</td>
                        <td>
                            <asp:DropDownList ID="listCreditCard" runat="server" CssClass="SelectField" 
                                DataTextField="Item_Desc" DataValueField="Item_Id" ValidationGroup="Form" />
                            <asp:RequiredFieldValidator ID="validatorCreditCard" 
                                runat="server" ControlToValidate="listCreditCard" ValidationGroup="Form" InitialValue="0"
                                CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                SetFocusOnError="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">Card Number:*</td>
                        <td>
                            <asp:textbox ID="textCardNumber" runat="server" CssClass="InputField" MaxLength="19" AutoCompleteType="None" autocomplete="off" />
                            <asp:RequiredFieldValidator ID="validatorCardNumber"
                                runat="server" ControlToValidate="textCardNumber" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                SetFocusOnError="true" />
                            <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textCardNumber" ValidChars=" -0123456789" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">Expiration Date:*</td>
                        <td>
                            <asp:DropDownList ID="listExpirationMonth" runat="server" CssClass="SelectField Width40" ValidationGroup="Form" />
                            /
                            <asp:DropDownList ID="listExpirationYear" runat="server" CssClass="SelectField Width60" ValidationGroup="Form" />
                        </td>
                    </tr>
                    <tr runat="server" id="amountHolder">
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
                <div id="ssl">
                    <span id="siteseal"><script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=<%=ConfigurationSettings.AppSettings["SSL_GoDaddy_Seal_Id"]%>"></script></span>
                </div>
            </div>
                            
            <div id="buttonHolder">                
                <asp:LinkButton id="buttonSubmit" runat="server" CssClass="button" OnClick="buttonSubmit_Click" CausesValidation="true" ValidationGroup="Form"><span>Pay Now</span></asp:LinkButton>
                <asp:LinkButton id="buttonCancel" runat="server" CssClass="button" OnClientClick="javascript:history.go(-1);"><span>Cancel</span></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:content>