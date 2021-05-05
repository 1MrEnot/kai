import express from 'express';
import path from 'path';
import router from './routers/ticketsRouter.js';

const ROOT = path.resolve(path.resolve(), 'static');
const PORT = 3000;
const app = express();

app.use(express.static(ROOT));
app.use('/api', router);

app.listen(PORT, () => {
    console.log(`App started on port ${PORT}...`);
    console.log(`Root is ${ROOT}`);
});
