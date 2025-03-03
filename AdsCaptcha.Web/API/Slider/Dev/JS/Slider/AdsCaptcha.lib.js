var AdsCaptcha = {};
AdsCaptcha.Slider = {
    Options : {
        Width: 300,
        Height: 250,
        Theme: "default",
        Type: 101, //101: captcha, 102: skip, 103: preroll, 104: banner
        Challenge: "",
        ClickUrl: "",
        LikeUrl: "",
        AutoValidate: false,
        Language: "English",
        BaseUrl: "http://api.dev.com/"
    },
    Timeinslider: 0,
    ImageCounter:0,
    AdscaptchaBgImages: new Array(),
    Languages: { AdsCaptchaLang_en : { name: "English", facebook: "en_US", instructions: "Type text here", slide: "SLIDE TO FIT", refresh: "Refresh", security: "Show random text", help: "Help", direction: "ltr", align: "left" },
                AdsCaptchaLang_nl : { name: "Dutch", facebook: "nl_NL", instructions: "Typ tekst hier", slide: "SLIDE TO FIT", refresh: "Verversen", security: "Toon willekeurige tekst", help: "Helpen", direction: "ltr", align: "left" },
                AdsCaptchaLang_fr : { name: "French", facebook: "fr_FR", instructions: "Entrez votre texte ici", slide: "Faire glisser et adapter", refresh: "Rafraîchir", security: "Afficher le texte aléatoire", help: "Aider", direction: "ltr", align: "left" },
                AdsCaptchaLang_de : { name: "German", facebook: "de_DE", instructions: "Bitte Text hier eingeben", slide: "SLIDE TO FIT", refresh: "Erfrischen", security: "Show zufälligen Text", help: "Hilfe", direction: "ltr", align: "left" },
                AdsCaptchaLang_pt : { name: "Portuguese", facebook: "pt_BR", instructions: "Digite o texto aqui", slide: "SLIDE TO FIT", refresh: "Refrescar", security: "Mostrar texto aleatório", help: "Ajuda", direction: "ltr", align: "left" },
                AdsCaptchaLang_ru : { name: "Russian", facebook: "ru_RU", instructions: "Введите слово здесь", slide: "SLIDE TO FIT", refresh: "обновление", security: "Показать случайный текст", help: "помощь", direction: "ltr", align: "left" },
                AdsCaptchaLang_es : { name: "Spanish", facebook: "es_ES", instructions: "Escriba el texto de la imagen aquí", slide: "DESPLAZAR PARA ENCAJAR", refresh: "Refrescar", security: "Mostrar texto al azar", help: "Ayuda", direction: "ltr", align: "left" },
                AdsCaptchaLang_it : { name: "Italian", facebook: "it_IT", instructions: "Digitare il testo qui", slide: "SLIDE TO FIT", refresh: "Rinfrescare", security: "Mostra testo casuale", help: "Aiuto", direction: "ltr", align: "left" },
                AdsCaptchaLang_hi : { name: "Hindi", facebook: "hi_IN", instructions: "प्रकार यहाँ पाठ", slide: "SLIDE TO FIT", refresh: "ताज़ा करना", security: "दिखाएँ यादृच्छिक पाठ", help: "मदद", direction: "ltr", align: "left" },
                AdsCaptchaLang_el : { name: "Greek", facebook: "el_GR", instructions: "Πληκτρολογήστε κείμενο εδώ", slide: "SLIDE TO FIT", refresh: "φρεσκάρω", security: "Εμφάνιση τυχαίο κείμενο", help: "βοήθεια", direction: "ltr", align: "left" },
                AdsCaptchaLang_tr : { name: "Turkish", facebook: "tr_TR", instructions: "Lütfen buraya yazınız", slide: "SLIDE TO FIT", refresh: "Serinletmek", security: "Göstermek rasgele metin", help: "Yardım", direction: "ltr", align: "left" },
                AdsCaptchaLang_ro : { name: "Romanian", facebook: "ro_RO", instructions: "Tipul de text aici", slide: "SLIDE TO FIT", refresh: "Reîmprospăta", security: "Afişare aleatoare text", help: "Ajutor", direction: "ltr", align: "left" },
                AdsCaptchaLang_he : { name: "Hebrew", facebook: "he_IL", instructions: "הקלד את הטקסט", slide: "גלול והתאם את התמונה", refresh: "טען מחדש", security: "הצג תווים אקראיים", help: "עזרה", direction: "rtl", align: "right" }
    },
    Init: function(opt, images)
    {
        this.Options = opt;
        this.AdscaptchaBgImages = images;
    },
    Render: function(){

       if(this.Options.Type == 101)
       {
           var strDivMain = "<div id=\"adscap_cap_main\" class=\"adscap_cap_" + this.Options.Width  + "\">";
           document.write(strDivMain);

           var divMain = jQuery("#adscap_cap_main"); //jQuery("<div />").attr("class", "adscap_cap_300");
           var divslider = jQuery("<div />").attr("class", "adscap_cap_slider").appendTo(divMain);

           var divimage = jQuery("<div />").attr("class", "adscap_cap_image").appendTo(divslider);
           var divimage_container = jQuery("<div />").attr("class", "adscap_cap_innerimage").attr("id", "adscap_image_container").appendTo(divimage);
           var div_loading = jQuery("<div />").attr("class", "loading").attr("id", "adscap_loading").appendTo(divimage_container);
           var imgInner = jQuery("<img />").attr("id", "adscap_slider_image");


           var div_sliderline = jQuery("<div />").attr("class", "adscap_cap_sliderline").appendTo(divslider);
           var input_sliderline = jQuery("<input />").attr("id", "adscap_slider_line").attr("type", "slider").attr("value", "0").attr("name", "area").appendTo(div_sliderline);

           var hidden_challenge = jQuery("<input />").attr("id", "adscaptcha_challenge_field").attr("type", "hidden").attr("name", "adscaptcha_challenge_field").appendTo(divMain);
           var hidden_response = jQuery("<input />").attr("id", "adscaptcha_response_field").attr("type", "hidden").attr("name", "adscaptcha_response_field").appendTo(divMain);


           jQuery("#adscap_slider_line").slider({ from: 0, to: 29, step: 1, dimension: '', calculate: function (value) {
               //jQuery("#temp").html(value);

               if (this.ImageCounter == 30) {
                   //jQuery("#adscap_image_container").css("background", "url('" + AdscaptchaBgImages[value].src + "') no-repeat");

                   //jQuery("#adscap_slider_image").attr("src", AdscaptchaBgImages[value].src);

                   $(".adscapimageloaded").hide();
                   $("#adscapimage_" + value).show();
               }
           }
           });

           divimage.bind("click", function (e) {
               //alert("clicked");
               if(this.Options.ClickUrl != "")
               {
                var x =  e.pageX - this.offsetLeft;
                var y = e.pageY - this.offsetTop;
                //AdsCaptcha.Statistics.Click(x, y);
                window.open(this.Options.ClickUrl);
               }
           });

           divMain.bind("mouseenter", function (e) {
               this.Timeinslider = jQuery.now();
           });

           divMain.bind("mouseleave", function (e) {
               var timeTemp = jQuery.now() - this.Timeinslider;
               if(timeTemp > 800)
               {
               //jQuery("#sliderTime").html(timeTemp - timeinslider);
               }
               this.Timeinslider= 0;
           });

           $(document).keydown(function (e) {
               if (e.keyCode == 37) {
                   if (jQuery("#adscap_slider_line").val() > 0) {
                       var index = parseInt(jQuery("#adscap_slider_line").val()) - 1;
                       jQuery("#adscap_slider_line").slider("value", index);
                       $(".adscapimageloaded").hide();
                       $("#adscapimage_" + index).show();
                   }
               }
               else if (e.keyCode == 39) {

                   if (jQuery("#adscap_slider_line").val() < 29) {

                       var index = parseInt(jQuery("#adscap_slider_line").val()) + 1;
                       jQuery("#adscap_slider_line").slider("value", index);
                       $(".adscapimageloaded").hide();
                       $("#adscapimage_" + index).show();
                   }
               }
           });

           //////////////////////////Loading images////////////////////////////////////////

           var imgUrl = this.Options.BaseUrl + 'Slider/SliderData.ashx?cid=' + this.Options.Challenge + '&callback=?';
           $.getJSON(imgUrl, function(data) {

               $.each(data, function (key, val) {
                   //alert(this.src);
                   AdsCaptcha.Slider.AdscaptchaBgImages.push({src:val.src});
                   $("<img />").attr('src', val.src).attr('id', 'adscapimage_' + key).attr('class', 'adscapimageloaded').attr('style', 'display:none;')
                       .load(function () {
                           if (!this.complete || typeof this.naturalWidth == "undefined" || this.naturalWidth == 0) {
                               //alert('broken image!');
                           } else {
                               AdsCaptcha.Slider.ImageCounter++;
                           }
                       }).appendTo(divimage_container);
               });

           });
       }



    },
    Validate: function(){}
};

AdsCaptcha.Slider.Render();

//////////////////////////Loading slider////////////////////////////////////////

(function()
{
    var AutoSliderIndex = 0;
    var AutoSliderDirection = 1;

    function MoveSliderAuto() {

        if ((AutoSliderIndex != 0) || (AutoSliderDirection != 0)) {

            jQuery("#adscap_slider_line").slider("value", AutoSliderIndex);

            $(".adscapimageloaded").hide();
            $("#adscapimage_" + AutoSliderIndex).show();

            if ((AutoSliderDirection == 1) && (AutoSliderIndex < 29)) AutoSliderIndex++;
            else { AutoSliderIndex--; AutoSliderDirection = 0; }

            setTimeout(MoveSliderAuto, 10);
        }
    }

    function AdsCapCheckImagesLoading() {

        if (AdsCaptcha.Slider.ImageCounter == 30) {

            $("#adscap_loading").hide();

            $(".adscapimageloaded").hide();
            $("#adscapimage_0").show();

            MoveSliderAuto();

        }
        else {
            setTimeout(AdsCapCheckImagesLoading, 200);
        }
    }

    AdsCapCheckImagesLoading();

}());



AdsCaptcha.SimpleCaptcha = {

}
