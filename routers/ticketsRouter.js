import {Router} from 'express';
import {getAll, search} from '../controllers/ticketsController.js';

const router = Router();

router.get('/tickets', getAll);
router.get('/ticketSearch', search);

export default router;