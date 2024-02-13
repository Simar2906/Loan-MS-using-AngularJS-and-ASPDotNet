app.component('approvedApplications', {
    templateUrl: 'app/Manager/Approved-Applications/approved-applications.component.html',
    controller: approvedApplicationsController
});
approvedApplicationsController.$inject = ['$scope', 'LoanService'];
function approvedApplicationsController($scope, LoanService) {
    var vm = $scope;
    vm.approvedApps = [];

    LoanService.apiResource.getAllApproved().$promise
        .then(function (response) {
            vm.approvedApps = response.loans;
            console.log(vm.approvedApps);
        })
        .catch(function (error) {
            console.log(error);
        });
    vm.getLoanStatus = function (loan) {
        return LoanService.getLoanStatus(loan);
    }
}