//var templateFacebookLike = "<iframe src='http://www.facebook.com/plugins/like.php?href=http%3A%2F%2Fwww.facebook.com%2Fpages%2FAdsCaptcha%2F158921537481633&amp;layout=button_count&amp;show_faces=false&amp;width=120&amp;action=like&amp;font=arial&amp;colorscheme=light&amp;height=21&amp;locale=P_LOCALE' scrolling='no' frameborder='0' style='border:none;overflow:hidden;width:120px;height:21px;' allowTransparency='true'></iframe>";
var templateFacebookLike = "<iframe src='//www.facebook.com/plugins/like.php?href=XXX&amp;send=false&amp;layout=button_count&amp;width=120&amp;show_faces=true&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21&amp;locale=P_LOCALE' scrolling='no' frameborder='0' style='border:none; overflow:hidden; width:120px; height:21px;' allowTransparency='true'></iframe>";
var templateFacebookDefaultPage = "http%3A%2F%2Fwww.facebook.com%2Fpages%2FAdsCaptcha%2F158921537481633";
var templateReset = "#adscaptcha_widget div,#adscaptcha_widget input,#adscaptcha_widget span,#adscaptcha_widget object,#adscaptcha_widget iframe,#adscaptcha_widget p,#adscaptcha_widget a,#adscaptcha_widget a:hover,#adscaptcha_widget font,#adscaptcha_widget img,#adscaptcha_widget form,#adscaptcha_widget label,#adscaptcha_widget table,#adscaptcha_widget tbody,#adscaptcha_widget tfoot,#adscaptcha_widget thead,#adscaptcha_widget tr,#adscaptcha_widget th,#adscaptcha_widget td{margin:0;padding:0;outline:0;border:0 none!important;font-weight:inherit;font-style:inherit;font-size:100%;font-family:inherit;line-height:1px;vertical-align:baseline;}#adscaptcha_widget:focus {outline:0;}#adscaptcha_widget #adscaptcha_challenge_table tr,#adscaptcha_widget #adscaptcha_challenge_table td,#adscaptcha_widget #adscaptcha_r1 tr,#adscaptcha_widget #adscaptcha_r1 td,#adscaptcha_widget #adscaptcha_r2 tr,#adscaptcha_widget #adscaptcha_r2 td,#adscaptcha_widget #adscaptcha_r3 tr,#adscaptcha_widget #adscaptcha_r3 td,#adscaptcha_widget #adscaptcha_r4 tr,#adscaptcha_widget #adscaptcha_r5 tr,#adscaptcha_widget #adscaptcha_r4 td,#adscaptcha_widget #adscaptcha_r5 td{padding:0px;margin:0px;border-collapse:separate;border:0 none;background-color:Transparent!important;}";
var templateCSS = templateReset + "#adscaptcha_widget{position:relative;direction:P_DIRECTION;background-color:P_WIDGET_BACKGROUND_COLOR!important;border:solid P_WIDGET_BORDER_WIDTHpx P_WIDGET_BORDER_COLOR!important;padding:5px!important;}#adscaptcha_table{width:100%!important;border-collapse:separate!important;border-spacing:0!important;}#adscaptcha_widget #adscaptcha_r1{margin:5px 0 0 0!important;}#adscaptcha_widget #adscaptcha_r1 td{padding-bottom:3px!important;}#adscaptcha_widget #adscaptcha_ad_holder{border:P_AD_BORDER_WIDTHpx solid P_AD_BORDER_COLOR!important;padding:0!important;margin:0 0 5px 0!important;}#adscaptcha_widget #adscaptcha_ad_image{padding:0!important;}#adscaptcha_widget #adscaptcha_challenge_holder{width:100%!important;height:45px!important;vertical-align:middle!important;margin:5px 0 5px 0!important;}#adscaptcha_widget #adscaptcha_instructions_holder,#adscaptcha_widget #adscaptcha_input_holder{width:100%!important;vertical-align:bottom!important;text-align:center!important;}#adscaptcha_widget #adscaptcha_instructions{width:100%!important;font-family:Arial!important;font-size:P_INSTRUCTIONS_FONT_SIZE!important;font-weight:normal!important;height:16px!important;line-height:16px!important;color:P_WIDGET_TEXT_COLOR!important;direction:P_DIRECTION;text-align:P_ALIGN;}#adscaptcha_widget #adscaptcha_response_field{direction:P_DIRECTION;text-align:P_ALIGN;border:solid P_RESPONSE_BORDER_WIDTHpx P_RESPONSE_BORDER_COLOR!important;background-color:P_RESPONSE_BACKGROUND_COLOR!important;background-image:none!important;height:22px!important;line-height:22px!important;vertical-align:middle;padding:0 3px 0 3px!important;font-family:Arial;font-size:12px;color:P_RESPONSE_TEXT_COLOR!important;letter-spacing:0.5px;-moz-border-radius:0!important;-webkit-border-radius:0!important;}#adscaptcha_widget #adscaptcha_buttons_holder{width:50%!important;vertical-align:bottom!important;text-align:P_IALIGN!important;}#adscaptcha_widget #adscaptcha_about_holder{width:50%!important;height:9px!important;vertical-align:bottom!important;text-align:P_ALIGN!important;}#adscaptcha_widget #adscaptcha_about_image{height:5px;}";
var templateHTML = "<div id='adscaptcha_widget'><table id='adscaptcha_table' cellpadding='0' cellspacing='0'><tr id='adscaptcha_r1'><td><table cellpadding='0' cellspacing='0' width='100%'><tr><td id='adscaptcha_about_holder'><a id='adscaptcha_about_button' target='_blank'><img id='adscaptcha_about_image'></a></td><td id='adscaptcha_buttons_holder'><a id='adscaptcha_get_security_button'><img id='adscaptcha_get_security_image' width='22' height='20' /></a>&nbsp;<a id='adscaptcha_refresh_button'><img id='adscaptcha_refresh_image' width='22' height='20' /></a>&nbsp;<a id='adscaptcha_help_button'><img id='adscaptcha_help_image' width='22' height='20' /></a></td></tr></table></td></tr><tr id='adscaptcha_r2'><td id='adscaptcha_ad_holder'><div id='adscaptcha_ad_image'></div></td></tr><tr id='adscaptcha_r3'><td id='adscaptcha_challenge_holder'><div id='adscaptcha_challenge_image'></div></td></tr><tr id='adscaptcha_r4'><td id='adscaptcha_input_holder'><input id='adscaptcha_response_field' name='adscaptcha_response_field' type='text' /><input id='adscaptcha_challenge_field' name='adscaptcha_challenge_field' type='hidden' /></td></tr><tr id='adscaptcha_r5'><td id='adscaptcha_instructions_holder'><span id='adscaptcha_instructions'></span></td></tr></table></div>";
var templateFlashCSS = templateReset + "#adscaptcha_widget{position:relative;direction:P_DIRECTION;background-color:P_WIDGET_BACKGROUND_COLOR!important;border:solid P_WIDGET_BORDER_WIDTHpx P_WIDGET_BORDER_COLOR!important;padding:5px!important;}#adscaptcha_table{width:100%!important;border-collapse:separate!important;border-spacing:0!important;}#adscaptcha_widget #adscaptcha_r1 td,#adscaptcha_widget #adscaptcha_r3 td{padding-bottom:3px!important;}#adscaptcha_widget #adscaptcha_instructions_holder{width:100%!important;vertical-align:middle!important;text-align:center!important;}#adscaptcha_widget #adscaptcha_instructions{font-family:Arial!important;font-size:14px!important;font-weight:bold!important;height:16px!important;line-height:16px!important;color:P_WIDGET_TEXT_COLOR!important;direction:P_DIRECTION!important;}#adscaptcha_widget #adscaptcha_facebook_like_holder{width:60%!important;vertical-align:middle!important;text-align:P_ALIGN!important;}#adscaptcha_widget #adscaptcha_buttons_holder{width:40%!important;vertical-align:middle!important;text-align:P_IALIGN!important;}#adscaptcha_widget #adscaptcha_about_holder{padding:5px;width:100%!important;height:9px!important;vertical-align:bottom;text-align:center!important;}#adscaptcha_widget #adscaptcha_about_image{width:105px;height:5px;}";
var templateFlashInnerHTML = "<table id='adscaptcha_table' cellpadding='0' cellspacing='0'><tr id='adscaptcha_r1'><td><table cellpadding='0' cellspacing='0' width='100%'><tr><td id='adscaptcha_facebook_like_holder'>" + templateFacebookLike + "</td><td id='adscaptcha_buttons_holder'><a id='adscaptcha_get_security_button'><img id='adscaptcha_get_security_image' width='22' height='20' /></a>&nbsp;<a id='adscaptcha_refresh_button'><img id='adscaptcha_refresh_image' width='22' height='20' /></a>&nbsp;<a id='adscaptcha_help_button'><img id='adscaptcha_help_image' width='22' height='20' /></a></td></tr></table></td></tr><tr id='adscaptcha_r2'><td id='adscaptcha_flash_holder'></td></tr><tr id='adscaptcha_r3'><td id='adscaptcha_instructions_holder'><span id='adscaptcha_instructions'></span><input id='adscaptcha_response_field' name='adscaptcha_response_field' type='hidden' /><input id='adscaptcha_challenge_field' name='adscaptcha_challenge_field' type='hidden' /></td></tr><tr id='adscaptcha_r4'><td id='adscaptcha_about_holder'><a id='adscaptcha_about_button' target='_blank'><img id='adscaptcha_about_image' width='105' height='5'></a></td></tr></table>";
var templateFlashHTML = "<div id='adscaptcha_widget'>" + templateFlashInnerHTML + "</div>";

