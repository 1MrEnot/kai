import { Router } from 'express';
import { getAll, add, search } from '../controllers/ticketsController.js';

const ticketRouter = Router();

ticketRouter.get('/tickets', getAll);
ticketRouter.get('/ticketSearch', search);

ticketRouter.post('/tickets', add);


export default ticketRouter;