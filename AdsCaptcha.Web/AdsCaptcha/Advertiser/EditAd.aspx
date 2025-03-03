<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Advertiser/AdvertiserAccount.Master" AutoEventWireup="true" CodeFile="EditAd.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.EditAd" %>
<%@ Import Namespace="Inqwise.AdsCaptcha.SystemFramework" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<script type="text/javascript" src="/scripts/utils/utils.js"></script>
<!--[if lt IE 8]>
<script type="text/javascript" src="/scripts/utils/json2.js"></script>
<![endif]-->

<script type="text/javascript" src="/scripts/utils/date.js"></script>
<script type="text/javascript" src="/scripts/datepicker/datepicker.js"></script>
<script type="text/javascript" src="/scripts/validator/validator-1.2.8.js"></script>


<style type="text/css" media="screen">
        ul.tabs {
        list-style:none;
        margin:0 !important;
        padding:0;
        height:44px;
    }
    ul.tabs li {
        float:left;
        text-indent:0;
        padding:0;
        margin:0 !important;
        list-style-image:none !important;
    }
    ul.tabs a {
      float: left;
      padding: 10px;
      position: relative;
      top: 2px;
      font-size: 14px;
      min-width: 140px;
      text-align: center;
      cursor: pointer;
      margin: 5px 5px 5px 0px;
      padding: 10px 20px;
      background: #ECEDEF;
      border: 1px solid #ccc;
      -moz-border-radius: 5px 5px 0 0;
      -webkit-border-radius: 5px 5px 0 0;
      border-radius: 5px 5px 0 0;
      
      /*text-shadow: 1px 1px 1px white;*/
      text-decoration:none;
      color: #AAA;
    font-family: Arial;
    font-size: 14px;
     font-weight:700;
    }
    ul.tabs a:hover {
background-color: #F6F6F6;
color: #7C7C7C;
    }
    ul.tabs a.current, ul.tabs a.current:hover, ul.tabs li.current a {
      background: white;
      border-bottom: 1px solid white;
      font-weight:700;
      color: #19C12D;
      z-index: 101;
    }
    .panes .pane {
        display:none;
    }
    .panes {
      padding: 10px 10px 10px 10px;
      background-color: white;
      position: relative;
      z-index: 100;
      
    }
    .panes
    {
    	border: 1px solid #ccc;
    }
  </style>
 
 <style type="text/css">
            .uploadwrapper {
	width: 133px;
	/* Centering button will not work, so we need to use additional div */
	margin: 0 auto;
}
div.uploadbutton {

	background: url(images/bg-button-small.png) repeat-x 0 0;
	
	 border: 1px solid #61AE28;
    border-radius: 5px 5px 5px 5px;
    color: #FFFFFF;
    cursor:pointer;
    display: block;
    float: left;
    font-family: 'oswaldregular';
    height: 22px;
    line-height: 22px;
    overflow: hidden;
    padding-left: 5px;
    text-align: center;
    width: 92px;
    font-size:14px;
}
/* 
We can't use ":hover" preudo-class because we have
invisible file input above, so we have to simulate
hover effect with javascript. 
 */
div.uploadbutton.hover {
	background: url(images/bg-button-small.png) repeat-x 0 0;
	color: #ffffff;	
	cursor:pointer;
}
</style>
    
<style type="text/css">

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
.ui-link-disabled {
    color: #999999 !important;
    cursor: default;
    text-decoration: none !important;
}

.ui-form label {
    color: #333333;
    display: table;
}
.ui-form label span {
	display: table-cell;
	width: 20px;
}
.ui-form label input[type="radio"],
.ui-form label input[type="checkbox"] {
	margin:0;
    padding: 0;
    vertical-align: baseline;
}
.status {
    color: #BA0A1C;
    padding: 3px 0 10px;
}
.btn {
	cursor: pointer;
	margin-left: 0;
}
#content .description {
	font-family: arial,helvetica,sans-serif;
    font-size: 14px;
    line-height: 18px;
    padding-left: 0 !important;
}
#content .description ul {
    list-style: none outside none;
    padding: 5px 50px;
}


   .InputField, .TextareaField
   {
   border: 2px solid #DEDEDD;
    border-radius: 3px 3px 3px 3px;
    color: #333333;
    font: 20px Arial,Helvetica,sans-serif;
    padding: 6px;
    width: 200px;
   }
   
   .SelectField
   {
   	width: 215px;
   }
   
   #buttonHolder
   {
   	margin:30px 0 30px 0;
   	padding-bottom: 20px
   }
   
   .ButtonHolder a
   {
   	color:#FFFFFF;
   }
   
   .container {
    /*background: -moz-radial-gradient(center center , ellipse farthest-corner, #FFFFFF 0%, #EAEAEA 100%) repeat scroll 0 0 transparent;
    border: 2px solid #FFFFFF;
    box-shadow: 0 5px 22px #BBBBBB;
    height: 370px;
    margin: 20px auto;
    overflow: hidden;
    padding: 50px;*/
    margin: 0px auto;
    overflow: hidden;
    padding: 50px 0  0 10px;
    position: relative;
    width: auto;
}

