<%@ Page EnableViewStateMac="false" Language="C#" MasterPageFile="~/Advertiser/AdvertiserAccount.Master" AutoEventWireup="true" CodeFile="NewCampaign.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.NewCampaign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server"> 
id="body" class="page"
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">

<link rel="stylesheet" type="text/css" href="/css/datepicker/datepicker.css" />
<style type="text/css">
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
</style>


<script type="text/javascript" src="/scripts/utils/utils.js"></script>
<!--[if lt IE 8]>
<script type="text/javascript" src="/scripts/utils/json2.js"></script>
<![endif]-->

<script type="text/javascript" src="/scripts/utils/date.js"></script>
<script type="text/javascript" src="/scripts/datepicker/datepicker.js"></script>
<script type="text/javascript" src="/scripts/validator/validator-1.2.8.js"></script>

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

div.list-box {
	background: none repeat scroll 0 0 #FFFFFF;
    border-color: #CCCCCC;
    border-style: solid;
    border-width: 1px;
    height: 184px;
    overflow-x: hidden;
    overflow-y: auto;
    width: 200px;
    padding: 0 !important;
    font-family: arial, helvetica, sans-serif;
    font-size: 14px;
}
div.list-box div {
	padding: 4px 6px;
}
div.list-box div label { Xwidth: 100%; }
div.list-box div:hover { background: #efefef; }




.InputField, .TextareaField {
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
   
   .ButtonHolder
   {
   	margin-top:30px;
   }
   
   .ButtonHolder a
   {
   	color:#FFFFFF;
   }
   
.container {
    /*background: -moz-radial-gradient(center center , ellipse farthest-corner, #FFFFFF 0%, #EAEAEA 100%) repeat scroll 0 0 transparent;
    border: 2px solid #FFFFFF;
    box-shadow: 0 5px 22px #BBBBBB;
    height: 370px;*/
    margin: 0px auto;
    overflow: hidden;
    padding: 50px 0  0 0;
    position: relative;
    width: auto;
}
.page #status {
	position: relative;
}
#content .inner-content, #content-form {
    padding-top: 20px;
}
.FieldHeader {
	white-space:nowrap;
}
</style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
New Campaign
</asp:Content>

