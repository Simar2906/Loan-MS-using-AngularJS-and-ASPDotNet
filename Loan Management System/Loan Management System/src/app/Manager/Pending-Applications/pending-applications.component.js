app.component('pendingApplications', {
    templateUrl: 'app/Manager/Pending-Applications/pending-applications.component.html',
    controller: pendingApplicationsController
});
pendingApplicationsController.$inject = ['$scope', 'LoanService'];
function pendingApplicationsController($scope, LoanService) {
    var vm = $scope;
    vm.pendApps = [];

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
}