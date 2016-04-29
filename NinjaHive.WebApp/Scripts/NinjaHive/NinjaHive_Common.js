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
        NH.addEventListener = function (obj, evt, func, useCapture)
        {
            useCapture = useCapture === undefined ? false : useCapture;
            if (evt.toLowerCase() == "domcontentloaded") {
                // If the DOM is already loaded, go ahead and call this
                if (document.readyState == "interactive") {
                    func();
                    return;
                }
            }
            obj.addEventListener(evt, func, useCapture);
        };
        NH.removeEventListener = function(obj, evt, func, useCapture)
        {
            useCapture = useCapture === undefined ? false : useCapture;
            obj.removeEventListener(evt, func, useCapture);
        }
        NH.setAttribute = function (obj, attribute, value) {
            obj.setAttribute(attribute, value);
        };
        NH.getAttribute = function (obj, attribute) {
            return obj.getAttribute(attribute);
        };
        NH.removeAttribute = function (obj, attribute)
        {
            return obj.removeAttribute(attribute);
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
        NH.removeEventListener = function(obj, evt, func)
        {
            if (evt.toLowerCase() == "domcontentloaded")
                obj.detachEvent("onreadystatechange", func);
            else
                obj.detachEvent("on" + evt, func);
        }
        NH.setAttribute = function (obj, attribute, value) {
            // Take advantage of how all objects in JS are maps
            obj[attribute] = value;
        };
        NH.getAttribute = function (obj, attribute) {
            return obj[attribute];
        };
        NH.removeAttribute = function (obj, attribute)
        {
            obj[attribute] = "";
            delete obj[attribute];
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

    NH.moveChildren = function(destNode, sourceNode, checkEachFunc)
    {
        NH.clearChildren(destNode);
        checkEachFunc = checkEachFunc || function () { };

        while (sourceNode.hasChildNodes())
        {
            var node = sourceNode.firstChild;

            if (!checkEachFunc(node) !== false)
                destNode.appendChild(node);
        }
    }
    NH.removeFromParent = function (element)
    {
        if (element.parentNode)
        {
            element.parentNode.removeChild(element);
            return true;
        }
        return false;
    }
    NH.containsNode = function(containerElem, checkElem)
    {
        var elem = checkElem.parentNode;
        while(elem != containerElem && elem != null)
        {
            elem = elem.parentNode;
        }
        return elem == containerElem;
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
    
    /*
     sendVerifiableForm(form, requestUrl)
     sendVerifiableForm(form, requestUrl, callbackOptions)
     sendVerifiableForm(form, additionalData, requestUrl, callbackOptions)
     */
    NH.sendVerifiableForm = function (form)
    {
        var callbackOptions = {};
        var customData = {};
        var requestUrl = arguments[1];

        if (arguments.length >= 3)
        {
            if (arguments.length >= 4)
            {
                customData = arguments[1];
                requestUrl = arguments[2];
                callbackOptions = arguments[3];
            }
            else
            {
                callbackOptions = arguments[2];
            }
        }

        var data = customData;

        // compile the form into the custom data
        var fields = form.elements;
        for (var i = 0; i < fields.length; ++i)
        {
            var name = NH.getAttribute(fields[i], "name");
            if (name && name != "")
            {
                data[name] = fields[i].value;
            }
        }


        var ajax = NH.createHttpRequest("POST", requestUrl, true);
        ajax.setCallbacks(callbackOptions);
        ajax.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        ajax.send(data);
        return ajax;
    }

    function deleteFormConfirm(evt)
    {
        NH.removeEventListener(this.target, "submit", deleteFormSubmit);

        NH.setAttribute(this.target, "action", NH.getAttribute(this.target, "data-action"));

        this.target.submit();

    }
    function deleteFormSubmit(evt)
    {
        evt.preventDefault();
        NH.modal.deleteConfirmDialog()
                    .on("submit", deleteFormConfirm)
                    .target = this;
        return false;
    }
    NH.promptBeforeDelete = function()
    {
        var deletes = document.getElementsByClassName("form-delete");

        for(var i=0;i<deletes.length;++i)
        {
            var form = deletes[i];
            NH.setAttribute(form, "data-action", NH.getAttribute(form, "action"));
            NH.setAttribute(form, "action", "");
            NH.addEventListener(form, "submit", deleteFormSubmit);
        }
    }
})(ninjaHive);