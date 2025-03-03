<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="ViewCharges.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.ViewCharges" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_5').addClass('selected');
        });
    </script>    
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li><a href="ViewCharges.aspx" class="selected">Charges From Advertisers</a></li>
                <li><a href="ViewPaymentsPub.aspx">Payments To Site Owners</a></li>
                <li><a href="ViewPaymentsDev.aspx">Payments To Developers</a></li>
                <li><a href="ViewProfits.aspx">Daily Net Income</a></li>
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
                <table>
                    <tr>
                        <td><b>Advertisers:</b></td>
                        <td>
                            <asp:DropDownList ID="listAdvertisers" runat="server" AutoPostBack="true"
                                CssClass="SelectField"  
                                onselectedindexchanged="listAdvertisers_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td><b>From:</b></td>
                        <td>
                            <ComponentArt:Calendar 
                                ID="PickerFrom"
                                runat="server"
                                PickerFormat="Custom"
                                PickerCustomFormat="dd/MM/yyyy"
                                ControlType="Picker"
                                PickerCssClass="picker"
                                OnSelectionChanged="PickerFrom_SelectionChanged"
                                AutoPostBackOnSelectionChanged="true">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="PickerFrom_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>
                            <img id="calendar_from_button" alt="From Date" class="calendar_button" src="images/Calendar.jpg" 
                                onclick="ButtonFrom_OnClick(event)" onmouseup="ButtonFrom_OnMouseUp(event)" />
                        
                            <ComponentArt:Calendar runat="server"
                                ID="CalendarFrom"
                                AllowMultipleSelection="false"
                                AllowWeekSelection="false"
                                AllowMonthSelection="false"
                                ControlType="Calendar"
                                PopUp="Custom"
                                PopUpExpandControlId="calendar_from_button"
                                CalendarTitleCssClass="title"
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
                                ImagesBaseUrl="images"
                                PrevImageUrl="CalendarPrevMonth.gif"
                                NextImageUrl="CalendarNextMonth.gif"
                                OnSelectionChanged="CalendarFrom_SelectionChanged"
                                AutoPostBackOnSelectionChanged="true">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="CalendarFrom_OnChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>
                        </td>
                   </tr>
                    <tr>
                        <td><b>To:</b></td>
                        <td>
                            <ComponentArt:Calendar 
                                ID="PickerTo"
                                runat="server"
                                PickerFormat="Custom"
                                PickerCustomFormat="dd/MM/yyyy"
                                ControlType="Picker"
                                PickerCssClass="picker"
                                OnSelectionChanged="PickerTo_SelectionChanged"
                                AutoPostBackOnSelectionChanged="true">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="PickerTo_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>
                            <img id="calendar_to_button" alt="To Date" class="calendar_button" src="images/Calendar.jpg"
                                onclick="ButtonTo_OnClick(event)" onmouseup="ButtonTo_OnMouseUp(event)" />

                            <ComponentArt:Calendar runat="server"
                                ID="CalendarTo"
                                AllowMultipleSelection="false"
                                AllowWeekSelection="false"
                                AllowMonthSelection="false"
                                ControlType="Calendar"
                                PopUp="Custom"
                                PopUpExpandControlId="calendar_to_button"
                                CalendarTitleCssClass="title"
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
                                ImagesBaseUrl="images"
                                PrevImageUrl="CalendarPrevMonth.gif"
                                NextImageUrl="CalendarNextMonth.gif"
                                OnSelectionChanged="CalendarTo_SelectionChanged"
                                AutoPostBackOnSelectionChanged="true">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="CalendarTo_OnChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>
                        </td>
                    </tr>
                </table>
                
                <br />
                
                <asp:UpdateProgress ID="UpdateProgress" runat="server"> 
                    <ProgressTemplate>
                        <span style="font-size: 10px;">Loading...&nbsp;</span><img src="images/table/spinner.gif" width="16" height="16" border="0" alt="Loading" />
                    </ProgressTemplate>
                </asp:UpdateProgress>                    

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
                    EmptyGridText="No charges found."
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
                            DataKeyField="Payment_Id"
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
                                <ComponentArt:GridColumn DataField="Payment_Id" HeadingText="#" FixedWidth="true" Width="40" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="IdTemplate" />
                                <ComponentArt:GridColumn DataField="Date" Align="Center" FixedWidth="true" Width="70" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FormatString="dd/MM/yyyy" />
                                <ComponentArt:GridColumn DataField="Advertiser_Id" HeadingText="ID" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="AdvertiserIdTemplate" Width="30" />
                                <ComponentArt:GridColumn DataField="Email" HeadingText="Advertiser" Align="Left" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="NameTemplate" />
                                <ComponentArt:GridColumn DataField="Payment_Method_Id" Visible="false" />
                                <ComponentArt:GridColumn DataField="Payment_Method" HeadingText="Charge Method" FixedWidth="true" Width="90" Align="Left" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" />
                                <ComponentArt:GridColumn DataField="Amount" FixedWidth="true" Width="55" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="C0" />
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
                        <ComponentArt:ClientTemplate ID="LoadingFeedbackTemplate">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td style="font-size:10px;">Loading...&nbsp;</td>
                                    <td><img src="images/table/spinner.gif" width="16" height="16" border="0" alt="Loading" /></td>
                                </tr>
                            </table>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="IdTemplate">
                            <a href="#?PaymentId=## DataItem.getMember('Payment_Id').get_value() ##">## DataItem.getMember('Payment_Id').get_value() ##</a>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="AdvertiserIdTemplate">
                            <a href="ManageCampaigns.aspx?AdvertiserId=## DataItem.getMember('Advertiser_Id').get_value() ##">## DataItem.getMember('Advertiser_Id').get_value() ##</a>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="NameTemplate">
                            <a href="mailto:## DataItem.getMember('Email').get_value() ##">## DataItem.getMember('Email').get_value() ##</a>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
        </div> 
    </div>
    
</asp:content>