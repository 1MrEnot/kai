import {TicketModel, UserModel} from '../models.js'
import {Router} from "express";

export const userRouter = Router();

userRouter.get('/api/users/:email', info);

userRouter.post('/api/signIn', signIn);
userRouter.post('/api/login', login);
userRouter.post('/api/users/:userId/tickets/:ticketId', buyTicket);

userRouter.put('/api/users/:userId/money/:money', resetMoney)

userRouter.delete('/api/users/:userId/tickets/:ticketId', deleteUsersTicket)
userRouter.delete('/api/users/:userId/', deleteUser)


async function login (req, res) {
    let email = req.body.email,
        password = req.body.password;

    if (!email || !password){
        res.json({message: 'Не введён email или пароль!'}).status(400);
        return;
    }

    let existing = await UserModel.find({email: email});
    if (existing.length !== 1){
        res.json({message : 'Нет такого пользователя!'}).status(400);
        return;
    }

    let user = existing[0];
    if (user.password !== password){
        res.json({message: 'Неверный пароль!'}).status(400);
        return;
    }

    res.cookie('email', email);
    if (user.isAdmin){
        res.send('/admin.html');
    }
    else{
        res.send('/user.html');
    }
}

async function signIn (req, res) {
    let email = req.body.email,
        password = req.body.password;
    if (!email || !password){
        return res.json({message: 'Не введён email или пароль!'}).sendStatus(400);
    }

    let existing = await UserModel.find({email: email});
    if (existing.length !== 0){
        return res.json({message : 'Пользователь уже существует'}).sendStatus(400);
    }

    let newUser = new UserModel({
        email: email,
        password: password,
        money: 0,
        isAdmin: false,
        tickets: []
    });

    let saveResult = await newUser.save();
    if (saveResult.errors){
        res.json({
            message: 'Не удалось создать пользователя',
            errors: saveResult.errors
        }).sendStatus(500);
        return;
    }

    res.cookie('email', email).sendStatus(200);
}

async function info (req, res) {
    let email = req.params.email;
    let user = await UserModel.findOne({email: email});

    res.json(user).sendStatus(200);
}

async function deleteUsersTicket (req, res) {
    let userId = req.params.userId;
    let ticketId = req.params.ticketId;

    let user = await UserModel.findById(userId);

    user.tickets.pull(ticketId);
    await user.save();

    res.sendStatus(200);
}

async function deleteUser (req, res) {
    let userId = req.params.userId;
    await UserModel.findByIdAndDelete(userId);
    res.sendStatus(200);
}


async function buyTicket (req, res) {
    let userId = req.params.userId;
    let ticketId = req.params.ticketId;

    let user = await UserModel.findById(userId);
    let ticket = await TicketModel.findById(ticketId);

    if(user.money < ticket.cost){
        res.json({message: "Not enough money"}).sendStatus(400);
        return;
    }

    user.tickets.push(ticket);
    user.money -= ticket.cost;
    await user.save();
    await TicketModel.findByIdAndDelete(ticketId);

    res.sendStatus(200);
}

async function resetMoney (req, res) {
    let user = await UserModel.findById(req.params.userId);
    user.money = Number(req.params.money) || 0;
    await user.save();

    res.sendStatus(200);
}