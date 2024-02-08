app.component('allLoans', {
    templateUrl: 'app/Customer/All-Loans/all-loans.component.html',
    controller: allLoansController
});
allLoansController.$inject = ['$scope', 'LoanService'];
function allLoansController($scope, LoanService) {
    var vm = $scope;
    vm.allLoans = [];
    LoanService.apiResource.getAllLoans().$promise
        .then(function (response) {
            vm.allLoans = response.loans;
            console.log(vm.allLoans);
        })
        .catch(function (error) {
            console.log(error);
        });
}