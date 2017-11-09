﻿    var pageSize = 2;
    var pageIndex = 0;

    $(document).ready(function () {
        GetData();

        $(document).scroll(function () {
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

    function GetData() {
        var url = $('#url-path').html();
        $.ajax({
            type: 'GET',
            url: '/home/GetSingleImagesOnScroll',
            data: { 'url': url, 'pageIndex': pageIndex},
            dataType: 'json',
            success: function (data) {
                if (data !== null) {
                    for (var i = 0; i < data.length; i++) {
                        $("#single-images-container").append( 
                            "<div class='single-image-container'>" +
                            "<a href='SingleImages/Details/" + data[i].SingleImageId + "'>" +
                                "<img class='single-image' src='data:image/jpg;base64," + data[i].SingleImageData + "' />" +
                            "</a>" + 
                            "<div class='description'>" + data[i].Description + "</div>" +
                            "<div>" + data[i].CreatodOn + "</div>" +
                        "</div>")
                    }
                    pageIndex++;
                }
            },
            beforeSend : function () {
                $("#progress").show();
            },
            complete : function () {
                $("#progress").hide();
            },
            error: function () {
                alert("Error while retrieving data!");
            }
        });
    }


        