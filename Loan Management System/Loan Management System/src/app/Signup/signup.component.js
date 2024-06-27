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
        FirstName: '',
        LastName: '',
        Password: '',
        Role: '',
        Salary: 0,
        Employer: '',
        Designation: '',
        UserPic: '',
        FileName: ''
    };
    vm.setFile = function (element) {
        vm.$apply(function () {
            var file = element.files[0];
            var reader = new FileReader();
            vm.userData.FileName = file.name;
            reader.onload = function (e) {
                vm.userData.UserPic = e.target.result;

                vm.$digest();
            };

            reader.readAsDataURL(file);
        });
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
                alert('Couldn\'t Create User!');
            });
    };
}
