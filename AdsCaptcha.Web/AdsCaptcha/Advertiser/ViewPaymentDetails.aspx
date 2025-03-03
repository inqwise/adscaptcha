<%@ Page EnableViewStateMac="false" Language="C#" AutoEventWireup="true" CodeFile="ViewPaymentDetails.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.ViewPaymentDetails" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head>
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <meta http-equiv="Content-Language" content="en-us" />
    
    <title>Payment Details</title>
    <link href="../css/layout.css" type="text/css" rel="stylesheet" />
    
    <style type="text/css">
        body { padding:5px; }
        th { font-weight:bold;text-align:right;vertical-align:top; }
        td { text-align:left;vertical-align:top;}
        .close { position:absolute;top:10px;right:10px; }               
        .close a, .close a:hover { color:#1cbbb4;text-decoration:underline; }        
        .content { padding:10px 0px 10px 0px; }
        .footer { position:absolute;bottom:0px; }
    </style>
</head>

<body>
    
    <div class="close">
        <a href="javascript:window.print();">Print</a> | <a href="javascript:window.close();">Close</a>
    </div>
    
    <div>
        <img src="../images/logo.png" alt="AdsCaptcha" />
    </div>

    <div class="content">
        <asp:Table ID="tablePaymentDetails" runat="server" CellSpacing="6">
        </asp:Table>
    </div>

</body>

</html>