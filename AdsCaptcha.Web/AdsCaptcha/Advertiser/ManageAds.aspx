<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Advertiser/AdvertiserAccount.Master" AutoEventWireup="true" CodeFile="ManageAds.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.ManageAds" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../js/jquery.tooltip.pack.js" type="text/javascript"></script>
    <link href="../css/jquery.tooltip.css" type="text/css" rel="stylesheet" />
    <link type="text/css" rel="Stylesheet" href="../css/Inqwise/selectmenu/jquery.ui.theme.css" />
<link type="text/css" rel="Stylesheet" href="../css/Inqwise/selectmenu/jquery.ui.selectmenu.css" />
<script type="text/javascript" src="http://cdn.jquerytools.org/1.2.7/full/jquery.tools.min.js"></script>  
<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jqueryui/1.9.0/jquery-ui.min.js"></script>  
<script type="text/javascript" src="../css/Inqwise/selectmenu/jquery.ui.selectmenu.js"></script>  
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
	.table .td a {
	    color: #2CB677;
	}
  </style>
    <script type="text/javascript" charset="utf-8">
        var ADSCAPTCHA_URL = '<%=ConfigurationSettings.AppSettings["AWSCloudFront"]%>';
        var imgloader = '<%=ConfigurationSettings.AppSettings["URL"]%>Images/preloader.gif';
        var arrImages = new Array();
        $(document).ready(function() {
            $('.adrow').tooltip({
                delay: 0,
                track: true,
                showURL: false,
                bodyHandler: function() {
                    //debugger;
                    if ($(this).find("span.AdImage").html() != null) {



                        var iurl = ADSCAPTCHA_URL + $(this).find("span.AdImage").html().trim();
                        //alert(iurl);
                        var divImg = $("<div/>");

                        if (arrImages[iurl] != undefined) {
                            $(divImg).html("<img src='" + iurl + "'/>");
                        }
                        else {
                            $(divImg).html("<img src='" + imgloader + "'/>");


                            if (($.browser.msie) && ($.browser.version < 9)) {
                                img.src = iurl;
                                $(divImg).html("");
                                $(divImg).append($(this));
                            }
                            else {
                                var img = $("<img/>").attr("src", iurl);
                                img.hide();
                                img.load(function() {
                                    $(divImg).html("");

                                    $(divImg).append($(this));
                                    $(this).fadeIn("slow");
                                    arrImages[$(this).attr("src")] = 1;
                                });
                            }
                        }

                        return divImg; //$("<img/>").attr("src", iurl);
                    }
                    else {
                        try { $(this).tooltip.block; } catch (ee) { }
                        return false;
                    }
                }
            });
        });
    </script>
    <style type="text/css">
   .InputField, .TextareaField
   {
   border: 2px solid #DEDEDD;
    border-radius: 3px 3px 3px 3px;
    color: #333333;
    font: 20px Arial,Helvetica,sans-serif;
    padding: 6px;
    width: 200px;
   }
   
   .SelectField
   {
   	width: 215px;
   }
   
   #buttonHolder
   {
   	margin-top:30px;
   	padding-bottom: 50px;
   }
   
   .buttonHolder a
   {
   	color:#FFFFFF;
   }
   
   .container {
    /*background: -moz-radial-gradient(center center , ellipse farthest-corner, #FFFFFF 0%, #EAEAEA 100%) repeat scroll 0 0 transparent;
    border: 2px solid #FFFFFF;
    box-shadow: 0 5px 22px #BBBBBB;
    height: 370px;*/
    margin: 0px auto;
    overflow: hidden;
    padding: 50px 0  0 10px;
    position: relative;
    width: auto;
}

#content h4
{
	background-position: left 4px !important;
    margin-bottom: 30px;
    line-height: 1;
}

