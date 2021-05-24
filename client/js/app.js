let toDoObjects = {};
let userName = "";

function organizeByTags(toDoObjects) {
    // создание пустого массива для тегов
    let tags = [];
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

    return tags.map(function (tag) {
        // здесь мы находим все задачи,
        // содержащие этот тег
        let toDosWithTag = [];
        toDoObjects.forEach(function (toDo) {
            // проверка, что результат indexOf is *не* равен -1
            if (toDo.tags.indexOf(tag) !== -1) {
                toDosWithTag.push(toDo.description);
            }
        });
        // мы связываем каждый тег с объектом, который
        // содержит название тега и массив
        return {name: tag, toDos: toDosWithTag};
    });
}

function addRow(parent, toDo){
    const el = $("<li>");
    el.text(toDo.description);
    el.prop("id", toDo._id);

    const delButton = $("<a>");
    delButton.text("удалить")

    delButton.on('click', () => {
        el.remove();
        $.ajax({
            url: `/users/${userName}/todos/${toDo._id}`,
            type: 'DELETE'
        });
    })

    el.append(delButton);
    parent.append(el);
}


function main() {
    const content = $("main .content");

    $(".tabs a span").toArray().forEach((element) => {
        let $element = $(element);

        $element.on("click", () => {
            let $content;

            $(".tabs a span").removeClass("active");
            $element.addClass("active");
            content.empty();

            if ($element.parent().is(":nth-child(1)")) {
                $content = $("<ul>");
                for (let i = toDoObjects.length-1; i >= 0; i--) {
                    addRow($content, toDoObjects[i]);
                }

            } else if ($element.parent().is(":nth-child(2)")) {
                $content = $("<ul>");
                toDoObjects.forEach((todo) => {
                    addRow($content, todo);
                });

            }
            else if ($element.parent().is(":nth-child(3)")) {
                let organizedByTag = organizeByTags(toDoObjects);

                organizedByTag.forEach(function (tag) {
                    let $tagName = $("<h3>").text(tag.name),
                    $content = $("<ul>");
                    tag.toDos.forEach(function (todo) {
                        let $li = $("<li>").text(todo);
                        $content.append($li);
                    });
                    content.append($tagName);
                    content.append($content);
                });
            
            } else if ($element.parent().is(":nth-child(4)")) {
                let $descrInput = $("<input>");
                let $tagInput = $("<input>");
                let $button = $("<button>").text("+");

                let descrPart = $("<div>");
                descrPart.append($("<div>").text("Описание"))
                    .append($descrInput);

                let tagsPart = $("<div>");
                tagsPart.append($("<div>").text("Теги"))
                    .append($tagInput)
                    .append($button);

                $content = $("<div>")
                    .append(descrPart)
                    .append(tagsPart);

                $button.on("click", () => {
                    if ($descrInput.val() !== "" || $tagInput.val() !== "") {
                        let newTodo = {
                            "description": $descrInput.val(),
                            "tags": $tagInput.val().split(',')
                        };

                        $.post("todos", newTodo);
                        toDoObjects.push(newTodo);

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
}




$(() => {
    $.getJSON("todos", function (objects) {
        toDoObjects = objects;
        main();
    });
})
