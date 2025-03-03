<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Publisher/PublisherAccount.Master" AutoEventWireup="true" CodeFile="EditCaptcha.aspx.cs" Inherits="Inqwise.AdsCaptcha.Publisher.EditCaptcha" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">


<script type="text/javascript" src="/scripts/utils/utils.js"></script>
<!--[if lt IE 8]>
<script type="text/javascript" src="/scripts/utils/json2.js"></script>
<![endif]-->

<script type="text/javascript" src="/scripts/utils/date.js"></script>
<script type="text/javascript" src="/scripts/datepicker/datepicker.js"></script>
<script type="text/javascript" src="/scripts/validator/validator-1.2.8.js"></script>

    
<style type="text/css">
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

#upload_image_preview img {
	width: 100px;
	height: 83px;
}
#container_upload_image {
	min-width: 350px;
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
.item-params { clear: both; overflow: hidden; min-height: 24px; }
.item-params .item-param-name { width: 130px; float: left;}
.item-params .item-param-value { width: 200px; float: left;}
.item-params .item-param-value input {
	font-family: arial;
	font-size: 12px;
}

ul.list-radio li {
	padding-bottom: 6px;
}





#tdCommercial table td {
    	padding:0 0 0 10px;
}
.FieldHeader {
    	height:60px;
    	vertical-align:middle;
}
.CheckBoxField td label, #tdCommercial table td label {
	margin:-2px 5px 0 5px;
}
.InputField, .TextareaField {
	border: 2px solid #DEDEDD;
	border-radius: 3px 3px 3px 3px;
	color: #333333;
    font: 20px Arial,Helvetica,sans-serif;
    padding: 6px;
    width: 200px;
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
.status {
    color: #BA0A1C !important;
    padding: 3px 0 10px;
}
.btn {
	cursor: pointer;
	margin-left: 0;
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


.container { padding: 40px 0px; }
#content .inner-content {
    margin: 0 auto;
    width: 960px;
}
</style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
Edit Captcha
</asp:Content>
<asp:content ContentPlaceHolderID="MainContent" runat="server">
<div id="content" class="container">
    
   <div class="inner-content">
            <br />
        <!--div id="breadCrambs">
            <asp:Label ID="labelNavigationPath" runat="server" />        
        </div-->

		<table cellpadding="0" cellspacing="0" border="0" width="100%">
	        		<tr>
	        			<td valign="top">

		<div>
			<div>
				<h4>Sliding Captcha Information</h4>
				<div class="description">
					<div class="params">
        				<div class="param-name">Captcha Name:*</div>
        				<div class="param-value">
        					<div><input type="text" id="text_captcha_name" name="captcha_name" autocomplete="off" class="InputField" value="<%=Captcha.Name %>" />&nbsp;<img rel="You may add different CAPTCHAs on your website.&lt;br /&gt;Use &lt;i&gt;Captcha Name&lt;/i&gt; to identify each of your CAPTCHAs." class="tooltip" src="../css/Inqwise/images/helpicon.png"></div>
        					<div><label id="status_captcha_name" class="status"></label></div>
        				</div>
        			</div>
        			<div class="params">
        				<div class="param-name">Status:*</div>
        				<div class="param-value">
        					<div><b id="label_status">Pending</b></div>
        					<div id="label_status_description" style="display: none;">
        					Your website is being reviewed by our team.<br/> 
        					As long as the status of your account is pending, you can only receive<br/> 
        					Non commercial CAPTCHAs.
        					</div>
        				</div>
        			</div>
        			<div class="params">
        				<div class="param-name">Dimensions:*</div>
        				<div class="param-value">
        					<select id="select_dimensions" autocomplete="off">
        						<option value="180x150">180x150</option>
								<option value="300x250" selected="selected">300x250</option>
        					</select>
        					<span>pixels</span>&nbsp;<img rel="Please choose the dimensions of your CAPTCHA." class="tooltip" src="../css/Inqwise/images/helpicon.png">
        				</div>
        			</div>
        			<div class="params">
        				<div class="param-name">Captcha Type:*</div>
        				<div class="param-value">
        					<div class="ui-form">
		        				<ul style="padding: 0px;">
		        					<li>
		        						<label><span><input type="radio" name="captcha_type" checked="checked" autocomplete="off" value="1" /></span>Commercial&nbsp;<img rel="&lt;b&gt;Commercial&lt;/b&gt; - &lt;i&gt;Income generating captcha&lt;/i&gt;, an  &lt;b&gt;&lt;i&gt;ad&lt;/i&gt;&lt;/b&gt;  will be displayed as captcha image, to solve the captcha users will have to normalize the image by moving the pointer on the slider." class="tooltip" src="../css/Inqwise/images/helpicon.png"></label>
		        					</li>
		        					<li>
		        						<label><span><input type="radio" name="captcha_type" autocomplete="off" value="0" /></span>Non commercial&nbsp;<img rel="&lt;b&gt;Non Commercial&lt;/b&gt;  - &lt;i&gt;Security only captcha&lt;/i&gt;,  a random picture will be displayed as a captcha image, to solve the captcha users will have to normalize the image by moving the pointer on the slider." class="tooltip" src="../css/Inqwise/images/helpicon.png"></label>
		        					</li>
		        					<li>
		        						<label><span><input type="radio" name="captcha_type" autocomplete="off" value="2" /></span>House Ads&nbsp;<img rel="&lt;b&gt;House Ads&lt;/b&gt; - Upload your own images - Logo, Produces images, Your company's messages the images will be displayed only in your site. &lt;br/&gt;This service is &lt;i&gt;Free&lt;/i&gt; up to 15,000 captchas per month. Contact support@Inqwise.com if you have more captchas." class="tooltip" src="../css/Inqwise/images/helpicon.png"></label>
		        						<div id="container_upload_image" style="display: none">
		        							
											
											<div style="padding-top: 10px; padding-left: 20px;">
												<ul id="list_house_ads"></ul>
												<div id="iframe_upload_house_ad"></div>
												<div style="clear: both;">
													<a href="#" title="Add Ad" id="button_add_house_ad">+ Add Ad</a>
												</div>
											</div>
											
											
		        						</div>
		        					</li>
		        				</ul>
		        			</div>
        				</div>
                    </div>
        			<div class="params" style="padding-top: 20px;">
                        <div class="param-name">Security Level:*</div>
        				<div class="param-value">
        					<div class="ui-form">
		        				<ul style="padding: 0px;" class="list-radio">
		        					<li>
		        						<label><span><input type="radio" name="security_level" autocomplete="off" value="11004" /></span>High</label>
		        					</li>
		        					<li>
		        						<label><span><input type="radio" name="security_level" autocomplete="off" value="11003" /></span>Medium</label>
		        					</li>
		        					<li>
		        						<label><span><input type="radio" name="security_level" checked="checked" autocomplete="off" value="11002" /></span>Low</label>
		        					</li>
		        				</ul>
								<br/>
								<div>
									<label><span><input type="checkbox" id="checkbox_auto_detect_security_level" autocomplete="off" /></span>Auto-change security level</label>
								</div>
		        			</div>
        				</div>
        			</div>
				</div>
			</div>
			
			<div style="clear: both; padding-top: 24px;">
        		<div class="params">
    				<div class="param-name"></div>
    				<div class="param-value">
    					<a id="button_submit" title="Submit" class="btn">Submit</a>
    				</div>
    				<div class="param-value" style="margin-left: 6px;">
    					<a href="/publisher/ManageCaptchas.aspx?WebsiteId=<%=Request.QueryString["WebsiteId"]%>" title="Cancel" class="btn"><span>Cancel</span></a>
    				</div>
    			</div>
        	</div>
				        	
		</div>
		
						</td>
	        			<td valign="top" style="width: 300px;">
	        				<script type="text/javascript" src="<%=ApiUrl %>slider/get.ashx?imageTypeId=<%=(int)Captcha.SourceType == 1 || (int)Captcha.SourceType == 2 ? 5 : 1%>&w=300&h=250&securityLevelId=<%=(int)Captcha.SecurityLevel%>"></script>
	        			</td>
	        		</tr>
        		</table>
		

	</div>
</div>


<script type="text/javascript">
    
var update = function(params) {

	var obj = {
		update : {
			websiteId : params.websiteId,
			captchaId : params.captchaId,
			captchaName : params.captchaName,
			width : params.width,
			height : params.height,
			captchaSourceTypeId : params.captchaSourceTypeId,
			imageId : params.imageId,
			securityLevelId : params.securityLevelId,
			autoDetect : params.autoDetect
		}
	};

	$.ajax({
        url: "/handlers/captchas.ashx",
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
            securityLevelId : params.securityLevelId,
			autoDetect : params.autoDetect,
            responseType : "json",
            w : params.width,
            h : params.height
        },
        dataType: "jsonp",
        jsonp: "callback",
        success: function (data, b) {
        
        	if(Inqwise != undefined) {
				try {
					Inqwise.render(data);
				} catch(e) {}
        	}
        	
        },
        error: function (a, b, c) {
            //
        }
	});

};

