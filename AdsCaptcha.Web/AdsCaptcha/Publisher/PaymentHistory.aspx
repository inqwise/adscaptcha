<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Publisher/PublisherAccount.Master" AutoEventWireup="true" CodeFile="PaymentHistory.aspx.cs" Inherits="Inqwise.AdsCaptcha.Publisher.PaymentHistory" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<link type="text/css" rel="Stylesheet" href="../css/Inqwise/selectmenu/jquery.ui.theme.css" />
<link type="text/css" rel="Stylesheet" href="../css/Inqwise/selectmenu/jquery.ui.selectmenu.css" />
<script type="text/javascript" src="http://cdn.jquerytools.org/1.2.7/full/jquery.tools.min.js"></script>  
<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jqueryui/1.9.0/jquery-ui.min.js"></script>  
<script type="text/javascript" src="../css/Inqwise/selectmenu/jquery.ui.selectmenu.js"></script> 
<style>
.tablediv .td 
{
	float:left;
	white-space:nowrap;
}
</style>
<style type="text/css" media="screen">
        ul.tabs {
        list-style:none;
        margin:0 !important;
        padding:0;
        height:44px;
    }
    ul.tabs li {
        float:left;
        text-indent:0;
        padding:0;
        margin:0 !important;
        list-style-image:none !important;
    }
    ul.tabs a {
      float: left;
      padding: 10px;
      position: relative;
      top: 2px;
      font-size: 14px;
      min-width: 140px;
      text-align: center;
      cursor: pointer;
      margin: 5px 5px 5px 0px;
      padding: 10px 20px;
      background: #ECEDEF;
      border: 1px solid #ccc;
      -moz-border-radius: 5px 5px 0 0;
      -webkit-border-radius: 5px 5px 0 0;
      border-radius: 5px 5px 0 0;
      
      /*text-shadow: 1px 1px 1px white;*/
      text-decoration:none;
      color: #AAA;
    font-family: Arial;
    font-size: 14px;
     font-weight:700;
    }
    ul.tabs a:hover {
background-color: #F6F6F6;
color: #7C7C7C;
    }
    ul.tabs a.current, ul.tabs a.current:hover, ul.tabs li.current a {
      background: white;
      border-bottom: 1px solid white;
      font-weight:700;
      color: #19C12D;
      z-index: 101;
    }
    .panes .pane {
        display:none;
    }
    .panes {
      padding: 10px 10px 10px 10px;
      background-color: white;
      position: relative;
      z-index: 100;
      
    }
    .panes
    {
    	border: 1px solid #ccc;
    }
    .container {
       /*background: -moz-radial-gradient(center center , ellipse farthest-corner, #FFFFFF 0%, #EAEAEA 100%) repeat scroll 0 0 transparent;
    border: 2px solid #FFFFFF;
    box-shadow: 0 5px 22px #BBBBBB;
    height: 370px;*/
    margin: 0px auto;
    overflow: hidden;
    padding: 50px 0  0 20px;
    position: relative;
    width: auto;
}
.calendar
    {
    	background-color:#fff;
    	border:1px solid Gray;
    	}
    	
    	#top-content
    	{
    		padding:0 0 30px 0;
    	}
  </style>
