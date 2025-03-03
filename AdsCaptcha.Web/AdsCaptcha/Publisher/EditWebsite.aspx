<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Publisher/PublisherAccount.Master" AutoEventWireup="true" CodeFile="EditWebsite.aspx.cs" Inherits="Inqwise.AdsCaptcha.Publisher.EditWebsite" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<script type="text/javascript" src="/scripts/utils/utils.js"></script>
<!--[if lt IE 8]>
<script type="text/javascript" src="/scripts/utils/json2.js"></script>
<![endif]-->
<script type="text/javascript" src="/scripts/validator/validator-1.2.8.js"></script>


<style type="text/css">
table td {
	white-space:nowrap;
	font-size:14px;
}
table td div {
	vertical-align:top;
	white-space:normal;
	font-size:11px;
}

.container {
    padding: 40px 0;
}
#content .inner-content {
    margin: 0 auto;
    width: 960px;
}

/*
#content .inner-content a {
	color: #19C12D;
}
*/
#content .description {
    font-family: arial,helvetica,sans-serif;
    font-size: 14px;
    line-height: 18px;
}

#image_preview_lightbox {
	background: #fff;
	position: absolute;
	top: 0;
	left : 0;
	padding: 4px;
	border: 1px solid #ccc;
	border:1px solid rgba(0, 0, 0, .45);
	border-bottom:1px solid #666;
	-moz-box-shadow: 0 3px 8px rgba(0, 0, 0, .3);
	-webkit-box-shadow: 0 3px 8px rgba(0, 0, 0, .3);
	box-shadow:0 3px 8px rgba(0, 0, 0, .3);
}
#image_preview_lightbox a {
	display: inline-block;
	background-color: #fff;
	border: 1px solid #aaa;
	color: #000;
	position: absolute;
	top: 8px;
	right: 8px;
	font-family: arial;
	font-size: 12px;
	padding: 3px 6px;
	-webkit-border-radius:3px;
	-moz-border-radius:3px;
	border-radius:3px;
	cursor: pointer;
	-moz-box-shadow: 0 1px 3px rgba(0, 0, 0, .3);
	-webkit-box-shadow: 0 1px 3px rgba(0, 0, 0, .3);
	box-shadow:0 1px 3px rgba(0, 0, 0, .3);
}
#image_preview_lightbox a:hover {
	background-color: #f1f1f1;
}

