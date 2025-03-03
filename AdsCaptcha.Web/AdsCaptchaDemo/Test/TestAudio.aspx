<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TestAudio.aspx.cs" Inherits="Test_TestAudio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SloganHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" Runat="Server">



<div id="audio" style="dispay:none;"></div>

<input id="playSound" type="button" class="btn" value="play" />

<script type="text/javascript">

    $(document).ready(function () {
        $("#playSound").bind("click", function () {

            if ($("#audio").html() == '') {
                var audio = $("<audio />").attr("id", "audioTest").attr("preload", "auto");
                $("<source />").attr("type", "audio/wav").attr("src", "../slider/Speak.ashx").appendTo(audio);
                $("#audio").append(audio);
            }

            $("#audioTest")[0].play();
        });
    });

</script>

</asp:Content>

