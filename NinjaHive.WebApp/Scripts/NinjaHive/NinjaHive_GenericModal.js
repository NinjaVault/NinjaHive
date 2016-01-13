// namespace ninjaHive
var ninjaHive = ninjaHive || {};

(function(NH)
{
	var _btnSubmitClass = 'btn-primary';
	var _btnCancelClass = 'btn-default';


/*==========================================
*		class ModalManager()
*-----------------------------------------------------------------
*		Allows for code interfacing with modals.
*       Creates, manages, and manipulates existing modals on the page
*/
function ModalManager()
{
// private:
	var _modalList = [];

	var _pageHasOpenModal = function()
	{
		
		var n=_modalList.length;
		for(var i=0;i<n;++i)
		{
			if(_modalList[i].isOpen())
				return true;
		}
		return false;
	}
	var _getUnopenedModal = function()
	{
		var n=_modalList.length;
		for(var i=0;i<n;++i)
		{
			var modal = _modalList[i];
			if(!modal.isOpen())
				return modal;
		}
		// If we get here, they're all in use. Make a new one
		var modal = new GenericModal();
		_modalList.push(modal);
		return modal;
	}
	
// public:
	this.genericDialog = function()
	{
		var zIndex = 0;
		for(var i=0;i<_modalList.length;++i)
		{
			if(_modalList[i].isOpen())
				zIndex++;
		}
		
		// Redirect the arguments to the modal
		// Makes for easier to maintain code
		var modal = _getUnopenedModal();
		modal.showDialog.apply(modal, arguments);
		modal.getElement().style.zIndex = 1050 + zIndex;
		
		return modal;
	}
	
	/*=============================
	*		agreeDialog
	*------------------------------------------------
	*		Overloads:
	*			agreeDialog(String title, HTMLString body)
	*			agreeDialog(String title, HTMLString body, Boolean hasDynamicContent)
	*/
	this.agreeDialog = function(title, body, dynamicContent)
	{
	    var buttonArgs = [{'button': 'all', 'visible': false},
            {'button':'submit', 'label':'I Agree','visible':true,'className':_btnSubmitClass},
            {'button':'cancel', 'label':'I Do Not Agree','visible':true,'className':_btnCancelClass}];
	    
	
	    return this.genericDialog.apply(this, NH.mergeArrays(arguments, buttonArgs));
	};
	
	/*=============================
	*		confirmDialog
	*------------------------------------------------
	*		Overloads:
	*			confirmDialog(String title, HTMLString body)
	*			confirmDialog(String title, HTMLString body, Boolean hasDynamicContent)
	*/
	this.confirmDialog = function(title, body, dynamicContent)
	{
	    var buttonArgs = [{'button': 'all', 'visible': false},
            {'button':'submit', 'label':'Ok','visible':true,'className':_btnSubmitClass},
            {'button':'cancel', 'label':'Cancel','visible':true,'className':_btnCancelClass}];
	
	    return this.genericDialog.apply(this, NH.mergeArrays(arguments, buttonArgs));
	};
	
	
	/*=============================
	*		questionDialog
	*------------------------------------------------
	*		Overloads:
	*			questionDialog(String title, HTMLString body)
	*			questionDialog(String title, HTMLString body, Boolean hasDynamicContent)
	*/
	this.questionDialog = function(title, body, dynamicContent)
	{
	    var buttonArgs = [{'button': 'all', 'visible': false},
            {'button':'submit', 'label':'Yes','visible':true,'className':_btnSubmitClass},
            {'button':'cancel', 'label':'No','visible':true,'className':_btnCancelClass}];
	
	    return this.genericDialog.apply(this, NH.mergeArrays(arguments, buttonArgs));
	};
	
	/*=============================
	*		questionDialog
	*------------------------------------------------
	*		Overloads:
	*			questionDialog(String title, HTMLString body)
	*			questionDialog(String title, HTMLString body, Boolean hasDynamicContent)
	*/
	this.alertDialog = function(title, body, dynamicContent)
	{
	    var buttonArgs = [{'button': 'all', 'visible': false},
            {'button':'submit', 'label':'Ok','visible':true,'className':_btnSubmitClass}];
	
	    return this.genericDialog.apply(this, NH.mergeArrays(arguments, buttonArgs));
	};
}



/*==========================================
*		class GenericModal()
*-----------------------------------------------------------------
*		A generic modal class that allows for altering the contents of a BootStrap modal
*			and !potentially! creating multiple simultaneous BS modals
*/
function GenericModal(parent)
{
	var ModalState = {CLOSED:0,    OPEN:1,    ACTION_CLOSED:2};
	
	var self = this;
// private members:
	var _modal, _dialog, _container;
	var _header, _body, _footer;
	
	var _state = 0;
	
	
	
// private functions: Trying to keep the modal interactions abstracted away

	// Generates the generic modal so this class is reusable (multiple modal support)
	var _generate= function()
	{
		_modal = document.createElement("div");
			_modal.className = "modal fade";
			// TODO: Fix ARIA support
			NH.setAttributes(_modal,"tabindex:-1,  role:dialog"); // ,  aria-labelledby:genericModalLabel
			
		_dialog = document.createElement("div");
			_dialog.className = "modal-dialog";
			NH.setAttribute(_dialog,"role","document");
			_modal.appendChild(_dialog);
			
		_container = document.createElement("div");
			_container.className = "modal-content";
			_dialog.appendChild(_container);
			
		_header = new GenericModal.ModalHeader(self);
		_body = new GenericModal.ModalBody(self);
		_footer = new GenericModal.ModalFooter(self);
		
		// If the modal is closed in any way, we want to know about it.
		$(_modal).on("hidden.bs.modal",_actionClose);
	}
	var _actionClose = function()
	{
		_state = ModalState.CLOSED;
	}
	var _hasButton = function(type)
	{
		return _footer.getButton(type) != null;
	}

	var _hideDialog = function()
	{
		$(_modal).modal( 'hide' );
	}
	var _showDialog = function()
	{
		$(_modal).modal( 'show' );
		_footer.arrangeButtons();
		_state = ModalState.OPEN;
	}
	var _setTitle = function(str)
	{
		_header.setTitle(str);
	}
	var _setBody = function(htmlStr)
	{
		_body.setBody(htmlStr);
	}
	var _setDynamicContent = function(newContent)
	{
		_body.setDynamicContent(newContent);
	}
	var _showDynamicContent = function()
	{
		_body.showDynamicContent();
	}
	var _hideDynamicContent = function()
	{
		_body.hideDynamicContent();
	}
	var _setButtonLabel = function(type, str)
	{
		if(!_footer.hasButton(type) )
			throw new Error("Button "+type+" does not exist on this modal.");
		
		_footer.setButtonLabel(type, str);
	}
	

	
// public:
	/*=============================
	*		create
	*------------------------------------------------
	*		Overloads:
	*			create()
	*			create(DOMNode parent)
	*		Details:
	*			Will generate the modal if it doesn't already exist, and add it to the beginning of the provided [parent]
	*			If no parent is provided, it will add it to the beginning of the BODY
	*			If the modal is already on the page, it will be moved to [parent]
	*/
	this.create	= function(parent)
	{
		parent = parent || document.getElementsByTagName("body")[0];
		
		if(!_modal)
			_generate(this);
		
		parent.appendChild(_modal);
		
		// Gross jQuery to enable to modal interactions
		$(_modal).modal(  {'backdrop':true, 'keyboard':true, 'show':false, 'remote':false}  );
	}
	
	/*=============================
	*		destroy
	*------------------------------------------------
	*		Overloads:
	*			destroy()
	*		Details:
	*			Removes the modal from the page
	*/
	this.destroy = function()
	{
		_modal.parentNode.removeChild(_modal);
	}
	/*=============================
	*		on
	*------------------------------------------------
	*		Overloads:
	*			on(String type, Function(event, modalTitle) listener)
	*		Details:
	*			[type] can be either "submit" or "cancel".
	*			Will call the provided listener when the corresponding button is pressed
	*			Set the listener to null or leave it undefined to clear the event.
	*/
	this.on = function(type, func)
	{
		var funcType = typeof func;
		if(funcType != "function" && funcType != "object" && funcType != "undefined")
			throw new TypeError("on: The listener must be null, undefined, or a function: "+funcType);
	
		if(func === undefined || func === null)
			func = function(){};
		
		if(!_footer.setEventListener(type, func))
			throw new Error("on: Unsupported event: "+type);
		
		return this;
	}
	/*=============================
	*		clearAllListeners
	*------------------------------------------------
	*		Overloads:
	*			clearAllListeners()
	*		Details:
	*			Clears both the submit and cancel events.
	*/
	this.clearAllListeners = function()
	{
		_footer.clearAllListeners();
	}
	
	this.isOpen = function()
	{
		return _state === ModalState.OPEN;
	}
	this.getContainer = function()
	{
		return _container;
	}
	this.closeDialog = function()
	{
		_hideDialog();
	}


	this.addButton = function(options)
	{
		if(typeof options == 'string')
			options = {'button':options};
		return _footer.addButton(options);
	}
	this.setButtonLabel = function(button,label)
	{
		_footer.setButtonLabel(button,label);
	}
	this.showButton = function(type)
	{
		_footer.setButtonOptions({'button':type,'visible':true});
	}
	this.hideButton = function(type)
	{
		_footer.setButtonOptions({'button':type,'visible':false});
	}
	
	this.getElement = function()
	{
		return _modal;
	}
	
	/*=============================
	*		setDynamicContent
	*------------------------------------------------
	*		Overloads:
	*			setDynamicContent(HTMLString content)
	*			setDynamicContent(DOMNode content)
	*			setDynamicContent(DOMNode[] content)
	*		Details:
	*			Changes the dynamic content in the Modal.
	*			Won't display if the modal wasn't opened with dynamic content enabled.
	*/
	this.setDynamicContent	= function(newContent)
	{
		_setDynamicContent(newContent);
	}
	
	/*=============================
	*		showDialog
	*------------------------------------------------
	*		Overloads:
	*			showDialog(String title, HTMLString body)
	*			showDialog(String title, HTMLString body, String submitLabel)
	*			showDialog(String title, HTMLString body, String submitLabel, String cancelLabel)
	*			showDialog(String title, HTMLString body, String submitLabel, String cancelLabel, Boolean hasDynamicContent)
	*/
	this.showDialog = function(title, body, dynamicContent)
	{
	    var buttonStart = 3;
	    var dynamic = dynamicContent;
	    if (dynamicContent === undefined || typeof dynamicContent != "boolean")
	    {
	        dynamic = false;
	        buttonStart = 2;
	    }
	
		_setTitle(title);
		_setBody(body);
		
		
		if (dynamic)
		{
			_setDynamicContent("Loading...");
			_showDynamicContent();
		}
		else
		{
			_hideDynamicContent();
		}
		
		// If there are more arguments than named above, the rest SHOULD BE button options
		for(var i=buttonStart; i<arguments.length;++i)
		{
			var options = arguments[i];
			if(  !_footer.setButtonOptions(options)  )
				_footer.addButton(options);
		}
		
		this.clearAllListeners();
		_showDialog(this);
		
		return this;
	};
	
	
	
	
// Constructor:
	/*=============================
	*		GenericModal
	*------------------------------------------------
	*		Overloads:
	*			GenericModal()
	*			GenericModal(DOMNode parent)
	*/
	
	this.create(parent);
}



/*==========================================
*		class ModalHeader()
*-----------------------------------------------------------------
*		Handles the title and close of the modal
*/
GenericModal.ModalHeader = function(parent)
{
	if(!parent)
		throw new Error("ModalHeader requires a parent.");
	
	var _container;
	var _title;
	
	var _generate = function()
	{
		_container = document.createElement("div");
			_container.className = "modal-header";
			parent.getContainer().appendChild(_container);
			
		var close = document.createElement("button");
			close.className = "close";
			NH.setAttributes(close, "type:button,  data-dismiss:modal,  aria-label:Close");
			close.innerHTML = '<span aria-hidden="true">&times;</span>';
			_container.appendChild(close);
		
		_title = document.createElement("h4");
			_title.className = "modal-title";
			_container.appendChild(_title);
	}
	var _setTitle = function(str)
	{
		NH.clearChildren(_title);
		// Append a text node to ensure nothing gets parsed (no tags, scripts, etc.)
		_title.appendChild(document.createTextNode(str));
	}
	
	this.setTitle = function(str)
	{
		if(typeof str != "string")
			throw new TypeError("ModalHeader.setTitle requires a string.");
		_setTitle(str);
	}
	
// Constructor
	_generate();
}




/*==========================================
*		class ModalBody()
*-----------------------------------------------------------------
*		Handles the content and dynamic content of the modal
*/
GenericModal.ModalBody = function(parent)
{
	if(!parent)
		throw new Error("ModalBody requires a parent.");
	
	var _container;
	var _body, _content;
	
	var _generate = function()
	{
		_container = document.createElement("div");
			_container.className="modal-body";
			parent.getContainer().appendChild(_container);
			
		_body = document.createElement("div");
			_container.appendChild(_body);
			
		_content = document.createElement("div");
			_content.className="well modal-subcontent";
			_container.appendChild(_content);
	}
	var _setBody = function(newContent)
	{
		NH.clearChildren(_body);
		if(typeof newContent == "string")
		{
			_body.innerHTML = newContent;
		}
		else if(typeof newContent == "object")
		{
			// If it's an array:
			if(newContent.toString() == ([]).ToString())
			{
				for(var i=0;i<newContent.length;++i)
				{
					_body.appendChild(newContent[i]);
				}
			}
			// Otherwise it should be a single document node
			else
			{
				_body.appendChild(newContent);
			}
		}
	}
	var _setDynamicContent = function(newContent)
	{
		NH.clearChildren(_content);
		if(typeof newContent == "string")
		{
			_content.innerHTML = newContent;
		}
		else if(typeof newContent == "object")
		{
			// If it's an array:
			if(newContent.toString() == [].toString())
			{
				for(var i=0;i<newContent.length;++i)
				{
					_content.appendChild(newContent[i]);
				}
			}
			// Otherwise it should be a single document node
			else
			{
				_content.appendChild(newContent);
			}
		}
	}
	var _showDynamicContent = function()
	{
		_content.style.display = "";
	}
	var _hideDynamicContent = function()
	{
		_content.style.display = "none";
	}
	
// public:
	this.setBody = _setBody;
	this.setDynamicContent = _setDynamicContent;
	this.showDynamicContent = _showDynamicContent;
	this.hideDynamicContent = _hideDynamicContent;
	
// Constructor:
	_generate();
}



/*==========================================
*		class ModalButton()
*-----------------------------------------------------------------
*		Handles the button options, appearance, and functionality for the modal
*/
GenericModal.ModalButton = function(type, label, visible, className, parent)
{
// private:
	var _type, _element, _label, _parent,
		_visible = true,
		_className = 'btn-default';
		_element = document.createElement("button");
		
	var _event = function(){};
	var _action = function(evt)
	{
		if(!_event.call(_parent.getParentModal(), evt))
			_parent.getParentModal().closeDialog();
	};

// public:
	this.setType = function(type)
	{
		return _type = type.toLowerCase();
	};
	this.getType = function()
	{
		return _type;
	};
	this.setLabel = function(label)
	{
		_label = label;
		NH.clearChildren(_element);
		_element.appendChild(document.createTextNode(_label));
		return _label;
	};
	this.getLabel = function()
	{
		return _label;
	};
	this.setVisible = function(vis)
	{
		return _visible = vis;
	};
	this.isVisible = function()
	{
		return _visible;
	};
	this.setClassName = function(className)
	{
		_className = className;
		_element.className = "btn " + _className;
		return _className;
	};
	this.getClassName = function()
	{
		return _className;
	};
	this.setParent = function(node)
	{
		return _parent = node;
	};
	this.getParent = function()
	{
		return _parent;
	};
	this.getElement = function()
	{
		return _element;
	};
	this.setEventListener = function(func)
	{
		_event = func;
	};
	this.useOptions = function(options)
	{
		if(!options)
			throw new TypeError("ModalButton.parseOptions requires a valid options object.");
		else if(options.button == 'close')
			throw new Error("Cannot name a ModalButton 'close', that is a reserved event name.");
		
		if(options.button)
			this.setType(options.button);
		if(options.label)
			this.setLabel(options.label);
		if(options.visible != undefined)
			this.setVisible(options.visible);
		if(options.className)
			this.setClassName(options.className);
		if(options.parent)
			this.setParent(options.parent);
	};
	
	
	

	
	
// Constructor:
	(function(self){
		var options;
		if(typeof type == 'string')
		{
			options = {};
			options.button = type;
			options.label = type[0].toUpperCase() + type.substring(1);
			
			if(visible === undefined)
			{
				options.parent = label;
			}
			else if(className === undefined)
			{
				options.label = label;
				options.parent = visible;
			}
			else if(parent === undefined)
			{
				options.label = label;
				options.visible = visible;
				options.parent = className;
			}
			else
			{
				options.label = label;
				options.visible = visible;
				options.className = className;
				options.parent = parent;
			}
		}
		else
		{
			options = type;
		}
		self.useOptions(options);
		
		NH.setAttribute(_element, "type","button");
		NH.addEventListener(_element, "click", _action);
	})(this)
}


/*==========================================
*		class ModalFooter()
*-----------------------------------------------------------------
*		Manages the modal's buttons and events
*/
GenericModal.ModalFooter = function(parent)
{
	if(!parent)
		throw new Error("ModalFooter requires a parent.");
	
// private:
	var _container;
	var _buttons = [];
	
	var _generate = function()
	{
		_container = document.createElement("div");
			_container.className = "modal-footer";
			parent.getContainer().appendChild(_container);
		
		_generateButtons();
	}
	
	var _generateButtons = function(arrOptions)
	{
		if(arrOptions)
		{
			var n = arrOptions.length;
			for(var i=0;i<n;++i)
			{
				var options = arrOptions[i];
				
				_addButton(options);
			}
		}
		
		if(!_hasSubmit())
			_addButton({'button':'submit', 'className':'btn-primary', 'label':'Ok'});
		
		if(!_hasCancel())
			_addButton({'button':'cancel', 'className':'btn-default', 'label':'Cancel'});
	}
	
	var _addButton = function(options)
	{
		if(!options.button)
			throw new Error("Modal creation error: No button type provided for button: " + i );
		
		var buttonType = options.button.toLowerCase();
		if(  _findButtonByType(buttonType) > -1  )
			throw new Error("Modal creation error: Duplicate button type provided: " + buttonType);

		var button = new GenericModal.ModalButton(options);
		
		if(buttonType == 'submit' || !_hasSubmit())
			_buttons.push(button);
		else if(buttonType == 'cancel')
			_buttons.unshift(button);
		else
			_buttons.splice(_buttons.length-1, 0, button);
		
		
		return button;
	}
	var _deleteButton = function(index)
	{
		if(index == 0 || index == _buttons.length - 1)
			throw new Error("Cannot delete the submit or cancel buttons! (Try hiding them)");
		
		_buttons.splice(index, 1);
	}
	
	var _findButtonByType = function(buttonType)
	{
		var n = _buttons.length;
		for(var i=0;i<n;++i)
		{
			if(_buttons[i].getType() == buttonType)
				return i;
		}
		return -1;
	}
	var _findButtonByReference = function(buttonObject)
	{
		var n = _buttons.length;
		for(var i=0;i<n;++i)
		{
			if(_buttons[i] == buttonObject)
				return i;
		}
		return -1;
	}
	var _hasSubmit = function()
	{
		return _buttons.length > 0 && _buttons[_buttons.length-1].getType() == 'submit';
	}
	var _hasCancel = function()
	{
		return _buttons.length > 0 && _buttons[0].getType() == 'cancel';
	}
	
// public:
	this.getParentModal = function () {
	    return parent;
	}
	this.buttonCount = function()
	{
		return _buttons.length;
	}
	this.getButton = function(index)
	{
		var indexType = typeof index;
		if(indexType == 'number')
		{
			return _buttons[index];
		}
		else if(indexType == 'string')
		{
			index = _findButtonByType(index);
			if(index<0)
				return null;
			return _buttons[index];
		}
		throw new Error("getButton only accepts button type strings or integer indexes.");
	}
	this.hasButton = function(index)
	{
		var indexType = typeof index;
		if(indexType == 'number')
		{
			return index >= 0 && index < _buttons.length;
		}
		else if(indexType == 'string')
		{
			index = _findButtonByType(index);
			return index >= 0;
		}
		else if(indexType == 'object')
		{
			index = _findButtonByReference(index);
			return index >= 0;
		}
		throw new Error("hasButton only accepts button type strings, integer indexes, or button objects.");
	}
	this.addButton = function(options)
	{
		options.parent = this;
		return _addButton(options);
	}
	this.deleteButton = function(arg)
	{
		var argType = typeof arg;
		
		if(argType == 'number')
		{
			var index = arg;
			if(index < 0 || index >= _buttons.length)
				return null;
			return _deleteButton( index );
		}
		else if(argType == 'string')
		{
			var index = _findButtonByType(arg);
			if(index < 0)
				return null;
			return _deleteButton( index );
		}
		else if(argType == 'object')
		{
			var index = _findButtonByReference(arg);
			if(index < 0)
				return null;
			return _deleteButton( index );
		}
	}
	this.clearAllListeners = function()
	{
		for(var i=0;i<_buttons.length; ++i)
		{
			_buttons[i].setEventListener(function(){});
		}
	}
	this.setEventListener = function(type, func)
	{
		var button = this.getButton(type);
		if(button == null)
			return false;
		
		button.setEventListener(func);
		return true;
	}
	this.arrangeButtons = function()
	{
		NH.clearChildren(_container);
		for(var i=0;i<_buttons.length;++i)
		{
			if(_buttons[i].isVisible())
				_container.appendChild(_buttons[i].getElement());
		}
	}
	this.setButtonOptions = function(options)
	{
		if(!options.button || typeof options.button != 'string')
			throw new Error("ModalFooter.handleButtonOptions parameter requires a 'button' member of type 'string'.");
		
		options.parent = this;
		if(options.button == 'all')
		{
			delete options.button;
			
			var num = _buttons.length;
			for(var i=0;i<num;++i)
			{
				_buttons[i].useOptions(options);
			}
		}
		else
		{
			var button = this.getButton(options.button);
			if(button != null)
			{
				button.useOptions(options);
			}
			else
			{
				return false;
			}
		}
		return true;
	}
	this.setButtonLabel = function(type,str)
	{
		this.setButtonOptions({'button':type, 'label':str});
	}
	_generate();
}




NH.modal = null;
NH.addEventListener(document,"DOMContentLoaded", function(){NH.modal = new ModalManager();});
})(ninjaHive);