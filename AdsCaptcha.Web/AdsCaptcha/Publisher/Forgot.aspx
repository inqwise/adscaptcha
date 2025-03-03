<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/OldAdsCaptcha.Master" AutoEventWireup="true" CodeFile="Forgot.aspx.cs" Inherits="Inqwise.AdsCaptcha.Publisher.Forgot" %>
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
   
   .errorMessage
   {
   	color:Red;
   	font-weight:bold;
   	margin:10px 0 20px 0;
   }
   
   #content h4 
   {
   	line-height:55px;
   }
   </style>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
id="body" class="page"
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="LoginContent" runat="server"> 
    <li>|
    </li>
    <li><a id="loginBox" runat="server" href="~/Publisher/Login.aspx" class="login-window">SIGN IN</a></li>
        <li>|
    </li>
    <li><a id="signBox" href="SignUp.aspx">SIGN UP</a></li>
</asp:Content>
<asp:content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

  <div id="boxes"></div><!--end boxes-->

    <div id="status">
        <div class="inner-status">
            <div class="status-box">
                <h2>Site owner, Forgot password</h2><!-- <div class="status-number">144,453,448</div> -->
            </div><!--end status-box-->
        </div><!--end inner-status-->
    </div><!--end status-->

    <div id="content">
        <div class="inner-content">
        
                    <br>
<h4>Forgot your password? <span class="uppercase"></span></h4>
    <div class="description">
        <div id="loginFrame">
            <table cellspacing="5" cellpadding="0" align="center">
                <tr>
                    <td>                
                        <asp:Panel ID="panelForm" runat="server" Visible="true">
                            <div class="frameTitle">Enter your e-mail here and we'll send you a new password.</div>
                            
                            <br />
                            E-mail: <asp:TextBox ID="textEmail" runat="server" CssClass="InputField" MaxLength="100" Width="200px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="textEmail" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="textEmail" ValidationGroup="Form" 
                                ValidationExpression=".*@.*\..*"
                                CssClass="ValidationMessage" ErrorMessage="* Invalid"
                                Display="dynamic" SetFocusOnError="true" />
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="textEmail" Display="Dynamic"
                                OnServerValidate="checkEmailExists_ServerValidate" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Email not exists" SetFocusOnError="true" />
                            <br />
                            <asp:Label ID="labelErrorMessage" runat="server" CssClass="ValidationMessage"></asp:Label>
                            <div class="ButtonHolder">
                            <asp:LinkButton id="buttonRemindMe" runat="server" style="color:#FFFFFF;" CssClass="btn" onclick="buttonRemindMe_Click" ValidationGroup="Form">Send</asp:LinkButton>
                            </div>
                        </asp:Panel>
                        
                        <asp:Panel ID="panelSent" runat="server" Visible="false">
                            <b>Password sent</b>
                            <br />
                            Thank you. We will email your new password momentarily - you may have to check your SPAM folder.
                            <br />
                            Please login with your new password and change it.
                            <br />
                            If you have further difficulties, please visit our support page.
                        </asp:Panel>        
                    </td>
                </tr>
            </table>
            </div>
            </div>
        </div>
    </div>
</asp:content>