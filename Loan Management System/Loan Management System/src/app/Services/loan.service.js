app.service('LoanService', ['$resource', 'jwtHelper', function ($resource, jwtHelper) {

    this.yearsToYearsMonthsDays = function (totalYears) {
        const years = Math.floor(totalYears);
        const decimalMonths = (totalYears - years) * 12;
        const months = Math.floor(decimalMonths);
        const days = Math.floor((decimalMonths - months) * 30.44);

        return { years, months, days };
    }

    this.getLoanStatus = function (loan) {
        switch (loan.status) {
            case 1:
                return "Approved";
                break;
            case 2:
                return "Rejected";
                break;
            case 3:
                return "Pending";
            default:
                return null;
        }
    }
    this.apiResource = $resource('/api/loan', {}, {
        getAllLoans: {
            url: '/api/loan/getAllLoans',
            method: 'GET'
        },
        getLoansByUser: {
            url: '/api/loan/getLoansByUser',
            method: 'POST',
            params: { userId: '@userId' }
        }
    });
}]);
