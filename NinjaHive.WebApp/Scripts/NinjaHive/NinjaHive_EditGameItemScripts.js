var ninjaHive = ninjaHive || {};

(function (NH) {
    NH.requestUrl = {};


    NH.swapSubCategories = function(main, subCategories) {
        var form = main.form;
        var subSelect = form["GameItem.CategoryId"];

        NH.changeSelectOptions(subSelect, subCategories || []);
        if (!subCategories) {
            var ajax = new XMLHttpRequest();
            ajax.open("GET", NH.requestUrl.subCategories + "?parentId=" + main.value);

            ajax.onreadystatechange = function () {
                if (ajax.readyState == 4) {
                    if (ajax.status == 200) {
                        NH.changeSelectOptions(subSelect, JSON.parse(ajax.responseText));
                    }
                    else {
                        console.log(ajax.responseText);
                    }
                }
            }
            ajax.send();
        }
    }
    NH.changeSelectOptions = function(select, options, defaultName) {
        // Clear the sub-categories drop-down
        while (select.firstChild)
            select.removeChild(select.firstChild);


        if (options.length == 0) {
            options.push({ Id: "0.0.0.0", Name: "Loading" });
        }
        var defaultIndex = 0;
        for (var i = 0; i < options.length; ++i) {
            var option = document.createElement("option");
            option.value = options[i].Id;
            option.innerHTML = options[i].Name;
            if (options[i].Name == defaultName)
                defaultIndex = i;
            select.appendChild(option);
        }
        select.options[defaultIndex].selected = true;
    }

    NH.setMainCategory = function(mainSelect, categoryLabel, subCategoryLabel) {
        var ajax = new XMLHttpRequest();
        ajax.open("GET", NH.requestUrl.mainCategories + "?Name=" + categoryLabel, true);

        ajax.onreadystatechange = function () {
            if (ajax.readyState == 4) {
                if (ajax.status == 200) {
                    var category = JSON.parse(ajax.responseText)[0];
                    NH.changeSelectOptions(mainSelect.form["GameItem.CategoryId"], category.SubCategories, subCategoryLabel);
                    NH.selectOptionWithLabel(mainSelect, categoryLabel);
                }
                else {
                    console.log(ajax.responseText);
                }
            }
        }
        ajax.send();
    }
    NH.selectOptionWithLabel = function(select, optionLabel) {
        var options = select.getElementsByTagName("option");
        for (var i = 0; i < options.length; ++i) {
            if (options[i].innerHTML.indexOf(optionLabel) >= 0) {
                options[i].selected = true;
                return true;
            }
        }
        return false;
    }
})(ninjaHive);