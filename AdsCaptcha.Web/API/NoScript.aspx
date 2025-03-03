<!--%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoScript.aspx.cs" Inherits="Inqwise.AdsCaptcha.API.NoScript" ValidateRequest="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head>
    <title>AdsCaptcha</title>
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />

    <style type="text/css">
        * {font-family:Arial;font-size:12px;color:#000;border:0 none;}
        body {width:100%;height:100%;padding:0;margin:0;direction:ltr;background-color:#ffffff;}
        #adscaptcha_widget {position:relative;height:90px;padding:2px}
        table, tr, td {width:100%;padding:0;margin:0;text-align:left;vertical-align:top;}
        .label {width:100px;font-weight:bold;}
        .code {font-size:100%;text-align:left;}
        .about {position:relative;bottom:0;right:0;text-align:right;}
    </style>
</head>

<body>
    <asp:Panel ID="panelCaptcha" runat="server">
        <div id="adscaptcha_widget">
            <table id="adscaptcha_table" cellpadding='0' cellspacing='2'>
                <tr>
                    <th class="label">Challenge:</th>
                    <td><img src="<!--%=//API_URL%>/Challenge.aspx?cid=<!--%//=_ChallengeCode%>&w=<!--%//=_Width%>" width="<!--%//=_Width%>" height="30" alt="AdsCaptcha Challenge"/></td>
                </tr>
                <tr>
                    <th class="label">Code:</th>
                    <td class="code"><!--%//=_ChallengeCode%></td>
                </tr>
            </table>
            <div class="about">
                <a href='http://www.com' target='_blank'><img src="images/default/powered_by_adscaptcha.gif" alt="Captcha" /></a></td>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="panelError" runat="server" Visible="false">
        <asp:Label ID="labelError" runat="server"></asp:Label>
    </asp:Panel>
</body-->