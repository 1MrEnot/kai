import { Router } from 'express';
import {signIn, login, info, deleteUsersTicket} from '../controllers/usersController.js'

const userRouter = Router();

userRouter.get('/user/:email', info);

userRouter.post('/signIn', signIn);
userRouter.post('/login', login);

userRouter.delete('/user/:userId/ticket/:ticketId', deleteUsersTicket)

export default userRouter;