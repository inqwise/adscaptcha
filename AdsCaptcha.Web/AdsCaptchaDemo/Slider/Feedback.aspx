<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="Slider_Feedback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Inqwise Feedback</title>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.2.min.js" type="text/javascript"></script>
    <style type="text/css">
       input, textarea {
	padding: 4px;
	border: solid 1px #E5E5E5;
	outline: 0;
	font: normal 13px/100% Verdana, Tahoma, sans-serif;
	width: 200px;
	background: #FFFFFF url('bg_form.png') left top repeat-x;
	background: -webkit-gradient(linear, left top, left 10, from(#FFFFFF), color-stop(4%, #EEEEEE), to(#FFFFFF));
	background: -moz-linear-gradient(top, #FFFFFF, #EEEEEE 1px, #FFFFFF 10px);
	box-shadow: rgba(0,0,0, 0.1) 0px 0px 8px;
	-moz-box-shadow: rgba(0,0,0, 0.1) 0px 0px 8px;
	-webkit-box-shadow: rgba(0,0,0, 0.1) 0px 0px 8px;
	width: 280px;
	}
textarea {
	
	max-width: 300px;
	height: 70px;
	line-height: 150%;
	}
input:hover, textarea:hover,
input:focus, textarea:focus {
	border-color: #C9C9C9;
	-webkit-box-shadow: rgba(0, 0, 0, 0.15) 0px 0px 8px;
	}
.label, table th {
	margin-left: 10px;
	color: #999999;
	}
	table th 
	{
		font-weight:bold;
	}
.submit{
	width: auto;
	padding: 6px 15px;
	background: #617798;
	border: 0;
	font-size: 14px;
	color: #FFFFFF;
	-moz-border-radius: 5px;
	-webkit-border-radius: 5px;
	border-radius: 5px;
	cursor:pointer;
	}
	
	.success {
	background: #C0F0B3 url('http://www.Inqwise.com/css/Inqwise/images/success.png') no-repeat 20px center;
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
  width:200px;
  }
  .warning {
  background: #FFCEBE url('http://www.Inqwise.com/css/Inqwise/images/warning.png') no-repeat 20px center;
  border: 1px solid #E09B85;
  border-radius: 10px;
  -moz-border-radius: 10px;
  -webkit-border-radius: 10px;
  text-shadow: 1px 1px 1px #fff;
  color: #957368;
  padding: 8px 20px 4px 75px;
  margin: 10px 0px;
  min-width: 32px;
  min-height: 50px;
   font-size:14px;
   width:200px;
   margin-left:7px;
  }

.adscap_box_close 
{
	position:absolute; top:-10px; right:-10px; 
	width:30px; height:30px; cursor:pointer; 
	background:url(GetCSSImage.ashx?w=180&n=close&e=png) 
	no-repeat;z-index:9999
}

    </style>
    <script type="text/javascript">
        function isValidEmailAddress(emailAddress) {
            var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
            return pattern.test(emailAddress);
        };
        var currentErrorMessage;
        function Validate() {

            var errorMessage = "";
            var iserror = false;
            $("#feedbackForm .required").each(function (index) {
                //alert(index + ': ' + $(this).text());
                if ($.trim($(this).val()) == "") {
                    iserror = true;
                    errorMessage += $(this).attr("reqargs") + "<br />";
                    //$("#contactargs").html($("#contactargs").html() + $(this).attr("reqargs") + "<br />");
                    $(this).css("border", "1px solid #E09B85");
                    //$(this).effect("shake", { times: 3 }, 150);
                    //$(this).effect("highlight", { times: 3 }, 400);
                }
                else {
                    $(this).css("border", "1px solid #E5E5E5");
                }
            });

            if (!iserror) {
                $("#feedbackForm .email").each(function (index) {
                    if (!isValidEmailAddress($(this).val())) {
                        iserror = true;
                        errorMessage += $(this).attr("validargs") + "<br />";
                        //$("#contactargs").html($("#contactargs").html() + $(this).attr("validargs") + "<br />");
                        $(this).css("border", "1px solid #E09B85");
                        //$(this).effect("shake", { times: 3 }, 150);
                        //$(this).effect("highlight", { times: 3 }, 400);
                    }
                    else {
                        $(this).css("border", "1px solid #E5E5E5");
                    }
                });
            }

            if (iserror) {
                $("#errorMessage").html(errorMessage);
                $("#error").fadeIn();

            }

            return !iserror;
        }

        $(document).ready(function () {
            $("#btnSend").click(function () {
                if (Validate()) {
                    $("#imgLoading").show();
                    var dataToSubmit = 'name=' + encodeURI($("#txtName").val());
                    dataToSubmit += "&email=" + encodeURI($("#txtEmail").val());
                    dataToSubmit += "&args=" + encodeURI($("#txtMessage").val());
                    dataToSubmit += "&referrer=" + encodeURI(window.parent.window.location);
                    $.ajax({
                        type: 'POST',
                        url: 'SubmitFeedback.ashx',
                        data: dataToSubmit,
                        success: function (data) {
                            $("#imgLoading").hide();
                            if (data == '0') {
                                $("#feedbackForm").fadeOut(function () {
                                    $("#feedBackResult").fadeIn();
                                });
                            }
                            else {
                                $("#errorMessage").html(errorMessage);
                                $("#error").fadeIn();
                               
                            }
                        }
                    });




                }
            });
            $("#errorClose").click(function () {
                $("#error").hide();
            });
        });
    </script>
</head>
<body>

    <asp:Panel id="form1" runat="server"  style="width:300px;height:300px;overflow:hidden;">
    <div id="feedbackForm">
    <table>
        <tr>
            <th>Do you have a trouble with captcha? Leave your feedback:</th>
        </tr>
        <tr>
            <td class="label">name</td>
        </tr>
        <tr>
            <td>
                <input id="txtName" class="required" type="text" placeholder="John Smith" reqargs="Name is required" /></td>
        </tr>
        <tr>
            <td class="label">email</td>
        </tr>
        <tr>
            <td>
                <input id="txtEmail" class="required email" type="text" placeholder="john@domain.com" reqargs="Email is required" validargs="Wrong email format" /></td>
        </tr>
        <tr>
            <td class="label">args</td>
        </tr>
        <tr>
            <td class="style1">
                <textarea id="txtMessage" class="required" name="txtMessage" placeholder="Your args" reqargs="Message is required"></textarea></td>
        </tr>
        <tr>
            <td>
                <input id="btnSend" type="button" class="submit" value="Send feedback" style="float:left;" />
                <img id="imgLoading" src="css/300x250/images/Inqwise_loading.gif" alt="sending..." style="width:32px;height:32px;float:left;display:none;" />
                </td>
        </tr>
    </table>
    </div>

 <div id="feedBackResult" class="success" style="display:none;">
    You feedback is sent successfully. We will answer as soon as possible.
    </div>
    <div id="error" class="warning" style="display:none;position:absolute;top:40px;left:0;z-index:10;">
        <div id="errorClose" class="adscap_box_close"></div>
        <div id="errorMessage"></div>
    </div>
    </asp:Panel>
       
</body>
</html>
