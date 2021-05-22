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
    // TODO брать время вылета из datetime
    let fromDate = $('#whenInput').val();
    let fromTime = $('#whenTimeInput').val();
    let lengthInputVal = $('#lengthInput').val();
    let [flightH, flightM] = lengthInputVal.split(':');
    let [fromH, fromM] = fromTime.split(':');

    let from = $('#fromInput').val();
    let to = $('#toInput').val();

    let bAmount = Number($('#businessAmountInput').val());
    let bCost = Number($('#businessCostInput').val());
    let eAmount = Number($('#economyAmountInput').val());
    let eCost = Number($('#economyCostInput').val());

    if (!fromDate || !fromTime || !lengthInputVal || !from || !to ||
        (!bAmount || !bCost) && (!eAmount || !eCost)){
        return;
    }

    let addRequest = {
        from: from,
        to: to,
        fromDate: new Date(fromDate).resetTime(fromH, fromM),
        toDate: new Date(fromDate).resetTime(fromH, fromM).addTime(flightH, flightM),
        businessAmount: bAmount,
        businessCost: bCost,
        economyAmount: eAmount,
        economyCost: eCost
    };

    $.post("/api/tickets/", addRequest, clearInput);
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

function getFlightComponent(f, addBuyButton, addDeleteButton){
    let root = $("<div>").addClass("row my-2");

    let card = $("<div>").addClass("ticket col-12 card");
    root.append(card);

    let cardBody = $("<div>").addClass("row card-body");
    card.append(cardBody);

    let cardBodyLeft = $("<div>").addClass("col");
    cardBodyLeft.append($("<div>").addClass("cost text-center").text(f.cost));
    cardBodyLeft.append($("<div>").addClass("class text-muted text-center my-2").text(f.class));

    if (addBuyButton){
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

    if (addDeleteButton){
        let delData = $("<div>").addClass("col-2 del");
        delData.append($("<input>")
            .addClass("btn-block")
            .text(f.class)
            .attr("style", "width:100%")
            .attr("type", "button")
            .attr("value", "Удалить"));
        cardBody.append(delData);

    }

    return root
}

function showFlights(flights){
    let list = $("#ticket-list");

    flights.forEach(f => {
        let component = getFlightComponent(f, false, true);
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

Date.prototype.resetTime = function(h, m) {
    h = Number(h);
    m = Number(m);

    this.setTime(this.getTime() + ((h-3)*60*60*1000 + m*60*1000));
    return this;
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
