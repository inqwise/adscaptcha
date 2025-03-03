var MASTER_PAGE_PREFIX = '';


/*************************/
/*       GENERAL         */
/*************************/

function show(target) {    
    target = document.getElementById(target);
    target.style.display = 'block';
}

function hide(target) {
    target = document.getElementById(target);
    target.style.display = 'none';
}

/*************************/
/*    SYSTEM MESSAGES    */
/*************************/

function HideSystemMessages() {
    hide(MASTER_PAGE_PREFIX + 'SystemMessagesHolder');
}

/*************************/
/*         GRID          */
/*************************/

function GetSubTotal(column, digits) {
    var total = 0;
    var table = column.Table;

    var recordCount = table.getRowCount();

    for (var i = 0; i < recordCount; i++) {
        var value = table.getRow(i).getMember(column.get_dataField()).get_value();
        if (value == null) value = 0;
        total += parseFloat(value);
    }

    //return parseFloat(total);
    return parseFloat(total.toFixed(digits));
}

/*************************/
/* SECURITY LEVEL SLIDER */
/*************************/

function OnDragSecurityLevel(level) {
    switch (level) {
        case 0: return "Low";
        case 1: return "Low";
        case 2: return "2";
        case 3: return "3";
        case 4: return "4";
        case 5: return "High";
    }
}

function GetSecurityLevlDesc(level) {
    return "";
    
    switch (level) {
        case 1: return "No security: Slogan only";
        case 2: return "Low security: Slightly deformed slogan and 1 random letter";
        case 3: return "Medium security: Slightly deformed slogan and 2 random letters";
        case 4: return "High security: Highly deformed slogan and 3 random letters";
        case 5: return "Top security: Highly deformed slogan and 4 random letters";
    }
}

/*************************/
/*     CAPTCHA TYPE      */
/*************************/

function OnCaptchaTypeChange() {
    var selectedValue = $('#' + MASTER_PAGE_PREFIX + 'listCaptchaType').val();
    if (typeof ChangeCaptchaTypeValue != "undefined") selectedValue = ChangeCaptchaTypeValue;

    var holderPayPerType = '#' + MASTER_PAGE_PREFIX + 'CaptchaTypePayPerTypeHolder';
    var holderSecurityOnly = '#' + MASTER_PAGE_PREFIX + 'CaptchaTypeSecurityOnlyHolder';
    var holderSlider = '#' + MASTER_PAGE_PREFIX + 'CaptchaTypeSliderHolder';
    var holderSecuritySlider = '#' + MASTER_PAGE_PREFIX + 'CaptchaTypeSecuritySliderHolder';
    
    var holderClick = $('#' + MASTER_PAGE_PREFIX + 'AllowClickOptions');

    var holderCaptchaDims   = '#' + MASTER_PAGE_PREFIX + 'tableCaptchaDims';
    var holderSliderDims    = '#' + MASTER_PAGE_PREFIX + 'tableSliderDims';
    var holderSecurityLevel = '#' + MASTER_PAGE_PREFIX + 'tableSliderDims';

    var captchaDimension = $('.captchaDimension');
    var captchaSecuritySlider = $('.captchaSecurityLevel');

    switch (selectedValue) {
        case '13001': // Pay Per Typed
        case '130061':
            captchaDimension.show();
            captchaSecuritySlider.show();
            $(holderSecurityOnly).hide(); ChangeSlogan(previewSlogan, previewWidth, previewDir);
            $(holderSlider).hide();
            $(holderSecuritySlider).hide();
            $(holderPayPerType).show();
            $(holderCaptchaDims).show();
            $(holderSliderDims).hide();
            $(holderClick).show();
            toggleAllowContent();
            toggleCaptchaTypePreview(2);
            previewSlogan = 'FOX SALE';
            ChangeSlogan(previewSlogan, previewWidth, previewDir);
            break;
        case '13002': // Security Only
        case '130062':
            captchaDimension.hide();
            captchaSecuritySlider.show();
            $(holderPayPerType).hide();
            $(holderSlider).hide();
            $(holderSecuritySlider).hide();
            $(holderSecurityOnly).show(); ChangeSlogan('A53Fx4', previewWidth, previewDir);
            $(holderCaptchaDims).show();
            $(holderSliderDims).hide();
            $(holderClick).hide();
            toggleCaptchaTypePreview(0);
            previewSlogan = 'A53Fx4';
            ChangeSlogan(previewSlogan, previewWidth, previewDir);
            break;
        case '13003': // Slider
        case '130051':
        case '13007':
            captchaDimension.show();
            captchaSecuritySlider.hide();
            $(holderPayPerType).hide();
            $(holderSecurityOnly).hide(); ChangeSlogan(previewSlogan, previewWidth, previewDir);
            $(holderSlider).show();
            $(holderSecuritySlider).hide();
            $(holderCaptchaDims).hide();
            $(holderSliderDims).show();
            $(holderClick).show();
            toggleCaptchaTypePreview(5);
            break;
        case '13004': // Security Slider
        case '130052':
            captchaDimension.show();
            captchaSecuritySlider.hide();
            $(holderPayPerType).hide();
            $(holderSecurityOnly).hide(); ChangeSlogan(previewSlogan, previewWidth, previewDir);
            $(holderSlider).hide();
            $(holderSecuritySlider).show();
            $(holderCaptchaDims).hide();
            $(holderSliderDims).show();
            $(holderClick).hide();
            toggleCaptchaTypePreview(6);
            break;
    }
}

