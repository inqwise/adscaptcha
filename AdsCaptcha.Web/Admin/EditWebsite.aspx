<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeFile="EditWebsite.aspx.cs" Inherits="Inqwise.AdsCaptcha.Admin.EditWebsite" %>
<%@ Register assembly="ComponentArt.Web.UI" namespace="ComponentArt.Web.UI" tagprefix="ComponentArt" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
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
                <h2>Website's Information</h2>
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
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="textUrl" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" ID="textUrlRfValidator" />
                                <asp:CustomValidator runat="server" ControlToValidate="textUrl" Display="Dynamic"
                                    OnServerValidate="checkWebsiteExist_ServerValidate" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Website url already exist" SetFocusOnError="true" ID="textUrlCValidator" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Status:*</td>
                            <td>
                                <asp:DropDownList ID="listStatus" CssClass="SelectField" runat="server" DataTextField="Item_Desc" DataValueField="Item_Id" ValidationGroup="Form" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="listStatus" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" ID="listStatusRfValidator" />
                            </td>
                        </tr>
                    </table>
                </div>
                
                <h2>Targeting</h2>
                <div class="Section">
                    <table>
                        <tr>
                            <td class="FieldHeader">Language:*</td>
                            <td>
                                <asp:DropDownList ID="listLanguage" CssClass="SelectField" runat="server" DataTextField="Language_Name" DataValueField="Language_Id" ValidationGroup="Form" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="listLanguage" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" ID="listLanguageRfValidator" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Country:*</td>
                            <td>
                                <asp:DropDownList ID="listCountry" runat="server" CssClass="SelectField" DataTextField="Country_Name" DataValueField="Country_Id" ValidationGroup="Form" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="listCountry" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" ID="listCountryRfValidator" />
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
                                    CssClass="ValidationMessage" ErrorMessage="* One of your keywords exceeds 50 chars" SetFocusOnError="true"/>
                                <br />
                                <span class="Explanation">Example: music, movies, sports</span>                    
                            </td>
                        </tr>
                    </table>
                </div>
                         
                <asp:Panel ID="panelSecurityLevel" runat="server" Visible="false">
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
                                    Increment="1"
                                    ClientIDMode="AutoID">
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
                </asp:Panel>
                
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
                        <tr>
                            <td class="FieldHeader">Total Revenue:</td>
                            <td>
                                $ <asp:Label ID="totalRevenue" runat="server"></asp:Label>
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