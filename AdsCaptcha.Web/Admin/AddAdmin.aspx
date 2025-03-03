<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="AddAdmin.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.AddAdmin" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_7').addClass('selected');
        });
    </script>    
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li><a href="ManageAdmins.aspx">Manage</a></li>
                <li><a href="AddAdmin.aspx" class="selected">Add Admin</a></li>
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
                <h2>Admin's Information</h2>
                <div class="Section">
                    <table>
                        <tr>
                            <td class="FieldHeader">Name:*</td>
                            <td>
                                <asp:textbox ID="textName" runat="server" CssClass="InputField" MaxLength="100" />
                                <asp:RequiredFieldValidator ID="rfvTextName" runat="server" ControlToValidate="textName" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">E-mail:*</td>
                            <td>
                                <asp:textbox ID="textEmail" runat="server" CssClass="InputField" MaxLength="100" ValidationGroup="Form" />
                                <asp:RegularExpressionValidator ID="revTextEmail" runat="server" SetFocusOnError="true"
                                    ControlToValidate="textEmail" ValidationGroup="Form" 
                                    ValidationExpression=".*@.*\..*"
                                    CssClass="ValidationMessage" ErrorMessage="* Not a valid e-mail"
                                    Display="dynamic" />
                                <asp:RequiredFieldValidator ID="rfvTextEmail" runat="server" ControlToValidate="textEmail" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:CustomValidator ID="cvTextEmail" runat="server" ControlToValidate="textEmail" Display="Dynamic"
                                    OnServerValidate="checkEmailExsists_ServerValidate" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* User already exsists" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Password:*</td>
                            <td>
                                <asp:textbox ID="textPassword" runat="server" CssClass="InputField" MaxLength="50" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="rfvTextPassword" runat="server" ControlToValidate="textPassword" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:RegularExpressionValidator ID="revTextPassword" runat="server" ControlToValidate="textPassword" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Minimum 5 letters" Display="dynamic"
                                    ValidationExpression=".{5}.*" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Confirm Password:*</td>
                            <td>
                                <asp:textbox ID="textPasswordConfirm" runat="server" CssClass="InputField" MaxLength="50" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="rfvTextPasswordConfirm" runat="server" 
                                    ControlToValidate="textPasswordConfirm" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:CompareValidator ID="cvTextPasswordConfirm" runat="server"
                                    ControlToValidate="textPasswordConfirm" ControlToCompare="textPassword"
                                    Type="String" Operator="Equal" ValidationGroup="Form" 
                                    CssClass="ValidationMessage" ErrorMessage="* Must be identical to password" SetFocusOnError="true" />
                            </td>
                        </tr>
                    </table>
                </div>
                        
                <div id="buttonHolder">
                    <asp:ImageButton ID="buttonSubmit" runat="server" ImageUrl="Images/submit.gif"
                        CausesValidation="true" ValidationGroup="Form" onclick="buttonSubmit_Click" />
                    <a id="buttonCancel" class="button" href="ManageAdmins.aspx"><span>Cancel</span></a>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>    
        
        </div>
    </div>

</asp:content>