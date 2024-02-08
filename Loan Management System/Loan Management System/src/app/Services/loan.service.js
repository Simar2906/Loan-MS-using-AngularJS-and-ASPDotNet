app.service('LoanService', ['$resource', 'jwtHelper', function ($resource, jwtHelper) {
    this.apiResource = $resource('/api/loan/getAllLoans', {}, {
        getAllLoans: {
            method: 'GET'
        },
        getLoansByUser: {
            method: 'POST'
        }
    });
}]);
