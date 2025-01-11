require({cache:{"dojo/io/script":function(){define("../_base/connect ../_base/kernel ../_base/lang ../sniff ../_base/window ../_base/xhr ../dom ../dom-construct ../request/script ../aspect".split(" "),function(u,k,l,v,z,e,r,w,m,y){k.deprecated("dojo/io/script","Use dojo/request/script.","2.0");var f={get:function(a){var c,d=this._makeScriptDeferred(a,function(a){c&&c.cancel()}),b=d.ioArgs;e._ioAddQueryToUrl(b);e._ioNotifyStart(d);c=m.get(b.url,{timeout:a.timeout,jsonp:b.jsonp,checkString:a.checkString,
ioArgs:b,frameDoc:a.frameDoc,canAttach:function(a){b.requestId=a.id;b.scriptId=a.scriptId;b.canDelete=a.canDelete;return f._canAttach(b)}},!0);y.around(c,"isValid",function(a){return function(b){f._validCheck(d);return a.call(this,b)}});c.then(function(){d.resolve(d)}).otherwise(function(a){d.ioArgs.error=a;d.reject(a)});return d},attach:m._attach,remove:m._remove,_makeScriptDeferred:function(a,c){var d=e._ioSetArgs(a,c||this._deferredCancel,this._deferredOk,this._deferredError),b=d.ioArgs;b.id=k._scopeName+
"IoScript"+this._counter++;b.canDelete=!1;b.jsonp=a.callbackParamName||a.jsonp;b.jsonp&&(b.query=b.query||"",0<b.query.length&&(b.query+="\x26"),b.query+=b.jsonp+"\x3d"+(a.frameDoc?"parent.":"")+k._scopeName+".io.script.jsonp_"+b.id+"._jsonpCallback",b.frameDoc=a.frameDoc,b.canDelete=!0,d._jsonpCallback=this._jsonpCallback,this["jsonp_"+b.id]=d);d.addBoth(function(a){b.canDelete&&(a instanceof Error?f["jsonp_"+b.id]._jsonpCallback=function(){delete f["jsonp_"+b.id];if(b.requestId)k.global[m._callbacksProperty][b.requestId]()}:
f._addDeadScript(b))});return d},_deferredCancel:function(a){a.canceled=!0},_deferredOk:function(a){a=a.ioArgs;return a.json||a.scriptLoaded||a},_deferredError:function(a,c){return a},_deadScripts:[],_counter:1,_addDeadScript:function(a){f._deadScripts.push({id:a.id,frameDoc:a.frameDoc});a.frameDoc=null},_validCheck:function(a){if((a=f._deadScripts)&&0<a.length){for(var c=0;c<a.length;c++)f.remove(a[c].id,a[c].frameDoc),delete f["jsonp_"+a[c].id],a[c].frameDoc=null;f._deadScripts=[]}return!0},_ioCheck:function(a){a=
a.ioArgs;return a.json||a.scriptLoaded&&!a.args.checkString?!0:(a=a.args.checkString)&&eval("typeof("+a+") !\x3d 'undefined'")},_resHandle:function(a){f._ioCheck(a)?a.callback(a):a.errback(Error("inconceivable dojo.io.script._resHandle error"))},_canAttach:function(){return!0},_jsonpCallback:function(a){this.ioArgs.json=a;if(this.ioArgs.requestId)k.global[m._callbacksProperty][this.ioArgs.requestId](a)}};l.setObject("dojo.io.script",f);return f})},"dojo/request/script":function(){define("module ./watch ./util ../_base/array ../_base/lang ../on ../dom ../dom-construct ../has ../_base/window".split(" "),
function(u,k,l,v,z,e,r,w,m,y){function f(a,b){a.canDelete&&n._remove(a.id,b.options.frameDoc,!0)}function a(a){s&&s.length&&(v.forEach(s,function(a){n._remove(a.id,a.frameDoc);a.frameDoc=null}),s=[]);return a.options.jsonp?!a.data:!0}function c(a){return!!this.scriptLoaded}function d(a){return(a=a.options.checkString)&&eval("typeof("+a+') !\x3d\x3d "undefined"')}function b(a,b){if(this.canDelete){var c=this.response.options;s.push({id:this.id,frameDoc:c.ioArgs?c.ioArgs.frameDoc:c.frameDoc});c.ioArgs&&
(c.ioArgs.frameDoc=null);c.frameDoc=null}b?this.reject(b):this.resolve(a)}function n(p,g,m){var q=l.parseArgs(p,l.deepCopy({},g));p=q.url;g=q.options;var h=l.deferred(q,f,a,g.jsonp?null:g.checkString?d:c,b);z.mixin(h,{id:x+A++,canDelete:!1});g.jsonp&&(RegExp("[?\x26]"+g.jsonp+"\x3d").test(p)||(p+=(~p.indexOf("?")?"\x26":"?")+g.jsonp+"\x3d"+(g.frameDoc?"parent.":"")+x+"_callbacks."+h.id),h.canDelete=!0,t[h.id]=function(a){q.data=a;h.handleResponse(q)});l.notify&&l.notify.emit("send",q,h.promise.cancel);
if(!g.canAttach||g.canAttach(h)){var r=n._attach(h.id,p,g.frameDoc);if(!g.jsonp&&!g.checkString)var w=e(r,B,function(a){if("load"===a.type||C.test(r.readyState))w.remove(),h.scriptLoaded=a})}k(h);return m?h:h.promise}m.add("script-readystatechange",function(a,b){return"undefined"!==typeof b.createElement("script").onreadystatechange&&("undefined"===typeof a.opera||"[object Opera]"!==a.opera.toString())});var x=u.id.replace(/[\/\.\-]/g,"_"),A=0,B=m("script-readystatechange")?"readystatechange":"load",
C=/complete|loaded/,t=this[x+"_callbacks"]={},s=[];n.get=n;n._attach=function(a,b,c){c=c||y.doc;var d=c.createElement("script");d.type="text/javascript";d.src=b;d.id=a;d.async=!0;d.charset="utf-8";return c.getElementsByTagName("head")[0].appendChild(d)};n._remove=function(a,b,c){w.destroy(r.byId(a,b));t[a]&&(c?t[a]=function(){delete t[a]}:delete t[a])};n._callbacksProperty=x+"_callbacks";return n})}}});
define("app/nav/Baidu",["dojo/_base/declare","dojo/_base/lang","app/util/_WidgetBase","dojo/io/script"],function(u,k,l,v){return u("app/nav/Baidu",[l],{"class":"w1000",templateString:'\x3cdiv\x3e\x3ciframe data-dojo-attach-point\x3d"frameNode" width\x3d"100%" height\x3d"1200" frameborder\x3d"none"  border\x3d"0" style\x3d"border: none;"\x3e\x3c/iframe\x3e\x3c/div\x3e',init:function(){var l=this.frameNode,e=App.user,k="company\x3d"+(e.orgFullName||"")+"\x26cspid\x3d"+e.userLongId+"\x26mail\x3d"+e.email+
"\x26name\x3d"+e.name+"\x26source\x3dcustomer\x26telephone\x3d"+(e.mobile||e.officePhone);v.get({url:"http://www.chanjet.com/chanjet/customer/web/getMd5?str\x3d"+encodeURIComponent(k),callbackParamName:"callback",load:function(e){l.src="http://shichang.sinan.baidu.com/web/chanjet/market?"+encodeURIComponent(k+"\x26sign\x3d"+e.sign)},error:function(e){}})}})});
