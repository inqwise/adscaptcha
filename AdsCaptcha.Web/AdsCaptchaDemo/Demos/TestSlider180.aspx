<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.master" CodeFile="TestSlider180.aspx.cs" Inherits="TestSlider180" %>
<%@ Import Namespace="Inqwise.AdsCaptcha.SystemFramework" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleHolder">
Slider 180x150

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
	<div style="padding-left: 174px;">
    	<span id="label_captcha_result" style="display: none; font-weight: bold;font-size: 14px;"></span>
	</div>

    <h4 style="margin-left: 40px;">Sample form</h4>
    <div class="description" style="margin-top:10px;">
        <div style="float:left;color:#5B5757; font-family:'oswaldregular';margin:7px 7px 7px 39px;">eMail: </div>
        <div style="float:left"><input name="demail" class="InputField" value="" type="text" size="30" style="width:300px;" /></div>
        <div style="float:left;color:#5B5757; font-family:'oswaldregular';margin:7px;font-size:13px;display:none;">We respest your privacy</div>

        <div style="clear:both;width:100%;height:1px;"></div>

        <div style="float:left;color:#5B5757; font-family:'oswaldregular';margin:7px;">username: </div>
        <div style="float:left"><input name="dname" class="InputField" value="" type="text" size="30" style="width:300px;" /></div>
        <div style="float:left;color:#5B5757; font-family:'oswaldregular';margin:7px;font-size:13px;display:none;">No spaces please</div>

     </div>
     <br /><br />
       <table style="margin-left:20px;">
        <tr>
                        <td style="width:110px;" valign="top">  
            <img src="../styles/Inqwise/images/recaptcha.png" alt="recaptcha" style="margin: 0;" />
            <div style="width:68px;margin: -20px 0 0 50px; text-align:center;color: #5B5757;font-family: 'oswaldregular';font-size: 27px;font-weight: 400;">Are You a Robot?</div>
            </td>
             <td  style="width:200px;"><div id="sliderholder">
          
          <script type="text/javascript" src="<%=ApplicationConfiguration.ApiUrl.Value %>slider/get.ashx?imageTypeId=5&w=180&h=150"></script>
          </div></td>

        </tr>
        <tr>
          <td colspan="2">
          
          		<div style="padding-left: 240px;">
          		<a style="color: #fff" class="btn" title="Submit captcha" id="button_submit" href="#">Submit</a>
          		</div>
                  
          </td>
        </tr>
     </table>
      

         <br />
         
         
<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
<script type="text/javascript">window.jQuery || document.write("<script type=\"text/javascript\" src=\"/scripts/jquery/1.8.2/jquery.min.js\"><\/script>")</script>
<!--[if lt IE 8]>
<script type="text/javascript" src="//yandex.st/json2/2011-10-19/json2.min.js"></script>
<![endif]-->
<script type="text/javascript">
var validateCaptcha = function(params) {
	$.ajax({
		type: "POST",
        url: "<%=ApplicationConfiguration.ApiUrl.Value %>slider/validate.ashx",
        data : {
        	ImageTypeId : 5,
        	ChallengeCode : $("#adscaptcha_challenge_field").val(),
        	UserResponse : $("#adscaptcha_response_field").val(),
        	RemoteAddress : "127.0.0.1"
        },
        dataType: "jsonp",
		jsonp: "callback",
        success: function (data, status) {
			if(data == true) {
				if(params.success != undefined 
					&& typeof params.success == 'function') {
					params.success();
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
	
$(function() {

	$('#button_submit').click(function(e) {
		
		$('#label_captcha_result').hide();
		
		validateCaptcha({
			success : function() {
				$('#label_captcha_result')
				.text("Yuuuuup! You're a human being...")
				.css("color", "green")
				.show();
			},
			error: function() {
				$('#label_captcha_result')
				.text("No, Mr. Robot, you shall not pass!")
				.css("color", "red")
				.show();
			}
		});
		
		e.preventDefault();
	});
	
});
</script>
         
         
</asp:Content>