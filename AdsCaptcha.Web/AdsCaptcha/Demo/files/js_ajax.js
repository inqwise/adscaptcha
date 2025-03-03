
function ajaxFunction(action,params)
{   
    document.getElementById("username_status").style.display="none";
    if ((action != 'check_email' && action != 'check_username' && action != 'send_sms_verification' && action != 'validate_sms_verification_code' && action != 'destroy_session' && action != 'setHTML') || params[0] == '')
    {
        return false;
    }
    
    var xmlHttp;
    try 
    { 
        // Firefox, Opera 8.0+, Safari
        xmlHttp=new XMLHttpRequest();
    }
    catch (e)
    {
        // Internet Explorer
        try
        {
            xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e)
        {
            try
            {
            xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (e)
            {
                alert("Your browser does not support AJAX!");
                return false;
            }
        }
    } 
    xmlHttp.onreadystatechange=function()
    {
        if(xmlHttp.readyState==4)
        {
            actualresponse=xmlHttp.responseText;
            cleanresponse=actualresponse.replace(new RegExp("[\\n]+","g"),"");
            cleanresponse=cleanresponse.replace(new RegExp("[\\r]+","g"),"");

            if (action == 'check_email') {
                document.getElementById("email_status").innerHTML=cleanresponse;
            } else if (action == 'send_sms_verification') {
                document.getElementById("email_status").innerHTML=cleanresponse;
            } else if (action == 'validate_sms_verification_code') {
                document.getElementById("email_status").innerHTML=cleanresponse;
            } else if (action == 'check_username') {
                document.getElementById("username_status").innerHTML=cleanresponse;
                if (cleanresponse == "") {
					document.getElementById("username_status").style.display="none";
               	}else{
					document.getElementById("username_status").style.display="block";
				}
            }
        }
    }
    post_params=null;
    if (action == 'check_email') {
        xmlHttp.open("POST","/svc_signup/ajax.php",true);
        post_params="email="+encodeURIComponent(params[0])+"&gcm=g_check_email";
    } else if (action == 'send_sms_verification') {
        xmlHttp.open("POST","/svc_signup/ajax.php",true);
        post_params="cell_number="+encodeURIComponent(params[0])+"&cell_carrier="+encodeURIComponent(params[1])+"&fname="+encodeURIComponent(params[2])+"&lname="+encodeURIComponent(params[3])+"&email="+encodeURIComponent(params[4])+"&gcm=g_send_verification";
    } else if (action == 'validate_sms_verification_code') {
        xmlHttp.open("POST","/svc_signup/ajax.php",true);
        post_params="verification_code="+encodeURIComponent(params[0])+"&gcm=g_verify_received_code";
    } else if (action == 'destroy_session') {
        xmlHttp.open("POST","/svc_signup/ajax.php",true);
        post_params="gcm=g_destroy_session";
    } else if (action == 'check_username') {
        var path = 'web_signup_service/web_ajax_user.php?gcm=g_check_username&username=' + params[0];
        var url = '/svc_signup/pajax.php?path=' + encodeURIComponent(path);
        xmlHttp.open("POST",url,true);
        post_params="username="+encodeURIComponent(params[0])+"&gcm=g_check_username";
    }
    xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
    xmlHttp.send(post_params);
}


