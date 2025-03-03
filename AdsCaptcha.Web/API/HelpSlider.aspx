<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HelpSlider.aspx.cs" Inherits="Inqwise.AdsCaptcha.API.HelpSlider" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head" runat="server">
    <title>Help | AdsCaptcha</title> 
    <meta http-equiv="Content-Type" content="text/html; charset=Windows-1255"/> 
    <link rel="Stylesheet" href="http://livedemo.com/css/Inqwise/css/layout.css" />
    <style type="text/css">
        * {margin:0;padding:0;font-family:Arial;font-size:14px;line-height:160%;border:none 0!important;}
        body {width:100%;height:100%;background-color:#fefefe;}
        .header {width:100%;background:#eee url(images/header_bg.jpg) repeat-x;height:86px;}      
        .content {padding:20px;}
        h2 {color:#1cbbb4;font-size:130%;font-weight:bold;}
        li {list-style-type:circle;margin-left:18px;font-size:13px;}        
        .RTL {direction:rtl;}
        #inner-header {
    height: 70px;
    margin: 0 0 0 20px;
    padding-bottom: 5px;
    width: 940px;
}
h4
{
line-height: 55px;
}
a {
    color: #2CB677;
}
a {
    text-decoration: none;
}

a:hover {
    text-decoration: underline;
}
    </style>
    

</head> 
  
<body id="Body" runat="server">
    <!--div id="header">
        <div id="inner-header" style="width:90%;">
        <a href="http://www.Inqwise.com" target="_blank">
            <img class="logo" src="http://livedemo.com/css/Inqwise/images/logo.png" alt="Inqwise"> 
        </a>
        </div>
    </div> 
    <div class="clear"></div-->
    <div id="content" class="content" style="width:630px;">
    <div  style="float:left;width:340px;margin:5px;">
        <asp:Panel ID="langEN" runat="server" Visible="false">
            <h4>Instructions</h4>
            <ul>
                <li>Rearrange the picture on the slider to form a complete image. Use your <b>mouse</b> or the <b>arrow keys</b> to drag the sliding button below the image to the left or right until the picture is equally aligned.</li>
                <li>Sliding to normalize this image helps us to ensure that you are a human, and not an automated computer hacker.</li>
                <li>If you have difficulty arranging this image you can select a new image by pressing refresh button or refreshing your web page.</li>
                <li>If you need further assistance please <a href="http://www.Inqwise.com" target="_blank">contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langHE" runat="server" Visible="false">
            <h4 style="background-image:none;">הוראות</h4>
            <ul>
                <li>סדר/י מחדש את התמונה באמצעות הזזת הסליידר בעזרת העכבר או בעזרת שימוש בחצים ימינה/ שמאלה עד להשלמת הפאזל. </li>
                <li>השלמת הפאזל עוזרת לנו לאשר שאת/ה אנושי/ת ולא רובוט או האקר.</li>
				<li>אם את/ה מתקשה לסדר את התמונה, תוכל/י ללחוץ על כפתור רענון  <img src="images/default/button_refresh.gif" />  ולבחור פאזל חדש או ללחוץ על כפתור "הצג תווים אקראיים" <img src="images/default/button_security.gif" /> ולהקליד את התווים המופיעים בתמונה.</li>
                <li>לעזרה נוספת, אנא <a href="http://www.Inqwise.com" target="_blank">צור קשר</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langFR" runat="server" Visible="false">
            <h4>Instructions</h4>
            <ul>
                <li>Accommoder l'image avec le curseur pour former une image complète. Utilisez votre souris ou les touches fléchées pour faire glisser le bouton coulissant sous l'image à gauche ou à droite jusqu'à ce que l'image soit alignée. 
                <li>La réalisation de ce puzzle nous aide à vous assurer que vous êtes un être humain, et non un pirate informatique automatisé.</li>
                <li>Si vous avez du mal à arranger cette image, vous pouvez sélectionner une nouvelle image en cliquant sur le bouton d'actualisation ou en rafraîchissant votre page web.</li>
                <li>Si vous avez besoin d'aide veuillez <a href="http://www.Inqwise.com" target="_blank">nous contacter</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langRU" runat="server" Visible="false">
            <h4>Инструкции</h4>
            <ul>
                <li>Перестановка изображение на слайдере, чтобы сформировать полный образ. С помощью мышки или клавиш со стрелками перетащите слайдер ниже изображения влево или вправо, пока картинка не выровнена. 
                <li>этой головоломки помогает нам убедиться, что вы являетесь человеком, а не автоматизированной компьютерным хакером.</li>
                <li>Если у вас возникли трудности с организацией картинки вы можете выбрать новое изображение, нажав кнопку "Обновить" или обновлением вашей веб-страницы.</li>
                <li>Все еще нужна помощь? <a href="http://www.Inqwise.com" target="_blank">Свяжитесь с нами</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langNL" runat="server" Visible="false">
            <h4>Instructions</h4>
            <ul>
                <li>Rearrange the picture on the slider to form a complete image. Use your mouse or the arrow keys to drag the sliding button below the image to the left or right until the picture is equally aligned. 
                <li>Completing this puzzle helps us to ensure that you are a human, and not an automated computer hacker.</li>
                <li>If you have difficulty arranging this image you can select a new image by pressing refresh button or refreshing your web page.</li>
                <li>If you need further assistance please <a href="http://www.Inqwise.com" target="_blank">contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langDE" runat="server" Visible="false">
            <h4>Instructions</h4>
            <ul>
                <li>Rearrange the picture on the slider to form a complete image. Use your mouse or the arrow keys to drag the sliding button below the image to the left or right until the picture is equally aligned. 
                <li>Completing this puzzle helps us to ensure that you are a human, and not an automated computer hacker.</li>
                <li>If you have difficulty arranging this image you can select a new image by pressing refresh button or refreshing your web page.</li>
                <li>If you need further assistance please <a href="http://www.Inqwise.com" target="_blank">contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langPT" runat="server" Visible="false">
            <h4>Instructions</h4>
            <ul>
                <li>Rearrange the picture on the slider to form a complete image. Use your mouse or the arrow keys to drag the sliding button below the image to the left or right until the picture is equally aligned. 
                <li>Completing this puzzle helps us to ensure that you are a human, and not an automated computer hacker.</li>
                <li>If you have difficulty arranging this image you can select a new image by pressing refresh button or refreshing your web page.</li>
                <li>If you need further assistance please <a href="http://www.Inqwise.com" target="_blank">contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langES" runat="server" Visible="false">
            <h4>Instructions</h4>
            <ul>
                <li>Alinea las piezas para formar una imagen completa.  Use su ratón o las flechas de su teclado para arrastrar el botón que encuentre por debajo de la imagen hacia la izquierda o la derecha hasta que la imagen este alineada. 
                <li>Completar este puzzle nos ayuda a confirmar que Ud. es humano y no una maquina automatizada.</li>
                <li>Si encuentra problemas en alinear esta imagen podrá elegir una foto alternativa recargando la hoja en la que se encuentra.</li>
                <li>Si requiere asistencia adicional por favor <a href="http://www.Inqwise.com" target="_blank">contáctenos</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langIT" runat="server" Visible="false">
            <h4>Instructions</h4>
            <ul>
                <li>Rearrange the picture on the slider to form a complete image. Use your mouse or the arrow keys to drag the sliding button below the image to the left or right until the picture is equally aligned. 
                <li>Completing this puzzle helps us to ensure that you are a human, and not an automated computer hacker.</li>
                <li>If you have difficulty arranging this image you can select a new image by pressing refresh button or refreshing your web page.</li>
                <li>If you need further assistance please <a href="http://www.Inqwise.com" target="_blank">contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langHI" runat="server" Visible="false">
            <h4>Instructions</h4>
            <ul>
                <li>Rearrange the picture on the slider to form a complete image. Use your mouse or the arrow keys to drag the sliding button below the image to the left or right until the picture is equally aligned. 
                <li>Completing this puzzle helps us to ensure that you are a human, and not an automated computer hacker.</li>
                <li>If you have difficulty arranging this image you can select a new image by pressing refresh button or refreshing your web page.</li>
                <li>If you need further assistance please <a href="http://www.Inqwise.com" target="_blank">contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langEL" runat="server" Visible="false">
            <h4>Instructions</h4>
            <ul>
                <li>Rearrange the picture on the slider to form a complete image. Use your mouse or the arrow keys to drag the sliding button below the image to the left or right until the picture is equally aligned. 
                <li>Completing this puzzle helps us to ensure that you are a human, and not an automated computer hacker.</li>
                <li>If you have difficulty arranging this image you can select a new image by pressing refresh button or refreshing your web page.</li>
                <li>If you need further assistance please <a href="http://www.Inqwise.com" target="_blank">contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langTR" runat="server" Visible="false">
            <h4>Instructions</h4>
            <ul>
                <li>Rearrange the picture on the slider to form a complete image. Use your mouse or the arrow keys to drag the sliding button below the image to the left or right until the picture is equally aligned. 
                <li>Completing this puzzle helps us to ensure that you are a human, and not an automated computer hacker.</li>
                <li>If you have difficulty arranging this image you can select a new image by pressing refresh button or refreshing your web page.</li>
                <li>If you need further assistance please <a href="http://www.Inqwise.com" target="_blank">contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langRO" runat="server" Visible="false">
            <h4>Instructions</h4>
            <ul>
                <li>Rearrange the picture on the slider to form a complete image. Use your mouse or the arrow keys to drag the sliding button below the image to the left or right until the picture is equally aligned. 
                <li>Completing this puzzle helps us to ensure that you are a human, and not an automated computer hacker.</li>
                <li>If you have difficulty arranging this image you can select a new image by pressing refresh button or refreshing your web page.</li>
                <li>If you need further assistance please <a href="http://www.Inqwise.com" target="_blank">contact us</a>.</li>
            </ul>
        </asp:Panel>
    </div>
      <div style="float:left;width:270px;margin:15px 5px 5px 5px;cursor:pointer;"><img name="imgAnimated" src="" onclick="ReloadImg();" /></div>
    </div>
  
         <script type="text/javascript" >

             function ReloadImg() {
                 var datetime = new Date();
                 var imageHelpSrc = "images/miteye_amimated.gif?dumm=" + datetime.getTime();
                 var images = document.getElementsByName("imgAnimated");
                 for (var i = 0; i < images.length; i++) {
                     images[i].src = imageHelpSrc;
                 }

             }
             ReloadImg();
    </script>
</body> 
</html>