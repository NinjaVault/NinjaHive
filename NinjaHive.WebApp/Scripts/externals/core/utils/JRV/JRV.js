// --------------------------------------------------------------------------------
// Copyright (c) 2015 Ruggero Enrico Visintin
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE. 
// --------------------------------------------------------------------------------

console.log("JRV.js included");

var JRV = {
    includedLibs: [],
    basePath: "",
};

JRV.setBasePath = function (basePath) {
    this.basePath = basePath;

    console.log(this.basePath);
};

JRV.include = function (path) {
    for (var i = 0; i < this.includedLibs.length; i++) {
        if (this.basePath + path === this.includedLibs[i]) {
            return;
        }
   }

   var included = false;

   var js = document.createElement("script");
   js.type = "text/javascript";

   js.onload = function () {
       included = true;
       return;
   };

   js.src = this.basePath + path;
   document.getElementsByTagName("head")[0].appendChild(js);
   this.includedLibs.push(this.basePath + path);
};

JRV.isMobile = {
    Android: function () {
        return (navigator.userAgent.match(/Android/i) != null);
    },

    BlackBerry: function () {
        return ((navigator.userAgent.match(/BB10; Touch/i) != null) || (navigator.userAgent.match(/BlackBerry/i) != null)) || (navigator.userAgent.match(/PlayBook/i) != null);
    },

    iOS: function () {
        return (navigator.userAgent.match(/iPhone|iPad|iPod/i) != null);
    },


    Linux: {
        Opera: function () {
            return ((navigator.userAgent.match(/Opera Mini/i) != null) || (navigator.userAgent.match(/Opera Mobi/i) != null) || (navigator.userAgent.match(/MeeGo/i) != null));
        },

        Linux: function() {
            return (navigator.userAgent.match(/Silk/i) != null);
        },

        any: function () {
            return (this.Linux() || this.Opera());
        }
    },

    Windows: function () {
        return (navigator.userAgent.match(/IEMobile/i) != null);
    },

    any: function () {
        return (JRV.isMobile.Android() || JRV.isMobile.BlackBerry() || JRV.isMobile.iOS() || JRV.isMobile.Windows() || JRV.isMobile.Linux.any());
    }
};

JRV.supportTouch = function () {
    return 'ontouchstart' in window;
};

JRV.supportMouse = function () {    
    return 'onmousedown' in window;
};

JRV.waitUntil = function (condition, value, callback) {
    var interval = setInterval(
        function () {
            if (condition == value) {
                clearInterval(interval);
                callback();
                return;
            }
        }, 1);
};

JRV.xmlHttpGetRequest = function (path, sync, callback) {
    var xmlhttp;

    if (window.XMLHttpRequest) {
        xmlhttp= new XMLHttpRequest();
    } else {
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

    xmlhttp.onreadystatechange=function()
    {
        if (xmlhttp.readyState==4 && xmlhttp.status==200)
        {
            callback(xmlhttp.responseText);
        }
    }

    xmlhttp.open("GET", path, sync);
    xmlhttp.send();
};