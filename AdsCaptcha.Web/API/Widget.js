var previewThemeId = 0;
var previewURL = ADSCAPTCHA_URL + 'Images/Preview/Preview.jpg';
var previewSlogan = 'Your Message';
var previewDir = 'ltr';
var previewWidth = 300;
var previewHeight = 250;

var previewSecuritySliderURL = ADSCAPTCHA_URL + 'Images/Preview/SecuritySlider.jpg';

var AdsCaptchaTheme = new Array();
AdsCaptchaTheme[0] = { name: "Default", folder: "default", widget_background_color: "#ffffff", widget_border_color: "#ffffff", widget_border_width: 0, widget_text_color: "#707070", ad_border_color: "#cccccc", ad_border_width: 2, response_background_color: "#ffffff", response_border_color: "#000000", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#ffffff", captcha_text_color: "#707070" };
AdsCaptchaTheme[1] = { name: "Blue", folder: "blue", widget_background_color: "#dbf3f8", widget_border_color: "#dbf3f8", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, response_background_color: "#ffffff", response_border_color: "#99dfef", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#dbf3f8", captcha_text_color: "#000000" };
AdsCaptchaTheme[2] = { name: "Green", folder: "green", widget_background_color: "#ddf2bd", widget_border_color: "#ddf2bd", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, response_background_color: "#ffffff", response_border_color: "#000000", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#ddf2bd", captcha_text_color: "#000000" };
AdsCaptchaTheme[3] = { name: "Yellow", folder: "yellow", widget_background_color: "#fff899", widget_border_color: "#fff899", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 0, response_background_color: "#ffffff", response_border_color: "#ddd454", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#fff899", captcha_text_color: "#000000" };
AdsCaptchaTheme[4] = { name: "Purple", folder: "purple", widget_background_color: "#dbc8db", widget_border_color: "#dbc8db", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, response_background_color: "#ffffff", response_border_color: "#c3acc3", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#dbc8db", captcha_text_color: "#000000" };
AdsCaptchaTheme[5] = { name: "Brown", folder: "brown", widget_background_color: "#e4ddd4", widget_border_color: "#e4ddd4", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, response_background_color: "#ffffff", response_border_color: "#d2cac0", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#e4ddd4", captcha_text_color: "#000000" };
AdsCaptchaTheme[6] = { name: "Orange", folder: "orange", widget_background_color: "#fdc689", widget_border_color: "#fdc689", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, response_background_color: "#ffffff", response_border_color: "#fca545", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#fdc689", captcha_text_color: "#000000" };
AdsCaptchaTheme[7] = { name: "Pink", folder: "pink", widget_background_color: "#fed3e6", widget_border_color: "#fed3e6", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, response_background_color: "#ffffff", response_border_color: "#000000", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#fed3e6", captcha_text_color: "#000000" };
AdsCaptchaTheme[8] = { name: "Black", folder: "black", widget_background_color: "#000000", widget_border_color: "#000000", widget_border_width: 0, widget_text_color: "#ffffff", ad_border_color: "#ffffff", ad_border_width: 2, response_background_color: "#ffffff", response_border_color: "#555555", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#000000", captcha_text_color: "#ffffff" };
AdsCaptchaTheme[9] = { name: "Grey", folder: "grey", widget_background_color: "#c9c9c9", widget_border_color: "#c9c9c9", widget_border_width: 0, widget_text_color: "#000000", ad_border_color: "#555555", ad_border_width: 2, response_background_color: "#ffffff", response_border_color: "#d5d4d4", response_border_width: 1, response_text_color: "#000000", captcha_background_color: "#c9c9c9", captcha_text_color: "#000000" };

function getThemeId() {
    var selectedIndex = document.getElementById(MASTER_PAGE_PREFIX + 'listThemes').selectedIndex;
    var selectedValue = document.getElementById(MASTER_PAGE_PREFIX + 'listThemes').options[selectedIndex].value;

    return selectedIndex;
}

function getThemeById(themeId) {
    if (themeId < 0) themeId = 0;
    if (themeId > AdsCaptchaTheme.length - 1) themeId = AdsCaptchaTheme.length - 1;
    
    return AdsCaptchaTheme[themeId];
}

function previewLoadTheme(themeId) {
    var theme = getThemeById(themeId);

    previewThemeId = themeId;

    /*
    theme.widget_background_color
    theme.widget_border_color
    theme.widget_border_width
    theme.widget_text_color
    theme.ad_border_color
    theme.ad_border_width
    theme.response_background_color
    theme.response_border_color
    theme.response_border_width
    theme.response_text_color
    theme.captcha_background_color
    theme.captcha_text_color
    */

    $('.AdsCaptchaWidget').css('background-color', theme.widget_background_color);
    $('.AdsCaptchaWidget').css('borderColor', theme.widget_border_color);
    $('.AdsCaptchaWidget').css('border-width', theme.widget_border_width + 'px');
    $('.AdsCaptchaWidget').css('color', theme.widget_text_color);

    $('.RefreshButton').attr('src', ADSCAPTCHA_API + 'images/' + theme.folder + '/button_refresh.gif');
    $('.HelpButton').attr('src', ADSCAPTCHA_API + 'images/' + theme.folder + '/button_help.gif');

    $('.AdHolder').css('border-width', theme.ad_border_width + 'px');
    $('.AdHolder').css('borderColor', theme.ad_border_color);

    $('.ResponseField').css('background-color', theme.response_background_color);
    $('.ResponseField').css('borderColor', theme.response_border_color);
    $('.ResponseField').css('border-width', theme.response_border_width + 'px');
    $('.ResponseField').css('color', theme.response_text_color);
    $('.Instructions').css('color', theme.widget_text_color);

    ChangeSlogan(previewSlogan, previewWidth, previewDir);
    
    SetSlider('FlashSlider', previewURL, previewWidth, previewHeight, previewThemeId);
    SetSlider('FlashSecuritySlider', previewSecuritySliderURL, previewWidth, previewHeight, previewThemeId);
}

function ChangeAd(url, width, height) {
    previewURL = url;

    var imageURL = url.replace("../", ADSCAPTCHA_URL);
    var imageHolder = $('.AdImage');
    imageHolder.attr("width", width);
    imageHolder.attr("height", height);
    imageHolder.attr("src", imageURL);

    var sliderURL = url.replace("../", ADSCAPTCHA_CDN);
    SetSlider('FlashSlider', sliderURL, previewWidth, previewHeight, previewThemeId);
}

function ChangeAdSize(width, height) {
    var url = previewURL;
    var previewWidth = width;
    var previewHeight = height;
    
    var imageURL = url.replace("../", ADSCAPTCHA_URL);
    var imageHolder = $('.AdImage');
    imageHolder.attr("width", width);
    imageHolder.attr("height", height);
    imageHolder.attr("src", imageURL);

    var sliderURL = url.replace("../", ADSCAPTCHA_CDN);
    SetSlider('FlashSlider', sliderURL, previewWidth, previewHeight, previewThemeId);
    SetSlider('FlashSecuritySlider', previewSecuritySliderURL, previewWidth, previewHeight, previewThemeId);
}

function ChangeSlogan(slogan, width, dir) {
    var challengeHolder = $('.ChallengeImage');

    if (slogan == '' || slogan == null)
        slogan = 'Your Message';
    slogan = encodeURIComponent(slogan);

    var theme = getThemeById(previewThemeId);
    var challengeURL = ADSCAPTCHA_API + "Widget.aspx" + "?s=" + slogan + "&w=" + width + "&bc=" + theme.captcha_background_color.replace('#', '') + "&tc=" + theme.captcha_text_color.replace('#', '') + "&d=" + dir;
    challengeHolder.attr("src", challengeURL);
}

function SetSlider(id, url, width, height, themeId) {
    var theme = getThemeById(themeId);
    
    var gameString = url + ',2,' + width + ',' + height + ',0,' + (width / 3) + ',' + width + ',' + (width / 3) + ',0,' + width + ',null';
    var gameID = Math.random();
    var widgetBackground = theme.widget_background_color.replace('#', '');
    var frameColor = theme.widget_border_color.replace('#', '');
    var frameThickness = theme.widget_border_width;
    var borderColor = 'FFFFFF';
    var borderThickness = 2;
    var callback = 'updateAdsCaptcha';
    var gameDemoMode = 'true';
    var flashParamsArray = ['gameString=' + encodeURIComponent(gameString), 'gameID=' + encodeURIComponent(gameID), 'callback=' + encodeURIComponent(callback), 'frameColor=' + encodeURIComponent(frameColor), 'frameThickness=' + encodeURIComponent(frameThickness), 'borderColor=' + encodeURIComponent(borderColor), 'borderThickness=' + encodeURIComponent(borderThickness), 'gameDemoMode=' + encodeURIComponent(gameDemoMode)];

    var flash_holder = document.getElementById(id);

    var height2 = parseInt(height) + 50;
    var flash_html = renderFlash('AdsCaptchaFlash', 'attrClass', 'AdsCaptcha', width, height2, widgetBackground, 'AdsCaptchaGames', flashParamsArray);

    var div = document.createElement('div');
    div.innerHTML = flash_html;
    if (flash_holder.firstChild)
        flash_holder.removeChild(flash_holder.firstChild);
    flash_holder.appendChild(div);
}

function GetSlider(id, url, width, height, themeId) {
    var theme = getThemeById(themeId);

   
    var likeButtonWidth = Math.min(180, width - 44 - 20);
    var innerHTML = "";

    var message = "SLIDE TO FIT";

    innerHTML += "<div id='AdsCaptchaWidget' class='AdsCaptchaWidget' style='width:" + (width + 2 * theme.widget_border_width) + "px;'>";

    innerHTML += "<div class='ButtonsHolder' style='text-align:right;'>";
    innerHTML += "<div style='float:left;width:116px;height:25px;overflow:hidden;'><div id='likeButton' style='width:450px;height:25px;'></div></div>";
    innerHTML += "<img class='RefreshButton' src='" + ADSCAPTCHA_API + "images/default/button_refresh.gif' width='22' height='20'>";
    innerHTML += "&nbsp;";
    innerHTML += "<img class='HelpButton' src='" + ADSCAPTCHA_API + "images/default/button_help.gif' width='22' height='20'>";
    innerHTML += "</div>";

    innerHTML += "<div id='" + id + "' class='AdsCaptchaSlider'>";
    innerHTML += "</div>";
        
    innerHTML += "<div class='SliderMessage'>";
    innerHTML += "<span class='Instructions'>" + message + "</span>";
    innerHTML += "</div>";

    innerHTML += "</div>";

    document.write(innerHTML);

    SetSlider(id, url, width, height, themeId);
}

// type: 1=security, 2=slogan, 3=image, 4=flash(swf)
function GetCaptcha(type, slogan, url, width, height, dir, themeId) {
    var theme = getThemeById(themeId);

    var innerHTML = "";

    var message = "Type text here";
    var direction = "ltr";
    var align = "left";

    var adWidth = width;
    width = width + (2 * theme.widget_border_width);

    innerHTML += "<div id='AdsCaptchaWidget' class='AdsCaptchaWidget' style='direction:" + direction + ";width:" + width + "px;'>";

    innerHTML += "<div class='ButtonsHolder' style='text-align:" + (align == "left" ? "right" : "left") + ";'>";
    innerHTML += "<img class='RefreshButton' src='" + ADSCAPTCHA_API + "images/" + theme.folder + "/button_refresh.gif' width='22' height='20'>";
    innerHTML += "&nbsp;";
    innerHTML += "<img class='HelpButton' src='" + ADSCAPTCHA_API + "images/" + theme.folder + "/button_help.gif' width='22' height='20'>";
    innerHTML += "</div>";
    
    if (type != 1 && type != 2)
        innerHTML += "<div class='ImageHolder' style='width:" + adWidth + "px;border-width:" + theme.widget_border_width + "px;border-color:" + theme.widget_border_color + ";'>";

    if (type == 3) { // image
        if (url != null && url != "") {
            innerHTML += "<img class='AdImage' src='" + url + "' width='" + adWidth + "' height='" + height + "' />";
        }
    }
    if (type == 4) { // flash
        if (url != null && url != "") {
            innerHTML += "<object height='" + height + "' width='" + adWidth + "' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0' classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000'>";
            innerHTML += "<param name='swLiveConnect' value='true' />";
            innerHTML += "<param name='movie' value='" + url + "' />";
            innerHTML += "<param name='src' value='" + url + "' />";
            innerHTML += "<param name='wmode' value='null' />";
            innerHTML += "<embed src='" + url + "' quality='autohigh' width='" + adWidth + "' height='" + height + "' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash'></embed>";
            innerHTML += "</object>";
        }
    }

    if (type != 1 && type != 2)
        innerHTML += "</div>";

    var challengeURL = ADSCAPTCHA_API + "Widget.aspx" + "?s=" + slogan + "&w=" + width + "&bc=" + theme.captcha_background_color.replace('#', '') + "&tc=" + theme.captcha_text_color.replace('#', '') + "&d=" + dir;
    innerHTML += "<div class='ChallengeHolder'>";
    innerHTML += "<img class='ChallengeImage' src='" + challengeURL + "' width='" + width + "' height='30' />";
    innerHTML += "</div>";

    innerHTML += "<div class='ResponseHolder'>";
    innerHTML += "<input class='ResponseField' type='text' value='' maxlength='26' autocomplete='off' disabled style='width:" + (width * 0.9) + "px;' />";
    innerHTML += "<span class='Instructions'>" + message + "</span>";
    innerHTML += "</div>";

    innerHTML += "</div>";

    document.write(innerHTML);
}

function renderFlash(attrId, attrClass, attrTitle, attrW, attrH, background, flashSrcName, flashParamsArray) {
    var flashHTML = '';
    var isIE = navigator.appName.indexOf("Microsoft") != -1;
    var flashVars = flashParamsArray.join("&");
    var flashPath = ADSCAPTCHA_URL + "images/preview/" + flashSrcName + ".swf";

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
    } else {
        flashHTML += '<embed name="' + attrId + '" src="' + flashPath + '" ' + ' title="' + attrTitle + '" ' + ' width="' + attrW + '" height="' + attrH + '" ' + ' quality="high" ' + ' type="application/x-shockwave-flash" pluginspage="https://www.macromedia.com/go/getflashplayer" play="true" loop="true" allowScriptAccess="always" allowFullScreen="false" scale="noScale" align="middle" menu="false" bgcolor="' + background + '" wmode="null" ' + ' FlashVars="' + flashVars + '">';
    }

    return flashHTML;
}




/* NEW WIDGET */

var previewType = 1;

var previewImage = '';
var previewVideo = '';
var previewSlider = '';

// type: 1=security | 2=slogan | 3=image | 4=video | 5=slider | 6=security slider
function previewChangeType(type) {
    var imageHolder = $('.ImageHolder');
    var videoHolder = $('.VideoHolder');
    var challengeHolder = $('.ChallengeHolder');
    var sliderHolder = $('.SliderHolder');
    var responseHolder = $('.ResponseHolder');

    switch (type) {
        case 1:
        case 2:
            imageHolder.hide(); videoHolder.hide(); challengeHolder.show(); sliderHolder.hide(); responseHolder.show(); break;
        case 3:
            imageHolder.show(); videoHolder.hide(); challengeHolder.show(); sliderHolder.hide(); responseHolder.show(); break;
        case 4:
            imageHolder.hide(); videoHolder.show(); challengeHolder.show(); sliderHolder.hide(); responseHolder.show(); break;
        case 5:
        case 6:
            imageHolder.hide(); videoHolder.hide(); challengeHolder.hide(); sliderHolder.show(); responseHolder.hide(); break;
    }

    previewType = type;
}

function previewLoadImage(url, width, height) {
    var imageHolder = $('.ImageHolder');

    previewImage = url;
    previewWidth = width;
    previewHeight = height;

    url = url.replace("../", ADSCAPTCHA_URL);

    var innerHTML = ""; 
    innerHTML += "<img src='" + url + "' width='" + width + "' height='" + height + "' />";

    imageHolder.html(innerHTML);
}

function previewLoadVideo(url, width, height) {
    var videoHolder = $('.VideoHolder');

    previewVideo = url;
    previewWidth = width;
    previewHeight = height;

    var innerHTML = "";
    innerHTML += "<object height='" + (height) + "' width='" + width + "' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0' classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000'>";
    innerHTML += "<param name='swLiveConnect' value='true' />";
    innerHTML += "<param name='movie' value='" + url + "' />";
    innerHTML += "<param name='src' value='" + url + "' />";
    innerHTML += "<param name='wmode' value='null' />";
    innerHTML += "<embed src='" + url + "' quality='autohigh' width='" + width + "' height='" + height + "' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash'></embed>";
    innerHTML += "</object>";

    videoHolder.html(innerHTML);
}

function previewLoadSlider(url, width, height) {
    var sliderHolder = $('.SliderHolder');

    previewSlider = url;
    previewWidth = width;
    previewHeight = height;

    var theme = getThemeById(themeId);

    var gameString = url + ',2,' + width + ',' + height + ',0,' + (width / 3) + ',' + width + ',' + (width / 3) + ',0,' + width + ',null';
    var gameID = Math.random();
    var background = theme.widget_background_color;
    var frameColor = theme.ad_border_color.replace('#', '');
    var frameThickness = theme.ad_border_width;
    var borderColor = 'FFFFFF';
    var borderThickness = 2;
    var callback = 'updateAdsCaptcha';
    var gameDemoMode = 'true';
    var flashParamsArray = ['gameString=' + encodeURIComponent(gameString), 'gameID=' + encodeURIComponent(gameID), 'callback=' + encodeURIComponent(callback), 'frameColor=' + encodeURIComponent(frameColor), 'frameThickness=' + encodeURIComponent(frameThickness), 'borderColor=' + encodeURIComponent(borderColor), 'borderThickness=' + encodeURIComponent(borderThickness), 'gameDemoMode=' + encodeURIComponent(gameDemoMode)];

    var innerHTML = previewGetSliderHTML('AdsCaptchaFlash', 'attrClass', 'AdsCaptcha', width, height + 50, background, 'AdsCaptchaGames', flashParamsArray);

    sliderHolder.html(innerHTML);
}

function previewLoadSlogan(slogan, width, dir) {
    var challengeHolder = $('.ChallengeHolder');
    var theme = getThemeById(themeId);

    previewSlogan = slogan;

    var captchaBackColor = theme.captcha_background_color.replace('#', '');
    var captchaTextColor = theme.captcha_text_color.replace('#', '');

    if (slogan == '' || slogan == null)
        slogan = 'Your Message';

    slogan = encodeURIComponent(slogan);

    var challengeImage = ADSCAPTCHA_API + "Widget.aspx" + "?s=" + slogan + "&w=" + width + "&bc=" + captchaBackColor + "&tc=" + captchaTextColor + "&d=" + dir;
    var innerHTML = "<img class='ChallengeImage' src='" + challengeImage + "' width='" + width + "' height='30' />";

    challengeHolder.html(innerHTML);
}

function previewDisplayCaptcha(slogan, width, height, dir, themeId) {
    var theme = getThemeById(themeId);

    var innerHTML = "";

    var message = "Type text here";
    var direction = "ltr";
    var align = "left";

    innerHTML += "<div id='AdsCaptchaWidget' class='AdsCaptchaWidget' style='direction:" + direction + ";width:" + width + "px;'>";

    innerHTML += "<div class='ButtonsHolder' style='text-align:" + (align == "left" ? "right" : "left") + ";'>";
    innerHTML += "<img class='RefreshButton' src='" + ADSCAPTCHA_API + "images/default/button_refresh.gif' width='22' height='20'>";
    innerHTML += "&nbsp;";
    innerHTML += "<img class='HelpButton' src='" + ADSCAPTCHA_API + "images/default/button_help.gif' width='22' height='20'>";
    innerHTML += "</div>";

    innerHTML += "<div class='AdHolder ImageHolder' style='width:" + width + "px;'></div>";
    innerHTML += "<div class='AdHolder VideoHolder' style='width:" + width + "px;'></div>";

    innerHTML += "<div class='SliderHolder'>";
    innerHTML += "</div>";

    innerHTML += "<div class='ChallengeHolder'>";
    innerHTML += "</div>";

    innerHTML += "<div class='ResponseHolder'>";
    innerHTML += "<input class='ResponseField' type='text' value='' maxlength='26' autocomplete='off' disabled style='width:" + (width * 0.9) + "px;' />";
    innerHTML += "<span class='Instructions'>" + message + "</span>";
    innerHTML += "</div>";

    innerHTML += "</div>";

    document.write(innerHTML);
}

function previewGetSliderHTML(attrId, attrClass, attrTitle, attrW, attrH, background, flashSrcName, flashParamsArray) {
    var flashHTML = '';
    var isIE = navigator.appName.indexOf("Microsoft") != -1;
    var flashVars = flashParamsArray.join("&");
    var flashPath = ADSCAPTCHA_URL + "images/preview/" + flashSrcName + ".swf";

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
    } else {
        flashHTML += '<embed name="' + attrId + '" src="' + flashPath + '" ' + ' title="' + attrTitle + '" ' + ' width="' + attrW + '" height="' + attrH + '" ' + ' quality="high" ' + ' type="application/x-shockwave-flash" pluginspage="https://www.macromedia.com/go/getflashplayer" play="true" loop="true" allowScriptAccess="always" allowFullScreen="false" scale="noScale" align="middle" menu="false" bgcolor="' + background + '" wmode="null" ' + ' FlashVars="' + flashVars + '">';
    }

    return flashHTML;
}