function toggleAllowContent() {
    var selectedValue = $('#' + MASTER_PAGE_PREFIX + 'listCaptchaType').val();    
    
    var allowClick = $('#' + MASTER_PAGE_PREFIX + 'radioAllowClick_0').attr('checked');
    var allowVideo = $('#' + MASTER_PAGE_PREFIX + 'checkAllowVideo').attr('checked');
    var allowImage = $('#' + MASTER_PAGE_PREFIX + 'checkAllowImage').attr('checked');
    var allowSlogan = $('#' + MASTER_PAGE_PREFIX + 'checkAllowSlogan').attr('checked');

    if (selectedValue == '13001') {
        if (allowVideo && allowClick)
            toggleCaptchaTypePreview(4);
        else if (allowVideo)
            toggleCaptchaTypePreview(4);
        else if (allowImage && allowClick)
            toggleCaptchaTypePreview(3);
        else if (allowImage)
            toggleCaptchaTypePreview(2);
        else if (allowSlogan)
            toggleCaptchaTypePreview(1);
    }
}

function toggleCaptchaTypePreview(value) {
    for (i = 0; i < 7; i++) {
        var obj = '#' + MASTER_PAGE_PREFIX + 'CaptchaTypeHolder' + i;
        $(obj).hide();
    }

    var obj = '#' + MASTER_PAGE_PREFIX + 'CaptchaTypeHolder' + value;
    $(obj).show();
}

function validateAllowedContentClient(sender, args) {
    var selectedValue = $('#' + MASTER_PAGE_PREFIX + 'listCaptchaType').val();

    if (selectedValue == '13001') {
        var allowVideo = $('#' + MASTER_PAGE_PREFIX + 'checkAllowVideo').attr('checked');
        var allowImage = $('#' + MASTER_PAGE_PREFIX + 'checkAllowImage').attr('checked');
        var allowSlogan = $('#' + MASTER_PAGE_PREFIX + 'checkAllowSlogan').attr('checked');

        args.IsValid = (allowVideo == true || allowImage == true || allowSlogan == true) ? true : false;
    } else {
        args.IsValid = true;
    }
}

/*************************/
/*        AD TYPE        */
/*************************/

