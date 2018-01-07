$(document).ready(function () {
    var text = $(".travel-text").html();
    GetData();
});

function GetData() {
    var url = $('#url-path').html();
    var travelObjectId = $(".travel-text").attr("data-id");
    $.ajax({
        type: 'GET',
        url: '/travels/GetImagesRelatedToTravel/' + travelObjectId,
        dataType: 'json',
        success: function (data) {
            if (data !== null) {
                var text = $(".travel-text").html();
                for (var i = 0; i < data.length; i++) {   
                    var stringifiedDate = data[i].CreatedOn
                    var value = new Date
                        (
                        parseInt(stringifiedDate.replace(/(^.*\()|([+-].*$)/g, ''))
                        );
                    var date = value.getMonth() +
                        1 +
                        "/" +
                        value.getDate() +
                        "/" +
                        value.getFullYear();

                    var imageContainer =
                        "<div class='single-image-container-in-travel image-in-travel'>" +
                        "<img class='single-image' src='data:image/jpg;base64," + data[i].ImageData + "' />" +
                        "<a id='single_image' href='data:image/jpg;base64," + data[i].ImageData + "'>" +
                        "<div class='cover-container-left'>" + "</div>" +
                        "</a>" +
                        "<a href='Images/Details/" + data[i].Id + "'>" +
                        "<div class='cover-container-right' data-id='" + data[i].Id + "'>" +
                        "</div>" +
                        "</a>" +
                        "<div class='description'>" + date + "</div>" +
                        "</div>";

                    text = text.replace(data[i].TravelLabel, imageContainer);
                }

                $(".travel-text").html(text);

            }
        },
        error: function () {
            alert("Error while retrieving data!");
        }
    });
}


$(document).on("click", $('.cover-container-right'), function () {
    window.history.replaceState({}, document.title, "/" + "");   
});