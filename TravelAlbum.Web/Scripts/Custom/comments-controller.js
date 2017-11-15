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

// $("#show-comments-button").on("click", function () { showComments() });
// function showComments() {
//     var id = $("#comments-container").attr("data-id");
//     $("#show-comments-button").css("display", "none");
//     $("#comments-part").css("display", "inline-block");
//     $("#hide-comments-button").css("display", "inline-block")
//     handleActionCall(id);
// }

// $("#hide-comments-button").on("click", function () { hideComments() });
// function hideComments() {
//     $("#hide-comments-button").css("display", "none")
//     $("#comments-part").css("display", "none")
//     $("#show-comments-button").css("display", "inline-block")
// }

function handleActionCall(id) {
    history.pushState({}, null, "/Comments/");
    $("#comments-container").load("/Comments/ShowBatchComments/" + id);
    history.pushState({}, null, "/SingleImages/Details/" + id);
}
var page = 1;

//$("#show-comments-button").on("click", function (event) {
//    var id = $("#comments-container").attr("data-id");
//    $.ajax({
//        type: 'GET',
//        url: '/Comments/ShowBatchComments/' + id,
//        data: { 'page': page },
//        dataType: 'json',
//        success: function (data) {
//            if (data !== null) {
//                for (var i = 0; i < data.length; i++) {
//                    $("#comments-part").css("display", "inline-block");
//                }

//                page++;
//            }
//        },
//        beforeSend: function () {
//            $("#progress").show();
//        },
//        complete: function () {
//            $("#progress").hide();
//        },
//        error: function () {
//            alert("Error while retrieving data!");
//        }
//    });
//})



$("#show-comments-button").on("click", function (event) {
    var id = $("#comments-container").attr("data-id");
    history.pushState({}, null, "/Comments/");
    $("#comments-container").load("ShowBatchComments/" + id + "?page=" + page);
    $("#comments-part").css("display", "inline-block");
    history.pushState({}, null, "/SingleImages/Details/" + id);   
    page++;
})

