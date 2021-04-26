import mongoose from 'mongoose';
import ToDo from './todo';

const UserSchema = mongoose.Schema({
    id: String,
    username: String,
    todos: ToDo
});
export const User = mongoose.model("User", UserSchema);
