var app = angular.module('myApp', []);



app.controller('calculatorCtrl', function ($scope, $http) {
    $scope.allLoans = [];
    $scope.aggregate = [];
    $scope.addable = false;
    $scope.loan =
        {
            balance: '',
            term: '',
            rate: '',
        };

    window.onload = function () {
        $scope.getAllCashflows();
        $scope.getAggregate();
    }
    $scope.getAllCashflows = function () {
        $scope.allLoans = [];
        $http.get('api/loan/GetAllLoans'
        ).then(function (response) {
            $scope.allLoans = response.data;
        }, function (response) {
            alert(response.status);
            alert('getallloans');
            });
        $scope.getAggregate();
    }
    $scope.getAggregate = function () {
        $scope.aggregate = [];
        $http.get('api/loan/GetAggregate'
        ).then(function (response) {
            $scope.aggregate = response.data;
        }, function (response) {
            alert("Aggregate failed. " + response.status);
        });
    }
    $scope.addLoan = function () {
        $scope.addable = true;
        var params = { Principal: $scope.loan.balance, Term: $scope.loan.term, Rate: $scope.loan.rate }
        $http.post('api/loan/AddLoan', params).then(function (response) {
            $scope.allLoans.push(response.data);
            $scope.getAggregate();
            $scope.addable = false;
            $scope.loan.balance = "";
            $scope.loan.term = "";
            $scope.loan.rate = "";
        }, function (response) {
                $scope.addable = false;
                alert("Invalid input. " + $scope.loan.balance);
            });
    }

    $scope.removeLoan = function (index) {
        $http.delete('api/loan/RemoveLoan',
            {
                params: { index: index }
            }).then(function (response) {
                $scope.getAggregate();
            }, function (response) {
                alert("Invalid delete. Loan " + index);
                alert(response.status + ' Data: ' + response.statusText);
            }
            );
    }
});
