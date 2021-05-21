import {TicketModel} from '../models.js'

function getTicketsToAdd(addRequest){
    let res = [];

    for (let i = 0; i < addRequest.businessAmount; i++) {
        res.push({
            from: addRequest.from,
            to: addRequest.to,
            fromDate: addRequest.fromDate,
            toDate: addRequest.fromDate,
            cost: addRequest.businessCost,
            class: "Бизнес"
        });
    }

    for (let i = 0; i < addRequest.economyAmount; i++) {
        res.push({
            from: addRequest.from,
            to: addRequest.to,
            fromDate: addRequest.fromDate,
            toDate: addRequest.fromDate,
            cost: addRequest.economyCost,
            class: "Эконом"
        });
    }

    return res;
}

export async function add (req, res){
    let body = req.body;

    body.fromDate = new Date(body.fromDate);
    body.toDate = new Date(body.toDate);
    body.businessAmount = Number(body.businessAmount);
    body.businessCost = Number(body.businessCost);
    body.economyAmount = Number(body.economyAmount);
    body.economyCost = Number(body.economyCost);

    let ticketsToAdd = getTicketsToAdd(body);

    for (const t of ticketsToAdd) {
        let ticketModel = new TicketModel(t);
        let addResult = await ticketModel.save();

        if (addResult.errors){
            return res.json({
                message: 'Failed to add new ticket(s)',
                errors: addResult.errors
            }).status(500);
        }
    }

    res.json({message: `Added ${ticketsToAdd.length} tickets`}).sendStatus(200);
}


export async function getAll (req, res){
    let tickets = await TicketModel.find();
    res.json(tickets).sendStatus(200);
}

export async function search(req, res){
    let flights = [
        {
            from: req.query.from,
            to:  req.query.to,
            fromDate: new Date(2021, 2, 12, 14, 30),
            toDate: new Date(2021, 2, 12, 16, 30),
            cost: 7500,
            class: req.query.class
        }
    ];
    res.json(flights);
}