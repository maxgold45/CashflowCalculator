var app = angular.module('myApp', []);


app.controller('calculatorCtrl', function ($scope, $http) {
    $scope.parseInt = function (num) {
        return parseInt(num);
    }

    $scope.allCashflows = [];
    $scope.aggregate = null;
    $scope.loan =
        {
            balance: '',
            term: '',
            rate: '',
        };

    $scope.addLoan = function () {
        //     $scope.updateAggregate();
        $http.get('api/loan/GetRow',
            {
                params: { balance: $scope.loan.balance, term: $scope.loan.term, rate: $scope.loan.rate, aggregate: $scope.aggregate}
            }).then(function (response) {
                //$scope.cashflow = response.data;
                $scope.cashflow = response.data[0];
                $scope.aggregate = response.data[1];
                $scope.allCashflows.push($scope.cashflow);
            }, function (response) {
                alert("Invalid input");
            });

    

        /*   $scope.updateAggregate = function () {
               
       
           }*/
    }
});