let seed = 1;

const colorStart = 8;
const colorWidth = 32;

const darkLower = colorStart
const darkUpper = darkLower + colorWidth;

const lightUpper = 255 - colorStart;
const lightLower = lightUpper - colorWidth;


function random() {
    let x = Math.sin(seed++) * 10000;
    return x - Math.floor(x);
}

function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(random() * (max - min)) + min; //Максимум не включается, минимум включается
}

function getPositions(minX, maxX, minY, maxY, count, delta){
    let res = [];

    const rowLengths = [Math.ceil(count/2), Math.floor(count/2)]
    const midX = (maxX - minX) / 2;
    const midY = (maxY - minY) / 2;

    for (let rowIndex = 0; rowIndex < rowLengths.length; rowIndex++) {
        const dy = delta * (rowIndex - 1/rowLengths.length);
        const y = midY + dy;

        for (let i = 0; i < rowLengths[rowIndex]; i++) {
            let dx = delta * (i - rowLengths[rowIndex]/2);
            const x = midX + dx;

            res.push({
                x: x,
                y: y,
            })
        }
    }

    return res;
}

function getDark(){
    return {
        r: getRandomInt(darkLower, darkUpper),
        g: getRandomInt(darkLower, darkUpper),
        b: getRandomInt(darkLower, darkUpper),
    }
}

function getLight(){
    return {
        r: getRandomInt(lightLower, lightUpper),
        g: getRandomInt(lightLower, lightUpper),
        b: getRandomInt(lightLower, lightUpper),
    }
}

function pad(num, size) {
    let s = "000000000" + num;
    return s.substr(s.length-size);
}

function colorToHex(colorStruct){
    let r = pad(colorStruct.r.toString(16), 2);
    let g = pad(colorStruct.g.toString(16), 2);
    let b = pad(colorStruct.b.toString(16), 2);

    return "#" + r + g + b;
}

function shuffle(array) {
    for (let i = array.length - 1; i > 0; i--) {
        let j = Math.floor(random() * (i + 1));
        let temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    return array;
}