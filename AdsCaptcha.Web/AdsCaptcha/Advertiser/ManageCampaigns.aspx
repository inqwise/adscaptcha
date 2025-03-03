<%@ Page Inherits="Inqwise.AdsCaptcha.Advertiser.ManageCampaigns" EnableViewStateMac="false" Language="C#" MasterPageFile="~/Advertiser/AdvertiserAccount.Master" AutoEventWireup="true" CodeFile="ManageCampaigns.aspx.cs" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
   <style type="text/css">
    .calendar
    {
    	background-color:#fff;
    	border:1px solid Gray;
    	}
    	#content .inner-content, #content-form {
       padding: 0px 0 40px 0;
    margin: 0 auto;
    width: 950px;
}
.menu-right .active a {
    color: #19C12D !important;
}
.menu-right li a {
    color: #7C7C7C !important;
    font-weight: 700;
}
.menu-right li a:hover {
    color: #19C12D !important;
}
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Manage Campaigns
</asp:Content>
<asp:content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


        <div id="content" >
        <div class="inner-content">
            <asp:Panel ID="PanelGrid" runat="server" Visible="true">        
                <asp:ScriptManager ID="ScriptManager" runat="server" OnInit="scriptManagerOnInit" EnablePartialRendering="true" />
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>

                        
                    <!-- System messages -->
                    <div id="SystemMessagesHolder" class="systemMessages" runat="server" visible="false">
                        <div class="close"><a href="javascript:HideSystemMessages();">X</a></div>
                        <asp:Label ID="labelSystemMessages" runat="server" Text=""></asp:Label>
                    </div>

                    <!--table id="SummaryTable">
                        <tr>
                            <td><asp:Label ID="labelChargesTitle" runat="server" Text="Total Charges:"></asp:Label></td>
                            <td><asp:Label ID="labelChargesSum" runat="server" CssClass="Total"></asp:Label></td>
                        </tr>
                    </table-->
                     <div class="description">
                            
                            <div id="top-content">
        	                <div class="left">
        	                <label>Dates:</label> 
        	                <asp:DropDownList ID="listFilterDate" runat="server" AutoPostBack="true" Width="100px" 
                                    OnSelectedIndexChanged="listFilterDate_SelectedIndexChanged">
                             </asp:DropDownList> 
                              <asp:Panel ID="panelCustomDatesFilter" runat="server"  CssClass="description" style="position:absolute;z-index:100;background-color:White;border:1px solid Gray;padding:10px;">
                                <b><font color="gray">From</font></b>
                                <ComponentArt:Calendar 
                                    ID="PickerFrom"
                                    runat="server"
                                    PickerFormat="Custom"
                                    PickerCustomFormat="MMMM d yyyy"
                                    ControlType="Picker"
                                    PickerCssClass="picker">
                                    <ClientEvents>
                                        <SelectionChanged EventHandler="PickerFrom_OnDateChange" />
                                    </ClientEvents>
                                </ComponentArt:Calendar>
                                <img id="calendar_from_button" alt="From Date" class="calendar_button" src="../images/calendar.jpg" 
                                    onclick="ButtonFrom_OnClick(event)" onmouseup="ButtonFrom_OnMouseUp(event)" />
                                <b><font color="gray">To</font></b>
                                <ComponentArt:Calendar 
                                    ID="PickerTo"
                                    runat="server"
                                    PickerFormat="Custom"
                                    PickerCustomFormat="MMMM d yyyy"
                                    ControlType="Picker"
                                    PickerCssClass="picker">
                                    <ClientEvents>
                                        <SelectionChanged EventHandler="PickerTo_OnDateChange" />
                                    </ClientEvents>
                                </ComponentArt:Calendar>                                  
                                <img id="calendar_to_button" alt="To Date" class="calendar_button" src="../images/calendar.jpg"
                                    onclick="ButtonTo_OnClick(event)" onmouseup="ButtonTo_OnMouseUp(event)" />
                        
                                <ComponentArt:Calendar runat="server"
                                    ID="CalendarFrom"
                                    AllowMultipleSelection="false"
                                    AllowWeekSelection="false"
                                    AllowMonthSelection="false"
                                    ControlType="Calendar"
                                    PopUp="Custom"
                                    PopUpExpandControlId="calendar_from_button"
                                    CalendarTitleCssClass="title"
                                    SelectedDate="2005-12-13"
                                    VisibleDate="2005-12-13"
                                    DayHeaderCssClass="dayheader"
                                    DayCssClass="day"
                                    DayHoverCssClass="dayhover"
                                    OtherMonthDayCssClass="othermonthday"
                                    SelectedDayCssClass="selectedday"
                                    CalendarCssClass="calendar"
                                    NextPrevCssClass="nextprev"
                                    MonthCssClass="month"
                                    SwapSlide="Linear"
                                    SwapDuration="300"
                                    DayNameFormat="FirstTwoLetters"
                                    ImagesBaseUrl="../images"
                                    PrevImageUrl="calendar_prev_month.gif"
                                    NextImageUrl="calendar_next_month.gif">
                                    <ClientEvents>
                                        <SelectionChanged EventHandler="CalendarFrom_OnChange" />
                                    </ClientEvents>
                                </ComponentArt:Calendar>

                                <ComponentArt:Calendar runat="server"
                                    ID="CalendarTo"
                                    AllowMultipleSelection="false"
                                    AllowWeekSelection="false"
                                    AllowMonthSelection="false"
                                    ControlType="Calendar"
                                    PopUp="Custom"
                                    PopUpExpandControlId="calendar_to_button"
                                    CalendarTitleCssClass="title"
                                    SelectedDate="2005-12-17"
                                    VisibleDate="2005-12-17"
                                    DayHeaderCssClass="dayheader"
                                    DayCssClass="day"
                                    DayHoverCssClass="dayhover"
                                    OtherMonthDayCssClass="othermonthday"
                                    SelectedDayCssClass="selectedday"
                                    CalendarCssClass="calendar"
                                    NextPrevCssClass="nextprev"
                                    MonthCssClass="month"
                                    SwapSlide="Linear"
                                    SwapDuration="300"
                                    DayNameFormat="FirstTwoLetters"
                                    ImagesBaseUrl="../images"
                                    PrevImageUrl="calendar_prev_month.gif"
                                    NextImageUrl="calendar_next_month.gif">
                                    <ClientEvents>
                                        <SelectionChanged EventHandler="CalendarTo_OnChange" />
                                    </ClientEvents>
                                </ComponentArt:Calendar>                            
                                <asp:LinkButton ID="linkUpdateCustomDates" runat="server" 
                                        onclick="linkUpdateCustomDates_Click">Update</asp:LinkButton>                       
                                </asp:Panel>
                               
                               <label>Satuses:</label>
                                 <asp:DropDownList ID="listStatus" runat="server" AutoPostBack="true"
                                CssClass="SelectField" DataValueField="Item_Id" DataTextField="Item_Desc" 
                                onselectedindexchanged="listStatus_SelectedIndexChanged" Width="150px" />
                                </div>
                            <div class="menu-right">
                                <ul style="padding:0;">
            	                <li class="active"><a href="ManageCampaigns.aspx">Manage Campaigns</a></li>
                                <li><a href="BillingSummary.aspx">Billing Summary</a></li>
                                <li><a href="NewCampaign.aspx">New Campaign</a></li>
                                </ul>
                            </div>
                            <div class="clear"></div>
                        </div>
                         <asp:UpdateProgress ID="UpdateProgress" runat="server"> 
                                    <ProgressTemplate>
                                        <span style="font-size: 10px;">Loading...&nbsp;</span><img src="../images/table/spinner.gif" width="16" height="16" border="0" alt="Loading" />
                                    </ProgressTemplate>
                         </asp:UpdateProgress>  
                    <!--table width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="80"><b>Dates:</b></td>
                            <td>
                                                       
                            </td>
                            <td>                      
                               
                            </td>
                            <td rowspan="2" valign="top">
                  
                            </td>
                            <td rowspan="2" style="float:right">
                                <a href="NewCampaign.aspx" class="button"><span>+ Add New Campaign</span></a>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Statuses:</b></td>
                            <td colspan="2">
                               < <asp:LinkButton ID="linkFilterStatusAll" runat="server" CausesValidation="false"
                                    OnClick="linkFilterStatusAll_Click">All Statuses</asp:LinkButton>
                                |
                                <asp:LinkButton ID="linkFilterStatusRunningPending" runat="server" CausesValidation="false"
                                    OnClick="linkFilterStatusRunningPending_Click">Running & Pending</asp:LinkButton>
                                |
                                <asp:LinkButton ID="linkFilterStatusPausedRejected" runat="server" CausesValidation="false" 
                                    OnClick="linkFilterStatusPausedRejected_Click">Paused & Rejected</asp:LinkButton>
                                    >
                                  
                            </td>
                        </tr>                    
                    </table-->
                    
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
                        CssClass="table"
                        DataAreaCssClass="gridData"
                        FooterCssClass="gridFooter"
                        ImagesBaseUrl="../images/table"
                        ShowHeader="false"
                        ShowFooter="false"
                        AllowColumnResizing="false"
                        AllowMultipleSelect="false"
                        EmptyGridText="No campaigns found."                    
                        PageSize="20"
                        ScrollBar="Auto"
                        ScrollTopBottomImagesEnabled="true"
                        ScrollTopBottomImageHeight="2"
                        ScrollTopBottomImageWidth="16"
                        ScrollImagesFolderUrl="../images/table"
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
                                DataMember="Campaigns"
                                DataKeyField="Campaign_Id"
                                TableHeadingCssClass=""
                                HeadingCellCssClass="td"
                                HeadingRowCssClass="top tr header"
                                HeadingTextCssClass=""
                                HeadingCellHoverCssClass=""
                                HeadingCellActiveCssClass=""                    
                                DataCellCssClass="td"
                                RowCssClass="tr" AlternatingRowCssClass="tr"
                                HoverRowCssClass=""
                                SelectedRowCssClass=""
                                ShowFooterRow="true"
                                FooterRowCssClass="tr footer"
                                SortAscendingImageUrl="sortAscPub.gif"
                                SortDescendingImageUrl="sortDescPub.gif"
                                SortImageWidth="8"
                                SortImageHeight="7"
                                >
                                <Columns>
                                    <ComponentArt:GridColumn DataField="Advertiser_Id" Visible="False" />
                                    <ComponentArt:GridColumn DataField="Campaign_Id" Visible="False" />
                                    <ComponentArt:GridColumn DataField="Campaign_Name" HeadingText="Campaign Name" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="CampaignNameTemplate" FooterCellClientTemplateId="TotalTemplate" Width="180" />
                                    <ComponentArt:GridColumn DataField="Status" HeadingText="Status" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="CampaignStatusTemplate" FixedWidth="true" Width="70" />
                                    <ComponentArt:GridColumn DataField="Ads" HeadingText="Ads" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" DataCellClientTemplateId="CampaignAdsTemplate" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="40" />
                                    <ComponentArt:GridColumn DataField="Daily_Budget" HeadingText="Daily Budget" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CurrencyTemplate" FixedWidth="true" Width="100" />
                                    <ComponentArt:GridColumn DataField="Types" HeadingText="Types" Visible="false" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="50" />
                                    <ComponentArt:GridColumn DataField="Fits" HeadingText="Fits" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="50" />
                                    <ComponentArt:GridColumn DataField="Clicked" HeadingText="Clicks" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="50" />
                                    <ComponentArt:GridColumn DataField="Charges" HeadingText="Charges" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CurrencyTemplate" FooterCellClientTemplateId="SummaryCurrencyTemplate" FixedWidth="true" Width="70" />
                                    <ComponentArt:GridColumn DataField="Add_Date" HeadingText="Add Date" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FormatString="dd/MM/yyyy" FixedWidth="true" Width="70" />
                                    <ComponentArt:GridColumn DataField="Modify_Date" HeadingText="Modify Date" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FormatString="dd/MM/yyyy" FixedWidth="true" Width="70" />
                                    <ComponentArt:GridColumn HeadingText="Actions" Align="Center" AllowSorting="False" DataCellClientTemplateId="ActionsTemplate" FixedWidth="true" Width="90" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        
                        <ClientTemplates>                        
                            <ComponentArt:ClientTemplate ID="HeadingSortableCellTemplate">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr valign="middle">
                                            <td align="left" class="headingCellText ## if (DataItem.get_allowSorting()) 'sortable' ##" style="white-space:nowrap; text-align:## DataItem.get_align() ##;">
                                                ##DataItem.get_headingText()####if (sortedDataField == DataItem.get_dataField()) '<img style="padding-left:3px;" width="' + Grid.get_levels()[0].SortImageWidth + '" height="' + Grid.get_levels()[0].SortImageHeight + '" src="' + (sortedDescending ? Grid.get_levels()[0].SortDescendingImageUrl : Grid.get_levels()[0].SortAscendingImageUrl) + '" alt="Sort" />';##
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="TotalTemplate">
                                Total
                            </ComponentArt:ClientTemplate>                
                            <ComponentArt:ClientTemplate ID="CampaignNameTemplate">
                                <a href="ManageAds.aspx?CampaignId=## DataItem.getMember('Campaign_Id').get_value() ##">## DataItem.getMember('Campaign_Name').get_value() ##</a>
                            </ComponentArt:ClientTemplate>                
                            <ComponentArt:ClientTemplate ID="CampaignAdsTemplate">
                                <a href="ManageAds.aspx?CampaignId=## DataItem.getMember('Campaign_Id').get_value() ##">## DataItem.getMember('Ads').get_value() ##</a>
                            </ComponentArt:ClientTemplate>                
                            <ComponentArt:ClientTemplate ID="CampaignStatusTemplate">
                                <font class="## DataItem.GetMember('Status').Value ##">
                                ## DataItem.GetMember("Status").Value ##
                                </font>
                            </ComponentArt:ClientTemplate>                          
                            <ComponentArt:ClientTemplate ID="ActionsTemplate">
                                <a class="action" href='EditCampaign.aspx?CampaignId=##DataItem.GetMember("Campaign_Id").Value##'>Edit</a>
                                |
                                <a class="action" href='NewAd.aspx?CampaignId=##DataItem.GetMember("Campaign_Id").Value##'>Add Ad</a>
                            </ComponentArt:ClientTemplate>                
                            <ComponentArt:ClientTemplate ID="CurrencyTemplate">
                                $## DataItem.getCurrentMember().get_value() ## 
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="SummaryCountTemplate">
                                ## GetSubTotal(DataItem, 0) ##
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="SummaryCurrencyTemplate">
                                $## GetSubTotal(DataItem, 2) ##
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="LoadingFeedbackTemplate">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td style="font-size:10px;">Loading...&nbsp;</td>
                                        <td><img src="../images/table/spinner.gif" width="16" height="16" border="0" alt="Loading" /></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>

                    <p class="note">
                    * Reports received are not processed in real-time. Information from the last 6 hours may not be available for your review.
                    </p>
                    </div>
                    </ContentTemplate>        
                </asp:UpdatePanel>
            </asp:Panel>
            
            <asp:Panel ID="PanelNoCampaigns" runat="server" Visible="false" CssClass="note">
                You have no campaigns. Click <a href="NewCampaign.aspx">here</a> to add a new campaign.
            </asp:Panel>
        </div>

    </div>
</asp:content>