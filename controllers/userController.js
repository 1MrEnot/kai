import {Router} from "express";

import {User} from "../models/user.js";


export const UserRouter = Router();

UserRouter.get('/users/:username', get);
UserRouter.post('/users/:username', create);
UserRouter.put('/users/:username', update);
UserRouter.delete('/users/:username', remove);

async function get (req, res){
    console.log("Get");
    res.send(200);
}

async function create (req, res){
    let userName = req.params.username || "";
    let existing = await getUserByName(userName);

    if (existing){
        return res.json({
            message: "User already exists"
        }).status(500);
    }

    let user = new User({
        username: req.params.username
    });
    await user.save();

    res.sendStatus(200);
}

async function update (req, res){
    console.log("Update");
    res.send(200);
}

async function remove (req, res){
    console.log("Remove");
    res.send(200);
}

export async function getUserByName(userName){
    userName = userName || "";
    let userResult = await User.find({username: userName});
    if (userResult.length === 0){
        return null;
    }
    return  userResult[0];
}