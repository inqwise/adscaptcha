<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="ManageCampaigns.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.ManageCampaigns" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

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
                <li><a href="ManageAdvertisers.aspx" class="selected">Manage</a></li>
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

        <asp:ScriptManager ID="ScriptManager" runat="server" OnInit="scriptManagerOnInit" EnablePartialRendering="true" />
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>                
            <div>
                <table width="100%">
                    <tr>
                        <td width="80"><b>Statuses:</b></td>
                        <td colspan="2">
                            <asp:DropDownList ID="listStatus" runat="server" AutoPostBack="true"
                                CssClass="SelectField" DataValueField="Item_Id" DataTextField="Item_Desc" 
                                onselectedindexchanged="listStatus_SelectedIndexChanged" />
                        </td>
                        <td valign="top">
                            <asp:UpdateProgress ID="UpdateProgress" runat="server"> 
                                <ProgressTemplate>
                                    <span style="font-size: 10px;">Loading...&nbsp;</span><img src="images/table/spinner.gif" width="16" height="16" border="0" alt="Loading" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>                    
                        </td>
                        <td class="right">
                            <% string advertiserId = (string.IsNullOrEmpty(Request.QueryString["AdvertiserId"]) ? "" : Request.QueryString["AdvertiserId"]); %>
                            <a href="NewCampaign.aspx?AdvertiserId=<%=advertiserId%>" class="button"><span>+ Add New Campaign</span></a>
                        </td>
                    </tr>                    
                </table>
                
                <br />

                <script type="text/javascript">
                    var sortedDataField = '';
                    var sortedDescending = false;
                    function Grid_onSortChange(sender, e) {
                        sortedDataField = e.get_column().get_dataField();
                        sortedDescending = e.get_descending();
                    }
                </script>

                <ComponentArt:Grid ID="Grid" 
                    RunningMode="Client"
                    AllowPaging="true"
                    CssClass="grid"
                    DataAreaCssClass="gridData"
                    ImagesBaseUrl="images/table"
                    ShowHeader="false"
                    ShowFooter="false"
                    AllowColumnResizing="false"
                    AllowMultipleSelect="false"
                    EmptyGridText="No campaign found."
                    PageSize="5000"
                    ScrollBar="Auto"
                    ScrollTopBottomImagesEnabled="true"
                    ScrollTopBottomImageHeight="2"
                    ScrollTopBottomImageWidth="16"
                    ScrollImagesFolderUrl="images/table"
                    ScrollButtonWidth="16"
                    ScrollButtonHeight="17"
                    ScrollBarCssClass="scrollBar"
                    ScrollGripCssClass="scrollGrip"
                    ScrollBarWidth="16"
                    LoadingPanelClientTemplateId="LoadingFeedbackTemplate"
                    LoadingPanelPosition="MiddleCenter"
                    runat="server"
                    ClientIDMode="AutoID"
                    >
                    <ClientEvents>
                        <SortChange eventhandler="Grid_onSortChange" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel 
                            DataKeyField="Campaign_Id"
                            TableHeadingCssClass="gridHeader"
                            HeadingCellCssClass="headingCell"
                            HeadingRowCssClass="headingRow"
                            HeadingTextCssClass="headingCellText"
                            HeadingCellHoverCssClass="headingCellHover"
                            HeadingCellActiveCssClass="headingCellActive"                    
                            DataCellCssClass="dataCell"
                            RowCssClass="row" AlternatingRowCssClass="alternatingRow"
                            HoverRowCssClass="hoverRow"
                            SelectedRowCssClass="row"
                            ShowFooterRow="false"
                            AllowSorting="True"
                            SortAscendingImageUrl="sortAscAdv.gif"
                            SortDescendingImageUrl="sortDescAdv.gif"
                            SortImageWidth="8"
                            SortImageHeight="7">
                            <Columns>
                                <ComponentArt:GridColumn DataField="Advertiser_Id" Visible="False" />
                                <ComponentArt:GridColumn DataField="Campaign_Id" HeadingText="ID" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="IdTemplate" Width="30" />
                                <ComponentArt:GridColumn DataField="Campaign_Name" HeadingText="Name" Align="Left" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" Width="240" />
                                <ComponentArt:GridColumn DataField="Status" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="StatusTemplate" />
                                <ComponentArt:GridColumn DataField="Ads" HeadingText="Ads" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" DataCellClientTemplateId="AdsTemplate" />
                                <ComponentArt:GridColumn DataField="Served" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" />
                                <ComponentArt:GridColumn DataField="Types" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" />
                                <ComponentArt:GridColumn DataField="Fits" HeadingText="Attempted Fits" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="90" />
                                <ComponentArt:GridColumn DataField="Clicks" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="50" />
                                <ComponentArt:GridColumn DataField="Charges" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="C0" />
                                <ComponentArt:GridColumn DataField="Add_Date" HeadingText="Add Date" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FormatString="dd/MM/yyyy" />
                                <ComponentArt:GridColumn DataField="Modify_Date" HeadingText="Modify Date" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FormatString="dd/MM/yyyy" />
                                <ComponentArt:GridColumn HeadingText="Actions" Align="Left" AllowSorting="False" DataCellClientTemplateId="ActionsTemplate" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    
                    <ClientTemplates>                        
                        <ComponentArt:ClientTemplate ID="HeadingSortableCellTemplate">
                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr valign="middle">
                                        <td align="left" class="HeadingCellText ## if (DataItem.get_allowSorting()) 'Sortable' ##" style="white-space:nowrap; text-align:## DataItem.get_align() ##;">
                                            ##DataItem.get_headingText()####if (sortedDataField == DataItem.get_dataField()) '<img style="padding-left:3px;" width="' + Grid.get_levels()[0].SortImageWidth + '" height="' + Grid.get_levels()[0].SortImageHeight + '" src="' + (sortedDescending ? Grid.get_levels()[0].SortDescendingImageUrl : Grid.get_levels()[0].SortAscendingImageUrl) + '" alt="Sort" />';##
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="IdTemplate">
                            <a href="ManageAds.aspx?AdvertiserId=## DataItem.getMember('Advertiser_Id').get_value() ##&CampaignId=## DataItem.getMember('Campaign_Id').get_value() ##">## DataItem.getMember('Campaign_Id').get_value() ##</a>
                        </ComponentArt:ClientTemplate>                
                        <ComponentArt:ClientTemplate ID="AdsTemplate">
                            <a href="ManageAds.aspx?AdvertiserId=## DataItem.getMember('Advertiser_Id').get_value() ##&CampaignId=## DataItem.getMember('Campaign_Id').get_value() ##">## DataItem.getMember('Ads').get_value() ##</a>
                        </ComponentArt:ClientTemplate>                
                        <ComponentArt:ClientTemplate ID="StatusTemplate">
                            <font class="## DataItem.GetMember('Status').Value ##">
                            ## DataItem.GetMember("Status").Value ##
                            </font>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="ActionsTemplate">
                            <a href='EditCampaign.aspx?AdvertiserId=## DataItem.getMember('Advertiser_Id').get_value() ##&CampaignId=## DataItem.getMember('Campaign_Id').get_value() ##'>Edit</a> | 
                            <a href='NewAd.aspx?AdvertiserId=## DataItem.getMember('Advertiser_Id').get_value() ##&CampaignId=## DataItem.getMember('Campaign_Id').get_value() ##'>Add Ad</a>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="LoadingFeedbackTemplate">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td style="font-size:10px;">Loading...&nbsp;</td>
                                    <td><img src="images/table/spinner.gif" width="16" height="16" border="0" alt="Loading" /></td>
                                </tr>
                            </table>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
            </div>
            </ContentTemplate>        
        </asp:UpdatePanel>
    
        </div> 
    </div>

</asp:content>