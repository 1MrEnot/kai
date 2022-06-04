import express, {raw} from 'express';
import path from 'path';
import mongoose from 'mongoose';

import {ticketRouter} from './controllers/ticketsController.js';
import {userRouter} from './controllers/usersController.js';

const CONNECTION_STRING = 'mongodb+srv://admin:admin@cluster0.qt8ou.mongodb.net/planerDb?retryWrites=true&w=majority';
// const CONNECTION_STRING = 'mongodb://127.0.0.1:27017/planer';
const ROOT = path.resolve(path.resolve(), 'static');
const PORT = 3000;
const app = express();

app.use(express.static(ROOT));
app.use(express.urlencoded({ extended: true }))

app.use(userRouter);
app.use(ticketRouter);

app.listen(PORT, async () => {
    await mongoose.connect(CONNECTION_STRING, {
        useNewUrlParser: true,
        useUnifiedTopology: true
    });

    console.log(`App started on port ${PORT}...`);
    console.log(`Root is ${ROOT}`);
});