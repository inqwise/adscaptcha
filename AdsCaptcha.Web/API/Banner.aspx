<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Banner.aspx.cs" Inherits="Inqwise.AdsCaptcha.API.Banner" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head>
    <title>AdsCaptcha</title>

    <style type="text/css">
        *, body {width:100%;height:100%;padding:0;margin:0;}
    </style>
    
    <script type="text/javascript">
        function getParamValue(param) {
            var loc = location.search.substring(1, location.search.length);
            var paramValue;
            var params = loc.split('&')
            for (i = 0; i < params.length; i++) {
                paramName = params[i].substring(0, params[i].indexOf('='));
                if (paramName == param) {
                    paramValue = params[i].substring(params[i].indexOf('=') + 1);
                }
            }
            return paramValue;
        }

        function isHTTPS() {
            try {
                var url = document.location.toString();
                if (url.match('^https://')) {
                    return true;                    
                }
                return false;
            } catch (e) {
                return false;
            }
        }
    </script>
</head>

<body>
    <script type="text/javascript">
        var AD_SERVER = '<%=ConfigurationSettings.AppSettings["YBrantURL"]%>';

        if (isHTTPS()) {
            AD_SERVER = AD_SERVER.replace('http://', 'https://');
        }

        var day = '&banned_pop_types=28&pop_frequency=86400';
        var hour = '&banned_pop_types=29&pop_frequency=3600';
        var minute = '&banned_pop_types=29&pop_frequency=60';

        var src = AD_SERVER + '&ad_type=pop&ad_size=0x0&pop_times=1' + minute;


        document.write('<scr' + 'ipt type="text/javascript" src="' + src + '"></scr' + 'ipt>');
    </script>
</body>