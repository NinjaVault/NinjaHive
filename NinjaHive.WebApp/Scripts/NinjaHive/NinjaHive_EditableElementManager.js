var ninjaHive = ninjaHive || {};

(function (NH)
{
    NH.EditableElementManager = function(displayTemplate, editTemplate, dataSource)
    {
    // private:
        var self = this;

        // Create a template from the data provided if it already isn't one
        var _display = (displayTemplate instanceof NH.DynamicObjectTemplate) ?
                        displayTemplate :
                        NH.createHtmlTemplate(displayTemplate);

        // Create a template from the data provided if it already isn't one
        var _edit = (editTemplate instanceof NH.DynamicObjectTemplate) ?
                        editTemplate :
                        NH.createHtmlTemplate(editTemplate);

        // If the data source is a single object, put it in an array
        this.data = dataSource instanceof Array ?
                        dataSource :
                        [dataSource];

        var _elements = [];

        var _currentEdit = {element:null, dataIndex:-1};

        function finishEdit()
        {
            if (!_currentEdit.element) return;
            // In this function, "this" is the target of the double-click, the container
            var index = _currentEdit.dataIndex;
            var element = _currentEdit.element;
            var data = self.data[index];

            var displayNode = _display.createObjectNode(data);

            NH.moveChildren(element, displayNode);
            NH.removeEventListener(window, "click", handleClickOut);
            NH.addEventListener(element, "dblclick", self.editNode);

            _currentEdit.element = null;
            _currentEdit.dataIndex = -1;
        }
        function isInCurrentEdit(element)
        {
            return _currentEdit.element == element || NH.containsNode(_currentEdit.element, element);
        }
        function handleClickOut(evt)
        {
            if ( _currentEdit.element == null ||  isInCurrentEdit(evt.target)  )
                return;
            handleCancel.apply(_currentEdit.element, [evt]);
            NH.removeEventListener(window, "click", handleClickOut);
        }
        function handleSubmit(evt)
        {
            if (!this.parentNode) return;
            var index = getElementIndex(this.parentNode);
            var data = self.data[index];

            if( self.onVerify.apply(this, [data, evt]) !== false )
            {
                for (var i = 0; i < this.elements.length; ++i)
                {
                    var element = this.elements[i];
                    var name = NH.getAttribute(element,"name");
                    if(name)
                    {
                        data[name] = element.value;
                    }
                }
                finishEdit();
                self.onSubmit.apply(this, [data, evt]);
            }
        }
        function handleCancel(evt)
        {
            var element = _currentEdit.element;
            var data = self.data[_currentEdit.dataIndex];

            self.onCancel.apply(element, [data, evt]);
            finishEdit();
        }
        function getElementIndex(element)
        {
            var index = NH.getAttribute(element, "data-edit-index");
            if (index)
                return parseInt(index);
            return -1;
        }
        function setElementIndex(element, value)
        {
            NH.setAttribute("data-edit-index", value+"");
        }

        this.editNode = function (evt)
        {
            // In this function, "this" is the target of the double-click, the container
            var element = evt instanceof Element ? evt : this;

            if (_currentEdit.element == element)
                return;
            if (_currentEdit.element)
                handleCancel();

            var index = getElementIndex(element);
            var data = self.data[index];
            var editNode = _edit.createObjectNode(data);

            NH.moveChildren(element, editNode,
                function (node)
                {
                    if (node instanceof Element && node.tagName.toLowerCase() === "form")
                    {
                        NH.addEventListener(node, "submit", handleSubmit);
                        var cancel = null; var destroy = null;
                        if (cancel = node["cancel"] || node["Cancel"])
                        {
                            NH.addEventListener(cancel, "click", handleCancel);
                        }
                        if (destroy = node["delete"] || node["Delete"])
                        {
                            NH.addEventListener(destroy, "mouseup", self.deleteNode);
                        }
                    }
                });

            // Set timeout otherwise it will register the current click and cancel immediately
            setTimeout(function ()
            {
                NH.addEventListener(window, "click", handleClickOut);
            }, 10);
            NH.removeEventListener(element, "dblclick", editNode);
            var firstInput = element.getElementsByTagName("input")[0];
            if (firstInput)
                firstInput.select();

            _currentEdit.element = element;
            _currentEdit.dataIndex = index;
        }
        this.deleteNode = function(element)
        {
            if (this instanceof Element)
            {
                element = self.getNodeByChild(this);
            }
            var index = element instanceof Element ? getElementIndex(element) : element;

            // using 'self' because 'this' may change
            if (self.onDelete.apply(element, [self.data[index]]) !== false)
            {
                finishEdit();
                self.data.splice(index, 1);

                var parent = _elements[0].parentNode;
                self.populate(parent);
            }
        }
        this.selectNode = function(elem)
        {
            var element = null;
            var index = -1;
            if (elem instanceof Element)
            {
                element = elem;
                index = getElementIndex(element);
            }
            else if (elem instanceof Event)
            {
                element = this;
                index = getElementIndex(element);
            }
            else if (elem instanceof Number)
                element = _elements[index = elem];
            else
                throw "SelectNode object type not supported: " + elem;

            self.onSelect.apply(element,[self.data[index]]);
        }
        this.getNodeByChild = function(child)
        {
            var elementCount = _elements.length;
            var elm = child;
            while(elm != null)
            {
                for(var i=0;i<elementCount;++i)
                {
                    if (_elements[i] == elm)
                        return _elements[i];
                }
                elm = elm.parentNode;
            }
            return null;
        }
        this.getNode = function(index)
        {
            return _elements[index];
        }
        this.setDataSource = function(data)
        {
            var parent = null;
            if(_elements.length > 0)
            {
                for (var i = _elements.length - 1; i >= 0; --i)
                {
                    if (_elements[i].parentNode)
                    {
                        parent = _elements[i].parentNode;
                        break;
                    }
                }
                this.clearElements();
            }
            self.data = data instanceof Array ? data : [data];

            if (parent)
                this.populate(parent);
        }
        this.getDataIndex = function(data)
        {
            for(var i=0;i<this.data.length;++i)
            {
                if (this.data[i] == data)
                    return i;
            }
            return -1;
        }

        this.clearElements = function()
        {
            for(var i=0;i<_elements.length;++i)
            {
                NH.removeFromParent(_elements[i]);
            }
            _elements = [];
        }

        this.populate = function(parent)
        {
            this.clearElements();
            for(var i=0;i<self.data.length;++i)
            {
                var node = _display.createObjectNode(self.data[i]);
                NH.setAttribute(node, "data-edit-index", i + "");
                NH.addEventListener(node, "dblclick", self.editNode);
                NH.addEventListener(node, "click", self.selectNode);
                parent.appendChild(node);
                _elements.push(node);
            }
        }
        this.repopulate = function()
        {
            if (_elements.length == 0)
                return;

            var parent = _elements[0].parentNode;
            if (parent)
                this.populate(parent);
        }

        this.onVerify = function (dataSource, event) { }
        this.onSubmit = function (dataSource, event) { }
        this.onCancel = function (dataSource, event) { }
        this.onDelete = function (dataSource) { }
        this.onSelect = function (dataSource, event) { }
    }


    NH.createEditablesManager = function (displayTemplate, editTemplate, dataSource)
    {
        return new NH.EditableElementManager(displayTemplate, editTemplate, dataSource);
    }

})(ninjaHive);