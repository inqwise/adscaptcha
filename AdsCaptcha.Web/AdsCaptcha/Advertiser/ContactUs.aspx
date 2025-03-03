<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="Advertiser.Master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.ContactUs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<asp:content ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="warp">
                <div id="contact_us">
                    <div id="pageHeader" class="contact_us"></div>        
                    <div id="pageTitle">Contact Us</div>

                    <asp:Panel ID="panelContactForm" runat="server">
                    To join Ads Captcha™, please complete this form and one of our representatives will be in contact with you.
                    <br />
                    <br />
                    
                    <table>
                        <tr>
                            <td class="FieldHeader">Name:*</td>
                            <td>
                                <asp:TextBox ID="textName" runat="server" CssClass="InputField" MaxLength="50" Text="" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="textName" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Who are you:*</td>
                            <td>
                                <asp:DropDownList ID="listType" runat="server" CssClass="SelectField">
                                    <asp:ListItem Value="0" Text="-- Select --"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Advertisement Agency"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Private Business"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="listType" ValidationGroup="Form" InitialValue="0"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Agency/Brand:*</td>
                            <td>
                                <asp:TextBox ID="textAgencyBrandName" runat="server" CssClass="InputField" MaxLength="50" Text="" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="textAgencyBrandName" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Website:</td>
                            <td>
                                <asp:TextBox ID="textWebsite" runat="server" CssClass="InputField" MaxLength="50" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">E-mail:*</td>
                            <td>
                                <asp:TextBox ID="textEmail" runat="server" CssClass="InputField" MaxLength="100" Text="" />
                                <asp:RegularExpressionValidator runat="server" SetFocusOnError="true"
                                    ControlToValidate="textEmail" ValidationGroup="Form" 
                                    ValidationExpression=".*@.*\..*"
                                    CssClass="ValidationMessage" ErrorMessage="* Not a valid e-mail"
                                    Display="dynamic" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="textEmail" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Phone:*</td>
                            <td>
                                <asp:textbox ID="textPhone" runat="server" CssClass="InputField" MaxLength="30" Text="" />
                                <AjaxControlToolkit:FilteredTextBoxExtender runat="server" TargetControlID="textPhone" ValidChars="+-0123456789" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="textPhone" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Country:*</td>
                            <td>
                                <asp:textbox ID="textCountry" runat="server" CssClass="InputField" MaxLength="50" Text="" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="textCountry" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Comments:</td>
                            <td>
                                <asp:TextBox ID="textNotes" runat="server" CssClass="TextareaField" TextMode="MultiLine" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <br />
                                <div id="ButtonHolder">                
                                    <asp:LinkButton id="buttonSubmit" runat="server" CssClass="buttonSubmit" OnClick="buttonSubmit_Click" CausesValidation="true" ValidationGroup="Form"></asp:LinkButton>
                                    <a id="buttonCancel" class="button" href="StartPage.aspx"><span>Cancel</span></a>
                                </div>
                            </td>
                        </tr>
                    </table>

                    </asp:Panel>

                    <asp:Panel ID="panelMessageSent" runat="server">
                        Thanks.
                        <br />
                        Your request has been sent successfully.
                    </asp:Panel>        
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>                
</asp:content>