var app = angular.module('myApp', []);

app.controller('calculatorCtrl', function ($scope, $http) {

    $scope.loan =
        {
            balance: '',
            term: '',
            rate: '',
        };

    $scope.allLoans = [];
    $scope.aggregate = [];

    $scope.addable = true;
    $scope.removable = true;
    

    /**
     * Populates the cashflows and aggregate tables on startup.
     * */
    window.onload = function () {
        $scope.getAllLoans();
        $scope.getAggregate();
    }

    /**
     * Sets allLoans to contain all loans in the database.
     * 
     * This method should only be called when the page is loaded.
     * */
    $scope.getAllLoans = function () {
        $http.get('api/loan/GetAllLoans'
        ).then(function (response) {
            $scope.allLoans = response.data;
        }, function (response) {
            alert("Get all loans failed. " + response.status);
        });
    }

    /**
     * Sets aggregate to contain the aggregate of all loans in the database.
     *
     * This method should be called when the page is loaded, or when loans are added or removed.
     * */
    $scope.getAggregate = function () {
        $http.get('api/loan/GetAggregate'
        ).then(function (response) {
            $scope.aggregate = response.data;
        }, function (response) {
            alert("Aggregate failed. " + response.status);
        });
    }

    /**
     * Adds the current loan to the database. Disables the button after it is pressed, then
     *     enables it after the loan is added to the database. It also updates aggregate.
     * 
     * This method does not call getAllLoans(). Instead, it will just add the new loan object
     *     into allLoans.
     * 
     * This method should be called when the Add Loan button is pressed.
     * */
    $scope.addLoan = function () {
        $scope.addable = false;
        var params = { Principal: $scope.loan.balance, Term: $scope.loan.term, Rate: $scope.loan.rate }
        $scope.loan.balance = "";
        $scope.loan.term = "";
        $scope.loan.rate = "";
        $http.post('api/loan/AddLoan', params).then(function (response) {
            $scope.allLoans.push(response.data);
            $scope.getAggregate();
            $scope.addable = true;
        }, function (response) {
            alert("Invalid input. " + response.status);
            $scope.addable = true;
        });
    }

    /**
    * Removes the selected loan from the database given its id. It also removes the loan from allLoans given its index. It
    *     disables all remove buttons until the loan is successfully removed from the database. It also updates aggregate.
    *
    * This method does not call getAllLoans(). Instead, it will just remove the loan object
    *     from allLoans.
    *
    * This method should be called when any Remove Loan button is pressed.
    * */
    $scope.removeLoan = function (id, index) {
        $scope.removable = false;
        $http.delete('api/loan/RemoveLoan',
            {
                params: { loanId: id }
            }).then(function (response) {
                $scope.allLoans.splice(index, 1);
                $scope.getAggregate();
                $scope.removable = true;
            }, function (response) {
                alert("Delete failed. " + response.status);
                $scope.removable = true;
            }
        );
    }
});
