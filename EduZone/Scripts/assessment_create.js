var Q_Add = document.getElementById("add_question")
var Num_Q = 1;
var Num_O = 0;
$(function () {
    Num_Q = parseInt(document.getElementById("NQ").textContent);
})
function add_Q() {
    var Q = `
    <div class='question row' id = "Q${Num_Q}">
     <div class="col-11">
        <!-- Q-H -->
        <div class="input-group mb-3">
            <div class="input-group-text">
                <label class='m-0 question_number' id="QNtxt${Num_Q}">Q${Num_Q}</label>
            </div>
            <textarea id="QInput${Num_Q}" name="Q${Num_Q}" class="form-control question_title" required placeholder='Enter the question title...' ></textarea>
        </div>    
        <!-- Q-H -->
        <!-- O-n-  -->
        <div class='m-3'>
            <div id = "Q${Num_Q}O">
                <input type = "text" style="display: none;" value = "2" id="ON${Num_Q}">                                                            
                <div class="input-group mb-3 option" id = "O${Num_Q}0">
                    <div class="input-group-text">
                        <input class=" mt-0 p-0" value="0" id = "Q${Num_Q}R0" required type="radio" name="QR${Num_Q}">
                    </div>
                    <input id="Q${Num_Q}I0" name="Q${Num_Q}I0" class="form-control option_title" type="text" required placeholder='Enter the option title...'>
                </div>
                <div class="input-group mb-3 option" id = "O${Num_Q}1">
                    <div class="input-group-text">
                        <input class=" mt-0 p-0" value="1" id = "Q${Num_Q}R1" required type="radio" name="QR${Num_Q}">
                    </div>
                    <input id="Q${Num_Q}I1" name="Q${Num_Q}I1" class="form-control option_title" type="text" required placeholder='Enter the option title...'>
                </div>
            </div>
            <button type="button" id="btn_add${Num_Q}" onclick="add_option(${Num_Q})"  class="btn btn-sm btn-outline-info px-2 me-2">Add Option</button>
            <button type="button" id="btn_sub${Num_Q}" onclick="sub_option(${Num_Q})"  class="btn btn-sm btn-outline-info px-2"> Delete last Option </button>
            <div class="mx-3 d-inline">
                <label>Points</label>
                <input type="number" value="1" name="QPI${Num_Q}" id="QPI${Num_Q}" min="0" max="100" >
            </div>
        </div>
        <!-- O-n-  -->
    </div>
    <div class="col-1">
        <button class="btn" id = "Del_Q${Num_Q}" onclick="del_question(${Num_Q})">
            <i class="fa-solid fa-delete-left" style="color: #ff1100;"></i>
        </button>
    </div>
    <hr>
</div>
`
    Num_Q = Num_Q + 1;
    $("#questions_id").append(Q);
    document.getElementById("NQ").textContent = Num_Q;
    document.getElementById("N_question").value = Num_Q;

}

function del_question(id) {
    var idx = "Q" + id;
    document.getElementById(idx).remove()
    var val = 0;
    console.log(typeof (id))
    for (var i = 0; i < Num_Q; i++) {
        if (i !== parseInt(id)) {
            console.log(i + "enter")

            //div all
            var item = document.getElementById("Q" + i)
            item.setAttribute('id', "Q" + (parseInt(val)));
            //lable
            idx = "QNtxt" + i;
            item = document.getElementById(idx)
            item.textContent = "Q" + (parseInt(val))
            item.setAttribute('id', "QNtxt" + (parseInt(val)));
            // delete item
            item = document.getElementById("Del_Q" + i)
            item.setAttribute('id', "Del_Q" + (parseInt(val)));
            item.setAttribute('onclick', "del_question(" + (parseInt(val)) + ")");
            //Q Input
            item = document.getElementById("QInput" + i)
            item.setAttribute('id', "QInput" + (parseInt(val)));
            item.setAttribute('name', "Q" + (parseInt(val)));
            //Q Input Point
            item = document.getElementById("QPI" + i)
            item.setAttribute('id', "QPI" + (parseInt(val)));
            item.setAttribute('name', "QPI" + (parseInt(val)));
            //btn add
            item = document.getElementById("btn_add" + i)
            item.setAttribute('id', "btn_add" + parseInt(val));
            item.setAttribute('onclick', "add_option(" + (parseInt(val)) + ")");
            //btn sub
            item = document.getElementById("btn_sub" + i)
            item.setAttribute('id', "btn_sub" + parseInt(val));
            item.setAttribute('onclick', "sub_option(" + (parseInt(val)) + ")");
            //Q Option div
            item = document.getElementById("Q" + i + "O")
            item.setAttribute('id', "Q" + parseInt(val) + "O");
            //Q Option hidden input
            var ON = document.getElementById("ON" + i).value;
            item = document.getElementById("ON" + i)
            item.setAttribute('id', "ON" + parseInt(val));

            //Q Option name
            for (var j = 0; j < parseInt(ON); j++) {
                // radio
                item = document.getElementById("Q" + i + "R" + j)
                item.setAttribute('id', "Q" + val + "R" + j);
                item.setAttribute('name', "QR" + val);
                // input
                item = document.getElementById("Q" + i + "I" + j)
                item.setAttribute('id', "Q" + val + "I" + j);
                item.setAttribute('name', "Q" + val + "I" + j);
                // div
                item = document.getElementById("O" + i + j)
                item.setAttribute('id', "O" + val + j);
            }
            val++;
        }
        console.log(i + "end")
    }
    Num_Q--;
    document.getElementById("NQ").textContent = Num_Q;
    document.getElementById("N_question").value = Num_Q;
};

function add_option(valx) {
    var ON = document.getElementById("ON" + valx).value;
    console.log(ON)
    if (parseInt(ON) < 4) {
        var Option = `
        <div class="input-group mb-3 option" id = "O${valx}${parseInt(ON)}">
                <div class="input-group-text">
                 <input class=" mt-0 p-0" value="${parseInt(ON)}" id = "Q${valx}R${parseInt(ON)}" required type="radio" name="QR${valx}">
                </div>
            <input id="Q${valx}I${parseInt(ON)}"  name="Q${valx}I${parseInt(ON)}" class="form-control option_title" type="text" required placeholder='Enter the option title...'>
        </div>`;
        const idO = "Q" + valx + "O";
        $(`#Q${valx}O`).append(Option);
        ON = parseInt(ON) + 1;
        document.getElementById("ON" + valx).value = parseInt(ON);
    }

}
function sub_option(valx) {
    var ON = document.getElementById("ON" + valx).value;
    console.log(ON)
    if (parseInt(ON) > 2) {
        ON = parseInt(ON) - 1;
        document.getElementById("O" + valx + ON).remove()
        document.getElementById("ON" + valx).value = parseInt(ON);
    }

}