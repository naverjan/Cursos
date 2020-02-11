/**
 * 
 */
var app = angular.module('myapp', ['ngRoute']);
app.controller('myCtrl', ['$scope','$http']);
app.controller('loginController', function ($scope, $http) {
    //Controller Here
    $scope.name = "Andres verjan";
    $scope.getCourses = function () {

        $http({
            method: "GET",
            url: "https://localhost:44377/api/courses"
        }).success(function (response) {
            console.log("Todo bien");
            console.log(response);
        }).error(function () {
            console.log("Ocurrio un error");
        });
    }
});