<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo.aspx.cs" Inherits="LiveDemo.Demo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html ml:lang="en" xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head>
	<title>Sign Up and Build Your Email List for Free | MailChimp.com</title>

	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="robots" content="noindex,nofollow" />
    
    <link rel="canonical" href="http://www.mailchimp.com/signup">
    <link rel="stylesheet" media="screen" href="files/reset.css" type="text/css">
    <link rel="stylesheet" media="print" href="files/print.css" type="text/css">

    <!--<script type="text/javascript" src="files/jsapi"></script>
    <script type="text/javascript" src="files/jquery.js"></script>-->
    <script type="text/javascript" src="http://code.jquery.com/jquery.min.js"></script>


    <!--[if lte IE 6]>
    <link rel="stylesheet" media="screen" type="text/css" href="http://webcss.mailchimp.com/css/ie6.css" />
    <script type="text/javascript" src="http://webcss.mailchimp.com/js/supersleight-min.js"></script>
    <script src="http://ie7-js.googlecode.com/svn/version/2.0(beta3)/IE8.js" type="text/javascript"></script>
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" media="screen" type="text/css" href="http://webcss.mailchimp.com/css/ie7.css" />
    <![endif]-->
   
    <!--
    <script type="text/javascript">
        $(document).ready(function(){

        $('input.cancel').css('opacity', '.6');
	        $('#signup_form form').validate({
			        rules: {
				        email: {
					        required: true,
					        email: true
				        },
				        username: "required",
				        password: {
					        required: true,
					        minlength: 4
				        },
				        password_confirm: {
					        required: true,
					        minlength: 4,
					        equalTo: "#password"
				        }
			        },
			        messages: {
				        email: "Email is required",
				        username: "Please enter a username",
				        password: "Please enter a password",
				        password_confirm: "Passwords don’t match."
			        }
	        });			
        });
    </script>
    -->

    <style>
        #signup_form legend { -moz-border-radius:0px;border-radius:0px;-webkit-border-radius:0px;margin:0 -.4em;padding:5px 10px;font-weight:bold;font-size:1.2em;background:#eef3f8;border:1px solid #c4d3ea;text-transform:capitalize;color:#336699; }
        #signup_form label {float:left;width:30%;margin-top:9px;text-align:right;font-size:12px;font-weight:bold;color:#555;text-transform:capitalize;}
        #signup_form fieldset { clear:both;padding:1em 1.4em 1.4em 1.4em;margin:0 0 30px .3em;background:#fff;border:1px solid #c4d3ea; }
        #signup_form input:focus,textarea:focus { border:2px solid #ef9e4d;background:#fff; }
        #signup_form .field-group { float:left;clear:both;width:100%;margin:6px 0 4px; }
        #signup_form input,textarea,select { width:60%;-moz-border-radius:4px;border-radius:4px;-webkit-border-radius:4px;font-size:1.1em;color:#666;margin-left:15px;padding:5px;font-family:"Lucida Grande","Helvetica Neue",Helvetica,Arial,Verdana,sans-serif; }

        #signup_form label.error { width:50% !important;text-align:left;color:#D12F19;border-color:#FBC2C4; position:relative; top:-25px; margin-bottom:-18px;}
        #signup_form div.error { width:50% !important;display:block;  margin-bottom:1em; padding:10px 4px 10px 60px; text-align:left; position:relative; top:-15px; background-position:12px 50% !important; color: #D12F19; width:52%; margin-left:33%; margin-bottom:0; margin-top:0px; z-index:50; position:relative; top:-5px; }
        .helper{margin-bottom:20px !important;}
        .btn, input[type="submit"]{color:#fff !important;}

        .indicates-required{margin-bottom:0 !important;}

        .by-clicking{font-size:10px; margin-top:15px; color:#999; margin-bottom:0; padding-bottom:0;}
        div.field-help{border:0; background:0; font-size:100%; float:none; right:auto; left:0px; padding:13px 2px 4px; text-align:left !important; font-size:95%;}
    </style>
</head>

<body id="interior">

<div style="display:none;position:absolute;top:600px;left:500px;z-index:10000;width:500px;height:500px;background-color:Gray;">

</div>

<script type="text/javascript">
    function SWFTransparent() {
        var embed = document.getElementsByTagName("embed");
        for (var i = 0; i < embed.length; i++) {
            embed[i].setAttribute('wmode', 'transparent');
            embed[i].setAttribute('style', 'position:absolute;z-index:0;');
        }

        var param = document.getElementsByTagName("param");
        for (var i = 0; i < param.length; i++) {
            if (param[i].getAttribute('name') == "wmode")
                param[i].setAttribute('value', 'transparent');
            
        }
    }
    function addLoadEvent(func) {
        var oldonload = window.onload;
        if (typeof window.onload != 'function') {
            window.onload = func;
        } else {
            window.onload = function() {
                if (oldonload) {
                    oldonload();
                }
                func();
            }
        }
    }

    addLoadEvent(SWFTransparent);

</script>

    <!-- \\\ START GLOBAL HEADER  /// -->
    <!--
    <div id="header_full">	    	
        <div id="global_header">
		    <div id="header_main">
			    <h1 id="logo"><a href="javascript:window.location=document.location.href;">MailChimp</a></h1>
    					
		    </div>
    		
		    <ul id="global_nav">
			    <li class="features"><a href="#/">Features</a></li>
			    <li class="pricing"><a href="#">Pricing</a></li>
			    <li class="customers"><a href="#">Customers</a></li>
			    <li class="resources"><a href="#">Resources</a></li>
			    <li class="support"><a href="#">Support</a></li>
			    <li class="about"><a href="#">About Us</a></li>
			    <li class="blog last"><a href="#">Blog</a></li>
		    </ul>
    		
		    <div id="global_search">
			    <div id="searchform">
				    <form id="searchbox_003125691254608490735:4onskqo03ra" action="#">
				      <input name="cx" value="003125691254608490735:4onskqo03ra" type="hidden">
				      <input name="q" size="40" id="keywords" type="text">
				      <input name="sa" value="Search" id="submitquery" type="submit">
				      <input name="cof" value="FORID:9" type="hidden">
				    </form>
		        </div>
		    </div>
	    </div>	
    </div>
    -->
    <!-- \\\ END GLOBAL HEADER  /// -->


    <!-- \\\ START INTERIOR  /// -->
    <div id="interior_white_bg">
	    <div id="interior_container">
	    <div id="interior_end_container">
    		
		    <div class="container">
			    <div class="content">
				    <div class="sub-heading">
					    <h2>Create Your MailChimp Account</h2>
				    </div>
    				
				    <div class="sub-content">
					    <!-- \\\ START CONTENT /// -->
					    <script type="text/javascript" src="files/js_ajax.js"></script>

					    <div style="border-left: 1px dotted rgb(211, 211, 211); float: right; margin-left: 0pt; padding-left: 25px;" class="span-7">
    					
					    <asp:Panel ID="panelForm" runat="server" Visible="true">
					    
					    <div id="signup_form">

                        <!-- <form name="tryit" method="post" action="http://www.mailchimp.com/svc_signup/post.php"> -->
                        <form id="tryit" name="tryit" runat="server">
    						
						    <!-- signupservice - required -->
						    <input name="formURL" value="#" type="hidden">
						    <input name="confirmationURL" value="#" type="hidden">
						    <input name="activationREF" value="" type="hidden">
                                                    
						    <fieldset>
							    <legend style="color: rgb(85, 85, 85);">Step 1. Your Email Address</legend>

							    <div class="field-group helper">
								    <label for="email">email:</label>
								    <input id="email" name="email" tabindex="3" class="required" type="text">

    					
								    <div id="email_status"></div>
								    <div class="field-help"><a target="_blank" href="#" rel="external">we respect your privacy</a></div>
							    </div>
						    </fieldset>

					        <fieldset>
							    <legend style="color: rgb(85, 85, 85);">Step 2. Username and Password</legend>

							    <div class="field-group helper">

								    <label for="username">username:</label>
								    <input id="username" name="username" tabindex="5" class="required" onfocus="document.getElementById('username_status').style.display='none';" onblur="ajaxFunction('check_username',[this.value]);" type="text">

					        <div class="field-help">no spaces please</div>
    					
					        <div id="username_status" class="error inline-error" style="display: none;"></div>

							    </div>

							    <div class="field-group helper">
								    <label for="password">Password:</label>
								    <input id="password" name="password" tabindex="6" class="required" value="" type="password">
					                <div class="field-help">at least 4 characters</div>
							    </div>

							    <div class="field-group">
								    <label for="password">Confirm Password:</label>
								    <input id="password_confirm" name="password_confirm" class="required" tabindex="7" type="password">
															    </div>
						    </fieldset>

						    <fieldset>
							    <legend style="color: rgb(85, 85, 85);">Step 3. Are you a robot?</legend>
							    <div class="field-group helper" style="width:auto;text-align:right;float:right;">
							        <asp:Label ID="labelAdsCaptcha" runat="server" />
							    </div>
							    <asp:Label ID="labelWrong" runat="server" Visible="false"
							        Text="Wrong. Please try again..."
							        ForeColor="Red" Font-Bold="true" Font-Size="Medium">
							    </asp:Label>
						    </fieldset>

							    <input id="referral" name="referral" tabindex="8" value="" type="hidden">

						    <div class="form-button-group">
							    <asp:Button ID="Submit" runat="server" 
                                    style="padding: 12px 24px; color: rgb(255, 255, 255); font-size: 18px; margin-left: 0pt;" 
                                    Text="Submit" onclick="Submit_Click1" />
						    </div>
						    <p class="by-clicking">By clicking this button, you agree to MailChimp's <a href="#" target="_blank" title="Anti-spam Policy &amp; Terms of Use">Anti-spam Policy &amp; Terms of Use</a>.</p>
                            
					    </form>
					    </div>

                        </asp:Panel>
                        
                        <asp:Panel ID="panelCorrect" runat="server" Visible="false">
                        
                            <span style="font-size:20px;color:Green;font-weight:bold;">
                                Correct!
                            </span>
                            <br />
                            <br />
                            <a href="javascript:window.location=document.location.href;">try again?</a>
                        </asp:Panel>
                        
	                    </div>

					    <div id="signup_sidebar" style="width: 265px; float: left; margin-top: -5px;">

    <img alt="500free" src="files/500free.jpg" style="position: relative; left: -17px;" height="275" width="294">

    						
    <h3>How much is this?</h3>
						    <p>Mailchimp is <strong>totally free</strong> for lists below 500 subscribers, so give it a try. You'll be amazed by all the powerful email marketing features.</p>
						    <p><strong>No Credit Card Required. No Contracts.</strong></p>
                                                    <p>If you like what you see, upgrade your account for only $30 per month.</p>


    	
    <div class="quotes">
			    <div class="testimonial_bubble">
			            <h4>MailChimp is THE BEST</h4>
			            <blockquote>“ For email campaigns we've tried everything from Constant Contact to Godaddy. MailChimp is THE BEST. Check it out.. ”</blockquote>
			    </div>
			            <cite>– <strong>Taylor Phillips</strong>, Marketing Specialist</cite>
			    </div>



    <div class="customers_overview">
			    <h3>Who Uses MailChimp?</h3>
			    <img src="files/makewish.gif" class="customer_logos" alt="Make-A-Wish" title="Make-A-Wish">
			    <img src="files/staples.gif" class="customer_logos" alt="Staples" title="Staples">
			    <img src="files/firefox.gif" class="customer_logos" alt="Firefox" title="Firefox">
		    </div>

    <hr>


						    <div id="verification-logos" style="">
							    <a target="_blank" href="http://www.mcafeesecure.com/RatingVerify?ref=www.mailchimp.com"><img secure="" sites="" help="" keep="" you="" safe="" from="" identity="" theft,="" credit="" card="" fraud,="" spyware,="" spam,="" viruses="" and="" online="" scams="" src="files/55.gif" oncontextmenu="alert('Copying Prohibited by Law - McAfee Secure is a Trademark of McAfee, Inc.'); return false;" border="0" width="65alt=&quot;McAfee"></a>
							    <a href="http://www.bbbonline.org/cks.asp?id=105113010411" target="_blank" style="margin: 0pt; padding: 0pt;"><img style="margin: 2px 4px;" src="files/bbb_logo.png" alt="BBB Online - Reliablity Program"></a>
							    <a style="margin-left: 15px;" href="#" target="_blank"><img src="files/truste_seal_ctv_small.gif" alt="Truste" border="0" height="63" vspace="8" width="45"></a><br clear="all">
						    </div>

    </div>



					    <!-- \\\ END CONTENT /// -->
				    </div>
    				
			    </div>
		    </div>
		    </div>		

	    </div>
    </div>
    <!-- \\\ END INTERIOR /// -->

    <div id="interior_footer">
			    <div id="interior_footer_top">
				    <div id="interior_footer_bottom">
    					
						    <div class="footer_sections">
							    <h6><a style="color: rgb(92, 92, 92);" href="#"><img src="files/blogs_stack.png" style="margin-right: 6px;">Free HTML Templates →</a></h6>
							    <p class="small" style="margin-bottom: 17px;">Templates tested in all major email clients, ready for customizing.</p>
    						
						    </div>
    						
						    <div class="footer_sections">
							    <h6><a style="color: rgb(92, 92, 92);" href="#"><img src="files/table__arrow.png" style="margin-right: 6px;">Compare Us →</a></h6>
							    <p class="small" style="margin-bottom: 17px;">Check out how we compare against other email marketing providers. </p>
    					
						    </div>
    						
						    <div class="footer_sections">
							    <h6><a style="color: rgb(92, 92, 92);" href="#"><img src="files/money_dollar.png" style="margin-right: 6px;">View Pricing Plans →</a></h6>
							    <p class="small" style="margin-bottom: 17px;">MailChimp offers flexible plans for every budget.</p>
    			
						    </div>
    						
						    <div class="footer_sections">
							    <h6><a style="color: rgb(92, 92, 92);" href="#"><img src="files/star.png" style="margin-right: 6px;">Forever Free Plan →</a></h6>
							    <p class="small" style="margin-bottom: 17px;">Manage up to 500 subscribers, send up to 3,000 emails/month for free.</p>
						    </div>
    						
				    </div>
			    </div>
		    </div>
    		
			    <div id="bottom_links">
    				
				    <p class="small" style="margin-bottom: 9px;">
				    <a href="#">Follow us on Twitter</a> <span>&nbsp;•&nbsp;</span>
				    <a href="#">Subscribe to our Newsletter</a> <span>&nbsp;•&nbsp;</span>
				    <a href="#">MailChimp Jungle</a> <span>&nbsp;•&nbsp;</span>
				    <a href="#">MailChimp Labs</a>
				    </p>
    				
				    <p class="small">
				    Copyright © 2009 MailChimp. All rights reserved <span>&nbsp;•&nbsp;</span>
				    <a href="#">Contact Us</a> <span>&nbsp;•&nbsp;</span>
				    <a href="#">Partner With Us</a> <span>&nbsp;•&nbsp;</span>
				    <a href="#">Abuse Desk</a> <span>&nbsp;•&nbsp;</span>
				    <a href="#">Privacy Policy</a> <span>&nbsp;•&nbsp;</span>
                    <a href="#">Copyright Policy</a> <span>&nbsp;•&nbsp;</span>
				    <a href="#">Terms of Use</a>
				    </p>
			    </div>

    <div style="opacity: 0.4;" id="footer_logos"> 
	    <ul>

    <li><a href="#" target="_blank" style="margin: 0pt 0pt 0pt 10px; padding: 0pt;"><img style="margin: 2px 4px;" src="files/truste_seal_web.png" alt="Truste Certified Privacy"></a></li>

    <li><a href="#" target="_blank" style="margin: 0pt; padding: 0pt;"><img style="margin: 2px 4px;" src="files/bbb_accredited.gif" alt="BBB Online - Reliablity Program"></a></li>

    <li><a href="#" target="_blank" style="margin: 0pt 0pt 0pt 15px; padding: 0pt;"><img src="files/member_eec.gif" alt="Member of Email Experience Council"></a></li>

    <li><a href="#" target="_blank" style="margin: 0pt; padding: 0pt;"><img style="margin: 2px 4px;" src="files/member_espc.png" alt="Member of Email Sender and Provider Coalition"></a></li>

    <li><a href="#" target="_blank" style="margin: 0pt; padding: 0pt;"><img style="margin: 2px 4px;" src="files/logo_maawg.gif" alt="Member of Messaging Anti-Abuse Working Group"></a></li>

    <li class="last"><a href="#" target="_blank" style="margin: 0pt 0pt 0pt 10px; padding: 0pt;"><img src="files/ota_logo.png" alt="Member of Online Trust Alliance" height="32" width="90"></a></li>

	    </ul>
    </div>

    <a href="#" style="display: none;">http://www.mailchimp.com/nonrestrictiveocean.php</a>

    <script type="text/javascript" src="files/1161.js"> </script>

    <div id="videotour_side" style="display: none;">
        <embed src="files/gtRe7Ixhk8cS" type="application/x-shockwave-flash" allowscriptaccess="always" allowfullscreen="true" height="310" width="452">
    </div>

    <div id="facebox" style="display: none;">       
    <div class="popup"><table><tbody><tr><td class="tl"></td><td class="b"></td><td class="tr"></td></tr><tr><td class="b"></td><td class="body"><div class="facebox-content"></div><div class="footer"><a href="#" class="close"><img src="files/closelabel.gif" title="close" class="close_image"></a></div></td><td class="b"></td></tr><tr><td class="bl"></td><td class="b"></td><td class="br"></td></tr></tbody></table></div></div>
</body>

</html>