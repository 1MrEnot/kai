const flights = [
    {
        "from": "Казань",
        "to": "Москва",
        "fromDate": new Date(2021, 2, 12, 14, 30),
        "toDate": new Date(2021, 2, 12, 16, 30),
        "cost": 7500,
        "class": "Бизнес"
    },
    {
        "from": "Санкт-Петербург",
        "to": "Казань",
        "fromDate": new Date(2021, 2, 12, 23, 30),
        "toDate": new Date(2021, 2, 13, 0, 30),
        "cost": 1500,
        "class": "Эконом"
    },
];

export async function getAll (req, res){
    res.json(flights);
}

export async function search(req, res){
    let flights = [
        {
            from: req.query.from,
            to:  req.query.to,
            fromDate: new Date(2021, 2, 12, 14, 30),
            toDate: new Date(2021, 2, 12, 16, 30),
            cost: 7500,
            class: req.query.class
        }
    ];
    res.json(flights);
}