<asp:content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <!--div id="breadCrambs">
        <asp:Label ID="labelNavigationPath" runat="server" />        
    </div-->

   

    <div id="content" class="container">
        <div id="content-form">
        	<table cellpadding="0" cellspacing="0" border="0" width="100%">
        		<tr>
        			<td valign="top">
	        			<div>
			        		<h4>Create your new campaign now</h4>
			        		<div class="description">
			        			<div class="params">
			        				<div class="param-name">Campaign Name:*</div>
			        				<div class="param-value">
			        					<div><input type="text" id="text_campaign_name" name="campaign_name" autocomplete="off" class="InputField" /></div>
			        					<div><label id="status_campaign_name" class="status"></label></div>
			        				</div>
			        			</div>
			        			<div class="params">
			        				<div class="param-name">Ad Name:*</div>
			        				<div class="param-value">
			        					<div><input type="text" id="text_ad_name" name="ad_name" autocomplete="off" class="InputField" /></div>
			        					<div><label id="status_ad_name" class="status"></label></div>
			        				</div>
			        			</div>
			        			<div class="params">
			        				<div class="param-name">Image:*</div>
			        				<div class="param-value">
			        					<div>
			        						<input type="hidden" id="hidden_ad_image_id" autocomplete="off" />
			        						<div id="iframe_upload"></div>
			        						<div><img width="125" height="104" style="cursor:-moz-zoom-in; display: none" onclick="window.open(this.src);" alt="Preview" id="upload_preview" ></div>
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
			        						<input type="text" id="text_image_click_url" name="image_click_url" autocomplete="off" class="InputField" /><a id="link_check_image_click_url" title="Check URL" target="_blank" class="ui-link-disabled" style="margin-left: 6px;">Check URL</a>&nbsp;<img rel="the image in the captcha can be clickable, insert here the target site or landing page URL , were you want to drive your traffic" class="tooltip" src="../css/Inqwise/images/helpicon.png">
			        					</div>
			        					<div><label id="status_image_click_url" class="status"></label></div>
			        				</div>
			        			</div>
			        			<div class="params">
			        				<div class="param-name">Facebook Like URL:</div>
			        				<div class="param-value">
			        					<div><input type="text" id="text_facebook_like_url" name="facebook_like_url" autocomplete="off" class="InputField" /><a id="link_check_facebook_like_url" title="Check URL" target="_blank" class="ui-link-disabled" style="margin-left: 6px;">Check URL</a>&nbsp;<img rel="Insert here You Facebook page URL, when users will click on the &lt;b&gt;Like&lt;/b&gt; button they will become fans" class="tooltip" src="../css/Inqwise/images/helpicon.png"></div>
			        					<div><label id="status_facebook_like_url" class="status"></label></div>
			        				</div>
			        			</div>
			        		</div>
			        	</div>
			        	<div style="clear: both; padding-top: 24px;">
			        		<h4>Targeting</h4>
			        		<div class="description">
			        			<div style="font-size: 18px; font-weight: bold;">Choose a geographic location for your ads to appear:</div>
			        			<div class="ui-form" style="padding-top: 12px;">
			        				<ul style="padding: 0;">
			        					<li>
			        						<label><span><input type="radio" name="country" checked="checked" autocomplete="off" value="0" /></span>Show my ad anywhere in the world</label>
			        					</li>
			        					<li>
			        						<label><span><input type="radio" name="country" autocomplete="off" value="1" /></span>Choose countries</label>
			        						<div id="container_countries" style="display: none;padding-top: 12px;">
			        						    <div class="list-box">
			        						    <asp:Repeater ID="rptCountries" runat="server">
			        						        <ItemTemplate>
			        						            <div><label><span><input type="checkbox" value="<%# Eval("Country_Id") %>" name="country" autocomplete="off" /></span><%# Eval("Country_Name") %></label></div>
			        						        </ItemTemplate>
			        						    </asp:Repeater>
			        						    </div>
			        						</div>
			        					</li>
			        				</ul>
			        			</div>
			        			<div style="font-size: 18px; font-weight: bold; padding-top: 24px;">Choose your campaign category:</div>
			        			<div class="ui-form" style="padding-top: 12px;">
			        				<ul style="padding: 0px;">
			        					<li>
			        						<label><span><input type="radio" name="category" checked="checked" autocomplete="off" value="0" /></span>Show my ad in all categories</label>
			        					</li>
			        					<li>
			        						<label><span><input type="radio" name="category" autocomplete="off" value="1" /></span>Choose categories</label>
			        						<div id="container_categories" style="display: none; padding-top: 12px;">
			        						     <div class="list-box">
			        						     <asp:Repeater ID="rptCategories" runat="server">
			        						        <ItemTemplate>
			        						            <div><label><span><input type="checkbox" value="<%# Eval("Category_Id") %>" name="category" autocomplete="off" /></span><%# Eval("Category_Desc") %></label></div>
			        						        </ItemTemplate>
			        						    </asp:Repeater>
			        						    </div>
			        						</div>
			        					</li>
			        				</ul>
			        			</div>
			        		</div>
			        	</div>
			        	<div style="clear: both; padding-top: 24px;">
			        		<h4>Budget and Schedule</h4>
			        		<div class="description">
			        			<div style="font-size: 18px; font-weight: bold; ">What are your campaign goal?</div>
			        			<div>
			        				<ul style="padding: 0px;">
			        					<li>By using Sliding captcha you can achieve both traffic and branding (engagement) goals</li>
										<li>By selecting Pay per Click you will be charged only for clicks made on your ad</li>
										<li>By selecting Pay per Fit you will be charged only when a user normalize your ad correctly - Full engagement</li> 
			        				</ul>
			        			</div>
			        			<div style="font-size: 18px; font-weight: bold; padding-top: 24px;">Campaign Charging method:</div>
			        			<div class="ui-form" style="padding-top: 12px;">
			        				<ul style="padding: 0px;">
			        					<li>
			        						<label><span><input type="radio" name="budget_type" checked="checked" autocomplete="off" value="24002" /></span>Pay per Click</label>
			        					</li>
			        					<li>
			        						<label><span><input type="radio" name="budget_type" autocomplete="off" value="24001" /></span>Pay per Fit</label>
			        					</li>
			        				</ul>
			        			</div>
			        			<div style="font-size: 18px; font-weight: bold;padding-top: 24px;">Choose to limit your daily budget and your <label class="label_budget_type">Pay-Per-Click</label> bid:</div>
			        			<div class="params" style="padding-top: 12px;">
			        				<div class="param-name">Daily Budget (Min. $10):*</div>
			        				<div class="param-value">
			        					<div><span>$</span><input type="text" id="text_daily_budget" name="daily_budget" autocomplete="off" value="100" class="InputField" />&nbsp;<img rel="The amount of money per day an advertiser is willing to pay for his CAPTCHAs to be clicked/fitted." class="tooltip" src="../css/Inqwise/images/helpicon.png"></div>
			        					<div><label id="status_daily_budget" class="status"></label></div>
			        				</div>
			        			</div>
			        			<div style="font-size: 18px; font-weight: bold;padding-top: 24px;clear: both">What is your payment limit per <label class="label_budget_type">Pay-Per-Click</label>? (Min. $0.01)</div>
			        			<div class="params" style="padding-top: 12px;">
			        				<div class="param-name">Max. <label class="label_budget_type">Pay-Per-Click</label> Bid:*</div>
			        				<div class="param-value">
			        					<div><span>$</span><input type="text" id="text_max_bid" name="max_bid" autocomplete="off" value="0.01" class="InputField" />&nbsp;<img rel="This defines what an advertiser will pay when a user successfully clicks/fits the ad." class="tooltip" src="../css/Inqwise/images/helpicon.png"></div>
			        					<div><label id="status_max_bid" class="status"></label></div>
			        				</div>
			        			</div>
			        			<div style="font-size: 18px; font-weight: bold;padding-top: 24px;clear: both;">When do you wish to start running your ad?</div>
			        			<div class="ui-form" style="padding-top: 12px;">
			        				<div class="params">
			        					<div class="param-name">Schedule:*</div>
			        					<div class="param-value">
				        					<ul style="padding: 0px;">
					        					<li>
					        						<label><span><input type="radio" name="schedule" checked="checked" autocomplete="off" value="0" /></span>Now</label>
					        					</li>
					        					<li>
					        						<label><span><input type="radio" name="schedule" autocomplete="off" value="1" /></span>Specific dates</label>
					        						<div id="container_schedule" style="display: none; padding-top: 12px; padding-bottom: 24px;">
				        								<div class="params">
						        							<div class="param-name">From:</div>
						        							<div class="param-value"><input type="text" id="date_from" autocomplete="off" class="InputField" /></div>
						        						</div>
						        						<div class="params">
						        							<div class="param-name">To:</div>
						        							<div class="param-value"><input type="text" id="date_to" autocomplete="off" class="InputField" /></div>
						        						</div>
					        						</div>
					        					</li>
					        				</ul>
			        					</div>
			        				</div>
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
			    					<a href="/advertiser/ManageCampaigns.aspx" title="Cancel" class="btn"><span>Cancel</span></a>
			    				</div>
			    			</div>
			        	</div>
        			</td>
        			<td valign="top" style="width: 300px;">
        				<div style="padding-top: 24px;">
        					&nbsp;
                            <script type="text/javascript" src="<%=ApiUrl %>slider/get.ashx?imageTypeId=5&w=300&h=250"></script>
			  			</div>
        			</td>
        		</tr>
        	</table>
        
        	
        
        
        	
        
