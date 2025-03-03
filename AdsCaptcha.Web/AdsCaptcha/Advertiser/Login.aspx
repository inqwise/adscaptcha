<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/OldAdsCaptcha.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.Login" %>
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
   </style>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
id="body" class="page"
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="LoginContent" runat="server"> 
    <li>|
    </li>
    <li><a id="loginBox" runat="server" href="~/Advertiser/Login.aspx" class="login-window">SIGN IN</a></li>
        <li>|
    </li>
    <li><a id="signBox" href="SignUp.aspx">SIGN UP</a></li>
</asp:Content>
<asp:content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

   <div id="boxes"></div><!--end boxes-->

    <div id="status">
        <div class="inner-status">
            <div class="status-box">
                <h2>Advertisers, Login</h2><!-- <div class="status-number">144,453,448</div> -->
            </div><!--end status-box-->
        </div><!--end inner-status-->
    </div><!--end status-->

    <div id="content">
        <div class="inner-content">
            <br>
<h4>To access this site, <span class="uppercase">you need to login:</span></h4>
    <div class="description">
        <div id="loginFrame">
            <table cellspacing="5" cellpadding="0" align="center" style="margin-left:30px;">
            <tr>
                    <td align="center" colspan="2">
                        <asp:label id="labelMessage" runat="server" CssClass="errorMessage"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td>E-mail:</td>
                    <td><asp:textbox id="textEmail" runat="server" MaxLength="100"  CssClass="InputField"></asp:textbox></td>
                </tr>
                <tr>
                    <td>Password:</td>
                    <td><asp:textbox id="textPassword" runat="server" MaxLength="20" TextMode="Password"  CssClass="InputField"></asp:textbox></td>
                </tr>
                
                <tr>
                <td>
                        <asp:CheckBox id="checkRememberMe" runat="server" Checked="true" Text="    Remember me?" CssClass="rememberMe" />
                       
                    </td>
                </tr>
            </table>
            <table cellspacing="5" cellpadding="0" align="center" style="margin:10px 0 0 10px;">
                <tr>
                    <td>
                        <asp:LinkButton id="buttonLogin" runat="server" CssClass="btn"  style="color:#FFFFFF;" onclick="buttonLogin_Click" Text="Sign In"></asp:LinkButton>
                         <br />
                        <asp:LinkButton id="linkForgotPassword" runat="server" PostBackUrl="Forgot.aspx" CssClass="forgot" style="font-size:12px;margin-left:10px;">Forgot your password?</asp:LinkButton>
                    </td>
                    
                </tr>
            </table>
        </div>        
    </div>
    
       </div>
   </div>
    
</asp:content>