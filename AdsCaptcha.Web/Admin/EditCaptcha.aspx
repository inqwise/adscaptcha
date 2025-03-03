<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="EditCaptcha.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.EditCaptcha" %>
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

        function ChangeFlashDimensions() {
            var i = 0;

            var s = $("#<%=listSliderDims.ClientID %>");
            var w = s.val().split("x")[0];
            var h = s.val().split("x")[1];
            previewWidth = w;
            previewHeight = h;
            if (i == 0) {
                ChangeAdSize(w, h);
            }
            
            var widgetOffsetH = $("div.ButtonsHolder").first().css("height") + $("div.SliderMessage").first().css("height");
            $("div.AdsCaptchaWidget").css({ width: w, height: h + widgetOffsetH + 20 });

            var tdW = $("td.previewCaptcha").width();
            var divMargin = (tdW - w) / 2;
            if (divMargin < 50) divMargin = 0;
            $("div.previewCaptchaHolder").css({ "margin-right": divMargin });

            //$("#AdsCaptchaWidget").css("height", 2 * h + "px")
            //            $("embed").attr("height", h);
            //            $("embed").attr("width", w);
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
                <h2>Captcha's Information</h2>
                <div class="Section">
                    <table>
                        <tr>
                            <td class="FieldHeader">Captcha Name:*</td>
                            <td>
                                <asp:textbox ID="textCaptchaName" runat="server" CssClass="InputField" MaxLength="50" Text="" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="textCaptchaName" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="textCaptchaName" Display="Dynamic"
                                    OnServerValidate="checkCaptchaExist_ServerValidate" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Captcha name already exist" SetFocusOnError="true">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Status:*</td>
                            <td>
                                <asp:DropDownList ID="listStatus" runat="server" CssClass="SelectField" ValidationGroup="Form" DataValueField="Item_Id" DataTextField="Item_Desc" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="listStatus" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">ID:</td>
                            <td><asp:Label ID="labelCaptchaId" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Public Key:</td>
                            <td><asp:Label ID="labelPublicKey" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Private Key:</td>
                            <td><asp:Label ID="labelPrivateKey" runat="server"></asp:Label></td>
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
                                    <td id="tdCommercial">
                                    <asp:RadioButtonList ID="rblCommercial" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="1">Commercial</asp:ListItem>
                                        <asp:ListItem Value="0">Not commercial</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
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
                                <div id="CaptchaTypeHolder5" runat="server" class="previewCaptchaHolder"> <!-- slider-->
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
                        
                <div id="buttonHolder">
                    <asp:ImageButton ID="buttonSubmit" runat="server" ImageUrl="images/submit.gif"
                        CausesValidation="true" ValidationGroup="Form" onclick="buttonSubmit_Click" />
                    <a id="buttonCancel" class="button" href="ManageCaptchas.aspx?DeveloperId=<%=Request.QueryString["DeveloperId"]%>&PublisherId=<%=Request.QueryString["PublisherId"]%>&WebsiteId=<%=Request.QueryString["WebsiteId"]%>"><span>Cancel</span></a>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
        </div> 
    </div>
<script type="text/javascript">    ChangeFlashDimensions();</script>
</asp:content>