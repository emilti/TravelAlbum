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
                    var imageContainer = "<div class='single-image-container image-in-travel'>" +
                        "<img class='single-image' src='data:image/jpg;base64," + data[i].ImageData + "' />" +
                        "<a href='data:image/jpg;base64," + data[i].ImageData + "' data-lightbox='image-1'>" +
                        "<div class='cover-container-left'>" + "</div>" +
                        "</a>" +
                        "<a href='Images/Details" + data[i].Id + "'>" +
                        "<div class='cover-container-right'>" +
                        "</div>" +
                        "</a>" +
                        "<div class='description'>" + data[i].CreatedOn + "</div>" +
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