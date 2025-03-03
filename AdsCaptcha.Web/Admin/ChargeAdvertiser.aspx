<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="ChargeAdvertiser.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.ChargeAdvertiser" %>
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
                    <h2>Charge Advertiser</h2>
                    <div class="Section">
                        <table>
                            <tr>
                                <td class="FieldHeader">Current:</td>
                                <td>
                                    <asp:Label ID="labelChargeMethod" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldHeader">Charge Method:</td>
                                <td>
                                    <asp:RadioButtonList ID="radioChargeMethod" runat="server" />
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
            </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>    
    
        </div>
    </div>

</asp:content>