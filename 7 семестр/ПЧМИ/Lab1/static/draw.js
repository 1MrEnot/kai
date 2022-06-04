const selectContainer = $("#res-select");

function clearSelection(){
    selectContainer.empty();
}

function drawSelection(size, data, selected){
    clearSelection();

    for (let figure of shuffle(data)) {
       let selectionOption = $(
            " <div class='col bordered'>\n" +
            "        <label>\n" +
            "            <input type='checkbox'>\n" +
            `                <canvas width='${size}' height='${size}'></canvas>\n` +
            "        </label>\n" +
            "    </div>");

       selectContainer.append(selectionOption);

       const canvas = selectionOption.find("canvas")[0];
       const ctx = canvas.getContext('2d');
       ctx.font = "48px serif";
       ctx.fillStyle = figure.color;
       ctx.fillText(figure.value, 45, 75);

       const checkBox = selectionOption.find("input");
       checkBox.change(() => {
           const index = selected.indexOf(figure);
           if (index === -1){
               selected.push(figure);
           }
           else{
               selected.splice(index, 1);
           }
       });
    }
}