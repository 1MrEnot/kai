import express, {raw} from 'express';
import path from 'path';
import mongoose from 'mongoose';

import ticketRouter from './routers/ticketsRouter.js';
import userRouter from './routers/usersRouter.js';

// const CONNECTION_STRING = 'mongodb+srv://admin:admin@cluster0.qt8ou.mongodb.net/planerDb?retryWrites=true&w=majority';
const CONNECTION_STRING = 'mongodb://127.0.0.1:27017/planer';
const ROOT = path.resolve(path.resolve(), 'static');
const PORT = 3000;
const app = express();

app.use(express.static(ROOT));
app.use(express.urlencoded({ extended: true }))
app.use((req, res, next) => {
    req.email = req.header("email");
    next();
})
app.use('/api', ticketRouter);
app.use('/api', userRouter);

app.listen(PORT, async () => {
    await mongoose.connect(CONNECTION_STRING, { useNewUrlParser: true , useUnifiedTopology: true });

    console.log(`App started on port ${PORT}...`);
    console.log(`Root is ${ROOT}`);
});