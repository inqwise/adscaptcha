<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Advertiser/AdvertiserAccount.Master" AutoEventWireup="true" CodeFile="BillingSummary.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.BillingSummary" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
.tablediv .td 
{
	float:left;
	white-space:nowrap;
}
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

</style>
</asp:content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Billing Summary
</asp:Content>
<asp:content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    function Print() {
    var url = "Print.aspx";
    url += "?tr=" + $("#<%=listTransaction.ClientID %>").val()
    url += "&dates=" + $("#<%=labelDatesTitle.ClientID %>").html();
            window.open(encodeURI(url), "Print window");
        
    }

</script>


    <!--div id="navigation">
        <div class="navigation">
            <ul>
                <li><a href="ManageCampaigns.aspx">Campaigns</a></li>
                <li class="selected"><a href="BillingSummary.aspx">Billing</a></li>                
                <li><a href="AccountPreferences.aspx">My Account</a></li>
            </ul>
        </div>
    </div>
    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li>Billing Summary</li>
            </ul>
        </div>
                <div id="breadCrambs">
            <asp:Label ID="labelNavigationPath" runat="server" />        
        </div>
        
    </div-->
    

        <div id="content">
               <div class="inner-content">
            <asp:ScriptManager ID="ScriptManager" runat="server" OnInit="scriptManagerOnInit" EnablePartialRendering="true" />
            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                <ContentTemplate>
                 <!--div class="menu-right">
                <li class="active"><a href="BillingSummary.aspx">Billing Summary</a></li>
             </div>
            <div class="clear"></div-->
            <br />

                <!-- System messages -->
                <div id="SystemMessagesHolder" class="systemMessages" runat="server" visible="false">
                    <div class="close"><a href="javascript:HideSystemMessages();">X</a></div>
                    <asp:Label ID="labelSystemMessages" runat="server" Text=""></asp:Label>
                </div>

                <div class="clear"></div>
                
          <div class="table tablediv">
        	<div class="top tr header">
            	<div class="td w200">Last Payment</div>
                <div class="td w200">Billing Method</div>
                <div class="td w200">Payment Method</div>
                <div class="td w200">Current Balance</div>
                <div class="clear"></div>
            </div>
            <div class="tr">
            	<div class="td w200"><asp:Literal ID="labelLastPayment" runat="server" ></asp:Literal></div>
                <div class="td w200"><asp:Literal ID="labelBillingMethod" runat="server" ></asp:Literal></div>
                <div class="td w200"><asp:Literal ID="labelPaymentMethod" runat="server" ></asp:Literal></div>
                <div class="td w200"><b><asp:Literal ID="labelBalance" runat="server" ></asp:Literal></b></div>
                <div class="clear"></div>
            </div>
            <div class="tr footer">
            	             <asp:Panel ID="panelPrePay" CssClass="td w300" runat="server" Visible="false">
                                <asp:LinkButton ID="linkAddCredit" runat="server" Text="Click here to add more credit" ForeColor="White"></asp:LinkButton>
                            </asp:Panel>
                            <asp:Panel ID="panelPostPay" CssClass="td w300" runat="server" Visible="false">
                                You will be billed every <asp:Literal ID="labelBillingAmount" runat="server"></asp:Literal>
                            </asp:Panel>  
                <div class="clear"></div>
            </div>
        </div><!--end table-->
<br />

                <br />
                 <br />
                <h4><asp:Label ID="labelDatesTitle" runat="server" style="position: relative;top: 0px;"></asp:Label>&nbsp;&nbsp;&nbsp;<img id="imgPrint" src="../Images/printer.png" onclick="Print()" alt="print report" style="cursor:hand;cursor:pointer;width:28px;height:28px;" /></h4>
                <div class="clear"></div>
                <div class="description">
                    <div id="top-content" style="padding-top: 10px;">
        	                <div class="left" style="margin-left: 10px; padding-bottom: 10px;">
                <table width="100%">
                    <tr>
                        <td width="80"><b>Dates:</b></td>
                        <td>
                            <asp:DropDownList ID="listFilterDate" runat="server" AutoPostBack="true" CssClass="SelectField"
                                OnSelectedIndexChanged="listFilterDate_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Panel ID="panelCustomDatesFilter" runat="server">
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
                            <img ID="calendar_to_button" alt="To Date" class="calendar_button" src="../images/calendar.jpg"
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
                        </td>
                        <td rowspan="2" valign="top">
                            <asp:UpdateProgress ID="UpdateProgress" runat="server"> 
                                <ProgressTemplate>
                                    <span style="font-size: 10px;">Loading...&nbsp;</span><img src="../images/table/spinner.gif" width="16" height="16" border="0" alt="Loading" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>                    
                        </td>
                    </tr>
                    <tr>
                        <td><b>Transaction:</b></td>
                        <td colspan="2">
                            <asp:DropDownList ID="listTransaction" runat="server" AutoPostBack="true" 
                                CssClass="SelectField" DataValueField="Item_Id" DataTextField="Item_Desc" 
                                onselectedindexchanged="listTransaction_SelectedIndexChanged" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                            </div>
                    </div>
                </div>
                <br />

                <script type="text/javascript">
                    var sortedDataField = '';
                    var sortedDescending = false;
                    function Grid_onSortChange(sender, e) {
                        sortedDataField = e.get_column().get_dataField();
                        sortedDescending = e.get_descending();
                    }
                    function ViewPaymentDetails(id) {
                        window.open('ViewPaymentDetails.aspx?PaymentId=' + id + '','Payment Details','width=600,height=400');
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
                    EmptyGridText="No transactions found."
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
                            DataMember="Billings"
                            DataKeyField="Billing_Date"
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
                                <ComponentArt:GridColumn DataField="Payment_Id" Visible="False" />
                                <ComponentArt:GridColumn DataField="Date" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FormatString="MMM dd, yyyy" FixedWidth="true" Width="100" />
                                <ComponentArt:GridColumn DataField="Description" HeadingText="Description" Align="Left" AllowSorting="False" DataCellClientTemplateId="DescriptionTemplate" />
                                <ComponentArt:GridColumn DataField="Type_Id" Visible="False" />
                                <ComponentArt:GridColumn DataField="Type" Visible="False" HeadingText="Transaction Type" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" />
                                <ComponentArt:GridColumn DataField="Charge" HeadingText="Charges" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CurrencyTemplate" FixedWidth="true" Width="100" />
                                <ComponentArt:GridColumn DataField="Credit" HeadingText="Credits" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CurrencyTemplate" FixedWidth="true" Width="100" />
                                <ComponentArt:GridColumn DataField="Balance" HeadingText="Balance" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CurrencyTemplate" FixedWidth="true" Width="100" />
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
                        <ComponentArt:ClientTemplate ID="DescriptionTemplate">
                            ## DataItem.getCurrentMember().get_value() ##
                            ## if (DataItem.GetMember("Payment_Id").Value != null) 
                                '<span class="Explanation">(<a href="javascript:ViewPaymentDetails(' + DataItem.GetMember("Payment_Id").Value + ');">View Details</a>)</span>'
                            ##
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="CurrencyTemplate">
                            ## if (DataItem.getCurrentMember().get_value() != null) '$'####DataItem.getCurrentMember().get_value() ##
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

                </ContentTemplate>        
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>