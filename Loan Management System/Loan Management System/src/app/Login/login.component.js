app.component('login', {
    templateUrl: 'app/Login/login.component.html',
    controller: loginController
});
loginController.$inject = ['$scope', 'LoginService', '$location'];
function loginController($scope, LoginService, $location) {
    var vm = $scope;
    console.log('inside login');
    vm.loginData = { email: '', password: '' };
    vm.loginProcedure = function () {
        console.log(vm.loginData);
        LoginService.apiResource.validateUser(vm.loginData).$promise
            .then(function (response) {
                console.log(response.token);
                LoginService.setToken(response.token);
                let user = LoginService.getUserData();
                if (user != null) {
                    if (user.role == 'CUSTOMER') {
                        console.log('Customer');
                        $location.path('/customer');
                    }
                    else {
                        console.log('Manager');
                        $location.path('/manager');
                    }
                }
            })
            .catch(function (error) {
                console.log(error);
                alert('Login Credentials are Incorrect!');
                vm.loginData.password = '';
            });
    };

}