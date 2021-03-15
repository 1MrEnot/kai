"use strict";

function main() {
    var toDos = [
        "Закончить писать эту книгу",
        "Вывести Грейси на прогулку в парк",
        "Ответить на электронные письма",
        "Подготовиться к лекции в понедельник",
        "Обновить несколько новых задач",
        "Купить продукты",
    ];

    $(".tabs a span").toArray().forEach(function (element) {
        $(element).on("click", function () {
            $(".tabs a span").removeClass("active");
            $(element).addClass("active");
            $("main .content ul").empty();

            var $element = $(element), 
                $list = $("<ul>");

            if ($element.parent().is(":nth-child(1)")){
                for (var i = toDos.length; i > -1; i--) {
                    $list.append($("<li>").text(toDos[i]));
                }
                $("main .content").append($list);
            }

            if ($element.parent().is(":nth-child(2)")){
                toDos.forEach(function (todo) {
                    $list.append($("<li>").text(todo));
                });
                $("main .content").append($list);
            }

            if ($element.parent().is(":nth-child(3)")){
                console.log("3 click");
            }

            return false;
        });
    });

    $(".tabs a:first-child span").trigger("click");
};

$(document).ready(main);