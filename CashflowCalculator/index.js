var app = angular.module('myApp', []);
app.controller('calculatorCtrl', function ($scope, $http) {
    $scope.loan =
        {
            balance: "200000",
            term: "360",
            rate: ".03"
        };

    $scope.getInterestPayment = function () {

        $http({
            method: "GET",
            url: '../loan/getInterestPayment/' + $scope.loan
        }).then(function (data) {
            $scope.loan = [{ balance: data.balance, term: data.term, rate: data.rate }];
        }, function (data) {
            $scope.loan = {
                balance: "",
                term: "",
                rate: ""
            };
            alert(data.statusText);
        });
    };
});