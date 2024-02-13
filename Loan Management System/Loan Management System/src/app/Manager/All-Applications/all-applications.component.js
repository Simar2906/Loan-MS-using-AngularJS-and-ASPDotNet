app.component('allApplications', {
    templateUrl: 'app/Manager/All-Applications/all-applications.component.html',
    controller: allApplicationsController
});
allApplicationsController.$inject = ['$scope', 'LoanService'];
function allApplicationsController($scope, LoanService) {
    var vm = $scope;
    vm.allApps = [];

    LoanService.apiResource.getAllApplications().$promise
        .then(function (response) {
            vm.allApps = response.loans;
            console.log(vm.allApps);
        })
        .catch(function (error) {
            console.log(error);
        });
    vm.getLoanStatus = function (loan) {
        return LoanService.getLoanStatus(loan);
    }
}