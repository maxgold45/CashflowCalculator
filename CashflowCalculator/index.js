var app = angular.module('myApp', []);



app.controller('calculatorCtrl', function ($scope, $http) {
    $scope.allLoans = [];
    $scope.aggregate = [];
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
            //alert(response.status);
            //alert('getAggregate');
        });
    }

    $scope.addLoan = function () {
        var params = { Principal: $scope.loan.balance, Term: $scope.loan.term, Rate: $scope.loan.rate }
        $http.post('api/loan/AddLoan', params).then(function (response) {
            $scope.getAllCashflows();
            }, function (response) {
                alert("Invalid input. " + $scope.loan.balance);
                alert(response.data);
                alert(response.statusText);
            });
    }

    $scope.removeLoan = function (index) {
        $http.delete('api/loan/RemoveLoan',
            {
                params: { index: index }
            }).then(function (response) {
                $scope.getAllCashflows();
            }, function (response) {
                alert("Invalid delete. Loan " + index);
                alert(response.status + ' Data: ' + response.statusText);
            }
            );
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



    $scope.sorterFunc = function (loan) {
        return parseInt(loan.LoanId);
    };


});
