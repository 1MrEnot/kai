import { Router } from 'express';
import {signIn, login} from '../controllers/usersController.js'

const userRouter = Router();

userRouter.post('/signIn', signIn);
userRouter.post('/login', login);

export default userRouter;