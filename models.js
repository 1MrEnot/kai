import mongoose from 'mongoose';

const ticketName = 'Ticket';
const userName = 'User';

const ticketSchema = new mongoose.Schema({
    from: String,
    to: String,
    dateFrom: Date,
    dateTo: Date,
    cost: Number,
    class: String
});

const userSchema = new mongoose.Schema({
    email: String,
    password: String,
    money: Number,
    isAdmin: Boolean,
    tickets: [{ type: mongoose.Schema.Types.ObjectId, ref: ticketName}]
});

export const TicketModel = mongoose.model(ticketName, ticketSchema);
export const UserModel = mongoose.model(userName, userSchema);
