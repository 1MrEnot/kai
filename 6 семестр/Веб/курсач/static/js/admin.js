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

let flights = [];

function getAndShow(){
    $.get("/api/tickets", (res) => {
        flights = res;
        flights.forEach((f) => {
            f.fromDate = new Date(f.fromDate);
            f.toDate = new Date(f.toDate);
        })
        showFlights(flights);
    });
}

function addTickets(){
    let fromDateTime = $('#whenInput').val();
    let length = $('#lengthInput').val();

    let from = $('#fromInput').val();
    let to = $('#toInput').val();

    let bAmount = Number($('#businessAmountInput').val());
    let bCost = Number($('#businessCostInput').val());
    let eAmount = Number($('#economyAmountInput').val());
    let eCost = Number($('#economyCostInput').val());

    if (!fromDateTime || !length || !from || !to ||
        (!bAmount || !bCost) && (!eAmount || !eCost)){
        alert("Полностью заполните поля!");
        return;
    }

    let [flightH, flightM] = length.split(':');
    let addRequest = {
        from: from,
        to: to,
        fromDate: new Date(fromDateTime),
        toDate: new Date(fromDateTime).addTime(flightH, flightM),
        businessAmount: bAmount,
        businessCost: bCost,
        economyAmount: eAmount,
        economyCost: eCost
    };

    $.post("/api/tickets/", addRequest, ()=> {
        clearInput();
        getAndShow();
    });
}

function clearInput(){
    $('#whenInput').val("");
    $('#lengthInput').val("");
    $('#fromInput').val("");
    $('#toInput').val("");

    $('#businessAmountInput').val("");
    $('#businessCostInput').val("");
    $('#economyAmountInput').val("");
    $('#economyCostInput').val("");
}


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

function showFlights(flights){
    let list = $("#ticket-list");

    flights.forEach(f => {
        let component = getFlightComponent(f, null, deleteTicket);
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

function deleteTicket(id){
    $.ajax({
        url: `api/tickets/${id}`,
        type: 'DELETE',
        success: () => {
            $(`#${id}`).remove();
        }
    });
}

Date.prototype.addTime = function(h, m) {
    h = Number(h);
    m = Number(m);

    this.setTime(this.getTime() + (h*60*60*1000 + m*60*1000));
    return this;
}

$(()=>{
    getAndShow();
    $("#addButton").on("click", addTickets);
});
