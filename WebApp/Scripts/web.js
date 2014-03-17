function _GetScriptLocation() {
    var scriptLocation = "";
    var scriptName = "Utility.js";
    var scripts = document.getElementsByTagName('script');
    for (var i = 0; i < scripts.length; i++) {
        var src = scripts[i].getAttribute('src');
        if (src) {
            var index = src.lastIndexOf(scriptName);
            // is it found, at the end of the URL?
            if ((index > -1) && (index + scriptName.length == src.length)) {
                scriptLocation = src.slice(0, -scriptName.length);
                break;
            }
        }
    }
    return scriptLocation;
}

var _scriptLocation = _GetScriptLocation();
var _basepath = _scriptLocation.replace("JavaScript/", "");
var _versionNO = '';
function _IncludeScript(inc, baseLocation) {
    if (!baseLocation) {
        baseLocation = _scriptLocation;
    }
    var script = '<' + 'script type="text/javascript" src="' + baseLocation
            + inc + _versionNO + '"' + '><' + '/script>';
    document.writeln(script);
}
function _IncludeStyle(inc, baseLocation) {
    if (!baseLocation) {
        baseLocation = _scriptLocation + "../styles/";
    }
    var style = '<' + 'link type="text/css" rel="stylesheet" href="'
            + baseLocation + inc + _versionNO + '"' + ' />';
    document.writeln(style);
}
function _GetBrowser() {
    var ua = navigator.userAgent.toLowerCase();
    if (ua.indexOf('opera') != -1)
        return 'opera';
    else if (ua.indexOf('msie') != -1)
        return 'ie';
    else if (ua.indexOf('safari') != -1)
        return 'safari';
    else if (ua.indexOf('gecko') != -1)
        return 'gecko';
    else
        return false;
}

function ResolveUrl(path) {
    if (path) {
        path = path.replace("\\", "/").replace("//", "/");
        if (path.indexOf(_basepath) == 0)
            return path;
        else if (path.indexOf(_basepath.substring(1, _basepath.length)) == 0)
            return "/" + path;
        else if (path == "/")
            return _basepath;
        else
            return (_basepath + path).replace("//", "/");
    } else
        return _basepath;
}
function resolveUrl(path) { return ResolveUrl(path); }
function resolveurl(path) { return ResolveUrl(path); }

/*
使用时本脚本需要从后台注册 
ClientScript.RegisterClientScriptInclude(this.GetType(), "Include", ResolveUrl("~/js/Include.js"));
一般这样的方法放在pagebase初始化或load方法中            
//获取相对于虚拟目录的url，如果是网站则返回相对于根目录的url， 如果path是空或者“/”返回虚拟目录名称
ResolveUrl("");
*/