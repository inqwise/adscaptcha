<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="LiveDemo.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AdsCaptcha Demo</title>
    <style>
        body { font-family:Verdana, Arial; font-size:16px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Enter your CAPTCHA ID: <asp:TextBox ID="textCaptchaId" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="buttonGenerate" runat="server" Text="Generate" 
            onclick="buttonGenerate_Click" style="height: 26px" />
    </div>
    </form>
</body>
</html>
