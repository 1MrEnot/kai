import { Router } from 'express';
import { getAll, search } from '../controllers/ticketsController.js';

const ticketRouter = Router();

ticketRouter.get('/tickets', getAll);
ticketRouter.get('/ticketSearch', search);

export default ticketRouter;