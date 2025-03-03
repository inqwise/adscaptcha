<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="CreditPublisherCheck.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.CreditPublisherCheck" %>
<%@ Register assembly="ComponentArt.Web.UI" namespace="ComponentArt.Web.UI" tagprefix="ComponentArt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_2').addClass('selected');
        });
    </script>    
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li><a href="ReportPublishers.aspx">Report</a></li>
                <li><a href="ManagePublishers.aspx">Manage</a></li>
                <li><a href="PendingWebsites.aspx">Pending Websites</a></li>
                <li><a href="PublishersToBePaid.aspx">To Be Paid</a></li>
                <li><a href="NewPublisher.aspx">New Site Owner</a></li>
                <li><a href="ReportCountries.aspx">Countries Report</a></li>
                <li><a href="RandomImage.aspx">Random Images</a></li>
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
                <h2>Credit Site Owner</h2>
                <div class="Section">
                    <table>
                        <tr>
                            <td class="FieldHeader">Payee Name:</td>
                            <td>
                                <asp:textbox ID="textPayeeName" runat="server" CssClass="InputField" MaxLength="50" />
                                <asp:RequiredFieldValidator ID="validatorPayeeName" 
                                    runat="server" ControlToValidate="textPayeeName" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Recipient Name:</td>
                            <td>
                                <asp:textbox ID="textRecipientName" runat="server" CssClass="InputField" MaxLength="50" />
                                <asp:RequiredFieldValidator ID="validatorRecipientName" 
                                    runat="server" ControlToValidate="textRecipientName" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Address:</td>
                            <td>
                                <asp:textbox ID="textAddress" runat="server" CssClass="InputField" MaxLength="100" />
                                <asp:RequiredFieldValidator ID="validatorAddress" 
                                    runat="server" ControlToValidate="textAddress" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">City/Town:</td>
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
                                <asp:RequiredFieldValidator ID="validatorState" 
                                    runat="server" ControlToValidate="textState" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Country:</td>
                            <td>
                                <asp:textbox ID="textCountry" runat="server" CssClass="InputField" MaxLength="50" />
                                <asp:RequiredFieldValidator ID="validatorCountry" 
                                    runat="server" ControlToValidate="textCountry" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Zip/Postal Code:</td>
                            <td>
                                <asp:textbox ID="textZipCode" runat="server" CssClass="InputField" MaxLength="10" />
                                <asp:RequiredFieldValidator ID="validatorZipCode" 
                                    runat="server" ControlToValidate="textZipCode" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" />
                            </td>
                        </tr>
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
                            <td class="FieldHeader">Check Number:</td>
                            <td>
                                <asp:textbox ID="textCheckNumber" runat="server" CssClass="InputField" MaxLength="50" />
                                <asp:RequiredFieldValidator ID="validatorCheckNumber" 
                                    runat="server" ControlToValidate="textCheckNumber" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" />
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textCheckNumber" ValidChars="0123456789" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Auth. Number:</td>
                            <td>
                                <asp:textbox ID="textAuthNumber" runat="server" CssClass="InputField" MaxLength="100" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Additional Data:</td>
                            <td>
                                <asp:TextBox ID="textAdditionalData" runat="server" MaxLength="500" TextMode="MultiLine" Rows="5" Columns="20" CssClass="InputField"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div id="ssl">
                        <span id="siteseal"><script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=<%=ConfigurationSettings.AppSettings["SSL_GoDaddy_Seal_Id"]%>"></script></span>
                    </div>
                </div>

                <div id="buttonHolder">
                    <asp:ImageButton ID="buttonSubmit" runat="server" ImageUrl="images/submit.gif"
                        CausesValidation="true" ValidationGroup="Form" onclick="buttonSubmit_Click" />
                    <a id="buttonCancel" class="button" href="PublishersToBePaid.aspx"><span>Cancel</span></a>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
        </div>
    </div>

</asp:content>