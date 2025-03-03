<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Publisher/PublisherAccount.Master" AutoEventWireup="true" CodeFile="ManageCaptchas.aspx.cs" Inherits="Inqwise.AdsCaptcha.Publisher.ManageCaptchas" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
   <style type="text/css">
    .calendar
    {
    	background-color:#fff;
    	border:1px solid Gray;
    	}
    	
.systemMessages
{
	margin: 10px 0 10px 22px;
}
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Manage Captchas
</asp:Content>
<asp:content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 
 
    

        <!--div id="breadCrambs">
            <asp:Label ID="labelNavigationPath" runat="server" />        
        </div-->

        <div id="content">
        <div class="inner-content">
        <br />
            <asp:Panel ID="PanelGrid" runat="server" Visible="true">        
                <asp:ScriptManager ID="ScriptManager" runat="server" OnInit="scriptManagerOnInit" EnablePartialRendering="true" />
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>
                    <% string websiteId = Request.QueryString["WebsiteId"].ToString(); %>
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
                                    PickerCssClass="picker" BorderColor="Gray" BackColor="White">
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
                                    PickerCssClass="picker" BorderColor="#999999" BackColor="White">
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
                                    NextImageUrl="calendar_next_month.gif"
                                     BorderColor="Gray" BackColor="White" BorderStyle="Solid" BorderWidth="1px">
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
                                    NextImageUrl="calendar_next_month.gif"
                                     BorderColor="Gray" BackColor="White" BorderStyle="Solid" BorderWidth="1px">
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
            	                <li><a href="ManageWebsites.aspx">Manage Websites</a></li>
                                <li><a href="PaymentHistory.aspx">View Payment History</a></li>
                                <li class="active"><a href="NewCaptcha.aspx?WebsiteId=<%=websiteId%>">New Captcha</a></li>
                            </div>
                            <div class="clear"></div>
                        </div>
                        
                    <!-- System messages -->
                    <div id="SystemMessagesHolder" class="systemMessages" runat="server" visible="false">
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
                        CssClass="table"
                        DataAreaCssClass="table"
                        FooterCssClass="gridFooter"
                        ImagesBaseUrl="../images/table"
                        ShowHeader="false"
                        ShowFooter="false"
                        AllowColumnResizing="false"
                        AllowMultipleSelect="false"
                        EmptyGridText="No captchas found."
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
                        OnNeedRebind="OnNeedRebind"
                        OnNeedDataSource="OnNeedDataSource"
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
                                    <ComponentArt:GridColumn DataField="Publisher_Id" Visible="False" />
                                    <ComponentArt:GridColumn DataField="Website_Id" Visible="False" />
                                    <ComponentArt:GridColumn DataField="Captcha_Id" Visible="False" />
                                    <ComponentArt:GridColumn DataField="Public_Key" Visible="False" />
                                    <ComponentArt:GridColumn DataField="Captcha_Name" HeadingText="Name" Align="Left" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FooterCellClientTemplateId="TotalTemplate" Width="120" />
                                    <ComponentArt:GridColumn DataField="Status" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="CaptchaStatus" FixedWidth="true" Width="60" />
                                    <ComponentArt:GridColumn DataField="Type" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FixedWidth="true" Width="120" />
                                    <ComponentArt:GridColumn DataField="Security_Level_Id" Visible="false" HeadingText="Security Level" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="SecurityLevelTemplate" Width="120" />
                                    <ComponentArt:GridColumn DataField="Security_Level" Visible="false" />
                                    <ComponentArt:GridColumn DataField="Max_Size" HeadingText="Dimensions" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FixedWidth="true" Width="80" />
                                    
                                    <ComponentArt:GridColumn DataField="Served" HeadingText="Impressions" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="80" />
                                    <ComponentArt:GridColumn DataField="PPT_Types" Visible="false" HeadingText="Types" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="40" />
                                    <ComponentArt:GridColumn DataField="PPF_Fits" HeadingText="Fits" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="40" />
                                    <ComponentArt:GridColumn DataField="Security_Only_Types" HeadingText="Security" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="50" />
                                    <ComponentArt:GridColumn DataField="Success" HeadingText="Success(%)" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="SuccessTemplate" HeadingCellClientTemplateId="HeadingTooltipSuccessCellTemplate" FixedWidth="true" Width="70" />
                                    <ComponentArt:GridColumn DataField="Success" HeadingText="eCPM" Align="Center" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="ECPMTemplate" FixedWidth="false" Width="40" />
                                    <ComponentArt:GridColumn DataField="Earnings" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CurrencyTemplate" FooterCellClientTemplateId="SummaryCurrencyTemplate" FixedWidth="true" Width="60" />
                                    <ComponentArt:GridColumn DataField="Add_Date" HeadingText="Add Date" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FormatString="dd/MM/yyyy" FixedWidth="true" Width="70" />
                                    <ComponentArt:GridColumn DataField="Modify_Date" HeadingText="Modify Date" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FormatString="dd/MM/yyyy" FixedWidth="true" Width="70" />
                                    <ComponentArt:GridColumn HeadingText="Actions" Align="Right" AllowSorting="False" DataCellClientTemplateId="ActionsTemplate" FixedWidth="true" Width="100" />
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
                            <ComponentArt:ClientTemplate ID="HeadingTooltipSuccessCellTemplate">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr valign="middle">
                                            <td align="left" class="headingCellText tooltip ## if (DataItem.get_allowSorting()) 'sortable' ##" rel="Success rate (in %)" style="white-space:nowrap; text-align:## DataItem.get_align() ##;">
                                                ##DataItem.get_headingText()####if (sortedDataField == DataItem.get_dataField()) '<img style="padding-left:3px;" width="' + Grid.get_levels()[0].SortImageWidth + '" height="' + Grid.get_levels()[0].SortImageHeight + '" src="' + (sortedDescending ? Grid.get_levels()[0].SortDescendingImageUrl : Grid.get_levels()[0].SortAscendingImageUrl) + '" alt="Sort" />';##
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="TotalTemplate">
                                Total
                            </ComponentArt:ClientTemplate>                
                            <ComponentArt:ClientTemplate ID="CaptchaStatus">                    
                                <font class="## DataItem.GetMember('Status').Value ##">
                                ## DataItem.GetMember("Status").Value ##
                                </font>
                            </ComponentArt:ClientTemplate>                          
                            <ComponentArt:ClientTemplate Id="SecurityLevelTemplate">
                                <img src='../images/table/## DataItem.GetMember("Security_Level_Id").Value ##.png' width="114" height="20" border="0" alt='## DataItem.GetMember("Security_Level").Value ##' />
                            </ComponentArt:ClientTemplate>                
                            <ComponentArt:ClientTemplate ID="ActionsTemplate">
                                <a class="action" style="color:#7c7c7c;font-weight:500;" href='EditCaptcha.aspx?WebsiteId=## DataItem.GetMember("Website_Id").Value ##&CaptchaId=##DataItem.GetMember("Captcha_Id").Value##'>Edit</a>
                                |
                                <a class="action" style="color:#7c7c7c;font-weight:500;" href='GetCode.aspx?WebsiteId=## DataItem.GetMember("Website_Id").Value ##&CaptchaId=##DataItem.GetMember("Captcha_Id").Value##'>Code</a>
                                |
                                <a class="action" style="color:#7c7c7c;font-weight:500;" target="_blank" href='../Captcha.aspx?public_key=## DataItem.GetMember("Public_Key").Value ##&captcha_id=##DataItem.GetMember("Captcha_Id").Value##'>#</a>
                            </ComponentArt:ClientTemplate>                
                            <ComponentArt:ClientTemplate ID="SummaryCountTemplate">
                                ## GetSubTotal(DataItem, 0) ##
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="SummaryCurrencyTemplate">
                                $## GetSubTotal(DataItem, 2) ##
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="CurrencyTemplate">
                                $## DataItem.getCurrentMember().get_value() ## 
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="SuccessTemplate">
                                  ## (DataItem.getMember("Served").get_value() == 0 ? 0 : DataItem.getMember("Success").get_value() / DataItem.getMember("Served").get_value() * 100).toFixed(2) ##
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="ECPMTemplate">
                                  $## (DataItem.getMember("Served").get_value() == 0 ? 0 : DataItem.getMember("Earnings").get_value() / DataItem.getMember("Served").get_value() * 1000).toFixed(3) ##
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate Id="LoadingFeedbackTemplate">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td style="font-size:10px;">Loading...&nbsp;</td>
                                        <td><img src="../images/table/spinner.gif" width="16" height="16" border="0" alt="Loading" /></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>

                    <p class="gridActions">
                      
                    </p>
<br />
                    <p class="note">
                    * Reports received are not processed in real-time. Information from the last 6 hours may not be available for your review.
                    </p>

                    </ContentTemplate>        
                </asp:UpdatePanel>
            </asp:Panel>

            <asp:Panel ID="PanelNoWebsites" runat="server" Visible="false" CssClass="noFound">
                <% string websiteId = Request.QueryString["WebsiteId"].ToString(); %>
                Your website have no captchas. Click <a href="NewCaptcha.aspx?WebsiteId=<%=websiteId%>">here</a> to add a new captcha.
            </asp:Panel>
        </div>
</div>
</asp:content>