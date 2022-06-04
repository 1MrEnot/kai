import {TicketModel} from '../models.js'
import {Router} from "express";


export const ticketRouter = Router();

ticketRouter.get('/api/tickets', getAll);
ticketRouter.post('/api/tickets', add);
ticketRouter.delete('/api/tickets/:id', remove);


export async function add(req, res){
    let ticketsToAdd = getTicketsToAdd(req.body);

    for (const t of ticketsToAdd) {
        t.fromDate = new Date(t.fromDate);
        t.toDate = new Date(t.toDate);

        let ticketModel = new TicketModel(t);
        let addResult = await ticketModel.save();

        if (addResult.errors){
            return res.json({
                message: 'Не удалось добавить',
                errors: addResult.errors
            }).status(500);
        }
    }

    res.json({
        message: `Добавлено ${ticketsToAdd.length} билетов`
    }).status(200);
}

export async function getAll(req, res){
    const query = req.query;

    if (query.date){
        const date = new Date(query.date);
        const lowerDate = new Date(query.date);
        lowerDate.setDate(lowerDate.getDate()+1);
        query.fromDate = {
            $gte: date,
            $lte: lowerDate,
        }
        delete query.date;
    }

    const tickets = await TicketModel.find(query);
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
