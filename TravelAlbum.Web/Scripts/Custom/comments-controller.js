// $(document).ready(function () {  

$(".submit-comment-button").on("click", function (event) {
    postComment(this, "/SingleImages/Details/", "SingleImages");
})

$(".submit-travel-comment-button").on("click", function (event) {
    postComment(this, "/Travels/Details/", "Travels");
})

function postComment(root ,route, controller) {
    var travelObjectId = $(root).attr("data-id"),
        commentBoxes = $(".add-comment-textarea"),
        commentBox,
        content;
    if (commentBoxes.length > 0) {
        commentBox = commentBoxes[0];
        content = $(commentBox).val()
    }
    $.post("/Comments/AddComment",
        { id: travelObjectId, content: content, controller: controller }, function success(data, textStatus, jqXHR) {
            $(".add-comment-textarea").val("");
            handleActionCall(travelObjectId, route);
        });
}

function handleActionCall(id, route) {
    history.pushState({}, null, "/Comments/");
    $("#comments-container").load("/Comments/ShowBatchComments/" + id);
    history.pushState({}, null, route + id);
}

var page = 1;

$("#show-comments-button").on("click", function (event) {
    var id = $("#comments-container").attr("data-id");
    var route = $("#comments-container").attr("data-route");
    history.pushState({}, null, "/Comments/");
    $("#comments-container").load("ShowBatchComments/" + id + "?page=" + page);
    $("#comments-part").css("display", "inline-block");
    history.pushState({}, null, route + id);   
    page++;
})

