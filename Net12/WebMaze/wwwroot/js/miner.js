﻿"use strict";

window.addEventListener('contextmenu', function (e) {
    e.preventDefault();
}, false);
   

function setFlag(cellId) {
    let data = {
        idCell: cellId
    };
    $.post('/Miner/SetFlag', data)
        .done(function () {
            location.reload();
        });
};

/*function openNearWithFlags(cellId) {
    let data = {
        idCell: cellId
    };
    $.post('/Miner/OpenNearWithFlags', data)
        .done(function () {
            location.reload();
        });
};*/





//******************************************

function openNearWithFlagsOrPressNear(cellId) {
    let data = {
        idCell: cellId
    };
    $.get('/Miner/CheckFlagsAndNearBombsCount', data)
        .done(function(answer) {
            if (answer == true) {
                $.post('/Miner/OpenNearWithFlags', data)
                    .done(function () {
                        location.reload();
                    });
            }
            else
            {
                /*$(".y").mousedown(function () {
                    $(".face.miner-elements").attr('src', '../../../images/miner_buttons/face1.png');
                }).mouseup(function () {
                    $(".face.miner-elements").attr('src', '../../../images/miner_buttons/face0.png');
                })*/

                $(".y").mousedown(function () {
                    $(".face.miner-elements").attr('src', '../../../images/miner_buttons/face1.png');
                    for (let i = 0; i < answer.length; i++) {
                        let y = "#" + answer[i];
                        $(y).attr('src', '../../../images/miner_buttons/t0.png');
                    }
                }).mouseup(function () {
                    $(".face.miner-elements").attr('src', '../../../images/miner_buttons/face0.png');
                    for (let i = 0; i < answer.length; i++) {
                        let y = "#" + answer[i];
                        $(y).attr('src', "../../../images/miner_buttons/miner_button.png");
                    }
                })

               /* $(".y").mousedown(function () {
                    for (let i = 0; i < answer.length; i++) {
                        let y = "#" + answer[i];
                        $(y).attr('src', '../../../images/miner_buttons/t0.png');
                    }
                })

                for (let i = 0; i < answer.length; i++) {
                    let y = "#" + answer[i];
                    $(y).attr('src', "../../../images/miner_buttons/miner_button.png");
                }
*/
              

                //$(".face.miner-elements").attr('src', '../../../images/miner_buttons/face_dead.png');

                /*$(".y").mouseup(function () {
                    $(".face.miner-elements").attr('src', '../../../images/miner_buttons/face0.png');
                })*/

               /*function changeFaceWhilePressed() {
                    $(".face.miner-elements").attr('src', '../../../images/miner_buttons/face1.png');
                }
                setTimeout(changeFaceWhilePressed, 300);
*/
                /*for (let i = 0; i < answer.length; i++)
                    {
                        let y = "#" + answer[i];
                        $(y).attr('src', '../../../images/miner_buttons/t0.png');
                    }*/

                
                /*function aaa() {
                    for (let i = 0; i < answer.length; i++) {
                        let y = "#" + answer[i];
                        $(y).attr('src', "../../../images/miner_buttons/miner_button.png");
                    }
                }
                setTimeout(aaa, 500);*/
            }
        });

    
};

//******************************************



/*
$('#Clicker').mousedown(function () {
    //do something here
    timeout = setInterval(function () {
        //do same thing here again
    }, 500);

    return false;
});
$('#Clicker').mouseup(function () {
    clearInterval(timeout);
    return false;
});
$('#Clicker').mouseout(function () {
    clearInterval(timeout);
    return false;
});*/

/*timer();

function timer() {
    $.get('/Miner/SetUpTimerForStartedGame')
        .done(function (fieldId) {
            if (!localStorage.getItem(fieldId)) {

            }
        })
    };*/



/*let i = 0;
let k = 0;
let h = 0;

let timer = 0;


let cl1 = new Image(); cl1.src = '../../../images/miner_buttons/d1.png';
let cl2 = new Image(); cl2.src = '../../../images/miner_buttons/d2.png';
let cl3 = new Image(); cl3.src = '../../../images/miner_buttons/d3.png';
let cl4 = new Image(); cl4.src = '../../../images/miner_buttons/d4.png';
let cl5 = new Image(); cl5.src = '../../../images/miner_buttons/d5.png';
let cl6 = new Image(); cl6.src = '../../../images/miner_buttons/d6.png';
let cl7 = new Image(); cl7.src = '../../../images/miner_buttons/d7.png';
let cl8 = new Image(); cl8.src = '../../../images/miner_buttons/d8.png';
let cl9 = new Image(); cl9.src = '../../../images/miner_buttons/d9.png';
let cl0 = new Image(); cl0.src = '../../../images/miner_buttons/d0.png';
let clock_numbers = new Array(cl0, cl1, cl2, cl3, cl4, cl5, cl6, cl7, cl8, cl9);
*/





function time1()
{
    if (i >= 10) {
        i = 0;
    }
    if (h = 10) {
        return false;
    }
    document.getElementById('imag1').src = clock_numbers[i].src;
    i += 1;
    setTimeout(time1, 1000);
}
function time2() {
    if (k >= 10) {
        k = 0;
    }
    if (h = 10) {
        return false;
    }
    document.getElementById('imag2').src = clock_numbers[k].src;
    k += 1;
    setTimeout(time2, 10000);
}
function time3() {
    if (h = 10) {
        return false;
    }
    document.getElementById('imag3').src = clock_numbers[h].src;
    h += 1;
    setTimeout(time3, 100000);
}
