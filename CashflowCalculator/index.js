var app = angular.module('myApp', []);


app.controller('calculatorCtrl', function ($scope, $http) {
    $scope.parseInt = function (num) {
        return parseInt(num);
    }


    $scope.loan =
        {
            balance: '200000',
            principal: '',
            term: '24',
            rate: '3',
            interestPayment: ''
        };

   
    $scope.getCashflow = function () {
        
        $http({
            method: "GET",
            url: 'api/loan/GetRow/?balance=' + $scope.loan.balance + '&term=' + $scope.loan.term + '&rate=' + $scope.loan.rate
        }).then(function (response) {
            $scope.cashflow = response.data;
        }, function (response) {
            $scope.loan = {
                'balance': "",
                'term': "",
                'rate': ""
            };
            alert(response.status);
        });
    };
});