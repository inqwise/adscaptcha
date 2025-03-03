<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="NewAdvertiser.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.NewAdvertiser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_3').addClass('selected');
        });
    </script>    
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li><a href="ReportAdvertisers.aspx">Report</a></li>
                <li><a href="ManageAdvertisers.aspx">Manage</a></li>
                <li><a href="AdvertisersToBeCharged.aspx">To Be Charge</a></li>
                <li><a href="NewAdvertiser.aspx" class="selected">New Advertiser</a></li>
            </ul>
        </div>
    </div>

    <div class="warp">
        <div id="content">

        <div id="breadCrambs">
            <asp:Label ID="labelBreadCrambs" runat="server" />
        </div>

        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>        
            <div>
                <h2>Account Information</h2>
                <div class="Section">        
                    <table>
                        <tr>
                            <td class="FieldHeader">E-mail:</td>
                            <td>
                                <asp:textbox ID="textEmail" runat="server" CssClass="InputField" MaxLength="100" Text="" />
                                <asp:RegularExpressionValidator ID="validatorEmail1"
                                    runat="server"
                                    ControlToValidate="textEmail" ValidationGroup="Form" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    CssClass="ValidationMessage" ErrorMessage="* Not a valid e-mail"
                                    Display="dynamic" SetFocusOnError="true" />
                                <asp:RequiredFieldValidator ID="validatorEmail2"
                                    runat="server" ControlToValidate="textEmail" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:CustomValidator ID="validatorEmail3"
                                    runat="server" ControlToValidate="textEmail" Display="Dynamic"
                                    OnServerValidate="checkEmailExsists_ServerValidate" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* User already exsists" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Password:*</td>
                            <td>
                                <asp:textbox ID="textPassword" runat="server" CssClass="InputField" MaxLength="50" TextMode="Password" onkeyup="runPassword(this.value,'Password_Strength');" />
                                <asp:RequiredFieldValidator ID="validatorPassword1" 
                                    runat="server" ControlToValidate="textPassword" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:RegularExpressionValidator ID="validatorPassword2" 
                                    runat="server" ControlToValidate="textPassword" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Minimum 5 letters" Display="dynamic"
                                    ValidationExpression=".{5}.*" SetFocusOnError="true" />
                                <div style="width:100px;"> 
	                                <div id="Password_Strength_text" style="font-size:10px;"></div>
	                                <div id="Password_Strength_bar" style="font-size:1px; height:2px; width:0px; border:1px solid white;"></div> 
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Confirm Password:*</td>
                            <td>
                                <asp:textbox ID="textPasswordConfirm" runat="server" CssClass="InputField" MaxLength="50" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="validatorConfirmPassword1" 
                                    runat="server" ControlToValidate="textPasswordConfirm" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:CompareValidator ID="validatorConfirmPassword2" 
                                    runat="server" ControlToValidate="textPasswordConfirm" ControlToCompare="textPassword"
                                    Type="String" Operator="Equal" ValidationGroup="Form" 
                                    CssClass="ValidationMessage" ErrorMessage="* Must be identical to password" SetFocusOnError="true" />
                            </td>
                        </tr>
                    </table>
                </div>
                                                                    
                <h2>Billing Preferences</h2>
                <div class="Section">
                    <table>
                        <tr>
                            <td class="FieldHeader">Payment Method:*</td>
                            <td>
                                <asp:DropDownList ID="listPaymentMethod" runat="server" CssClass="SelectField" ValidationGroup="Form" />
                                <asp:RequiredFieldValidator ID="validatorPaymentMethod" 
                                    runat="server" ControlToValidate="listPaymentMethod" ValidationGroup="Form" InitialValue="0"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Billing Method:*</td>
                            <td>
                                <asp:DropDownList ID="listBillingMethod" runat="server" CssClass="SelectField" ValidationGroup="Form" />
                                <asp:RequiredFieldValidator ID="validatorBillingMethod1" 
                                    runat="server" ControlToValidate="listBillingMethod" ValidationGroup="Form" InitialValue="0"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:CustomValidator ID="validatorBillingMethod2"
                                    runat="server" ControlToValidate="listBillingMethod" Display="Dynamic"
                                    OnServerValidate="checkBillingMethod_ServerValidate" ValidationGroup="Form"
                                    ErrorMessage="* Postpay is not permitted with Paypal payments."
                                    CssClass="ValidationMessage" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Min. Billing Amount:*</td>
                            <td>
                                $ <asp:textbox ID="textMinBillingAmount" runat="server" CssClass="InputField Width40" MaxLength="5" />
                                <asp:RequiredFieldValidator ID="validatorMinBillingAmount"
                                    runat="server" ControlToValidate="textMinBillingAmount" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" /> 
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textMinBillingAmount" ValidChars="0123456789" />
                            </td>
                        </tr>
                    </table>
                </div>

                <h2>Billing Address</h2>
                <div class="Section">
                    <table>
                        <tr>
                            <td class="FieldHeader">First Name:</td>
                            <td>
                                <asp:textbox ID="textFirstName" runat="server" CssClass="InputField" MaxLength="30" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Last Name:</td>
                            <td>
                                <asp:textbox ID="textLastName" runat="server" CssClass="InputField" MaxLength="30" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Company:</td>
                            <td>
                                <asp:textbox ID="textCompanyName" runat="server" CssClass="InputField" MaxLength="30" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Address:</td>
                            <td>
                                <asp:textbox ID="textAddress" runat="server" CssClass="InputField" MaxLength="100" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">City/Town:</td>
                            <td>
                                <asp:textbox ID="textCity" runat="server" CssClass="InputField" MaxLength="50" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Country:</td>
                            <td>
                                <asp:DropDownList ID="listCountry" runat="server" CssClass="SelectField" ValidationGroup="Form" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">State/Province:</td>
                            <td>
                                <asp:textbox ID="textState" runat="server" CssClass="InputField" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Zip/Postal Code:</td>
                            <td>
                                <asp:textbox ID="textZipCode" runat="server" CssClass="InputField" MaxLength="10" Text="" />
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textZipCode" ValidChars="0123456789" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Phone:</td>
                            <td>
                                <asp:textbox ID="textPhone" runat="server" CssClass="InputField" MaxLength="30" Text="" />
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textPhone" ValidChars="+-0123456789" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Fax:</td>
                            <td>
                                <asp:textbox ID="textFax" runat="server" CssClass="InputField" MaxLength="30" Text="" />
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textFax" ValidChars="+-0123456789" />
                            </td>
                        </tr>
                    </table>
                </div>

                <h2>E-mail Preferences</h2>
                <div class="Section">
                    <asp:CheckBox ID="checkGetEmailAnnouncements" runat="server" />
                    Send E-mail service announcements that relate to your agreement with AdsCaptcha.

                    <br />
                    
                    <asp:CheckBox ID="checkGetEmailNewsletters" runat="server" />
                    Send periodic newsletters with tips and best practices and occasional surveys to help us improve AdsCaptcha.
                </div>

                <div id="buttonHolder">
                    <asp:ImageButton ID="buttonSubmit" runat="server" ImageUrl="images/submit.gif"
                        CausesValidation="true" ValidationGroup="Form" onclick="buttonSubmit_Click" />
                    <a id="buttonCancel" class="button" href="ManageAdvertisers.aspx"><span>Cancel</span></a>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>    
    
        </div> 
    </div>

</asp:content>