$(document).on("click",".list-group-item", function (e) {
    if (this.className == "active")
        return;
    var previous = $(this).closest(".list-group").children(".active");
    previous.removeClass("active"); // previous list-item
    $(this).addClass("active"); // activated list-item
});