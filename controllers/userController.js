import User from "../models/user";
import mongoose from "mongoose";

function index (req, res){
    User.find({}, function (err, result) {
        if (err !== null) {
            console.log(err);
            return;
        }
        
        if (result.length !== 0) {
            return;
        }

        console.log("Создание тестового пользователя...");
        let testUser = new User({
            "username":"usertest"
        });

        testUser.save((err, result) => {
            if (err) {
                console.log(err);
            } else {
                console.log("Тестовый пользователь сохранен");
            }
        });
    });        
};

function get (req, res){
    console.log("Get");
    res.send(200);
};

function create (req, res){
    console.log("Index");
    res.send(200);
};

function update (req, res){
    console.log("Index");
    res.send(200);
};

function remove (req, res){
    console.log("Index");
    res.send(200);
};


const UserController = {
    "index": index,
    "get": get,
    "create": create,
    "update": update,
    "remove": remove
};

module.exports = UserController;