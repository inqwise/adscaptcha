<%@ Page EnableViewStateMac="false" Title="Home | Inqwise" Language="C#" MasterPageFile="AdsCaptcha.Master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Inqwise.AdsCaptcha.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>


<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginContent" runat="server">
		
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">id="home"</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="content">
	<div class="inner-content-wide">
		<div class="breadcrumbs"></div>
		<div class="landing">
			<div class="landing-box">
				<div class="landing-box-header">
					<h2>Pre-Roll Skip Ad&trade;</h2>
					<p>Enhance engagement. Increase ROI.</p>
				</div>
				<div class="landing-box-video">
					<div id="thumb_image_container">
						<div id="thumb_image"></div>
						<div id="thumb_image_play"></div>
						<div id="thumb_image_stop"></div>
					</div>
				</div>
				<div class="landing-box-footer">
					<ul>
						<li><a href="/advertiser/prerollskipad.aspx" title="Advertiser">Advertiser</a></li><li><a href="/publisher/prerollskipad.aspx" title="Site Owner">Site Owner</a></li>
					</ul>
					
					<!--
					<div class="landing-box-footer-left">
						<a href="/advertiser/prerollskipad.aspx" title="For Advertisers"><h3>For Advertisers &gt;&gt;</h3></a>
						<h3>Advertiser</h3>
						<ul>
							<li><a href="contactus.aspx" title="Contact sales">Contact sales</a></li><li><a href="/advertiser/prerollskipad.aspx" title="Read more">Read more</a></li>
						</ul>
					</div>
					<div class="landing-box-footer-right">
						<a href="publisher/prerollskipad.aspx" title="For Site Owners"><h3>For Site Owners &gt;&gt;</h3></a>
						<h3>Site Owner</h3>
						<ul>
							<li><a href="contactus.aspx" title="Contact sales">Contact sales</a></li><li><a href="publisher/prerollskipad.aspx" title="Read more">Read more</a></li>
						</ul>
					</div>
					-->
					
				</div>
			</div>
			<div class="landing-box last-item">
				<div class="landing-box-header">
					<h2>Sliding Captcha&trade;</h2>
					<p>Secure your site. Improve user experience.</p>
				</div>
				<div class="landing-box-video">
					<div id="thumb_image_container_captcha">
						<div id="thumb_image_captcha"></div>
						<div id="thumb_image_play_captcha"></div>
						<div id="thumb_image_stop_captcha"></div>
					</div>
				</div>
				<div class="landing-box-footer">
					<ul>
						<li><a href="/advertiser/slidingcaptcha.aspx" title="Advertiser">Advertiser</a></li><li><a href="/publisher/slidingcaptcha.aspx" title="Site Owner">Site Owner</a></li>
					</ul>
					
					<!--
					<div class="landing-box-footer-left">
						<a href="/advertiser/slidingcaptcha.aspx" title="For Advertisers"><h3>For Advertisers &gt;&gt;</h3></a>
						<h3>Advertiser</h3>
						<ul>
							<li><a href="advertiser/signup.aspx" title="Start Campaign">Start Campaign</a></li><li><a href="/advertiser/slidingcaptcha.aspx" title="Read more">Read more</a></li>
						</ul>	
					</div>
					<div class="landing-box-footer-right">
						<a href="publisher/slidingcaptcha.aspx" title="For Site Owners"><h3>For Site Owners &gt;&gt;</h3></a>
						<h3>Site Owner</h3>
						<ul>
							<li><a href="publisher/signup.aspx" title="Get Captcha">Get Captcha</a></li><li><a href="publisher/slidingcaptcha.aspx" title="Read more">Read more</a></li>
						</ul>
					</div>
					-->
						
				</div>
			</div>
		</div>
		
	</div>