var initCaptcha = function() {

	var _dimensions = $('#select_dimensions').val();
	//var _captchaSourceTypeId = 0;
	$("input:radio[name=captcha_type]").each(function(i, el) {
		if($(el).prop("checked")) {
			_captchaSourceTypeId = $(el).val();
		}
	});
	//var _securityLevelId = 11002;
	$("input:radio[name=security_level]").each(function(i, el) {
	    if($(el).prop("checked")) {
	        _securityLevelId = $(el).val();
	    }
	});
	
	var _autoDetect = $('#checkbox_auto_detect_security_level').prop('checked');
			
	reloadCaptcha({
		imageTypeId : ((_captchaSourceTypeId == 1 || _captchaSourceTypeId == 2) ? 5 : 1),
		width : _dimensions.substring(0, _dimensions.indexOf("x")),
		height : _dimensions.substring(_dimensions.indexOf("x") + 1, _dimensions.length),
		securityLevelId : _securityLevelId,
		autoDetect : _autoDetect
	});
	
};

var _status = "<%=Captcha.Status%>";
var _statusId = <%=(int)Captcha.Status%>;
var _captchaSourceTypeId = <%=(int)Captcha.SourceType%>;
var _width = <%=Captcha.MaxWidth%>;
var _height = <%=Captcha.MaxHeight%>;
var _securityLevelId = <%=(int)Captcha.SecurityLevel%>;
var _autoDetect = <%=Captcha.AttackDetectionAutoChange.ToString().ToLower()%>;

