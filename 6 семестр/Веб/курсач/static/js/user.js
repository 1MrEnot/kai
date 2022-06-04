const months = {
    0: "января",
    1: "февраля",
    2: "марта",
    3: "апреля",
    4: "мая",
    5: "июня",
    6: "июля",
    7: "августа",
    8: "сентября",
    9: "октября",
    10: "ноября",
    11: "декабря"
};

let tickets = [];
let userId = null;

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2)
        return parts.pop().split(';').shift();

    return null;
}

function setUserData(){
    let email = getCookie("email");
    $.get(`/api/users/${email}`, (user) => {
        let $email = $("#email");
        let $money = $("#money");

        tickets = user.tickets;
        userId = user._id;
        $email.text(`Email: ${user.email}`);
        $money.val(user.money);

        showTickets(tickets);
    });
}

function getTicketComponent(f, buyButtonTrigger, deleteButtonTrigger){
    let root = $("<div>").addClass("row my-2").attr("id", f._id);

    let card = $("<div>").addClass("ticket col-12 card");
    root.append(card);

    let cardBody = $("<div>").addClass("row card-body");
    card.append(cardBody);

    let cardBodyLeft = $("<div>").addClass("col");
    cardBodyLeft.append($("<div>").addClass("cost text-center").text(f.cost));
    cardBodyLeft.append($("<div>").addClass("class text-muted text-center my-2").text(f.class));

    if (buyButtonTrigger){
        cardBodyLeft.append($("<input>")
            .addClass("btn-block")
            .text(f.class)
            .attr("style", "width:100%")
            .attr("type", "button")
            .attr("value", "Купить"));
    }

    cardBody.append(cardBodyLeft);

    let cardBodyRight = $("<div>").addClass("col-8");
    let row = $("<div>").addClass("row");
    cardBodyRight.append(row);

    let fromData = $("<div>").addClass("col from");
    fromData.append($("<div>").addClass("city").text(f.from));
    fromData.append($("<div>").addClass("time").text(hourAndMinute(f.fromDate)));
    fromData.append($("<div>").addClass("date text-muted").text(dayMonthYear(f.fromDate)));

    let delta = $("<div>").addClass("col delta").text(getDelta(f.fromDate, f.toDate));

    let toData = $("<div>").addClass("col to");
    toData.append($("<div>").addClass("city").text(f.to));
    toData.append($("<div>").addClass("time").text(hourAndMinute(f.toDate)));
    toData.append($("<div>").addClass("date text-muted").text(dayMonthYear(f.toDate)));

    row.append(fromData).append(delta).append(toData);

    cardBody.append(cardBodyLeft);
    cardBody.append(cardBodyRight);

    if (deleteButtonTrigger){
        let delData = $("<div>").addClass("col-2 del");
        let button = $("<input>")
            .addClass("btn-block")
            .text(f.class)
            .attr("style", "width:100%")
            .attr("type", "button")
            .attr("value", "Удалить");

        delData.append(button);
        cardBody.append(delData);

        button.on('click', () =>{
            deleteButtonTrigger(f._id);
        });
    }

    return root
}

function showTickets(tickets){
    let list = $("#ticket-list");

    if (tickets.length === 0){
        list.append($('<p>').text("Билетов пока нет"));
    }
    else{
        tickets.forEach(t => {
            t.fromDate = new Date(t.fromDate);
            t.toDate = new Date(t.toDate);

            const component = getTicketComponent(t, null, deleteTicket);
            list.append(component);
        });
    }
}

function hourAndMinute(datetime){
    return formatTwo(datetime.getMinutes(), datetime.getHours());
}

function getDelta(fromDatetime, toDatetime){
    let ms = toDatetime - fromDatetime;
    let mins = ms / 60 / 1000;
    let h = Math.ceil(mins / 60);
    let m = mins % 60;

    return formatTwo(m, h);
}

function formatTwo(low, high){
    if (low < 10){
        low = "0" + low;
    }

    if (high < 10){
        high = "0" + high;
    }

    return high + ":" + low;
}

function dayMonthYear(datetime){
    let day = datetime.getDate();
    let month = months[datetime.getMonth()];
    let year = datetime.getFullYear();
    return day + " " + month + " " + year;
}

function deleteTicket(id){
    $.ajax({
        url: `api/users/${userId}/tickets/${id}`,
        type: 'DELETE',
        success: () =>{
            $(`#${id}`).remove();
        }
    })
}

function resetMoney(){
    let newMoney = $("#money").val();

    $.ajax({
        url: `api/users/${userId}/money/${newMoney}`,
        type: 'put'
    });
}

function deleteAccount(){
    $.ajax({
        url: `api/users/${userId}`,
        type: 'delete'
    });
    window.location = "/";
}


$(() => {
    setUserData();

    $("#money").on('change', resetMoney);
    $("#account-delete").on('click', deleteAccount);
});