﻿/**
* SWFObject v1.5: Flash Player detection and embed - http://blog.deconcept.com/swfobject/
*
* SWFObject is (c) 2007 Geoff Stearns and is released under the MIT License:
* http://www.opensource.org/licenses/mit-license.php
*
*/
if (typeof deconcept == "undefined") { var deconcept = new Object(); } if (typeof deconcept.util == "undefined") { deconcept.util = new Object(); } if (typeof deconcept.SWFObjectUtil == "undefined") { deconcept.SWFObjectUtil = new Object(); } deconcept.SWFObject = function(_1, id, w, h, _5, c, _7, _8, _9, _a) { if (!document.getElementById) { return; } this.DETECT_KEY = _a ? _a : "detectflash"; this.skipDetect = deconcept.util.getRequestParameter(this.DETECT_KEY); this.params = new Object(); this.variables = new Object(); this.attributes = new Array(); if (_1) { this.setAttribute("swf", _1); } if (id) { this.setAttribute("id", id); } if (w) { this.setAttribute("width", w); } if (h) { this.setAttribute("height", h); } if (_5) { this.setAttribute("version", new deconcept.PlayerVersion(_5.toString().split("."))); } this.installedVer = deconcept.SWFObjectUtil.getPlayerVersion(); if (!window.opera && document.all && this.installedVer.major > 7) { deconcept.SWFObject.doPrepUnload = true; } if (c) { this.addParam("bgcolor", c); } var q = _7 ? _7 : "high"; this.addParam("quality", q); this.setAttribute("useExpressInstall", false); this.setAttribute("doExpressInstall", false); var _c = (_8) ? _8 : window.location; this.setAttribute("xiRedirectUrl", _c); this.setAttribute("redirectUrl", ""); if (_9) { this.setAttribute("redirectUrl", _9); } }; deconcept.SWFObject.prototype = { useExpressInstall: function(_d) { this.xiSWFPath = !_d ? "expressinstall.swf" : _d; this.setAttribute("useExpressInstall", true); }, setAttribute: function(_e, _f) { this.attributes[_e] = _f; }, getAttribute: function(_10) { return this.attributes[_10]; }, addParam: function(_11, _12) { this.params[_11] = _12; }, getParams: function() { return this.params; }, addVariable: function(_13, _14) { this.variables[_13] = _14; }, getVariable: function(_15) { return this.variables[_15]; }, getVariables: function() { return this.variables; }, getVariablePairs: function() { var _16 = new Array(); var key; var _18 = this.getVariables(); for (key in _18) { _16[_16.length] = key + "=" + _18[key]; } return _16; }, getSWFHTML: function() { var _19 = ""; if (navigator.plugins && navigator.mimeTypes && navigator.mimeTypes.length) { if (this.getAttribute("doExpressInstall")) { this.addVariable("MMplayerType", "PlugIn"); this.setAttribute("swf", this.xiSWFPath); } _19 = "<embed type=\"application/x-shockwave-flash\" src=\"" + this.getAttribute("swf") + "\" width=\"" + this.getAttribute("width") + "\" height=\"" + this.getAttribute("height") + "\" style=\"" + this.getAttribute("style") + "\""; _19 += " id=\"" + this.getAttribute("id") + "\" name=\"" + this.getAttribute("id") + "\" "; var _1a = this.getParams(); for (var key in _1a) { _19 += [key] + "=\"" + _1a[key] + "\" "; } var _1c = this.getVariablePairs().join("&"); if (_1c.length > 0) { _19 += "flashvars=\"" + _1c + "\""; } _19 += "/>"; } else { if (this.getAttribute("doExpressInstall")) { this.addVariable("MMplayerType", "ActiveX"); this.setAttribute("swf", this.xiSWFPath); } _19 = "<object id=\"" + this.getAttribute("id") + "\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" width=\"" + this.getAttribute("width") + "\" height=\"" + this.getAttribute("height") + "\" style=\"" + this.getAttribute("style") + "\">"; _19 += "<param name=\"movie\" value=\"" + this.getAttribute("swf") + "\" />"; var _1d = this.getParams(); for (var key in _1d) { _19 += "<param name=\"" + key + "\" value=\"" + _1d[key] + "\" />"; } var _1f = this.getVariablePairs().join("&"); if (_1f.length > 0) { _19 += "<param name=\"flashvars\" value=\"" + _1f + "\" />"; } _19 += "</object>"; } return _19; }, write: function(_20) { if (this.getAttribute("useExpressInstall")) { var _21 = new deconcept.PlayerVersion([6, 0, 65]); if (this.installedVer.versionIsValid(_21) && !this.installedVer.versionIsValid(this.getAttribute("version"))) { this.setAttribute("doExpressInstall", true); this.addVariable("MMredirectURL", escape(this.getAttribute("xiRedirectUrl"))); document.title = document.title.slice(0, 47) + " - Flash Player Installation"; this.addVariable("MMdoctitle", document.title); } } if (this.skipDetect || this.getAttribute("doExpressInstall") || this.installedVer.versionIsValid(this.getAttribute("version"))) { var n = (typeof _20 == "string") ? document.getElementById(_20) : _20; n.innerHTML = this.getSWFHTML(); return true; } else { if (this.getAttribute("redirectUrl") != "") { document.location.replace(this.getAttribute("redirectUrl")); } } return false; } }; deconcept.SWFObjectUtil.getPlayerVersion = function() { var _23 = new deconcept.PlayerVersion([0, 0, 0]); if (navigator.plugins && navigator.mimeTypes.length) { var x = navigator.plugins["Shockwave Flash"]; if (x && x.description) { _23 = new deconcept.PlayerVersion(x.description.replace(/([a-zA-Z]|\s)+/, "").replace(/(\s+r|\s+b[0-9]+)/, ".").split(".")); } } else { if (navigator.userAgent && navigator.userAgent.indexOf("Windows CE") >= 0) { var axo = 1; var _26 = 3; while (axo) { try { _26++; axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash." + _26); _23 = new deconcept.PlayerVersion([_26, 0, 0]); } catch (e) { axo = null; } } } else { try { var axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.7"); } catch (e) { try { var axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.6"); _23 = new deconcept.PlayerVersion([6, 0, 21]); axo.AllowScriptAccess = "always"; } catch (e) { if (_23.major == 6) { return _23; } } try { axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash"); } catch (e) { } } if (axo != null) { _23 = new deconcept.PlayerVersion(axo.GetVariable("$version").split(" ")[1].split(",")); } } } return _23; }; deconcept.PlayerVersion = function(_29) { this.major = _29[0] != null ? parseInt(_29[0]) : 0; this.minor = _29[1] != null ? parseInt(_29[1]) : 0; this.rev = _29[2] != null ? parseInt(_29[2]) : 0; }; deconcept.PlayerVersion.prototype.versionIsValid = function(fv) { if (this.major < fv.major) { return false; } if (this.major > fv.major) { return true; } if (this.minor < fv.minor) { return false; } if (this.minor > fv.minor) { return true; } if (this.rev < fv.rev) { return false; } return true; }; deconcept.util = { getRequestParameter: function(_2b) { var q = document.location.search || document.location.hash; if (_2b == null) { return q; } if (q) { var _2d = q.substring(1).split("&"); for (var i = 0; i < _2d.length; i++) { if (_2d[i].substring(0, _2d[i].indexOf("=")) == _2b) { return _2d[i].substring((_2d[i].indexOf("=") + 1)); } } } return ""; } }; deconcept.SWFObjectUtil.cleanupSWFs = function() { var _2f = document.getElementsByTagName("OBJECT"); for (var i = _2f.length - 1; i >= 0; i--) { _2f[i].style.display = "none"; for (var x in _2f[i]) { if (typeof _2f[i][x] == "function") { _2f[i][x] = function() { }; } } } }; if (deconcept.SWFObject.doPrepUnload) { if (!deconcept.unloadSet) { deconcept.SWFObjectUtil.prepUnload = function() { __flash_unloadHandler = function() { }; __flash_savedUnloadHandler = function() { }; window.attachEvent("onunload", deconcept.SWFObjectUtil.cleanupSWFs); }; window.attachEvent("onbeforeunload", deconcept.SWFObjectUtil.prepUnload); deconcept.unloadSet = true; } } if (!document.getElementById && document.all) { document.getElementById = function(id) { return document.all[id]; }; } var getQueryParamValue = deconcept.util.getRequestParameter; var FlashObject = deconcept.SWFObject; var SWFObject = deconcept.SWFObject;


