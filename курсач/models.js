import mongoose from 'mongoose';

const ticketName = 'Ticket';
const userName = 'User';

const ticketSchema = new mongoose.Schema({
    from: String,
    to: String,
    fromDate: Date,
    toDate: Date,
    cost: Number,
    class: String
});

const userSchema = new mongoose.Schema({
    email: String,
    password: String,
    money: Number,
    isAdmin: Boolean,
    tickets: [ticketSchema]
});

export const TicketModel = mongoose.model(ticketName, ticketSchema);
export const UserModel = mongoose.model(userName, userSchema);