</div>
    
    
    	<script type="text/javascript">
		var isFlashInstalled = false;
		var defaultIndex = 0; // 0
		var last = null;
		
		var videos = [
			{ src : "", html5 : "" },
			{ src : "", html5 : "" },
			{ src : "", html5 : "" }
		];
		
		$("#thumb_image_play").click(function() {
			$(this).hide();
			$("#thumb_image").empty();
			
			
			// http://skipad.test.Inqwise.com/FW/SkipRollTagDemo.html?srqatnocache=true&pid=91A3-883&tagurl=http%3A%2F%2Fskipc1.Inqwise.com%2Ftag%3Fauid%3D7de074e7-b973-4f19-a15e-93d14388927b%26otp%3Dvast&width=640&height=360
			
			$("<iframe src=\"http://skipad.test.Inqwise.com/FW/SkipRollTagDemo_512x288.html?srqatnocache=true&pid=91A3-883&tagurl=http%3A%2F%2Fskipc1.Inqwise.com%2Ftag%3Fauid%3D7e7f3be9-9717-4289-9094-b77eedc4086a%26otp%3Dvast&width=640&height=360\" width=\"512\" height=\"288\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#thumb_image");
			
			$("#thumb_image_stop").show();
			
		});
		
		$("#thumb_image_stop").click(function() {
			$(this).hide();
			$("#thumb_image").empty();
			$("#thumb_image_play").show();
		});
		
		
		// captcha
		$("#thumb_image_play_captcha").click(function() {
			$(this).hide();
			$("#thumb_image_captcha").empty();
			
			
			$("<iframe src=\"\" width=\"512\" height=\"288\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#thumb_image_captcha");
			
			$("#thumb_image_stop_captcha").show();
			
		});
		
		$("#thumb_image_stop_captcha").click(function() {
			$(this).hide();
			$("#thumb_image_captcha").empty();
			$("#thumb_image_play_captcha").show();
		});
		
		
		
		
		
		
		
		var setVideo = function(index, autoplay) {
		
			$("#video_frame_1, #video_frame_2").empty();
			
			
			
			
			
			
			/*
			// set video iframe
			$("<iframe src=\"" + (isFlashInstalled ? videos[0].src : videos[0].html5) + "\" width=\"712\" height=\"398\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#video_frame_1");
			
			$("<iframe src=\"" + (isFlashInstalled ? videos[1].src : videos[1].html5) + "\" width=\"712\" height=\"398\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#video_frame_2");
			*/
		
			/*
			if(videos[index].custom != undefined) {
			
				if(!autoplay) {
				
					var custom = $("<div id=\"thumb_image_container_custom\">" +
						"<div id=\"thumb_image_custom\"></div>" +
						"<div id=\"thumb_image_play_custom\"></div>" +
						"<div id=\"thumb_image_stop_custom\"></div>" +
					"</div>").appendTo("#video_frame");
					
					custom.find("#thumb_image_play_custom")
					.unbind("click")
					.bind("click", function() {
			
						$(this).hide();
					
						custom.find("#thumb_image_custom").empty();
						$("<iframe src=\"" + (isFlashInstalled ? videos[index].src : videos[index].html5 + "?autoplay=1") + (autoplay ? "?autoplay=1" : "") + "\" width=\"712\" height=\"398\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo(custom.find("#thumb_image_custom"));
						
						custom.find("#thumb_image_stop_custom").show();
						
					});
					
					custom.find("#thumb_image_stop_custom")
					.unbind("click")
					.bind("click", function() {
						$(this).hide();
						
						custom.find("#thumb_image_custom").empty();
						custom.find("#thumb_image_play_custom").show();
						
					});
		
				} else {
				
					// set video iframe
					$("<iframe src=\"" + (isFlashInstalled ? videos[index].src : videos[index].html5) + (autoplay ? "?autoplay=1" : "") + "\" width=\"712\" height=\"398\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#video_frame");
					
				}
			
			} else {
		
				// set video iframe
				$("<iframe src=\"" + (isFlashInstalled ? videos[index].src : videos[index].html5) + (autoplay ? "?autoplay=1" : "") + "\" width=\"712\" height=\"398\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>").appendTo("#video_frame");
			
			}
			*/
			
			
		};
		
		var j = function () {
            var c = false;
            u = 10;
            h = "0.0";
            if (navigator.plugins && navigator.plugins.length) {
                for (x = 0; x < navigator.plugins.length; x++) {
                    if (navigator.plugins[x].name.indexOf('Shockwave Flash') != -1) {
                        h = navigator.plugins[x].description.split('Shockwave Flash ')[1];
                        c = true;
                        break;
                    }
                }
            } else if (window.ActiveXObject) {
                for (x = 2; x <= u; x++) {
                    try {
                        oFlash = eval("new ActiveXObject('ShockwaveFlash.ShockwaveFlash." + x + "');");
                        if (oFlash) {
                            c = true;
                            h = x + '.0';
                        }
                    } catch (e) {}
                }
            }
            return {
                isInstalled: c,
                version: h
            }
        };
		
		$(function() {
			
			isFlashInstalled = j().isInstalled;
			
			//setVideo();
			
			
			/*
			$("#label_searved").html('178,269,267');
			$.ajax({
            	url: "Async/CommonStat.ashx?type=requests",
            	success: function(data) {
                	$("#label_searved").html(data);
            	}
        	});
			
			$(".buttons .button").each(function(i, el) {
				if(defaultIndex == i) {
					$(el).addClass("active");
					last = $(el);
					
					// set video iframe
					setVideo(i, false);
					
				}
				$(el).attr({ "video" : i }).click(function() {
					if(last != null && last != $(this)) {
						$(last).removeClass("active");
					}
					last = $(this);
					
					$(this).addClass("active");
					
					// set video iframe
					setVideo($(this).attr("video"), true);
					
					
				});
				
			});
			*/
			
		});
		</script>


</asp:Content>