<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="CreditDeveloper.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.CreditDeveloper" %>
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
                            <td class="FieldHeader">Current:</td>
                            <td>
                                <asp:Label ID="labelCreditMethod" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Credit Method:</td>
                            <td>
                                <asp:RadioButtonList ID="radioCreditMethod" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>

                <div id="buttonHolder">
                    <asp:ImageButton ID="buttonSubmit" runat="server" ImageUrl="images/submit.gif"
                        CausesValidation="true" ValidationGroup="Form" onclick="buttonSubmit_Click" />
                    <a id="buttonCancel" class="button" href="DevelopersToBePaid.aspx"><span>Cancel</span></a>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
        </div>
    </div>

</asp:content>