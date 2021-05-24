import mongoose from 'mongoose';


const UserSchema = new mongoose.Schema({
    username: String
});

export const UserName = "User"
export const User = mongoose.model(UserName, UserSchema);
