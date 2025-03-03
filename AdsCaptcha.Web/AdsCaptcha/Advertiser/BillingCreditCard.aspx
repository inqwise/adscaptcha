<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Advertiser/AdvertiserAccount.Master" AutoEventWireup="true" CodeFile="BillingCreditCard.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.BillingCreditCard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
   .InputField, .TextareaField
   {
   border: 2px solid #DEDEDD;
    border-radius: 3px 3px 3px 3px;
    color: #333333;
    font: 20px Arial,Helvetica,sans-serif;
    padding: 6px;
    width: 200px;
   }
   
   .SelectField
   {
   	width: auto;
   }
   
   #buttonHolder
   {
   	margin-top:30px;
   	padding-bottom: 50px;
   }
   
   .buttonHolder a
   {
   	color:#FFFFFF;
   }
   
   .container {
    /*background: -moz-radial-gradient(center center , ellipse farthest-corner, #FFFFFF 0%, #EAEAEA 100%) repeat scroll 0 0 transparent;
    border: 2px solid #FFFFFF;
    box-shadow: 0 5px 22px #BBBBBB;
    height: 370px;*/
    margin: 0px auto;
    overflow: hidden;
    padding: 50px 0  0 10px;
    position: relative;
    width: auto;
}

#content h4
{
	background-position: left 4px !important;
    margin-bottom: 30px;
    line-height: 1;
}

.FieldHeader
{
	white-space:nowrap;
}
#content .inner-content, #content-form {
        padding: 0px 0 40px 0;
    margin: 0 auto;
    width: 950px;
}
   </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Credit Card
</asp:Content>
<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <!--div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li><a href="BillingSummary.aspx">Billing Summary</a></li>
            </ul>
        </div>
    </div>
    

        <div id="breadCrambs">
            <asp:Label ID="labelNavigationPath" runat="server" />        
        </div-->

        <div id="content" class="container">
               <div class="inner-content">
               <br />
            <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="false" />
            <asp:Panel ID="panelForm" runat="server" Visible="true"  CssClass="description" style="margin-left:20px">                    
                <h4><asp:Literal ID="labelTitle" runat="server"></asp:Literal></h4>
                <div>                    
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
                                <AjaxControlToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="textCardNumber" ValidChars="-0123456789" />
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
                        <tr id="amountHolder" runat="server">
                            <td class="FieldHeader">Amount limit:*</td>
                            <td>
                                $ <asp:textbox ID="textAmount" runat="server" CssClass="InputField Width60" MaxLength="6" AutoCompleteType="None" />
                                <asp:CustomValidator ID="validatorAmount"
                                    runat="server" ControlToValidate="textAmount" Display="Dynamic"
                                    OnServerValidate="checkAmount_ServerValidate" ValidationGroup="Form"
                                    CssClass="ValidationMessage" SetFocusOnError="true" ValidateEmptyText="true" />
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textAmount" ValidChars="0123456789" />
                            </td>
                        </tr>
                    </table>
                    <div id="ssl" style="float:left;margin:20px 0 20px 0;">
                        <span id="siteseal"><script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=<%=ConfigurationSettings.AppSettings["SSL_GoDaddy_Seal_Id"]%>"></script></span>
                    </div>
                </div>
<br />
                <div id="buttonHolder" style="clear:both;margin-top:10px;">                
                    <asp:LinkButton id="buttonSubmit" runat="server"  style="color:#FFFFFF;" CssClass="btn" OnClick="buttonSubmit_Click" CausesValidation="true" ValidationGroup="Form"><span>Submit</span></asp:LinkButton>
                    <a id="buttonCancel" class="btn"  style="color:#FFFFFF;" href="BillingSummary.aspx"><span>Cancel</span></a>
                </div>
            </asp:Panel>
        
            <asp:Panel ID="panelMessage" runat="server" Visible="false" CssClass="description" style="margin-left:20px;">
                <div id="messageHolder">
                    <asp:Label ID="labelMessage" runat="server"></asp:Label>
                    <br />
                    <br />
                    Continue to <a href="ManageCampaigns.aspx">Manage Campaigns</a>, <a href="BillingSummary.aspx">Billing Summary</a> or <a href="AccountPreferences.aspx">My Account</a> page.
                </div>
            </asp:Panel>
        </div>
</div>
</asp:content>  