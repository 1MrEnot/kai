import {TicketModel} from '../models.js'
import {Router} from "express";


export const ticketRouter = Router();

ticketRouter.get('/api/tickets', getAll);
ticketRouter.post('/api/tickets', add);
ticketRouter.delete('/api/tickets/:id', remove);


export async function add(req, res){
    let ticketsToAdd = getTicketsToAdd(req.body);

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

    res.json({
        message: `Added ${ticketsToAdd.length} tickets`
    }).status(200);
}

export async function getAll(req, res){
    let tickets = await TicketModel.find(req.query);
    res.json(tickets).status(200);
}

export async function remove(req, res){
    let tickets = await TicketModel.remove({
        _id: req.params.id
    });
    res.json(tickets).status(200);
}

function getTicketsToAdd(addRequest){
    let res = [];

    addRequest.businessAmount = Number(addRequest.businessAmount);
    addRequest.businessCost = Number(addRequest.businessCost);
    addRequest.economyAmount = Number(addRequest.economyAmount);
    addRequest.economyCost = Number(addRequest.economyCost);

    for (let i = 0; i < addRequest.businessAmount; i++) {
        res.push({
            from: addRequest.from,
            to: addRequest.to,
            fromDate: addRequest.fromDate,
            toDate: addRequest.toDate,
            cost: addRequest.businessCost,
            class: "Бизнес"
        });
    }

    for (let i = 0; i < addRequest.economyAmount; i++) {
        res.push({
            from: addRequest.from,
            to: addRequest.to,
            fromDate: addRequest.fromDate,
            toDate: addRequest.toDate,
            cost: addRequest.economyCost,
            class: "Эконом"
        });
    }

    return res;
}
