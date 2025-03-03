<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Publisher/PublisherAccount.Master" AutoEventWireup="true" CodeFile="PaymentPreferences.aspx.cs" Inherits="Inqwise.AdsCaptcha.Publisher.PaymentPreferences" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<%@ Register TagPrefix="paymentPreferences" TagName="bank" Src="~/Controls/PaymentPreferences/PaymentPreferencesBank.ascx" %>
<%@ Register TagPrefix="paymentPreferences" TagName="check" Src="~/Controls/PaymentPreferences/PaymentPreferencesCheck.ascx" %>
<%@ Register TagPrefix="paymentPreferences" TagName="paypal" Src="~/Controls/PaymentPreferences/PaymentPreferencesPayPal.ascx" %>
<%@ Register TagPrefix="paymentPreferences" TagName="later" Src="~/Controls/PaymentPreferences/PaymentPreferencesLater.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link type="text/css" rel="Stylesheet" href="../css/Inqwise/selectmenu/jquery.ui.theme.css" />
<link type="text/css" rel="Stylesheet" href="../css/Inqwise/selectmenu/jquery.ui.selectmenu.css" />
<script type="text/javascript" src="http://cdn.jquerytools.org/1.2.7/full/jquery.tools.min.js"></script>  
<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jqueryui/1.9.0/jquery-ui.min.js"></script>  
<script type="text/javascript" src="../css/Inqwise/selectmenu/jquery.ui.selectmenu.js"></script> 

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
   
   .ButtonHolder
   {
   	margin-top:30px;
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
    padding: 50px 0  0 20px;
    position: relative;
    width: auto;
}

#content h4
{
	background-position: left 4px !important;
    margin-bottom: 30px;
    line-height: 1;
}

#buttonHolder
{
	border:0;
	height:50px;
}
#content .description
{
	border:0;
}
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
 
  <style type="text/css" media="screen">
  .success {
	background: #C0F0B3 url('../css/Inqwise/images/success.png') no-repeat 20px center;
	border: 1px solid #91C184;
	color: #5C8A50;
  border-radius: 10px;
  -moz-border-radius: 10px;
  -webkit-border-radius: 10px;
  text-shadow: 1px 1px 1px #fff;
  padding: 16px 20px 8px 75px;
  margin: 15px 0px;
  min-width: 32px;
  min-height: 28px;
  }
}

#content h4
{
	background-position: left 4px !important;
    margin-bottom: 30px;
    line-height: 1;
}
   </style>
    <style type="text/css" media="screen">
  .success {
	background: #C0F0B3 url('../css/Inqwise/images/success.png') no-repeat 20px center;
	border: 1px solid #91C184;
	color: #5C8A50;
  border-radius: 10px;
  -moz-border-radius: 10px;
  -webkit-border-radius: 10px;
  text-shadow: 1px 1px 1px #fff;
  padding: 16px 20px 8px 75px;
  margin: 15px 0px;
  min-width: 32px;
  min-height: 28px;
  }
</style>
  <script type="text/javascript">
      $(function() {
      $('select#<%=listCreditMethod.ClientID %>').selectmenu();
      });
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Payment Preferences
</asp:Content>
<asp:content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



        <!--div id="breadCrambs">
            <asp:Label ID="labelNavigationPath" runat="server" />        
        </div-->


        <div id="content"  class="container">
        <div class="inner-content">
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                 <!--div class="menu-right">
                <li><a href="AccountPreferences.aspx">Account Preferences</a></li>
                <li class="active"><a href="PaymentPreferences.aspx">Payment Preferences</a></li>
                <li><a href="ChangePassword.aspx">Change Password</a></li>
            </div>
            <div class="clear"></div>
            <br />
            
                        <ul class="tabs">
	            <li><a href="AccountPreferences.aspx">Account Preferences</a></li>
	            <li><a href="PaymentPreferences.aspx" class="current">Payment Method</a></li>
	            <li><a href="ChangePassword.aspx">Change Password</a></li>
            </ul-->
            
                 <div id="top-content">
                    <div class="menu-right">
                     <ul  style="width:auto;text-align:right;">
                        <li><a href="AccountPreferences.aspx">Account Preferences</a></li>
                        <li class="active"><a href="PaymentPreferences.aspx">Payment Preferences</a></li>
                        <li class="none"><a href="ChangePassword.aspx">Change Password</a></li>
                        </ul>
                    </div>
                 </div>
                             <div style="background-color:#19C12D;height: 45px;margin-left: 12px;width: 96%;"></div>


             <div  class="description"><br /><br />
            <div class="description">
            
                <p>Choose the payment method you would like us to pay you:</p>
                <table>
                    <tr>
                        <td class="FieldHeader">Payment Method:*</td>
                        <td>
                            <asp:DropDownList ID="listCreditMethod" runat="server" CssClass="SelectField" DataTextField="Item_Desc" DataValueField="Item_Id" ValidationGroup="Form"/>
                        </td>
                    </tr>                    
                </table>
                <div id="ssl">
                    <span id="siteseal"><script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=<%=ConfigurationSettings.AppSettings["SSL_GoDaddy_Seal_Id"]%>"></script></span>
                </div>
            </div>
            <br />
            <div class="description">
                <table>
                    <tr>
                        <td>
                        <div id="PaymentBank_Section">
                            <paymentPreferences:bank id="ctlPaymentBank" runat="server"/>
                        </div>
                        <div id="PaymentCheck_Section">
                            <paymentPreferences:check id="ctlPaymentCheck" runat="server"/>
                        </div>
                        <div id="PaymentPayPal_Section">
                            <paymentPreferences:paypal id="ctlPaymentPayPal" runat="server"/>
                        </div>
                        <div id="PaymentLater_Section">
                            <paymentPreferences:later id="ctlPaymentLater" runat="server"/>
                        </div>
                        </td>
                    </tr>
                </table>
            </div>
              <br />
            <div id="buttonHolder">                
                <asp:LinkButton id="buttonSubmit" runat="server" CssClass="btn"  style="color:#FFFFFF;"  OnClick="buttonSubmit_Click" OnClientClick="removeNonRelevantPaymentInfo();" CausesValidation="true" ValidationGroup="Form"><span>Submit</span></asp:LinkButton>
                <a id="buttonCancel" class="btn" style="color:#FFFFFF;" href="ManageWebsites.aspx"><span>Cancel</span></a>
            </div>
            
            </div>

            <script type="text/javascript">
                $('#PaymentCheck_Section').ready(function() {
                    OnPaymentMethodChange();
                });
                pageLoad = function() {
                    OnPaymentMethodChange();
                };
            </script>

            </ContentTemplate>
        </asp:UpdatePanel>    
        </div>
    </div>
</asp:content>  