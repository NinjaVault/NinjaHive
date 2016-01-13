var ninjaHive = ninjaHive || {};
(function (NH) {
    /*==================================
    *	Cross-browser code
    *---------------------------------------------------------
    *		One-time setup cross browser code
    */
    NH.addEventListener;
    NH.setAttribute;
    NH.getAttribute;

    // For speed gains, we pre-determine these functions
    if (window.addEventListener) {
        // Suports common addEventListener
        NH.addEventListener = function (obj, evt, func) {
            if (evt.toLowerCase() == "domcontentloaded") {
                // If the DOM is already loaded, go ahead and call this
                if (document.readyState == "interactive") {
                    func();
                    return;
                }
            }
            obj.addEventListener(evt, func);
        };
        NH.setAttribute = function (obj, attribute, value) {
            obj.setAttribute(attribute, value);
        };
        NH.getAttribute = function (obj, attribute) {
            return obj.getAttribute(attribute);
        };
    }
    else {
        // Requires attachEvent, also probably doesn't support DOMContentLoaded
        NH.addEventListener = function (obj, evt, func) {
            if (evt.toLowerCase() == "domcontentloaded") {
                // If the DOM is already loaded, go ahead and call this
                if (document.readyState == "interactive") {
                    func();
                    return;
                }

                obj.attachEvent("onreadystatechange", function (event) {
                    if (document.readyState == "interactive") func(event);
                });
            }
            else
                obj.attachEvent("on" + evt, func);
        };
        NH.setAttribute = function (obj, attribute, value) {
            // Take advantage of how all objects in JS are maps
            obj[attribute] = value;
        };
        NH.getAttribute = function (obj, attribute) {
            return obj[attribute];
        };
    }

    NH.setAttributes = function (obj, data) {
        if (typeof data == "string") {
            data = data.split(",");
        }

        if (typeof data == "object") {
            for (var i = 0; i < data.length; ++i) {
                var pair = data[i].split(":");
                NH.setAttribute(obj, pair[0].trim(), pair[1].trim());
            }
        }
        else
            throw new TypeError("setAttributes: Invalid data provided.");
    }
    NH.clearChildren = function (node) {
        while (node.firstChild)
            node.removeChild(node.firstChild);
    }
    NH.cloneArray = function(array)
    {
        var newArray = [];

        // Go in reverse order to force the array to its final size, no constant resizing
        for (var i = array.length-1; i >= 0; --i)
            newArray[i] = array[i];

        return newArray;
    }
    NH.mergeArrays = function()
    {
        var pos = 0;
        var newArray = [];
        // We want to go to the end of the new array
        for (var i = 0; i < arguments.length; ++i)
        {
            pos += arguments[i].length;
        }

        for(var i=arguments.length-1;   i >= 0;   --i)
        {
            var array = arguments[i];
            pos -= array.length;

            for(var j = array.length-1; j >= 0; --j)
            {
                newArray[pos + j] = array[j];
            }
        }
        return newArray;
    }

    NH.enforceUniqueValue = function(form, elementName, validationUrl)
    {
        NH.addEventListener(form, "submit", function (evt) {
            var element = form[elementName];

            var ajax = new XMLHttpRequest();
            ajax.open("GET", validationUrl + "?itemName=" + element.value, true);
            ajax.send();
            ajax.onreadystatechange = function ()
            {
                if (ajax.readyState == 4) {
                    var error = element.nextElementSibling;
                    if (ajax.status == 200)
                    {
                        var unique = JSON.parse(ajax.responseText);
                        if (unique)
                        {
                            form.submit();
                        }
                        else
                        {
                            error.innerHTML = "This value already exists.";
                        }
                    }
                    else
                    {
                        error.innerHTML = "An error has occured trying to validate this name. Please try again later.";
                    }
                }
            }

            evt.preventDefault();
            return false;
        });

    }
})(ninjaHive);