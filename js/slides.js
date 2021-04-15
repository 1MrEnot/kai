const url = "https://www.flickr.com/services/rest/";
const key = "1a7b1f584bf13839e06157367c7b9269";
let photos = [];

function serach_by_tag(tag){
    $.get(url, 
        {
            method: "flickr.photos.search",
            api_key: key,
            tags: tag,
            format: "json",
            nojsoncallback: 1
        },
        (response) => photos = response.photos.photo
        );
}

function show_images(){
    let i = 0;

    setInterval(function(){
        draw_sized_image(photos[i].id);
        i = (i+1) % photos.length;
    },
    5000);
}

function draw_sized_image(photo_id){
    $.get(url,
        {
            method: "flickr.photos.getSizes",
            api_key: key,
            photo_id: photo_id,
            format: "json",
            nojsoncallback: 1
        },
        function(response){
            let imgUrl = response.sizes.size[6].source;
            draw_image(imgUrl);
        });
}

function draw_image(url){
    var $img = $("<img>").attr("src", url).hide();
    $(".message").empty();
    $(".message").append($img);
    $img.fadeIn();
}

function main() {
    $(".textbox").on('change', function() {
        serach_by_tag($(".textbox").val());
    });
    show_images();
}

$(document).ready(main);
