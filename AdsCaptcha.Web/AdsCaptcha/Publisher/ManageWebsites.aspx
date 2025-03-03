<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Publisher/PublisherAccount.Master" AutoEventWireup="true" CodeFile="ManageWebsites.aspx.cs" Inherits="Inqwise.AdsCaptcha.Publisher.ManageWebsites" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
   <style type="text/css">
    .calendar
    {
    	background-color:#fff;
    	border:1px solid Gray;
    	}
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Manage Websites
</asp:Content>
<asp:content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
        <!--div id="breadCrambs">
            <asp:Label ID="labelNavigationPath" runat="server" />        
        </div-->


        <div id="content" >
        <div class="inner-content">
            <asp:Panel ID="PanelGrid" runat="server" Visible="true">        
                <asp:ScriptManager ID="ScriptManager" runat="server" OnInit="scriptManagerOnInit" EnablePartialRendering="true" />
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>

                          <asp:UpdateProgress ID="UpdateProgress" runat="server"> 
                                    <ProgressTemplate>
                                        <span style="font-size: 10px;">Loading...&nbsp;</span><img src="../images/table/spinner.gif" width="16" height="16" border="0" alt="Loading" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                        <div id="top-content">
        	                <div class="left">
        	                <label>Dates:</label>
            	                <asp:DropDownList ID="listFilterDate" runat="server" AutoPostBack="true" CssClass="Dates" 
                                    OnSelectedIndexChanged="listFilterDate_SelectedIndexChanged">
                                </asp:DropDownList>  
                            
                             <asp:Panel ID="panelCustomDatesFilter" runat="server" CssClass="description" style="position:absolute;z-index:100;background-color:White;border:1px solid Gray;padding:10px;">
                                <b>From</b>
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
                                <b>To</b>
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
                                CssClass="statuses" DataValueField="Item_Id" Width="120" DataTextField="Item_Desc" 
                                onselectedindexchanged="listStatus_SelectedIndexChanged" />
                            </div>
                            <div class="menu-right">
            	                <li class="active"><a href="ManageWebsites.aspx">Manage Websites</a></li>
                                <li><a href="PaymentHistory.aspx">View Payment History</a></li>
                                <li class="none"><a href="NewWebsite.aspx">New Website</a></li>
                            </div>
                            <div class="clear"></div>
                        </div>
                        
                    <!-- System messages -->
                    <div id="SystemMessagesHolder" class="systemMessages" runat="server" visible="false" style="margin-left:30px;">
                        <div class="close"><a href="javascript:HideSystemMessages();">X</a></div>
                        <asp:Label ID="labelSystemMessages" runat="server" Text=""></asp:Label>
                    </div>

                    <!--table id="SummaryTable">
                        <tr>
                            <td><asp:Label ID="labelEarningsTitle" runat="server" Text="Total Earnings:"></asp:Label></td>
                            <td><asp:Label ID="labelEarningsSum" runat="server" CssClass="Total"></asp:Label></td>
                        </tr>
                    </table>
                    <a href="PaymentHistory.aspx">View payment history</a-->
                                                                  <!-- <asp:LinkButton ID="linkFilterStatusAll" runat="server" CausesValidation="false" 
                                    OnClick="linkFilterStatusAll_Click">All Statuses</asp:LinkButton>
                                |
                                <asp:LinkButton ID="linkFilterStatusRunningPending" runat="server" CausesValidation="false"                       
                                    OnClick="linkFilterStatusRunningPending_Click">Running & Pending</asp:LinkButton>
                                |
                                <asp:LinkButton ID="linkFilterStatusPausedRejected" runat="server" CausesValidation="false" 
                                    OnClick="linkFilterStatusPausedRejected_Click">Paused & Rejected</asp:LinkButton>
                                    -->
                   

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
                        EmptyGridText="No websites found."
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
                                DataMember="Websites"
                                DataKeyField="Website_Id"
                                TableHeadingCssClass=""
                                HeadingCellCssClass="td"
                                HeadingRowCssClass="top tr header"
                                HeadingTextCssClass=""
                                HeadingCellHoverCssClass=""
                                HeadingCellActiveCssClass=""                    
                                DataCellCssClass="td"
                                RowCssClass="tr" AlternatingRowCssClass="tralt"
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
                                    <ComponentArt:GridColumn DataField="Publisher_Id" Visible="False" />
                                    <ComponentArt:GridColumn DataField="Website_Id" Visible="False" />
                                    <ComponentArt:GridColumn DataField="Url" HeadingText="Url" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="WebsiteUrlTemplate" FooterCellClientTemplateId="TotalTemplate" Width="180" />
                                    <ComponentArt:GridColumn DataField="Status" HeadingText="Status" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="WebsiteStatusTemplate" FixedWidth="true" Width="70" />
                                    <ComponentArt:GridColumn DataField="Captchas" HeadingText="Captchas" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="WebsiteCaptchasTemplate" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="70" />
                                    
                                    <ComponentArt:GridColumn DataField="Served" HeadingText="Impressions" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="80" />
                                    <ComponentArt:GridColumn DataField="PPT_Types" HeadingText="Types" Align="Center" Visible="false" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="50" />
                                    <ComponentArt:GridColumn DataField="PPF_Fits" HeadingText="Fits" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="40" />
                                    <ComponentArt:GridColumn DataField="Security_Only_Types" HeadingText="Security"  Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="90" />
                                    <ComponentArt:GridColumn DataField="Served" HeadingText="eCPM"  Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="WebsiteECPMTemplate" FixedWidth="true" Width="40" />
                                    <ComponentArt:GridColumn DataField="Earnings" HeadingText="Earnings" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CurrencyTemplate" FooterCellClientTemplateId="SummaryCurrencyTemplate" FixedWidth="true" Width="80" />
                                    <ComponentArt:GridColumn DataField="Add_Date" HeadingText="Add Date" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FormatString="dd/MM/yyyy" FixedWidth="true" Width="70" />
                                    <ComponentArt:GridColumn DataField="Modify_Date" HeadingText="Modify Date" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FormatString="dd/MM/yyyy" FixedWidth="true" Width="80" />
                                    <ComponentArt:GridColumn HeadingText="Actions" Align="Center" AllowSorting="False" DataCellClientTemplateId="ActionsTemplate" FixedWidth="true" Width="120" />
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
                            <ComponentArt:ClientTemplate ID="WebsiteUrlTemplate">
                                <a href="ManageCaptchas.aspx?WebsiteId=## DataItem.getMember('Website_Id').get_value() ##">## DataItem.getMember('Url').get_value() ##</a>
                            </ComponentArt:ClientTemplate>                
                            <ComponentArt:ClientTemplate ID="WebsiteCaptchasTemplate">
                                <a href="ManageCaptchas.aspx?WebsiteId=## DataItem.getMember('Website_Id').get_value() ##">## DataItem.getMember('Captchas').get_value() ##</a>
                            </ComponentArt:ClientTemplate>                
                            <ComponentArt:ClientTemplate ID="WebsiteStatusTemplate">
                                <font class="## DataItem.GetMember('Status').Value ##">
                                ## DataItem.GetMember("Status").Value ##
                                </font>
                            </ComponentArt:ClientTemplate>   
                            <ComponentArt:ClientTemplate ID="WebsiteECPMTemplate" >
                                ## (DataItem.getMember("Served").get_value() == 0 ? 0 : DataItem.getMember("Earnings").get_value() / DataItem.getMember("Served").get_value() * 1000).toFixed(3) ##
                            </ComponentArt:ClientTemplate>                         
                            <ComponentArt:ClientTemplate ID="ActionsTemplate">
                                <a class="action" style="color:#7c7c7c;font-weight:500;" href='NewCaptcha.aspx?WebsiteId=##DataItem.GetMember("Website_Id").Value##'>Add Captcha</a>
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

                    <br />
                    <p class="note">
                    * Reports received are not processed in real-time. Information from the last 6 hours may not be available for your review.
                    </p>

                    <asp:Label ID="table" runat="server"></asp:Label>
                    
                    </ContentTemplate>        
                </asp:UpdatePanel>
            </asp:Panel>
            <div class="description">
            <asp:Panel ID="PanelNoWebsites" runat="server" Visible="false" style="padding:50px 0 0 240px;min-height:200px;">
                You have no websites. Click <a href="NewWebsite.aspx">here</a> to add a new website.
            </asp:Panel>
            </div>
            </div>
        </div>

</asp:content>