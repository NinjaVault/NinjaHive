var ninjaHive = ninjaHive || {};

// Make it a function with a parameter so if we decide to change the namespace later
// It is much easier
(function (NH)
{

    NH.DynamicObjectTemplate = function (template, contAttrib)
    {
    // public

        ///createObjectNode(Object obj)
        ///
        /// Creates HTML Elements based on the template filled by the provided Object
        ///     returns a DOMNode containing the formatted template
        this.createObjectNode = function (obj)
        {
            var newHtml = _templateString;

            for (var i = _templateVariables.length - 1; i >= 0; --i)
            {
                var varMatch = _templateVariables[i];
                newHtml = newHtml.slice(0, varMatch.index) +
                            // second element of the regex match is the variable name, which we extract from the entity
                            obj[varMatch[1]] +
                            // first element of the regex match is the full matched variable code
                            newHtml.slice(varMatch.index + varMatch[0].length, newHtml.length);
            }
            var elemType = _attrib.tag || "div";
            var container = document.createElement(elemType);
            container.innerHTML = newHtml;

            for (var attribute in _attrib)
            {
                if(attribute != "tag" && _attrib.hasOwnProperty(attribute))
                {
                    NH.setAttribute(container,attribute, _attrib[attribute]);
                }
            }

            return container;
        }

        ///populate(DOMNode parent, Object obj)
        ///populate(DOMNode parent, Object[] obj)
        ///populate(DOMNode parent, Object[] obj, Int start)
        ///populate(DOMNode parent, Object[] obj, Int start, Int end)
        ///
        /// Populates an element with instance(s) of the template filled by the provided Object
        this.populate = function(parent, obj, start, end)
        {
            if (typeof obj != "object")
                throw "Populate error: Templates can only be filled through an object.";

            if(obj instanceof Array)
            {
                start = start === undefined ? 0 : start;
                end = end === undefined ? obj.length : end;
                for(var i=start; i<end; ++i)
                {
                    this.populate(parent,obj,start,end);
                }
            }
            else
            {
                parent.appendChild( this.createObjectNode(obj) );
            }
        }

    // private:
        var self = this;
        var _templateString = "";
        var _templateVariables = [];
        var _attrib = contAttrib || {};

        function _stringifyTemplateArray(template)
        {
            if (typeof template == "string")
            {
                _templateString = template;
            }
            else if (typeof template == "object")
            {
                var div = document.createElement("div");

                if (template instanceof Array)
                {
                    for (var i = 0; i < template.length; ++i)
                    {
                        if (template[i].id)
                            NH.removeAttribute(template[i],"id");
                        div.appendChild(template[i]);
                    }
                }
                else if (template instanceof Element)
                {
                    if (template.id)
                        NH.removeAttribute(template, "id");
                    div.appendChild(template);
                }
                else
                    throw "DynamicObjectTemplate template must be a DOMNode, array of DOMNodes, or an HTMLString.";

                _templateString = div.innerHTML;
            }
            else
            {
                throw "DynamicObjectTemplate template must be a DOMNode, array of DOMNodes, or an HTMLString.";
            }

            _templateString = _templateString.trim();
        }

        function _markTemplateStringVariables()
        {
            _templateString = _templateString.replace(/\{\{/g, "&#123;")
																.replace(/\}\}/g, "&#125;");
            var variableRegex = /\{([a-z0-9_-]+)\}/gi;

            var match = null;
            while ((match = variableRegex.exec(_templateString)))
            {
                _templateVariables.push(match);
            }
        }


    // constructor:
        (function ()
        {
            _stringifyTemplateArray(template);
            _markTemplateStringVariables();
        })()
    }


    ///createHtmlTemplate(HTMLString template, [optional]Object containerAttributes)
    ///createHtmlTemplate(DOMNode template, [optional]Object containerAttributes)
    ///createHtmlTemplate(DOMNode[] template, [optional]Object containerAttributes)
    ///
    ///     returns a DynamicObjectTemplate object
    NH.createHtmlTemplate = function (template, containerAttributes)
    {
        return new NH.DynamicObjectTemplate(template, containerAttributes);
    }

})(ninjaHive)