function OnAdTypeChange() {
    var selectedIndex = document.getElementById(MASTER_PAGE_PREFIX + 'listAdType').selectedIndex;
    var selectedValue = document.getElementById(MASTER_PAGE_PREFIX + 'listAdType').options[selectedIndex].value;

    for (i = 0; i < 5; i++) {
        if (i == selectedIndex) {
            document.getElementById(MASTER_PAGE_PREFIX + 'AdTypeHolder' + i).style.display = 'block';
        } else {
            document.getElementById(MASTER_PAGE_PREFIX + 'AdTypeHolder' + i).style.display = 'none';
        }
    }

    var messageHolder = $('#' + MASTER_PAGE_PREFIX + 'TextMessageHolder');
    var imageHolder = $('#' + MASTER_PAGE_PREFIX + 'AdTypeImageHolder');
    var videoHolder = $('#' + MASTER_PAGE_PREFIX + 'AdTypeVideoHolder');
    var couponHolder = $('#' + MASTER_PAGE_PREFIX + 'AdTypeCouponHolder');
    var imageClickURLHolder = MASTER_PAGE_PREFIX + 'AdTypeClickURLHolder'; // =='MasterPage_MainContent_AdTypeClickURLHolder'

    switch (selectedValue) {
        case '16001': // Slogan Only
            messageHolder.show();
            imageHolder.hide();
            videoHolder.hide();
            couponHolder.hide();
            document.getElementById(imageClickURLHolder).style.display = 'none';
            break;
        case '16002': // Slogan and Image
            messageHolder.show();
            imageHolder.show();
            videoHolder.hide();
            couponHolder.hide();
            document.getElementById(imageClickURLHolder).style.display = 'block';
            document.getElementById(imageClickURLHolder).style.display = 'table-row';                
            break;
        case '16003': // Slogan and Video
            messageHolder.show();
            imageHolder.hide();
            videoHolder.show();
            couponHolder.hide();
            document.getElementById(imageClickURLHolder).style.display = 'none';
            break;
        case '16004': // Slogan and Coupon
            messageHolder.show();
            imageHolder.show();
            videoHolder.hide();
            couponHolder.show();
            document.getElementById(imageClickURLHolder).style.display = 'none';
            break;
        case '16005': // Slide to Fit
            messageHolder.hide();
            imageHolder.show();
            videoHolder.hide();
            couponHolder.hide();
            document.getElementById(imageClickURLHolder).style.display = 'block';
            document.getElementById(imageClickURLHolder).style.display = 'table-row';
            break;
    }

    _AdType = selectedValue;          
}

/*************************/
/*        AD LINK        */
/*************************/

function CheckLinkURL() {
    var obj = $('#' + MASTER_PAGE_PREFIX + 'txtImageClickURL');
    var link = obj.val();
    if (link == null || link == '') {
        return;
    } else {
        var str = new String(link);        
        var https = str.indexOf('https');
        var http = str.indexOf('http');
        if (http < 0 && https < 0) {
            link = 'http://' + link;
        }
        
        window.open(link);
    }
}

function CheckLikeURL(txtid) {
    var obj = $("#" + txtid );
    var link = obj.val();
    if (link == null || link == '') {
        return;
    } else {
        var str = new String(link);
        var https = str.indexOf('https');
        var http = str.indexOf('http');
        if (http < 0 && https < 0) {
            link = 'http://' + link;
        }

        window.open(link);
    }
}

/*************************/
/*        PREVIEW        */
/*************************/

function OnThemeChange() {
    var selectedIndex = document.getElementById(MASTER_PAGE_PREFIX + 'listThemes').selectedIndex;
    var selectedValue = document.getElementById(MASTER_PAGE_PREFIX + 'listThemes').options[selectedIndex].value;

    previewLoadTheme(selectedValue);
}

function UpdateTextMessagePreview() {
    var objSlogan = $('#' + MASTER_PAGE_PREFIX + 'textAdSlogan');
    var message = objSlogan.val();
    var dir = objSlogan.css('direction');
    
    ChangeSlogan(message, previewWidth, dir);
}

