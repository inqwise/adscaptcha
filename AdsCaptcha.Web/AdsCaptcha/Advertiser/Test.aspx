<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Advertiser.Master" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.Test" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<%@ Register assembly="ComponentArt.Web.UI" namespace="ComponentArt.Web.UI" tagprefix="ComponentArt" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        var ADSCAPTCHA_URL = '<%=ConfigurationSettings.AppSettings["URL"]%>';
        var ADSCAPTCHA_API = '<%=ConfigurationSettings.AppSettings["API"]%>';
        var ADSCAPTCHA_CDN = '<%=ConfigurationSettings.AppSettings["AWSCloudFront"]%>';
    </script>

    <link href="<%=ConfigurationSettings.AppSettings["API"]%>Widget.css" type="text/css" rel="stylesheet" />
    <script src="<%=ConfigurationSettings.AppSettings["API"]%>Widget.js" type="text/javascript"></script>
    <script src="../js/jquery.tinysort.min.js" type="text/javascript"></script>
  
    <script type="text/javascript" charset="utf-8">
        var days = 9;

        function clickUrlChange() {
            var url = $('#textUrl').val();
            var click = document.getElementById('checkClickUrl');

            click.href = url;

            if (!url || url == '') {
                $('.CPC').hide();
                $('#checkClickUrl').hide();
            } else {
                $('.CPC').show();
                
                if (url.toLowerCase().indexOf('http') == 0)
                    $('#checkClickUrl').show();
                else
                    $('#checkClickUrl').hide();
            }
        }

        function calcEstimate() {
            var CPT = 0;
            var cost = 0;

            var radioPreDefinedType = $("input[name='radioPreDefinedType']:checked").val();

            if (radioPreDefinedType == 1) { // Budget
                var budget = $('#textBudget').val();
                if (budget == 'undefined' || budget <= 0 || budget == '') {
                    CPT = 0;
                } else {
                    CPT = budget * 2.5;
                }

                CPT = Math.round(days * CPT);
                $("#estimatedHiddenCPT").val(CPT);
                $("#estimatedCPT").text(CPT);
                $('#textBulkCPT').val(CPT);

            } else if (radioPreDefinedType == 2) { // Bulk
                var bulkCPT = $('#textBulkCPT').val();
                if (bulkCPT == 'undefined' || bulkCPT <= 0 || bulkCPT == '') {
                    cost = 0;
                } else {
                    cost = bulkCPT * 0.01;
                }

                cost = Math.round((90 - days + 10) * cost);
                $("#estimatedHiddenCharge").val(cost);
                $("#estimatedCharge").text(cost);
                $('#textBudget').val(cost);
            }
        }
        
        function changePreDefinedType() {
            var selectedPreDefinedType = $("input[name='radioPreDefinedType']:checked").val();
            
            if (selectedPreDefinedType == 1) { // Budget
                $('#PreDefinedBudgetHolder').show();
                $('#PreDefinedBulkHolder').hide();
                $('#textBudget').attr('disabled', '');
                $('#textBudget').removeClass('disabled');
                $('#textBudget').focus();
                $('#textBulkCPT').attr('disabled', 'disabled');
                $('#textBulkCPT').addClass('disabled');
                $('#estimatedCPTHolder').show();
                $('#estimatedChargeHolder').hide();
            } else if (selectedPreDefinedType == 2) { // Bulk
                $('#PreDefinedBudgetHolder').hide();
                $('#PreDefinedBulkHolder').show();
                $('#textBudget').attr('disabled', 'disabled');
                $('#textBudget').addClass('disabled');
                $('#textBulkCPT').attr('disabled', '');
                $('#textBulkCPT').removeClass('disabled');
                $('#textBulkCPT').focus();
                $('#estimatedCPTHolder').hide();
                $('#estimatedChargeHolder').show();
            }

            calcEstimate();
        }
        
        function adTypeChange() {
            var selectedIndex = document.getElementById('listAdType').selectedIndex;
            var selectedValue = document.getElementById('listAdType').options[selectedIndex].value;

            for (i = 16001; i <= 16004; i++) {
                if (i == selectedValue) {
                    document.getElementById('AdTypeHolder' + i).style.display = 'block';
                } else {
                    document.getElementById('AdTypeHolder' + i).style.display = 'none';
                }
            }

            var sloganHolder = $('#textAdSlogan');
            var urlHolder = $('#textUrl');       
            var imageHolder = $('#AdTypeImageHolder');

            switch (selectedValue) {
                case '16001': // Slogan Only
                    sloganHolder.attr('disabled', '');
                    sloganHolder.removeClass('disabled');
                    //imageHolder.hide();
                    urlHolder.attr('disabled', 'disabled');
                    urlHolder.addClass('disabled');
                    $('.CPT').show();
                    $('.CPC').hide();
                    break;
                case '16002': // Slogan and Image
                    sloganHolder.attr('disabled', '');
                    sloganHolder.removeClass('disabled');
                    imageHolder.show();
                    urlHolder.attr('disabled', '');
                    urlHolder.removeClass('disabled');
                    $('.CPT').show();
                    $('.CPC').hide();
                    break;
                case '16003': // Dynamic Captcha
                    sloganHolder.attr('disabled', 'disabled');
                    sloganHolder.addClass('disabled');
                    imageHolder.show();
                    urlHolder.attr('disabled', 'disabled');
                    urlHolder.addClass('disabled');
                    $('.CPT').show();
                    $('.CPC').hide();
                    break;
                case '16004': // Image Only
                    sloganHolder.attr('disabled', 'disabled');
                    sloganHolder.addClass('disabled');
                    imageHolder.show();
                    urlHolder.attr('disabled', '');
                    urlHolder.removeClass('disabled');
                    $('.CPT').hide();
                    $('.CPC').show();
                    break;
            }
        }

        previewSlogan = 'Your Message';

        $(document).ready(function() {
            $(".buttonMove").click(function() {
                var arr = $(this).attr("name").split("2");
                var from = MASTER_PAGE_PREFIX + arr[0];
                var to = MASTER_PAGE_PREFIX + arr[1];

                $("#" + from + " option:selected").each(function() {
                    $("#" + to).append($(this).clone());
                    $(this).remove();

                    $("#" + to + " option").tsort("", { attr: "text" })
                    $("#" + from + " option").tsort("", { attr: "text" })
                });
            });

            var objSlogan = $('#textAdSlogan');

            $(".cb-enable").addClass('selected');
            objSlogan.css('direction', 'ltr');

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

            $('input[name="radioPreDefinedType"]').attr('checked', '');
            $('input[name="radioPreDefinedType"]:nth(0)').attr('checked', 'checked');
        });
    </script>

	<style type="text/css">
        .mode {color:#eee;font-size:95%;}
        .mode a, .mode a:hover{color:#eee;}
        .disabled {background-color:#ccc;} 
	    #estimatedDiv {vertical-align:middle;}
	    #estimatedHolder {width:320px;padding:10px;background-color:#e8f8f7;border:solid 2px #1cbbb4;text-align:center;color:#1cbbb4;font-weight:bold;font-size:14px;line-height:150%;text-transform:uppercase;}
	    #PreDefinedBudgetHolder, #PreDefinedBulkHolder {margin:6px 0 0 40px;}
        .estimateValue {width:30%;text-align:center;border-left:solid 3px #fff;padding-left:10px;}
        .estimateDesc {width:70%;text-align:left;}
	    #slider {margin:10px;}
	</style>
	<script type="text/javascript">
	    $(function() {
	        $("#slider").slider({
	            range: "min",
	            value: days,
	            min: 1,
	            max: 12,
	            step: 1,
	            slide: function(event, ui) {
	                days = ui.value;
	                $("#days").text(days);
	                calcEstimate();
	            }
	        });
	    });
	</script>
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">   
    <div class="signupInfo">
        <div class="warp">
            This signup wizard will make it easier for you to create your new ad campaign.
            <br />
            Try it now, no obligation required: Note that your ad will not run until you submit your billing information at the end of the process. You can change your ad or halt your campaign at any given time.    
        </div>
    </div>
    <div class="warp">
        <div id="content-form">
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
            
            <h2>Create Your Ad</h2>
            <div class="Section">
                <table width="100%">
                    <tr valign="top">
                        <td width="60%">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <table> 
                                            <tr>
                                                <td class="FieldHeader"><b>Campaign Name:*</b></td>
                                                <td><input type="text" id="textCampaignName" class="InputFrameField" maxlength="50" /><br /><br /></td>
                                            </tr>
                                            <tr>
                                                <td class="FieldHeader"><b>Ad Name:*</b></td>
                                                <td>
                                                    <input type="text" id="textAdName" class="InputFrameField" maxlength="50" /> <img src="../css/Inqwise/images/helpicon.png" class="tooltip" rel="Give your ad a meaningful name, so you can identify it easily" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="FieldHeader"><b>Ad Type:*</b></td>
                                                <td>                                                                                        
                                                    <select id="listAdType" class="SelectFrameField" onchange="javascript:adTypeChange();"> <img src="../css/Inqwise/images/helpicon.png" class="tooltip" rel="" />
                                                        <option value="16001">Text Message Only</option> 
                                                        <option value="16002" selected="selected">Text Message and Image</option> 
                                                        <!--<option value="16004">Image Only</option>-->
                                                        <option value="16003">Dynamic Captcha</option>
                                                    </select>
                                                </td>
                                            </tr>            
                                            <tr id="AdTypeSloganHolder">
                                                <td class="FieldHeader"><b>Text Message:*</b></td>
                                                <td>
                                                    <input type="text" id="textAdSlogan" class="InputFrameField" maxlength="20" style="float:left;margin-right:3px;" /> 
                                                    <span class="switch" style="float:left;">
                                                        <asp:CheckBox ID="checkRtl" runat="server" />
		                                                <label class="cb-enable"><span>L</span></label>
	                                                    <label class="cb-disable"><span>R</span></label>
                                                    </span>                                                    
                                                    <div class="clearfix"></div>
                                                    <span class="Explanation">Text limited to 20 characters</span>
                                                </td>
                                            </tr>
                                            <tr id="AdTypeUrlHolder">
                                                <td class="FieldHeader"><b>Click url:</b></td>
                                                <td>
                                                    <input type="text" id="textUrl" class="InputFrameField" maxlength="100" onkeyup="javascript:clickUrlChange();" /> <img src="../css/Inqwise/images/helpicon.png" class="tooltip" rel="If you like your ad to be clickabel, please enter your url (http://yourwebsite. If not, just leave the field blank.<br/><b>Note:</b> most site owners don't allow clickable CAPTCHAs. Your ad will apear only on sites who do allow it." />
                                                    <span style="font-size:85%"><a href="" id="checkClickUrl" target="_blank" style="display:none;">Check</a></span>
                                                    <br />
                                                    <span class="Explanation">
                                                        <span style="font-weight:bold;color:Red;">NOTE:</span> most site owners don't allow clickable CAPTCHAs. Your ad will apear only on sites who do allow it.
                                                        <br />
                                                        Leave blank for no click
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr id="AdTypeImageHolder">
                                                <td class="FieldHeader"><b>Image:*</b></td>
                                                <td>
                                                    <input id="UploadHidden" runat="server" type="hidden" value="" />
                                                    <div id="UploadImageHolder">
                                                        <iframe id="UploadImageFrame" onload="initPhotoUpload()" src="UploadImage.aspx" scrolling="no" frameborder="0" hidefocus="true" style="text-align:left;vertical-align:top;border-style:none;margin:0px;width:100px;height:24px"></iframe>
                                                    </div>
                                                    <asp:Label ID="labelImageValidation" CssClass="ValidationMessage" runat="server" ForeColor="Red"></asp:Label>
                                                    <div id="UploadProgressHolder" style="display:none;">
                                                        <img src="images/uploading.gif" alt="Uploading..." />
                                                    </div>
                                                    <div id="UploadedImageHolder" runat="server" style="display:none;padding-top:2px;padding-bottom:2px;">
                                                        <img id="UploadPreview" runat="server" src="" alt="Preview" onclick="window.open(this.src);" style="cursor:-moz-zoom-in;" />
                                                        <input id="UploadWidth" runat="server" type="hidden" />
                                                        <input id="UploadHeight" runat="server" type="hidden" />
                                                        <input id="AdWidth" runat="server" type="hidden" value="0" />
                                                        <input id="AdHeight" runat="server" type="hidden" value="0" />
                                                    </div>
                                                    <span class="Explanation">
                                                        Max. file size: 2 MB
                                                        <br />
                                                        Allowed image types: GIF, JPG
                                                        <br />
                                                        Min. image dimensions: 200x60 pixels
                                                        <br />
                                                        Max. image dimensions: 600x300 pixels
                                                    </span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        
                        <td class="arrow" align="right"></td>
                        
                        <td width="30%" class="previewCaptcha">
                            <div id="AdTypeHolder16001" style="display:none;"> <!-- slogan only -->
                                <script type="text/javascript">
                                    var type = 2;
                                    previewURL = null;

                                    GetCaptcha(type, previewSlogan, previewURL, previewWidth, previewHeight, previewDir, previewThemeId);
                                </script>
                            </div>
                            <div id="AdTypeHolder16002"> <!-- image -->
                                <script type="text/javascript">
                                    var type = 3;
                                    previewURL = ADSCAPTCHA_URL + 'Images/Preview/GeneratorImage.jpg';

                                    GetCaptcha(type, previewSlogan, previewURL, previewWidth, previewHeight, previewDir, previewThemeId);
                                </script>
                            </div>
                            <div id="AdTypeHolder16003" style="display:none;width:250px;text-align:center;"> <!-- slider -->
                                <img  src="../Images/Preview/Preview.jpg" /> 
                            </div>
                            <div id="AdTypeHolder16004" style="display:none;""> <!-- image only -->
                                <script type="text/javascript">
                                    var type = 3;
                                    previewURL = ADSCAPTCHA_URL + 'Images/Preview/GeneratorImage.jpg';

                                    GetCaptcha(type, (Math.round(99999 + Math.random() * 999999)) + '', url, previewWidth, previewHeight, previewDir, previewThemeId);
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
                <div id="countryTargeting" runat="server" class="targetingBox">
                    <table>
                        <tr>
                            <td><asp:ListBox CssClass="multiselectList" id="listAvailableCountries" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            <td valign="middle"><input name="listAvailableCountries2listSelectedCountries" class="buttonMove" value=">" type="button" /><br /><input name="listSelectedCountries2listAvailableCountries" class="buttonMove" value="<" type="button" /></td>
                            <td><asp:ListBox CssClass="multiselectList" id="listSelectedCountries" runat="server" SelectionMode="Multiple"></asp:ListBox></td>
                        </tr>
                    </table>
                </div>

                <br />
                
                <b>Choose your campaign language:</b>
                <br />
                <asp:RadioButtonList CssClass="RadioField" ID="radioLanguage" runat="server">
                    <asp:ListItem Value="1">Show my ad in all languages</asp:ListItem>
                    <asp:ListItem Value="2">Choose languages</asp:ListItem>
                </asp:RadioButtonList>
                <div id="languageTargeting" runat="server" class="targetingBox">
                    <table>
                        <tr>
                            <td><asp:ListBox CssClass="multiselectList" id="listAvailableLanguages" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            <td valign="middle"><input name="listAvailableLanguages2listSelectedLanguages" class="buttonMove" value=">" type="button" /><br /><input name="listSelectedLanguages2listAvailableLanguages" class="buttonMove" value="<" type="button" /></td> 
                            <td><asp:ListBox CssClass="multiselectList" id="listSelectedLanguages" runat="server" SelectionMode="Multiple"></asp:ListBox></td>
                        </tr>
                    </table>
                </div>
                
                <br />
                
                <b>Choose your campaign category:</b>
                <br />
                <asp:RadioButtonList CssClass="RadioField" ID="radioCategory" runat="server">
                    <asp:ListItem Value="1">Show my ad in all categories</asp:ListItem>
                    <asp:ListItem Value="2">Choose categories</asp:ListItem>
                </asp:RadioButtonList>
                <div id="categoryTargeting" runat="server" class="targetingBox">
                    <table>
                        <tr>
                            <td><asp:ListBox CssClass="multiselectList" id="listAvailableCategories" runat="server" SelectionMode="Multiple"></asp:ListBox></td>
                            <td valign="middle"><input name="listAvailableCategories2listSelectedCategories" class="buttonMove" value=">" type="button" /><br /><input name="listSelectedCategories2listAvailableCategories" class="buttonMove" value="<" type="button" /></td>
                            <td><asp:ListBox CssClass="multiselectList" id="listSelectedCategories" runat="server" SelectionMode="Multiple"></asp:ListBox></td>
                        </tr>
                    </table>
                </div>
                        
                <br />
                
                <b>Choose keywords that best relate to your product or business:</b>
                <br />
                <asp:RadioButtonList CssClass="RadioField" ID="radioKeywords" runat="server">
                    <asp:ListItem Value="1">Do not target by keywords</asp:ListItem>
                    <asp:ListItem Value="2">Enter keywords</asp:ListItem>
                </asp:RadioButtonList>
                <div id="keywordsTargeting" runat="server" class="targetingBox">
                    <asp:TextBox ID="textKeywords" runat="server" CssClass="TextareaField" TextMode="MultiLine" ></asp:TextBox>
                    <br />
                    <span class="Explanation">Example: music, movies, sports</span>
                </div>
            </div>
            
            <h2>Budget and Schedule <span class="mode">(basic | <a href="#">advanced</a>)</span></h2>
            <div class="Section">                
                <table width="100%">
                    <tr>
                        <td width="50%">
                            <p><b>Choose to limit your budget or the number of types:</b></p>
                            
                            <table>
                                <!-- budget -->
                                <tr>
                                    <td>
                                        <input type="radio" name="radioPreDefinedType" id="radioPreDefinedType_Budget" value="1" checked="checked" onclick="javascript:changePreDefinedType();" />
                                        <label for="radioPreDefinedType_Budget">Pre defined budget limit</label>
                                    </td>
                                    <td align="right">
                                        $ <input id="textBudget" type="text" class="InputField Width80" onkeyup="javascript:calcEstimate();" maxlength="7" value="" />
                                    </td>
                                    <td align="left">
                                        <img src="../css/Inqwise/images/helpicon.png" class="tooltip" rel="Define in advance your campaign's BUDGET limit" />
                                        <span class="Explanation">(Min. $10)</span>                                    
                                    </td>
                                </tr>
                                <!-- bulk -->
                                <tr>
                                    <td>
                                        <input type="radio" name="radioPreDefinedType" id="radioPreDefinedType_Bulk" value="2" onclick="javascript:changePreDefinedType();" />
                                        <label for="radioPreDefinedType_Bulk">Pre defined number of user typings</label>
                                    </td>
                                    <td align="right">
                                        <input id="textBulkCPT" type="text" class="InputField Width80 disabled" maxlength="12" onkeyup+="javascript:calcEstimate();" value="" />
                                    </td>
                                    <td align="left">
                                        <img src="../css/Inqwise/images/helpicon.png" class="tooltip" rel="Define in advance the number of times users will TYPE your text args" />
                                    </td>
                                </tr>                            
                            </table>
                            
                            <p><b>Choose your campaign's length (days):</b></p>

                            <table id="sliderHolder">
                                <tr>
                                    <td><div id="slider"></div></td>
                                    <td rowspan="2" valign="middle"><span class="Explanation">You can shrink/expand your campaign's length and see how it effects the estimated clicks/cost.</span></td>
                                </tr>
                                <tr>
                                    <td><img src="../images/slider_axis.bmp" /></td>
                                </tr>
                            </table>                                                                                                
                            
                            <br />

                            <b>Choose when to start running your campaign:</b>
                            <table>
                                <tr style="line-height:30px;">
                                    <td>Schedule:*</td>
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
                                        <img id="calendar_from_button" alt="From Date" class="calendar_button" src="../images/calendar.jpg" 
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
                                        </div>                                               
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td id="estimatedDiv" align="right">
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
                </table>
            
                <div id="buttonHolder">                
                    <a class="button"><span>Submit</span></a>
                    <a class="button"><span>Cancel</span></a>
                </div>
            </div>
                            
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </div>
</asp:content>