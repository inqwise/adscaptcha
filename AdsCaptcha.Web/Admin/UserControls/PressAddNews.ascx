<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PressAddNews.ascx.cs" Inherits="Inqwise.AdsCaptcha.Admin.UserControls.PressAddNews" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<style type="text/css">
#tableAdd td
{
	padding-bottom:30px;
}

#tableAdd .header
{
	font-weight:bold;
	}
    #TextArea1
    {
        height: 111px;
        width: 690px;
    }
</style>
<table id="tableAdd" style="width:100%;">
<tr>
        <td colspan="2"><asp:Label ID="lblMessage" runat="server"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </td>

    </tr>
    <tr>
        <td class="header">
            Title<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtTitle" ErrorMessage="Title is required">*</asp:RequiredFieldValidator></td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" Width="580px" 
                ValidationGroup="AddNews"></asp:TextBox>
        </td>

    </tr>
    <tr>
        <td class="header">
            Summary<asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                runat="server" ControlToValidate="txtSummary" 
                ErrorMessage="Summary is required">*</asp:RequiredFieldValidator></td>
        <td>
            
            <textarea id="txtSummary" runat="server" name="S1"></textarea></td>

    </tr>
    <tr>
        <td class="header">
            Body<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="FreeTextBox1" ErrorMessage="Body is required">*</asp:RequiredFieldValidator></td>
        <td>
           <FTB:FreeTextBox id="FreeTextBox1" runat="server" BaseUrl="/FreeTextBox/" 
                Height="250px" ImageGalleryPath="~/FreeTextBox/images/" 
                SupportFolder="/FreeTextBox/" Width="700px"  />  
           
           
           </td>

    </tr>
    <tr>
        <td class="header">
            Source Name</td>
        <td>
            <asp:TextBox ID="txtSource" runat="server" Width="580px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="header">
            Source Url</td>
        <td>
            <asp:TextBox ID="txtSourceUrl" runat="server" Width="580px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;</td>

    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
        </td>
      
    </tr>
</table>