/*
* jQuery Media Plugin for converting elements into rich media content.
*
* Examples and documentation at: http://malsup.com/jquery/media/
* Copyright (c) 2007-2010 M. Alsup
* Dual licensed under the MIT and GPL licenses:
* http://www.opensource.org/licenses/mit-license.php
* http://www.gnu.org/licenses/gpl.html
*
* @author: M. Alsup
* @version: 0.96 (23-MAR-2011)
* @requires jQuery v1.1.2 or later
* $Id: jquery.media.js 2460 2007-07-23 02:53:15Z malsup $
*
* Supported Media Players:
*	- Flash
*	- Quicktime
*	- Real Player
*	- Silverlight
*	- Windows Media Player
*	- iframe
*
* Supported Media Formats:
*	 Any types supported by the above players, such as:
*	 Video: asf, avi, flv, mov, mpg, mpeg, mp4, qt, smil, swf, wmv, 3g2, 3gp
*	 Audio: aif, aac, au, gsm, mid, midi, mov, mp3, m4a, snd, rm, wav, wma
*	 Other: bmp, html, pdf, psd, qif, qtif, qti, tif, tiff, xaml
*
* Thanks to Mark Hicken and Brent Pedersen for helping me debug this on the Mac!
* Thanks to Dan Rossi for numerous bug reports and code bits!
* Thanks to Skye Giordano for several great suggestions!
* Thanks to Richard Connamacher for excellent improvements to the non-IE behavior!
*/
; (function($) {

    var lameIE = $.browser.msie && $.browser.version < 9;

    /**
    * Chainable method for converting elements into rich media.
    *
    * @param options
    * @param callback fn invoked for each matched element before conversion
    * @param callback fn invoked for each matched element after conversion
    */
    $.fn.media = function(options, f1, f2) {
        if (options == 'undo') {
            return this.each(function() {
                var $this = $(this);
                var html = $this.data('media.origHTML');
                if (html)
                    $this.replaceWith(html);
            });
        }

        return this.each(function() {
            if (typeof options == 'function') {
                f2 = f1;
                f1 = options;
                options = {};
            }
            var o = getSettings(this, options);
            // pre-conversion callback, passes original element and fully populated options
            if (typeof f1 == 'function') f1(this, o);

            var r = getTypesRegExp();
            var m = r.exec(o.src.toLowerCase()) || [''];

            o.type ? m[0] = o.type : m.shift();
            for (var i = 0; i < m.length; i++) {
                fn = m[i].toLowerCase();
                if (isDigit(fn[0])) fn = 'fn' + fn; // fns can't begin with numbers
                if (!$.fn.media[fn])
                    continue;  // unrecognized media type
                // normalize autoplay settings
                var player = $.fn.media[fn + '_player'];
                if (!o.params) o.params = {};
                if (player) {
                    var num = player.autoplayAttr == 'autostart';
                    o.params[player.autoplayAttr || 'autoplay'] = num ? (o.autoplay ? 1 : 0) : o.autoplay ? true : false;
                }
                var $div = $.fn.media[fn](this, o);

                $div.css('backgroundColor', o.bgColor).width(o.width);

                if (o.canUndo) {
                    var $temp = $('<div></div>').append(this);
                    $div.data('media.origHTML', $temp.html()); // store original markup
                }

                // post-conversion callback, passes original element, new div element and fully populated options
                if (typeof f2 == 'function') f2(this, $div[0], o, player.name);
                break;
            }
        });
    };

    /**
    * Non-chainable method for adding or changing file ContentType / player mapping
    * @name mapFormat
    * @param String ContentType File ContentType extension (ie: mov, wav, mp3)
    * @param String player Player name to use for the ContentType (one of: flash, quicktime, realplayer, winmedia, silverlight or iframe
    */
    $.fn.media.mapFormat = function(format, player) {
        if (!format || !player || !$.fn.media.defaults.players[player]) return; // invalid
        format = format.toLowerCase();
        if (isDigit(format[0])) format = 'fn' + format;
        $.fn.media[format] = $.fn.media[player];
        $.fn.media[format + '_player'] = $.fn.media.defaults.players[player];
    };

    // global defautls; override as needed
    $.fn.media.defaults = {
        standards: true,       // use object tags only (no embeds for non-IE browsers)
        canUndo: true,       // tells plugin to store the original markup so it can be reverted via: $(sel).mediaUndo()
        width: 400,
        height: 400,
        autoplay: 0, 	   	// normalized cross-player setting
        bgColor: '#ffffff', 	// background color
        params: { wmode: 'transparent' }, // added to object element as param elements; added to embed element as attrs
        attrs: {}, 		// added to object and embed elements as attrs
        flvKeyName: 'file', 	// key used for object src param (thanks to Andrea Ercolino)
        flashvars: {}, 		// added to flash content as flashvars param/attr
        flashVersion: '7', // required flash version
        expressInstaller: null, // src for express installer

        // default flash video and mp3 player (@see: http://jeroenwijering.com/?item=Flash_Media_Player)
        flvPlayer: 'mediaplayer.swf',
        mp3Player: 'mediaplayer.swf',

        // @see http://msdn2.microsoft.com/en-us/library/bb412401.aspx
        silverlight: {
            inplaceInstallPrompt: 'true', // display in-place install prompt?
            isWindowless: 'true', // windowless mode (false for wrapping markup)
            framerate: '24',   // maximum framerate
            version: '0.9',  // Silverlight version
            onError: null,   // onError callback
            onLoad: null,   // onLoad callback
            initParams: null,   // object init params
            userContext: null	  // callback arg passed to the load callback
        }
    };

    // Media Players; think twice before overriding
    $.fn.media.defaults.players = {
        flash: {
            name: 'flash',
            title: 'Flash',
            types: 'flv,mp3,swf',
            mimetype: 'application/x-shockwave-flash',
            pluginspage: 'http://www.adobe.com/go/getflashplayer',
            ieAttrs: {
                classid: 'clsid:d27cdb6e-ae6d-11cf-96b8-444553540000',
                type: 'application/x-oleobject',
                codebase: 'http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=' + $.fn.media.defaults.flashVersion
            }
        },
        quicktime: {
            name: 'quicktime',
            title: 'QuickTime',
            mimetype: 'video/quicktime',
            pluginspage: 'http://www.apple.com/quicktime/download/',
            types: 'aif,aiff,aac,au,bmp,gsm,mov,mid,midi,mpg,mpeg,mp4,m4a,psd,qt,qtif,qif,qti,snd,tif,tiff,wav,3g2,3gp',
            ieAttrs: {
                classid: 'clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B',
                codebase: 'http://www.apple.com/qtactivex/qtplugin.cab'
            }
        },
        realplayer: {
            name: 'real',
            title: 'RealPlayer',
            types: 'ra,ram,rm,rpm,rv,smi,smil',
            mimetype: 'audio/x-pn-realaudio-plugin',
            pluginspage: 'http://www.real.com/player/',
            autoplayAttr: 'autostart',
            ieAttrs: {
                classid: 'clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA'
            }
        },
        winmedia: {
            name: 'winmedia',
            title: 'Windows Media',
            types: 'asx,asf,avi,wma,wmv',
            mimetype: $.browser.mozilla && isFirefoxWMPPluginInstalled() ? 'application/x-ms-wmp' : 'application/x-mplayer2',
            pluginspage: 'http://www.microsoft.com/Windows/MediaPlayer/',
            autoplayAttr: 'autostart',
            oUrl: 'url',
            ieAttrs: {
                classid: 'clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6',
                type: 'application/x-oleobject'
            }
        },
        // special cases
        img: {
            name: 'img',
            title: 'Image',
            types: 'gif,png,jpg'
        },
        iframe: {
            name: 'iframe',
            types: 'html,pdf'
        },
        silverlight: {
            name: 'silverlight',
            types: 'xaml'
        }
    };

    //
    //	everything below here is private
    //


    // detection script for FF WMP plugin (http://www.therossman.org/experiments/wmp_play.html)
    // (hat tip to Mark Ross for this script)
    function isFirefoxWMPPluginInstalled() {
        var plugs = navigator.plugins;
        for (var i = 0; i < plugs.length; i++) {
            var plugin = plugs[i];
            if (plugin['filename'] == 'np-mswmp.dll')
                return true;
        }
        return false;
    }

    var counter = 1;

    for (var player in $.fn.media.defaults.players) {
        var types = $.fn.media.defaults.players[player].types;
        $.each(types.split(','), function(i, o) {
            if (isDigit(o[0])) o = 'fn' + o;
            $.fn.media[o] = $.fn.media[player] = getGenerator(player);
            $.fn.media[o + '_player'] = $.fn.media.defaults.players[player];
        });
    };

    function getTypesRegExp() {
        var types = '';
        for (var player in $.fn.media.defaults.players) {
            if (types.length) types += ',';
            types += $.fn.media.defaults.players[player].types;
        };
        return new RegExp('\\.(' + types.replace(/,/ig, '|') + ')\\b');
    };

    function getGenerator(player) {
        return function(el, options) {
            return generate(el, options, player);
        };
    };

    function isDigit(c) {
        return '0123456789'.indexOf(c) > -1;
    };

    // flatten all possible options: global defaults, meta, option obj
    function getSettings(el, options) {
        options = options || {};
        var $el = $(el);
        var cls = el.className || '';
        // support metadata plugin (v1.0 and v2.0)
        var meta = $.metadata ? $el.metadata() : $.meta ? $el.data() : {};
        meta = meta || {};
        var w = meta.width || parseInt(((cls.match(/\bw:(\d+)/) || [])[1] || 0)) || parseInt(((cls.match(/\bwidth:(\d+)/) || [])[1] || 0));
        var h = meta.height || parseInt(((cls.match(/\bh:(\d+)/) || [])[1] || 0)) || parseInt(((cls.match(/\bheight:(\d+)/) || [])[1] || 0))

        if (w) meta.width = w;
        if (h) meta.height = h;
        if (cls) meta.cls = cls;

        // crank html5 style data attributes
        var dataName = 'data-';
        for (var i = 0; i < el.attributes.length; i++) {
            var a = el.attributes[i], n = $.trim(a.name);
            var index = n.indexOf(dataName);
            if (index === 0) {
                n = n.substring(dataName.length);
                meta[n] = a.value;
            }
        }

        var a = $.fn.media.defaults;
        var b = options;
        var c = meta;

        var p = { params: { bgColor: options.bgColor || $.fn.media.defaults.bgColor} };
        var opts = $.extend({}, a, b, c);
        $.each(['attrs', 'params', 'flashvars', 'silverlight'], function(i, o) {
            opts[o] = $.extend({}, p[o] || {}, a[o] || {}, b[o] || {}, c[o] || {});
        });

        if (typeof opts.caption == 'undefined') opts.caption = $el.text();

        // make sure we have a source!
        opts.src = opts.src || $el.attr('href') || $el.attr('src') || 'unknown';
        return opts;
    };

    //
    //	Flash Player
    //

    // generate flash using SWFObject library if possible
    $.fn.media.swf = function(el, opts) {
        if (!window.SWFObject && !window.swfobject) {
            // roll our own
            if (opts.flashvars) {
                var a = [];
                for (var f in opts.flashvars)
                    a.push(f + '=' + opts.flashvars[f]);
                if (!opts.params) opts.params = {};
                opts.params.flashvars = a.join('&');
            }
            return generate(el, opts, 'flash');
        }

        var id = el.id ? (' id="' + el.id + '"') : '';
        var cls = opts.cls ? (' class="' + opts.cls + '"') : '';
        var $div = $('<div' + id + cls + '>');

        // swfobject v2+
        if (window.swfobject) {
            $(el).after($div).appendTo($div);
            if (!el.id) el.id = 'movie_player_' + counter++;

            // replace el with swfobject content
            swfobject.embedSWF(opts.src, el.id, opts.width, opts.height, opts.flashVersion,
			opts.expressInstaller, opts.flashvars, opts.params, opts.attrs);
        }
        // swfobject < v2
        else {
            $(el).after($div).remove();
            var so = new SWFObject(opts.src, 'movie_player_' + counter++, opts.width, opts.height, opts.flashVersion, opts.bgColor);
            if (opts.expressInstaller) so.useExpressInstall(opts.expressInstaller);

            for (var p in opts.params)
                if (p != 'bgColor') so.addParam(p, opts.params[p]);
            for (var f in opts.flashvars)
                so.addVariable(f, opts.flashvars[f]);
            so.write($div[0]);
        }

        if (opts.caption) $('<div>').appendTo($div).html(opts.caption);
        return $div;
    };

    // map flv and mp3 files to the swf player by default
    $.fn.media.flv = $.fn.media.mp3 = function(el, opts) {
        var src = opts.src;
        var player = /\.mp3\b/i.test(src) ? $.fn.media.defaults.mp3Player : $.fn.media.defaults.flvPlayer;
        var key = opts.flvKeyName;
        src = encodeURIComponent(src);
        opts.src = player;
        opts.src = opts.src + '?' + key + '=' + (src);
        var srcObj = {};
        srcObj[key] = src;
        opts.flashvars = $.extend({}, srcObj, opts.flashvars);
        return $.fn.media.swf(el, opts);
    };

    //
    //	Silverlight
    //
    $.fn.media.xaml = function(el, opts) {
        if (!window.Sys || !window.Sys.Silverlight) {
            if ($.fn.media.xaml.warning) return;
            $.fn.media.xaml.warning = 1;
            alert('You must include the Silverlight.js script.');
            return;
        }

        var props = {
            width: opts.width,
            height: opts.height,
            background: opts.bgColor,
            inplaceInstallPrompt: opts.silverlight.inplaceInstallPrompt,
            isWindowless: opts.silverlight.isWindowless,
            framerate: opts.silverlight.framerate,
            version: opts.silverlight.version
        };
        var events = {
            onError: opts.silverlight.onError,
            onLoad: opts.silverlight.onLoad
        };

        var id1 = el.id ? (' id="' + el.id + '"') : '';
        var id2 = opts.id || 'AG' + counter++;
        // convert element to div
        var cls = opts.cls ? (' class="' + opts.cls + '"') : '';
        var $div = $('<div' + id1 + cls + '>');
        $(el).after($div).remove();

        Sys.Silverlight.createObjectEx({
            source: opts.src,
            initParams: opts.silverlight.initParams,
            userContext: opts.silverlight.userContext,
            id: id2,
            parentElement: $div[0],
            properties: props,
            events: events
        });

        if (opts.caption) $('<div>').appendTo($div).html(opts.caption);
        return $div;
    };

    //
    // generate object/embed markup
    //
    function generate(el, opts, player) {
        var $el = $(el);
        var o = $.fn.media.defaults.players[player];

        if (player == 'iframe') {
            o = $('<iframe' + ' width="' + opts.width + '" height="' + opts.height + '" >');
            o.attr('src', opts.src);
            o.css('backgroundColor', o.bgColor);
        }
        else if (player == 'img') {
            o = $('<img>');
            o.attr('src', opts.src);
            opts.width && o.attr('width', opts.width);
            opts.height && o.attr('height', opts.height);
            o.css('backgroundColor', o.bgColor);
        }
        else if (lameIE) {
            var a = ['<object width="' + opts.width + '" height="' + opts.height + '" '];
            for (var key in opts.attrs)
                a.push(key + '="' + opts.attrs[key] + '" ');
            for (var key in o.ieAttrs || {}) {
                var v = o.ieAttrs[key];
                if (key == 'codebase' && window.location.protocol == 'https:')
                    v = v.replace('http', 'https');
                a.push(key + '="' + v + '" ');
            }
            a.push('></ob' + 'ject' + '>');
            var p = ['<param name="' + (o.oUrl || 'src') + '" value="' + opts.src + '">'];
            for (var key in opts.params)
                p.push('<param name="' + key + '" value="' + opts.params[key] + '">');
            var o = document.createElement(a.join(''));
            for (var i = 0; i < p.length; i++)
                o.appendChild(document.createElement(p[i]));
        }
        else if (opts.standards) {
            // Rewritten to be standards compliant by Richard Connamacher
            var a = ['<object type="' + o.mimetype + '" width="' + opts.width + '" height="' + opts.height + '"'];
            if (opts.src) a.push(' data="' + opts.src + '" ');
            if ($.browser.msie) {
                for (var key in o.ieAttrs || {}) {
                    var v = o.ieAttrs[key];
                    if (key == 'codebase' && window.location.protocol == 'https:')
                        v = v.replace('http', 'https');
                    a.push(key + '="' + v + '" ');
                }
            }
            a.push('>');
            a.push('<param name="' + (o.oUrl || 'src') + '" value="' + opts.src + '">');
            for (var key in opts.params) {
                if (key == 'wmode' && player != 'flash') // FF3/Quicktime borks on wmode
                    continue;
                a.push('<param name="' + key + '" value="' + opts.params[key] + '">');
            }
            // Alternate HTML
            a.push('<div><p><strong>' + o.title + ' Required</strong></p><p>' + o.title + ' is required to view this media. <a href="' + o.pluginspage + '">Download Here</a>.</p></div>');
            a.push('</ob' + 'ject' + '>');
        }
        else {
            var a = ['<embed width="' + opts.width + '" height="' + opts.height + '" style="display:block"'];
            if (opts.src) a.push(' src="' + opts.src + '" ');
            for (var key in opts.attrs)
                a.push(key + '="' + opts.attrs[key] + '" ');
            for (var key in o.eAttrs || {})
                a.push(key + '="' + o.eAttrs[key] + '" ');
            for (var key in opts.params) {
                if (key == 'wmode' && player != 'flash') // FF3/Quicktime borks on wmode
                    continue;
                a.push(key + '="' + opts.params[key] + '" ');
            }
            a.push('></em' + 'bed' + '>');
        }
        // convert element to div
        var id = el.id ? (' id="' + el.id + '"') : '';
        var cls = opts.cls ? (' class="' + opts.cls + '"') : '';
        var $div = $('<div' + id + cls + '>');
        $el.after($div).remove();
        (lameIE || player == 'iframe' || player == 'img') ? $div.append(o) : $div.html(a.join(''));
        if (opts.caption) $('<div>').appendTo($div).html(opts.caption);
        return $div;
    };


})(jQuery);
