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

$("#show-comments-button").on("click", function () { showComments() });
function showComments() {
    var id = $("#comments-container").attr("data-id");
    $("#show-comments-button").css("display", "none");
    $("#comments-part").css("display", "inline-block");
    $("#hide-comments-button").css("display", "inline-block")
    handleActionCall(id);
}

$("#hide-comments-button").on("click", function () { hideComments() });
function hideComments() {
    $("#hide-comments-button").css("display", "none")
    $("#comments-part").css("display", "none")
    $("#show-comments-button").css("display", "inline-block")
}

function handleActionCall(id) {
    history.pushState({}, null, "/Comments/");
    $("#comments-container").load("/Comments/ShowBatchComments/" + id);
    history.pushState({}, null, "/SingleImages/Details/" + id);
}