var AdsCaptchaLang_en = { name: "English", facebook: "en_US", instructions: "Type text here", slide: "SLIDE TO FIT", refresh: "Refresh", security: "Show random text", help: "Help", direction: "ltr", align: "left" },
    AdsCaptchaLang_nl = { name: "Dutch", facebook: "nl_NL", instructions: "Typ tekst hier", slide: "SLIDE TO FIT", refresh: "Verversen", security: "Toon willekeurige tekst", help: "Helpen", direction: "ltr", align: "left" },
    AdsCaptchaLang_fr = { name: "French", facebook: "fr_FR", instructions: "Entrez votre texte ici", slide: "Faire glisser et adapter", refresh: "Rafraîchir", security: "Afficher le texte aléatoire", help: "Aider", direction: "ltr", align: "left" },
    AdsCaptchaLang_de = { name: "German", facebook: "de_DE", instructions: "Bitte Text hier eingeben", slide: "SLIDE TO FIT", refresh: "Erfrischen", security: "Show zufälligen Text", help: "Hilfe", direction: "ltr", align: "left" },
    AdsCaptchaLang_pt = { name: "Portuguese", facebook: "pt_BR", instructions: "Digite o texto aqui", slide: "SLIDE TO FIT", refresh: "Refrescar", security: "Mostrar texto aleatório", help: "Ajuda", direction: "ltr", align: "left" },
    AdsCaptchaLang_ru = { name: "Russian", facebook: "ru_RU", instructions: "Введите слово здесь", slide: "SLIDE TO FIT", refresh: "обновление", security: "Показать случайный текст", help: "помощь", direction: "ltr", align: "left" },
    AdsCaptchaLang_es = { name: "Spanish", facebook: "es_ES", instructions: "Escriba el texto de la imagen aquí", slide: "DESPLAZAR PARA ENCAJAR", refresh: "Refrescar", security: "Mostrar texto al azar", help: "Ayuda", direction: "ltr", align: "left" },
    AdsCaptchaLang_it = { name: "Italian", facebook: "it_IT", instructions: "Digitare il testo qui", slide: "SLIDE TO FIT", refresh: "Rinfrescare", security: "Mostra testo casuale", help: "Aiuto", direction: "ltr", align: "left" },
    AdsCaptchaLang_hi = { name: "Hindi", facebook: "hi_IN", instructions: "प्रकार यहाँ पाठ", slide: "SLIDE TO FIT", refresh: "ताज़ा करना", security: "दिखाएँ यादृच्छिक पाठ", help: "मदद", direction: "ltr", align: "left" },
    AdsCaptchaLang_el = { name: "Greek", facebook: "el_GR", instructions: "Πληκτρολογήστε κείμενο εδώ", slide: "SLIDE TO FIT", refresh: "φρεσκάρω", security: "Εμφάνιση τυχαίο κείμενο", help: "βοήθεια", direction: "ltr", align: "left" },
    AdsCaptchaLang_tr = { name: "Turkish", facebook: "tr_TR", instructions: "Lütfen buraya yazınız", slide: "SLIDE TO FIT", refresh: "Serinletmek", security: "Göstermek rasgele metin", help: "Yardım", direction: "ltr", align: "left" },
    AdsCaptchaLang_ro = { name: "Romanian", facebook: "ro_RO", instructions: "Tipul de text aici", slide: "SLIDE TO FIT", refresh: "Reîmprospăta", security: "Afişare aleatoare text", help: "Ajutor", direction: "ltr", align: "left" },
    AdsCaptchaLang_he = { name: "Hebrew", facebook: "he_IL", instructions: "הקלד את הטקסט", slide: "גלול והתאם את התמונה", refresh: "טען מחדש", security: "הצג תווים אקראיים", help: "עזרה", direction: "rtl", align: "right" };

