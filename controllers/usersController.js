import {UserModel} from '../models.js'

export async function login (req, res) {
    let email = req.body.email,
        password = req.body.password;

    if (!email || !password){
        return res.json({message: 'No email or password!'}).status(400);
    }

    let existing = await UserModel.find({email: email});
    if (existing.length !== 1){
        return res.json({message : 'No such user!'}).status(400);
    }

    let user = existing[0];
    if (user.password !== password){
        return res.json({message: 'Wrong password!'}).status(400);
    }

    res.cookie('email', email);
    if (user.isAdmin){
        res.send('/admin.html');
    }
    else{
        res.send('/user.html');
    }
}

export async function signIn (req, res) {
    let email = req.body.email,
        password = req.body.password;
    if (!email || !password){
        return res.json({message: 'No email or password'}).status(400);
    }

    let existing = await UserModel.find({email: email});
    if (existing.length !== 0){
        return res.json({message : 'User already exists'}).status(400);
    }

    let newUser = new UserModel({
        email: email,
        password: password,
        money: 0,
        isAdmin: false,
        tickets: []
    });

    let saveResult = await newUser.save();
    if (saveResult.errors !== undefined){
        return res.json({
            message: 'Can not create new user',
            errors: saveResult.errors
        }).status(500);
    }

    res.cookie('email', email).status(200);
}

export async function info (req, res) {
    let email = req.params.email;
    let existing = await UserModel.find({email: email});
    let user = existing[0];

    res.json(user).status(200);
}

export async function deleteUsersTicket (req, res) {
    let userId = req.params.userId;
    let ticketId = req.params.ticketId;

    let existing = await UserModel.find({_id: userId});
    let user = existing[0];

    user.tickets.removeIf((el) => el._id === ticketId);
    await user.save();

    res.status(200);
}

Array.prototype.removeIf = function(callback) {
    var i = 0;
    while (i < this.length) {
        if (callback(this[i], i)) {
            this.splice(i, 1);
        }
        else {
            ++i;
        }
    }
};