.FieldHeader
{
	white-space:nowrap;
}
#content .inner-content, #content-form {
       padding: 0px 0 40px 0;
    margin: 0 auto;
    width: 1050px;
}
 .calendar
    {
    	background-color:#fff;
    	border:1px solid Gray;
    	}
    	
    	#top-content .menu-right
    	{
    		width:auto;
    	}
    	#top-content
    	{
    		padding:0;
    	}
   </style>
</asp:content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Manage Ads
</asp:Content>
<asp:content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--div id="navigation">
        <div class="navigation">
            <ul>
                <li class="selected"><a href="ManageCampaigns.aspx">Campaigns</a></li>
                <li><a href="BillingSummary.aspx">Billing</a></li>
                <li><a href="AccountPreferences.aspx">My Account</a></li>
            </ul>
        </div>
    </div>
    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li>Manage</li>
                <li><a href="NewCampaign.aspx">New Campaign</a></li>
            </ul>
        </div>
    </div>
         <div id="breadCrambs">
            <asp:Label ID="labelNavigationPath" runat="server" />        
        </div-->
        
        <div id="content"  class="container">
        <div class="inner-content">
            <asp:Panel ID="PanelGrid" runat="server" Visible="true">        
                <asp:ScriptManager ID="ScriptManager" runat="server" OnInit="scriptManagerOnInit" EnablePartialRendering="true" />
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>
                 <!--div class="menu-right">
                <li class="active"><a href="ManageAds.aspx">Manage Ads</a></li>
                <li><a href="NewAd.aspx?CampaignId=<%=Page.Request.QueryString["CampaignId"]%>">New Ad</a></li>
             </div>
            <div class="clear"></div>
            <br />
            
            
             <ul class="tabs">
	            <li></li>
	            <li></li>
            </ul-->
            
                                       
              <div id="SystemMessagesHolder" class="systemMessages" runat="server" visible="false">
                <div class="close"><a href="javascript:HideSystemMessages();">X</a></div>
                <asp:Label ID="labelSystemMessages" runat="server" Text=""></asp:Label>
            </div>             

            
            <div class="decsription">
             <div id="top-content">
        	                <div class="left" style="padding:10px 0 10px 0;">
        	                <label>Dates:</label> 
        	                <asp:DropDownList ID="listFilterDate" runat="server" AutoPostBack="true" Width="100px" 
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
                                CssClass="SelectField" DataValueField="Item_Id" DataTextField="Item_Desc" 
                                onselectedindexchanged="listStatus_SelectedIndexChanged" Width="120" />
                                <asp:UpdateProgress ID="UpdateProgress" runat="server"> 
                                    <ProgressTemplate>
                                        <span style="font-size: 10px;">Loading...&nbsp;</span><img src="../images/table/spinner.gif" width="16" height="16" border="0" alt="Loading" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                </div>
                            <div class="menu-right">
            	                <li class="active"><a  href="ManageAds.aspx?CampaignId=<%=Page.Request.QueryString["CampaignId"]%>" class="current">Manage Ads</a></li>
                                <li class="none"><a  href="NewAd.aspx?CampaignId=<%=Page.Request.QueryString["CampaignId"]%>">New Ad</a></li>
                            </div>
                          
                          </div>
             <br />
            
           
            
                    <!-- System messages -->
                    

                    <!--table id="SummaryTable">
                        <tr>
                            <td><asp:Label ID="labelChargesTitle" runat="server" Text="Total Charges:"></asp:Label></td>
                            <td><asp:Label ID="labelChargesSum" runat="server" CssClass="Total"></asp:Label></td>
                        </tr>
                    </table>
                    
                    <h4>Ads</h4-->
                    
                    
                    <!--table width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="80"><b>Dates:</b></td>
                            <td>
                                                     
                            </td>
                            <td>                      
                               
                            </td>
                            <td rowspan="2" valign="top">
                               
                            </td>
                            <td rowspan="2" style="float:right;display:none;">
                                <% string campaignId = Request.QueryString["CampaignId"].ToString(); %>                                
                                <a href="NewAd.aspx?CampaignId=<%=campaignId%>" class="button"><span>+ Add New Ad</span></a>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Statuses:</b></td>
                            <td colspan="2">
                                <<asp:LinkButton ID="linkFilterStatusAll" runat="server" CausesValidation="false" 
                                    OnClick="linkFilterStatusAll_Click">All Statuses</asp:LinkButton>
                                |
                                <asp:LinkButton ID="linkFilterStatusRunningPending" runat="server" CausesValidation="false"                       
                                    OnClick="linkFilterStatusRunningPending_Click">Running & Pending</asp:LinkButton>
                                |
                                <asp:LinkButton ID="linkFilterStatusPausedRejected" runat="server" CausesValidation="false" 
                                    OnClick="linkFilterStatusPausedRejected_Click">Paused & Rejected</asp:LinkButton>
                                 
                            </td>
                        </tr>
                    </table>                       

                    <br /-->

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
                        DataAreaCssClass="gridData"
                        FooterCssClass="gridFooter"
                        ImagesBaseUrl="../images/table"
                        ShowHeader="false"
                        ShowFooter="false"
                        AllowColumnResizing="false"
                        AllowMultipleSelect="false"
                        EmptyGridText="No ads found."
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
                                    <ComponentArt:GridColumn DataField="Ad_Id" HeadingText="Ad Id" Visible="False" />
                                    <ComponentArt:GridColumn DataField="Ad_Name" HeadingText="Name" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FooterCellClientTemplateId="TotalTemplate" Width="120" />
                                    <ComponentArt:GridColumn DataField="Ad_Slogan" Visible="false" HeadingText="Slogan" Align="Left" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" Width="160" />
                                    <ComponentArt:GridColumn DataField="Ad_Image" HeadingText=""  Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="AdImage" FixedWidth="true" Width="1"  />
                                    <ComponentArt:GridColumn DataField="Status" HeadingText="Status" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DataCellClientTemplateId="AdStatus" FixedWidth="true" Width="60" />
                                    <ComponentArt:GridColumn DataField="Type" HeadingText="Type" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True"  Width="80"/>
                                    <ComponentArt:GridColumn DataField="Max_Cpt" HeadingText="Max. Bid" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CurrencyTemplate" FixedWidth="true" Width="75" />
                                    <ComponentArt:GridColumn DataField="Types" HeadingText="Types" Visible="false" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="50" />
                                    <ComponentArt:GridColumn DataField="Fits" HeadingText="Fits" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="50" />
                                    <ComponentArt:GridColumn DataField="Clicked" HeadingText="Clicks" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" FormatString="N0" FooterCellClientTemplateId="SummaryCountTemplate" FixedWidth="true" Width="50" />
                                    <ComponentArt:GridColumn DataField="Charges" HeadingText="Charges" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CurrencyTemplate" FooterCellClientTemplateId="SummaryCurrencyTemplate" FixedWidth="true" Width="70" />
                                    <ComponentArt:GridColumn DataField="Clicked" HeadingText="Average CPC" Align="Center" HeadingCellClientTemplateId="HeadingTooltipCPCTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="AverageCPCTemplate" FixedWidth="true" Width="90" />
                                    <ComponentArt:GridColumn DataField="Fits" HeadingText="Average CPF" Align="Center" HeadingCellClientTemplateId="HeadingTooltipCPFTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CPFTemplate" FixedWidth="true" Width="90" />
                                    <ComponentArt:GridColumn DataField="Add_Date" HeadingText="Add Date" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FormatString="dd/MM/yyyy" FixedWidth="true" Width="70" />
                                    <ComponentArt:GridColumn DataField="Modify_Date" HeadingText="Modify Date" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FormatString="dd/MM/yyyy" FixedWidth="true" Width="80" />
                                    <ComponentArt:GridColumn HeadingText="Actions" Align="Center" AllowSorting="False" DataCellClientTemplateId="ActionsTemplate" FixedWidth="true" Width="50" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="HeadingSortableCellTemplate">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr valign="middle" >
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
                            <ComponentArt:ClientTemplate ID="AdStatus">                    
                                <font class="## DataItem.GetMember('Status').Value ##">
                                ## DataItem.GetMember("Status").Value ##
                                </font>
                            </ComponentArt:ClientTemplate>    
                            <ComponentArt:ClientTemplate ID="AdImage">                    
                                <span style="display:none;" class="AdImage">
                                ## DataItem.GetMember("Ad_Image").Value ##
                                </span>
                            </ComponentArt:ClientTemplate>                      
                            <ComponentArt:ClientTemplate Id="SecurityLevelTemplate">
                                <img src='../images/table/## DataItem.GetMember("Security_Level_Id").Value ##.png' class="pngfix" width="114" height="20" border="0" alt='## DataItem.GetMember("Security_Level").Value ##' />
                            </ComponentArt:ClientTemplate>                
                            <ComponentArt:ClientTemplate ID="ActionsTemplate">
                                <a class="action" href='EditAd.aspx?CampaignId=## DataItem.GetMember("Campaign_Id").Value ##&AdId=##DataItem.GetMember("Ad_Id").Value##'>Edit</a>
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
                            <ComponentArt:ClientTemplate ID="AverageCPCTemplate">
                                $## (DataItem.GetMember("Clicked").Value==0 ? 0 : DataItem.GetMember("Charges").Value/DataItem.GetMember("Clicked").Value).toFixed(3) ##
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="CPFTemplate">
                                $## (DataItem.GetMember("Fits").Value==0 ? 0 : DataItem.GetMember("Charges").Value/DataItem.GetMember("Fits").Value).toFixed(3) ##
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="HeadingTooltipCPFTemplate">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr valign="middle">
                                            <td align="left" class="headingCellText tooltip ## if (DataItem.get_allowSorting()) 'sortable' ##" rel="Average Cost-Per-Fit" style="white-space:nowrap; text-align:## DataItem.get_align() ##;">
                                                ##DataItem.get_headingText()####if (sortedDataField == DataItem.get_dataField()) '<img style="padding-left:3px;" width="' + Grid.get_levels()[0].SortImageWidth + '" height="' + Grid.get_levels()[0].SortImageHeight + '" src="' + (sortedDescending ? Grid.get_levels()[0].SortDescendingImageUrl : Grid.get_levels()[0].SortAscendingImageUrl) + '" alt="Sort" />';##
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="HeadingTooltipCPCTemplate">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr valign="middle">
                                            <td align="left" class="headingCellText tooltip ## if (DataItem.get_allowSorting()) 'sortable' ##" rel="Average Cost-Per-Click" style="white-space:nowrap; text-align:## DataItem.get_align() ##;">
                                                ##DataItem.get_headingText()####if (sortedDataField == DataItem.get_dataField()) '<img style="padding-left:3px;" width="' + Grid.get_levels()[0].SortImageWidth + '" height="' + Grid.get_levels()[0].SortImageHeight + '" src="' + (sortedDescending ? Grid.get_levels()[0].SortDescendingImageUrl : Grid.get_levels()[0].SortAscendingImageUrl) + '" alt="Sort" />';##
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
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

                    <p style="margin-top:10px;display:none;">
                        <a href="ManageCampaigns.aspx"><< Back</a>
                    </p>

                    <p class="note">
                    * Reports received are not processed in real-time. Information from the last 6 hours may not be available for your review.
                    </p>
                    
                  <asp:Panel ID="PanelNoCampaigns" runat="server" Visible="false" CssClass="noFound">
                <% string campaignId = Request.QueryString["CampaignId"].ToString(); %>
                Your campaign have no ads. Click <a href="NewAd.aspx?CampaignId=<%=campaignId%>">here</a> to add a new ad.
            </asp:Panel>
            
            </div>
            </div>
                    </ContentTemplate>        
                </asp:UpdatePanel>
            </asp:Panel>

          
        </div>    
    </div>
</asp:content>