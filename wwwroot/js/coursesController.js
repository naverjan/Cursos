var app = angular.module('myapp', ['ngRoute']);
app.controller('myCtrl', ['$scope']);

//app.config(["$routeProvider", function ($routeProvider) {
//    $routeProvider
//        .when('/ui-login', {
//            templateUrl: 'ui-login.html',
//            controller: 'loginController'
//        })
//        .otherwise({
//            redirectTo: '/ui-login'
//        });
//}]);
app.controller('loginController', function ($scope) {
    //Controller Here
    $scope.name = "Andres verjan";
    $scope.submit = function () {
        console.log("hi i m coming");
    }
    console.log("in controller");
});