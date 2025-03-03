<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Advertiser.Master" AutoEventWireup="true" CodeFile="SetCreditCard.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.SetCreditCard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<asp:content ContentPlaceHolderID="MainContent" runat="server">
    <div id="navigation">
        <div class="navigation">
            <ul>
                <li><a href="ManageCampaigns.aspx">Campaigns</a></li>
                <li><a href="BillingSummary.aspx">Billing</a></li>                
                <li class="selected"><a href="AccountPreferences.aspx">My Account</a></li>
            </ul>
        </div>
    </div>
    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li><a href="AccountPreferences.aspx">Account Preferences</a></li>
                <li><a href="ChangePassword.aspx">Change Password</a></li>
            </ul>
        </div>
    </div>
    
    <div class="warp">
        <div id="breadCrambs">
            <asp:Label ID="labelNavigationPath" runat="server" />        
        </div>

        <div id="content-form">
            <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="false" />
            <asp:Panel ID="panelMessage" runat="server" Visible="false">
                <asp:Label ID="labelMessage" runat="server"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="panelForm" runat="server" Visible="true">
                <h2>Set Credit Card</h2>
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
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textCardNumber" ValidChars="-0123456789" />
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
                    </table>
                    <div id="ssl">
                        <span id="siteseal"><script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=<%=ConfigurationSettings.AppSettings["SSL_GoDaddy_Seal_Id"]%>"></script></span>
                    </div>
                
                    <div id="buttonHolder">                
                        <asp:LinkButton id="buttonSubmit" runat="server" CssClass="button" OnClick="buttonSubmit_Click" CausesValidation="true" ValidationGroup="Form"><span>Submit</span></asp:LinkButton>
                        <asp:LinkButton id="buttonCancel" runat="server" CssClass="button" OnClientClick="javascript:history.go(-1);"><span>Cancel</span></asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:content>  