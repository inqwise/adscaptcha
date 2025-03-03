<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="ChargeAdvertiserManual.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.ChargeAdvertiserManual" %>
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
                <h2>Billing Address</h2>
                <div class="Section">        
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

                <h2>Amount To Charge</h2>
                <div class="Section">        
                    <table>
                        <tr>
                            <td class="FieldHeader">Min. Billing Amount:</td>
                            <td>
                                <asp:Label ID="labelMinBillingAmount" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Current Balance:</td>
                            <td>
                                <asp:Label ID="labelBalance" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Amount To Charge:*</td>
                            <td>
                                $ <asp:textbox ID="textAmountToCharge" runat="server" CssClass="InputField Width60" MaxLength="8" />
                                <asp:RequiredFieldValidator ID="validatorAmountToCharge"
                                    runat="server" ControlToValidate="textAmountToCharge" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" 
                                    SetFocusOnError="true" /> 
                                <asp:RangeValidator ID="validatorAmountToChargeRange"
                                    runat="server" ControlToValidate="textAmountToCharge" ValidationGroup="Form" Type="Double"
                                    CssClass="ValidationMessage" ErrorMessage="* Not in range" Display="dynamic" SetFocusOnError="true">
                                </asp:RangeValidator>
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textAmountToCharge" ValidChars=".0123456789" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Additional Data:</td>
                            <td>
                                <asp:TextBox ID="textAdditionalData" runat="server" MaxLength="500" TextMode="MultiLine" Rows="5" Columns="20"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                        
                <div id="buttonHolder">
                    <asp:ImageButton ID="buttonSubmit" runat="server" ImageUrl="images/submit.gif"
                        CausesValidation="true" ValidationGroup="Form" onclick="buttonSubmit_Click" />
                    <a id="buttonCancel" class="button" href="AdvertisersToBeCharged.aspx"><span>Cancel</span></a>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>    
    
        </div>
    </div>

</asp:content>