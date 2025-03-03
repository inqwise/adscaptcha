<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="NewAd.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.NewAd" %>
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
                <h2>Ad's Information</h2>
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
                                                        <asp:RequiredFieldValidator ID="validatorAdName1"
                                                            runat="server" ControlToValidate="textAdName" ValidationGroup="Form"
                                                            CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                                        <asp:CustomValidator ID="validatorAdName2"
                                                            runat="server" ControlToValidate="textAdName" Display="Dynamic"
                                                            OnServerValidate="checkAdExist_ServerValidate" ValidationGroup="Form"
                                                            CssClass="ValidationMessage" ErrorMessage="* Ad name already exists" SetFocusOnError="true">
                                                        </asp:CustomValidator>
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
                                                            SetFocusOnError="true" ValidateEmptyText="true" Enabled="false" />
                                                        <br />
                                                        <span class="Explanation">Text limited to <% =ApplicationConfiguration.MAX_SLOGAN_LENGTH.ToString() %> characters</span>
                                                        <AjaxControlToolkit:FilteredTextBoxExtender ID="filterAdSlogan" runat="server" TargetControlID="textAdSlogan" FilterType="Custom" FilterMode="InvalidChars" InvalidChars=";`\|/" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="FieldFrameHeader">Max. Bid:*</td>
                                                    <td>
                                                        $ <asp:textbox ID="textMaxPpt" runat="server" CssClass="InputFrameField Width40" MaxLength="6" Text="0.05" />
                                                        <asp:RequiredFieldValidator ID="validatorMaxPpt1"
                                                            runat="server" ControlToValidate="textMaxPpt" ValidationGroup="Form"
                                                            CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                                        <asp:RangeValidator ID="validatorMaxPpt2"
                                                            runat="server" ControlToValidate="textMaxPpt" ValidationGroup="Form"
                                                            MaximumValue="50000" MinimumValue="0" Type="Double"
                                                            CssClass="ValidationMessage" ErrorMessage="* Bid range is $0-$50K" Display="dynamic" SetFocusOnError="true">
                                                        </asp:RangeValidator>
                                                        <AjaxControlToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="textMaxPpt" ValidChars=".0123456789" />
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
                                                <tr id="AdTypeClickLikeHolder" runat="server" style="display:none;">
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
                    
                <div id="buttonHolder">
                    <asp:ImageButton ID="buttonSubmit" runat="server" ImageUrl="images/submit.gif"
                        CausesValidation="true" ValidationGroup="Form" onclick="buttonSubmit_Click" />
                    <a id="buttonCancel" class="button" href="ManageAds.aspx?AdvertiserId=<%=Request.QueryString["AdvertiserId"]%>&CampaignId=<%=Request.QueryString["CampaignId"]%>"><span>Cancel</span></a>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
        </div> 
    </div>

</asp:content>