<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="NewWebsite.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.NewWebsite" %>
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

<%
    if (String.IsNullOrEmpty(Page.Request.QueryString["DeveloperId"]))
    {
%>
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_2').addClass('selected');
        });
    </script>    
<% 
    }
    else
    {
%>
    <script type="text/javascript" charset="utf-8">
        $(function() {
            $('#menu_4').addClass('selected');
        });
    </script>    
<%
    }
%>

    <script type="text/javascript" charset="utf-8">
        previewSlogan = 'FOX SALE';

        pageLoad = function() {
            OnCaptchaTypeChange();

            var themeId = getThemeId();
            previewLoadTheme(themeId);
        };


        var ChangeCaptchaTypeValue = "130051";

        function ChangeCaptchaType() {
            var select = $('#<%=listCaptchaType.ClientID %>').val();
            var radio = $('input[name="<%=rblCommercial.ClientID.Replace("_", "$") %>"]:checked').val(); ;
            if ((select == 13005) && (radio == 1)) ChangeCaptchaTypeValue = "130051";
            else if ((select == 13005) && (radio == 0)) ChangeCaptchaTypeValue = "130052";
            else if ((select == 13006) && (radio == 1)) ChangeCaptchaTypeValue = "130061";
            else if ((select == 13006) && (radio == 0)) ChangeCaptchaTypeValue = "130062";
            else if (select == 13007) ChangeCaptchaTypeValue = "13007";
            OnCaptchaTypeChange();
        }
        
    </script>    
</asp:Content>

