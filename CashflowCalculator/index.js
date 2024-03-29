﻿var app = angular.module('myApp', []);

app.controller('calculatorCtrl', function ($scope, $http) {
    $scope.allCashflows = [];
    $scope.aggregate = null;
    $scope.loan =
        {
            balance: '',
            term: '',
            rate: '',
        };

    $scope.addLoan = function () {
        $http.post('api/loan/GetRow', $scope.aggregate,
            {
                params: { Principal: $scope.loan.balance, Term: $scope.loan.term, Rate: $scope.loan.rate}
            }).then(function (response) {
                //$scope.cashflow = response.data;
                $scope.cashflow = response.data[0];
                $scope.aggregate = response.data[1];
                $scope.allCashflows.push($scope.cashflow);
            }, function (response) {
                alert("Invalid input. " + $scope.loan.balance);
                alert(response.data);
                alert(response.statusText);
            });
    }

    $scope.removeLoan = function (index) {
        $http.post('api/loan/RemoveRow', $scope.aggregate,
            {
                params: { allCashflows: $scope.allCashflows, index: index }
            }).then(function (response) {
                //$scope.cashflow = response.data;
                alert(index);
                alert(response.data);
                $scope.allCashflows = response.data[0];
                $scope.aggregate = response.data[1];
            }, function (response) {
                alert("Invalid input. Loan " + index);
                alert(response.status + ' Data: ' + response.statusText );
                }
        );
    }
    $scope.remove = function (array, index) {
        array.splice(index, 1);
    }
});