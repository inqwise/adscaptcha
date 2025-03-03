<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="LandingDemo_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<style type="text/css">
.description table td
{
	padding:10px 20px 10px 0;
}
</style>
</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleHolder">
Add new demo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h4>Demo details</h4>
<div class="description">
    <table>
     <tr>
        <td colspan="2">
            <asp:Label ID="lblError" runat="server" BackColor="#FF99FF" BorderColor="Red" 
                BorderStyle="Solid" BorderWidth="1px" EnableViewState="False" Font-Bold="True" 
                ForeColor="#CC0000" Text="Error! Try again later..." Visible="False" 
                Width="100%"></asp:Label>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                BackColor="#FF99FF" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px" 
                DisplayMode="List" Font-Bold="True" ForeColor="Red" Width="100%" />

         </td>
    </tr>
    <tr>
        <td style="white-space:nowrap;">Demo name: <asp:RequiredFieldValidator 
                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" 
                Display="Dynamic" ErrorMessage="Demo name is mandatory" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" CssClass="InputField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="white-space:nowrap;">Demo description: </td>
        <td>
            <textarea id="txtDesc" name="txtDesc" style="width:200px;height:100px;" runat="server" CssClass="TextareaField"></textarea></td>
    </tr>
    <tr>
        <td style="white-space:nowrap;">Click url: </td>
        <td>
            <asp:TextBox ID="txtClick" runat="server" CssClass="InputField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="white-space:nowrap;">Like url: </td>
        <td>
            <asp:TextBox ID="txtLike" runat="server" CssClass="InputField"></asp:TextBox>
        </td>
    </tr>
     <tr>
        <td style="white-space:nowrap;">Dimentions: </td>
        <td>
            <asp:DropDownList ID="ddlDimentions" runat="server" Height="16px" Width="209px">
                <asp:ListItem Value="1">300x250</asp:ListItem>
                <asp:ListItem Value="2">180x150</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
     <tr>
        <td style="white-space:nowrap;">Deformation</td>
        <td>
            
            <asp:DropDownList ID="ddlDeformations" runat="server" Height="16px" Width="209px">
                <asp:ListItem Value="1">Twist</asp:ListItem>
                <asp:ListItem Value="3">Tile</asp:ListItem>
                <asp:ListItem Value="2">Polar</asp:ListItem>
                <asp:ListItem Value="4">Frosted glass</asp:ListItem>
                <asp:ListItem Value="5">Radial blur</asp:ListItem>
                <asp:ListItem Value="6">Motion blur</asp:ListItem>
            </asp:DropDownList>
            
        </td>
    </tr>
    <tr>
        <td style="white-space:nowrap;">Demo image<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Demo image is mandatory" ControlToValidate="uploadImage" 
                Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator> :
        </td>
        <td>
            <asp:FileUpload ID="uploadImage" runat="server"  CssClass="InputField"/>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="white-space:nowrap;">
        <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Save"  CssClass="btn"  
                onclick="btnSubmit_Click" />
        </td>
    </tr>
</table>
</div>
</asp:Content>

