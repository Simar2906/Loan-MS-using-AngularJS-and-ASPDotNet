app.component('rejectedApplications', {
    templateUrl: 'app/Manager/Rejected-Applications/rejected-applications.component.html',
    controller: rejectedApplicationsController
});
rejectedApplicationsController.$inject = ['$scope', 'LoanService'];
function rejectedApplicationsController($scope, LoanService) {
    var vm = $scope;
    vm.rejcApps = [];

    LoanService.apiResource.getAllRejected().$promise
        .then(function (response) {
            vm.rejcApps = response.loans;
            console.log(vm.rejcApps);
        })
        .catch(function (error) {
            console.log(error);
        });
    vm.getLoanStatus = function (loan) {
        return LoanService.getLoanStatus(loan);
    }
}