let counter = 0;

(function () { add(); })();

function add() {
    let div = document.createElement('div');

    div.id = 'column' + counter;
    div.className = 'form-group';
    div.innerHTML = `
            <hr>
            <h3 style="display: inline-block;">Type:</h3>
            <select required id="select`+ counter + `" name="select` + counter + `" style="color: #000" onchange="select('` + counter + `')">
                <option disabled selected value>Please select type</option>
                <option value="text">Text</option>
                <option value="number">Number</option>
                <option value="date">Date</option>
                <option value="password">Password</option>
                <option value="radio">Radio</option>
                <option value="checkbox">Checkbox</option>
                <option value="file">File</option>
                <option value="color">Color</option>
                <option value="time">Time</option>
                <option value="range">Range</option>
            </select>
            `;
    document.getElementById('main').appendChild(div);
    counter++;

    document.getElementById('numberOfSelects').value++;
}

function remove() {
    if (counter > 1) {
        document.getElementById('main').removeChild(document.getElementById('column' + --counter));
        document.getElementById('numberOfSelects').value--;
    }

}

function select(cnt) {
    let select = document.getElementById('select' + cnt);
    let type = select.options[select.selectedIndex].value;

    if (document.getElementById('selectDiv' + cnt) != null) {
        document.getElementById('column' + cnt).removeChild(document.getElementById('selectDiv' + cnt));
    }

    if (type == 'text' || type == 'number' || type == 'date' || type == 'password' || type == 'file' || type == 'color' || type == 'time') {
        let div = document.createElement('div');
        div.id = 'selectDiv' + cnt;
        div.name = 'selectDiv' + cnt;
        div.innerHTML = `
            <label class="control-label">Field name:</label>
            <input required class="form-control" type="text" name="input`+ cnt + `">`;
        document.getElementById('column' + cnt).appendChild(div);
    }

    if (type == 'radio') {
        let div = document.createElement('div');
        div.id = 'selectDiv' + cnt;
        div.name = 'selectDiv' + cnt;
        div.innerHTML = `<label>Radio group name:</label>
                             <input required name="radioGroupName` + cnt + `" type="text">
                             <label>Number of radio buttons:</label>
                             <input required id="radioNumber` + cnt + `" name="radioNumber` + cnt + `" type="number" min="1" max="10"  onKeyDown="return false" onchange="radioInputs(` + cnt + `)"> `;
        document.getElementById('column' + cnt).appendChild(div);
    }

    if (type == 'checkbox') {
        let div = document.createElement('div');
        div.id = 'selectDiv' + cnt;
        div.name = 'selectDiv' + cnt;
        div.innerHTML = `<label>Checkbox group name:</label>
                             <input required name="cboxGroupName` + cnt + `" type="text">
                             <label>Number of checkboxes:</label>
                             <input required id="cboxNumber` + cnt + `" name="cboxNumber` + cnt + `" type="number" min="1" max="10"  onKeyDown="return false" onchange="checkboxInputs(` + cnt + `)">`;
        document.getElementById('column' + cnt).appendChild(div);
    }

    if (type == 'range') {
        let div = document.createElement('div');
        div.id = 'selectDiv' + cnt;
        div.name = 'selectDiv' + cnt;
        div.innerHTML = `
                <label>Field name:</label>
                <input required type="text" id="rangeName`+ cnt + `" name="rangeName` + cnt + `">
                <label>Min number</label>
                <input required type="number" id="rangeMin`+ cnt + `" name="rangeMin` + cnt + `">
                <label>Max number</label>
                <input required type="number" id="rangeMax`+ cnt + `" name="rangeMax` + cnt + `">
             `;
        document.getElementById('column' + cnt).appendChild(div);
    }
}

function radioInputs(cnt) {
    let no = 0;
    let div = document.getElementById('selectDiv' + cnt);
    if (document.getElementById('radioDiv' + cnt) != null) {
        div.removeChild(document.getElementById('radioDiv' + cnt));
    }
    let number = document.getElementById('radioNumber' + cnt).value;

    let childDiv = document.createElement('div');
    childDiv.id = 'radioDiv' + cnt;
    childDiv.name = 'radioDiv' + cnt;
    for (let i = 0; i < number; +i++) {
        let input = document.createElement('input');
        input.name = 'radio' + cnt + 'input' + no;
        no++;
        input.type = 'text';
        input.required = true;
        let label = document.createElement('label');
        label.innerHTML = 'Radio button ' + no + ' name:';
        childDiv.appendChild(label);
        childDiv.appendChild(input);
        let br = document.createElement('br');
        childDiv.appendChild(br);
    }
    div.appendChild(childDiv);
}

function checkboxInputs(cnt) {
    let no = 0;
    let div = document.getElementById('selectDiv' + cnt);
    if (document.getElementById('cboxDiv' + cnt) != null) {
        div.removeChild(document.getElementById('cboxDiv' + cnt));
    }
    let number = document.getElementById('cboxNumber' + cnt).value;

    let childDiv = document.createElement('div');
    childDiv.id = 'cboxDiv' + cnt;
    childDiv.name = 'cboxDiv' + cnt;
    for (let i = 0; i < number; +i++) {
        let input = document.createElement('input');
        input.name = 'cbox' + cnt + 'input' + no;
        no++;
        input.type = 'text';
        input.required = true;
        let label = document.createElement('label');
        label.innerHTML = 'Checkbox ' + no + ' name:';
        childDiv.appendChild(label);
        childDiv.appendChild(input);
        let br = document.createElement('br');
        childDiv.appendChild(br);
    }
    div.appendChild(childDiv);
}

function preview() {
    var style = document.getElementById('stylePreview');
    style.innerHTML = document.getElementById('selectS').value;
}


$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

$(document).ready(function () {
    $(document).on("click", ".clsDetails", OpenModalPopUp);
});

function OpenModalPopUp() {
    var itemName = $(this).data("item");
    $("#myModal").modal();
}