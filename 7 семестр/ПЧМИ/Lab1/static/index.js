console.log("Простите за вёрстку, я не фронтендщик")
console.log("Но бэк я тоже не покажу")

const id = Math.floor(Math.random() * 10000);
const symbols = ["★", "⚑", "✓", "♥", "•", "❞", "×", "＄", "∞", "♣"];

const optionCount = 10;
const shownCount = 6;

const stageCount = 6;
let stage = 1;

const timeout = 750;

let currentSelection = [];
let lastCreated = [];
let lastShown = [];

let canvas;
let ctx;
const canvasOffset = 100;
const step = 100;

const nextButton = $("#next");
const applyButton = $("#apply");


function showHelp(){
    alert(
    "Нажми на кнопку Next. На секунду на поле слева появятся различные фигуры." +
    " Потом они пропадут и справа появится 10 фигур, среди которых будут те, которые только что показывались." +
    " Выбери те, которые запомнил и нажми Apply." +
    " Повтори так 6 раз");
}

function getNumbers(amount){
    let res = [];

    for (let i = 1; i < amount + 1; i++) {
        let value = i;
        let color = random() > 0.5
            ? colorToHex(getDark())
            : colorToHex(getLight());
        res.push({
            value: value,
            color: color,
        });
    }

    return res;
}

function getSymbols(amount){
    let res = [];

    for (let i = 0; i < amount; i++) {
        res.push({
            value: random() > 0.5
                ? symbols[i]
                : i.toString(),
            color: "#000000"
        })
    }

    return res;
}

function onNextButtonClick() {
    if (stage === stageCount){
        alert("молодец, спасиба, дальше играться не надо (обновишь страницу - вычислю по ip)");
        return;
    }

    nextButton.prop('disabled', true);

    if (stage % 2)
        checkColors();
    else
        checkShapes();

    stage += 1;
    setTimeout(() => {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        drawSelection(125, lastCreated, currentSelection);
        applyButton.prop('disabled', false);
    }, timeout);
}

function onApplyButtonClick(){
    if (currentSelection.length === 0){
        alert("Выбери хоть что-то!");
        return;
    }

    const req = {
        id: id,
        shown: lastShown,
        created: lastCreated,
        correct: currentSelection.filter(e => lastShown.indexOf(e) !== -1),
    };

    $.ajax({
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(req),
        dataType: 'json',
        url: '/results',
        success: function () {
            nextButton.prop('disabled', false);
            applyButton.prop('disabled', true);
            clearSelection();
            currentSelection = [];
        }});
}

function drawSomething(elementFactory){
    lastCreated = shuffle(elementFactory(optionCount));
    lastShown = [];
    let positions = getPositions(
        canvasOffset, canvas.width - canvasOffset,
        canvasOffset, canvas.height - canvasOffset,
        shownCount, step
    );

    for (let i = 0; i < shownCount; i++) {
        let el = lastCreated[i];
        let pos = positions[i];
        ctx.fillStyle = el.color;
        ctx.fillText(el.value, pos.x, pos.y);
        lastShown.push(el);
    }

    lastShown = shuffle(lastShown);
}

function checkColors(){
    drawSomething(getNumbers);
}

function checkShapes(){
    drawSomething(getSymbols);
}

$(document).ready(() => {
    showHelp();
    applyButton.prop('disabled', true);
    canvas = document.getElementById('canvas');
    ctx = canvas.getContext('2d');
    ctx.font = "48px serif";
});

nextButton.on("click", onNextButtonClick);
applyButton.on("click", onApplyButtonClick);