app.component('allLoans', {
    templateUrl: 'app/Customer/All-Loans/all-loans.component.html',
    controller: allLoansController
});
allLoansController.$inject = ['scope'];
function allLoansController($scope) {
    console.log('all loans opened');


}