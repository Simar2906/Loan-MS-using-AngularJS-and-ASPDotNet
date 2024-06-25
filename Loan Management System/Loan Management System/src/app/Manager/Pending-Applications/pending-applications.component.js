app.component('pendingApplications', {
    templateUrl: 'app/Manager/Pending-Applications/pending-applications.component.html',
    controller: pendingApplicationsController
});
pendingApplicationsController.$inject = ['$scope', 'LoanService'];
function pendingApplicationsController($scope, LoanService) {
    var vm = $scope;
    vm.pendApps = [];
    vm.getGender = function (loan) {
        if (loan.gender == 0) {
            return 'male';
        }
        else if (loan.gender == 1) {
            return 'female';
        }
    }
    LoanService.apiResource.getAllPending().$promise
        .then(function (response) {
            vm.pendApps = response.loans;
            console.log(vm.pendApps);
        })
        .catch(function (error) {
            console.log(error);
        });
    vm.getLoanStatus = function (loan) {
        return LoanService.getLoanStatus(loan);
    }
    vm.approveLoan = function (loanId) {
        LoanService.apiResource.approveLoan({ loanId: loanId }).$promise
            .then(function (response) {
                vm.pendApps = vm.pendApps.filter(v => v.id != loanId);
                console.log('Approved Successfully');
            })
            .catch(function (error) {
                console.log(error);
            });
    }
    vm.rejectLoan = function (loanId) {
        LoanService.apiResource.rejectLoan({ loanId: loanId }).$promise
            .then(function (response) {
                vm.pendApps = vm.pendApps.filter(v => v.id != loanId);
                console.log('Rejected Successfully');
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}