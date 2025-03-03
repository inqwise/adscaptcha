<%@ Page EnableViewStateMac="false" Title="Contact Us | Inqwise" Language="C#" MasterPageFile="AdsCaptcha.Master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="Inqwise.AdsCaptcha.ContactUs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
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
   </style>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
id="body" class="page"
</asp:Content>
<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginContent" runat="server">
    
	<div>
		<ul class="menu-top">
			<li><a href="aboutus.aspx" title="About Us">About Us</a></li>
			<li><a href="products.aspx" title="Products">Products</a></li>
		</ul>
	</div>
	<div class="menu-lobby-container">	
		<ul class="menu-lobby">
			<li><a href="advertiser/StartPage.aspx" title="Advertisers" class="button-green"><span>Advertisers</span></a></li>
			<li><a href="publisher/StartPage.aspx" title="Site Owners" class="button-green"><span>Site Owners</span></a></li>
		</ul>
	</div>
	
</asp:Content>
<asp:content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="content">

  <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
        
		<div class="breadcrumbs">
			<a href="/" title="Home">Home</a>&nbsp;&rsaquo;&nbsp;Contact Us
		</div>
	    <div class="inner-wrapper">
			<div class="inner-content">
				<div class="page-heading-wrapper">
					<h1>Contact Us</h1>
				</div>
				<div class="inner-content-left-side">
		            
					<h4>Send Us a Mail</h4>
		            <div class="desc">
		            	Contact our support service at <a href='mailto:support@Inqwise.com'>support@Inqwise.com</a>.<br/><br/>
		                <b>Our Address</b><br />
		                New York  Office<br/>
						2373 Broadway, Suite 1024<br/>
						New York, NY 10024<br/>
						Phone: 646 709 3654
		                <br /><br />
						Tel Aviv Office<br/> 
						46 Montefiore Street,<br/> 
						Tel Aviv<br/>
						Phone: +972 3 562 8669<br/>
						Fax: +972 3 561 9148
		            </div>
					
				</div>
				<div class="inner-content-right-side">
					
		            <h4>Send Us a Message</h4>
		            <div class="desc">
		            <asp:Panel ID="panelContactForm" runat="server">
		            	We look forward to answering any question or inquiry you might have. <br />
		                For a fast response, please be as specific as possible so we can navigate your question to the proper department.<br /><br />
	                    
		                    <table>
		                        <tr>
		                            <td class="FieldHeader">Name:*</td>
		                            <td>
		                                <asp:TextBox ID="textName" runat="server" CssClass="InputField" MaxLength="50" Text="" />
		                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="textName" ValidationGroup="Form"
		                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
		                            </td>
		                        </tr>
		                        <tr>
		                            <td class="FieldHeader">E-mail:*</td>
		                            <td>
		                                <asp:TextBox ID="textEmail" runat="server" CssClass="InputField" MaxLength="100" Text="" />
		                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" SetFocusOnError="true"
		                                    ControlToValidate="textEmail" ValidationGroup="Form" 
		                                    ValidationExpression=".*@.*\..*"
		                                    CssClass="ValidationMessage" ErrorMessage="* Not a valid e-mail"
		                                    Display="dynamic" />
		                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="textEmail" ValidationGroup="Form"
		                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
		                            </td>
		                        </tr>
		                        <tr>
		                            <td class="FieldHeader">Phone:</td>
		                            <td>
		                                <asp:textbox ID="textPhone" runat="server" CssClass="InputField" MaxLength="30" Text="" />
		                                <AjaxControlToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="textPhone" ValidChars="+-0123456789" />
		                            </td>
		                        </tr>
		                        <tr>
		                            <td class="FieldHeader">Subject:*</td>
		                            <td>
		                                <asp:DropDownList ID="listSubject" runat="server" CssClass="SelectField" DataTextField="Item_Desc" DataValueField="Item_Id" ValidationGroup="Form" />
		                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="listSubject" ValidationGroup="Form"
		                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
		                                <asp:RangeValidator ID="RangeValidator1" runat="server" 
		                                    ControlToValidate="listSubject" CssClass="ValidationMessage" Display="Dynamic"  ValidationGroup="Form"  SetFocusOnError="true"
		                                    ErrorMessage="* Required" MaximumValue="9999999" MinimumValue="1"></asp:RangeValidator>
		                            </td>
		                        </tr>
		                        <tr>
		                            <td class="FieldHeader">Message:*</td>
		                            <td>
		                                <asp:TextBox ID="textMessage" runat="server" CssClass="TextareaField" TextMode="MultiLine" Text="" />
		                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="textMessage" ValidationGroup="Form"
		                                    CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
		                            </td>
		                        </tr>
		                        <tr>
		                            <td></td>
		                            <td>
		                                <br />
		                                <div class="ButtonHolder">                
		                                    <asp:LinkButton id="buttonSubmit" style="color:#FFFFFF;" Text="Send"  CssClass="btn" runat="server" OnClick="buttonSubmit_Click" CausesValidation="true" ValidationGroup="Form"></asp:LinkButton>
		                                    <a id="buttonCancel" class="btn" href="Index.aspx" style="color:#FFFFFF;"><span>Cancel</span></a>
		                                </div>
		                            </td>
		                        </tr>
		                    </table>
	
		                    </asp:Panel>
	
		                    <asp:Panel ID="panelMessageSent" runat="server">
		                        Thanks.
		                        <br />
		                        Your request has been sent successfully.</asp:Panel>
	            
		            </div>
					
				</div>
	         </div>
	    </div>
	    
	</div>
    
    
    

        </ContentTemplate>
    </asp:UpdatePanel>                
</asp:content>