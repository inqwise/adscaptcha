<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Advertiser/AdvertiserAccount.Master" AutoEventWireup="true" CodeFile="BillingPayPal.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.BillingPayPal" %>
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
PayPal
</asp:Content>
<asp:content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div id="content">
               <div class="inner-content">
               <br />
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="panelMessage" runat="server" Visible="false">
                    <asp:Label ID="labelMessage" runat="server"></asp:Label>
                </asp:Panel>

                <asp:Panel ID="panelForm" runat="server" Visible="true">
                    <p class="Explanation" style="font-size:14px;">
                    Follow the prompts to pay at the Paypal site. Once you have completed the payment, you will be redirected automatically back to your Inqwise account.
                    </p>
<br />
                  

                    <h4>Add Credit</h4>
                    <div class="description">
                        <table>
                            <tr>
                                <td class="FieldHeader">Amount limit:*</td>
                                <td>
                                    $ <asp:textbox ID="textAmount" runat="server" CssClass="InputField Width60" MaxLength="6" AutoCompleteType="None" />
                                    <asp:RequiredFieldValidator ID="validatorAmount1" 
                                        runat="server" ControlToValidate="textAmount" ValidationGroup="Form"
                                        CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                        EnableClientScript="false" SetFocusOnError="true" />
                                    <asp:CustomValidator ID="validatorAmount2"
                                        runat="server" ControlToValidate="textAmount" Display="Dynamic"
                                        OnServerValidate="checkAmount_ServerValidate" ValidationGroup="Form"
                                        CssClass="ValidationMessage" SetFocusOnError="true" />
                                    <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textAmount" ValidChars="0123456789" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    
                      <h4 style="display:none;">Billing Address</h4>
                    <div class="description" style="display:none;">
                        <table>
                            <tr>
                                <td class="FieldHeader">First Name:</td>
                                <td>
                                    <asp:Label ID="labelFirstName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldHeader">Last Name:</td>
                                <td>
                                    <asp:Label ID="labelLastName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldHeader">Company:</td>
                                <td>
                                    <asp:Label ID="labelCompany" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldHeader">Address:</td>
                                <td>
                                    <asp:Label ID="labelAddress" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldHeader">City/Town:</td>
                                <td>
                                    <asp:Label ID="labelCity" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldHeader">Country:</td>
                                <td>
                                    <asp:Label ID="labelCountry" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldHeader">State/Province:</td>
                                <td>
                                    <asp:Label ID="labelState" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldHeader">Zip/Postal Code:</td>
                                <td>
                                    <asp:Label ID="labelZipCode" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldHeader">Phone:</td>
                                <td>
                                    <asp:Label ID="labelPhone" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldHeader">Fax:</td>
                                <td>
                                    <asp:Label ID="labelFax" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                                        
                    <div id="buttonHolder" style="clear:both;margin-top:10px;">                
                        <asp:LinkButton id="buttonSubmit" runat="server" style="color:#FFFFFF;" CssClass="btn" OnClick="buttonSubmit_Click" CausesValidation="true" ValidationGroup="Form"><span>Pay Now</span></asp:LinkButton>
                        <a id="buttonCancel" style="color:#FFFFFF;" class="btn" href="BillingSummary.aspx"><span>Cancel</span></a>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>    
        </div>
    </div>
</asp:content>  