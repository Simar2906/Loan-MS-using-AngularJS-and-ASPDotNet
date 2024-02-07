app.service('LoanService', ['$resource', 'jwtHelper', function ($resource, jwtHelper) {

    this.getAllLoans = function () {

    }
    this.apiResource = $resource('/api/validation/ValidateUser', {}, {
        validateUser: {
            method: 'POST'
        }
    });
    this.updateUserData();
}]);