/*************************/
/*        COUNTRY        */
/*************************/

function changeCountry(e) {
    var selectedIndex = document.getElementById(MASTER_PAGE_PREFIX + 'listCountry').selectedIndex;
    var selectedValue = document.getElementById(MASTER_PAGE_PREFIX + 'listCountry').options[selectedIndex].value;

    if (selectedValue != 0) {
        var bankField = document.getElementById(MASTER_PAGE_PREFIX + 'textBankCountry');
        var bankCountry = bankField.value;

        /*if (bankCountry == '') {*/
            var selectedCountry = document.getElementById(MASTER_PAGE_PREFIX + 'listCountry').options[selectedIndex].text;

            bankField.value = selectedCountry;
        /*}*/
    } 
}

/*************************/
/*       TARGETING       */
/*************************/

function toggleCountryTargeting(e) {
    if (document.getElementById(MASTER_PAGE_PREFIX + 'radioCountry_0').checked) {
        hide(MASTER_PAGE_PREFIX + 'countryTargeting');
    } else if (document.getElementById(MASTER_PAGE_PREFIX + 'radioCountry_1').checked) {
        show(MASTER_PAGE_PREFIX + 'countryTargeting');
    }
}

function toggleLanguageTargeting(e) {
    if (document.getElementById(MASTER_PAGE_PREFIX + 'radioLanguage_0').checked) {
        hide(MASTER_PAGE_PREFIX + 'languageTargeting');
    } else if (document.getElementById(MASTER_PAGE_PREFIX + 'radioLanguage_1').checked) {
        show(MASTER_PAGE_PREFIX + 'languageTargeting');
    }
}

function toggleCategoryTargeting(e) {
    if (document.getElementById(MASTER_PAGE_PREFIX + 'radioCategory_0').checked) {
        hide(MASTER_PAGE_PREFIX + 'categoryTargeting');
    } else if (document.getElementById(MASTER_PAGE_PREFIX + 'radioCategory_1').checked) {
        show(MASTER_PAGE_PREFIX + 'categoryTargeting');
    }
}

function toggleKeywordsTargeting(e) {
    if (document.getElementById(MASTER_PAGE_PREFIX + 'radioKeywords_0').checked) {
        hide(MASTER_PAGE_PREFIX + 'keywordsTargeting');
    } else if (document.getElementById(MASTER_PAGE_PREFIX + 'radioKeywords_1').checked) {
        show(MASTER_PAGE_PREFIX + 'keywordsTargeting');
    }
}

function toggleSchedule(e) {
    if (document.getElementById(MASTER_PAGE_PREFIX + 'radioSchedule_0').checked) {
        hide(MASTER_PAGE_PREFIX + 'scheduleDates');
    } else if (document.getElementById(MASTER_PAGE_PREFIX + 'radioSchedule_1').checked) {
        show(MASTER_PAGE_PREFIX + 'scheduleDates');
    }
}

/*************************/
/*      UPLOAD IMAGE     */
/*************************/

var _UploadImageHolder;
var _ValidationMessage;
var _UploadProgressHolder;
var _UploadImageFrame;
var _AdType;

function initPhotoUpload() {
    _UploadImageHolder = document.getElementById('UploadImageHolder');
    _ValidationMessage = document.getElementById(MASTER_PAGE_PREFIX + 'labelImageValidation');
    _UploadProgressHolder = document.getElementById('UploadProgressHolder');
    _UploadImageFrame = document.getElementById('UploadImageFrame');
    _UploadHidden = document.getElementById(MASTER_PAGE_PREFIX + 'UploadHidden');
    _UploadedImageHolder = document.getElementById(MASTER_PAGE_PREFIX + 'UploadedImageHolder');
    _UploadPreview = document.getElementById(MASTER_PAGE_PREFIX + 'UploadPreview');
    _UploadWidth = document.getElementById(MASTER_PAGE_PREFIX + 'UploadWidth');
    _UploadHeight = document.getElementById(MASTER_PAGE_PREFIX + 'UploadHeight');
    _AdWidth = document.getElementById(MASTER_PAGE_PREFIX + 'AdWidth');
    _AdHeight = document.getElementById(MASTER_PAGE_PREFIX + 'AdHeight');
}

