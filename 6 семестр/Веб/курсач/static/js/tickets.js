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

let userId = null;
let money = 0;
let flights = [];

function getFlightComponent(f, buyButtonTrigger, deleteButtonTrigger){
    let root = $("<div>").addClass("row my-2").attr("id", f._id);

    let card = $("<div>").addClass("ticket col-12 card");
    root.append(card);

    let cardBody = $("<div>").addClass("row card-body");
    card.append(cardBody);

    let cardBodyLeft = $("<div>").addClass("col");
    cardBodyLeft.append($("<div>").addClass("cost text-center").text(f.cost));
    cardBodyLeft.append($("<div>").addClass("class text-muted text-center my-2").text(f.class));

    if (buyButtonTrigger){
        let button = $("<input>")
            .addClass("btn-block")
            .text(f.class)
            .attr("style", "width:100%")
            .attr("type", "button")
            .attr("value", "Купить");

        cardBodyLeft.append(button);
        button.on('click', () => buyButtonTrigger(f._id));
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

function showFlights(flights){
    let list = $("#ticket-list");
    list.empty();

    flights.forEach(f => {
        let component = getFlightComponent(f, buyTicket, false);
        list.append(component);
    });
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
        userId = user._id;
        money = user.money;
        console.log(`${user.email} - ${money}`);
        resetBalance();
    });
}

function searchTickets() {
    let params = {};
    let from = $("#from").val();
    let to = $("#to").val();
    let classVal = $("#class").val();
    let date = $("#date").val();

    if (from){
        params.from = from;
    }

    if (to){
        params.to = to;
    }

    if (classVal){
        params.class = classVal;
    }

    if (date){
        params.date = date;
    }

    let url = "/api/tickets";
    if (params){
        url += '?' + $.param(params)
    }

    $.get(url, (res) => {
        flights = normalizeFlights(res);
        showFlights(flights);
    });
}

function normalizeFlights(flights){
    flights.forEach((f) => {
        f.fromDate = new Date(f.fromDate);
        f.toDate = new Date(f.toDate);
    });
    return flights;
}

function buyTicket(id){
    let ticket = $(`#${id}`);
    let cost = Number(ticket.find(".cost").text());

    if (cost > money){
        alert("Недостаточно средств");
        return;
    }

    $.post(`api/users/${userId}/tickets/${id}`, (res) => {
        if(res.message){
            alert(res.message);
        }
        else{
            money -= cost;
            resetBalance();
            ticket.remove();
        }
    });
}

function resetBalance(){
    $('#money').text(`Баланс: ${money || 0}`);
}

$(()=>{
    $.get("/api/tickets", (res) => {
        flights = normalizeFlights(res);
        showFlights(flights);
    });

    $("#search-button").on('click', searchTickets);

    setUserData();
    resetBalance();
});
