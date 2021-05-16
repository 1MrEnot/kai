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

    if (existing[0].password !== password){
        return res.json({message: 'Wrong password!'}).status(400);
    }

    res.cookie('email', email).redirect(301, '/user.html');
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

    res.cookie('email', email).sendStatus(200);
}
