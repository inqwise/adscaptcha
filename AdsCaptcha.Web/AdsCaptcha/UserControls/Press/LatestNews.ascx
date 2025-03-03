<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LatestNews.ascx.cs" Inherits="Inqwise.AdsCaptcha.UserControls.Press.LatestNews" %>

<div class="description">
<asp:Repeater ID="rptNews" runat="server" 
    onitemdatabound="rptNews_ItemDataBound">
<ItemTemplate>
 <h4 id="h5Header" runat="server"><asp:Literal ID="lblHeader" runat="server"></asp:Literal></h4>
 <div class="Explanation"><asp:Literal ID="lblDate" runat="server"></asp:Literal></div><br />
 <asp:Label ID="lblSummary" runat="server"></asp:Label><br /><br />
 <a href="" target="_blank" id="aPDF" runat="server" style="float:left;"><img src="/Images/pdf.png" style="border:0;" /></a>
  &nbsp;&nbsp;<a href="" target="_blank" id="aSource" runat="server" style="float:left;"></a>
  <br /><br />
</ItemTemplate>
</asp:Repeater>

</div>
