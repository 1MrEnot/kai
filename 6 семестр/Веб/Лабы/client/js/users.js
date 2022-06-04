"use strict";

let users = [];

function showUsers(){
    users.map(u => u.username).forEach(addUser);
}

function addUser(userName) {
    if (!userName || users.indexOf(userName) !== -1) {
        return;
    }

    const input = $(".comment-input input");
    const $newUser = $("<p>").text(userName);
    const $delLink = $("<a>").text("X")

    $newUser.hide();
    $newUser.append($delLink);
    $(".comments").append($newUser);
    $newUser.fadeIn();

    input.val("");
    users.push(userName);
    $delLink.on('click', () => {
        $newUser.remove();
        $.ajax({
            url: `/users/${userName}`,
            type: 'DELETE'
        });
    })

    $.post(`/users/${userName}`);
}

function main () {
    $(".comment-input button").on("click", ()=>{
        addUser($(".comment-input input").val());
    });

    $(".comment-input input").on("keypress", function (event) {
        if (event.keyCode === 13) {
            addUser();
        }
    });

    $.get("/users", (res) => {
        users = res;
        showUsers();
    });
}

$(main);
    