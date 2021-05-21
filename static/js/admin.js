function addTickets(){
    let whenInputVal = $('#whenInput').val();
    let lengthInputVal = $('#lengthInput').val();
    let [h, m] = lengthInputVal.split(':');

    let addRequest = {
        from: $('#fromInput').val(),
        to: $('#toInput').val(),
        fromDate: new Date(whenInputVal),
        toDate: new Date(whenInputVal).addTime(h, m),
        businessAmount: Number($('#businessAmountInput').val()),
        businessCost: Number($('#businessCostInput').val()),
        economyAmount: Number($('#economyAmountInput').val()),
        economyCost: Number($('#economyCostInput').val())
    };

    $.post("/api/tickets/", addRequest, console.log);
}


Date.prototype.addTime = function(h, m) {
    h = Number(h);
    m = Number(m);

    this.setTime(this.getTime() + (h*60*60*1000 + m*60*1000));
    return this;
}




function main(){
    $('#addButton').click(addTickets);
}

$(main);
