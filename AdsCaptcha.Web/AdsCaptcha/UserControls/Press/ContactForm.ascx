<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactForm.ascx.cs" Inherits="Inqwise.AdsCaptcha.UserControls.Press.ContactForm" %>

<style type="text/css">

#contactform p
{
	padding: 5px 0 5px 0;
}
#contactform p label
{
	vertical-align:middle;
	font-size:14px;
}
.success {
	background: #C0F0B3 url('css/Inqwise/images/success.png') no-repeat 20px center;
	border: 1px solid #91C184;
	color: #5C8A50;
  border-radius: 10px;
  -moz-border-radius: 10px;
  -webkit-border-radius: 10px;
  text-shadow: 1px 1px 1px #fff;
  padding: 8px 20px 4px 75px;
  margin: 10px 0px;
  min-width: 32px;
  min-height: 28px;
  font-size:14px;
  }
  .warning {
  background: #FFCEBE url('css/Inqwise/images/warning.png') no-repeat 20px center;
  border: 1px solid #E09B85;
  border-radius: 10px;
  -moz-border-radius: 10px;
  -webkit-border-radius: 10px;
  text-shadow: 1px 1px 1px #fff;
  color: #957368;
  padding: 8px 20px 4px 75px;
  margin: 10px 0px;
  min-width: 32px;
  min-height: 28px;
   font-size:14px;
  }

#contactform .btn
{
	margin: 4px 10px 0 0;
	}
</style>

<div class="description">
<h4>Press inquire</h4>
<!--  "success" / "warning"-->
<div id="contactmessage" style="display:none;width:190px;"></div>

</div>
<div id="contactform" style="width:290px;">
<p>
<input name="firstname" type="text" id="firstname" class="InputField required" reqargs="First Name is required"/>
<label for="firstname">First Name</label>
</p>
<p>
<input name="lastname" type="text" id="lastname" class="InputField required" reqargs="Last Name is required"/>
<label for="lastname">Last Name</label>
</p>
<p>
<input name="email" type="text" id="email"  class="InputField required email" reqargs="Email is required" validargs="Wrong email format"/>
<label for="email">Email</label>
 </p>
<p>
<input name="company" type="text" id="company"  class="InputField"/>
<label for="company">Company </label>
 </p>
<p>
<textarea name="args" id="message" class="InputField required" reqargs="Message is required"></textarea>
<label for="message">Message</label>
 </p>
<p>
<input type="button" id="btnSend" value="Send" class="btn" />
<img id="imgLoading" style="float:left;display:none;" src="../../Advertiser/images/Inqwise_loading.gif" alt="loading..."  />
 </p>
</div>
<script type="text/javascript">

    $(document).ready(function() {

        $("#btnSend").click(function(evt) {

            if (validateContactForm()) {
                $("#imgLoading").show();
                $("#contactargs").hide();
                $.post("async/press/SendInquire.ashx", { firstname: $("#firstname").val(),
                    lastname: $("#lastname").val(),
                    email: $("#email").val(),
                    company: $("#company").val(),
                    message: $("#args").val()
                },
                function(data) {
                    $("#imgLoading").hide();
                    $("#contactargs").slideDown("slow");
                    if (data == "success") {
                        $("#contactargs").attr("class", "success");
                        $("#contactargs").html("Inquire is sent successfully");
                        $("#args").val("");
                    }
                    else {
                        $("#contactargs").attr("class", "warning");
                        $("#contactargs").html(data);
                    }
                });
            }

        });
        //        $("#contactform input").each(function(index) {
        //            $(this).bind("blur", function() { validateContactForm(); });
        //        });
        //        $("#contactform textarea").each(function(index) {
        //            $(this).bind("blur", function() { validateContactForm(); });
        //        });
    });
    
    function isValidEmailAddress(emailAddress) {
        var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
        return pattern.test(emailAddress);
    };

    var lastErrorMessage = "";
    function validateContactForm() {

        var errorMessage = "";
        var iserror = false;
        $("#contactform .required").each(function(index) {
            //alert(index + ': ' + $(this).text());
            if ($.trim($(this).val()) == "") {
                iserror = true;
                errorMessage += $(this).attr("reqargs") + "<br />";
                //$("#contactargs").html($("#contactargs").html() + $(this).attr("reqargs") + "<br />");
                $(this).css("border", "2px solid #E09B85");
                //$(this).effect("shake", { times: 3 }, 150);
                $(this).effect("highlight", { times: 3 }, 400);
            }
            else {
                $(this).css("border", "2px solid #DEDEDD");
            }
        });

        if (!iserror) {
            $("#contactform .email").each(function(index) {
                if (!isValidEmailAddress($(this).val())) {
                    iserror = true;
                    errorMessage += $(this).attr("validargs") + "<br />";
                    //$("#contactargs").html($("#contactargs").html() + $(this).attr("validargs") + "<br />");
                    $(this).css("border", "2px solid #E09B85");
                    //$(this).effect("shake", { times: 3 }, 150);
                    $(this).effect("highlight", { times: 3 }, 400);
                }
                else {
                    $(this).css("border", "2px solid #DEDEDD");
                }
            });
        }
//        if (!iserror) {
//            $("#contactargs").attr("class", "success");
//            $("#contactargs").html("Form is correct");
//            
//        }
//        else {
//           
//        }

        if (iserror) {
            $("#contactargs").attr("class", "warning");
            if (errorMessage != lastErrorMessage) {
                lastErrorMessage = errorMessage;
                //$("#contactargs").hide();
                $("#contactargs").html(errorMessage);
                if ($("#contactargs").css("display") == "none")
                    $("#contactargs").slideDown("slow");
            }
        }
        
        return !iserror;
    }
</script>
