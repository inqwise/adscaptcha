<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="ReportAdvertisers.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.ReportAdvertisers" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <script src="<%=ConfigurationSettings.AppSettings["URL"]%>js/jquery.qtip-1.0.0-rc3.min.js" type="text/javascript"></script>    
    <script src="js/report_filters.js" type="text/javascript"></script>    

    <script type="text/javascript" charset="utf-8">
        var ADSCAPTCHA_CDN = '<%=ConfigurationSettings.AppSettings["AWSBucketUrl"]%>';
        
        $(function() {
            $('#menu_3').addClass('selected');
        });

        $(document).ready(function(){
		    $('#cssdropdown li.headlink').hover(
			    function() { $('ul', this).css('display', 'block'); },
			    function() { $('ul', this).css('display', 'none'); });
	    });

        pageLoad = function() {
            $('.filterGroup').hide();

            $('.moreDetails').each(function() {
                $(this).qtip({
                    content: { text: $(this).attr('rel') },
                    position: { adjust: { screen: true } },
                    style: { width: 300, tip: true, border: { width: 2, radius: 6 }, name: 'light' }
                })
            });

            $('.preview').each(function() {
                var image = ADSCAPTCHA_CDN + $(this).attr('rel');
                var content = '<img src="' + image + '">';
                $(this).qtip({
                    content: content,
                    position: { adjust: { screen: true } },
                })
            });
        };
    </script>    
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li><a href="ReportAdvertisers.aspx" class="selected">Report</a></li>
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
                                    <asp:ListItem Text="Charges" Value="Advertiser_Charges"></asp:ListItem>
                                    <asp:ListItem Text="Add Date" Value="Add_Date"></asp:ListItem>
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
                    <span class="filterBy">Advertisers</span>
                    <table class="filterTable" cellspacing="4">
                        <tr>
                            <th>ID</th>
                            <td>
                                <asp:TextBox ID="textAdvertiserId" runat="server" CssClass="filterID"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Email</th>
                            <td>
                                <asp:TextBox ID="textAdvertiserEmail" runat="server" CssClass="filterName"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Status</th>
                            <td>
                                <asp:DropDownList ID="listAdvertiserStatus" runat="server" CssClass="filterList" />
                            </td>
                        </tr>
                    </table>
                </div>
                    
                <div class="filterGroup">
                    <span class="filterBy">Campaigns</span>
                    <table class="filterTable" cellspacing="4">
                        <tr>
                            <th>ID</th>
                            <td>
                                <asp:TextBox ID="textCampaignId" runat="server" CssClass="filterID"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Status</th>
                            <td>
                                <asp:DropDownList ID="listCampaignStatus" runat="server" CssClass="filterList" />
                            </td>
                        </tr>
                        <tr>
                            <th>Category</th>
                            <td>
                                <asp:DropDownList ID="listCampaignCategory" runat="server" CssClass="filterList" />
                            </td>
                        </tr>
                        <tr>
                            <th>Added Date</th>
                            <td>
                                <asp:DropDownList ID="listAddDate" runat="server" CssClass="filterList">
                                    <asp:ListItem Value="0" Text="-- All Dates --" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Last 7 days"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="Last 30 days"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                
                <div class="filterGroup">
                    <span class="filterBy">Ads</span>
                    <table class="filterTable" cellspacing="4">
                        <tr>
                            <th>ID:</th>
                            <td>
                                <asp:TextBox ID="textAdId" runat="server" CssClass="filterID"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Type</th>
                            <td>
                                <asp:DropDownList ID="listAdType" runat="server" CssClass="filterList" />
                            </td>
                        </tr>
                        <tr>
                            <th>Status</th>
                            <td>
                                <asp:DropDownList ID="listAdStatus" runat="server" CssClass="filterList" />
                            </td>
                        </tr>
                    </table>
                </div>
                
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
                                <ComponentArt:GridColumn DataField="Advertiser_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Advertiser_Status_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Advertiser_Email" Visible="false" />
                                <ComponentArt:GridColumn DataField="Payment_Method" Visible="false" />
                                
                                <ComponentArt:GridColumn DataField="Campaign_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Campaign_Name" HeadingText="Campaign" Align="Left" AllowSorting="True" FixedWidth="true" Width="120" />
                                <ComponentArt:GridColumn DataField="Campaign_Status_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Is_Bonus" Visible="false" />
                                <ComponentArt:GridColumn DataField="Add_Date" Visible="false" />
                                
                                <ComponentArt:GridColumn DataField="Ad_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Ad_Status_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Ad_Type" HeadingText="Ad" Align="Left" AllowSorting="True" DataCellClientTemplateId="TypeTemplate" FixedWidth="true" Width="160" />
                                <ComponentArt:GridColumn DataField="Ad_Dimensions" HeadingText="Dimensions" Align="Center" AllowSorting="True" FixedWidth="true" Width="80" />
                                <ComponentArt:GridColumn DataField="Ad_Slogan" Visible="false" />
                                <ComponentArt:GridColumn DataField="Ad_Image" Visible="false" />
                                <ComponentArt:GridColumn DataField="Ad_Click" Visible="false" />
                                <ComponentArt:GridColumn DataField="Ad_Bid" HeadingText="$BID" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="C4" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                
                                <ComponentArt:GridColumn DataField="Served" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Typed" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Fits" HeadingText="Attempted Fits" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Clickable" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Clicked" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                
                                <ComponentArt:GridColumn Visible="false" DataField="CTR" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn Visible="false" DataField="CTTR" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn Visible="false" DataField="eCTTR" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn Visible="false" DataField="eCPM" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                
                                <ComponentArt:GridColumn DataField="Advertiser_Charges" HeadingText="$Charges" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="C" FooterCellClientTemplateId="SummaryCurrencyTemplate" FixedWidth="true" />

                                <ComponentArt:GridColumn HeadingText="Details" Align="Center" DataCellClientTemplateId="MoreInfoTemplate" AllowSorting="False" FixedWidth="true" Width="50" />
                                <ComponentArt:GridColumn HeadingText="Actions" Align="Center" DataCellClientTemplateId="ActionsTemplate" AllowSorting="False" FixedWidth="true" Width="50" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="MoreInfoTemplate">
                            <span class="moreDetails" rel=
                                "
                                <b>Advertiser</b>
                                <table cellspacing='8'>
                                    <tr><th width='100'>ID</th><td>## DataItem.GetMember('Advertiser_Id').Value ##</td></tr>
                                    <tr><th>Status</td><th><img src='images/table/## DataItem.GetMember('Advertiser_Status_Id').Value ##.png' /></td></tr>
                                    <tr><th>Email</td><th>## DataItem.GetMember('Advertiser_Email').Value ##</td></tr>
                                    <tr><th>Payment Method</th><td>## DataItem.GetMember('Payment_Method').Value ##</td></tr>
                                </table>
                                <b>Campaign</b>
                                <table cellspacing='8'>
                                    <tr><th width='100'>ID</th><td>## DataItem.GetMember('Campaign_Id').Value ##</td></tr>
                                    <tr><th>Status</td><th><img src='images/table/## DataItem.GetMember('Campaign_Status_Id').Value ##.png' /></td></tr>
                                    <tr><th>Bonus?</th><td>## DataItem.GetMember('Is_Bonus').Value ##</td></tr>
                                </table>
                                <b>Ad</b>
                                <table cellspacing='8'>
                                    <tr><th width='100'>ID</th><td>## DataItem.GetMember('Ad_Id').Value ##</td></tr>
                                    <tr><th>Status</td><th><img src='images/table/## DataItem.GetMember('Ad_Status_Id').Value ##.png' /></td></tr>
                                    <tr><th>Type</th><td>## DataItem.GetMember('Ad_Type').Value ##</td></tr>
                                    <tr><th>Dimensions</th><td>## DataItem.GetMember('Ad_Dimensions').Value ##</td></tr>
                                    <tr><th>Slogan</th><td>## DataItem.GetMember('Ad_Slogan').Value ##</td></tr>
                                    <tr><th>Click?</th><td>## DataItem.GetMember('Ad_Click').Value ##</td></tr>
                                    <tr><th>BID</th><td>$## DataItem.GetMember('Ad_Bid').Value ##</td></tr>
                                </table>
                                ">Details</span>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="ActionsTemplate">
                            <ul id="cssdropdown">  
		                        <li class="headlink"> 
			                        <a href="#">Actions</a> 
 			                        <ul> 
                                        <li><a href='mailto:## DataItem.getMember('Advertiser_Email').get_value() ##'>Send Email</a></li>
                                        <li><a href='EditAdvertiser.aspx?AdvertiserId=## DataItem.getMember('Advertiser_Id').get_value() ##'>Edit Advertiser</a></li>
                                        <li><a href='EditCampaign.aspx?AdvertiserId=## DataItem.getMember('Advertiser_Id').get_value() ##&CampaignId=## DataItem.getMember('Campaign_Id').get_value() ##'>Edit Campaign</a></li>
                                        <li><a href='EditAd.aspx?AdvertiserId=## DataItem.getMember('Advertiser_Id').get_value() ##&CampaignId=## DataItem.getMember('Campaign_Id').get_value() ##&AdId=## DataItem.getMember('Ad_Id').get_value() ##'>Edit Ad</a></li>
			                        </ul> 
		                        </li>
	                        </ul>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="TypeTemplate">
                            <span class="preview" rel="## DataItem.GetMember('Ad_Image').Value ##">## DataItem.GetMember('Ad_Type').Value ##</span>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="StatusTemplate">
                            <img src="images/table/## DataItem.getCurrentMember().get_value() ##.png" />
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