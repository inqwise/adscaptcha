<%@ Page EnableViewStateMac="false" Language="C#" AutoEventWireup="true" CodeFile="PayPalGateway.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.PayPalGateway" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <script type="text/javascript">
        window.onload = function() {
            document.forms[0].submit();
        }
    </script>
</head>

<body>
	<form action="" method="post" name="PayPalForm" runat="server" id="PayPalForm">
		<input type="hidden" name="cmd" value="_xclick" id="cmd" />
		<input type="hidden" name="no_note" value="1" id="no_note" />
		<input type="hidden" name="currency_code" value="USD" id="currency_code" />
		<input type="hidden" name="no_shipping" value="1" id="no_shipping" />
		<input type="hidden" runat="server" name="item_number" value="" id="item_number" />
		<input type="hidden" runat="server" name="amount" value="" id="amount" />
		<input type="hidden" runat="server" name="business" value="" id="business" />
		<input type="hidden" runat="server" name="item_name" value="" id="item_name" />
		<input type="hidden" runat="server" name="page_style" value="" id="page_style" />
		<input type="hidden" runat="server" name="address1" value="" id="address1" />
		<input type="hidden" runat="server" name="city" value="" id="city" />
		<input type="hidden" runat="server" name="country" value="" id="country" />
		<input type="hidden" runat="server" name="email" value="" id="email" />
		<input type="hidden" runat="server" name="first_name" value="" id="first_name" />
		<input type="hidden" runat="server" name="last_name" value="" id="last_name" />
		<input type="hidden" runat="server" name="state" value="" id="state" />
		<input type="hidden" runat="server" name="zip" value="" id="zip" />
		<input type="hidden" runat="server" name="night_phone_b" value="" id="night_phone_b" />
		<input type="hidden" runat="server" name="notify_url" id="notify_url" />
   </form>
</body>

</html>

