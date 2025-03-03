﻿var Hashtable = function() { function c(b) { var d; if (typeof b == "string") return b; if (typeof b.hashCode == a) return d = b.hashCode(), typeof d == "string" ? d : c(d); if (typeof b.toString == a) return b.toString(); try { return String(b) } catch (e) { return Object.prototype.toString.call(b) } } function d(a, b) { return a.equals(b) } function e(b, c) { return typeof c.equals == a ? c.equals(b) : b === c } function f(a) { return function(b) { if (b === null) throw new Error("null is not a valid " + a); if (typeof b == "undefined") throw new Error(a + " must not be undefined") } } function i(a, b, c, d) { this[0] = a, this.entries = [], this.addEntry(b, c), d !== null && (this.getEqualityFunction = function() { return d }) } function m(a) { return function(b) { var c = this.entries.length, d, e = this.getEqualityFunction(b); while (c--) { d = this.entries[c]; if (e(b, d[0])) switch (a) { case j: return !0; case k: return d; case l: return [c, d[1]] } } return !1 } } function n(a) { return function(b) { var c = b.length; for (var d = 0, e = this.entries.length; d < e; ++d) b[c + d] = this.entries[d][a] } } function o(a, b) { var c = a.length, d; while (c--) { d = a[c]; if (b === d[0]) return c } return null } function p(a, b) { var c = a[b]; return c && c instanceof i ? c : null } function q(d, e) { var f = this, j = [], k = {}, l = typeof d == a ? d : c, m = typeof e == a ? e : null; this.put = function(a, b) { g(a), h(b); var c = l(a), d, e, f = null; return d = p(k, c), d ? (e = d.getEntryForKey(a), e ? (f = e[1], e[1] = b) : d.addEntry(a, b)) : (d = new i(c, a, b, m), j[j.length] = d, k[c] = d), f }, this.get = function(a) { g(a); var b = l(a), c = p(k, b); if (c) { var d = c.getEntryForKey(a); if (d) return d[1] } return null }, this.containsKey = function(a) { g(a); var b = l(a), c = p(k, b); return c ? c.containsKey(a) : !1 }, this.containsValue = function(a) { h(a); var b = j.length; while (b--) if (j[b].containsValue(a)) return !0; return !1 }, this.clear = function() { j.length = 0, k = {} }, this.isEmpty = function() { return !j.length }; var n = function(a) { return function() { var b = [], c = j.length; while (c--) j[c][a](b); return b } }; this.keys = n("keys"), this.values = n("values"), this.entries = n("getEntries"), this.remove = function(a) { g(a); var c = l(a), d, e = null, f = p(k, c); return f && (e = f.removeEntryForKey(a), e !== null && (f.entries.length || (d = o(j, c), b(j, d), delete k[c]))), e }, this.size = function() { var a = 0, b = j.length; while (b--) a += j[b].entries.length; return a }, this.each = function(a) { var b = f.entries(), c = b.length, d; while (c--) d = b[c], a(d[0], d[1]) }, this.putAll = function(b, c) { var d = b.entries(), e, g, h, i, j = d.length, k = typeof c == a; while (j--) e = d[j], g = e[0], h = e[1], k && (i = f.get(g)) && (h = c(g, i, h)), f.put(g, h) }, this.clone = function() { var a = new q(d, e); return a.putAll(f), a } } var a = "function", b = typeof Array.prototype.splice == a ? function(a, b) { a.splice(b, 1) } : function(a, b) { var c, d, e; if (b === a.length - 1) a.length = b; else { c = a.slice(b + 1), a.length = b; for (d = 0, e = c.length; d < e; ++d) a[b + d] = c[d] } }, g = f("key"), h = f("value"), j = 0, k = 1, l = 2; return i.prototype = { getEqualityFunction: function(b) { return typeof b.equals == a ? d : e }, getEntryForKey: m(k), getEntryAndIndexForKey: m(l), removeEntryForKey: function(a) { var c = this.getEntryAndIndexForKey(a); return c ? (b(this.entries, c[0]), c[1]) : null }, addEntry: function(a, b) { this.entries[this.entries.length] = [a, b] }, keys: n(0), values: n(1), getEntries: function(a) { var b = a.length; for (var c = 0, d = this.entries.length; c < d; ++c) a[b + c] = this.entries[c].slice(0) }, containsKey: m(j), containsValue: function(a) { var b = this.entries.length; while (b--) if (a === this.entries[b][1]) return !0; return !1 } }, q } (); (function(a) { function i(a, b, c) { this.dec = a, this.group = b, this.neg = c } function j() { for (var a = 0; a < h.length; a++) { localeGroup = h[a]; for (var c = 0; c < localeGroup.length; c++) b.put(localeGroup[c], a) } } function k(a, c) { b.size() == 0 && j(); var d = ".", e = ",", f = "-"; c == 0 && (a.indexOf("_") != -1 ? a = a.split("_")[1].toLowerCase() : a.indexOf("-") != -1 && (a = a.split("-")[1].toLowerCase())); var h = b.get(a); if (h) { var k = g[h]; k && (d = k[0], e = k[1]) } return new i(d, e, f) } var b = new Hashtable, c = ["ae", "au", "ca", "cn", "eg", "gb", "hk", "il", "in", "jp", "sk", "th", "tw", "us"], d = ["at", "br", "de", "dk", "es", "gr", "it", "nl", "pt", "tr", "vn"], e = ["cz", "fi", "fr", "ru", "se", "pl"], f = ["ch"], g = [[".", ","], [",", "."], [",", " "], [".", "'"]], h = [c, d, e, f]; a.fn.formatNumber = function(b, c, d) { return this.each(function() { c == null && (c = !0), d == null && (d = !0); var e; a(this).is(":input") ? e = new String(a(this).val()) : e = new String(a(this).text()); var f = a.formatNumber(e, b); c && (a(this).is(":input") ? a(this).val(f) : a(this).text(f)); if (d) return f }) }, a.formatNumber = function(b, c) { var c = a.extend({}, a.fn.formatNumber.defaults, c), d = k(c.locale.toLowerCase(), c.isFullLocale), e = d.dec, f = d.group, g = d.neg, h = "0#-,.", i = "", j = !1; for (var l = 0; l < c.format.length; l++) { if (h.indexOf(c.format.charAt(l)) != -1) { if (l == 0 && c.format.charAt(l) == "-") { j = !0; continue } break } i += c.format.charAt(l) } var m = ""; for (var l = c.format.length - 1; l >= 0; l--) { if (h.indexOf(c.format.charAt(l)) != -1) break; m = c.format.charAt(l) + m } c.format = c.format.substring(i.length), c.format = c.format.substring(0, c.format.length - m.length); var n = new Number(b); return a._formatNumber(n, c, m, i, j) }, a._formatNumber = function(b, c, d, e, f) { var c = a.extend({}, a.fn.formatNumber.defaults, c), g = k(c.locale.toLowerCase(), c.isFullLocale), h = g.dec, i = g.group, j = g.neg, l = !1; if (isNaN(b)) { if (c.nanForceZero != 1) return null; b = 0, l = !0 } d == "%" && (b *= 100); var m = ""; if (c.format.indexOf(".") > -1) { var n = h, o = c.format.substring(c.format.lastIndexOf(".") + 1); if (c.round == 1) b = new Number(b.toFixed(o.length)); else { var p = b.toString(); p = p.substring(0, p.lastIndexOf(".") + o.length + 1), b = new Number(p) } var q = b % 1, r = new String(q.toFixed(o.length)); r = r.substring(r.lastIndexOf(".") + 1); for (var s = 0; s < o.length; s++) { if (o.charAt(s) == "#" && r.charAt(s) != "0") { n += r.charAt(s); continue } if (o.charAt(s) == "#" && r.charAt(s) == "0") { var t = r.substring(s); if (t.match("[1-9]")) { n += r.charAt(s); continue } break } o.charAt(s) == "0" && (n += r.charAt(s)) } m += n } else b = Math.round(b); var u = Math.floor(b); b < 0 && (u = Math.ceil(b)); var v = ""; c.format.indexOf(".") == -1 ? v = c.format : v = c.format.substring(0, c.format.indexOf(".")); var w = ""; if (u != 0 || v.substr(v.length - 1) != "#" || l) { var x = new String(Math.abs(u)), y = 9999; v.lastIndexOf(",") != -1 && (y = v.length - v.lastIndexOf(",") - 1); var z = 0; for (var s = x.length - 1; s > -1; s--) w = x.charAt(s) + w, z++, z == y && s != 0 && (w = i + w, z = 0); if (v.length > w.length) { var A = v.indexOf("0"); if (A != -1) { var B = v.length - A, C = v.length - w.length - 1; while (w.length < B) { var D = v.charAt(C); D == "," && (D = i), w = D + w, C-- } } } } return !w && v.indexOf("0", v.length - 1) !== -1 && (w = "0"), m = w + m, b < 0 && f && e.length > 0 ? e = j + e : b < 0 && (m = j + m), c.decimalSeparatorAlwaysShown || m.lastIndexOf(h) == m.length - 1 && (m = m.substring(0, m.length - 1)), m = e + m + d, m }, a.fn.parseNumber = function(b, c, d) { c == null && (c = !0), d == null && (d = !0); var e; a(this).is(":input") ? e = new String(a(this).val()) : e = new String(a(this).text()); var f = a.parseNumber(e, b); if (f) { c && (a(this).is(":input") ? a(this).val(f.toString()) : a(this).text(f.toString())); if (d) return f } }, a.parseNumber = function(b, c) { var c = a.extend({}, a.fn.parseNumber.defaults, c), d = k(c.locale.toLowerCase(), c.isFullLocale), e = d.dec, f = d.group, g = d.neg, h = "1234567890.-"; while (b.indexOf(f) > -1) b = b.replace(f, ""); b = b.replace(e, ".").replace(g, "-"); var i = "", j = !1; if (b.charAt(b.length - 1) == "%" || c.isPercentage == 1) j = !0; for (var l = 0; l < b.length; l++) h.indexOf(b.charAt(l)) > -1 && (i += b.charAt(l)); var m = new Number(i); if (j) { m /= 100; var n = i.indexOf("."); if (n != -1) { var o = i.length - n - 1; m = m.toFixed(o + 2) } else m = m.toFixed(i.length - 1) } return m }, a.fn.parseNumber.defaults = { locale: "us", decimalSeparatorAlwaysShown: !1, isPercentage: !1, isFullLocale: !1 }, a.fn.formatNumber.defaults = { format: "#,###.00", locale: "us", decimalSeparatorAlwaysShown: !1, nanForceZero: !0, round: !0, isFullLocale: !1 }, Number.prototype.toFixed = function(b) { return a._roundNumber(this, b) }, a._roundNumber = function(a, b) { var c = Math.pow(10, b || 0), d = String(Math.round(a * c) / c); if (b > 0) { var e = d.indexOf("."); e == -1 ? (d += ".", e = 0) : e = d.length - (e + 1); while (e < b) d += "0", e++ } return d } })(jQuery), function() { var a = {}; this.tmpl = function b(c, d) { var e = /\W/.test(c) ? new Function("obj", "var p=[],print=function(){p.push.apply(p,arguments);};with(obj){p.push('" + c.replace(/[\r\t\n]/g, " ").split("<%").join("\t").replace(/((^|%>)[^\t]*)'/g, "$1\r").replace(/\t=(.*?)%>/g, "',$1,'").split("\t").join("');").split("%>").join("p.push('").split("\r").join("\\'") + "');}return p.join('');") : a[c] = a[c] || b(document.getElementById(c).innerHTML); return d ? e(d) : e } } (), function(a) { a.baseClass = function(b) { return b = a(b), b.get(0).className.match(/([^ ]+)/)[1] }, a.fn.addDependClass = function(b, c) { var d = { delimiter: c ? c : "-" }; return this.each(function() { var c = a.baseClass(this); c && a(this).addClass(c + d.delimiter + b) }) }, a.fn.removeDependClass = function(b, c) { var d = { delimiter: c ? c : "-" }; return this.each(function() { var c = a.baseClass(this); c && a(this).removeClass(c + d.delimiter + b) }) }, a.fn.toggleDependClass = function(b, c) { var d = { delimiter: c ? c : "-" }; return this.each(function() { var c = a.baseClass(this); c && (a(this).is("." + c + d.delimiter + b) ? a(this).removeClass(c + d.delimiter + b) : a(this).addClass(c + d.delimiter + b)) }) } } (jQuery), function(a) { function b() { this._init.apply(this, arguments) } b.prototype.oninit = function() { }, b.prototype.events = function() { }, b.prototype.onmousedown = function() { this.ptr.css({ position: "absolute" }) }, b.prototype.onmousemove = function(a, b, c) { this.ptr.css({ left: b, top: c }) }, b.prototype.onmouseup = function() { }, b.prototype.isDefault = { drag: !1, clicked: !1, toclick: !0, mouseup: !1 }, b.prototype._init = function() { if (arguments.length > 0) { this.ptr = a(arguments[0]), this.outer = a(".draggable-outer"), this.is = {}, a.extend(this.is, this.isDefault); var b = this.ptr.offset(); this.d = { left: b.left, top: b.top, width: this.ptr.width(), height: this.ptr.height() }, this.oninit.apply(this, arguments), this._events() } }, b.prototype._getPageCoords = function(a) { return a.targetTouches && a.targetTouches[0] ? { x: a.targetTouches[0].pageX, y: a.targetTouches[0].pageY} : { x: a.pageX, y: a.pageY} }, b.prototype._bindEvent = function(a, b, c) { var d = this; this.supportTouches_ ? a.get(0).addEventListener(this.events_[b], c, !1) : a.bind(this.events_[b], c) }, b.prototype._events = function() { var b = this; this.supportTouches_ = a.browser.webkit && navigator.userAgent.indexOf("Mobile") != -1, this.events_ = { click: this.supportTouches_ ? "touchstart" : "click", down: this.supportTouches_ ? "touchstart" : "mousedown", move: this.supportTouches_ ? "touchmove" : "mousemove", up: this.supportTouches_ ? "touchend" : "mouseup" }, this._bindEvent(a(document), "move", function(a) { b.is.drag && (a.stopPropagation(), a.preventDefault(), b._mousemove(a)) }), this._bindEvent(a(document), "down", function(a) { b.is.drag && (a.stopPropagation(), a.preventDefault()) }), this._bindEvent(a(document), "up", function(a) { b._mouseup(a) }), this._bindEvent(this.ptr, "down", function(a) { return b._mousedown(a), !1 }), this._bindEvent(this.ptr, "up", function(a) { b._mouseup(a) }), this.ptr.find("a").click(function() { b.is.clicked = !0; if (!b.is.toclick) return b.is.toclick = !0, !1 }).mousedown(function(a) { return b._mousedown(a), !1 }), this.events() }, b.prototype._mousedown = function(b) { this.is.drag = !0, this.is.clicked = !1, this.is.mouseup = !1; var c = this.ptr.offset(), d = this._getPageCoords(b); this.cx = d.x - c.left, this.cy = d.y - c.top, a.extend(this.d, { left: c.left, top: c.top, width: this.ptr.width(), height: this.ptr.height() }), this.outer && this.outer.get(0) && this.outer.css({ height: Math.max(this.outer.height(), a(document.body).height()), overflow: "hidden" }), this.onmousedown(b) }, b.prototype._mousemove = function(a) { this.is.toclick = !1; var b = this._getPageCoords(a); this.onmousemove(a, b.x - this.cx, b.y - this.cy) }, b.prototype._mouseup = function(b) { var c = this; this.is.drag && (this.is.drag = !1, this.outer && this.outer.get(0) && (a.browser.mozilla ? this.outer.css({ overflow: "hidden" }) : this.outer.css({ overflow: "visible" }), a.browser.msie && a.browser.version == "6.0" ? this.outer.css({ height: "100%" }) : this.outer.css({ height: "auto" })), this.onmouseup(b)) }, window.Draggable = b } (jQuery), function(a) { function b(a) { return typeof a == "undefined" ? !1 : a instanceof Array || !(a instanceof Object) && Object.prototype.toString.call(a) == "[object Array]" || typeof a.length == "number" && typeof a.splice != "undefined" && typeof a.propertyIsEnumerable != "undefined" && !a.propertyIsEnumerable("splice") ? !0 : !1 } function d() { return this.init.apply(this, arguments) } function e() { Draggable.apply(this, arguments) } a.slider = function(b, c) { var e = a(b); return e.data("jslider") || e.data("jslider", new d(b, c)), e.data("jslider") }, a.fn.slider = function(c, d) { function g(a) { return a !== undefined } function h(a) { return a != null } var e, f = arguments; return this.each(function() { var i = a.slider(this, c); if (typeof c == "string") switch (c) { case "value": if (g(f[1]) && g(f[2])) { var j = i.getPointers(); h(j[0]) && h(f[1]) && (j[0].set(f[1]), j[0].setIndexOver()), h(j[1]) && h(f[2]) && (j[1].set(f[2]), j[1].setIndexOver()) } else if (g(f[1])) { var j = i.getPointers(); h(j[0]) && h(f[1]) && (j[0].set(f[1]), j[0].setIndexOver()) } else e = i.getValue(); break; case "prc": if (g(f[1]) && g(f[2])) { var j = i.getPointers(); h(j[0]) && h(f[1]) && (j[0]._set(f[1]), j[0].setIndexOver()), h(j[1]) && h(f[2]) && (j[1]._set(f[2]), j[1].setIndexOver()) } else if (g(f[1])) { var j = i.getPointers(); h(j[0]) && h(f[1]) && (j[0]._set(f[1]), j[0].setIndexOver()) } else e = i.getPrcValue(); break; case "calculatedValue": var k = i.getValue().split(";"); e = ""; for (var l = 0; l < k.length; l++) e += (l > 0 ? ";" : "") + i.nice(k[l]); break; case "skin": i.setSkin(f[1]) } else !c && !d && (b(e) || (e = []), e.push(i)) }), b(e) && e.length == 1 && (e = e[0]), e || this }; var c = { settings: { from: 1, to: 10, step: 1, smooth: !0, limits: !0, round: 0, format: { format: "#,##0.##" }, value: "5;7", dimension: "" }, className: "jslider", selector: ".jslider-", template: tmpl('<span class="<%=className%>"><table><tr><td><div class="<%=className%>-bg"><i class="l"></i><i class="r"></i><i class="v"></i></div><div class="<%=className%>-pointer"></div><div class="<%=className%>-pointer <%=className%>-pointer-to"></div><div class="<%=className%>-label"><span><%=settings.from%></span></div><div class="<%=className%>-label <%=className%>-label-to"><span><%=settings.to%></span><%=settings.dimension%></div><div class="<%=className%>-value"><span></span><%=settings.dimension%></div><div class="<%=className%>-value <%=className%>-value-to"><span></span><%=settings.dimension%></div><div class="<%=className%>-scale"><%=scale%></div></td></tr></table></span>') }; d.prototype.init = function(b, d) { this.settings = a.extend(!0, {}, c.settings, d ? d : {}), this.inputNode = a(b).hide(), this.settings.interval = this.settings.to - this.settings.from, this.settings.value = this.inputNode.attr("value"), this.settings.calculate && a.isFunction(this.settings.calculate) && (this.nice = this.settings.calculate), this.settings.onstatechange && a.isFunction(this.settings.onstatechange) && (this.onstatechange = this.settings.onstatechange), this.is = { init: !1 }, this.o = {}, this.create() }, d.prototype.onstatechange = function() { }, d.prototype.create = function() { var b = this; this.domNode = a(c.template({ className: c.className, settings: { from: this.nice(this.settings.from), to: this.nice(this.settings.to), dimension: this.settings.dimension }, scale: this.generateScale() })), this.inputNode.after(this.domNode), this.drawScale(), this.settings.skin && this.settings.skin.length > 0 && this.setSkin(this.settings.skin), this.sizes = { domWidth: this.domNode.width(), domOffset: this.domNode.offset() }, a.extend(this.o, { pointers: {}, labels: { 0: { o: this.domNode.find(c.selector + "value").not(c.selector + "value-to") }, 1: { o: this.domNode.find(c.selector + "value").filter(c.selector + "value-to")} }, limits: { 0: this.domNode.find(c.selector + "label").not(c.selector + "label-to"), 1: this.domNode.find(c.selector + "label").filter(c.selector + "label-to")} }), a.extend(this.o.labels[0], { value: this.o.labels[0].o.find("span") }), a.extend(this.o.labels[1], { value: this.o.labels[1].o.find("span") }), b.settings.value.split(";")[1] || (this.settings.single = !0, this.domNode.addDependClass("single")), b.settings.limits || this.domNode.addDependClass("limitless"), this.domNode.find(c.selector + "pointer").each(function(a) { var c = b.settings.value.split(";")[a]; if (c) { b.o.pointers[a] = new e(this, a, b); var d = b.settings.value.split(";")[a - 1]; d && new Number(c) < new Number(d) && (c = d), c = c < b.settings.from ? b.settings.from : c, c = c > b.settings.to ? b.settings.to : c, b.o.pointers[a].set(c, !0) } }), this.o.value = this.domNode.find(".v"), this.is.init = !0, a.each(this.o.pointers, function(a) { b.redraw(this) }), function(b) { a(window).resize(function() { b.onresize() }) } (this) }, d.prototype.setSkin = function(a) { this.skin_ && this.domNode.removeDependClass(this.skin_, "_"), this.domNode.addDependClass(this.skin_ = a, "_") }, d.prototype.setPointersIndex = function(b) { a.each(this.getPointers(), function(a) { this.index(a) }) }, d.prototype.getPointers = function() { return this.o.pointers }, d.prototype.generateScale = function() { if (this.settings.scale && this.settings.scale.length > 0) { var a = "", b = this.settings.scale, c = Math.round(100 / (b.length - 1) * 10) / 10; for (var d = 0; d < b.length; d++) a += '<span style="left: ' + d * c + '%">' + (b[d] != "|" ? "<ins>" + b[d] + "</ins>" : "") + "</span>"; return a } return "" }, d.prototype.drawScale = function() { this.domNode.find(c.selector + "scale span ins").each(function() { a(this).css({ marginLeft: -a(this).outerWidth() / 2 }) }) }, d.prototype.onresize = function() { var b = this; this.sizes = { domWidth: this.domNode.width(), domOffset: this.domNode.offset() }, a.each(this.o.pointers, function(a) { b.redraw(this) }) }, d.prototype.update = function() { this.onresize(), this.drawScale() }, d.prototype.limits = function(a, b) { if (!this.settings.smooth) { var c = this.settings.step * 100 / this.settings.interval; a = Math.round(a / c) * c } var d = this.o.pointers[1 - b.uid]; return d && b.uid && a < d.value.prc && (a = d.value.prc), d && !b.uid && a > d.value.prc && (a = d.value.prc), a < 0 && (a = 0), a > 100 && (a = 100), Math.round(a * 10) / 10 }, d.prototype.redraw = function(a) { if (!this.is.init) return !1; this.setValue(), this.o.pointers[0] && this.o.pointers[1] && this.o.value.css({ left: this.o.pointers[0].value.prc + "%", width: this.o.pointers[1].value.prc - this.o.pointers[0].value.prc + "%" }), this.o.labels[a.uid].value.html(this.nice(a.value.origin)), this.redrawLabels(a) }, d.prototype.redrawLabels = function(a) { function b(a, b, d) { return b.margin = -b.label / 2, label_left = b.border + b.margin, label_left < 0 && (b.margin -= label_left), b.border + b.label / 2 > c.sizes.domWidth ? (b.margin = 0, b.right = !0) : b.right = !1, a.o.css({ left: d + "%", marginLeft: b.margin, right: "auto" }), b.right && a.o.css({ left: "auto", right: 0 }), b } var c = this, d = this.o.labels[a.uid], e = a.value.prc, f = { label: d.o.outerWidth(), right: !1, border: e * this.sizes.domWidth / 100 }; if (!this.settings.single) { var g = this.o.pointers[1 - a.uid], h = this.o.labels[g.uid]; switch (a.uid) { case 0: f.border + f.label / 2 > h.o.offset().left - this.sizes.domOffset.left ? (h.o.css({ visibility: "hidden" }), h.value.html(this.nice(g.value.origin)), d.o.css({ visibility: "visible" }), e = (g.value.prc - e) / 2 + e, g.value.prc != a.value.prc && (d.value.html(this.nice(a.value.origin) + "&nbsp;&ndash;&nbsp;" + this.nice(g.value.origin)), f.label = d.o.outerWidth(), f.border = e * this.sizes.domWidth / 100)) : h.o.css({ visibility: "visible" }); break; case 1: f.border - f.label / 2 < h.o.offset().left - this.sizes.domOffset.left + h.o.outerWidth() ? (h.o.css({ visibility: "hidden" }), h.value.html(this.nice(g.value.origin)), d.o.css({ visibility: "visible" }), e = (e - g.value.prc) / 2 + g.value.prc, g.value.prc != a.value.prc && (d.value.html(this.nice(g.value.origin) + "&nbsp;&ndash;&nbsp;" + this.nice(a.value.origin)), f.label = d.o.outerWidth(), f.border = e * this.sizes.domWidth / 100)) : h.o.css({ visibility: "visible" }) } } f = b(d, f, e); if (h) { var f = { label: h.o.outerWidth(), right: !1, border: g.value.prc * this.sizes.domWidth / 100 }; f = b(h, f, g.value.prc) } this.redrawLimits() }, d.prototype.redrawLimits = function() { if (this.settings.limits) { var a = [!0, !0]; for (key in this.o.pointers) if (!this.settings.single || key == 0) { var b = this.o.pointers[key], c = this.o.labels[b.uid], d = c.o.offset().left - this.sizes.domOffset.left, e = this.o.limits[0]; d < e.outerWidth() && (a[0] = !1); var e = this.o.limits[1]; d + c.o.outerWidth() > this.sizes.domWidth - e.outerWidth() && (a[1] = !1) } for (var f = 0; f < a.length; f++) a[f] ? this.o.limits[f].fadeIn("fast") : this.o.limits[f].fadeOut("fast") } }, d.prototype.setValue = function() { var a = this.getValue(); this.inputNode.attr("value", a), this.onstatechange.call(this, a) }, d.prototype.getValue = function() { if (!this.is.init) return !1; var b = this, c = ""; return a.each(this.o.pointers, function(a) { this.value.prc != undefined && !isNaN(this.value.prc) && (c += (a > 0 ? ";" : "") + b.prcToValue(this.value.prc)) }), c }, d.prototype.getPrcValue = function() { if (!this.is.init) return !1; var b = this, c = ""; return a.each(this.o.pointers, function(a) { this.value.prc != undefined && !isNaN(this.value.prc) && (c += (a > 0 ? ";" : "") + this.value.prc) }), c }, d.prototype.prcToValue = function(a) { if (this.settings.heterogeneity && this.settings.heterogeneity.length > 0) { var b = this.settings.heterogeneity, c = 0, d = this.settings.from; for (var e = 0; e <= b.length; e++) { if (b[e]) var f = b[e].split("/"); else var f = [100, this.settings.to]; f[0] = new Number(f[0]), f[1] = new Number(f[1]); if (a >= c && a <= f[0]) var g = d + (a - c) * (f[1] - d) / (f[0] - c); c = f[0], d = f[1] } } else var g = this.settings.from + a * this.settings.interval / 100; return this.round(g) }, d.prototype.valueToPrc = function(a, b) { if (this.settings.heterogeneity && this.settings.heterogeneity.length > 0) { var c = this.settings.heterogeneity, d = 0, e = this.settings.from; for (var f = 0; f <= c.length; f++) { if (c[f]) var g = c[f].split("/"); else var g = [100, this.settings.to]; g[0] = new Number(g[0]), g[1] = new Number(g[1]); if (a >= e && a <= g[1]) var h = b.limits(d + (a - e) * (g[0] - d) / (g[1] - e)); d = g[0], e = g[1] } } else var h = b.limits((a - this.settings.from) * 100 / this.settings.interval); return h }, d.prototype.round = function(a) { return a = Math.round(a / this.settings.step) * this.settings.step, this.settings.round ? a = Math.round(a * Math.pow(10, this.settings.round)) / Math.pow(10, this.settings.round) : a = Math.round(a), a }, d.prototype.nice = function(b) { return b = b.toString().replace(/,/gi, ".").replace(/ /gi, ""), a.formatNumber ? a.formatNumber(new Number(b), this.settings.format || {}).replace(/-/gi, "&minus;") : new Number(b) }, e.prototype = new Draggable, e.prototype.oninit = function(a, b, c) { this.uid = b, this.parent = c, this.value = {}, this.settings = this.parent.settings }, e.prototype.onmousedown = function(a) { this._parent = { offset: this.parent.domNode.offset(), width: this.parent.domNode.width() }, this.ptr.addDependClass("hover"), this.setIndexOver() }, e.prototype.onmousemove = function(a, b) { var c = this._getPageCoords(a); this._set(this.calc(c.x)) }, e.prototype.onmouseup = function(b) { this.parent.settings.callback && a.isFunction(this.parent.settings.callback) && this.parent.settings.callback.call(this.parent, this.parent.getValue()), this.ptr.removeDependClass("hover") }, e.prototype.setIndexOver = function() { this.parent.setPointersIndex(1), this.index(2) }, e.prototype.index = function(a) { this.ptr.css({ zIndex: a }) }, e.prototype.limits = function(a) { return this.parent.limits(a, this) }, e.prototype.calc = function(a) { var b = this.limits((a - this._parent.offset.left) * 100 / this._parent.width); return b }, e.prototype.set = function(a, b) { this.value.origin = this.parent.round(a), this._set(this.parent.valueToPrc(a, this), b) }, e.prototype._set = function(a, b) { b || (this.value.origin = this.parent.prcToValue(a)), this.value.prc = a, this.ptr.css({ left: a + "%" }), this.parent.redraw(this) } } (jQuery);