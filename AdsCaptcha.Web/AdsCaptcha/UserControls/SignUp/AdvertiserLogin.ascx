<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvertiserLogin.ascx.cs" Inherits="Inqwise.AdsCaptcha.UserControls.SignUp.Login" %>
<div id="login-box" class="login-popup">
        <fieldset class="textbox">
    	<label class="username">
        <span>Email</span>
        <input id="username" name="username" value="" type="text" autocomplete="on">
        </label>
        <label class="password">
        <span>Password</span>
        <input id="password" name="password" value="" type="password">
        </label>
        <button class="submit signinbutton" type="button">Sign in</button>
        <p>
        <a class="forgot" href="#">Forgot your password?</a>
        </p>
        </fieldset>
  </div>
  
  <script charset="utf-8">
	    $(document).ready(function() {
	        $('a.login-window').click(function() {

	            //Getting the variable's value from a link
	            var loginBox = $(this).attr('href');

	            //Fade in the Popup
	            $(loginBox).fadeIn(300);

	            //Set the center alignment padding + border see css style
	            var popMargTop = ($(loginBox).height() + 24) / 2;
	            var popMargLeft = ($(loginBox).width() + 24) / 2;

	            $(loginBox).css({
	                'margin-top': -popMargTop,
	                'margin-left': -popMargLeft
	            });

	            // Add the mask to body
	            $('body').append('<div id="mask"></div>');
	            $('#mask').fadeIn(300);

	            return false;
	        });

	        // When clicking on the button close or the mask layer the popup closed
	        $('a.close, #mask').live('click', function() {
	            $('#mask , .login-popup').fadeOut(300, function() {
	                $('#mask').remove();
	            });
	            return false;
	        });
	    });
	</script>