#content h4
{
	background-position: left 4px !important;
    margin-bottom: 30px;
    line-height: 1;
}

.FieldHeader
{
	white-space:nowrap;
}
	#content .inner-content, #content-form {
        padding: 0px 0 40px 0;
    margin: 0 auto;
    width: 950px;

}
   
  
#top-content { width: auto !important; }
#top-content .menu-right { text-align: right; }
#top-content { padding:0 0 30px 0; }
</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Edit Ad
</asp:Content>
<asp:content ContentPlaceHolderID="MainContent" runat="server">

 
    <!--div id="navigation">
        <div class="navigation">
            <ul>
                <li class="selected"><a href="ManageCampaigns.aspx">Campaigns</a></li>
                <li><a href="BillingSummary.aspx">Billing</a></li>
                <li><a href="AccountPreferences.aspx">My Account</a></li>
            </ul>
        </div>
    </div>
    <div id="subNavigation">
        <div class="subNavigation">
            <ul>
                <li><a href="ManageCampaigns.aspx">Manage</a></li>
                <li><a href="NewCampaign.aspx">New Campaign</a></li>
            </ul>
        </div>
    </div>
     <div id="breadCrambs">
            <asp:Label ID="labelNavigationPath" runat="server" />        
        </div-->
     
        <div id="content"  class="container">
        	<div class="inner-content">
        		
        		<div id="top-content">
                    <div class="menu-right">
                         <ul  style="width:auto;text-align:right;">
	            			<li class="none"><a href="ManageAds.aspx?CampaignId=<%=Page.Request.QueryString["CampaignId"]%>">Manage Ads</a></li>
                        </ul>
                    </div>
                </div>
                 
                 
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
	        		<tr>
	        			<td valign="top">
        					<h4>Edit Your Ad</h4>
        					<div class="description">
        						<div>
        							<div style="font-size: 18px; font-weight: bold;clear: both;">What will your ad say?</div>
        							<div>Your ad can display marketing argss (slogans / taglines / sales info. Etc.).</div> 
        						</div>
        						<div class="params" style="padding-top: 12px;">
				    				<div class="param-name">Ad Name:*</div>
				    				<div class="param-value">
				    					<div><input type="text" id="text_ad_name" name="ad_name" autocomplete="off" class="InputField" value="<%=Ad.Name %>" /></div>
				    					<div><label id="status_ad_name" class="status"></label></div>
				    				</div>
				    			</div>
        						<div class="params">
				    				<div class="param-name">Status:*</div>
				    				<div class="param-value">
				    					<select id="select_ad_status" autocomplete="off">
				    						<option value="10001">Running</option>
				    						<option value="10002">Paused</option>
				    					</select>
				    				</div>
				    			</div>
				    			<div style="font-size: 18px; font-weight: bold;padding-top: 24px;clear: both;">What is your payment limit per <label class="label_budget_type">Pay-Per-Click</label>? (Min. $0.01)</div>
				    			<div class="params" style="padding-top: 12px;">
				    				<div class="param-name">Max. <label class="label_budget_type">Pay-Per-Click</label> Bid:*</div>
				    				<div class="param-value">
				    					<div><span>$</span><input type="text" id="text_max_bid" name="max_bid" autocomplete="off" value="<%=String.Format("{0:0.##}",Ad.MaxBid) %>" class="InputField" />&nbsp;<img rel="&lt;b&gt;This defines what an advertiser will pay when a user successfully clicks/fits the ad." class="tooltip" src="../css/Inqwise/images/helpicon.png"></div>
				    					<div><label id="status_max_bid" class="status"></label></div>
				    				</div>
				    			</div>
				    			<div class="params">
				    				<div class="param-name">Image:*</div>
				    				<div class="param-value">
				    					<div>
				    						<input type="hidden" id="hidden_ad_image_id" autocomplete="off" />
				    						<div id="iframe_upload"></div>
				    						<div><img width="125" height="104" style="cursor:-moz-zoom-in;" onclick="window.open(this.src);" alt="Preview" id="upload_preview" src="<%=ImageUrl %>"></div>
				    					</div>
				    					<div><label id="status_ad_image_id" class="status" style="display: none">This field is required.</label></div>
				    					<div style="clear: both">
				    						<span class="Explanation">                                                        
				                                Max. file size: <span id="labelImageFileSize">200</span> KB
				                                <br/>
				                                Allowed image types: <span id="labelImageFormat">JPG</span>
				                                <br/>
				                                <b>Sliding captcha (W x H)</b>: 300x250
				                                <br/>
				                                <br/>
				                            </span>
				    					</div>
				    				</div>
				    			</div>
				    			<div class="params">
				    				<div class="param-name">Image Click URL:</div>
				    				<div class="param-value">
				    					<div>
				    						<input type="text" id="text_image_click_url" name="image_click_url" autocomplete="off" class="InputField" value="<%=Ad.ClickUrl %>" /><a id="link_check_image_click_url" title="Check URL" target="_blank" class="ui-link-disabled" style="margin-left: 6px;">Check URL</a>&nbsp;<img rel="the image in the captcha can be clickable, insert here the target site or landing page URL , were you want to drive your traffic" class="tooltip" src="../css/Inqwise/images/helpicon.png">
				    					</div>
				    					<div><label id="status_image_click_url" class="status"></label></div>
				    				</div>
				    			</div>
				    			<div class="params">
				    				<div class="param-name">Facebook Like URL:</div>
				    				<div class="param-value">
				    					<div><input type="text" id="text_facebook_like_url" name="facebook_like_url" autocomplete="off" class="InputField" value="<%=Ad.LikeUrl %>" /><a id="link_check_facebook_like_url" title="Check URL" target="_blank" class="ui-link-disabled" style="margin-left: 6px;">Check URL</a>&nbsp;<img rel="Insert here You Facebook page URL, when users will click on the &lt;b&gt;Like&lt;/b&gt; button they will become fans" class="tooltip" src="../css/Inqwise/images/helpicon.png"></div>
				    					<div><label id="status_facebook_like_url" class="status"></label></div>
				    				</div>
				    			</div>
        					</div>
        					
        					<div>
				        		<div class="params">
				    				<div class="param-name"></div>
				    				<div class="param-value">
				    					<a id="button_submit" title="Submit" class="btn">Submit</a>
				    				</div>
				    				<div class="param-value" style="margin-left: 6px;">
				    					<a href="ManageAds.aspx?CampaignId=<%=Page.Request.QueryString["CampaignId"]%>" title="Cancel" class="btn"><span>Cancel</span></a>
				    				</div>
				    			</div>
			    			</div>
        				</td>
        				<td valign="top" style="width: 300px;">
	        				<div style="padding-top: 24px;">
	        					&nbsp;
								<script type="text/javascript" src="<%=ApiUrl %>slider/get.ashx?imageTypeId=7&w=300&h=250&adId=<%=Ad.ExternalId%>"></script>
				  			</div>
	        			</td>
        			</tr>
        		</table>
        	
        
        </div>        
    </div>
    
<script type="text/javascript">


var adStatus = <%=(int)Ad.Status %>;
var update = function(params) {
	
	var obj = {
		update : {
			adId : params.adId,
			adName : params.adName,
			adStatusId : params.adStatusId,
			imageId : params.imageId,
			clickUrl : params.clickUrl,
			likeUrl : params.likeUrl,
			maxBid : params.maxBid
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

var reloadCaptcha = function(params) {

	jQuery.ajax({
		url: apiUrl + "slider/get.ashx?callback=?",
        data: {
            imageId : params.imageId,
            imageTypeId : params.imageTypeId,
            responseType : "json",
            w : params.width,
            h : params.height
        },
        dataType: "jsonp",
        jsonp: "callback",
        success: function (data, b) {
        
        	if(Inqwise != undefined) {
        		Inqwise.render(data);
        	}
        	
        },
        error: function (a, b, c) {
            //
        }
	});

};

$(function() {


	$("<iframe src=\"/upload.html?timestamp=1\" width=\"100%\" height=\"54\" seamless=\"seamless\" scrolling=\"no\" frameborder=\"0\" allowtransparency=\"true\"></iframe>").appendTo('#iframe_upload');
	
	
	// validator
	var v = null;
	v = new validator({
		elements : [
		    {
				element : $('#text_ad_name'),
				status : $('#status_ad_name'),
				rules : [
					{ method : 'required', message : 'This field is required.' }
				]
			},
			{
				element : $('#text_image_click_url'),
				status : $('#status_image_click_url'),
				validate : function() { return $('#text_image_click_url').val() != "" },
				rules : [
					{ method : 'required', message : 'This field is required.' },
					{ method : 'url', message : 'Please enter a valid URL.' }
				]
			},
			{
				element : $('#text_facebook_like_url'),
				status : $('#status_facebook_like_url'),
				validate : function() { return $('#text_facebook_like_url').val() != "" },
				rules : [
					{ method : 'required', message : 'This field is required.' },
					{ method : 'url', message : 'Please enter a valid URL.' }
				]
			},
			{
				element: $('#text_max_bid'),
				status : $('#status_max_bid'),
				rules : [
					{ method : 'required', message : 'This field is required.' },
					{ method : 'currency', message : 'Please enter a valid currency value.' },
					{ method : 'min', pattern : 0.01, message : 'Please enter a value greater than or equal to {0}.' }
				]
			}
		],
		submitElement : $('#button_submit'),
		accept : function () {
		
			if($('#hidden_ad_image_id').val() != "") {
			
				update({
					adId : $.getUrlParam("AdId"),
					adName : $('#text_ad_name').val(),
					adStatusId : $('#select_ad_status').val(),
					imageId : (lastImageId != null ? $('#hidden_ad_image_id').val() : undefined),
					clickUrl : $('#text_image_click_url').val(),
					likeUrl : $('#text_facebook_like_url').val(),
					maxBid : $('#text_max_bid').val(),
					success : function(data) {
						location.href = "/advertiser/ManageAds.aspx?CampaignId=" + $.getUrlParam("CampaignId");
					},
					error: function(error) {
						alert(JSON.stringify(error))
					}
				});
			
			} else {
				$("#status_ad_image").hide();
			}
		
		},
		error: function() {
		
		}
	});
	
	$("#button_submit")
	.on("click", function() {
	
		if($('#hidden_ad_image_id').val() == "") {
			$("#status_ad_image_id").text("This field is required.").show();
		} else {
			$("#status_ad_image_id").hide();
		}
		
	});
	
	
	function checkImageClickUrl(value) {
		if(/^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(value)) {
	    	$('#link_check_image_click_url')
	    	.attr("href", value)
	    	.removeClass("ui-link-disabled");
	    } else {
	    	$('#link_check_image_click_url')
	    	.removeAttr("href")
	    	.addClass("ui-link-disabled");
	    }
	};
	
	$('#text_image_click_url')
	.on("keyup", function() {
		var value = $(this).val();
	    checkImageClickUrl(value);
	})
	.bind('paste', function () {
		setTimeout(function () {
			var value = $('#text_image_click_url').val();
			checkImageClickUrl(value);
		},100);
	})
	.urlInputBox();
	
	
	function checkFacebookLikeUrl(value) {
		if(/^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(value)) {
	    	$('#link_check_facebook_like_url')
	    	.attr("href", value)
	    	.removeClass("ui-link-disabled");
	    } else {
	    	$('#link_check_facebook_like_url')
	    	.removeAttr("href")
	    	.addClass("ui-link-disabled");
	    }
	};
	
	$('#text_facebook_like_url')
	.on("keyup", function() {
		var value = $(this).val();
	    checkFacebookLikeUrl(value);
	})
	.bind('paste', function () {
		setTimeout(function () {
			var value = $('#text_facebook_like_url').val();
			checkFacebookLikeUrl(value);
		},100);
	})
	.urlInputBox();
	
	
	
	
	// set
	
	$('#select_ad_status').val(adStatus);
	
	$('#hidden_ad_image_id').val($.getUrlParam("AdId"));
	
	$('#text_image_click_url').trigger("keyup");
	$('#text_facebook_like_url').trigger("keyup");

});


var lastImageId = null;
function setImage(data) {
	if(data.imageId != undefined) {
		$('#status_ad_image_id').hide();
		$('#hidden_ad_image_id').val(data.imageId);
		
		lastImageId = data.imageId;
		
		$('#upload_preview').attr("src", "/handlers/image.ashx?imageId=" + data.imageId + "&imageTypeId=6").show();
	    
	    reloadCaptcha({
			imageId : data.imageId,
			imageTypeId : 6,
			width : 300,
			height : 250
		});
		
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
</asp:content>