import express from 'express'
import path from 'path';
import { fileURLToPath } from 'url';
import mongoose from 'mongoose';

import {TodoRouter} from './controllers/todosController.js';
import {UserRouter} from './controllers/userController.js';


const port = 3000;
const CONNECTION_STRING = 'mongodb+srv://admin:admin@cluster0.qt8ou.mongodb.net/amazeriffic?retryWrites=true&w=majority';
// const CONNECTION_STRING = "mongodb://localhost:27017/amazeriffic";

const app = express();
const ROOT = path.resolve(path.resolve(), 'client');

app.use(express.static(ROOT));
app.use(express.urlencoded({ extended: true }))

app.use(TodoRouter);
app.use(UserRouter);

app.listen(port, "localhost", async () => {
    await mongoose.connect(CONNECTION_STRING, {
        useNewUrlParser: true,
        useUnifiedTopology: true
    });

    console.log(`Listening at ${port}`);
});