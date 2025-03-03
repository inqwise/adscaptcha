<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="LandingDemo_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<style type="text/css">
.description table td
{
	padding:0 10px 8px 0;
}
</style>
</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleHolder">
Edit demo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="description">
<asp:Button ID="btnPreview" runat="server" Text="View this demo"   
        CssClass="btn" onclick="btnPreview_Click"/>
   </div>
   <br />
<h4>Demo details</h4>
<div class="description">
  <table>
     <tr>
        <td colspan="2">
            <asp:Label ID="lblError" runat="server" BackColor="#FF99FF" BorderColor="Red" 
                BorderStyle="Solid" BorderWidth="1px" EnableViewState="False" Font-Bold="True" 
                ForeColor="#CC0000" Text="Error! Try again later..." Visible="False" 
                Width="100%"></asp:Label>
                <asp:Label ID="lblSuccess" runat="server" BackColor="#66FF99" BorderColor="#003300" 
                BorderStyle="Solid" BorderWidth="1px" EnableViewState="False" Font-Bold="True" 
                ForeColor="#009900" Text="Demo is updated successfully" Visible="False" 
                Width="100%"></asp:Label>

         </td>
    </tr>
    <tr>
        <td style="white-space:nowrap;">Demo name: </td>
        <td>
            <asp:Label ID="lblName" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="white-space:nowrap;">Demo description :</td>
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
        <td style="white-space:nowrap;">Add new image: </td>
        <td>
            <asp:FileUpload ID="uploadImage" runat="server"  CssClass="InputField"/>
        </td>
    </tr>
    <tr>
     <br /><br />
        <td colspan="2" style="white-space:nowrap;" align="right">
            <asp:Button ID="btnSubmit" runat="server" Text="Update"  CssClass="btn"  
                onclick="btnSubmit_Click" />
        </td>
    </tr>
</table>
</div>
 <br /><br />
<h4>Images</h4>
<div class="description">
  <table>
      
            <asp:Repeater ID="rptImage" runat="server" 
          onitemcommand="rptImage_ItemCommand" onitemdatabound="rptImage_ItemDataBound">
            <ItemTemplate> <tr>
                 <td><asp:Image ID="img" runat="server" Width="180" Height="150" /> </td>
                 <td>
                    <asp:Button runat="server" ID="btnDelete" Text="Delete image" CssClass="btn" CommandName="DeleteImage" />
                 </td> </tr>
            </ItemTemplate>
            </asp:Repeater>
        
   
 </table>
 </div>
</asp:Content>

