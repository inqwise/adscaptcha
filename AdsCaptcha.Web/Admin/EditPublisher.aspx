<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="EditPublisher.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.EditPublisher" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
<%
    if (String.IsNullOrEmpty(Page.Request.QueryString["DeveloperId"]))
    {
%>
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_2').addClass('selected');
        });
    </script>    
<% 
    }
    else
    {
%>
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_4').addClass('selected');
        });
    </script>    
<%
    }
%>
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
            <%
                if (String.IsNullOrEmpty(Page.Request.QueryString["DeveloperId"]))
                {
            %>
                <li><a href="ReportPublishers.aspx">Report</a></li>
                <li><a href="ManagePublishers.aspx">Manage</a></li>
                <li><a href="PendingWebsites.aspx">Pending Websites</a></li>
                <li><a href="PublishersToBePaid.aspx">To Be Paid</a></li>
                <li><a href="NewPublisher.aspx">New Site Owner</a></li>
                <li><a href="ReportCountries.aspx">Countries Report</a></li>
                <li><a href="RandomImage.aspx">Random Images</a></li>
            <% 
                }
                else
                {
            %>
                <li><a href="ManageDevelopers.aspx">Manage</a></li>
                <li><a href="DevelopersToBePaid.aspx">To Be Paid</a></li>
                <li><a href="NewDeveloper.aspx">New Developer</a></li>
            <%
                }
            %>
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
                <h2>Site Owner's Information</h2>
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
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" />
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
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textMinCheckAmount" ValidChars="0123456789" />
                            </td>
                        </tr>                    
                        <tr>
                            <td class="FieldHeader">Developer Share:*</td>
                            <td>
                                <asp:textbox ID="textDevRevenueSharePct" runat="server" CssClass="InputField Width40" MaxLength="3" Text="" /> %
                                <asp:RequiredFieldValidator ID="validatorDevShareRevenue1"
                                    runat="server" ControlToValidate="textDevRevenueSharePct" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:RangeValidator ID="validatorDevShareRevenue2"
                                    runat="server" ControlToValidate="textDevRevenueSharePct" ValidationGroup="Form"
                                    MinimumValue="0" MaximumValue="100" Type="Integer"
                                    CssClass="ValidationMessage" ErrorMessage="* Must be between 0 to 100" Display="dynamic" SetFocusOnError="true" />
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textDevRevenueSharePct" ValidChars="0123456789" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Revenue Share:*</td>
                            <td>
                                <asp:textbox ID="textPubRevenueSharePct" runat="server" CssClass="InputField Width40" MaxLength="3" Text="" /> %
                                <asp:RequiredFieldValidator ID="validatorPubShareRevenue1"
                                    runat="server" ControlToValidate="textPubRevenueSharePct" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:RangeValidator ID="validatorPubShareRevenue2"
                                    runat="server" ControlToValidate="textPubRevenueSharePct" ValidationGroup="Form"
                                    MinimumValue="0" MaximumValue="100" Type="Integer"
                                    CssClass="ValidationMessage" ErrorMessage="* Must be between 0 to 100" Display="dynamic" SetFocusOnError="true" />
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textPubRevenueSharePct" ValidChars="0123456789" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Branded?*</td>
                            <td>
                                <asp:CheckBox ID="checkBranded" runat="server" />
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
                    <a id="buttonCancel" class="button" href="ManagePublishers.aspx?DeveloperId=<%=Request.QueryString["DeveloperId"]%>"><span>Cancel</span></a>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
        </div> 
    </div>

</asp:content>