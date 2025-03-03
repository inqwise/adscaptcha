<%@ Control Language="C#" AutoEventWireup="true" CodeFile="News.ascx.cs" Inherits="Inqwise.AdsCaptcha.Admin.UserControls.News" %>
<style type="text/css">
.description {
    font-family: "bitterregular";
    font-size: 18px;
    height: auto;
    line-height: 27px;
    padding-bottom: 20px;
}
h5 {
    background: none repeat scroll 0 0 #5B5757;
    border-radius: 10px 10px 10px 10px;
    clear: both;
    color: #FFFFFF;
    display: block;
    font-family: "oswaldregular";
    font-size: 20px;
    font-weight: 400;
    margin-top: 10px;
    padding: 0 10px;
    text-transform: uppercase;
    width: 70%;
}

h5 a
{
	color:#fff;
	text-decoration:undderline;
	float:right;
}

</style>
<div class="description">
<asp:Repeater ID="rptNews" runat="server" 
    onitemdatabound="rptNews_ItemDataBound">
<ItemTemplate>
 <h5 style=""><asp:Literal ID="lblHeader" runat="server"></asp:Literal> <a href="" id="aEdit" runat="server">Edit news</a> </h5><br />
 <asp:Literal ID="lblSummary" runat="server"></asp:Literal><br /><br />
 <a href="" target="_blank" id="aPDF" runat="server" style="float:left;"><img src="<%=ConfigurationManager.AppSettings["URL"] %>Images/pdf.png" style="border:0;" /></a>
  <a href="" target="_blank" id="aSource" runat="server" style="float:left;"></a>
  <br />
</ItemTemplate>
</asp:Repeater>

</div>