var ninjaHive = ninjaHive || {};


(function(NH)
{


    NH.initCategoriesPage = function()
    {
        return new CategoryPage();
    }

    // class CategoryPage
    function CategoryPage()
    {
        var self = this;

        var mainId = "000000.0000.0000.00000000";
        var mainParent = null;
        var subParent = null;
        var requestUrl = {};

        this.mainCategoryManager = null;
        this.subCategoryManager = null;

        this.addRequestUrl = function(name, url)
        {
            requestUrl[name] = url;
        }

        this.setupMainCategoryManager = function(data, parent, displayTemplate, editTemplate, editValidation, deleteValidation)
        {
            setupMainCategoryManager.apply(this, arguments);
        }
        this.setupSubCategoryManager = function (parent, displayTemplate, editTemplate, editValidation, deleteValidation)
        {
            setupSubCategoryManager.apply(this,arguments);
        }
        this.start = function()
        {
            if (self.mainCategoryManager.data.length > 0)
                self.mainCategoryManager.getNode(0).click();
            else
                pageLacksCategories();
        }








        function setupMainCategoryManager(data, parent, displayTemplate, editTemplate, editValidation, deleteValidation)
        {
            mainParent = parent;
            self.mainCategoryManager = ninjaHive.createEditablesManager(
                        displayTemplate,
                        editTemplate,
                        data
                        );
            self.mainCategoryManager.populate(mainParent);
            self.mainCategoryManager.onSelect = function(category)
            {
                if(!category) return;

                mainId = category.Id;
                if(category.SubCategories)
                {
                    self.subCategoryManager.setDataSource(category.SubCategories);
                    self.subCategoryManager.populate(subParent);
                }
            }
            self.mainCategoryManager.onDelete = function(category)
            {
                if(!category) return false;
                if(category.SubCategories.length > 0)
                {
                    var alert = ninjaHive.modal.alertDialog("Cannot Complete this Action","Unable to delete this Main Category because it has Subcategories:",true);
                    var ul = document.createElement("ul");
                    for(var i=category.SubCategories.length-1;i>=0;--i)
                    {
                        var li = document.createElement("li");
                        li.innerHTML = category.SubCategories[i].Name;
                        ul.appendChild(li);
                    }
                    alert.setDynamicContent(ul);
                    return false;
                }
                ninjaHive.sendVerifiableForm(deleteValidation,{Id:category.Id,IsMainCategory:true},requestUrl.deleteCategory,
                    {
                        onSuccess: function(evt, ajax)
                        {
                            var response = JSON.parse(ajax.responseText);
                            if(response.Success)
                            {
                                updateMainCategoryData(response.Data);
                            }
                            else
                            {
                                ninjaHive.modal.alertDialog("An Error Occurred",
                                    "Sorry, it appears that an error has occurred while deleting a main category. Please try again later.");
                                console.log(response.Data);
                            }
                        },
                        onError: function(evt, ajax)
                        {
                            ninjaHive.modal.alertDialog("Critical Error",
                                "Sorry, it appears that an error has occurred while deleting a main category. Please try again later.");
                            console.log(ajax.responseText);
                        }
                    });
                self.mainCategoryManager.getNode(0).click();
            }
            self.mainCategoryManager.onSubmit = function(category)
            {
                ninjaHive.sendVerifiableForm(editValidation,{Id:category.Id,Name:category.Name},requestUrl.editMainCategory,callbacks);
            }
            
            function updateMainCategoryData(data)
            {
                if(data instanceof Array)
                {
                    var elem = self.mainCategoryManager.data[ getMainCategoryIndex( mainId ) ];
                    var id = elem !==undefined ? elem.Id : 0;
                    mainCategories = data;
                    self.mainCategoryManager.setDataSource(mainCategories);
                    self.mainCategoryManager.repopulate();

                    var index = getMainCategoryIndex(id);
                    if(index > -1)
                    {
                        self.mainCategoryManager.getNode(index).click();
                    }
                    else if(data.length > 0)
                    {
                        self.mainCategoryManager.getNode(0).click();
                    }

                    if(data.length > 0)
                        pageHasCategories();
                    else
                        pageLacksCategories();
                }
            }
        }









        function setupSubCategoryManager(parent, displayTemplate, editTemplate, editValidation, deleteValidation)
        {
            subParent = parent;
            self.subCategoryManager = ninjaHive.createEditablesManager(
                        displayTemplate,
                        editTemplate,
                        []
                        );
            self.subCategoryManager.onSubmit = function(category)
            {
                ninjaHive.sendVerifiableForm(editValidation, { Id: category.Id, Name: category.Name, MainCategoryId: category.MainCategoryId },
                                             requestUrl.editSubCategory, callbacks);
            }
            self.subCategoryManager.onDelete = function(category)
            {
                var gameItemUrl = requestUrl.relatedItems;
                gameItemUrl.substring(0,gameItemUrl.lastIndexOf("/")+1);
                var ajax = ninjaHive.createHttpRequest("POST",gameItemUrl+"/"+category.Id,false);
                ajax.send({id: category.Id});
                var response = null;

                if(ajax.responseText && (response = JSON.parse(ajax.responseText)) && response.length > 0)
                {
                    var alert = ninjaHive.modal.alertDialog("Cannot Complete","Cannot delete subcategory because there are Items under it:",true);
                    var ul = document.createElement("ul");
                    for(var i=0;i<response.length;++i)
                    {
                        var li = document.createElement("li");
                        li.innerHTML = response[i];
                        ul.appendChild(li);
                    }
                    alert.setDynamicContent(ul);
                    return false;
                }
                else
                {
                    ninjaHive.sendVerifiableForm(deleteValidation, { Id: category.Id, IsMainCategory: false },
                                                    requestUrl.deleteCategory, callbacks);
                }
            }

            function hookSubCategoryForm()
            {
                function addSubCategory(evt)
                {
                    this["MainCategoryId"].value = mainId;
                    ninjaHive.sendVerifiableForm(this, this.action,
                        {
                            onSuccess: function (evt, ajax)
                            {
                                var response = null;
                                if (ajax.responseText && (response = JSON.parse(ajax.responseText)) && response.Success)
                                {
                                    updateSubCategoryData(response.Data);
                                }
                                else
                                {
                                    ninjaHive.modal.alertDialog("Error", "An error has occurred creating the subcategory. Please try again later.");
                                }
                            },
                            onError: function (evt, ajax)
                            {
                                ninjaHive.modal.alertDialog("Error", "An error has occurred creating the subcategory. Please try again later.");
                                console.log(ajax.responseText);
                            }
                        });
                    this["Name"].value = "";
                    evt.preventDefault();
                    return false;
                }
                ninjaHive.addEventListener(document.forms["addSubCategory"], "submit", addSubCategory);
            }

            function updateSubCategoryData(data)
            {
                if (data instanceof Array)
                {
                    if (data.length > 0)
                    {
                        var mainIndex = getMainCategoryIndex(data[0].MainCategoryId);
                        self.mainCategoryManager.data[mainIndex].SubCategories = data;
                        self.subCategoryManager.setDataSource(self.mainCategoryManager.data[mainIndex].SubCategories);

                        if (mainId == data[0].MainCategoryId)
                        {
                            self.subCategoryManager.populate(subParent);
                        }
                    }
                }
                else
                    throw "Invalid data provided to update subcategories with: " + data;
            }
            hookSubCategoryForm();
        }












        function getMainCategoryIndex(id)
        {
            for(var i=self.mainCategoryManager.data.length-1; i>=0;--i)
            {
                if(self.mainCategoryManager.data[i].Id == id)
                    return i;
            }
            return -1;
        }
        function pageLacksCategories()
        {
            var body = document.getElementsByTagName("body")[0];
            body.className = (body.className+" noCategories").trim();
        }
        function pageHasCategories()
        {
            var body = document.getElementsByTagName("body")[0];
            body.className = body.className.replace("noCategories","").trim();
        }


        var callbacks = {
            onSuccess: function (evt, ajax)
            {
                var response = JSON.parse(ajax.responseText);
                if (!response.Success)
                {
                    console.log("Error: ");
                    console.log(response.Data)
                }
            },
            onError: function (evt, ajax)
            {
                ninjaHive.modal.alertDialog("Failed", "An error has occured communicating with the server. Please try again later.");
                console.log(ajax.responseText);
            }
        };
    }
})(ninjaHive);