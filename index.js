import express from 'express'
import path from 'path';
import { fileURLToPath } from 'url';

const app = express();
const port = 3000;
const __dirname = path.dirname(fileURLToPath(import.meta.url));

app.use(express.static(__dirname + "/client"))
app.use(express.urlencoded())

let toDos = [
    {
       "description":"Купить продукты",
       "tags":[
          "шопинг",
          "рутина"
       ]
    },
    {
       "description":"Сделать несколько новых задач",
       "tags":[
          "писательство",
          "работа"
       ]
    },
    {
       "description":"Подготовиться к лекции в понедельник",
       "tags":[
          "работа",
          "преподавание"
       ]
    },
    {
       "description":"Ответить на электронные письма",
       "tags":[
          "работа"
       ]
    },
    {
       "description":"Вывести Грейси на прогулку в парк",
       "tags":[
          "рутина",
          "питомцы"
       ]
    },
    {
       "description":"Закончить писать книгу",
       "tags":[
          "писательство",
          "работа"
       ]
    }
];

app.get('/todos.json', (req, res) => {
    res.json(toDos);
});

app.post('/todos.json', (req, res) => {
    toDos.push(req.body);
    console.log(`Получена новая задача: ${req}`);
    res.json({'message': 'Данные получены'});
})

app.listen(port, () => {
  console.log(`Example app listening at http://localhost:${port}`);
});