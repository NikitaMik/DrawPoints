angular.module("PointApp", [])
    .constant("Url", "/api/")
    .controller("PointCtrl", function ($scope, $http, Url) {

        $scope.points = "300;300 500;500 200;200 400;400 100;100 800;800 700;700";
        //$scope.points = "500;500 600;400 400;300 700;600 300;200";

        $scope.checWeb = false;
        $scope.separator = " ";

        $scope.btnLine = function () {

            $scope.checWeb ? $scope.sendLinePoints() : $scope.jsLinePoints();
        };

        $scope.btnSpiral = function () {
            $scope.checWeb ? $scope.sendSpiralPoints() : $scope.jsSpiralPoints();
        };

        $scope.btnClear = function () {
            drawClear();
            $scope.message = "Чисто";

        };

        $scope.sendLinePoints = function () {
            $scope.message = "Идет отправка данных...";
            var points = createArrayPoints($scope.points, $scope.separator);

            $http({
                url: Url + "Line",
                method: "POST",
                data: points

            }).then(function (response) {
                draw(response.data);
                $scope.message = "Сервер - Сортировка пришла.";
                }, errorCallback);


        };

        $scope.sendSpiralPoints = function () {
            $scope.message = "Идет отправка данных...";
            var points = createArrayPoints($scope.points, $scope.separator);

            $http({
                url: Url + "Spiral",
                method: "POST",
                data: points

            }).then(function (response) {

                console.log(response.data);
                drawSpiral(response.data);
                $scope.message = "Сервер - Сортировка пришла.";
            }, errorCallback);
        };

        $scope.jsLinePoints = function () {

            $scope.message = "Идет сортировка...";
            var points = createArrayPoints($scope.points, $scope.separator);

            //Создание точек
            var workListPoint = new WorkListPoint();
            workListPoint.createPoints(points)
            workListPoint.Sort();

            draw(workListPoint.arrayPoint);
            $scope.message = "Клиент - Сортировка выполнена.";
        };

        $scope.jsSpiralPoints = function () {

            $scope.message = "Идет сортировка ...";
            var points = createArrayPoints($scope.points, $scope.separator);

            //Создание точек
            var workListPoint = new WorkListPoint();
            workListPoint.createPoints(points)
            workListPoint.Sort();

            //Сортировка относительно центральной точки
            var len = workListPoint.arrayPoint.length;
            var centr = (len % 2 == 1) ? ((len - 1) / 2) : ((len / 2) - 1);
            workListPoint.SpiralRadius(centr);
            workListPoint.Sort();

            var spiralPoint = workListPoint.CreateSpiralPoint(centr); 

            drawSpiral(spiralPoint);
            $scope.message = "Клиент - Сортировка выполнена.";
        };

    });


//Преобразование введенной строки точек
function createArrayPoints(points, separator) {
    var mas = points.split(separator);
    var arr = [[], []];

    try {
        for (var i = 0; i < mas.length; i++) {
            var q = mas[i].split(';');
            arr[0].push(parseInt(q[0]));
            arr[1].push(parseInt(q[1]));
        }

        return arr;

    } catch (e) {
        alert(e.message);
    }
    
};

//Point
function Point(x,y) {
    this.x = x;
    this.y = y;
    this.radius;
};
Point.prototype.RadiusCount = function () {
    this.radius = Math.round(Math.sqrt(Math.pow((this.x), 2) + Math.pow((this.y), 2)), 2);
};
//Расчет радиуса относительно центральной точки
Point.prototype.RadiusSentrCount = function (cx, cy) {
    this.radius = Math.round(Math.sqrt(Math.pow((this.x - cx), 2) + Math.pow((this.y - cy), 2)), 2);
};

//SpiralPoint
function SpiralPoint(start, end) {
    this.start = start;
    this.end = end;
    this.bezE = new Point(0,0);

    this.CalculatebezE();
};
var radian = (90 * Math.PI / 180);
//Расчет точки Безье
SpiralPoint.prototype.CalculatebezE = function () {
    var centerX = (this.start.x + this.end.x) / 2;
    var centerY = (this.start.y + this.end.y) / 2;

    this.bezE.x = Math.round(centerX + (this.start.x - centerX) * Math.cos(radian) - (this.start.y - centerY) * Math.sin(radian), 2);
    this.bezE.y = Math.round(centerY + (this.start.x - centerX) * Math.sin(radian) + (this.start.y - centerY) * Math.cos(radian), 2);
};

// Работа с массивами точек
function WorkListPoint() {

    this.arrayPoint = [];

    //Создание точек Point
    this.createPoints = function (date) {

        for (var i = 0; i < date[0].length; i++) {

            var point = new Point(date[0][i], date[1][i]);
            point.RadiusCount();
            this.arrayPoint.push(point);
        }
    };


    this.Sort = function () {
        var length = this.arrayPoint.length;
        var temp;

        for (var i = 0; i < length; i++) {
            for (var j = 0; j < length; j++) {
                if (this.arrayPoint[i].radius < this.arrayPoint[j].radius) {
                    temp = this.arrayPoint[i];
                    this.arrayPoint[i] = this.arrayPoint[j];
                    this.arrayPoint[j] = temp;
                }
            }
        }
    };

    //Расчет радиуса относительно центральной точки
    this.SpiralRadius = function (centr) {
        var cx = this.arrayPoint[centr].x;
        var cy = this.arrayPoint[centr].y;

        this.arrayPoint.forEach(function (item) {
            item.RadiusSentrCount(cx, cy);
        });
    };

    //Создание массива точек для SpiralPoint
    this.CreateSpiralPoint = function (centr) {

        var ListSpiralPoint = [];
        var flagSpiral = true;
        var array = this.arrayPoint;
        var length = array.length;

        for (var i = 1; i < length; i++) {
            var spiralPoint = new SpiralPoint(array[i - 1], array[i]);
            ListSpiralPoint.push(spiralPoint);
        }

        return ListSpiralPoint;
    };

};

function errorCallback(error) {
    alert(erroe.message);
};


