<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="EditAdvertiser.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.EditAdvertiser" %>
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
                <li><a href="NewAdvertiser.aspx">New Advertiser</a></li>
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
                <h2>Advertiser's Information</h2>
                <div class="Section">        
                    <table>
                        <tr>
                            <td class="FieldHeader">E-mail:</td>
                            <td>
                                <asp:Label ID="labelEmail" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Status:*</td>
                            <td>
                                <asp:DropDownList ID="listStatus" CssClass="SelectField" runat="server" ValidationGroup="Form" DataValueField="Item_Id" DataTextField="Item_Desc" />
                                <asp:RequiredFieldValidator ID="validatorStatus" runat="server" ControlToValidate="listStatus" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
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
                                <asp:RequiredFieldValidator ID="validatorPaymentMethod1" 
                                    runat="server" ControlToValidate="listPaymentMethod" ValidationGroup="Form" InitialValue="0"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:CustomValidator ID="validatorPaymentMethod2"
                                    runat="server" ControlToValidate="listPaymentMethod" Display="Dynamic"
                                    OnServerValidate="checkPaymentMethod_ServerValidate" ValidationGroup="Form"
                                    CssClass="ValidationMessage" SetFocusOnError="true" />
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
                                <AjaxControlToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="textMinBillingAmount" ValidChars="0123456789" />
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

                <h2>Change Password</h2>
                <div class="Section">        
                    <table>
                        <tr>
                            <td class="FieldHeader">New Password:</td>
                            <td>
                                <asp:textbox ID="textPassword" runat="server" CssClass="InputField" MaxLength="50" TextMode="Password" onkeyup="runPassword(this.value,'Password_Strength');" />
                                <div style="width:100px;"> 
	                                <div id="Password_Strength_text" style="font-size:10px;"></div>
	                                <div id="Password_Strength_bar" style="font-size:1px; height:2px; width:0px; border:1px solid white;"></div> 
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Confirm Password:</td>
                            <td>
                                <asp:textbox ID="textPasswordConfirm" runat="server" CssClass="InputField" MaxLength="50" TextMode="Password" />
                            </td>
                        </tr>
                    </table>
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