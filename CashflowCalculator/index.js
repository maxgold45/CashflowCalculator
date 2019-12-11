var app = angular.module('myApp', []);



app.controller('calculatorCtrl', function ($scope, $http) {
    $scope.allCashflows = [];
    $scope.allLoans = [];

    $scope.aggregate = null;
    $scope.loan =
        {
            balance: '',
            term: '',
            rate: '',
        };
    window.onload = function () {
        $scope.getAllCashflows();
    }

    $scope.addLoan = function () {
        $http.post('api/loan/AddLoan', $scope.aggregate,
            {
                params: { Principal: $scope.loan.balance, Term: $scope.loan.term, Rate: $scope.loan.rate }
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

    $scope.getAllCashflows = function () {
        $scope.allCashflows = [];
        $scope.allLoans = [];
        $http.get('api/loan/GetAllLoans'
        ).then(function (response) {
            for (var i = 0; i < response.data.length; i++){
                $scope.allLoans.push(response.data[i]);
                $http.get('api/loan/GetCashflowRows',
                    {
                        params: { LoanId: response.data[i].LoanId }
                    }).then(function (innerResponse) {
                        $scope.allCashflows.push(innerResponse.data);
                    }, function (innerResponse) {
                            alert('fail');
                    });
            }
        }, function (response) {
                alert("Not In Here!");

            });
    }

    $scope.getCashflow = function (loanId) {
        $http.get('api/loan/GetCashflowRows',
            {
                params: { LoanId: loanId }
            }).then(function (innerResponse) {
                $scope.allCashflows.push(response.data);
            }, function (innerResponse) {
                alert('fail');
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

app.filter('orderObjectBy', function () {
    return function (items, field, reverse) {
        var filtered = [];
        angular.forEach(items, function (item) {
            filtered.push(item);
        });
        filtered.sort(function (a, b) {
            return (a[field] > b[field] ? 1 : -1);
        });
        if (reverse) filtered.reverse();
        return filtered;
    };
});