</asp:content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Payment History
</asp:Content>
<asp:content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <!--div id="breadCrambs">
            <asp:Label ID="labelNavigationPath" runat="server" />        
        </div-->


        <div id="content"  class="container">
        <div class="inner-content">

            <asp:Panel ID="PanelGrid" runat="server" Visible="true">        
                <asp:ScriptManager ID="ScriptManager" runat="server" OnInit="scriptManagerOnInit" EnablePartialRendering="true" />
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>
                    <!--div class="menu-right">
                <li class="active"><a href="EarningsSummary.aspx">Earnings Summary</a></li>
                <li><a href="PaymentHistory.aspx">History</a></li>
             </div>
            <div class="clear"></div>
            <br />
             <ul class="tabs">
	            <li><a href="EarningsSummary.aspx">Earnings Summary</a></li>
	            <li><a href="PaymentHistory.aspx" class="current">Payment History</a></li>
            </ul-->
          
               <div id="top-content">
                    <div class="left">
                     <label>Dates:</label>
                      <asp:DropDownList ID="listFilterDate" runat="server" AutoPostBack="true" CssClass="SelectField"
                                    OnSelectedIndexChanged="listFilterDate_SelectedIndexChanged">
                       </asp:DropDownList>  
                     <asp:Panel ID="panelCustomDatesFilter" runat="server"  CssClass="description" style="position:absolute;z-index:100;background-color:White;border:1px solid Gray;padding:10px;">
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
                     <asp:UpdateProgress ID="UpdateProgress" runat="server"> 
                                    <ProgressTemplate>
                                        <span style="font-size: 10px;">Loading...&nbsp;</span><img src="../images/table/spinner.gif" width="16" height="16" border="0" alt="Loading" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>     
                    </div>
        	         <div class="menu-right">
        	                <ul  style="width:auto;text-align:right;">
                            <li><a href="EarningsSummary.aspx">Earnings Summary</a></li>
                            <li class="none active"><a href="PaymentHistory.aspx">History</a></li>
                            </ul>
                        </div>
                    </div>
                    
                                <div class="clear"></div>
            <br />
      
                    
                    
                
                    <div class="description">


                    <script type="text/javascript">
                        var sortedDataField = '';
                        var sortedDescending = false;
                        function Grid_onSortChange(sender, e) {
                            sortedDataField = e.get_column().get_dataField();
                            sortedDescending = e.get_descending();
                        }
                        function ViewPaymentDetails(id) {
                            window.open('ViewPaymentDetails.aspx?PaymentId=' + id + '', 'Payment Details', 'width=600,height=400');
                        }
                    </script>

                    <ComponentArt:Grid ID="Grid" 
                        RunningMode="Client"
                        CssClass="table"
                        Width="100%"
                        DataAreaCssClass="gridData"
                        FooterCssClass="gridFooter"
                        ImagesBaseUrl="../images/table"
                        ShowHeader="false"
                        ShowFooter="false"
                        AllowColumnResizing="false"
                        AllowMultipleSelect="false"
                        EmptyGridText="No payments found."
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
                                    <ComponentArt:GridColumn DataField="Payment_Id" Visible="False" />
                                    <ComponentArt:GridColumn DataField="Publisher_Id" Visible="False" />
                                    <ComponentArt:GridColumn DataField="Payment_Date" HeadingText="Payment Date" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FooterCellClientTemplateId="TotalTemplate" FixedWidth="true" Width="140" />
                                    <ComponentArt:GridColumn DataField="Amount" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CurrencyTemplate" FooterCellClientTemplateId="SummaryCurrencyTemplate" FixedWidth="true" Width="100" />
                                    <ComponentArt:GridColumn HeadingText="Payment Method" DataField="Credit_Method" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FixedWidth="true" Width="140" />
                                    <ComponentArt:GridColumn HeadingText="Details" Align="Left" AllowSorting="False" DataCellClientTemplateId="PaymentDetails" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        
                        <ClientTemplates>                        
                            <ComponentArt:ClientTemplate Id="HeadingSortableCellTemplate">
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
                            <ComponentArt:ClientTemplate ID="PaymentDetails">
                                <a href="javascript:ViewPaymentDetails('## DataItem.GetMember("Payment_Id").Value ##');">View Details</a>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="TotalTemplate">
                                Total
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
                    </div>
                    
                    
                      
            <div class="description">
<br />
                    <div class="table tablediv">
        	<div class="top tr header">
            	<div class="td w300">Total Earnings</div>
                <div class="td w300">Total Payments</div>
                <div class="td w300">To Be Paid</div>
                <div class="clear"></div>
            </div>
            <div class="tr">
            	<div class="td w300"><asp:Literal ID="labelEarningsSum" runat="server"></asp:Literal></div>
                <div class="td w300"><asp:Literal ID="labelPaymentsSum" runat="server"></asp:Literal></div>
                <div class="td w300"><asp:Literal ID="labelDiff" runat="server"></asp:Literal></div>

                <div class="clear"></div>
            </div>
            <div class="tr footer">
            	<div class="td w300"></div>
                <div class="td w300"></div>
                <div class="clear"></div>
            </div>
        </div><!--end table-->
       

        </div>
        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div> 
</asp:content>