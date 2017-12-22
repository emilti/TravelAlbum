var pageSize = 2;
var pageIndex = 0;

$(document).ready(function () {
    GetData();

    $(window).scroll(function () {
        if ($(window).scrollTop() >= ($(document).height() - $(window).height() - 1)) {
            GetData();
        }
    });

    $(document).scroll(function () {
        console.log("$(window).scrollTop() = " + $(window).scrollTop())
        console.log("$(document).height() = " + $(document).height());
        console.log("(window).height() = " + $(window).height());
    });
});



// $(document).ready(function () {
//     GetData();
//     $(window).endlessScroll({
//         inflowPixels: 1000,
//         callback: function () {
//             GetData();
//         }
//     });
// });

function GetData() {
    var url = $('#url-path').html();
    $.ajax({
        type: 'GET',
        url: '/home/GetSingleImagesOnScroll',
        data: { 'url': url, 'pageIndex': pageIndex },
        dataType: 'json',
        success: function (data) {
            if (data !== null) {
                for (var i = 0; i < data.length; i++) {
                    var dateSplitted = data[i].CreatedOn.split(/[(|)]/);
                    var dateTicks = parseInt(dateSplitted[1]);
                    var date = new Date(dateTicks);
                    var dateFormatted = date.getMonth() + 1 + '/' + date.getDate() + '/' + date.getFullYear();
                    $("#single-images-container").append(
                        "<div class='single-image-container'>" +
                            "<a href='Images/Details/" + data[i].SingleImageId + "'>" +
                                "<img class='single-image' src='data:image/jpg;base64," + data[i].SingleImageData + "' />" +
                                "<div class='cover-container'>" +
                                "</div>" +
                            "</a>" +
                            "<div class='description'>" + dateFormatted + "</div>" +
                            "</div>")
                }

                pageIndex++;
            }
        },
        beforeSend: function () {
            $("#progress").show();
        },
        complete: function () {
            $("#progress").hide();
        },
        error: function () {
            alert("Error while retrieving data!");
        }
    });
}


