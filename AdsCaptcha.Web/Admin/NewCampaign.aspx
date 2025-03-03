<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="NewCampaign.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.NewCampaign" %>
<%@ Import Namespace="Inqwise.AdsCaptcha.SystemFramework" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<%@ Register assembly="ComponentArt.Web.UI" namespace="ComponentArt.Web.UI" tagprefix="ComponentArt" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript">
        var ADSCAPTCHA_URL = '<%=ConfigurationSettings.AppSettings["URL"]%>';
        var ADSCAPTCHA_API = '<%=ConfigurationSettings.AppSettings["API"]%>';
        var ADSCAPTCHA_CDN = '<%=ConfigurationSettings.AppSettings["AWSCloudFront"]%>';
    </script>

    <link href="<%=ConfigurationSettings.AppSettings["API"]%>Widget.css" type="text/css" rel="stylesheet" />
    <script src="<%=ConfigurationSettings.AppSettings["API"]%>Widget.js" type="text/javascript"></script>
  
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_3').addClass('selected');
        });

        previewSlogan = 'Your Message';

        $(document).ready(function() {
            var selectedIndex = document.getElementById(MASTER_PAGE_PREFIX + 'listAdType').selectedIndex;
            var selectedValue = document.getElementById(MASTER_PAGE_PREFIX + 'listAdType').options[selectedIndex].value;
            _AdType = selectedValue;

            var objSlogan = $('#<% =textAdSlogan.UniqueID.Replace("$","_") %>');

            $(".cb-enable").addClass('selected');
            previewDir = 'ltr';
            objSlogan.css('direction', previewDir);

            $(".cb-enable").click(function() {
                var parent = $(this).parents('.switch');
                $('.cb-disable', parent).removeClass('selected');
                $(this).addClass('selected');
                $(':checkbox', parent).attr('checked', false);
                previewDir = 'ltr';
                objSlogan.css('direction', previewDir);
                UpdateTextMessagePreview();
            });
            $(".cb-disable").click(function() {
                var parent = $(this).parents('.switch');
                $('.cb-enable', parent).removeClass('selected');
                $(this).addClass('selected');
                $(':checkbox', parent).attr('checked', true);
                previewDir = 'rtl';
                objSlogan.css('direction', previewDir);
                UpdateTextMessagePreview();
            });

            objSlogan
                .change(function() {
                    UpdateTextMessagePreview();
                })
                .blur(function() {
                    UpdateTextMessagePreview();
                })
                .keyup(function() {
                    UpdateTextMessagePreview();
                });

            var objPreview = $('#<% =UploadPreview.UniqueID.Replace("$","_") %>');
            objPreview
                .load(function() {
                    var PREVIEW_WIDTH = 250;
                    var ratio = 1;
                    var width = $('#<% =UploadWidth.UniqueID.Replace("$","_") %>').val();
                    var height = $('#<% =UploadHeight.UniqueID.Replace("$","_") %>').val();

                    if (width > PREVIEW_WIDTH) {
                        ratio = width / PREVIEW_WIDTH;
                        height = height / ratio;
                    } else {
                        ratio = PREVIEW_WIDTH / width;
                        width = height * ratio;
                    }
                    width = PREVIEW_WIDTH;
                    height = Math.round(height);

                    var url = $('#<% =UploadPreview.UniqueID.Replace("$","_") %>').attr("src");
                    ChangeAd(url, width, height);
                });
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
                                <asp:RequiredFieldValidator ID="rfvTextCampaignName" runat="server" ControlToValidate="textCampaignName" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:CustomValidator ID="cvTextCampaignName" runat="server" ControlToValidate="textCampaignName" Display="Dynamic"
                                    OnServerValidate="checkCampaignExist_ServerValidate" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Campaign name already exist" SetFocusOnError="true">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Bonus Campaign?</td>
                            <td>
                                <asp:CheckBox ID="checkBonusCampaign" runat="server" Checked="false" />
                            </td>
                        </tr>
                    </table>
                </div>
                
                <h2>Create Your Ad</h2>
                <div class="Section">
                    <table width="100%">
                        <tr valign="top">
                            <td width="60%" class="FieldsSet">
                                <table cellpadding="0" cellspacing="0">
                                    <tr class="FrameTop"><td></td></tr>
                                    <tr>
                                        <td>
                                            <div class="FrameMiddle">
                                            <table> 
                                                <tr>
                                                    <td class="FieldFrameHeader">Ad Name:*</td>
                                                    <td>
                                                        <asp:textbox ID="textAdName" runat="server" CssClass="InputFrameField" MaxLength="50" />
                                                        <asp:RequiredFieldValidator ID="validatorAdName"
                                                            runat="server" ControlToValidate="textAdName" ValidationGroup="Form"
                                                            CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                                    </td>
                                                </tr>
                                                <tr valign="top" >
                                                    <td class="FieldFrameHeader">Ad Type:*</td>
                                                    <td>                                    
                                                        <asp:DropDownList ID="listAdType" runat="server" CssClass="SelectFrameField" DataTextField="Item_Desc" DataValueField="Item_Id" />
                                                        <asp:RequiredFieldValidator ID="validatorAdType"
                                                            runat="server" ControlToValidate="listAdType" ValidationGroup="Form"
                                                            CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                                    </td>
                                                </tr>            
                                                <tr id="TextMessageHolder" runat="server">
                                                    <td class="FieldFrameHeader">Text Message:*</td>
                                                    <td>
                                                        <asp:textbox ID="textAdSlogan" runat="server" CssClass="InputFrameField" />
                                                        <span class="switch">
                                                            <asp:CheckBox ID="checkRtl" runat="server" />
                                                            <label class="cb-enable"><span>L</span></label>
                                                            <label class="cb-disable"><span>R</span></label>
                                                        </span>
                                                        <asp:CustomValidator ID="validatorAdSlogan"
                                                            runat="server" ControlToValidate="textAdSlogan" Display="Dynamic"
                                                            OnServerValidate="checkTextMessage_ServerValidate" ValidationGroup="Form"
                                                            CssClass="ValidationMessage" ErrorMessage="* Required"
                                                            SetFocusOnError="true" ValidateEmptyText="true" />
                                                        <br />
                                                        <span class="Explanation">Text limited to <% =ApplicationConfiguration.MAX_SLOGAN_LENGTH.ToString() %> characters</span>
                                                        <AjaxControlToolkit:FilteredTextBoxExtender ID="filterAdSlogan" runat="server" TargetControlID="textAdSlogan" FilterType="Custom" FilterMode="InvalidChars" InvalidChars=";`\|/" />
                                                    </td>
                                                </tr>
                                                <tr id="AdTypeImageHolder" runat="server" style="display:none;">
                                                    <td class="FieldFrameHeader">Image:*</td>
                                                    <td>
                                                        <input id="UploadHidden" runat="server" type="hidden" value="" />
                                                        <div id="UploadImageHolder">
                                                            <iframe id="UploadImageFrame" onload="initPhotoUpload()" src="UploadImage.aspx" scrolling="no" frameborder="0" hidefocus="true" style="text-align:left;vertical-align:top;border-style:none;margin:0px;width:100px;height:24px"></iframe>
                                                        </div>
                                                        <asp:Label ID="labelImageValidation" CssClass="ValidationMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <div id="UploadProgressHolder" style="display:none;">
                                                            <img src="images/Uploading.gif" alt="Uploading..." />
                                                        </div>
                                                        <div id="UploadedImageHolder" runat="server" style="padding-top:2px;padding-bottom:2px;">
                                                            <img id="UploadPreview" runat="server" src="" alt="Preview" onclick="window.open(this.src);" style="cursor:-moz-zoom-in;" />
                                                            <input id="UploadWidth" runat="server" type="hidden" />
                                                            <input id="UploadHeight" runat="server" type="hidden" />
                                                            <input id="AdWidth" runat="server" type="hidden" value="0" />
                                                            <input id="AdHeight" runat="server" type="hidden" value="0" />
                                                        </div>                                                
                                                        <span class="Explanation">
                                                            Max. file size: <asp:Label ID="labelImageFileSize" runat="server" /> KB
                                                            <br />
                                                            Allowed image types: <asp:Label ID="labelImageFormat" runat="server" />
                                                        <br />
                                                        <!--b>Image</b>: 180x150 | 200x200 | 250x250 | 300x250 | 300x100 | 234x60 | 234x60
                                                        <br /-->
                                                        <b>Slide to Fit (W x H)</b>: 180x150 | 300x250 <!--200x200 | 250x250 | 300x300-->
                                                    </span>
                                                    </td>
                                                </tr>
                                                <tr id="AdTypeClickURLHolder" runat="server" style="display:none;">
                                                    <td class="FieldFrameHeader">Image Click URL:</td>
                                                    <td>
                                                        <asp:textbox ID="txtImageClickURL" runat="server" CssClass="InputFrameField" />
                                                        <a href="javascript:CheckLinkURL();">Check Link</a>
                                                    </td>
                                                </tr>
                                                <tr id="AdTypeClickLikeHolder" runat="server">
                                                <td class="FieldFrameHeader" style="white-space:nowrap;">Facebook Like URL:</td>
                                                <td>
                                                    <asp:textbox ID="txtLikeURL" runat="server" CssClass="InputFrameField" />
                                                    <a href="javascript:CheckLikeURL('<%=txtLikeURL.ClientID %>');">Check Like link</a>
                                                </td>
                                            </tr>
                                                <tr id="AdTypeVideoHolder" runat="server">
                                                    <td class="FieldFrameHeader">Video:*</td>
                                                    <td>
                                                        <asp:FileUpload id="uploadVideo" CssClass="InputFrameField" runat="server" />
                                                        <br />
                                                        <span class="Explanation">
                                                            Max. file size <asp:Label ID="labelVideoFileSize" runat="server" /> kb, <asp:Label ID="labelVideoFormat" runat="server" /><!--, <asp:Label ID="labelVideoDimensions" runat="server" />-->
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr id="AdTypeCouponHolder" runat="server">
                                                    <td class="FieldFrameHeader">Url:*</td>
                                                    <td>
                                                        <asp:textbox ID="textLinkUrl" runat="server" CssClass="InputFrameField" MaxLength="512" />
                                                    </td>
                                                </tr>
                                            </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="FrameBottom"><td></td></tr>
                                </table>
                            </td>
                            
                            <td class="arrow" align="right"></td>
                            
                            <td width="30%" class="previewCaptcha">
                                <div id="AdTypeHolder0" runat="server" class="previewCaptchaHolder"> <!-- slogan only -->
                                    <script type="text/javascript">
                                        var type = 2;
                                        previewURL = null;

                                        GetCaptcha(type, previewSlogan, previewURL, previewWidth, previewHeight, previewDir, previewThemeId);
                                    </script>
                                </div>
                                <div id="AdTypeHolder1" runat="server" class="previewCaptchaHolder"> <!-- image -->
                                    <script type="text/javascript">
                                        var type = 3;
                                        previewURL = ADSCAPTCHA_URL + 'images/Preview/GeneratorImage.jpg';

                                        GetCaptcha(type, previewSlogan, previewURL, previewWidth, previewHeight, previewDir, previewThemeId);
                                    </script>
                                </div>
                                <div id="AdTypeHolder2" runat="server" class="previewCaptchaHolder"> <!-- slider -->
                                    <script type="text/javascript">
                                        previewURL = ADSCAPTCHA_URL + 'Images/Preview/Slider.jpg';

                                        GetSlider('FlashSlider', previewURL, previewWidth, previewHeight, previewThemeId);
                                    </script>
                                </div>
                                <div id="AdTypeHolder3" runat="server" class="previewCaptchaHolder"> <!-- video -->
                                    <script type="text/javascript">
                                        var type = 4;
                                        previewURL = ADSCAPTCHA_URL + 'images/Preview/CaptchaVideoFox.swf';

                                        GetCaptcha(type, previewSlogan, previewURL, previewWidth, previewHeight, previewDir, previewThemeId);
                                    </script>
                                </div>
                                <div id="AdTypeHolder4" runat="server" class="previewCaptchaHolder"> <!-- image click -->
                                    <script type="text/javascript">
                                        var type = 3;
                                        previewURL = ADSCAPTCHA_URL + 'images/Preview/GeneratorImageClick.jpg';

                                        GetCaptcha(type, previewSlogan, previewURL, previewWidth, previewHeight, previewDir, previewThemeId);
                                    </script>
                                </div>
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
                <b>What is your campaign payment type?</b>
                        <table style="margin:30px 0 30px 0;">
                            <tr>
                                <td  style="font-weight:bold;">Campaign payment type:</td>
                                <td><asp:RadioButton ID="rbCampaignPaymentTypeClick" Checked="true" GroupName="CampaignPAymentType" runat="server" Text="Pay per click" /> </td>
                                <td><asp:RadioButton ID="rbCampaignPaymentTypeFit" GroupName="CampaignPAymentType" runat="server" Text="Pay per engagement" /></td>
                            </tr>
                        </table>
                </div>
                <div class="Section">
                    <b>What is your daily payment limit? (Min. $10)</b>
                    <table>
                        <tr>
                            <td class="FieldHeader">Daily Budget:*</td>
                            <td>
                                $ <asp:textbox ID="textDailyBudget" runat="server" CssClass="InputField Width40" MaxLength="5" Text="100" />
                                <asp:RequiredFieldValidator ID="rfvTextDailyBudget" runat="server" ControlToValidate="textDailyBudget" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:RangeValidator ID="rvTextDailyBudget" runat="server" ControlToValidate="textDailyBudget" ValidationGroup="Form"
                                    MaximumValue="50000" MinimumValue="10" Type="Double"
                                    CssClass="ValidationMessage" ErrorMessage="* Daily budget range is $10-$50K" Display="dynamic" SetFocusOnError="true">
                                </asp:RangeValidator>
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textDailyBudget" ValidChars=".0123456789" />
                            </td>
                        </tr>
                    </table>
                    
                    <br />

                    <b>What is your payment limit per CPT/CPE?</b>
                    <table>
                        <tr>
                            <td class="FieldHeader">Max. Bid:*</td>
                            <td>
                                $ <asp:textbox ID="textMaxPpt" runat="server" CssClass="InputField Width40" MaxLength="6" Text="0.05" />
                                <asp:RequiredFieldValidator ID="validatorMaxPpt1"
                                    runat="server" ControlToValidate="textMaxPpt" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:RangeValidator ID="validatorMaxPpt2"
                                    runat="server" ControlToValidate="textMaxPpt" ValidationGroup="Form"
                                    MaximumValue="50000" MinimumValue="0" Type="Double"
                                    CssClass="ValidationMessage" ErrorMessage="* Bid range is $0-$50K" Display="dynamic" SetFocusOnError="true">
                                </asp:RangeValidator>
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textMaxPpt" ValidChars=".0123456789" />
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
                                    PickerCssClass="picker">
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
                                    ImagesBaseUrl="Images"
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
                                    ImagesBaseUrl="Images"
                                    PrevImageUrl="CalendarPrevMonth.gif"
                                    NextImageUrl="CalendarNextMonth.gif">
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

</asp:content>