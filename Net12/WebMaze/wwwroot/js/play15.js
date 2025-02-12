﻿$(document).ready(function () {

    var places = [];
    places.push(new Array(4));
    places.push(new Array(4));
    places.push(new Array(4));
    places.push(new Array(4));
    //[row][column]
    //place 0..15
    //number 1..15 and undefined

    init();
    checkWin();

    function init() {               
        var residue;

        do {            
            for (var place = 0; place < 16; place++) {
                let cell = getRowAndColumnByPlace(place);
                places[cell.row][cell.column] = undefined;
            }

            choosePlaceForNumber();
            residue = checkPossibilityToWin();

        } while (residue == 1);
        
        for (let row = 0; row < 4; row++) {        
            for (let column = 0; column < 4; column++) {
                if (places[row][column]) {
                    let number = places[row][column];
                    
                    var element = $('[id-row="' + row + '"][id-column="' + column + '"]');
                    var piece = $('<div class="number"><span class="number-style">' + places[row][column] + '</span></div>');

                    piece.click(function () {                        
                        step(row, column);
                        checkWin();
                        });
                    element.html(piece);

                    changeColor(number, row, column);
                } 
            }
        }
    }

    function choosePlaceForNumber() {
        for (var number = 1; number < 16; number++) {
            do {
                let randomPlace = getRandomInt(0, 16);
                cell = getRowAndColumnByPlace(randomPlace);
            } while (places[cell.row][cell.column] !== undefined);

            places[cell.row][cell.column] = number;
        }
    }

    function checkPossibilityToWin() {
        var sum = 0;

        for (var place = 0; place < 16; place++) {
            let cell = getRowAndColumnByPlace(place);
            let numberTemp = places[cell.row][cell.column];

            if (numberTemp === undefined) {
                sum += (cell.row + 1);
                console.log('undefined, sum = ' + sum);
                continue;
            }

            for (var i = place; i < 16; i++) {
                cell = getRowAndColumnByPlace(i);
                if (places[cell.row][cell.column] < numberTemp) {
                    sum += 1;
                }
            }

        }
        
        var residue = sum % 2;
        console.log('sum = ' + sum + ' residue = ' + residue);
        return residue;
    }

    function step(row, column) {
        if (places[row - 1] && places[row - 1][column] === undefined) {
            return moveCell('up', row, column);          
        }

        if (places[row + 1] && places[row + 1][column] === undefined) {
            return moveCell('down', row, column);
        }

        if (places[row][column - 1] === undefined && ((column - 1) > -1)) {
            return moveCell('left', row, column);
        }

        if (places[row][column + 1] === undefined && ((column + 1) < 4)) {
            return moveCell('right', row, column);
        }
    }  

    function checkWin() {
        for (var place = 0; place < 15; place++) {
            let number = place + 1;
            cell = getRowAndColumnByPlace(place);

            console.log(number);

            if (number != places[cell.row][cell.column]) {
                console.log('win: ' + false);
                return false;
            }           
        }
       
        $(".win-display").css('visibility','visible');
        console.log('win');
        return true;
    }

    function moveCell(direction, row, column) {
        let newRow = row, newColumn = column;

        let temp = places[row][column];
        places[row][column] = undefined;

        switch (direction) {
            case 'up':
                newRow = row - 1;
                break;
            case 'down':
                newRow = row + 1;                           
                break;
            case 'left':
                newColumn = column - 1;                              
                break;
            case 'right':
                newColumn = column + 1;                
                break;
        }

        places[newRow][newColumn] = temp;
        var elm = $('[id-row="' + row + '"][id-column="' + column + '"] > div');
        elm.unbind("click");
        elm.click(function () {
            step(newRow, newColumn);
            checkWin();
        });
        elm.appendTo('[id-row="' + newRow + '"][id-column="' + newColumn + '"]');

        changeColor(temp, newRow, newColumn);
    }

    function changeColor(number, row, column) {
        var elm = $('[id-row="' + row + '"][id-column="' + column + '"] > div');
        elm.css('background-color', number == (row * 4 + column + 1) ? 'olivedrab' : 'darkgreen');  
    }

    function getRowAndColumnByPlace(place) {
        row = Math.floor(place / 4);
        column = place % 4;
        return {
            row: row,
            column: column
        };
    }

    function getRandomInt(min, max) {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min)) + min;
    }
});

