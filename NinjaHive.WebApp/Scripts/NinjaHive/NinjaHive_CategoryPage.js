// ninjaHive namespace
var ninjaHive = ninjaHive || {};

// ninjaHive.categoryPage namespace
ninjaHive.categoryPage = ninjaHive.categoryPage || {};

(function () {
    var categoryPage = function () {
        var CP = this;
        var lastMainCategory = null;
        var lastSubCategory = null;

        // global variable to let modification from outside the namespace
        this.subCategoryFormClassName;
        this.mainCategoryFormIdName;
        this.subCategoryGroupClassName;

        this.MainCategoryNode = function (domElement) {
            var _domElement = domElement;
            
            this.toDomElement = function () {
                return _domElement;
            }

            this.activate = function () {
                var dom = document.getElementsByClassName(_domElement.className + " active");                
                _domElement.className += " active";
            }

            this.deactivate = function () {                
                _domElement.className = _domElement.className.replace(" active", "");
            }

            this.startEdit = function () {
                for (var i = 0; i < _domElement.children.length; i++) {
                    if (_domElement.children[i].className == "input") {
                        _domElement.children[i].style.display = "inline";
                        this.activate();
                    }
                }
            }

            this.saveEdit = function () {

            }

            this.endEdit = function () {
                for (var i = 0; i < _domElement.children.length; i++) {
                    if (_domElement.children[i].className == "input") {
                        _domElement.children[i].style.display = "none";
                    }
                }
            }

            this.hideSubCategoryForm = function () {
                var elements = document.getElementsByClassName(CP.subCategoryFormClassName);

                for (var i = 0; i < elements.length; i++) {
                    if (elements[i].getAttribute("data-parent-id") == _domElement.getAttribute("data-id")) {
                        CP.SubCategoryForm(elements[i]).hide();
                    }
                }
            }

            this.showSubCategoryForm = function() {
                var elements = document.getElementsByClassName(CP.subCategoryFormClassName);

                for (var i = 0; i < elements.length; i++) {                    
                    if (elements[i].getAttribute("data-parent-id") == _domElement.getAttribute("data-id")) {
                        CP.SubCategoryForm(elements[i]).show();
                    }
                }
            }

            this.getSubCategoryForm = function () {
                var elements = document.getElementsByClassName(CP.subCategoryFormClassName);
                var result = [];

                for (var i = 0; i < elements.length; i++) {
                    if (elements[i].getAttribute("data-parent-id") == _domElement.getAttribute("data-id")) {
                        result.push(new CP.SubCategoryForm(elements[i]));
                    }
                }

                return result[0];
            }

            this.onclick = function () {
                CP.hideAllSubCategoriesForm(document.getElementsByClassName(CP.subCategoryFormClassName));

                this.activate();
                this.showSubCategoryForm();
            }

            return this;
        };

        this.MainCategoryForm = function (domElement) {
            var _domElement = domElement;

            this.hide = function () {
                _domElement.style.visibility = "hidden";
            }

            this.show = function () {
                _domElement.style.visibility = "visible";
            }

            this.getChildren = function () {
                var result = [];

                for (var i = 0; i < _domElement.children.length; i++) {
                    result.push(new CP.MainCategoryNode(_domElement.children[i]));
                }

                return result;
            }

            return this;
        };

        this.SubCategoryNode = function (domElement) {
            var _domElement = domElement;

            this.show = function () {
                _domElement.style.display = "block";
            }

            this.hide = function () {
                _domElement.style.display = "none";
            }

            return this;
        };

        this.SubCategoryForm = function (domElement) {
            var _domElement = domElement;

            this.hide = function () {
                _domElement.style.display = "none";
            }

            this.show = function () {
                _domElement.style.display = "block";
            }

            this.showSubCategories = function () {
                var elements = document.getElementsByClassName(CP.subCategoryGroupClassName);

                for (var i = 0; i < elements.length; i++) {
                    if (elements[i].getAttribute("data-parent-id") == _domElement.getAttribute("data-parent-id")) {
                        CP.SubCategoryNode(elements[i]).show();
                    }
                }
            }

            this.hideSubCategories = function () {
                var elements = document.getElementsByClassName(CP.subCategoryGroupClassName);

                for (var i = 0; i < elements.length; i++) {
                    if (elements[i].getAttribute("data-parent-id") == _domElement.getAttribute("data-parent-id")) {
                        CP.SubCategoryNode(elements[i]).hide();
                    }
                }
            }

            return this;
        }

        this.deactivateAllMainCategories = function () {
            var elements = document.getElementsByClassName(CP.mainCategoryFormIdName);

            for (var i = 0; i < elements.length; i++) {
                CP.MainCategoryNode(elements[i]).deactivate();
            }
        }

        this.hideAllSubCategoryForms = function () {
            var elements = document.getElementsByClassName(CP.subCategoryFormClassName);

            for (var i = 0; i < elements.length; i++) {
                CP.SubCategoryForm(elements[i]).hide();
            }          
        }

        this.hideAllSubCategories = function () {
            var elements = document.getElementsByClassName(CP.subCategoryFormClassName);

            for (var i = 0; i < elements.length; i++) {
                CP.SubCategoryForm(elements[i]).hideSubCategories();
            }
        }

        this.onMainCategoryClick = function (sender) {            
            var mainCategory = new ninjaHive.categoryPage.MainCategoryNode(sender);
     
            if (lastMainCategory != null) {
                if (lastMainCategory.toDomElement() != mainCategory.toDomElement()) {
                    lastMainCategory.getSubCategoryForm().hideSubCategories();
                    lastMainCategory.hideSubCategoryForm();
                    lastMainCategory.endEdit();
                    lastMainCategory.deactivate();
                }
            } else {
                CP.hideAllSubCategoryForms();
                CP.hideAllSubCategories();
            }

            mainCategory.activate();
            mainCategory.getSubCategoryForm().showSubCategories();
            mainCategory.showSubCategoryForm();

            lastMainCategory = mainCategory;
        }

        this.onMainCategoryEdit = function(sender) {
            var mainCategory = new ninjaHive.categoryPage.MainCategoryNode(sender);
            mainCategory.startEdit();

            if (lastMainCategory != null) {
                if (lastMainCategory.toDomElement() != mainCategory.toDomElement()) {
                    lastMainCategory.getSubCategoryForm().hideSubCategories();
                    lastMainCategory.hideSubCategoryForm();
                    lastMainCategory.endEdit();
                    lastMainCategory.deactivate();
                }
            } else {
                CP.hideAllSubCategoryForms();
                CP.hideAllSubCategories();
            }

            mainCategory.activate();
            mainCategory.getSubCategoryForm().showSubCategories();
            mainCategory.showSubCategoryForm();

            lastMainCategory = mainCategory;
        }

        this.onMainCategoryDelete = function (sender) {

        }

        this.onSubCategoryClick = function (sender) {
            var subCategory = new CP.SubCategoryNode(sender);


            if (lastSubCategory != null) {

            } else {

            }

            lastSubCategory = subCategory;
        }

        this.onSubCategoryDelete = function (sender) {

        }

        return this;
    };

    ninjaHive.categoryPage = new categoryPage();
})();


