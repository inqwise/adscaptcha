<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Advertiser/AdvertiserAccount.Master" AutoEventWireup="true" CodeFile="EditCampaign.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.EditCampaign" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<%@ Register assembly="ComponentArt.Web.UI" namespace="ComponentArt.Web.UI" tagprefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
   
   .ButtonHolder
   {
   	margin-top:30px;
   }
   
   .ButtonHolder a
   {
   	color:#FFFFFF;
   }
   
   .container {
    /*background: -moz-radial-gradient(center center , ellipse farthest-corner, #FFFFFF 0%, #EAEAEA 100%) repeat scroll 0 0 transparent;
    border: 2px solid #FFFFFF;
    box-shadow: 0 5px 22px #BBBBBB;
    height: 370px;
    margin: 20px auto;
    overflow: hidden;
    padding: 50px;*/
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
    width: 950px;
}
    .calendar
    {
    	background-color:#fff;
    	border:1px solid Gray;
    	}
   </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Edit Campaign
</asp:Content>
<asp:content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
        var CPT = "Pay-Per-Fit";
        var PPT = "Pay-Per-Click";
        function ChangeBudgetStrings() {
            var i = $('#<%=rbCampaignPaymentTypeClick.ClientID %>')[0].checked ? 1 : 0;
            if (i == 0) $("span.PaymentTypeText").html(CPT);
            else if (i == 1) $("span.PaymentTypeText").html(PPT);
        }

    </script>

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
                <li><a href="ManageCampaigns.aspx">Manage</a></li>
                <li><a href="NewCampaign.aspx">New Campaign</a></li>
            </ul>
        </div>
    </div>
            <div id="breadCrambs">
            <asp:Label ID="labelNavigationPath" runat="server" />        
        </div-->
        <div id="content"  class="container">
        <div class="inner-content">
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
            
            <h4>Campaign Information</h4>
            <div class="description">
                <table>
                    <tr>
                        <td class="FieldHeader">Campaign Name:*</td>
                        <td>
                            <asp:textbox ID="textCampaignName" runat="server" CssClass="InputField" MaxLength="50" Text="" />
                            <asp:RequiredFieldValidator ID="validatorCampaignName1" 
                                runat="server" ControlToValidate="textCampaignName" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            <asp:CustomValidator ID="validatorCampaignName2" 
                                runat="server" ControlToValidate="textCampaignName" Display="Dynamic"
                                OnServerValidate="checkCampaignExist_ServerValidate" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Campaign name already exist" SetFocusOnError="true">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">Status:*</td>
                        <td>
                            <asp:DropDownList ID="listStatus" CssClass="SelectField" runat="server" ValidationGroup="Form" />
                            <asp:Label ID="labelStatus" runat="server" Visible="false" />
                            <asp:RequiredFieldValidator ID="validatorStatus" 
                                runat="server" ControlToValidate="listStatus" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            <br />
                            <asp:Label ID="labelStatusPending" runat="server" CssClass="Explanation" Text="Your campaign is being reviewed by our team."></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
<br />
<hr />
<br />
<br />             
            <h4>Targeting</h4>
            <div class="description">
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
                <!--
                <b>Choose your campaign language:</b>
                <br />
                <asp:RadioButtonList CssClass="RadioField" ID="radioLanguage" runat="server">
                    <asp:ListItem Value="1">Show my ad in all languages</asp:ListItem>
                    <asp:ListItem Value="2">Choose languages</asp:ListItem>
                </asp:RadioButtonList>
                <div id="languageTargeting" runat="server" class="targetingBox" style="display: none;">
                    <asp:CheckBoxList CssClass="CheckBoxField" ID="checkLanguage" runat="server" RepeatColumns="5"></asp:CheckBoxList>
                </div>
                
                <br />-->
                
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
<br />
<hr />
<br />
<br />             
            <h4>Budget and Schedule</h4>
            <div class="description">
            <table>
                           <tr>
                <td colspan="3" style="margin-bottom:5px;margin-top:10px;">
                <div class="Explanation">What are your camping goal?</div>
                <div style="font-size:14px;font-weight:600;">By using Sliding captcha you  can achieve both  traffic and  branding (engagement) goals</div>
                <div style="font-size:13px;font-weight:500;">By selecting Pay per Click you will be charged only for clicks made on your ad,<br />
