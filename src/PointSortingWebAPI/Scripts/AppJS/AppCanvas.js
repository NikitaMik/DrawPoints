
var context;

window.onload = function () {
    context = CreateCanvas();
};

function CreateCanvas() {
    var canvas = document.getElementById("canvas");
    var context = canvas.getContext("2d");
    return context;
};

function draw(date) {
    context.beginPath();

    context.moveTo(date[0].x, date[0].y);

    for (var i = 1; i < date.length; i++) {
        context.lineTo(date[i].x, date[i].y);
    }
    context.stroke();
};

function drawSpiral(date) {
    context.beginPath();

    for (var i = 0; i < date.length; i++) {
        context.moveTo(date[i].start.x, date[i].start.y);
        context.quadraticCurveTo(date[i].bezE.x, date[i].bezE.y, date[i].end.x, date[i].end.y);
    }
    context.stroke();
};

function drawClear() {
    context.clearRect(0, 0, canvas.width, canvas.height);
};