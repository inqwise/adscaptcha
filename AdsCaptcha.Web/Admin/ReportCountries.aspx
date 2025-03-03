<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="ReportCountries.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.ReportCountry" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <script src="<%=ConfigurationSettings.AppSettings["URL"]%>js/jquery.qtip-1.0.0-rc3.min.js" type="text/javascript"></script>    
    <script src="js/report_filters.js" type="text/javascript"></script>    

    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_2').addClass('selected');
        });

        pageLoad = function() {
            $('.filterGroup').hide();

            $('.moreDetails').each(function() {
                $(this).qtip({
                    content: { text: $(this).attr('rel') },
                    style: { tip: true, border: { width: 0, radius: 4 }, name: 'blue' }
                })
            });
        };
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
                <li><a href="ReportCountries.aspx" class="selected">Countries Report</a></li>
                <li><a href="RandomImage.aspx">Random Images</a></li>
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
                <div class="customFilter">
                    <table class="filterTable" cellspacing="4">
                        <tr>
                            <th>Dates</th>
                            <td>
                                <asp:DropDownList ID="listDatesFilter" runat="server" CssClass="filterCustomList">
                                    <asp:ListItem Value="0" Text="-- All Time --" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yesterday"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Last week"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="Last month"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>Top</th>
                            <td>
                                <asp:DropDownList ID="listNumOfRows" runat="server" CssClass="filterCustomList">
                                    <asp:ListItem Text="-- All --" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="10"></asp:ListItem>
                                    <asp:ListItem Text="20" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="50"></asp:ListItem>
                                    <asp:ListItem Text="100"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <th>By</th>
                            <td>
                                <asp:DropDownList ID="listOrderBy" runat="server" CssClass="filterCustomList">
                                    <asp:ListItem Text="Served" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Typed"></asp:ListItem>
                                    <asp:ListItem Text="Attempted Fits" Value="Fits"></asp:ListItem>
                                    <asp:ListItem Text="Clicked"></asp:ListItem>
                                    <asp:ListItem Text="Security"></asp:ListItem> 
                                </asp:DropDownList>
                                <asp:DropDownList ID="listOrderByDirection" runat="server" CssClass="filterCustomListSmall">
                                    <asp:ListItem Text="A to Z" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Z to A" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>Country</th>
                            <td>
                                <asp:DropDownList ID="listCountry" runat="server" CssClass="filterCustomList" />
                            </td>
                        </tr>
                    </table>
                    <a id="toggleFilters" href="#" onclick="return false;">Advanced/Basic</a> | <a id="resetFilters" href="#" onclick="return false;">Reset</a>
                </div>

                <div class="clearfix"></div>
                
                <div class="filterGroup">
                    <span class="filterBy">Dates</span>
                    <table class="filterTable" cellspacing="4">
                        <tr>
                            <th>From</th>
                            <td>
                                <asp:TextBox ID="textFromDate" runat="server" CssClass="filterDatePicker"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>To</th>
                            <td>
                                <asp:TextBox ID="textToDate" runat="server" CssClass="filterDatePicker"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                
                <div class="clearfix"></div>

                <div class="filterButton">
                    <asp:LinkButton ID="filterButton" runat="server" CssClass="button" CausesValidation="true"><span>Filter</span></asp:LinkButton>
                </div>

                <div class="clearfix"></div>

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
                    CssClass="grid"
                    DataAreaCssClass="gridData"
                    ImagesBaseUrl="images/table"
                    ShowHeader="false"
                    ShowFooter="false"
                    AllowColumnResizing="false"
                    AllowMultipleSelect="false"
                    EmptyGridText="No data found."
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
                    runat="server"
                    ClientIDMode="AutoID"
                    >
                    <ClientEvents>
                        <SortChange eventhandler="Grid_onSortChange" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel 
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
                            ShowFooterRow="true"
                            FooterRowCssClass="summaryRow"
                            AllowSorting="True"
                            SortAscendingImageUrl="sortAscAdv.gif"
                            SortDescendingImageUrl="sortDescAdv.gif"
                            SortImageWidth="8"
                            SortImageHeight="7"
                            >
                            <Columns>
                                <ComponentArt:GridColumn DataField="Country_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Country_Prefix" HeadingText=" " Align="Center" AllowSorting="False" DataCellClientTemplateId="FlagTemplate" FixedWidth="true" Width="40" />
                                <ComponentArt:GridColumn DataField="Country_Name" HeadingText="Country" Align="Left" AllowSorting="True" FixedWidth="true" />
                                
                                <ComponentArt:GridColumn DataField="Served" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Security" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Typed" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Fits" HeadingText="Attempted Fits" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Clickable" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Clicked" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                
                                <ComponentArt:GridColumn Visible="false" DataField="CTR" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn Visible="false" DataField="CTTR" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn Visible="false" DataField="eCTTR" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn Visible="false" DataField="eCPM" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="FlagTemplate">                            
                            <img src="images/flags/## DataItem.getCurrentMember().get_value() ##.png" width="32" height="32" />
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="SummaryCountTemplate">
                            ## GetSubTotal(DataItem, 0) ##
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="SummaryCurrencyTemplate">
                            $## GetSubTotal(DataItem, 2) ##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
        </div> 
    </div>

</asp:content>