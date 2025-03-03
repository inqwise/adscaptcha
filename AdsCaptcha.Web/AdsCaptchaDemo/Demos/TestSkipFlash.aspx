<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TestSkipFlash.aspx.cs" Inherits="Demos_TestSkipFlash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script type="text/javascript" src="../Scripts/swfobject.js"></script>
<script type="text/javascript">
    swfobject.embedSWF("../DemoFiles/product_04_003.swf", "demoContent", "800", "700", "11.0.0", "expressInstall.swf");
</script>
</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleHolder">
Skip PreRoll Ad demo

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<div id="flashContainer" style="width:700px;height:950px;overflow:hidden;border:0;margin:-115px 0 0 -40px;">
	<div id="demoContent">
		<h1>Alternative content</h1>
		<p><a href="http://www.adobe.com/go/getflashplayer"><img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash player" /></a></p>
	</div>
</div>
</asp:Content>

