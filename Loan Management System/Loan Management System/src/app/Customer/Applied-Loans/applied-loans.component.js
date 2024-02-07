//app.directive('allLoans', function () {
//    return {
//        restrict: 'E',
//        templateUrl: 'app/Customer/All-Loans/all-loans.component.html',
//        controller: 'allLoansController',
//        controllerAs: 'allLoansController',
//        scope: {},
//        transclude: true,
//    };
//});
app.component('appliedLoans', {
    templateUrl: 'app/Customer/All-Loans/all-loans.component.html',
    controller: allLoansController
});

function allLoansController() {
    console.log('all loans opened');
}