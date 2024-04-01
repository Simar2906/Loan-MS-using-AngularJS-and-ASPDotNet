app.component('signup', {
    templateUrl: 'app/Signup/signup.component.html',
    controller: signupController
});

signupController.$inject = ['$scope', 'LoginService', '$location'];

function signupController($scope, LoginService, $location) {
    var vm = $scope;
    console.log('inside signup');
    vm.userData = {
        Email: '',
        Gender: '',
        Name: '',
        Password: '',
        Role: '',
        Salary: 0,
        Employer: '',
        Designation: '',
        UserPic: '',
    };

    vm.signupProcedure = function () {
        if (vm.userData.Role == 'MANAGER') {
            vm.userData.Salary = 50000;
            vm.userData.Employer = "Simar's Loan Company";
            vm.userData.Designation = "Manager";
        }
        console.log(vm.userData);

        LoginService.apiResource.createUser(vm.userData).$promise
            .then(function (response) {
                LoginService.setToken(response.token);
                let user = LoginService.getUserData();
                if (user != null) {
                    if (user.role == 'CUSTOMER') {
                        console.log('Customer');
                        $location.path('/customer');
                    } else {
                        console.log('Manager');
                        $location.path('/manager');
                    }
                }
            })
            .catch(function (error) {
                console.log(error);
                alert('User Not found!');
                vm.loginData.password = '';
            });
    };
}
