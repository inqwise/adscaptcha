<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Demos.aspx.cs" Inherits="LandingDemo_Demos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleHolder">
Custom demos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="description">
    <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="White"
        NavigateUrl="~/LandingDemo/Add.aspx" CssClass="btn">Create new</asp:HyperLink>
</div>
<br />
<br />
<div class="description">

    <asp:GridView ID="gvDemos" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" 
        DataKeyNames="DemoID,DemoUrlName" DataSourceID="SqlDataSource1" onrowcommand="gvDemos_RowCommand" 
        onselectedindexchanged="gvDemos_SelectedIndexChanged" PageSize="20" 
        BorderWidth="0px" CellPadding="0" CssClass="table" GridLines="None" 
        ShowFooter="True" Width="700px">
        <AlternatingRowStyle BorderWidth="0px" />
        <Columns>
            <asp:ButtonField CommandName="Preview" Text="Preview" />
            <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
            <asp:BoundField DataField="DemoID" HeaderText="DemoID" ReadOnly="True" 
                SortExpression="DemoID" Visible="False" />
            <asp:BoundField DataField="DemoName" HeaderText="DemoName" 
                SortExpression="DemoName" />
            <asp:BoundField DataField="DemoUrlName" HeaderText="DemoUrlName" 
                SortExpression="DemoUrlName" Visible="False" />
            <asp:BoundField DataField="DemoDescription" HeaderText="DemoDescription" 
                SortExpression="DemoDescription" />
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="IsDeleted" 
                SortExpression="IsDeleted" Visible="False" ReadOnly="True" />
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" 
                SortExpression="CreatedBy" Visible="False" />
            <asp:BoundField DataField="InsertDate" HeaderText="InsertDate" 
                SortExpression="InsertDate" Visible="False" />
            <asp:ButtonField CommandName="DeleteDemo" Text="Delete" />
        </Columns>
        <FooterStyle BorderWidth="0px" CssClass="tr footer" ForeColor="White" />
        <HeaderStyle BorderWidth="0px" CssClass="top tr header" ForeColor="White" />
        <RowStyle BorderWidth="0px" CssClass="tr" HorizontalAlign="Center" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AdsCaptcha_DemoConnectionString %>" 
        SelectCommand="SELECT * FROM [T_DEMOS] WHERE ([IsDeleted] = @IsDeleted) ORDER BY [InsertDate] DESC">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="IsDeleted" Type="Boolean" />
        </SelectParameters>
    </asp:SqlDataSource>

</div>
</asp:Content>

