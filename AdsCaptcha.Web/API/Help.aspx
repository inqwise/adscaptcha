<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="Inqwise.AdsCaptcha.API.Help" %>
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
            .logo {margin-left:10px;}        
        .footer {width:100%;background-color:#eee;height:40px;text-align:center;}
            .close {color:#1cbbb4;font-weight:bold;text-shadow:#ccc 0px 1px 1px;line-height:40px;}
            a.close, a:hover.close {text-decoration:none;}            
        .content {padding:20px;}
        h2 {color:#1cbbb4;font-size:130%;font-weight:bold;}
        li {list-style-type:circle;margin-left:18px;}        
        .RTL {direction:rtl;}
    </style>
</head> 
  
<body id="Body" runat="server">
    <div class="header"> 
        <a href="http://www.com">
            <img class="logo" src="images/logo.jpg" alt="AdsCaptcha" width="260" height="80"> 
        </a>
    </div> 
    <div class="content">
        <asp:Panel ID="langEN" runat="server" Visible="false">
            <h2>Instructions</h2>
            <ul>
                <li>Please type the words that appear in the picture, in the correct order, and separated by spaces. This helps us to prevent automated programs from gaining access to the site.</li>
                <li>If you are having trouble figuring out what the words are, either type in your best guess, or click the refresh button next to get a new picture.</li>
                <li>Still need help? <a href="http://www.com" target="_blank">Contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langHE" runat="server" Visible="false">
            <h2>הוראות</h2>
            <ul>
                <li>אנא הקלד את המילים המופיעות בתמונה, בסדר הנכון ומופרדים ע"י רווחים. בדיקה זו מסייעת לנו למנוע ניסיון פריצה לאתר.</li>
                <li>אם אתה מתקשה לפענח את אחת המילים, הקלד את הניחוש הטוב ביותר שלך, או לחץ על כפתור הטעינה מחדש לקבלת תמונה חדשה.</li>
                <li>עדין זקוק לעזרה? <a href="http://www.com" target="_blank">צור קשר</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langFR" runat="server" Visible="false">
            <h2>Instructions</h2>
            <ul>
                <li>Veuillez entrez les mots qui apparaissent dans l'image, dans le bon ordre, et séparés par des espaces. Cela nous aide à empêcher des programmes automatiques d'accéder au site.</li>
                <li>Si vous éprouvez des difficultés à lire les mots, soit entrez votre meilleure estimation, ou cliquez sur le bouton d'actualisation pour obtenir une nouvelle image.</li>
                <li>Besoin d'aide? <a href="http://www.com" target="_blank">Contactez-nous</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langRU" runat="server" Visible="false">
            <h2>Инструкции</h2>
            <ul>
                <li>Пожалуйста, введите слова, которые появляются на картинке, в правильном порядке и разделенные пробелами. Это помогает нам предотвратить доступ автоматизированных программ к сайту.</li>
                <li>Если у вас возникли проблемы с чтением слов, попытайтесь ввесте слова как вы их поняли, или нажмите кнопку "Обновить" чтобы получить новую картинку.</li>
                <li>Все еще нужна помощь? <a href="http://www.com" target="_blank">Свяжитесь с нами</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langNL" runat="server" Visible="false">
            <h2>Instructions</h2>
            <ul>
                <li>Please type the words that appear in the picture, in the correct order, and separated by spaces. This helps us to prevent automated programs from gaining access to the site.</li>
                <li>If you are having trouble figuring out what the words are, either type in your best guess, or click the refresh button next to get a new picture.</li>
                <li>Still need help? <a href="http://www.com" target="_blank">Contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langDE" runat="server" Visible="false">
            <h2>Instructions</h2>
            <ul>
                <li>Please type the words that appear in the picture, in the correct order, and separated by spaces. This helps us to prevent automated programs from gaining access to the site.</li>
                <li>If you are having trouble figuring out what the words are, either type in your best guess, or click the refresh button next to get a new picture.</li>
                <li>Still need help? <a href="http://www.com" target="_blank">Contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langPT" runat="server" Visible="false">
            <h2>Instructions</h2>
            <ul>
                <li>Please type the words that appear in the picture, in the correct order, and separated by spaces. This helps us to prevent automated programs from gaining access to the site.</li>
                <li>If you are having trouble figuring out what the words are, either type in your best guess, or click the refresh button next to get a new picture.</li>
                <li>Still need help? <a href="http://www.com" target="_blank">Contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langES" runat="server" Visible="false">
            <h2>Instructions</h2>
            <ul>
                <li>Please type the words that appear in the picture, in the correct order, and separated by spaces. This helps us to prevent automated programs from gaining access to the site.</li>
                <li>If you are having trouble figuring out what the words are, either type in your best guess, or click the refresh button next to get a new picture.</li>
                <li>Still need help? <a href="http://www.com" target="_blank">Contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langIT" runat="server" Visible="false">
            <h2>Instructions</h2>
            <ul>
                <li>Please type the words that appear in the picture, in the correct order, and separated by spaces. This helps us to prevent automated programs from gaining access to the site.</li>
                <li>If you are having trouble figuring out what the words are, either type in your best guess, or click the refresh button next to get a new picture.</li>
                <li>Still need help? <a href="http://www.com" target="_blank">Contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langHI" runat="server" Visible="false">
            <h2>Instructions</h2>
            <ul>
                <li>Please type the words that appear in the picture, in the correct order, and separated by spaces. This helps us to prevent automated programs from gaining access to the site.</li>
                <li>If you are having trouble figuring out what the words are, either type in your best guess, or click the refresh button next to get a new picture.</li>
                <li>Still need help? <a href="http://www.com" target="_blank">Contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langEL" runat="server" Visible="false">
            <h2>Instructions</h2>
            <ul>
                <li>Please type the words that appear in the picture, in the correct order, and separated by spaces. This helps us to prevent automated programs from gaining access to the site.</li>
                <li>If you are having trouble figuring out what the words are, either type in your best guess, or click the refresh button next to get a new picture.</li>
                <li>Still need help? <a href="http://www.com" target="_blank">Contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langTR" runat="server" Visible="false">
            <h2>Instructions</h2>
            <ul>
                <li>Please type the words that appear in the picture, in the correct order, and separated by spaces. This helps us to prevent automated programs from gaining access to the site.</li>
                <li>If you are having trouble figuring out what the words are, either type in your best guess, or click the refresh button next to get a new picture.</li>
                <li>Still need help? <a href="http://www.com" target="_blank">Contact us</a>.</li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="langRO" runat="server" Visible="false">
            <h2>Instructions</h2>
            <ul>
                <li>Please type the words that appear in the picture, in the correct order, and separated by spaces. This helps us to prevent automated programs from gaining access to the site.</li>
                <li>If you are having trouble figuring out what the words are, either type in your best guess, or click the refresh button next to get a new picture.</li>
                <li>Still need help? <a href="http://www.com" target="_blank">Contact us</a>.</li>
            </ul>
        </asp:Panel>
    </div>
    <!--
    <div class="footer"> 
        <a href="javascript:window.close();" class="close" title="Close">CLOSE</a> 
    </div> 
    -->
     
</body> 
</html>