app.component('appliedLoans', {
    templateUrl: 'app/Customer/Applied-Loans/applied-loans.component.html',
    controller: appliedLoansController
});
appliedLoansController.$inject = ['$scope', 'LoanService', 'LoginService'];
function appliedLoansController($scope, LoanService, LoginService) {
    console.log('applied loans opened');
    var vm = $scope;
    vm.appliedLoans = [];
    vm.createTime = function (time) {
        return LoanService.yearsToYearsMonthsDays(time);
    }
    vm.getLoanStatus = function (loan) {
        return LoanService.getLoanStatus(loan);
    }
    LoanService.apiResource.getLoansByUser({ userId: LoginService.getUserData().id }).$promise
        .then(function (response) {
            vm.appliedLoans = response.loanList;
            console.log(vm.appliedLoans);
        })
        .catch(function (error) {
            console.log(error);
        });
}