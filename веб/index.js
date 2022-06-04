import express from 'express'
import path from 'path';
import bodyParser from 'body-parser';
import mongoose from 'mongoose';

import {TodoRouter} from './controllers/todosController.js';
import {UserRouter} from './controllers/userController.js';


const port = 3000;
const CONNECTION_STRING = 'mongodb+srv://admin:admin@cluster0.qt8ou.mongodb.net/amazeriffic?retryWrites=true&w=majority';
// const CONNECTION_STRING = "mongodb://localhost:27017/amazeriffic";
const ROOT = path.resolve(path.resolve(), 'client');

const app = express();

app.use(express.static(ROOT));
app.use(bodyParser.urlencoded({ extended: true }))
app.use(bodyParser.json());


app.use(TodoRouter);
app.use(UserRouter);

app.listen(port, "localhost", async () => {
    await mongoose.connect(CONNECTION_STRING, {
        useNewUrlParser: true,
        useUnifiedTopology: true
    });

    console.log(`Listening at ${port}; Root is ${ROOT}`);
});