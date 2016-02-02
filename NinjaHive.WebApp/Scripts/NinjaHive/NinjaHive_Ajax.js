// namespace ninjaHive
var ninjaHive = ninjaHive || {};


(function (NH)
{

    function Ajax(requestType, uri, async)
    {
        var self = this;
        if (window.XMLHttpRequest != undefined)
            this.httpRequest = new XMLHttpRequest();
        else
            this.httpRequest = new ActiveXObject("Microsoft.XMLHTTP");


        this.open = function (requestType, uri, async)
        {
            this.httpRequest.open(requestType, uri, async);
            this.httpRequest.onreadystatechange = _updateEvent;
        }

        this.send = function (data)
        {
            if (data == undefined)
            {
                return this.httpRequest.send();
            }
            else if (typeof data == "string")
            {
                return this.httpRequest.send(data);
            }

            var request = "";
            for (var i = 0; i < arguments.length; ++i)
            {
                data = arguments[i];
                if (typeof data == "object" && !(data instanceof Array))
                {
                    request += _objectToKeyValueString(data);
                }
                else
                {
                    throw new TypeError("Incompatible ajax send data type " + (typeof data) + " for argument " + i);
                }
            }
            console.log("Sending "+request);
            return this.send(request);
        }
        this.setRequestHeader = function(headerType, data)
        {
            this.httpRequest.setRequestHeader(headerType, data);
        }
        this.setCallbacks = function (callbacksObject)
        {
            if (callbacksObject.onSuccess)
                this.onSuccess = callbacksObject.onSuccess;
            if (callbacksObject.onError)
                this.onError = callbacksObject.onError;
            if (callbacksObject.onReadyStateChange)
                this.onReadyStateChange = callbacksObject.onReadyStateChange;
        }

        function _updateEvent(evt)
        {
            if (self.onReadyStateChange)
            {
                self.onReadyStateChange(evt);
            }

            if (self.httpRequest.readyState == 4)
            {
                if (self.httpRequest.status == 200)
                {
                    if (self.onSuccess)
                    {
                        self.onSuccess(evt, self.httpRequest);
                    }
                }
                else
                {
                    console.log("Error!");
                    if (self.onError)
                    {
                        console.log("Submitting error!");
                        self.onError(evt, self.httpRequest);
                    }
                }
            }
        }

        function _objectToKeyValueString(obj)
        {
            var output = "";
            for (var key in obj)
            {
                if (obj.hasOwnProperty(key))
                {
                    if (typeof obj[key] != "function")
                    {
                        output += key + "=" + obj[key] + "&";
                    }
                }
            }
            return output.substring(0, output.length - 1);
        }

        // Constructor
        (function ()
        {
            self.open(requestType, uri, async);
        })();
    }

    
    NH.createHttpRequest = function(requestType, uri, async)
    {
        return new Ajax(requestType, uri, (async != undefined ? async : true));
    }
})(ninjaHive);