import express from 'express';
import path from 'path';

const ROOT = path.resolve(path.resolve(), 'static');
const PORT = 3000;
const app = express();

console.log(ROOT);
app.use(express.static(ROOT));


app.get('/user', (req, res) => {
    res.sendFile('index.html');
});

app.get('/', (req, res) => {
    res.sendFile('index.html');
});


app.listen(PORT, () => {
    console.log(`App started on port ${PORT}...`);
});
