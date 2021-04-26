import express from 'express'
import path from 'path';
import { fileURLToPath } from 'url';
import mongoose from 'mongoose';
const userController = require("./controllers/userController.js");


const port = 3000;
const mongoUrl = "mongodb://localhost:27017/amazeriffic";

const app = express();
const __dirname = path.dirname(fileURLToPath(import.meta.url));
app.use(express.static(__dirname + "/client"))
app.use(express.urlencoded())

mongoose.connect(mongoUrl);

app.get("/users.json", userController.index);


app.get('/todos.json', (req, res) => {
    ToDo.find({}, (err, toDos) => {
        if (err !== null) {
            console.log(err);
        }
        else {
            res.json(toDos);
        }
    })
});

app.post('/todos.json', (req, res) => {
    console.log(req.body);
    var newToDo = new ToDo({
       "description":req.body.description,
       "tags":req.body.tags
    });
    
   newToDo.save(function (err, result) {
      if (err !== null) {
         console.log(err);
         res.send("ERROR");
      } else {
         ToDo.find({}, function (err, result) {
            if (err !== null){
               res.send("ERROR");
            }
         res.json(result);
         });
      }
   });
});

app.listen(port, () => {
  console.log(`Example app listening at http://localhost:${port}`);
});