<asp:content ContentPlaceHolderID="MainContent" runat="server">

    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
            <%
                if (String.IsNullOrEmpty(Page.Request.QueryString["DeveloperId"]))
                {
            %>
                <li><a href="ReportPublishers.aspx">Report</a></li>
                <li><a href="ManagePublishers.aspx">Manage</a></li>
                <li><a href="PendingWebsites.aspx">Pending Websites</a></li>
                <li><a href="PublishersToBePaid.aspx">To Be Paid</a></li>
                <li><a href="NewPublisher.aspx">New Site Owner</a></li>
                <li><a href="ReportCountries.aspx">Countries Report</a></li>
                <li><a href="RandomImage.aspx">Random Images</a></li>
            <% 
                }
                else
                {
            %>
                <li><a href="ManageDevelopers.aspx">Manage</a></li>
                <li><a href="DevelopersToBePaid.aspx">To Be Paid</a></li>
                <li><a href="NewDeveloper.aspx">New Developer</a></li>
            <%
                }
            %>
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
                <h2>Targeting</h2>
                <div class="Section">
                    <table>
                        <tr>
                            <td class="FieldHeader">
                                <table width="100%">
                                    <tr><td align="left">Url:*</td><td align="right">http://</td></tr>
                                </table>
                            </td>
                            <td>
                                <asp:textbox ID="textUrl" runat="server" CssClass="InputField" MaxLength="100" Text="" />
                                <asp:RequiredFieldValidator ID="validatorUrl1"
                                    runat="server" ControlToValidate="textUrl" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:CustomValidator ID="validatorUrl2"
                                    runat="server" ControlToValidate="textUrl" Display="Dynamic"
                                    OnServerValidate="checkWebsiteExist_ServerValidate" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Website url already exist" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Language:*</td>
                            <td>
                                <asp:DropDownList ID="listLanguage" runat="server" CssClass="SelectField" DataTextField="Language_Name" DataValueField="Language_Id" ValidationGroup="Form" />
                                <asp:RequiredFieldValidator ID="validatorLanguage"
                                    runat="server" ControlToValidate="listLanguage" ValidationGroup="Form" InitialValue="0"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Country:*</td>
                            <td>
                                <asp:DropDownList ID="listCountry" runat="server" CssClass="SelectField" DataTextField="Country_Name" DataValueField="Country_Id" ValidationGroup="Form" />
                                <asp:RequiredFieldValidator ID="validatorRegion"
                                    runat="server" ControlToValidate="listCountry" ValidationGroup="Form" InitialValue="0"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Categories:</td>
                            <td>
                                <asp:CheckBoxList CssClass="CheckBoxField" ID="checkCategory" runat="server" RepeatColumns="3"></asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="FieldHeader">Keywords:</td>
                            <td>
                                <asp:TextBox ID="textKeywords" runat="server" CssClass="TextareaField" TextMode="MultiLine" ></asp:TextBox>
                                <asp:CustomValidator ID="validatorKeywords"
                                    runat="server" ControlToValidate="textKeywords" Display="Dynamic"
                                    OnServerValidate="checkKeywordsLength_ServerValidate" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* One of your keywords exceeds 50 chars" SetFocusOnError="true" />
                                <br />
                                <span class="Explanation">Example: music, movies, sports</span>                    
                            </td>
                        </tr>
                    </table>
                </div>
                         
                <h2>Captcha Information</h2>
                <div class="Section">
                    <table>
                        <tr>
                            <td class="FieldHeader">Captcha Name:*</td>
                            <td>
                                <asp:textbox ID="textCaptchaName" runat="server" CssClass="InputField" MaxLength="50" Text="" />
                                <asp:RequiredFieldValidator ID="validatorCaptchaName"
                                    runat="server" ControlToValidate="textCaptchaName" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                    </table>
                </div>
                
                <h2>Captcha Type</h2>
                <div class="Section">
                    <table width="100%">
                        <tr>
                            <td width="60%" class="FieldsSet">
                            <!--<p class="FieldsExplanation">
                            AdsCaptcha has 4 basic types:
                            <br />
                            <b>FREE Security Only</b> <img src="images/tooltip.jpg" class="tooltip" rel="This CAPTCHA has no commercial content. It generates no income." />, 
                            <b>FREE Security Slider</b> <img src="images/tooltip.jpg" class="tooltip" rel="This CAPTCHA has no commercial content. It generates no income." />, 
                            <b>GET Paid Per Type</b> <img src="images/tooltip.jpg" class="tooltip" rel="<b>GET PPT™ (Pay-Per-Type).</b> This defines what a site owner will earn when a user successfully types the captcha." /> and 
                            <b>GET Paid Per Fit</b> <img src="images/tooltip.jpg" class="tooltip" rel="<b>GET PPF (Pay-Per-Fit).</b> This defines what a site owner will earn when a user successfully fits the slider captcha." />
                            </p>
                            -->
                            <table width="100%">
                                <tr valign="top">
                                    <td class="FieldHeader">Captcha Type:*</td>
                                    <td align="left">
                                        <asp:DropDownList ID="listCaptchaType" runat="server" CssClass="SelectField" DataTextField="Item_Desc" DataValueField="Item_Id" />
                                        <asp:RequiredFieldValidator ID="validatorCaptchaType"
                                            runat="server" ControlToValidate="listCaptchaType" ValidationGroup="Form"
                                            CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                        
                                    </td>
                                    <td id="tdCommercial">
                                    <asp:RadioButtonList ID="rblCommercial" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="1">Commercial</asp:ListItem>
                                        <asp:ListItem Value="0">Not commercial</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <div id="AllowClickOptions" runat="server">
                                <table width="100%">
                                    <tr valign="top">
                                        <td class="FieldHeader" style="display:none;">Clickable?:*</td>
                                        <td align="left" style="display:none;">
                                            <asp:RadioButtonList CssClass="RadioField" ID="radioAllowClick" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="false"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td  style="display:none;">
                                            <asp:CheckBox ID="checkAllowPopup" runat="server" Text="Allow pop under" /> <img src="images/tooltip.jpg" class="tooltip" rel="Pop-unders are generally new web browser windows to display advertisements." />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="CaptchaTypePayPerTypeHolder" runat="server">
                                <b>Choose your advertisement content:</b>                            
                                <asp:CustomValidator ID="validatorAllowContent" runat="server" 
                                    ControlToValidate="listCaptchaType" Display="Dynamic"
                                    ClientValidationFunction="validateAllowedContentClient" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" SetFocusOnError="true" />
                                <table>
                                    <tr>
                                        <td width="280">
                                            <asp:CheckBox ID="checkAllowVideo" runat="server" Text="Allow video ad with text slogan" class="tooltip" rel="This CAPTCHA offers a video ad to go along with the text.<br /><br />It ranks 3 on the commercial orientation scale and generates the most income.<br /><br /><img width='200' height='168' src='../images/profitability_high.png' />" /> <span class="Explanation">(coming soon)</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="checkAllowImage" runat="server" Text="Allow image ad with text slogan" class="tooltip" rel="This CAPTCHA offers an image to go along with the text.<br /><br />It ranks 2 on the commercial orientation scales and generates more income than a text only Captcha.<br /><br /><img width='200' height='168' src='../images/profitability_medium.png' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="checkAllowSlogan" runat="server" Text="Allow text slogan" class="tooltip" rel="This CAPTCHA offers a slogan, domain name, brand name or any other commercial text as the keywords to be typed in by the user.<br /><br />It ranks 1 on the commercial orientation scale and generates income.<br /><br /><img width='200' height='168' src='../images/profitability_low.png' />" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="CaptchaTypeSecurityOnlyHolder" runat="server">
                                Security Only CAPTCHA has no commercial content. It generates no income.
                            </div>
                            <div id="CaptchaTypeSecuritySliderHolder" runat="server">
                                FREE Security Slider is a new approach in the field of CAPTCHA.
                                <br />
                                Instead of typing text, the user is required to use a slider in order to scroll the broken part of the picture until it merges and becomes complete.
                            </div>
                            <div id="CaptchaTypeSliderHolder" runat="server">
                                GET Paid Per Fit is a new approach in the field of CAPTCHA.
                                <br />
                                Instead of typing text, the user is required to use a slider in order to scroll the broken part of the picture until it merges and becomes complete.
                            </div>
                        </td>
                            
                            <td class="arrow" align="right"></td>
                            
                            <td width="30%" class="previewCaptcha">
                                <div id="CaptchaTypeHolder0" runat="server" class="previewCaptchaHolder"> <!-- security only -->
                                    <script type="text/javascript">
                                        var type = 1;

                                        GetCaptcha(type, 'A53Fx4', null, previewWidth, previewHeight, previewDir, previewThemeId);
                                    </script>
                                </div>
                                <div id="CaptchaTypeHolder1" runat="server" class="previewCaptchaHolder"> <!-- slogan only -->
                                    <script type="text/javascript">
                                        var type = 2;
                                        previewURL = null;

                                        GetCaptcha(type, previewSlogan, previewURL, previewWidth, previewHeight, previewDir, previewThemeId);
                                    </script>
                                </div>
                                <div id="CaptchaTypeHolder2" runat="server" class="previewCaptchaHolder"> <!-- image -->
                                    <script type="text/javascript">
                                        var type = 3;
                                        previewURL = ADSCAPTCHA_URL + 'images/Preview/GeneratorImage.jpg';

                                        GetCaptcha(type, previewSlogan, previewURL, previewWidth, previewHeight, previewDir, previewThemeId);
                                    </script>
                                </div>
                                <div id="CaptchaTypeHolder3" runat="server" class="previewCaptchaHolder"> <!-- image click -->
                                    <script type="text/javascript">
                                        var type = 3;
                                        previewURL = ADSCAPTCHA_URL + 'images/Preview/GeneratorImageClick.jpg';

                                        GetCaptcha(type, previewSlogan, previewURL, previewWidth, previewHeight, previewDir, previewThemeId);
                                    </script>
                                </div>
                                <div id="CaptchaTypeHolder4" runat="server" class="previewCaptchaHolder"> <!-- video -->
                                    <script type="text/javascript">
                                        var type = 4;
                                        previewURL = ADSCAPTCHA_URL + 'images/Preview/CaptchaVideoFox.swf';

                                        GetCaptcha(type, previewSlogan, previewURL, previewWidth, previewHeight, previewDir, previewThemeId);
                                    </script>
                                </div>
                                <div id="CaptchaTypeHolder5" runat="server" class="previewCaptchaHolder"> <!-- slider -->
                                    <script type="text/javascript">
                                        previewURL = ADSCAPTCHA_URL + 'Images/Preview/Slider.jpg';

                                        GetSlider('FlashSlider', previewURL, previewWidth, previewHeight, previewThemeId);
                                    </script>
                                </div>
                                <div id="CaptchaTypeHolder6" runat="server" class="previewCaptchaHolder"> <!-- security slider -->
                                    <script type="text/javascript">
                                        GetSlider('FlashSecuritySlider', previewSecuritySliderURL, previewWidth, previewHeight, previewThemeId);
                                    </script>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                            
                <!--h2>Design Your Captcha</h2-->
                <div class="Section">
                    <div class="captchaDimension">
                        <p class="FieldsExplanation">
                        Please choose the dimensions of the advertisement content for your CAPTCHA.
                        </p>
                        <table id="tableCaptchaDims" runat="server">
                            <tr>
                                <td class="FieldHeader">Dimensions:*</td>
                                <td>
                                    <asp:DropDownList ID="listCaptchaDims" runat="server" CssClass="SelectField Width80" /> pixles
                                    <asp:CustomValidator ID="validatorCaptchaDims"  
                                        runat="server" ControlToValidate="listCaptchaDims" Display="Dynamic"
                                        OnServerValidate="checkCaptchaDims_ServerValidate" ValidationGroup="Form"
                                        CssClass="ValidationMessage" ErrorMessage="* Required" SetFocusOnError="true">
                                    </asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                        <table id="tableSliderDims" runat="server">
                            <tr>
                                <td class="FieldHeader">Slider Dimensions:*</td>
                                <td>
                                    <asp:DropDownList ID="listSliderDims" runat="server" CssClass="SelectField Width80" /> pixles
                                    <asp:CustomValidator ID="validatorSliderDims"  
                                        runat="server" ControlToValidate="listSliderDims" Display="Dynamic"
                                        OnServerValidate="checkSliderDims_ServerValidate" ValidationGroup="Form"
                                        CssClass="ValidationMessage" ErrorMessage="* Required" SetFocusOnError="true">
                                    </asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <p class="FieldsExplanation">
                    <br />
                    Please choose the theme for your CAPTCHA.
                    </p>
                    <table width="100%">
                        <tr>
                            <td class="FieldHeader">Theme:*</td>
                            <td>
                                <asp:DropDownList ID="listThemes" runat="server" CssClass="SelectField Width80" />
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="captchaSecurityLevel">
                    <h2>Captcha Security Level</h2>
                    <div class="Section">
                        <table width="100%">
                            <tr valign="top">
                                <td class="FieldHeader">Security Level:*</td>
                                <td>
                                    <ComponentArt:Slider ID="sliderSecurityLevel" runat="server" 
                                        CssClass="slider"
                                        GripCssClass="grip"
                                        GripHoverCssClass="grip"
                                        TrackCssClass="track"
                                        IncreaseTrackCssClass="track-inc"
                                        TrackLength="218"
                                        MouseWheelFactor="-1"
                                        IncreaseToolTip="+"
                                        DecreaseToolTip="-"
                                        PopUpClientTemplateId="PopUpTemplate"
                                        PopUpOffsetY="10"
                                        GripToolTip="Drag"                        
                                        MinValue="0"
                                        MaxValue="5"                        
                                        Increment="1">
                                        <ClientTemplates>
                                            <ComponentArt:ClientTemplate ID="PopUpTemplate">
                                                <div class="popup">## OnDragSecurityLevel(Parent.get_value()); ##</div>
                                            </ComponentArt:ClientTemplate>
                                        </ClientTemplates>
                                    </ComponentArt:Slider>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                
                <h2>Bonus</h2>
                <div class="Section">
                    <table>
                        <tr>
                            <td class="FieldHeader">Enable Bonus?*</td>
                            <td>
                                <asp:CheckBox ID="checkBonus" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Bonus Limit:</td>
                            <td>
                                $ <asp:textbox ID="textBonusLimit" runat="server" CssClass="InputField Width40" MaxLength="3" />
                            </td>
                        </tr>
                    </table>
                </div>

                <div id="buttonHolder">
                    <asp:ImageButton ID="buttonSubmit" runat="server" ImageUrl="images/submit.gif"
                        CausesValidation="true" ValidationGroup="Form" onclick="buttonSubmit_Click" />
                    <a id="buttonCancel" class="button" href="ManageWebsites.aspx?DeveloperId=<%=Request.QueryString["DeveloperId"]%>&PublisherId=<%=Request.QueryString["PublisherId"]%>"><span>Cancel</span></a>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
        </div> 
    </div>

</asp:content>