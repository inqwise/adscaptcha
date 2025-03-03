<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="StartPage.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.StartPage" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="am.Charts" Namespace="am.Charts" TagPrefix="amCharts" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_1').addClass('selected');
        });
    </script>    
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li><a href="StartPage.aspx" class="selected">Dashboard</a></li>
            </ul>
        </div>
    </div>
    <div class="warp">
        <div id="content">
        
        <div id="breadCrambs">
            <asp:Label ID="labelBreadCrambs" runat="server" />
        </div>

        <asp:ScriptManager ID="ScriptManager" runat="server" OnInit="scriptManagerOnInit" EnablePartialRendering="true" />
                                                
        <div class="DashboardSection">
            <div class="DashboardBox">
                <div class="DashboardBoxHeader">
                    <span>Served CAPTCHAs</span>
                </div>
                <div class="DashboardBoxContent">                                        
                    <amCharts:LineChart ID="graph" runat="server" 
                        Width="100%" Height="160px"                        
                        EnableViewState="false">
                    </amCharts:LineChart>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>

            <div class="DashboardSection">
                <div class="DashboardBox">
                    <div class="DashboardBoxHeader">
                        <span>Overview</span>
                        <div style="position:absolute;right:10px;top:9px;">
                            <asp:DropDownList ID="listFilterDate" runat="server" AutoPostBack="true" Width="100px" 
                                OnSelectedIndexChanged="listFilterDate_SelectedIndexChanged">
                            </asp:DropDownList>                         
                            <asp:Panel ID="panelCustomDatesFilter" runat="server">
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
                            <img id="calendar_from_button" alt="From Date" class="calendar_button" src="images/Calendar.jpg" 
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
                            <img ID="calendar_to_button" alt="To Date" class="calendar_button" src="images/Calendar.jpg"
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
                                ImagesBaseUrl="images"
                                PrevImageUrl="CalendarPrevMonth.gif"
                                NextImageUrl="CalendarNextMonth.gif">
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
                                ImagesBaseUrl="images"
                                PrevImageUrl="CalendarPrevMonth.gif"
                                NextImageUrl="CalendarNextMonth.gif">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="CalendarTo_OnChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>                            
                            <asp:LinkButton ID="linkUpdateCustomDates" runat="server" 
                                    onclick="linkUpdateCustomDates_Click">Update</asp:LinkButton>                       
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="DashboardBoxContent Measures">
                        
                        <table id="OverviewTable">
                            <tr>
                                <td>
                                    <span class="OverviewTitle">CAPTCHAs</span>
                                    <ul>
                                        <li><asp:Label ID="labelServed" runat="server"></asp:Label> <a href="ViewRequests.aspx">Served *</a></li>
                                        <li><asp:Label ID="labelTyped" runat="server"></asp:Label> (<asp:Label ID="labelTypedPct" runat="server"></asp:Label>) <a href="#">Typed *</a></li>
                                        <li><asp:Label ID="labelTypedCorrect" runat="server"></asp:Label> (<asp:Label ID="labelTypedCorrectPct" runat="server"></asp:Label>) <a href="#">Correct *</a></li>
                                        <li><asp:Label ID="labelTypedIncorrect" runat="server"></asp:Label> (<asp:Label ID="labelTypedIncorrectPct" runat="server"></asp:Label>) <a href="#">Incorrect *</a></li>
                                    </ul>
                                    <span class="OverviewTitle">Finance</span>
                                    <ul>
                                        <li><asp:Label ID="labelIncome" runat="server"></asp:Label> <a href="ViewCharges.aspx">From Advertisers *</a></li>
                                        <li><asp:Label ID="labelOutcomePub" runat="server"></asp:Label> <a href="ViewPaymentsPub.aspx">To Site Owners *</a></li>
                                        <li><asp:Label ID="labelOutcomeDev" runat="server"></asp:Label> <a href="ViewPaymentsDev.aspx">To Developers *</a></li>
                                        <li><asp:Label ID="labelProfit" runat="server"></asp:Label> <a href="ViewProfits.aspx">Net Income *</a></li>
                                    </ul>
                                </td>
                                <td class="Seperator"></td>
                                <td>
                                    <span class="OverviewTitle">Advertisers</span>
                                    <ul>
                                        <li><asp:Label ID="labelAdvertisers" runat="server"></asp:Label> <!--[<asp:Label ID="labelActiveAdvertisers" runat="server"></asp:Label>] --><a href="ManageAdvertisers.aspx">Total<!-- [Active]--></a></li>
                                        <li><asp:Label ID="labelNewAdvertisers" runat="server"></asp:Label> <a href="#">New *</a></li>
                                        <li><asp:Label ID="labelAdvertiserAvgValue" runat="server"></asp:Label> <a href="#">Avg. Value</a></li>
                                    </ul>                                    
                                    <span class="OverviewTitle">Ads</span>
                                    <ul>
                                        <li><asp:Label ID="labelAds" runat="server"></asp:Label> <!--[<asp:Label ID="labelActiveAds" runat="server"></asp:Label>] --><a href="#">Total<!-- [Active]--></a></li>
                                        <li><asp:Label ID="labelNewAds" runat="server"></asp:Label> <a href="#">New *</a></li>
                                        <li><asp:Label ID="labelAvgBid" runat="server"></asp:Label> <a href="#">Avg. BID</a></li>
                                    </ul>
                                </td>
                                <td class="Seperator"></td>
                                <td>
                                    <span class="OverviewTitle">Developers</span>
                                    <ul>
                                        <li><asp:Label ID="labelDevelopers" runat="server"></asp:Label> <!--[<asp:Label ID="labelActiveDevelopers" runat="server"></asp:Label>] --><a href="ManageDevelopers.aspx">Total<!-- [Active]--></a></li>
                                        <li><asp:Label ID="labelNewDevelopers" runat="server"></asp:Label> <a href="#">New *</a></li>
                                    </ul>
                                    <span class="OverviewTitle">Site Owners</span>
                                    <ul>
                                        <li><asp:Label ID="labelPublishers" runat="server"> </asp:Label> <!--[<asp:Label ID="labelActivePublishers" runat="server"></asp:Label>] --><a href="ManagePublishers.aspx">Total<!-- [Active]--></a></li>
                                        <li><asp:Label ID="labelNewPublishers" runat="server"></asp:Label> <a href="#">New *</a></li>
                                    </ul>                                    
                                    <span class="OverviewTitle">Websites</span>
                                    <ul>
                                        <li><asp:Label ID="labelWebsites" runat="server"></asp:Label> <!--[<asp:Label ID="labelActiveWebsites" runat="server"></asp:Label>] --><a href="#">Total<!-- [Active]--></a></li>
                                        <li><asp:Label ID="labelNewWebsites" runat="server"></asp:Label> <a href="#">New *</a></li>
                                        <li><asp:Label ID="labelWebsiteAvgValue" runat="server"></asp:Label> <a href="#">Avg. Value</a></li>
                                    </ul>                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

            <div class="DashboardSection">
                <div id="LeftSection">
                    <div class="DashboardBox">
                        <div class="DashboardBoxHeader">
                            <span>Alerts</span>
                        </div>
                        <div class="DashboardBoxContent Measures">
                            <ul>
                                <li><asp:Label ID="labelErrors" runat="server"></asp:Label> <a href="#">Service Errors *</a></li>
                                <li><asp:Label ID="labelWarnings" runat="server"></asp:Label> <a href="#">Warnings *</a></li>
                            </ul>                
                        </div>
                    </div>                    
                </div>
                
                <div id="RightSection">
                    <div class="DashboardBox">
                        <div class="DashboardBoxHeader">
                            <span>To Do List</span>
                        </div>
                        <div class="DashboardBoxContent Measures">
                            <ul>
                                <li><asp:Label ID="labelPendingWebsites" runat="server"></asp:Label> <a href="PendingWebsites.aspx">Pending Websites</a></li>
                                <li><asp:Label ID="labelPublishersToBePaid" runat="server"></asp:Label> <a href="PublishersToBePaid.aspx">Site Owners To Be Paid</a></li>
                                <li><asp:Label ID="labelDevelopersToBePaid" runat="server"></asp:Label> <a href="DevelopersToBePaid.aspx">Developers To Be Paid</a></li>
                                <li><asp:Label ID="labelNewRequests" runat="server"></asp:Label> <a href="CRM_Requests.aspx">New Mails</a></li>
                            </ul>
                        </div>
                    </div>
                </div>                
            </div>
                        
            </ContentTemplate>
        </asp:UpdatePanel>
        
        </div>
    </div>
</asp:content>