var campaignId = <%=Captcha.CampaignId as object ?? "null"%>;

$(function() {
	
	$('#select_dimensions').change(function() {
		initCaptcha();
	});
	
	$('#checkbox_auto_detect_security_level')
	.prop('checked', _autoDetect)
	.change(function() {
		initCaptcha();
	});
	
	/*
	$("input:radio[name=captcha_type]").each(function(i, el) {
		$(el).change(function() {
			initCaptcha();	
		});
	});
	*/
	
	$('#label_status').text(_status);
	if(_statusId == 10003) {
		$("#label_status_description").show();
	}
	
	$('#select_dimensions').val(_width + "x" + _height);
	
	
	
	
	

	// validator
	var v = null;
	v = new validator({
		elements : [
			{
				element : $('#text_captcha_name'),
				status : $('#status_captcha_name'),
				rules : [
					{ method : 'required', message : 'This field is required.' }
				] 
			}
		],
		submitElement : $('#button_submit'),
		accept : function () {
			
			var captchaSourceTypeId = 0;
			$("input:radio[name=captcha_type]").each(function(i, el) {
				if($(el).prop("checked")) {
					captchaSourceTypeId = $(el).val();
				}
			});
			
			var securityLevelId = 11002;
			$("input:radio[name=security_level]").each(function(i, el) {
			    if($(el).prop("checked")) {
			        securityLevelId = $(el).val();
			    }
			});
			
			var autoDetect = $('#checkbox_auto_detect_security_level').prop('checked');

			var dimensions = $('#select_dimensions').val();
			
			
				
				// no image
				update({
					websiteId : <%=Request.QueryString["WebsiteId"]%>,
					captchaId : <%=Request.QueryString["CaptchaId"]%>,
					captchaName : $('#text_captcha_name').val(),
					width : dimensions.substring(0, dimensions.indexOf("x")),
					height : dimensions.substring(dimensions.indexOf("x") + 1, dimensions.length),
					captchaSourceTypeId : captchaSourceTypeId,
					securityLevelId : securityLevelId,
					autoDetect :  autoDetect,
					success : function(data) {
						
						location.href = "/publisher/GetCode.aspx?WebsiteId=<%=Request.QueryString["WebsiteId"]%>&CaptchaId=<%=Request.QueryString["CaptchaId"]%>";
						
					},
					error: function(error) {
						alert(JSON.stringify(error));
					}
				});
				
				
		}
			
	});
	
	
	
	
	/*
	$("#button_submit")
	.on("click", function() {
		if($("input:radio[name=captcha_type][value=2]").prop("checked")) {
			if($('#hidden_ad_image_id').val() == "") {
				$("#status_ad_image_id").text("This field is required.").show();
			} else {
				$("#status_ad_image_id").hide();
			}
		} else {
			$("#status_ad_image_id").hide();
		}
	});
	*/
	
	
	$('#text_website_url')
	.urlInputBox();
	
	// captcha_type
	$("input:radio[name=captcha_type]").change(function() {
		if($(this).val() == 2) {
			$('#container_upload_image').show();
		} else {
			$('#container_upload_image').hide();
		}
		
		initCaptcha();
	});
    
    // security_level
	$("input:radio[name=security_level]").change(function() {
	    initCaptcha();
	});
	
	
	
	
	
	
	
	
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
	
		listItem.find('.remove-item').click(function(e) {
			var el = $(this);
			
			// remove
			jsapi.ads.remove({
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
	
	// add house ad
	$('#button_add_house_ad').click(function(e) {
		
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
					"<div><input id=\"text_click_url\" type=\"text\" name=\"click_url\" /></div>" +
					"<div><label id=\"status_click_url\" class=\"status\"></label></div>" +
				"</div>" +
			"</div>" +
			"<div class=\"item-params\" style=\"padding-top: 10px;\">" +
				"<div class=\"item-param-name\">&nbsp;</div>" +
				"<div class=\"item-param-value\">" +
					"<a href=\"#\" title=\"Save\" id=\"button_save_upload\" class=\"small-button\">Save</a><a href=\"#\" title=\"Cancel\" id=\"button_cancel_upload\" style=\"margin-left: 8px;\">Cancel</a>" +
				"</div>" +
			"</div>" +
		"</div>").appendTo('#iframe_upload_house_ad');
		
		
		iframeBlock.find('#text_click_url').urlInputBox(); 
		
		iframeBlock.find('#button_cancel_upload').click(function(event) {
			$('#iframe_upload_house_ad').children().remove();
			$('#button_add_house_ad').show();
			event.preventDefault();
		});
		
		iframeBlock.find('#button_save_upload').click(function(event) {
			
			if($('#image_id').data().imageId != undefined) {
			
			
				if($('#text_click_url').val().length == 0) {
					$('#status_click_url').text("This field is required.").show();
				} else {
					$('#status_click_url').hide();
					if(/^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test($('#text_click_url').val())) {
						
						
						$('#status_click_url').hide();
						
						
						jsapi.ads.create({
							adName : "",
							imageId : $('#image_id').data().imageId,
							clickUrl : $('#text_click_url').val(),
							likeUrl : null, // website url
							maxBid : 0,
							campaignId : campaignId, // params.campaignId
							websiteId : <%=Request.QueryString["WebsiteId"]%>,
							success : function(data) {
				
								if(campaignId == null) {
									campaignId = data.campaignId;
								}
				
								// add ad
								addAd(data);
				
								$('#iframe_upload_house_ad').children().remove();
								$('#button_add_house_ad').show();
				
							}
						});
						
						
					} else {
						
						$('#status_click_url').text("Please enter a valid URL.").show();
						
					}
				}
		
			} else {
				
				$('#status_ad_image_id').text("This field is required").show();
				
				if($('#text_click_url').val().length == 0) {
					$('#status_click_url').text("This field is required.").show();
				} else {
					$('#status_click_url').hide();
					if(/^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test($('#text_click_url').val())) {
						
						
						$('#status_click_url').hide();
						
					} else {
						
						$('#status_click_url').text("Please enter a valid URL.").show();
						
					}
				}
				
			}
			
			
			
			$('#text_click_url').keyup(function() {
				
				if($('#text_click_url').val().length == 0) {
					$('#status_click_url').text("This field is required.").show();
				} else {
					$('#status_click_url').hide();
					if(/^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test($('#text_click_url').val())) {
						
						
						$('#status_click_url').hide();
						
					} else {
						
						$('#status_click_url').text("Please enter a valid URL.").show();
						
					}
				}
				
			});
			
			
			event.preventDefault();
		
		});
		
		
		$('#button_add_house_ad').hide();
		
		e.preventDefault();
	});
	
	
	

	// get ads
	if(campaignId != null) {
	
		jsapi.ads.getList({
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
	
	
	
	
	
	// set captcha type
	$("input:radio[name=captcha_type][value=" + _captchaSourceTypeId + "]")
		.prop("checked", true);
    
    // set security level
	$("input:radio[name=security_level][value=" + _securityLevelId + "]")
		.prop("checked", true);

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
			 $(this).fadeIn();
		})
		.attr("src", "/handlers/image.ashx?imageId=" + data.imageId + "&imageTypeId=6");
		
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

var jsapi = {
	ads : {
		getList : function(params) {
	
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
		},
		create : function(params) {
	
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
		},
		remove : function(params) {
	
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
		}
	}
};

</script>
</asp:content>