By selecting Pay per Fit  you will be charged only when a user normalize  your ad correctly - Full engagement
</div>
                </td>
                </tr>
                <tr>
                    
                    <td colspan="3"> <br /> 
                        <b>Campaign payment type:</b>
                <br />
                        <table style="margin:0;padding:0;" cellspacing="10">
                            <tr>
                                <td><asp:RadioButton ID="rbCampaignPaymentTypeClick" onclick="ChangeBudgetStrings();" Checked="true" GroupName="CampaignPAymentType" runat="server" Text="Pay per click" /> </td>
                                </tr><tr>
                                <td><asp:RadioButton ID="rbCampaignPaymentTypeFit" onclick="ChangeBudgetStrings();" GroupName="CampaignPAymentType" runat="server" Text="Pay per engagement" /></td>
                            </tr>
                        </table> 
                    </td>
                </tr>
                 <tr>
                    
                    <td colspan="3"> <br /> <br />
                        <b>Choose to limit your daily budget and your <span class="PaymentTypeText"></span> bid:</b>
                            <br />
                            
                        <div class="fieldName" style="float:left;margin:7px 10px 0 0">Daily Budget (Min. $10):*</div>
                        <div class="fieldValue" style="float:left;">
                            $ <asp:textbox ID="textDailyBudget" runat="server" CssClass="InputField Width40" MaxLength="5" Text="100" /> <img src="../css/Inqwise/images/helpicon.png" class="tooltip" rel="The amount of money per day an advertiser is willing to pay for his CAPTCHAs to be clicked/fitted." />
                        </div>
                        <div class="fieldValidation">
                            <asp:RequiredFieldValidator ID="validatorDailyBudget1"
                                runat="server" ControlToValidate="textDailyBudget" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            <asp:RangeValidator ID="validatorDailyBudget2"
                                runat="server" ControlToValidate="textDailyBudget" ValidationGroup="Form"
                                MaximumValue="50000" MinimumValue="10" Type="Double"
                                CssClass="ValidationMessage" ErrorMessage="* Daily budget range is $10-$50K" Display="dynamic" SetFocusOnError="true">
                            </asp:RangeValidator>
                            <AjaxControlToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="textDailyBudget" ValidChars=".0123456789" />
                        </div>
                    </td>
                </tr>
                 <tr>
                    
                    <td colspan="3"> <br /> <br />
                        <p><b>What is your payment limit per <span class="PaymentTypeText"></span>? (Min. $0.01)</b></p>
                        <div class="fieldName"  style="float:left;margin:9px 10px 0 0">Max. <span class="PaymentTypeText"></span> Bid:*</div>
                        <div class="fieldValue">
                            $ <asp:textbox ID="textMaxPpt" runat="server" CssClass="InputField Width40" MaxLength="6" Text="0.01" /> <img src="../css/Inqwise/images/helpicon.png" class="tooltip" rel="This defines what an advertiser will pay when a user successfully clicks/fits the ad." />
                        </div>
                        <div class="fieldValidation">
                            <asp:RequiredFieldValidator ID="validatorMaxPpt1"
                                runat="server" ControlToValidate="textMaxPpt" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            <asp:RangeValidator ID="validatorMaxPpt2"
                                runat="server" ControlToValidate="textMaxPpt" ValidationGroup="Form"
                                MaximumValue="50000" MinimumValue="0.01" Type="Double"
                                CssClass="ValidationMessage" ErrorMessage="* Bid range is $0.01-$50K" Display="dynamic" SetFocusOnError="true">
                            </asp:RangeValidator>
                            <AjaxControlToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="textMaxPpt" ValidChars=".0123456789" />
                        </div>
                    </td>
                 </tr>

                <tr class="campaignTableR2" class="optionsBasic">

                    <td colspan="3"> <br /> <br />
                        <p><b>When do you wish to start running your ad?</b></p>
                        <div class="fieldNameNarrow"  style="float:left;margin:13px 10px 0 0;">Schedule:*</div>
                        <div class="fieldValue"  style="float:left;white-space:nowrap;margin:4px">
                            <asp:RadioButtonList CssClass="RadioField" ID="radioSchedule" Width="180" runat="server" RepeatDirection="Horizontal"  CellSpacing="10" CellPadding="3">
                                <asp:ListItem Value="1">Now</asp:ListItem>
                                <asp:ListItem Value="2">Specific dates</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div id="scheduleDates" runat="server" style="clear:both;">
                            <div class="fieldNameNarrow">From</div>
                            <div class="fieldValue">
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
                            </div>
                            <div class="clearfix"></div>
                            <div class="fieldNameNarrow">To</div>
                            <div class="fieldValue">
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
                            </div>
                            
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
                        </div>
                    </td>
                </tr>
                <tr class="campaignTableR1" class="optionsAgencies" style="display:none;">
                    <td class="campaignTableR1C1">
                        Choose to limit your budget or the number of clicks/fits:
                    </td>
                    <td class="campaignTableR1C2">
                        Choose your campaign's length (days):
                    </td>
                    <td class="campaignTableR1C3">
                        Place your campaign!
                    </td>
                </tr>
                <tr class="campaignTableR2" class="optionsAgencies" style="display:none;">
                    <td class="campaignTableR2C1">
                        <!-- budget -->
                        <div class="fieldName">
                            <input type="radio" name="radioPreDefinedType" id="radioPreDefinedType_Budget" value="1" checked="checked" onclick="javascript:changePreDefinedType();" />
                            <label for="radioPreDefinedType_Budget">Pre defined budget limit ($)</label>
                        </div>
                        <div class="fieldValue">
                            <input id="textBudget" type="text" class="InputField Width60" onkeyup="javascript:calcEstimate();" maxlength="7" value="" />
                            <img src="../css/Inqwise/images/helpicon.png" class="tooltip" rel="Define in advance your campaign's BUDGET limit" />
                            <br />
                            <span class="Explanation">(Min. $10)</span>
                        </div>
                        <!-- bulk -->
                        <div class="fieldName">
                            <input type="radio" name="radioPreDefinedType" id="radioPreDefinedType_Bulk" value="2" onclick="javascript:changePreDefinedType();" />
                            <label for="radioPreDefinedType_Bulk">Pre defined number of user clicks/fits</label>
                        </div>
                        <div class="fieldValue">
                            <input id="textBulkCPT" type="text" class="InputField Width60 disabled" maxlength="12" onkeyup+="javascript:calcEstimate();" value="" />
                            <img src="../css/Inqwise/images/helpicon.png" class="tooltip" rel="Define in advance the number of times users will TYPE your text args" />
                        </div>
                    </td>
                    <td class="campaignTableR2C2">
                        <div id="slider"></div>
                        <span class="Explanation">You can shrink/expand your campaign's length and see how it effects the estimated clicks/cost.</span>
                        <p><b>Choose when to start running your campaign:</b></p>
                        <div class="fieldNameNarrow">Schedule:*</div>
                        <div class="fieldValue">
                            <input type="radio" name="schedule" value="1" class="RadioField" checked="checked">Now
                            <input type="radio" name="schedule" value="2" class="RadioField">Specific dates
                        </div>
                    </td>
                    <td class="campaignTableR2C3">
                            <div id="estimatedHolder">
                                <table width="100%">
                                    <tr>
                                        <td class="estimateDesc">campaign's length (days)</td>
                                        <td class="estimateValue"><span id="days">9</span></td>
                                    </tr>
                                    <tr id="estimatedCPTHolder" class="CPT">
                                        <td class="estimateDesc">Estimated types <img src="../css/Inqwise/images/helpicon.png" class="tooltip" rel="The number of times users TYPE your text args" /><input id="estimatedHiddenCPT" type="hidden"></input></td>
                                        <td class="estimateValue"><span id="estimatedCPT">?</span></td>
                                    </tr>
                                    <tr id="estimatedChargeHolder" style="display:none;">
                                        <td class="estimateDesc">Estimated cost<input id="estimatedHiddenCharge" type="hidden"></input></td>
                                        <td class="estimateValue">$<span id="estimatedCharge"></span></td>
                                    </tr>
                                </table>
                            </div>
                    </td>
                </tr>
                <tr class="campaignTableR3">
                    <td colspan="3">
                        <asp:LinkButton id="buttonSubmit" runat="server" CssClass="btn" ForeColor="White" OnClick="buttonSubmit_Click" CausesValidation="true" ValidationGroup="Form"><span>Submit</span></asp:LinkButton>
                        <a id="buttonCancel" class="btn" style="color:White;" href="ManageCampaigns.aspx"><span>Cancel</span></a>
                    </td>
                </tr>
            </table>
            </div>
             <script type="text/javascript">                 ChangeBudgetStrings();</script>
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </div>
</asp:content>