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

var flights = [];

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
        let component = getFlightComponent(f, true, false);
        list.append(component);
    });
}

function hourAndMinute(datetime){
    return fomratTwo(datetime.getMinutes(), datetime.getHours());
}


function getDelta(fromDatetime, toDatetime){
    let ms = toDatetime - fromDatetime;
    let mins = ms / 60 / 1000;
    let h = Math.ceil(mins / 60);
    let m = mins % 60;

    return fomratTwo(m, h);
}

function fomratTwo(low, high){
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

function main(){
    $.get("/api/tickets", (res) => {
        flights = res;
        flights.forEach((f) => {
            f.fromDate = new Date(f.fromDate);
            f.toDate = new Date(f.toDate);
        })
        showFlights(flights);
    });
}


$(main);

/*
<div class="row my-2">
    <div class="ticket col-12 card">
        <div class="row card-body">
            <div class="col-4">
                <div class="cost text-center">5 000</div>
                <div class="class text-muted text-center my-2">Эконом</div>

                <input class="btn-block" style="width:100%" type="button" value="Купить">
            </div>

            <div class="col-8">
                <div class="row">
                    <div class="col from">
                        <div class="city">
                            Казань
                        </div>
                        <div class="time">
                            12:00
                        </div>
                        <div class="date text-muted">
                            12 января 2021
                        </div>
                    </div>

                    <div class="col delta">
                        2:30
                    </div>

                    <div class="col to">
                        <div class="city">
                            Москва
                        </div>
                        <div class="time">
                            14:30
                        </div>
                        <div class="date text-muted">
                            12 января 2021
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
</div>
*/