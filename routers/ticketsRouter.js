import { Router } from 'express';
import { getAll, add, search, remove } from '../controllers/ticketsController.js';

const ticketRouter = Router();
const tickets = '/tickets'


ticketRouter.get(tickets, getAll);
ticketRouter.get('/ticketSearch', search);

ticketRouter.post(tickets, add);

ticketRouter.delete('/tickets/:id', remove);


export default ticketRouter;