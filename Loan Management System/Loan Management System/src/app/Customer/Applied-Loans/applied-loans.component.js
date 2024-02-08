app.component('appliedLoans', {
    templateUrl: 'app/Customer/Applied-Loans/applied-loans.component.html',
    controller: appliedLoansController
});
appliedLoansController.$inject = ['$scope', 'LoanService'];
function appliedLoansController($scope, LoanService) {
    console.log('applied loans opened');
    var vm = $scope;
    vm.appliedLoans = [];
    vm.createTime = function (time) {
        return LoanService.yearsToYearsMonthsDays(time);
    }

    
    LoanService.apiResource.getLoansByUser({ userId: 1 }).$promise
        .then(function (response) {
            vm.appliedLoans = response.loanList;
            console.log(vm.appliedLoans);
        })
        .catch(function (error) {
            console.log(error);
        });

}