#list_house_ads {  list-style-type: none; padding: 0px !important; margin: 0px !important; }
#list_house_ads li {
	border: 1px solid #EEEEEE;
	background-color: #fbfbfb;
	margin-bottom: 10px;
	padding: 4px;
	position: relative;
	overflow: hidden;
}
#list_house_ads li .image-block {
	width: 100px; 
	height: 83px; 
	border: 1px solid #ccc; 
	float: left; 
	background-color: #fff;
}
#list_house_ads li:hover .image-block {
	border-color: #666;
}
#list_house_ads li .actions {
	position: absolute;
	top: 4px;
	right: 4px;
}
.status {
    color: #BA0A1C !important;
    padding: 3px 0 10px;
}
.small-button {
	cursor: pointer;
	Xdisplay: inline-block;
	border: 1px solid #61AE28;
	background-color: #9AE461;
	background-image:-moz-linear-gradient(top, #9AE461, #65C900);
	background-image:-webkit-gradient(linear, left top, left bottom, from(#9AE461), to(#65C900));
	background-image:-webkit-linear-gradient(#9AE461, #65C900);
	background-image:-o-linear-gradient(#9AE461, #65C900);
	background-image:linear-gradient(top, #9AE461, #65C900);
	_filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#9AE461', endColorstr='#65C900');
	border: 1px solid #61AE28;
	-webkit-border-radius:5px;
	-moz-border-radius:5px;
	border-radius:5px;
	color: #ffffff !important;
	Xdisplay: block;
	Xfloat: left;
	Xdisplay: inline-block;
	font-family: 'oswaldregular';
	height: 22px;
	line-height: 22px;
	overflow: hidden;
	text-align: center;
	font-size:14px;
	text-shadow: 0 1px 0 #61AE28;
	padding-left: 10px;
	padding-right: 10px;
}

.params {
	clear:both;
	min-height:40px;
	padding-bottom:6px;
}
.params .param-name {
	width: 160px;
    min-height: 40px;
    color: #333;
    float: left;
    line-height: 39px;
    Xfont-size: 14px;
}
.params .param-value {
    min-height: 40px;
    float: left;
   	Xmax-width: 320px;
}

.item-params { clear: both; overflow: hidden;}
.item-params .item-param-name { width: 130px; float: left;}
.item-params .item-param-value { width: 300px; float: left;}


#upload_image_preview img {
	width: 100px;
	height: 83px;
}
</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Edit Website
</asp:Content>
  
<asp:content ContentPlaceHolderID="MainContent" runat="server">
<div id="content"  class="container">
    
   <div class="inner-content">
	   
	   
            <br />
        <!--div id="breadCrambs">
            <asp:Label ID="labelNavigationPath" runat="server" />        
        </div-->

        
            <h4>Website Information</h4>
            <div class="description">
                 
				 
				<div class="params">
    				<div class="param-name">Website Url:*</div>
    				<div class="param-value">
						
						http://<asp:Label ID="labelUrl" runat="server" />
						
					</div>
				</div>
				<div class="params">
    				<div class="param-name">Status:*</div>
    				<div class="param-value">
                        <asp:DropDownList ID="listStatus" CssClass="SelectField" runat="server" ValidationGroup="Form" />
                        <asp:Label ID="labelStatus" runat="server" Visible="false" />
                        <br />
						
						
                        <asp:Label ID="labelStatusPending" runat="server" CssClass="Explanation" Text="Your website is being reviewed by our team. As long as the status of your account is pending, you can only receive Security Only CAPTCHAs."></asp:Label>
						
						
						
					</div>
				</div>
            </div>             
			
			<div style="padding-top: 24px;" class="description">
				<h4>House Ads</h4>
				<div>
					<ul id="list_house_ads"></ul>
					<div id="iframe_upload"></div>
					<div style="clear: both;">
						<a href="#" title="Add Ad" id="button_add_image">+ Add Ad</a>
					</div>
				</div>
			</div>
                     
					 
					 
					 
            
			
			
			<div style="clear: both; padding-top: 24px;">
        		<div class="params">
    				<div class="param-name"></div>
    				<div class="param-value">
    					<a href="#" id="button_submit" title="Submit" class="btn">Submit</a>
    				</div>
    				<div class="param-value" style="margin-left: 6px;">
    					<a href="/publisher/ManageWebsites.aspx" title="Cancel" class="btn"><span>Cancel</span></a>
    				</div>
    			</div>
        	</div>

            
		
<script type="text/javascript">

var websiteId = <%=_website.Id%>;
var websiteUrl = "<%=_website.Url%>";
var campaignId = <%=_website.CampaignId as object ?? "null"%>;


var getList = function(params) {
	
	var obj = {
		getList : {
			campaignId : params.campaignId
		}
	};

	$.ajax({
        url: "/handlers/ads.ashx",
        data: { 
        	rq : JSON.stringify(obj),
        	timestamp : $.getTimestamp()
        },
        dataType: "json",
        success: function (data, status) {
        	if(data != undefined) {
	        	if(data.error != undefined) {
	        		if(params.error != undefined 
							&& typeof params.error == 'function') {
						params.error(data);
					}
				} else {
					if(params.success != undefined 
							&& typeof params.success == 'function') {
						params.success(data);
					}
				}
        	} else {
        		if(params.error != undefined 
						&& typeof params.error == 'function') {
					params.error();
				}
        	}
        },
        error: function (XHR, textStatus, errorThrow) {
            // error
        }
    });
};

var create = function(params) {
	
	var obj = {
		create : {
			adName : params.adName,
			imageId : params.imageId,
			clickUrl : params.clickUrl,
			likeUrl : params.likeUrl,
			maxBid : params.maxBid,
			campaignId : params.campaignId,
			websiteId : params.websiteId
		}
	};

	$.ajax({
        url: "/handlers/ads.ashx",
        data: { 
        	rq : JSON.stringify(obj),
        	timestamp : $.getTimestamp()
        },
        dataType: "json",
        success: function (data, status) {
        	if(data != undefined) {
	        	if(data.error != undefined) {
	        		if(params.error != undefined 
							&& typeof params.error == 'function') {
						params.error(data);
					}
				} else {
					if(params.success != undefined 
							&& typeof params.success == 'function') {
						params.success(data);
					}
				}
        	} else {
        		if(params.error != undefined 
						&& typeof params.error == 'function') {
					params.error();
				}
        	}
        },
        error: function (XHR, textStatus, errorThrow) {
            // error
        }
    });
};

var remove = function(params) {
	
	var obj = {
		remove : {
			adId : params.adId
		}
	};

	$.ajax({
        url: "/handlers/ads.ashx",
        data: { 
        	rq : JSON.stringify(obj),
        	timestamp : $.getTimestamp()
        },
        dataType: "json",
        success: function (data, status) {
        	if(data != undefined) {
	        	if(data.error != undefined) {
	        		if(params.error != undefined 
							&& typeof params.error == 'function') {
						params.error(data);
					}
				} else {
					if(params.success != undefined 
							&& typeof params.success == 'function') {
						params.success(data);
					}
				}
        	} else {
        		if(params.error != undefined 
						&& typeof params.error == 'function') {
					params.error();
				}
        	}
        },
        error: function (XHR, textStatus, errorThrow) {
            // error
        }
    });
};

function addAd(data) {
	
	var listItem = $("<li>" +
		"<div class=\"image-block\"><img src=\"/handlers/image.ashx?adId=" + data.adId + "\" width=\"100\" height=\"83\" data-value=\"" + data.width + " x " + data.height + "\" onclick=\"window.open(this.src);\" style=\"cursor: zoom-in;\" title=\"Click to preview\" /></div>" +
		"<div style=\"float: left; margin-left: 10px;\">" +
			"<div><b>Click Url:</b></div>" +
			"<div><a href=\"" +  data.clickUrl + "\" target=\"_blank\">" + data.clickUrl + "</a></div>" +
			"<div><b>Dimension:</b></div>" +
			"<div>" + data.width + " x " + data.height + "</div>" +
		"</div>" +
		"<div class=\"actions\"><a href=\"#\" data-value=\"" + data.adId + "\" title=\"Remove\" class=\"remove-item\">Remove</a></div>" +
	"</li>").appendTo('#list_house_ads');
	
	
	/*
	listItem.find('img').click(function(e) {
	
		var el = $(this);
		
		$('#image_preview_lightbox').remove();
		
		var imagePreviewLightbox = $("<div id=\"image_preview_lightbox\"><img src=\"" + el.attr("src") + "\" width=\"" + el.attr("data-value").split("x")[0] + "\" height=\"" + el.attr("data-value").split("x")[1] + "\" /><a href=\"#\" title=\"Close\">Close</a></div>").css({ top : el.offset().top, left : el.offset().left }).appendTo("body");
		
		imagePreviewLightbox.find('a').click(function(event) {
			imagePreviewLightbox.remove();
			event.preventDefault();
		});
		
		$(document).mouseup(function (e){
			var container = imagePreviewLightbox;
			if (!container.is(e.target) // if the target of the click isn't the container...
				&& container.has(e.target).length === 0) // ... nor a descendant of the container
				{
					container.remove();
				}
		});
		
		e.preventDefault();
	});
	*/
	
	listItem.find('.remove-item').click(function(e) {
		
		var el = $(this);
		
		remove({
			adId : el.attr("data-value"),
			success : function() {
				el.closest("li").remove();
			},
			error: function(error) {
				//
			}
		});
	
		e.preventDefault();
	});
};

$(function() {
	
	
	if(campaignId != null) {
		getList({
			campaignId : campaignId,
			success : function(data) {
			
				if(data.list != undefined) {
					for(var i = 0; i < data.list.length; i++) {
						addAd(data.list[i]);
					}
				}
			},
			error: function(error) {
			
			}
		});
	}

	$('#button_add_image').click(function(e) {
		
		var iframeBlock = $("<div style=\"border:1px solid #ccc; padding: 10px\">" +
			"<div class=\"item-params\">" +
				"<div class=\"item-param-name\">Image:</div>" +
				"<div class=\"item-param-value\" id=\"image_id\">" +
					"<iframe src=\"/upload.html?timestamp=1\" width=\"100%\" height=\"54\" seamless=\"seamless\" scrolling=\"no\" frameborder=\"0\" allowtransparency=\"true\"></iframe>" +
					"<div><label id=\"status_ad_image_id\" class=\"status\" style=\"display: none\">This field is required</label></div>" +
					"<div style=\"display: none;width: 100px; height: 83px; border: 1px solid #ccc\" id=\"upload_image_preview\"></div>" +
					"<div style=\"clear: both\">" +
						"<span class=\"Explanation\">Max. file size: <span id=\"labelImageFileSize\">200</span> KB<br>Allowed image types: <span id=\"labelImageFormat\">JPG</span><br/><b>Sliding captcha (W x H)</b>: 300x250</span>" +
					"</div>" +
				"</div>" +
			"</div>" +
			"<div class=\"item-params\" style=\"padding-top: 10px;\">" +
				"<div class=\"item-param-name\">Click Url:</div>" +
				"<div class=\"item-param-value\">" +
					"<div><input id=\"text_click_url\" type=\"text\" /></div>" +
					"<div><label id=\"status_click_url\"></label></div>" +
				"</div>" +
			"</div>" +
			"<div class=\"item-params\" style=\"padding-top: 10px;\">" +
				"<div class=\"item-param-name\">&nbsp;</div>" +
				"<div class=\"item-param-value\">" +
					"<a href=\"#\" title=\"Save\" id=\"button_save_upload\" class=\"small-button\">Save</a><a href=\"#\" title=\"Cancel\" id=\"button_cancel_upload\" style=\"margin-left: 8px;\">Cancel</a>" +
				"</div>" +
			"</div>" +
		"</div>").appendTo('#iframe_upload');
		
		
		iframeBlock.find('#text_click_url').urlInputBox(); 
		
		
		iframeBlock.find('#button_cancel_upload').click(function(event) {
			$('#iframe_upload').children().remove();
			$('#button_add_image').show();
			event.preventDefault();
		});
		
		iframeBlock.find('#button_save_upload').click(function(event) {
			
			
			//console.log("here..." + $('#image_id').data().imageId);
			
			if($('#image_id').data().imageId == undefined) {
				$('#status_ad_image_id').text("This field is required").show();
			}
			
			
			var v = null;
			v = new validator({
				elements : [
					{
						element : iframeBlock.find('#text_click_url'),
						status : iframeBlock.find('#status_click_url'),
						rules : [
							{ method : 'required', message : 'This field is required.' },
							{ method : 'url', message : 'Please enter a valid URL.' }
						]
					}
				],
				accept : function() {
					
					
					if($('#image_id').data().imageId != undefined) {
					
						create({
							adName : "",
							imageId : $('#image_id').data().imageId,
							clickUrl : iframeBlock.find('#text_click_url').val(),
							likeUrl : websiteUrl, // website url
							maxBid : 0,
							campaignId : campaignId, // params.campaignId
							websiteId : websiteId,
							success : function(data) {
							
								if(campaignId == null) {
									campaignId = data.campaignId;
								}
							
								addAd(data);
							
					
								$('#iframe_upload').children().remove();
								$('#button_add_image').show();
							
							}
						});
					
					} else {
						
						console.log("no image id");
						
					}
					
				}
			});
			
			
			v.validate();
			
			/*
			$('#iframe_upload').empty();
			$('#button_add_image').show();
			*/
			
			event.preventDefault();
		});
		
		$('#button_add_image').hide();
		
		e.preventDefault();
	});
	
});

function setImage(data) {
	
	$('#upload_image_preview').empty();
	
	if(data.imageId != undefined) {
		
		
		$('#image_id').data({ imageId : data.imageId });
		
		var img = new Image();		
		$(img).load(function () {
			 // set the image hidden by default    
			 $(this).hide();
			 $('#upload_image_preview').append(this).show();
	    
			 // fade our image in to create a nice effect
			 $(this).fadeIn();
	
		})
		.attr("src", "/handlers/image.ashx?imageId=" + data.imageId + "&imageTypeId=6")
		
		$('#status_ad_image_id').hide();
		
		
	}
};

function setImageError(data) {
	if(data.error == "InvalidDimension") {
		$("#status_ad_image_id").text("Invalid image dimension").show();
	}
	if(data.error == "InvalidFormat") {
		$("#status_ad_image_id").text("Invalid image format").show();
	}
};
</script>

	
</div>
</div>

</asp:content>