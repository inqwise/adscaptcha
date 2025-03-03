<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="EditAdmin.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.EditAdmin" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_7').addClass('selected');
        });
    </script>    
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

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
                                    CssClass="ValidationMessage" ErrorMessage="* Admin already exsists" SetFocusOnError="true" />
                            </td>
                        </tr>
                    </table>
                </div>
                        
                <div id="buttonHolder">
                    <asp:ImageButton ID="buttonSubmit" runat="server" ImageUrl="images/submit.gif"
                        CausesValidation="true" ValidationGroup="Form" onclick="buttonSubmit_Click" />
                    <a id="buttonCancel" class="button" href="ManageAdmins.aspx"><span>Cancel</span></a>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>    
    
        </div> 
    </div>

</asp:content>