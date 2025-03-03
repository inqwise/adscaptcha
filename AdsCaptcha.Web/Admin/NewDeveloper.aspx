<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="NewDeveloper.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.NewDeveloper" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_4').addClass('selected');
        });
    </script>    
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li><a href="ManageDevelopers.aspx">Manage</a></li>
                <li><a href="DevelopersToBePaid.aspx">To Be Paid</a></li>
                <li><a href="NewDeveloper.aspx" class="selected">New Developer</a></li>
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
                            <td class="FieldHeader">E-mail:*</td>
                            <td>
                                <asp:textbox ID="textEmail" runat="server" CssClass="InputField" MaxLength="100" ValidationGroup="Form" />
                                <asp:RegularExpressionValidator ID="validatorEmail1"
                                    runat="server"
                                    ControlToValidate="textEmail" ValidationGroup="Form" SetFocusOnError="true" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    CssClass="ValidationMessage" ErrorMessage="* Not a valid e-mail"
                                    Display="dynamic" />
                                <asp:RequiredFieldValidator ID="validatorEmail2"
                                    runat="server" ControlToValidate="textEmail" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:CustomValidator ID="validatorEmail3"
                                    runat="server" ControlToValidate="textEmail" Display="Dynamic"
                                    OnServerValidate="checkEmailExsists_ServerValidate" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* User already exsists" SetFocusOnError="true" />
                                <asp:CustomValidator ID="validatorEmail4"
                                    runat="server" ControlToValidate="textEmail" Display="Dynamic"
                                    OnServerValidate="checkWaitForActivation_ServerValidate" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* User waiting for activation" SetFocusOnError="true" />
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

                <h2>Personal Information</h2>
                <div class="Section">        
                    <table>
                        <tr>
                            <td class="FieldHeader">First Name:*</td>
                            <td>
                                <asp:textbox ID="textFirstName" runat="server" CssClass="InputField" MaxLength="30" Text="" />
                                <asp:RequiredFieldValidator ID="validatorFirstName"
                                    runat="server" ControlToValidate="textFirstName" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Last Name:*</td>
                            <td>
                                <asp:textbox ID="textLastName" runat="server" CssClass="InputField" MaxLength="30" Text="" />
                                <asp:RequiredFieldValidator ID="validatorLastName"
                                    runat="server" ControlToValidate="textLastName" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Company Name:</td>
                            <td>
                                <asp:textbox ID="textCompanyName" runat="server" CssClass="InputField" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Country:*</td>
                            <td>
                                <asp:DropDownList ID="listCountry" runat="server" CssClass="SelectField" DataTextField="Country_Name" DataValueField="Country_Id" ValidationGroup="Form" />
                                <asp:RequiredFieldValidator ID="validatorCountry"
                                    runat="server" ControlToValidate="listCountry" ValidationGroup="Form" InitialValue="0"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Address:*</td>
                            <td>
                                <asp:textbox ID="textAddress" runat="server" CssClass="InputField" MaxLength="100" />
                                <asp:RequiredFieldValidator ID="validatorAddress"
                                    runat="server" ControlToValidate="textAddress" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">City/Town:*</td>
                            <td>
                                <asp:textbox ID="textCity" runat="server" CssClass="InputField" MaxLength="50" />
                                <asp:RequiredFieldValidator ID="validatorCity"
                                    runat="server" ControlToValidate="textCity" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" />
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
                                <asp:textbox ID="textZipCode" runat="server" CssClass="InputField" MaxLength="10" />
                                <AjaxControlToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="textZipCode" ValidChars="0123456789" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Phone Number 1:*</td>
                            <td>
                                <asp:textbox ID="textPhone" runat="server" CssClass="InputField" MaxLength="30" />
                                <asp:RequiredFieldValidator ID="validatorPhone"
                                    runat="server" ControlToValidate="textPhone" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" /> 
                                <AjaxControlToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="textPhone" ValidChars="+-0123456789" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Phone Number 2:</td>
                            <td>
                                <asp:textbox ID="textPhone2" runat="server" CssClass="InputField" MaxLength="30" />
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textPhone2" ValidChars="+-0123456789" />
                            </td>
                        </tr>
                        <tr>                
                            <td class="FieldHeader">Min. Check Amount:*</td>
                            <td>
                                $ <asp:textbox ID="textMinCheckAmount" runat="server" CssClass="InputField Width40" MaxLength="5" Text="" />
                                <asp:RequiredFieldValidator ID="validatorMinCheckAmount1"
                                    runat="server" ControlToValidate="textMinCheckAmount" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:RangeValidator ID="validatorMinCheckAmount2"
                                    runat="server" ControlToValidate="textMinCheckAmount" ValidationGroup="Form"
                                    CssClass="ValidationMessage" Display="dynamic" SetFocusOnError="true" />
                                <AjaxControlToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="textMinCheckAmount" ValidChars="0123456789" />
                            </td>
                        </tr>
                        <tr>                
                            <td class="FieldHeader">Share Revenue:*</td>
                            <td>
                                <asp:textbox ID="textRevenueSharePct" runat="server" CssClass="InputField Width40" MaxLength="3" Text="" /> %
                                <asp:RequiredFieldValidator ID="validatorShareRevenue1"
                                    runat="server" ControlToValidate="textRevenueSharePct" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:RangeValidator ID="validatorShareRevenue2"
                                    runat="server" ControlToValidate="textRevenueSharePct" ValidationGroup="Form"
                                    MinimumValue="0" MaximumValue="100" Type="Integer"
                                    CssClass="ValidationMessage" Display="dynamic" SetFocusOnError="true" />
                                <AjaxControlToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="textRevenueSharePct" ValidChars="0123456789" />
                            </td>
                        </tr>            
                    </table>
                </div>
                
                <h2>Payment Preferences</h2>
                <div class="Section">
                    <table>
                        <tr>
                            <td class="FieldHeader">Payment Method:*</td>
                            <td>
                                <asp:DropDownList ID="listCreditMethod" runat="server" CssClass="SelectField" DataTextField="Item_Desc" DataValueField="Item_Id" ValidationGroup="Form" />
                                <asp:RequiredFieldValidator ID="validatorCreditMethod"
                                    runat="server" ControlToValidate="listCreditMethod" ValidationGroup="Form" InitialValue="0"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                    </table>
                </div>

                <h2>E-mail Preferences</h2>
                <div class="Section">
                    <asp:CheckBox ID="checkGetEmailAnnouncements" runat="server" Checked="true" />
                    I would like to receive E-mail service announcements about my agreement with AdsCaptcha.

                    <br />
                    
                    <asp:CheckBox ID="checkGetEmailNewsletters" runat="server" Checked="true" />
                    I would like to receive periodic newsletters with advice, news and occasional surveys to help me improve my AdsCaptcha.
                </div>
                
                <div id="buttonHolder">
                    <asp:ImageButton ID="buttonSubmit" runat="server" ImageUrl="images/submit.gif"
                        CausesValidation="true" ValidationGroup="Form" onclick="buttonSubmit_Click" />
                    <a id="buttonCancel" class="button" href="ManageDevelopers.aspx"><span>Cancel</span></a>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        </div> 
    </div>

</asp:content>