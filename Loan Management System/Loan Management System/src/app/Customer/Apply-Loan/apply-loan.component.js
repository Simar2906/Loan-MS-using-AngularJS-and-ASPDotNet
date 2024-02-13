app.component("applyLoan", {
    templateUrl: "app/Customer/Apply-Loan/apply-loan.component.html",
    controller: applyLoanController
});

applyLoanController.$inject = ['$scope', 'LoanService', 'LoginService'];

function applyLoanController($scope, LoanService, LoginService) {
    var vm = $scope;

    vm.amountApplied;
    vm.interestRate;
    vm.termLength;
    vm.range = [0, 0];
    vm.loanRange = [5000, 100000];
    vm.formSubmitted = false;

    vm.userData = LoginService.getUserData();
    vm.userData.salary = parseFloat(vm.userData.salary);

    vm.applyingLoan = LoanService.currentApplyingLoan;

    vm.$watch(function () {
        return LoanService.currentApplyingLoan;
    }, function (newValue, oldValue) {
        try {
            vm.applyingLoan = newValue;
            console.log('ApplyingLoan', vm.applyingLoan);
            vm.range = vm.applyingLoan.interestRates.split('-').map(v => parseFloat(v.slice(0, -1)));
            setValidators(vm);
        } catch (err) {
            console.log('loading');
        }
    });

    vm.applyLoan = function () {
        vm.formSubmitted = true;

        if (vm.applyForm.$valid) {
            // Perform submission logic
            var submitted_data = {
                UserId: parseInt(vm.userData.id),
                LoanId: vm.applyingLoan.id,
                AppliedAmount: vm.amountApplied,
                AppliedRate: vm.interestRate,
                TermLength: vm.termLength,
            };
            console.log('Form is valid. Submitting...');

            LoanService.apiResource.applyNewLoan(submitted_data).$promise
                .then(function (response) {
                    console.log(response);
                    LoanService.popUpFormStatus = false;
                    clearForm(vm);
                    vm.formSubmitted = false;
                })
                .catch(function (error) {
                    console.log(error);

                });
        } else {
            console.log('Form is invalid. Please check for errors.');
        }
    };

    vm.cancelApply = function () {
        // Cancel logic
        LoanService.popUpFormStatus = false;
        vm.formSubmitted = false;
        clearForm(vm);
        console.log('cancelled');
    };
}

function setValidators($scope) {
    $scope.applyForm.interestRate.$validators.invalidRange = function (modelValue, viewValue) {
        var value = modelValue || viewValue;
        return value >= $scope.range[0] && value <= $scope.range[1];
    };
    $scope.applyForm.termLength.$validators.invalidRange = function (modelValue, viewValue) {
        var value = modelValue || viewValue;
        return value >= $scope.applyingLoan.termLength && value <= 40;
    };
    $scope.applyForm.amountApplied.$validators.invalidRange = function (modelValue, viewValue) {
        var value = modelValue || viewValue;
        return value >= $scope.loanRange[0] && value <= $scope.loanRange[1];
    };
}

function clearForm($scope) {
    $scope.applyForm.$setPristine();
    $scope.applyForm.$setUntouched();
    $scope.applyForm.$rollbackViewValue();

    // Reset form fields
    $scope.amountApplied = null;
    $scope.interestRate = null;
    $scope.termLength = null;
}