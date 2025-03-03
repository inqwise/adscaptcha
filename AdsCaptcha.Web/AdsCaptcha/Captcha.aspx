<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Captcha.aspx.cs" Inherits="AdsCaptcha.Captcha" EnableSessionState="False" EnableViewState="False" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Captcha</title>
	<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
	<script type="text/javascript" src="/scripts/utils/utils.js"></script>
	<!--[if lt IE 8]>
	<script type="text/javascript" src="/scripts/utils/json2.js"></script>
	<![endif]-->
	
	<script type="text/javascript" src="/scripts/validator/validator-1.2.8.js"></script>
	
	<style type="text/css">
	body {
		margin: 10px; 
		padding: 0;
		font-family: arial, sans-serif;
		font-size: 12px;
	}
	input, button { font-family: arial, sans-serif; font-size: 12px; }
	.params { padding-bottom: 6px; }
	.desc { color: #999; }
	</style>
</head>
<body>


	
    
    <form id="theform">
			
			<div>
				<script type="text/javascript" src="<%=ApiUrl%>slider/get.ashx?captchaId=<%=CaptchaId %>&publicKey=<%=PublicKey%>&dummy=4304508469"></script>
			</div>
			
			<div class="params">
				<div class="param-name">Captcha ID:</div>
				<div class="param-value">
					<div><input type="text" autocomplete="off" id="text_captcha_id" name="captcha_id" value="<%=CaptchaId %>" style="width: 100px;" /></div>
					<div class="desc"><em>This id is unique to this specific Captcha.</em></div>
				</div>
			</div>
			<div class="params">
				<div class="param-name">Public Key:</div>
				<div class="param-value">
					<div><input type="text" autocomplete="off" id="text_public_key" name="public_key" value="<%=PublicKey%>" style="width: 250px" /></div>
					<div class="desc"><em>Use the public key in the client code to receive any CAPTCHA of this website.</em></div>
				</div>
			</div>
			<div class="params">
				<div class="param-name">Private Key:</div>
				<div class="param-value">
					<div><input type="text" autocomplete="off" id="text_private_key" name="private_key" value="" style="width: 250px" /></div>
					<div class="desc"><em>Use the private key when validating any CAPTCHA of this website.</em></div>
				</div>
			</div>
			
			<br/>
			<br/>
			
			<div>
				<button id="button_reload" type="button">Reload</button>
				<button id="button_submit" type="button">Submit</button>
			</div>
		</form>
		
		<script type="text/javascript">
		var validateCaptcha = function(params) {
			$.ajax({
				type: "POST",
		        url: "<%=ApiUrl%>slider/validate.ashx",
		        data : {
		        	CaptchaId : $('#text_captcha_id').val(),
		        	PublicKey : $('#text_public_key').val(),
		        	PrivateKey : $('#text_private_key').val(),
		        	ChallengeCode : $("#adscaptcha_challenge_field").val(),
		        	UserResponse : $("#adscaptcha_response_field").val(),
		        	RemoteAddress : "127.0.0.1"
		        },
		        /*data: $('#theform').serialize(),*/
		        dataType: "jsonp",
				jsonp: "callback",
		        success: function (data, status) {
					alert(JSON.stringify(data));
		        },
		        error: function (XHR, textStatus, errorThrow) {
		            // error
		        }
		    });
		};
		
		var reloadCaptcha = function(params) {

			jQuery.ajax({
				url: "<%=ApiUrl%>slider/get.ashx?callback=?",
		        data: {
		            captchaId : params.captchaId,
		            publicKey : params.publicKey,
		            responseType : "json"
		        },
		        dataType: "jsonp",
		        jsonp: "callback",
		        success: function (data, b) {
		        
		        	if(minteye != undefined) {
		        		minteye.render(data);
		        	}
		        	
		        },
		        error: function (a, b, c) {
		            //
		        }
			});
		
		};
		
		$(function() {
			
			$('#button_reload').click(function(e) {
				
				reloadCaptcha({
					captchaId : $('#text_captcha_id').val(),
		            publicKey : $('#text_public_key').val()
				});
				
			});
			
			$('#button_submit').click(function(e) {
				
				validateCaptcha({
					success : function() {
						alert("VALID")
					},
					error: function() {
						alert("NOT VALID")
					}
				});
				
				e.preventDefault();
			});
			
		});
		</script>
    
</body>
</html>
                                                                                                                                                    