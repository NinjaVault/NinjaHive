$(".list-group-item").on("click", function (e) {
    var previous = $(this).closest(".list-group").children(".active");
    previous.removeClass("active"); // previous list-item
    $(this).addClass("active"); // activated list-item
});