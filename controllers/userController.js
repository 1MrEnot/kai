import {Router} from "express";

import {User} from "../models/user.js";
import {ToDo} from "../models/todo.js";


export const UserRouter = Router();

UserRouter.get('/users/', getAll);
UserRouter.get('/users/:username', get);
UserRouter.post('/users/:username', create);
UserRouter.delete('/users/:username', remove);


async function getAll (req, res){
    let users = await User.find({});

    res.json(users).sendStatus(200);
}

async function get (req, res){
    res.sendFile('index.html');
}

async function create (req, res){
    let existing = await getUserByName(req.params.username);
    if (existing){
        return res.json({
            message: "User already exists"
        }).sendStatus(500);
    }

    let user = new User({
        username: req.params.username
    });
    await user.save();

    res.sendStatus(200);
}

async function remove (req, res){
    let userResult = await User.find({username: req.params.username || ""});
    if (userResult.length === 0){
        return  res.json({
            message: `No user with ${req.params.username} username`
        }).sendStatus(400);
    }
    let user = userResult[0];

    await ToDo.deleteMany({owner: user._id});
    await User.findByIdAndDelete(user._id);

    return res.status(200);
}

export async function getUserByName(userName){
    userName = userName || "";
    let userResult = await User.find({username: userName});
    if (userResult.length === 0){
        return null;
    }
    return userResult[0];
}