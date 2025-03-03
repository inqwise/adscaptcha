<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Videos.aspx.cs" Inherits="Demos_Videos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<style type="text/css">
h5
{
	background: none repeat scroll 0 0 #5B5757;
    border-radius: 10px 10px 10px 10px;
    color: #FFFFFF;
    display: inline;
    font-family: "oswaldregular";
    font-size: 20px;
    font-weight: 400;
    padding: 0 10px;
    text-transform: uppercase;
}
</style>
<script type="text/javascript" src="../Scripts/swfobject.js"></script>
<script type="text/javascript">
    var Videos = [{ url: '', id:'9KB6IgQE7g8', title: 'Slide to Fit captcha', thumbnail: '', width:700, height:428}];
    function LoadVideos() {
        $.each(Videos, function(i, value){
            //120x90
                $("<div />").attr("title", this.title)
                .attr("style", "width:120px;height:90px;cursor:pointer;background-image:url('" + this.thumbnail + "');border:0;float:left;margin:5px;")
                .appendTo("#videoMenu")
                .bind("click", function(){
                    PlayVideo(i);
                });       
        });
    }
    function PlayVideo(n) {
        $("#videoContainer").html("");
        //swfobject.embedSWF(Videos[n].url, "videoContainer", Videos[n].width, Videos[n].height, "9", null, null);

        var yurl = "http://www.youtube.com/embed/" + Videos[n].id + "?html5=1&autoplay=1";
        $("<iframe />").attr("class", "youtube-player")
                        .attr("type", "text/html")
                        .attr("width", Videos[n].width)
                        .attr("height", Videos[n].height)
                        .attr("frameborder", "0")
                        .attr("src", yurl)
                        .appendTo($("#videoContainer"));
        //<iframe class="youtube-player" type="text/html" width="640" height="385" src="http://www.youtube.com/embed/" frameborder="0"> </iframe>

        $("#videoTitle").html(Videos[n].title);
    }
    $(document).ready(function () {

        LoadVideos();
        PlayVideo(<%= StartVideoNo%>)
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleHolder" Runat="Server">
<span id="videoTitle"></span>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SloganHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" Runat="Server">
<div style="min-height:435px;">
<div id="videoContainer""></div>
</div>
<div class="description">
 <br />
<div id="videoMenu" style="width:600px;height:100px;">
 </div>

</div>
</asp:Content>

