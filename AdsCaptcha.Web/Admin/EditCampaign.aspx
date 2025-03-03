<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="EditCampaign.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.EditCampaign" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<%@ Register assembly="ComponentArt.Web.UI" namespace="ComponentArt.Web.UI" tagprefix="ComponentArt" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_3').addClass('selected');
        });
    </script>    
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li><a href="ReportAdvertisers.aspx">Report</a></li>
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

        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>            
            <div>
                <h2>Campaign's Information</h2>
                <div class="Section">
                    <table>
                        <tr>
                            <td class="FieldHeader">Campaign Name:*</td>
                            <td>
                                <asp:textbox ID="textCampaignName" runat="server" CssClass="InputField" MaxLength="50" Text="" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="textCampaignName" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" ID="textCampaignNameRFValidator" />
                                <asp:CustomValidator runat="server" ControlToValidate="textCampaignName" Display="Dynamic"
                                    OnServerValidate="checkCampaignExist_ServerValidate" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Campaign name already exist" SetFocusOnError="true" ID="textCampaignNameCValidator">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Status:*</td>
                            <td>
                                <asp:DropDownList ID="listStatus" CssClass="SelectField" runat="server" ValidationGroup="Form" DataValueField="Item_Id" DataTextField="Item_Desc" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="listStatus" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" ID="listStatusRfValidator" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Bonus Campaign?</td>
                            <td>
                                <asp:CheckBox ID="checkBonusCampaign" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                
                <h2>Targeting</h2>
                <div class="Section">
                    <b>Choose a geographic location for your ads to appear:</b>
                    <br />
                    <asp:RadioButtonList CssClass="RadioField" ID="radioCountry" runat="server">
                        <asp:ListItem Value="1">Show my ad anywhere in the world</asp:ListItem>
                        <asp:ListItem Value="2">Choose countries</asp:ListItem>
                    </asp:RadioButtonList>            
                    <div id="countryTargeting" runat="server" class="targetingBox" style="display: none;">
                        <asp:CheckBoxList CssClass="CheckBoxField" ID="checkCountry" runat="server" RepeatColumns="6"></asp:CheckBoxList>
                    </div>

                    <br />
                    <br />
                    
                    <b>Choose your campaign language:</b>
                    <br />
                    <asp:RadioButtonList CssClass="RadioField" ID="radioLanguage" runat="server">
                        <asp:ListItem Value="1">Show my ad in all languages</asp:ListItem>
                        <asp:ListItem Value="2">Choose languages</asp:ListItem>
                    </asp:RadioButtonList>
                    <div id="languageTargeting" runat="server" class="targetingBox" style="display: none;">
                        <asp:CheckBoxList CssClass="CheckBoxField" ID="checkLanguage" runat="server" RepeatColumns="5"></asp:CheckBoxList>
                    </div>
                    
                    <br />
                    <br />
                    
                    <b>Choose your campaign category:</b>
                    <br />
                    <asp:RadioButtonList CssClass="RadioField" ID="radioCategory" runat="server">
                        <asp:ListItem Value="1">Show my ad in all categories</asp:ListItem>
                        <asp:ListItem Value="2">Choose categories</asp:ListItem>
                    </asp:RadioButtonList>
                    <div id="categoryTargeting" runat="server" class="targetingBox">
                        <asp:CheckBoxList CssClass="CheckBoxField" ID="checkCategory" runat="server" RepeatColumns="3"></asp:CheckBoxList>
                    </div>
                            
                    <br />
                    <div style="display:none;">
                    
                    <b>Choose keywords that best relate to your product or business:</b>
                    <br />
                    <asp:RadioButtonList CssClass="RadioField" ID="radioKeywords" runat="server">
                        <asp:ListItem Value="1">Do not target by keywords</asp:ListItem>
                        <asp:ListItem Value="2">Enter keywords</asp:ListItem>
                    </asp:RadioButtonList>
                    <div id="keywordsTargeting" runat="server" class="targetingBox">
                        <asp:TextBox ID="textKeywords" runat="server" CssClass="TextareaField" TextMode="MultiLine" ></asp:TextBox>
                        <asp:CustomValidator ID="validatorKeywords"
                            runat="server" ControlToValidate="textKeywords" Display="Dynamic"
                            OnServerValidate="checkKeywordsLength_ServerValidate" ValidationGroup="Form"
                            CssClass="ValidationMessage" ErrorMessage="* One of your keywords exceeds 50 chars" SetFocusOnError="true" />
                        <br />
                        <span class="Explanation">Example: music, movies, sports</span>
                    </div>
                    </div>
                </div>
                
                <h2>Budget and Schedule</h2>
                <div class="Section">
                    <table>
                    <tr>
                    <td >
                        <table style="margin:30px 0 30px 0;">
                            <tr>
                                <td  style="font-weight:bold;">Campaign payment type:</td>
                                <td><asp:RadioButton ID="rbCampaignPaymentTypeClick" Checked="true" GroupName="CampaignPAymentType" runat="server" Text="Pay per click" /> </td>
                                <td><asp:RadioButton ID="rbCampaignPaymentTypeFit" GroupName="CampaignPAymentType" runat="server" Text="Pay per engagement" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                    </table>
                    <b>What is your daily payment limit? (Min. $10)</b>
                    <table>
                        <tr>
                            <td class="FieldHeader">Daily Budget:*</td>
                            <td>
                                $ <asp:textbox ID="textDailyBudget" runat="server" CssClass="InputField Width40" MaxLength="5" Text="100" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="textDailyBudget" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" ID="textDailyBudgetRFValidator" />
                                <asp:RangeValidator ID="textDailyBudgetRValidator" runat="server" ControlToValidate="textDailyBudget" ValidationGroup="Form"
                                    MaximumValue="50000" MinimumValue="10" Type="Double"
                                    CssClass="ValidationMessage" ErrorMessage="* Daily budget range is $10-$50K" Display="dynamic" SetFocusOnError="true">
                                </asp:RangeValidator>
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textDailyBudget" ValidChars=".0123456789" />
                            </td>
                        </tr>
                    </table>
                    
                    <b>When do you wish to start running your ad?</b>
                    <table>
                        <tr>
                            <td class="FieldHeader">Schedule:*</td>
                            <td>                    
                                <asp:RadioButtonList CssClass="RadioField" ID="radioSchedule" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Now</asp:ListItem>
                                    <asp:ListItem Value="2">Specific dates</asp:ListItem>
                                </asp:RadioButtonList>

                                <div id="scheduleDates" runat="server">
                                From
                                <ComponentArt:Calendar 
                                    ID="PickerFrom"
                                    runat="server"
                                    PickerFormat="Custom"
                                    PickerCustomFormat="MMMM d yyyy"
                                    ControlType="Picker"
                                    PickerCssClass="picker"
                                    ClientIDMode="AutoID">
                                    <ClientEvents>
                                        <SelectionChanged EventHandler="PickerFrom_OnDateChange" />
                                    </ClientEvents>
                                </ComponentArt:Calendar>
                                <img id="calendar_from_button" alt="From Date" class="calendar_button" src="images/Calendar.jpg" 
                                    onclick="ButtonFrom_OnClick(event)" onmouseup="ButtonFrom_OnMouseUp(event)" />
                                To
                                <ComponentArt:Calendar 
                                    ID="PickerTo"
                                    runat="server"                        
                                    PickerFormat="Custom"
                                    PickerCustomFormat="MMMM d yyyy"
                                    ControlType="Picker"
                                    PickerCssClass="picker"
                                    ClientIDMode="AutoID">
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
                                    ClientIDMode="AutoID">
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
                                    ImagesBaseUrl="images"
                                    PrevImageUrl="CalendarPrevMonth.gif"
                                    NextImageUrl="CalendarNextMonth.gif"
                                    ClientIDMode="AutoID">
                                    <ClientEvents>
                                        <SelectionChanged EventHandler="CalendarTo_OnChange" />
                                    </ClientEvents>
                                </ComponentArt:Calendar>
                                </div>                                               
                            </td>
                        </tr>
                    </table>
                </div>
                    
                <div id="buttonHolder">
                    <asp:ImageButton ID="buttonSubmit" runat="server" ImageUrl="images/submit.gif"
                        CausesValidation="true" ValidationGroup="Form" onclick="buttonSubmit_Click" />
                    <a id="buttonCancel" class="button" href="ManageCampaigns.aspx?AdvertiserId=<%=Request.QueryString["AdvertiserId"]%>"><span>Cancel</span></a>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
        </div> 
    </div>

</asp:content>