function photoUploadComplete(message, isError, fileurl, filename, width, height) {
    
    //_UploadProgressHolder.style.display = 'none';
    _ValidationMessage.style.display = 'none';
    _UploadImageHolder.style.display = '';
    _UploadHidden.value = filename;    
    
    if (message.length) {
        if (isError) {            
            _ValidationMessage.innerHTML = '* ' + message + '<br>'; 
            _ValidationMessage.style.display = '';
            _UploadImageFrame.contentWindow.document.getElementById('uploadImage').focus();
            _UploadedImageHolder.style.display = 'none';
        } else {
            _AdWidth.value = width;
            _AdHeight.value = height;
            
            var PREVIEW_WIDTH = 250;
            var ratio = 1;
            var previewWidth = width;
            var previewHeight = height;
            
            if (width > PREVIEW_WIDTH) {
                ratio = width / PREVIEW_WIDTH;
                previewHeight = height / ratio;
            } else {
                ratio = PREVIEW_WIDTH / width;
                previewWidth = height * ratio;
            }
            previewWidth = PREVIEW_WIDTH;
            previewHeight = Math.round(previewHeight);

            _ValidationMessage.style.display = 'none';
            _UploadPreview.width = (previewWidth / 2);
            _UploadPreview.height = (previewHeight / 2);
            _UploadWidth.value = width;
            _UploadHeight.value = height;
            _UploadPreview.src = fileurl + filename;
            _UploadedImageHolder.style.display = '';
        }
    }
}


/********************************/
/*      PAYMENT PREFERENCES     */
/********************************/
function OnPaymentMethodChange() {

    var selectedValue = $('#' + MASTER_PAGE_PREFIX + 'listCreditMethod').val();
    var paymentBank = 'PaymentBank_Section';
    var paymentCheck = 'PaymentCheck_Section';
    var paymentPayPal = 'PaymentPayPal_Section';
    var paymentLater = 'PaymentLater_Section';

    hide(paymentBank);
    hide(paymentCheck);
    hide(paymentPayPal);
    hide(paymentLater);

    switch (selectedValue) {
        case '23001': // Bank Wire
            show(paymentBank);
            break;
        case '23002': // Check
            show(paymentCheck);
            break;
        case '23004': // PayPal
            show(paymentPayPal);
            break;
        case '23005': // Set Later...
            show(paymentLater);
            break;
        default: 
            break;
    }
}

function removeNonRelevantPaymentInfo() {

    var selectedValue = $('#' + MASTER_PAGE_PREFIX + 'listCreditMethod').val();
    var paymentBank = '#PaymentBank_Section';
    var paymentCheck = '#PaymentCheck_Section';
    var paymentPayPal = '#PaymentPayPal_Section';
    var paymentLater = '#PaymentLater_Section';

    switch (selectedValue) {
        case '23001': // Bank Wire
            $(paymentCheck + ' :input').val('');
            $(paymentPayPal + ' :input').val('');
            $(paymentLater + ' :input').val('');
            break;
        case '23002': // Check
            $(paymentBank + ' :input').val('');
            $(paymentPayPal + ' :input').val('');
            $(paymentLater + ' :input').val('');
            break;
        case '23004': // PayPal
            $(paymentBank + ' :input').val('');
            $(paymentCheck + ' :input').val('');
            $(paymentLater + ' :input').val('');
            break;
        case '23005': // Set Later...
            $(paymentBank + ' :input').val('');
            $(paymentCheck + ' :input').val('');
            $(paymentPayPal + ' :input').val('');
            break;
        default:
            break;
    }

    //$('.ChangesSaved').fadeOut('slow').delay(5000);
}