<script type="text/javascript">


// handlers.campaigns.ashx
var create = function(params) {
	
	var obj = {
		create : {
			campaignName : params.campaignName,
			ad : params.ad,
			countries : params.countries,
			categories : params.categories,
			campaignPaymentTypeId : params.campaignPaymentTypeId,
			dailyBudget : params.dailyBudget,
			fromDate : params.fromDate,
			toDate : params.toDate,
			temporary : params.temporary
		}
	};

	$.ajax({
        url: "/handlers/campaigns.ashx",
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

var fromDate = new Date();
var toDate = new Date();
$(function() {


	if($.getUrlParam("Action") != "SignUp") {
		$('#boxes').hide();
	}

	$("<iframe src=\"/upload.html?timestamp=1\" width=\"100%\" height=\"54\" seamless=\"seamless\" scrolling=\"no\" frameborder=\"0\" allowtransparency=\"true\"></iframe>").appendTo('#iframe_upload');
	

	// validator
	var v = null;
	v = new validator({
		elements : [
			{
				element : $('#text_campaign_name'),
				status : $('#status_campaign_name'),
				rules : [
					{ method : 'required', message : 'This field is required.' }
				] 
			},
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
				element: $('#text_daily_budget'),
				status : $('#status_daily_budget'),
				rules : [
					{ method : 'required', message : 'This field is required.' },
					{ method : 'currency', message : 'Please enter a valid currency value.' },
					{ method : 'min', pattern : 10, message : 'Please enter a value greater than or equal to {0}.' }
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
		
			var countries = [];
			$("input:checkbox[name=country][checked=checked]").each(function(i, el) {
				countries.push($(el).val());
			});
			
			var categories = [];
			$("input:checkbox[name=category][checked=checked]").each(function(i, el) {
				categories.push($(el).val());
			});
			
			if($('#hidden_ad_image_id').val() != "") {
				
				
				var newFromDate, newToDate;
				if($("input:radio[name=schedule][checked=checked]").val() != 0) {
					var tempFromDate = new Date($('#date_from').val());
					var tempToDate = new Date($('#date_to').val());
					
					newFromDate = tempFromDate.format(dateFormat.masks.isoDate);
					newFromDate = newFromDate + " 00:00";
					
					newToDate = tempToDate.format(dateFormat.masks.isoDate);
					newToDate = newToDate + " 23:59";
					
					
				}
				
				create({
					campaignName : $('#text_campaign_name').val(),
					ad : {
						adName : $('#text_ad_name').val(),
						imageId : $('#hidden_ad_image_id').val(),
						clickUrl : $('#text_image_click_url').val(),
						likeUrl : $('#text_facebook_like_url').val(),
						maxBid : $('#text_max_bid').val()
					},
					countries : countries,
					categories : categories,
					campaignPaymentTypeId : $("input:radio[name=budget_type][checked=checked]").val(),
					dailyBudget : $('#text_daily_budget').val(),
					fromDate : newFromDate,
					toDate : newToDate,
					temporary : ($.getUrlParam("Action") == "SignUp" ? true : false),
					success : function(data) {
						if(data.guid != undefined) {
							location.href = "/advertiser/SignUp.aspx?campaign=" + encodeURIComponent(JSON.stringify(data));
						}
						
						if(data.campaignId != undefined) {
							location.href = "/advertiser/ManageCampaigns.aspx";
						}
					},
					error: function(error) {
						alert(JSON.stringify(error));
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
	
	
	// countries
	$("input:radio[name=country]").change(function() {
		if($(this).val() != 0) {
			$('#container_countries').show();
		} else {
			$('#container_countries').hide();
		}
	});
	
	
	// category
	$("input:radio[name=category]").change(function() {
		if($(this).val() != 0) {
			$('#container_categories').show();
		} else {
			$('#container_categories').hide();
		}
	});
	
	// budget_type
	$("input:radio[name=budget_type]").change(function() {
		if($(this).val() != 24002) {
			$('.label_budget_type').text("Pay-Per-Fit");
		} else {
			$('.label_budget_type').text("Pay-Per-Click");
		}
	});
	
	// schedule
	$("input:radio[name=schedule]").change(function() {
		if($(this).val() != 0) {
			$('#container_schedule').show();
		} else {
			$('#container_schedule').hide();
		}
	});
	
	
	$('#date_from').val(fromDate.format(dateFormat.masks.mediumDate));
	$('#date_from').DatePicker({
		date: fromDate.format(dateFormat.masks.isoDate),
		current: fromDate.format(dateFormat.masks.isoDate),
		starts: 1,
		/*
		onRender: function(date) {
			return {
				disabled: (date.valueOf() < now.valueOf()),
				className: date.valueOf() == now2.valueOf() ? 'datepickerSpecial' : false
			}
		},
		*/
		onBeforeShow: function(){
			var lastFromDate = new Date($('#date_from').val()); 
			$('#date_from').DatePickerSetDate(lastFromDate, true);
		},
		onChange: function(formated, dates){
			var newFromDate = new Date(formated);
			$('#date_from').val(newFromDate.format(dateFormat.masks.mediumDate));
			$('#date_from').DatePickerHide();
		}
	});
	
	
	toDate.addMonths(1);
	
	$('#date_to').val(toDate.format(dateFormat.masks.mediumDate));
	$('#date_to').DatePicker({
		date: toDate.format(dateFormat.masks.isoDate),
		current: toDate.format(dateFormat.masks.isoDate),
		starts: 1,
		/*
		onRender: function(date) {
			return {
				disabled: (date.valueOf() < now.valueOf()),
				className: date.valueOf() == now2.valueOf() ? 'datepickerSpecial' : false
			}
		},
		*/
		onBeforeShow: function(){
			var lastToDate = new Date($('#date_to').val()); 
			$('#date_to').DatePickerSetDate(lastToDate, true);
		},
		onChange: function(formated, dates){
			var newToDate = new Date(formated);
			$('#date_to').val(newToDate.format(dateFormat.masks.mediumDate));
			$('#date_to').DatePickerHide();
		}
	});

});

function setImage(data) {
	if(data.imageId != undefined) {
	
		$('#status_ad_image_id').hide();
		$('#hidden_ad_image_id').val(data.imageId);
	    
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
        
        </div>
    </div>        
</div>

</asp:content>