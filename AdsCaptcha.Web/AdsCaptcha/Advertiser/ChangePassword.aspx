<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Advertiser/AdvertiserAccount.Master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript" src="http://cdn.jquerytools.org/1.2.7/full/jquery.tools.min.js"></script>  
<style type="text/css" media="screen">
        ul.tabs {
        list-style:none;
        margin:0 !important;
        padding:0;
        height:44px;
    }
    ul.tabs li {
        float:left;
        text-indent:0;
        padding:0;
        margin:0 !important;
        list-style-image:none !important;
    }
    ul.tabs a {
      float: left;
      padding: 10px;
      position: relative;
      top: 2px;
      font-size: 14px;
      min-width: 140px;
      text-align: center;
      cursor: pointer;
      margin: 5px 5px 5px 0px;
      padding: 10px 20px;
      background: #ECEDEF;
      border: 1px solid #ccc;
      -moz-border-radius: 5px 5px 0 0;
      -webkit-border-radius: 5px 5px 0 0;
      border-radius: 5px 5px 0 0;
      
      /*text-shadow: 1px 1px 1px white;*/
      text-decoration:none;
      color: #AAA;
    font-family: Arial;
    font-size: 14px;
     font-weight:700;
    }
    ul.tabs a:hover {
background-color: #F6F6F6;
color: #7C7C7C;
    }
    ul.tabs a.current, ul.tabs a.current:hover, ul.tabs li.current a {
      background: white;
      border-bottom: 1px solid white;
      font-weight:700;
      color: #19C12D;
      z-index: 101;
    }
    .panes .pane {
        display:none;
    }
    .panes {
      padding: 10px 10px 10px 10px;
      background-color: white;
      position: relative;
      z-index: 100;
      
    }
    .panes
    {
    	border: 1px solid #ccc;
    }
  </style>
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
   
   #buttonHolder
   {
   	margin-top:30px;
   	padding-bottom: 50px;
   }
   
   .ButtonHolder a
   {
   	color:#FFFFFF;
   }
   
  .container {
       /*background: -moz-radial-gradient(center center , ellipse farthest-corner, #FFFFFF 0%, #EAEAEA 100%) repeat scroll 0 0 transparent;
    border: 2px solid #FFFFFF;
    box-shadow: 0 5px 22px #BBBBBB;
    height: 370px;*/
    margin: 0px auto;
    overflow: hidden;
    padding: 50px 0 0 0;
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
   </style>
    <style type="text/css">
  
  #top-content .menu-right
  {
  	width:600px;
  }
  #top-content
    {
	    padding:0 0 30px 0;
    }

    #content .description
    {
	    padding-left:20px;
    }
  </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Change Password
</asp:Content>
<asp:content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!--div id="navigation">
        <div class="navigation">
            <ul>
                <li><a href="ManageCampaigns.aspx">Campaigns</a></li>
                <li><a href="BillingSummary.aspx">Billing</a></li>
                <li class="selected"><a href="AccountPreferences.aspx">My Account</a></li>
            </ul>
        </div>
    </div>
    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li>Account Preferences</li>
                <li><a href="ChangePassword.aspx">Change Password</a></li>
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
                <!--div class="menu-right">
                <li><a href="AccountPreferences.aspx">Account Preferences</a></li>
                <li class="active"><a href="ChangePassword.aspx">Change Password</a></li>
            </div>
            <div class="clear"></div>
            <br />
                   <ul class="tabs">
	            <li><a href="AccountPreferences.aspx">Account Preferences</a></li>
	            <li><a href="ChangePassword.aspx" class="current">Change Password</a></li>
            </ul-->
                          <div id="top-content">
                    <div class="menu-right">
                         <ul  style="width:auto;text-align:right;">
                <li><a href="AccountPreferences.aspx">Account Preferences</a></li>
	            <li class="active none"><a href="ChangePassword.aspx">Change Password</a></li>

                        </ul>
                    </div>
                 </div>
             <div style="background-color:#19C12D;height: 45px;width: 100%;"></div>
            <div class="description">
             <div>
            <br /><br />     

            <div class="description">
            
                <!-- Change password form -->
                <asp:Panel ID="panelForm" runat="server" Visible="true">
                    <table>
                        <tr>
                            <td class="FieldHeader">Old Password:*</td>
                            <td>
                                <asp:textbox ID="textOldPassword" runat="server" CssClass="InputField" MaxLength="50" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="validatorOldPassword1"
                                    runat="server" ControlToValidate="textOldPassword" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:CustomValidator ID="validatorOldPassword2"
                                    runat="server" ControlToValidate="textOldPassword" Display="Dynamic"
                                    OnServerValidate="checkOldPassword_ServerValidate" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Wrong password" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Password:*</td>
                            <td>
                                <asp:textbox ID="textPassword" runat="server" CssClass="InputField" MaxLength="50" TextMode="Password" onkeyup="runPassword(this.value,'Password_Strength');" />
                                <asp:RequiredFieldValidator ID="validatorPassword1"
                                    runat="server" ControlToValidate="textPassword" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                                <asp:RegularExpressionValidator ID="validatorPassword2"
                                    runat="server" ControlToValidate="textPassword" ValidationGroup="Form"
                                    CssClass="ValidationMessage" ErrorMessage="* Minimum 5 letters" Display="dynamic"
                                    ValidationExpression=".{5}.*" SetFocusOnError="true" />
			                    <div style="width:100px;"> 
				                    <div id="Password_Strength_text" style="font-size:10px;"></div>
				                    <div id="Password_Strength_bar" style="font-size:1px; height:2px; width:0px; border:1px solid white;"></div> 
			                    </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldHeader">Confirm Password:*</td>
                            <td>
                                <asp:textbox ID="textPasswordConfirm" runat="server" CssClass="InputField" MaxLength="50" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="validatorConfirmPassword1" 
                                runat="server" ControlToValidate="textPasswordConfirm" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            <asp:CompareValidator ID="validatorConfirmPassword2" 
                                runat="server" ControlToValidate="textPasswordConfirm" ControlToCompare="textPassword"
                                Type="String" Operator="Equal" ValidationGroup="Form" 
                                CssClass="ValidationMessage" ErrorMessage="* Must be identical to password" SetFocusOnError="true" />
                            </td>
                        </tr>
                    </table>
                    
                    <div id="buttonHolder" style="margin: 20px 0 0 -10px;">                
                        <asp:LinkButton id="buttonSubmit" runat="server" CssClass="btn" style="color:#FFFFFF;" OnClick="buttonSubmit_Click" CausesValidation="true" ValidationGroup="Form"><span>Submit</span></asp:LinkButton>
                        <a id="buttonCancel" class="btn" style="color:#FFFFFF;" href="AccountPreferences.aspx"><span>Cancel</span></a>
                    </div>
                </asp:Panel>
                    
                <!-- Password changed -->
                <asp:Panel ID="panelPasswordChanged" runat="server" Visible="false" CssClass="ChangesSaved">
                    Password changed.
                    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                </asp:Panel>
            
            </div>
            
            </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>    
        </div>
    </div>
</asp:content>  