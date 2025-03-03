<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PressInquires.ascx.cs" Inherits="Inqwise.AdsCaptcha.Admin.UserControls.PressInquires" %>
<asp:GridView ID="gvInquires" runat="server" AllowPaging="True" 
    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
    GridLines="None" onpageindexchanging="gvInquires_PageIndexChanging" 
    Width="100%">
    <RowStyle BackColor="#E3EAEB" Height="40px" HorizontalAlign="Center" />
    <Columns>
        <asp:BoundField DataField="InquireID" HeaderText="InquireID" Visible="False" />
        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
        <asp:BoundField DataField="Company" HeaderText="Company" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="Message" HeaderText="Message" />
        <asp:BoundField DataField="InsertDate" HeaderText="Date" />
    </Columns>
    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#E8F8F7" ForeColor="#45BBB4" Height="40px" 
        HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#E8F8F7" Font-Bold="True" ForeColor="#45BBB4" 
        Height="40px" />
    <EditRowStyle BackColor="#7C6F57" />
    <AlternatingRowStyle BackColor="White" Height="40px" />
</asp:GridView>
