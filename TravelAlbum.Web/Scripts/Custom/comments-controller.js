// $(document).ready(function () {  
$(".submit-comment-button").on("click", function (event) {
    var singleImageId = $(this).attr("data-id"),
        commentBoxes = $(".add-comment-textarea"),
        commentBox,
        content;
    if (commentBoxes.length > 0) {
        commentBox = commentBoxes[0];
        content = $(commentBox).val()
    }
    $.post("/Comments/AddComment",
        { id: singleImageId, content: content }, function success(data, textStatus, jqXHR) {
            $(".add-comment-textarea").val("");
            handleActionCall(singleImageId);
        });
})

function handleActionCall(id) {
    history.pushState({}, null, "/Comments/");
    $("#comments-container").load("/Comments/ShowBatchComments/" + id);
    history.pushState({}, null, "/SingleImages/Details/" + id);
}

var page = 1;

$("#show-comments-button").on("click", function (event) {
    var id = $("#comments-container").attr("data-id");
    history.pushState({}, null, "/Comments/");
    $("#comments-container").load("ShowBatchComments/" + id + "?page=" + page);
    $("#comments-part").css("display", "inline-block");
    history.pushState({}, null, "/SingleImages/Details/" + id);   
    page++;
})

