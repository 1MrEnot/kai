import mongoose from 'mongoose';


const ToDoSchema = new mongoose.Schema({
    description: String,
    tags: [String],
    owner: { type: mongoose.Schema.Types.ObjectId, ref: "User"}

});

export const TodoName = "ToDo";
export const ToDo = mongoose.model(TodoName, ToDoSchema);
