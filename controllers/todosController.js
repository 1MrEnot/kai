import {Router} from "express";

import {getUserByName} from './userController.js'
import {User} from '../models/user.js';
import {ToDo} from '../models/todo.js';


export const TodoRouter = Router();

TodoRouter.get('/todos', getAll);
TodoRouter.get('/users/:username/todos', get);
TodoRouter.post('/users/:username/todos', create);
TodoRouter.put('/users/:username/todos/:id', update);
TodoRouter.delete('/users/:username/todos/:id', remove);


async function getAll (req, res){
    let toDoResult = await ToDo.find();
    res.json(toDoResult).status(200);
}

async function get (req, res){
    let user = await getUserByName(req.params.username);
    if (!user){
        return res.json({
            message: `No user with ${req.params.username} username`
        }).sendStatus(400);
    }

    let toDoResult = await ToDo.find({owner: user._id});
    await res.json(toDoResult).sendStatus(200);
}

async function create (req, res){
    let userResult = await User.find({username: req.params.username || ""});
    if (userResult.length === 0){
        return res.json({
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
    let userResult = await User.find({username: req.params.username || ""});
    if (userResult.length === 0){
        return res.json({
            message: `No user with ${req.params.username} username`
        }).sendStatus(400);
    }

    await ToDo.findByIdAndUpdate(req.params.id, req.body);

    await res.sendStatus(200);
}

async function remove (req, res){
    let userResult = await User.find({username: req.params.username || ""});
    if (userResult.length === 0){
        await res.json({
            message: `No user with ${req.params.username} username`
        }).sendStatus(400);
    }

    await ToDo.findByIdAndDelete(req.params.id);

    await res.sendStatus(200);
}