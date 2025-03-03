<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="CreditDeveloperManual.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.CreditDeveloperManual" %>
<%@ Register assembly="ComponentArt.Web.UI" namespace="ComponentArt.Web.UI" tagprefix="ComponentArt" %>
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
                <li><a href="ManageDevelopers.aspx" class="selected">Manage</a></li>
                <li><a href="DevelopersToBePaid.aspx">To Be Paid</a></li>
                <li><a href="NewDeveloper.aspx">New Developer</a></li>
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
                <h2>Credit Developer</h2>
                <div class="Section">
                    <table>
                        <tr>
                            <td class="FieldHeader">Min. Check Amount:</td>
                            <td>
                                <asp:Label ID="labelMinCheckAmount" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Current Balance:</td>
                            <td>
                                <asp:Label ID="labelBalance" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Amount To Credit:</td>
                            <td>                                
                                $ <asp:textbox ID="textAmountToCredit" runat="server" CssClass="InputField Width60" MaxLength="10" />
                                <asp:RequiredFieldValidator ID="validatorAmountToCredit1" 
                                    runat="server" ControlToValidate="textAmountToCredit" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" />
                                <asp:RangeValidator ID="validatorAmountToCredit2"
                                    runat="server" ControlToValidate="textAmountToCredit" ValidationGroup="Form"
                                    MaximumValue="99999" MinimumValue="1" Type="Double"
                                    CssClass="ValidationMessage" ErrorMessage="* Min. $1" Display="dynamic" SetFocusOnError="true" />
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textAmountToCredit" ValidChars="0123456789." />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Additional Data:</td>
                            <td>
                                <asp:TextBox ID="textAdditionalData" runat="server" MaxLength="500" TextMode="MultiLine" Rows="5" Columns="20" CssClass="InputField"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>

                <div id="buttonHolder">
                    <asp:ImageButton ID="buttonSubmit" runat="server" ImageUrl="images/submit.gif" CausesValidation="true" ValidationGroup="Form" onclick="buttonSubmit_Click" />
                    <a id="buttonCancel" class="button" href="DevelopersToBePaid.aspx"><span>Cancel</span></a>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
        </div>
    </div>

</asp:content>