app.component('allLoans', {
    templateUrl: 'app/Customer/All-Loans/all-loans.component.html',
    controller: allLoansController
});
allLoansController.$inject = ['$scope', 'LoanService'];
function allLoansController($scope, LoanService) {
    var vm = $scope;
    vm.allLoans = [];
    vm.loanFormStatus = LoanService.popUpFormStatus;

    vm.$watch(
        function () {
            return LoanService.popUpFormStatus;
        },
        function (newValue, oldValue) {
            vm.loanFormStatus = newValue;
        },
        true // Add this parameter for a deep watch
    );


    LoanService.apiResource.getAllLoans().$promise
        .then(function (response) {
            vm.allLoans = response.loans;
            console.log(vm.allLoans);
        })
        .catch(function (error) {
            console.log(error);
        });
    vm.applyNowClicked = function(loanDetails){
        LoanService.applyNewLoanClicked(loanDetails);
    }
    vm.getPath = function (path) {
        var newPath = "../../../" + path;
        return newPath;
    }
}