var AdsCaptchaTheme_0 = { name: "Default", folder: "default", widget_background_color: "#ffffff", widget_border_color: "#ffffff", widget_border_width: 0, widget_text_color: "#707070", ad_border_color: "#cccccc", ad_border_width: 2, instructions_font_size: "12px", response_background_color: "#ffffff", response_border_color: "#000000", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#ffffff", captcha_text_color: "#707070" },
    AdsCaptchaTheme_1 = { name: "Blue",    folder: "blue",    widget_background_color: "#dbf3f8", widget_border_color: "#dbf3f8", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, instructions_font_size: "12px", response_background_color: "#ffffff", response_border_color: "#99dfef", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#dbf3f8", captcha_text_color: "#000000" },
    AdsCaptchaTheme_2 = { name: "Green",   folder: "green",   widget_background_color: "#ddf2bd", widget_border_color: "#ddf2bd", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, instructions_font_size: "12px", response_background_color: "#ffffff", response_border_color: "#000000", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#ddf2bd", captcha_text_color: "#000000" },
    AdsCaptchaTheme_3 = { name: "Yellow",  folder: "yellow",  widget_background_color: "#fff899", widget_border_color: "#fff899", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 0, instructions_font_size: "12px", response_background_color: "#ffffff", response_border_color: "#ddd454", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#fff899", captcha_text_color: "#000000" },
    AdsCaptchaTheme_4 = { name: "Purple",  folder: "purple",  widget_background_color: "#dbc8db", widget_border_color: "#dbc8db", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, instructions_font_size: "12px", response_background_color: "#ffffff", response_border_color: "#c3acc3", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#dbc8db", captcha_text_color: "#000000" },
    AdsCaptchaTheme_5 = { name: "Brown",   folder: "brown",   widget_background_color: "#e4ddd4", widget_border_color: "#e4ddd4", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, instructions_font_size: "12px", response_background_color: "#ffffff", response_border_color: "#d2cac0", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#e4ddd4", captcha_text_color: "#000000" },
    AdsCaptchaTheme_6 = { name: "Orange",  folder: "orange",  widget_background_color: "#fdc689", widget_border_color: "#fdc689", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, instructions_font_size: "12px", response_background_color: "#ffffff", response_border_color: "#fca545", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#fdc689", captcha_text_color: "#000000" },
    AdsCaptchaTheme_7 = { name: "Pink",    folder: "pink",    widget_background_color: "#fed3e6", widget_border_color: "#fed3e6", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, instructions_font_size: "12px", response_background_color: "#ffffff", response_border_color: "#000000", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#fed3e6", captcha_text_color: "#000000" },
    AdsCaptchaTheme_8 = { name: "Black",   folder: "black",   widget_background_color: "#000000", widget_border_color: "#000000", widget_border_width: 0, widget_text_color: "#ffffff", ad_border_color: "#ffffff", ad_border_width: 2, instructions_font_size: "12px", response_background_color: "#ffffff", response_border_color: "#555555", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#000000", captcha_text_color: "#ffffff" },
    AdsCaptchaTheme_9 = { name: "Grey",    folder: "grey",    widget_background_color: "#c9c9c9", widget_border_color: "#c9c9c9", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, instructions_font_size: "12px", response_background_color: "#ffffff", response_border_color: "#d5d4d4", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#c9c9c9", captcha_text_color: "#000000" };

var AdsCaptchaLang = null;
var AdsCaptchaTheme = null;

var AdsCaptchaWidget = {
    timer: -1,
    timeout: 60 * 60 * 1000,
    $: function(a) { return typeof a == 'string' ? document.getElementById(a) : a },
    destroy: function() {
        var a = AdsCaptchaWidget;
        var obj = a.$;
        var widget = obj('adscaptcha_widget');
        widget && widget.parentNode.removeChild(widget);
        a.timer != -1 && clearInterval(a.timer);
        a.timer = -1;
    },
    init_opt: function() {
        var a = AdsCaptchaWidget;
        var opt = AdsCaptchaOptions;

        switch (opt.lang) {
            case 'en': AdsCaptchaLang = AdsCaptchaLang_en; break;
            case 'nl': AdsCaptchaLang = AdsCaptchaLang_nl; break;
            case 'fr': AdsCaptchaLang = AdsCaptchaLang_fr; break;
            case 'de': AdsCaptchaLang = AdsCaptchaLang_de; break;
            case 'pt': AdsCaptchaLang = AdsCaptchaLang_pt; break;
            case 'ru': AdsCaptchaLang = AdsCaptchaLang_ru; break;
            case 'es': AdsCaptchaLang = AdsCaptchaLang_es; break;
            case 'it': AdsCaptchaLang = AdsCaptchaLang_it; break;
            case 'hi': AdsCaptchaLang = AdsCaptchaLang_hi; break;
            case 'el': AdsCaptchaLang = AdsCaptchaLang_el; break;
            case 'tr': AdsCaptchaLang = AdsCaptchaLang_tr; break;
            case 'ro': AdsCaptchaLang = AdsCaptchaLang_ro; break;
            case 'he': AdsCaptchaLang = AdsCaptchaLang_he; break;
            default: AdsCaptchaLang = AdsCaptchaLang_en; break;
        }

        switch (opt.theme) {
            case 'default': AdsCaptchaTheme = AdsCaptchaTheme_0; break;
            case 'blue': AdsCaptchaTheme = AdsCaptchaTheme_1; break;
            case 'green': AdsCaptchaTheme = AdsCaptchaTheme_2; break;
            case 'yellow': AdsCaptchaTheme = AdsCaptchaTheme_3; break;
            case 'purple': AdsCaptchaTheme = AdsCaptchaTheme_4; break;
            case 'brown': AdsCaptchaTheme = AdsCaptchaTheme_5; break;
            case 'orange': AdsCaptchaTheme = AdsCaptchaTheme_6; break;
            case 'pink': AdsCaptchaTheme = AdsCaptchaTheme_7; break;
            case 'black': AdsCaptchaTheme = AdsCaptchaTheme_8; break;
            case 'grey': AdsCaptchaTheme = AdsCaptchaTheme_9; break;
            default: AdsCaptchaTheme = AdsCaptchaTheme_0; break;
        }

        try {
            AdsCaptchaOptions = a.merge_opt([AdsCaptchaOptions, AdsCaptchaCustomOptions || {}]);
        } catch (e) { }

        try {
            AdsCaptchaLang = a.merge_opt([AdsCaptchaLang, AdsCaptchaCustomLang || {}]);
        } catch (e) { }

        try {
            AdsCaptchaTheme = a.merge_opt([AdsCaptchaTheme, AdsCaptchaCustomTheme || {}]);
        } catch (e) { }

        try {
            var url = document.location.toString();
            if (url.match('^https://')) {
                AdsCaptchaOptions.server = AdsCaptchaOptions.server.replace('http://', 'https://');
                AdsCaptchaOptions.ad_server = AdsCaptchaOptions.ad_server.replace('http://', 'https://');
                templateHTML = templateHTML.replace('http://', 'https://');
                templateFlashHTML = templateFlashHTML.replace('http://', 'https://');
                AdsCaptchaOptions.slider_data = AdsCaptchaOptions.slider_data.replace('http://', 'https://');
            } else {
                AdsCaptchaOptions.server = AdsCaptchaOptions.server.replace('https://', 'http://');
                AdsCaptchaOptions.ad_server = AdsCaptchaOptions.ad_server.replace('https://', 'http://');
                AdsCaptchaOptions.slider_data = AdsCaptchaOptions.slider_data.replace('https://', 'http://');
            }
        } catch (e) { }

        
    },
    merge_opt: function(options) {
        var r = {};
        for (var a in options) {
            for (var b in options[a]) {
                r[b] = options[a][b]
            }
        }
        return r;
    },
    init: function() {
        var a = AdsCaptchaWidget;
        var obj = a.$;
        var opt = AdsCaptchaOptions;

        a.destroy();

        a.init_opt();

        var renderCSS;
        var renderHTML;

        try {
            if(opt.flikeurl == '')
                templateFlashHTML = templateFlashHTML.replace("XXX", templateFacebookDefaultPage);
            else
                templateFlashHTML = templateFlashHTML.replace("XXX", opt.flikeurl);
        }
        catch (e) { }

        if (opt.type == 'slider') {
            renderCSS = templateFlashCSS;
            renderHTML = templateFlashHTML;
            document.write('<script type=\'text/javascript\'>' + 'function AdsCaptchaFlash_DoFSCommand(command, args) {document.getElementById(\'adscaptcha_response_field\').value = args; }' + '</script>');
        } else {
            renderCSS = templateCSS;
            renderHTML = templateHTML;
        }

        renderCSS = renderCSS.replace(/P_WIDGET_BACKGROUND_COLOR/g, AdsCaptchaTheme.widget_background_color);
        renderCSS = renderCSS.replace(/P_WIDGET_BORDER_COLOR/g, AdsCaptchaTheme.widget_border_color);
        renderCSS = renderCSS.replace(/P_WIDGET_BORDER_WIDTH/g, AdsCaptchaTheme.widget_border_width);
        renderCSS = renderCSS.replace(/P_WIDGET_TEXT_COLOR/g, AdsCaptchaTheme.widget_text_color);
        renderCSS = renderCSS.replace(/P_AD_BORDER_COLOR/g, AdsCaptchaTheme.ad_border_color);
        renderCSS = renderCSS.replace(/P_AD_BORDER_WIDTH/g, AdsCaptchaTheme.ad_border_width);
        renderCSS = renderCSS.replace(/P_INSTRUCTIONS_FONT_SIZE/g, AdsCaptchaTheme.instructions_font_size);
        renderCSS = renderCSS.replace(/P_RESPONSE_BACKGROUND_COLOR/g, AdsCaptchaTheme.response_background_color);
        renderCSS = renderCSS.replace(/P_RESPONSE_BORDER_COLOR/g, AdsCaptchaTheme.response_border_color);
        renderCSS = renderCSS.replace(/P_RESPONSE_BORDER_WIDTH/g, AdsCaptchaTheme.response_border_width);
        renderCSS = renderCSS.replace(/P_RESPONSE_TEXT_COLOR/g, AdsCaptchaTheme.response_text_color);
        renderCSS = renderCSS.replace(/P_DIRECTION/g, AdsCaptchaLang.direction);
        renderCSS = renderCSS.replace(/P_ALIGN/g, AdsCaptchaLang.align);
        renderCSS = renderCSS.replace(/P_IALIGN/g, (AdsCaptchaLang.align == 'left' ? 'right' : 'left'));
        renderCSS = renderCSS.replace(/P_SERVER/g, opt.server);
        renderHTML = renderHTML.replace(/P_LOCALE/g, AdsCaptchaLang.facebook);

        a.add_style(renderCSS);
        document.write(renderHTML);
    },
    call_refresh: function() {
        var opt = AdsCaptchaOptions;
        var reason = (opt.type == 'noflash' ? 'security' : '');
        var refresh_url = opt.server + 'Refresh.aspx?CaptchaId=' + opt.captcha + '&PublicKey=' + opt.key + '&ChallengeCode=' + opt.challenge + "&Reason=" + reason + "&Dummy=" + Math.random();
        opt.popup = false;
        AdsCaptchaWidget.add_script(refresh_url);
    },
    call_timeout: function() {
        var opt = AdsCaptchaOptions;
        var reason = 'Timeout';
        var refresh_url = opt.server + 'Refresh.aspx?CaptchaId=' + opt.captcha + '&PublicKey=' + opt.key + '&ChallengeCode=' + opt.challenge + "&Reason=" + reason + "&Dummy=" + Math.random();
        opt.popup = false;
        AdsCaptchaWidget.add_script(refresh_url);
    },
    call_security: function() {
        var opt = AdsCaptchaOptions;
        var refresh_url = opt.server + 'Refresh.aspx?CaptchaId=' + opt.captcha + '&PublicKey=' + opt.key + '&ChallengeCode=' + opt.challenge + "&Reason=Security&Dummy=" + Math.random();
        opt.popup = false;
        AdsCaptchaWidget.add_script(refresh_url);
    },
    call_noflash: function() {
        var opt = AdsCaptchaOptions;
        var refresh_url = opt.server + 'Refresh.aspx?CaptchaId=' + opt.captcha + '&PublicKey=' + opt.key + '&ChallengeCode=' + opt.challenge + "&Reason=NoFlash&Dummy=" + Math.random();
        opt.popup = false;
        AdsCaptchaWidget.add_script(refresh_url);
    },
    flash_to_security: function() {
        var a = AdsCaptchaWidget;
        var obj = a.$;
        var opt = AdsCaptchaOptions;

        var widget = obj('adscaptcha_widget');

        var renderCSS = templateCSS;
        var renderHTML = templateHTML;

        renderCSS = renderCSS.replace(/P_WIDGET_BACKGROUND_COLOR/g, AdsCaptchaTheme.widget_background_color);
        renderCSS = renderCSS.replace(/P_WIDGET_BORDER_COLOR/g, AdsCaptchaTheme.widget_border_color);
        renderCSS = renderCSS.replace(/P_WIDGET_BORDER_WIDTH/g, AdsCaptchaTheme.widget_border_width);
        renderCSS = renderCSS.replace(/P_WIDGET_TEXT_COLOR/g, AdsCaptchaTheme.widget_text_color);
        renderCSS = renderCSS.replace(/P_AD_BORDER_COLOR/g, AdsCaptchaTheme.ad_border_color);
        renderCSS = renderCSS.replace(/P_AD_BORDER_WIDTH/g, AdsCaptchaTheme.ad_border_width);
        renderCSS = renderCSS.replace(/P_INSTRUCTIONS_FONT_SIZE/g, AdsCaptchaTheme.instructions_font_size);
        renderCSS = renderCSS.replace(/P_RESPONSE_BACKGROUND_COLOR/g, AdsCaptchaTheme.response_background_color);
        renderCSS = renderCSS.replace(/P_RESPONSE_BORDER_COLOR/g, AdsCaptchaTheme.response_border_color);
        renderCSS = renderCSS.replace(/P_RESPONSE_BORDER_WIDTH/g, AdsCaptchaTheme.response_border_width);
        renderCSS = renderCSS.replace(/P_RESPONSE_TEXT_COLOR/g, AdsCaptchaTheme.response_text_color);
        renderCSS = renderCSS.replace(/P_DIRECTION/g, AdsCaptchaLang.direction);
        renderCSS = renderCSS.replace(/P_ALIGN/g, AdsCaptchaLang.align);
        renderCSS = renderCSS.replace(/P_IALIGN/g, (AdsCaptchaLang.align == 'left' ? 'right' : 'left'));
        renderCSS = renderCSS.replace(/P_SERVER/g, opt.server);
        renderHTML = renderHTML.replace(/P_LOCALE/g, AdsCaptchaLang.facebook);

        a.add_style(renderCSS);
        widget.innerHTML = renderHTML;

        opt.type = 'noflash';
        a.call_noflash();
    },
    set_captcha: function() {
        var a = AdsCaptchaWidget;
        var obj = a.$;
        var opt = AdsCaptchaOptions;

        var ad_holder = obj('adscaptcha_ad_holder');
        var ad_image = obj('adscaptcha_ad_image');

        if (opt.ad) {
            var ad_width = opt.width;
            var ad_height = opt.height;
            var ad_image_url = opt.server + 'Ad.aspx?cid=' + opt.challenge + "&dummy=" + Math.random();
            var imageHTML = "<img src='" + ad_image_url + "' width='" + ad_width + "' height='" + ad_height + "' />";
            if (opt.link != null) {
                var ad_link_url = opt.server + 'Click.aspx?cid=' + opt.challenge;
                ad_image.innerHTML = "<a href='" + ad_link_url + "' target='_blank'>" + imageHTML + "</a>";
            } else {
                ad_image.innerHTML = imageHTML;
            }
            ad_holder.style.width = (opt.width) + 'px';
            ad_holder.style.display = 'block';
        } else if (opt.banner) {
            var ifrm = document.createElement('IFRAME');
            ifrm.setAttribute('src', opt.ad_server + '&ad_type=iframe&ad_size=' + opt.width + 'x' + opt.height);
            ifrm.setAttribute('frameborder', '0');
            ifrm.setAttribute('marginheight', '0');
            ifrm.setAttribute('marginwidth', '0');
            ifrm.setAttribute('scrolling', 'no');
            ifrm.style.width = opt.width + 'px';
            ifrm.style.height = opt.height + 'px';
            if (ad_image.firstChild)
                ad_image.removeChild(ad_image.firstChild);
            ad_image.appendChild(ifrm);
            ad_holder.style.display = 'block';
        } else {
            if (ad_holder) ad_holder.style.display = 'none';
        }

        var widget = obj('adscaptcha_widget');
        widget.style.width = (opt.width + 2 * AdsCaptchaTheme.ad_border_width) + 'px';

        var challenge_image_url = opt.server + 'Challenge.aspx?cid=' + opt.challenge + '&w=' + opt.width + '&dummy=' + Math.random();
        var challenge_image = obj('adscaptcha_challenge_image');
        challenge_image.innerHTML = "<img src='" + challenge_image_url + "' width='" + opt.width + "' height='30' />";

        var challenge_holder = obj('adscaptcha_challenge_holder');
        if (opt.hasOwnProperty('hide_challenge') && opt.hide_challenge) {
            challenge_holder.style.display = 'none';
        } else {
            challenge_holder.style.display = 'block';
        }

        var instructions = obj('adscaptcha_instructions');
        //instructions.innerHTML = AdsCaptchaLang.instructions;
        
        instructions.innerHTML = "<div id='well'>";
        instructions.innerHTML +="<h2><strong id='slider'></strong><span>";
        instructions.innerHTML +=AdsCaptchaLang.instructions;
        instructions.innerHTML +="</span></h2></div>";


        var response_field = obj('adscaptcha_response_field');
        response_field.value = '';
        response_field.setAttribute('autocomplete', 'off');
        response_field.tabIndex = opt.tabindex;
        response_field.maxLength = 26;
        response_field.style.width = (opt.width * 0.9) + 'px';
        if (opt.direction == 'rtl') {
            response_field.style.direction = 'rtl';
            response_field.style.textAlign = 'right';
        } else {
            response_field.style.direction = 'ltr';
            response_field.style.textAlign = 'left';
        }

        var challenge_field = obj('adscaptcha_challenge_field');
        challenge_field.value = opt.challenge;

        var help_button = obj('adscaptcha_help_button');
        help_button.href = 'javascript:AdsCaptchaWidget.help();';
        help_button.title = AdsCaptchaLang.help;
        var help_image = obj('adscaptcha_help_image');
        help_image.src = opt.server + 'images/' + AdsCaptchaTheme.folder + '/button_help.gif';
        help_image.alt = AdsCaptchaLang.help;

        var refresh_button = obj('adscaptcha_refresh_button');
        refresh_button.href = 'javascript:AdsCaptchaWidget.call_refresh();';
        refresh_button.title = AdsCaptchaLang.refresh;
        var refresh_image = obj('adscaptcha_refresh_image');
        refresh_image.src = opt.server + 'images/' + AdsCaptchaTheme.folder + '/button_refresh.gif';
        refresh_image.alt = AdsCaptchaLang.refresh;

        var get_security_button = obj('adscaptcha_get_security_button');
        get_security_button.href = 'javascript:AdsCaptchaWidget.call_security();';
        get_security_button.title = AdsCaptchaLang.security;
        var get_security_image = obj('adscaptcha_get_security_image');
        get_security_image.src = opt.server + 'images/' + AdsCaptchaTheme.folder + '/button_security.gif';
        get_security_image.alt = AdsCaptchaLang.security;

        var about_holder = obj('adscaptcha_about_holder');
        if (opt.logo) {
            var about_button = obj('adscaptcha_about_button');
            about_button.href = 'http://www.com';
            about_button.title = 'Captcha';
            var about_image = obj('adscaptcha_about_image');

            if (opt.width < 200) {
                about_image.src = opt.server + 'images/' + AdsCaptchaTheme.folder + '/powered_by_adscaptcha_short.gif';
                about_image.style.width = '59px';
            } else {
                about_image.src = opt.server + 'images/' + AdsCaptchaTheme.folder + '/powered_by_adscaptcha.gif';
                about_image.style.width = '105px';
            }
            about_image.style.height = '5px';

            about_image.alt = 'Captcha';
        } else {
            about_holder.style.display = 'none';
        }

        a.set_timer();

        if (opt.popup) {
            a.popup();
        }
    },
    add_style: function(a) {
        var b = document.createElement("style");
        b.type = "text/css";
        if (b.styleSheet)
            if (navigator.appVersion.indexOf("MSIE 5") != -1)
            document.write("<style type='text/css'>" + a + "</style>");
        else
            b.styleSheet.cssText = a;
        else
            if (navigator.appVersion.indexOf("MSIE 5") != -1)
            document.write("<style type='text/css'>" + a + "</style>");
        else {
            a = document.createTextNode(a);
            b.appendChild(a)
        }
        AdsCaptchaWidget.get_head().appendChild(b);
    },
    add_script: function(a) {
        var b = document.createElement("script");
        b.type = "text/javascript";
        b.src = a;
        AdsCaptchaWidget.get_head().appendChild(b)
    },
    get_head: function() {
        var a = document.getElementsByTagName("head");
        a = !a || a.length < 1 ? document.body : a[0];
        return a;
    },
    set_timer: function() {
        var a = AdsCaptchaWidget;
        var opt = AdsCaptchaOptions;
        clearInterval(a.timer);
        a.timer = setInterval("AdsCaptchaWidget.call_timeout();", a.timeout);
    },
    refresh: function(challenge, ad, banner, width, height, direction, link) {
        var a = AdsCaptchaWidget;
        var obj = a.$;
        var opt = AdsCaptchaOptions;

        opt.challenge = challenge;
        opt.ad = ad;
        opt.banner = banner;
        opt.width = width;
        opt.height = height;
        opt.direction = direction;
        opt.link = link;

        if (opt.type == 'slider') {
            a.flash_to_security();
        } else {
            a.set_captcha();
        }

        try {
            var response_field = obj('adscaptcha_response_field');
            response_field.focus();
        } catch (e) { }

        //discoveredAdsCaptcha();
    },
    refresh_flash: function(challenge, width, height, link, sliderData) {
        var a = AdsCaptchaWidget;
        var obj = a.$;
        var opt = AdsCaptchaOptions;

        opt.challenge = challenge;
        opt.width = width;
        opt.height = height;
        opt.link = link;
        opt.slider_data = sliderData;

        var response_field = obj('adscaptcha_response_field');
        response_field.value = '';

        a.set_flash();

        //discoveredAdsCaptcha();
    },
    help: function() {
        var opt = AdsCaptchaOptions;
        var helpUrl = opt.server + 'Help.aspx?lang=' + opt.lang;
        if (opt.type == 'slider') {
            helpUrl = opt.server + 'HelpSlider.aspx?lang=' + opt.lang;
        }
        window.open(helpUrl, 'AdsCaptchaHelpPopup', 'width=440,height=600,location=no,menubar=no,status=no,toolbar=no,scrollbars=yes,resizable=no');
    },
    check_flash: function() {
        var isIE = (navigator.appVersion.indexOf("MSIE") != -1) ? true : false;
        var isWin = (navigator.appVersion.toLowerCase().indexOf("win") != -1) ? true : false;
        var isOpera = (navigator.userAgent.indexOf("Opera") != -1) ? true : false;
        var flashVer = -1;
        if (navigator.plugins != null && navigator.plugins.length > 0) {
            if (navigator.plugins["Shockwave Flash 2.0"] || navigator.plugins["Shockwave Flash"]) {
                var swVer2 = navigator.plugins["Shockwave Flash 2.0"] ? " 2.0" : "";
                var flashDescription = navigator.plugins["Shockwave Flash" + swVer2].description;
                var descArray = flashDescription.split(" ");
                var tempArrayMajor = descArray[2].split(".");
                flashVer = tempArrayMajor[0];
            }
        } else if (isIE && isWin && !isOpera) {
            try {
                var axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.7");
                var flashVerStr = axo.GetVariable("$version");
                flashVer = flashVerStr.split(" ")[1].split(",")[0];
            } catch (e) {
            }
        }
        return flashVer >= 11;
    },
    set_flash: function() {
        var a = AdsCaptchaWidget;
        var obj = a.$;
        var opt = AdsCaptchaOptions;

        var widget = obj('adscaptcha_widget');
        widget.style.width = (opt.width) + 'px';

        var instructions = obj('adscaptcha_instructions');
        if (instructions.firstChild)
            instructions.removeChild(instructions.firstChild);
        instructions.appendChild(
            document.createTextNode(AdsCaptchaLang.slide)
        );

        var challenge_field = obj('adscaptcha_challenge_field');
        challenge_field.value = opt.challenge;

        var help_button = obj('adscaptcha_help_button');
        help_button.href = 'javascript:AdsCaptchaWidget.help();';
        help_button.title = AdsCaptchaLang.help;
        var help_image = obj('adscaptcha_help_image');
        help_image.src = opt.server + 'images/' + AdsCaptchaTheme.folder + '/button_help.gif';
        help_image.alt = AdsCaptchaLang.help;

        var refresh_button = obj('adscaptcha_refresh_button');
        refresh_button.href = 'javascript:AdsCaptchaWidget.call_refresh();';
        refresh_button.title = AdsCaptchaLang.refresh;
        var refresh_image = obj('adscaptcha_refresh_image');
        refresh_image.src = opt.server + 'images/' + AdsCaptchaTheme.folder + '/button_refresh.gif';
        refresh_image.alt = AdsCaptchaLang.refresh;

        var get_security_button = obj('adscaptcha_get_security_button');
        get_security_button.href = 'javascript:AdsCaptchaWidget.call_security();';
        get_security_button.title = AdsCaptchaLang.security;
        var get_security_image = obj('adscaptcha_get_security_image');
        get_security_image.src = opt.server + 'images/' + AdsCaptchaTheme.folder + '/button_security.gif';
        get_security_image.alt = AdsCaptchaLang.security;

        var about_holder = obj('adscaptcha_about_holder');

        if (opt.logo) {
            var about_button = obj('adscaptcha_about_button');
            about_button.href = 'http://www.com';
            about_button.title = 'Captcha';
            var about_image = obj('adscaptcha_about_image');
            about_image.src = opt.server + 'images/' + AdsCaptchaTheme.folder + '/powered_by_adscaptcha.gif';
            about_image.alt = 'Captcha';
        } else {
            about_holder.style.display = 'none';
        }

        var link = 'null';
        if (opt.link != null) {
            link = opt.server + 'Click.aspx?cid=' + opt.challenge;
        }

        var gameString = opt.slider_data + "," + link;
        var gameID = opt.challenge;
        var background = AdsCaptchaTheme.widget_background_color;
        var frameColor = AdsCaptchaTheme.ad_border_color.replace('#', '');
        var frameThickness = AdsCaptchaTheme.ad_border_width;
        var borderColor = 'FFFFFF';
        var borderThickness = 2;
        var gameDemoMode = 'false';
        var callback = 'updateAdsCaptcha';

        var flashParamsArray = ['gameString=' + encodeURIComponent(gameString), 'gameID=' + encodeURIComponent(gameID), 'callback=' + encodeURIComponent(callback), 'frameColor=' + encodeURIComponent(frameColor), 'frameThickness=' + encodeURIComponent(frameThickness), 'borderColor=' + encodeURIComponent(borderColor), 'borderThickness=' + encodeURIComponent(borderThickness)];

        var flash_holder = obj('adscaptcha_flash_holder');
        var flash_html = a.renderFlash('AdsCaptchaFlash', 'attrClass', 'AdsCaptcha', opt.width, opt.height + 50, 'AdsCaptchaGames', background, flashParamsArray);
        var div = document.createElement('div');
        div.innerHTML = flash_html;
        div.style.width = opt.width + 'px';
        div.style.height = (opt.height + 50) + 'px';
        if (flash_holder.firstChild)
            flash_holder.removeChild(flash_holder.firstChild);
        flash_holder.appendChild(div);

        a.set_timer();

        if (opt.popup) {
            a.popup();
        }
    },
    renderFlash: function(attrId, attrClass, attrTitle, attrW, attrH, flashSrcName, background, flashParamsArray) {
        var opt = AdsCaptchaOptions;

        var flashHTML = '';
        var isIE = navigator.appName.indexOf("Microsoft") != -1;
        var flashVars = flashParamsArray.join("&");
        var flashPath = opt.server + flashSrcName + ".swf";
        var attrW = '100%';
        var attrH = '100%';

        if (isIE) {
            flashHTML += '<object id="' + attrId + '" codebase="https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" type="application/x-shockwave-flash" title="' + attrTitle + '"' + ' width="' + attrW + '" height="' + attrH + '">';
            flashHTML += '<param name="movie" value="' + flashPath + '">';
            flashHTML += '<param name="flashvars" value="' + flashVars + '">';
            flashHTML += '<param name="allowScriptAccess" value="always">';
            flashHTML += '<param name="allowFullScreen" value="false">';
            flashHTML += '<param name="menu" value="false">';
            flashHTML += '<param name="quality" value="high">';
            flashHTML += '<param name="scale" value="noScale">';
            flashHTML += '<param name="align" value="middle">';
            flashHTML += '<param name="bgcolor" value="' + background + '">';
            flashHTML += '<param name="wmode" value="null">';
            flashHTML += '</object>';
            flashHTML += '<!--[if gte IE 5]><script type="text/javascript" event="FSCommand(command,args)" for="AdsCaptchaFlash">AdsCaptchaFlash_DoFSCommand(command, args);</script> <![endif]-->'
        } else {
            flashHTML += '<embed name="' + attrId + '" src="' + flashPath + '" ' + ' title="' + attrTitle + '" ' + ' width="' + attrW + '" height="' + attrH + '" ' + ' scale="noScale" quality="high" ' + ' type="application/x-shockwave-flash" pluginspage="https://www.macromedia.com/go/getflashplayer" play="true" loop="true" allowScriptAccess="always" allowFullScreen="false" align="middle" menu="false" bgcolor="' + background + '" wmode="null" ' + ' FlashVars="' + flashVars + '">';
        }

        return flashHTML;
    },
    no_flash: function(challenge, width, height, direction) {
        var a = AdsCaptchaWidget;
        var obj = a.$;
        var opt = AdsCaptchaOptions;

        opt.challenge = challenge;
        opt.ad = false;
        opt.link = null;
        opt.slider_data = null;
        opt.width = width;
        opt.height = height;
        opt.direction = direction;

        a.set_captcha();
    },
    popup: function() {
        /*
        var a = AdsCaptchaWidget;
        var obj = a.$;
        var opt = AdsCaptchaOptions;

        try {
        var widget = obj('adscaptcha_widget');
        var ifrm = document.createElement('IFRAME');
        ifrm.setAttribute('src', opt.server + 'Banner.aspx');
        ifrm.setAttribute('frameborder', '0');
        ifrm.setAttribute('marginheight', '0');
        ifrm.setAttribute('marginwidth', '0');
        ifrm.setAttribute('scrolling', 'no');
        ifrm.style.width = '0px';
        ifrm.style.height = '0px';
        widget.appendChild(ifrm);
        } catch (e) { }
        */
    }
};

function getInternetExplorerVersion()
// Returns the version of Internet Explorer or a -1
// (indicating the use of another browser).
{
  var rv = -1; // Return value assumes failure.
  if (navigator.appName == 'Microsoft Internet Explorer')
  {
    var ua = navigator.userAgent;
    var re  = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
    if (re.exec(ua) != null)
      rv = parseFloat( RegExp.$1 );
  }  
  return rv;
}
function isIE9 ()
{
	try	
	{
		var ver = getInternetExplorerVersion();		
		return ver=='9';
	}
	catch (err)
	{
		return false;
	}
}

if (AdsCaptchaOptions.type == 'slider') {
    if (!AdsCaptchaWidget.check_flash() || isIE9()) {
        AdsCaptchaOptions.type = 'noflash';
        document.write('<script type=\'text/javascript\'>AdsCaptchaWidget.init();</script>');
        document.write('<script type=\'text/javascript\'>AdsCaptchaWidget.call_noflash();</script>');
    } else {
        if (!AdsCaptchaWidget.$('adscaptcha_widget')) {
            document.write('<script type=\'text/javascript\'>AdsCaptchaWidget.init();</script>');
        } else {
            document.write('<script type=\'text/javascript\'>AdsCaptchaWidget.init_opt();</script>');
        }
        document.write('<script type=\'text/javascript\'>AdsCaptchaWidget.set_flash();</script>');
    }
} else {
    if (!AdsCaptchaWidget.$('adscaptcha_widget')) {
        document.write('<script type=\'text/javascript\'>AdsCaptchaWidget.init();</script>');
    } else {
        document.write('<script type=\'text/javascript\'>AdsCaptchaWidget.init_opt();</script>');
    }
    document.write('<script type=\'text/javascript\'>AdsCaptchaWidget.set_captcha();</script>');
}