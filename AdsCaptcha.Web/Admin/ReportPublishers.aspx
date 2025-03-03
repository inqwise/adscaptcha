<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="ReportPublishers.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.ReportPublishers" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <script src="<%=ConfigurationSettings.AppSettings["URL"]%>js/jquery.qtip-1.0.0-rc3.min.js" type="text/javascript"></script>    
    <script src="js/report_filters.js" type="text/javascript"></script>    

    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_2').addClass('selected');
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
        };
    </script>    
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <div id="subNavigation">
        <div class="subNavigation">
            <ul>                
                <li><a href="ReportPublishers.aspx" class="selected">Report</a></li>
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
                                    <asp:ListItem Text="$Dev" Value="Developer_Earnings"></asp:ListItem>
                                    <asp:ListItem Text="$Site" Value="Publisher_Earnings"></asp:ListItem>
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
                    <span class="filterBy">Site Owners</span>
                    <table class="filterTable" cellspacing="4">
                        <tr>
                            <th>ID</th>
                            <td>
                                <asp:TextBox ID="textPublisherId" runat="server" CssClass="filterID"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Email</th>
                            <td>
                                <asp:TextBox ID="textPublisherEmail" runat="server" CssClass="filterName"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Status</th>
                            <td>
                                <asp:DropDownList ID="listPublisherStatus" runat="server" CssClass="filterList" />
                            </td>
                        </tr>
                    </table>
                </div>
                    
                <div class="filterGroup">
                    <span class="filterBy">Websites</span>
                    <table class="filterTable" cellspacing="4">
                        <tr>
                            <th>ID</th>
                            <td>
                                <asp:TextBox ID="textWebsiteId" runat="server" CssClass="filterID"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>URL</th>
                            <td>
                                <asp:TextBox ID="textWebsiteUrl" runat="server" CssClass="filterName"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Status</th>
                            <td>
                                <asp:DropDownList ID="listWebsiteStatus" runat="server" CssClass="filterList" />
                            </td>
                        </tr>
                        <tr>
                            <th>Category</th>
                            <td>
                                <asp:DropDownList ID="listWebsiteCategory" runat="server" CssClass="filterList" />
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
                    <span class="filterBy">Captchas</span>
                    <table class="filterTable" cellspacing="4">
                        <tr>
                            <th>ID:</th>
                            <td>
                                <asp:TextBox ID="textCaptchaId" runat="server" CssClass="filterID"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Type</th>
                            <td>
                                <asp:DropDownList ID="listCaptchaType" runat="server" CssClass="filterList" />
                            </td>
                        </tr>
                        <tr>
                            <th>Status</th>
                            <td>
                                <asp:DropDownList ID="listCaptchaStatus" runat="server" CssClass="filterList" />
                            </td>
                        </tr>
                    </table>
                </div>
                
                <div class="filterGroup">
                    <span class="filterBy">Developers</span>
                    <table class="filterTable" cellspacing="4">
                        <tr>
                            <th>ID</th>
                            <td>
                                <asp:TextBox ID="textDeveloperId" runat="server" CssClass="filterID"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Email</th>
                            <td>
                                <asp:TextBox ID="textDeveloperEmail" runat="server" CssClass="filterName"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Status</th>
                            <td>
                                <asp:DropDownList ID="listDeveloperStatus" runat="server" CssClass="filterList" />
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
                                <ComponentArt:GridColumn DataField="Publisher_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Publisher_Status_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Publisher_Email" Visible="false" />
                                <ComponentArt:GridColumn DataField="Publisher_Name" Visible="false" />
                                <ComponentArt:GridColumn DataField="Publisher_Rev_Share" Visible="False" />
                                <ComponentArt:GridColumn DataField="Payment_Method" Visible="False" />
                                
                                <ComponentArt:GridColumn DataField="Website_Id" HeadingText="ID" Align="Center" AllowSorting="True" FixedWidth="true" Width="30" />
                                <ComponentArt:GridColumn DataField="Website_Status_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Url" Align="Left" AllowSorting="True" DataCellClientTemplateId="UrlTemplate" FixedWidth="true" Width="180" />
                                <ComponentArt:GridColumn DataField="Is_Bonus" Visible="false" />
                                <ComponentArt:GridColumn DataField="Add_Date" Visible="false" FormatString="dd/MM/yyyy" />
                                
                                <ComponentArt:GridColumn DataField="Captcha_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Captcha_Status_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Captcha_Type" HeadingText="Captcha Type" Align="Left" AllowSorting="True" DataCellClientTemplateId="CaptchaPopupTemplate" FixedWidth="true" Width="120" />
                                <ComponentArt:GridColumn DataField="Dimensions" Visible="false" />
                                <ComponentArt:GridColumn DataField="Pop_Under" Visible="false" />
                                <ComponentArt:GridColumn DataField="Security_Level" Visible="false" />
                                
                                <ComponentArt:GridColumn DataField="Developer_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Developer_Email" Visible="false" />
                                <ComponentArt:GridColumn DataField="Developer_Name" Visible="false" />
                                <ComponentArt:GridColumn DataField="Developer_Rev_Share" Visible="False" />
                                
                                <ComponentArt:GridColumn DataField="Served" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Security" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Typed" HeadingText="Attempted Fits/Typed" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Fits" HeadingText="Fits" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Clickable" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Clicked" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                
                                <ComponentArt:GridColumn Visible="false" DataField="CTR" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn Visible="false" DataField="CTTR" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn Visible="false" DataField="eCTTR" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn Visible="true" DataField="eCPM" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" />
                                
                                <ComponentArt:GridColumn DataField="Developer_Earnings" HeadingText="$Dev" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="C" FooterCellClientTemplateId="SummaryCurrencyTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Publisher_Earnings" HeadingText="$Site" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="C" FooterCellClientTemplateId="SummaryCurrencyTemplate" FixedWidth="true" />
                                <ComponentArt:GridColumn DataField="Profits" HeadingText="$Net" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" FormatString="C" FooterCellClientTemplateId="SummaryCurrencyTemplate" FixedWidth="true" />

                                <ComponentArt:GridColumn HeadingText="Details" Align="Center" DataCellClientTemplateId="MoreInfoTemplate" AllowSorting="False" FixedWidth="true" Width="50" />
                                <ComponentArt:GridColumn HeadingText="Actions" Align="Center" DataCellClientTemplateId="ActionsTemplate" AllowSorting="False" FixedWidth="true" Width="50" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="MoreInfoTemplate">
                            <span class="moreDetails" rel=
                                "
                                <b>Site Owner</b>
                                <table cellspacing='8'>
                                    <tr><th width='100'>ID</th><td>## DataItem.GetMember('Publisher_Id').Value ##</td></tr>
                                    <tr><th>Status</td><th><img src='images/table/## DataItem.GetMember('Publisher_Status_Id').Value ##.png' /></td></tr>
                                    <tr><th>Email</td><th>## DataItem.GetMember('Publisher_Email').Value ##</td></tr>
                                    <tr><th>Name</td><th>## DataItem.GetMember('Publisher_Name').Value ##</td></tr>
                                    <tr><th>Rev. Share</th><td>## DataItem.GetMember('Publisher_Rev_Share').Value ##%</td></tr>
                                    <tr><th>Payment Method</th><td>## DataItem.GetMember('Payment_Method').Value ##</td></tr>
                                </table>
                                <b>Website</b>
                                <table cellspacing='8'>
                                    <tr><th width='100'>ID</th><td>## DataItem.GetMember('Website_Id').Value ##</td></tr>
                                    <tr><th>Status</td><th><img src='images/table/## DataItem.GetMember('Website_Status_Id').Value ##.png' /></td></tr>
                                </table>
                                <b>Captcha</b>
                                <table cellspacing='8'>
                                    <tr><th width='100'>ID</th><td>## DataItem.GetMember('Captcha_Id').Value ##</td></tr>
                                    <tr><th>Status</td><th><img src='images/table/## DataItem.GetMember('Captcha_Status_Id').Value ##.png' /></td></tr>
                                    <tr><th>Type</th><td>## DataItem.GetMember('Captcha_Type').Value ##</td></tr>
                                    <tr><th>Dimensions</th><td>## DataItem.GetMember('Dimensions').Value ##</td></tr>
                                    <tr><th>Security Level</th><td>## DataItem.GetMember('Security_Level').Value ##</td></tr>
                                    <tr><th>Pop Under</th><td>## DataItem.GetMember('Pop_Under').Value ##</td></tr>
                                </table>
                                <b>Developer</b>
                                <table cellspacing='8'>
                                    <tr><th width='100'>ID</th><td>## DataItem.GetMember('Developer_Id').Value ##</td></tr>
                                    <tr><th>Name</th><td>## DataItem.GetMember('Developer_Name').Value ##</td></tr>
                                    <tr><th>Email</th><td>## DataItem.GetMember('Developer_Email').Value ##</td></tr>
                                    <tr><th>Rev. Share</th><td>## DataItem.GetMember('Developer_Rev_Share').Value ##%</td></tr>
                                </table>
                                ">Details</span>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="ActionsTemplate">
                            <ul id="cssdropdown">  
		                        <li class="headlink"> 
			                        <a href="#">Actions</a> 
 			                        <ul> 
                                        <li><a href='mailto:## DataItem.getMember('Publisher_Email').get_value() ##'>Send Email</a></li>
                                        <li><a href='EditPublisher.aspx?PublisherId=## DataItem.getMember('Publisher_Id').get_value() ##'>Edit Publisher</a></li>
                                        <li><a href='EditWebsite.aspx?PublisherId=## DataItem.getMember('Publisher_Id').get_value() ##&WebsiteId=## DataItem.getMember('Website_Id').get_value() ##'>Edit Website</a></li>
                                        <li><a href='EditCaptcha.aspx?PublisherId=## DataItem.getMember('Publisher_Id').get_value() ##&WebsiteId=## DataItem.getMember('Website_Id').get_value() ##&CaptchaId=## DataItem.getMember('Captcha_Id').get_value() ##'>Edit Captcha</a></li>
			                        </ul> 
		                        </li>
	                        </ul>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="UrlTemplate">
                            <a href="http://## DataItem.getMember('Url').get_value() ##" target="_blank">## DataItem.getMember('Url').get_value() ##</a>
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