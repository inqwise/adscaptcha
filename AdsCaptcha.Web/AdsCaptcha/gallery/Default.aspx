<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="gallery_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
	<meta name="description" content="">
    <title></title>
	
	
	<meta property="og:title" content="Play Video"/>
	  <meta property="og:description" content="Play Video"/>
	  <meta property="og:type" content="movie"/>
	  <meta property="og:video:height" content="270"/>
	  <meta property="og:video:width" content="480"/>
	  <meta property="og:url"  content="http://www.Inqwise.com/gallery?auid=<%=Auid%>"/>
	  <meta property="og:video" content="http://skipr.Inqwise.com/SkipRollPlayer.swf?tag=<%=Auid%>"/>
	  <meta property="og:video:secure_url" content="https://d1f9z24axq1qa6.cloudfront.net/SkipRollPlayer.swf?tag=<%=Auid%>"/>
	  <meta property="og:image" content="http://www.Inqwise.com/playvideo.jpg"/>
	  <meta property="og:video:type" content="application/x-shockwave-flash"/>
	
	
	<style type="text/css">
	html{color:#000;background:#FFF;}body,div,dl,dt,dd,ul,ol,li,h1,h2,h3,h4,h5,h6,pre,code,form,fieldset,legend,input,button,textarea,p,blockquote,th,td{margin:0;padding:0;}table{border-collapse:collapse;border-spacing:0;}fieldset,img{border:0;}address,caption,cite,code,dfn,em,strong,th,var,optgroup{font-style:inherit;font-weight:inherit;}del,ins{text-decoration:none;}li{list-style:none;}caption,th{text-align:left;}h1,h2,h3,h4,h5,h6{font-size:100%;font-weight:normal;}q:before,q:after{content:'';}abbr,acronym{border:0;font-variant:normal;}sup{vertical-align:baseline;}sub{vertical-align:baseline;}legend{color:#000;}input,button,textarea,select,optgroup,option{font-family:inherit;font-size:inherit;font-style:inherit;font-weight:inherit;}input,button,textarea,select{*font-size:100%;}
	body {
	    font-family: arial,sans-serif;
	    font-size: 12px;
	}
	a {
		color: #1B7FCC;
		cursor: pointer;
	}
	h1 {
		overflow: hidden;
		text-overflow: ellipsis;
		white-space: nowrap;
		word-wrap: normal;
		font-size: 24px;
		font-weight: bold;
	}
	.header { height: 122px; }
	.header .logo-container,
	.header .slogan-container { float: left; }
	.footer { height: 300px; }
	.center {
		margin: 0 auto;
		width: 940px;
	}
	.player-container {
		background: #000;
		width: 640px;
		height: 360px;
	}
	
	.video-list-item {
		margin-bottom: 15px;
		position: relative;
	}
	.video-list-item a {
	    color: #333333;
	    display: block;
	    overflow: hidden;
	    padding: 0 5px;
	    position: relative;
		text-decoration: none;
	}
	.video-list-item a:hover {
		
	}
	.video-list-item .thumb {
	    float: left;
	    margin: 0 8px 0 0;
		height: 68px;
	    display: inline-block;
	    overflow: hidden;
	    position: relative;
	}
	.thumb > img {
	    position: relative;
	    Xtop: -11px;
	}
	.video-time {
	    background-color: #000000;
	    color: #FFFFFF !important;
	    display: inline-block;
	    font-size: 11px;
	    font-weight: bold;
	    height: 14px;
	    line-height: 14px;
	    opacity: 0.75;
	    padding: 0 4px;
	    vertical-align: top;
	}
	.video-time {
	    margin-right: 0;
	    margin-top: 0;
	}
	.video-actions, 
	.video-time {
	    bottom: 2px;
	    position: absolute;
	    right: 2px;
	}
	.video-list .video-list-item .title {
	    color: #333333;
	    font-size: 13px;
	    font-weight: bold;
	}
	.video-list-item .title {
	    color: #0033CC;
	    cursor: pointer;
	    display: block;
	    font-size: 1.0833em;
	    font-weight: normal;
	    line-height: 1.2;
	    margin-bottom: 2px;
	    max-height: 2.4em;
	    overflow: hidden;
	}
	.video-list .video-list-item a:hover .title {color: #1b7fcc;}
	.video-list .video-list-item .description, 
	.video-list .video-list-item .stat {
	    color: #999999;
	    font-size: 11px;
	}
	.video-list-item .stat {
	    color: #666666;
	    display: block;
	    font-size: 0.9166em;
	    height: 1.4em;
	    line-height: 1.4em;
	    white-space: nowrap;
	}
	</style>
	
</head>
<body>
    <form id="form1" runat="server">
		
	
   <div class="header">
   		<div class="center" style="padding-top: 26px;">
   			<div class="logo-container">
   				<a title="Inqwise" href="/"><img width="326" src="/images/logo-2.png"></a>
   			</div>
			<div class="slogan-container">
			</div>
   		</div>
   </div>
   <div class="content">
	   <div class="center">
   		   <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
   				<tr>
   					<td valign="top" style="width: 640px;">
   						<div class="player-container">
   							<div id="player"></div>
   						</div>
						<div style="padding: 15px 20px 10px;">
							<h1 class="current-video-title"></h1>
						</div>
						<div>
							
							<br/>
							<br/>
							<br/>
							<br/>
							<br/>
							<br/>
							<br/>
							<br/>
							<br/>
							
						</div>
   					</td>
					<td valign="top">
						<div style="margin-left: 10px;">
							<ul class="video-list"></ul>
						</div>
					</td>
   				</tr>
   			</table>
		</div>
   </div>
   <div class="footer">
   		<div class="center">
   			Copyright &copy; 2014 Inqwise. All rights reserved.<br><a title="Pre-Roll Skip ad" href="/advertiser/prerollskipad.aspx">Pre-Roll Skip ad</a> is Inqwise's Patent Pending.
   		</div>
   </div>
   
   <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
   <script type="text/javascript" src="/scripts/utils/utils.js"></script>
   <script type="text/javascript">
   var ads = [
   		{
   		    id: "d25a3f45-436c-4025-8345-4ce0fb316e4e",
   		    thumb_url: "http://skipr.Inqwise.com/ddc1f6cb-c660-4bf0-9f5b-9fbe07a76a8e/i_852x479x8d1dc2b248ea880.jpg",
   		    title: "Nikon",
   		    duration: "00:00:30",
   		    customer: {
   		        name: "Nikon Poland"
   		    }
   		},
        {
			id : "88b948d8-679b-4b87-b24e-21327ba9691c",
			thumb_url : "http://skipr.Inqwise.com/c3a0f51f-204c-45ad-9924-7caa36908556/i_720x480x8d13e61afd313c0.jpg",
   			title : "Verizon FiOS",
			duration : "00:00:15",
			customer : {
				name : "Verizon"
			}
   		},
   		{
			id : "8745ca25-958a-4d3f-b981-411f50dc250c",
			thumb_url : "http://skipr.Inqwise.com/7715dbb4-7a4d-44cf-8a6b-3031ed97ea1c/i_1280x720x8d131d4d4fd9838.jpg",
   			title : "Comcast XFINITY",
			duration : "00:00:15",
			customer : {
				name : "Comcast"
			}
   		},
   		{
				id : "7e7f3be9-9717-4289-9094-b77eedc4086a",
			thumb_url : "http://skipr.Inqwise.com/cde11d40-9603-4dae-93fc-fa967a2a0b3d/i_1280x720x8d11bb495447b08.jpg",
   			title : "April",
			duration : "00:00:15",
			customer : {
				name : "April Cosmetics"
			}
   		},
   		{
   		    id: "dfa9a911-2561-4be6-aec9-75e8ce315c45",
   		    thumb_url: "http://skipr.Inqwise.com/99841c9c-fce2-4538-9c89-7bc2d2958abb/i_1500x844x8d1670b8336f270.jpg",
   			title : "NOKIA",
			duration : "00:00:30",
			customer : {
				name : "VisionAds"
			}
   		},
   		{
			id: "f76e3b3f-b532-4e2b-bd98-633d60b0523d",
			thumb_url: "http://skipr.Inqwise.com/6fa31439-a2d4-4dc5-af6f-2697e3f82576/i_500x355x8d1699424ea7063.jpg",
			title: "P&G Bounty",
   		    duration: "00:00:15",
   		    customer: {
   		        name: "P&G"
   		    }
   		},
   		{
   		    id: "8DA52E17-FB6E-43CA-B578-F77D9752D582",
   		    thumb_url: "http://skipr.Inqwise.com/1185cc66-0c6c-4277-95bb-230404e46022/i_854x480x8d182b225bcfc3d.jpg",
   		    title: "BMW M series",
   		    duration: "00:00:16",
   		    customer: {
   		        name: "BMW"
   		    }
   		},
   		{
   		    id: "4061652c-f260-46a0-85c7-071632fb7218",
   		    thumb_url: "http://skipr.Inqwise.com/e853d460-8c8e-420d-84da-9fe79c3b8037/i_440x330x8d185c7eac1adb8.jpg",
   		    title: "4.5G LTE ADVANCED",
   		    duration: "00:01:00",
   		    customer: {
   		        name: "Cellcom"
   		    }
   		}
   ];
   
   $(function() {
	   
	   var auid = "<%=Auid%>"; //$.getUrlParam("auid");
	   
	   var currentVideo = null;
	   for(var i = 0; i < ads.length; i++) {
		   if(ads[i].id != auid) {
		   		
			   $("<li class=\"video-list-item\">" +
					"<a href=\"?auid=" + ads[i].id + "\">" +
						"<span class=\"thumb\">" +
							"<img src=\"" + ads[i].thumb_url + "\" width=\"120\" />" + // height=\"90\"
							"<span class=\"video-time\">" + ads[i].duration + "</span>" +
						"</span>" +
						"<span class=\"title\" title=\"" + ads[i].title + "\">" + ads[i].title + "</span>" +
						"<span class=\"stat\">by <b>" + ads[i].customer.name + "</b></span>" +
					"</a>" +
				"</li>").appendTo("ul.video-list");
				
		   } else {
			   currentVideo = ads[i];
		   }
	   }
	   
	   if(currentVideo != null) {
		   	document.title = currentVideo.title;
	   		$(".current-video-title").text(currentVideo.title);
	   } else {
		   document.title = "Untitled";
		   $(".current-video-title").text("Untitled");
	   }
	   
	   if(auid != "") {
		   
		   $("#player").empty();
			$("<iframe src=\"http://skipad.test.Inqwise.com/FW/SkipRollTagDemo.html?srqatnocache=true&pid=91A3-883&tagurl=http%3A%2F%2Fskipc1.Inqwise.com%2Ftag%3Fauid%3D" + auid + "%26otp%3Dvast&width=640&height=360\" width=\"640\" height=\"360\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#player");
	   		
	   } else {
		   console.log("Ad id not set");
	   }
		
   });
   </script>
	
	
    </form>
</body>
</html>
