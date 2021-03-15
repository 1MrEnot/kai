"use strict";

function organizeByTags(toDoObjects) {
    // создание пустого массива для тегов
    var tags = [];
    // перебираем все задачи toDos
    toDoObjects.forEach(function (toDo) {
        // перебираем все теги для каждой задачи
        toDo.tags.forEach(function (tag) {
            // убеждаемся, что этого тега еще нет в массиве
            if (tags.indexOf(tag) === -1) {
                tags.push(tag);
            }
        });
    });

    var tagObjects = tags.map(function (tag) {
        // здесь мы находим все задачи,
        // содержащие этот тег
        var toDosWithTag = [];
        toDoObjects.forEach(function (toDo) {
            // проверка, что результат indexOf is *не* равен -1
            if (toDo.tags.indexOf(tag) !== -1) {
                toDosWithTag.push(toDo.description);
            }
        });
        // мы связываем каждый тег с объектом, который
        // содержит название тега и массив
        return { "name": tag, "toDos": toDosWithTag };
    });

    return tagObjects;
};


function main() {
    $(".tabs a span").toArray().forEach(function (element) {
        var $element = $(element);

        $element.on("click", function () {
            var $content;

            $(".tabs a span").removeClass("active");
            $element.addClass("active");
            $("main .content").empty();

            if ($element.parent().is(":nth-child(1)")) {
                $content = $("<ul>");
                for (var i = toDoObjects.length-1; i >= 0; i--) {
                    $content.append($("<li>").text(toDoObjects[i].description));
                }

            } else if ($element.parent().is(":nth-child(2)")) {
                $content = $("<ul>");
                toDoObjects.forEach(function (todo) {
                    $content.append($("<li>").text(todo.description));
                });

            }
            else if ($element.parent().is(":nth-child(3)")) {
                let organizedByTag = organizeByTags(toDoObjects);

                organizedByTag.forEach(function (tag) {
                    var $tagName = $("<h3>").text(tag.name),
                    $content = $("<ul>");
                    tag.toDos.forEach(function (todo) {
                        var $li = $("<li>").text(todo);
                        $content.append($li);
                    });
                    $("main .content").append($tagName);
                    $("main .content").append($content);
                });
            
            } else if ($element.parent().is(":nth-child(4)")) {
                var $descrInput = $("<input>");
                var $tagInput = $("<input>");
                var $button = $("<button>").text("+");

                var descrPart = $("<div>");
                descrPart.append($("<div>").text("Описание"))
                    .append($descrInput);

                var tagsPart = $("<div>");
                tagsPart.append($("<div>").text("Теги"))
                    .append($tagInput)
                    .append($button);

                $content = $("<div>")
                    .append(descrPart)
                    .append(tagsPart);

                $button.on("click", function () {
                    if ($descrInput.val() !== "" || $tagInput.val() !== "") {
                        let descr = $descrInput.val();
                        let tags = $tagInput.val().split(",");
                        toDoObjects.push({
                            "description": descr,
                            "tags": tags
                        });

                        $descrInput.val("");
                        $tagInput.val("");
                        $(".tabs a:first-child span").trigger("click");
                    }
                });
            }

            $("main .content").append($content);

            return false;
        });
    });

    $(".tabs a:first-child span").trigger("click");
};

var toDoObjects = {};


$(document).ready(function(){
    $.getJSON("todos.json", function (objects) {
        toDoObjects = objects;
        main();
    });
});


