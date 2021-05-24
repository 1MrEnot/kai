import {Router} from "express";

import {getUserByName} from './userController.js'
import {User} from '../models/user.js';
import {ToDo} from '../models/todo.js';


export const TodoRouter = Router();

TodoRouter.get('/users/:username/todos', get);
TodoRouter.post('/users/:username/todos', create);
TodoRouter.put('/users/:username/todos/:id', update);
TodoRouter.delete('/users/:username/todos/:id', remove);

async function get (req, res){
    let user = getUserByName(req.params.username);
    if (!user){
        await res.json({message: `No user with ${req.params.username} username`}).sendStatus(400);
        return;
    }

    let toDoResult = await ToDo.find({owner: user._id});
    res.send(toDoResult).status(200);
}

async function create (req, res){
    let userResult = await User.find({username: req.params.username || ""});
    if (userResult.length === 0){
        await res.json({
            message: `No user with ${req.params.username} username`
        }).sendStatus(400);
    }
    let user = userResult[0];

    let toDo = new ToDo({
        owner: user._id,
        description: req.body.description,
        tags: req.body.tags
    });
    await toDo.save();

    await res.sendStatus(200);
}

async function update (req, res){
    console.log("Update todos");
    res.send(200);
}

async function remove (req, res){
    let result = await User.find({username: req.params.username});

    if (result.length === 0){
        res.status(400);
    }
    else{
        let user = result[0];
        let newToDo = new ToDo(req.body);
        user.toDos.push(newToDo)

        await user.save();

        res.send(newToDo).status(200);
    }

}

