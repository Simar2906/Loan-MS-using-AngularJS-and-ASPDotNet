var app = angular.module('lms', ['ngRoute', 'ngResource', 'angular-jwt']);
app.controller('AppCtrl', AppCtrl);
app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when('/',{
            template:'<home></home>'
        })
        .when('/login', {
            template: '<login></login>'
        })
        .when('/customer', {
            template: '<customer></customer>'
        })
        .when('/manager', {
            template: '<manager></manager>'
        })
        .otherwise({
            redirectTo: ''
        });
    //$locationProvider.html5Mode({
    //    enabled: true,
    //    requireBase: true
    //});
});

AppCtrl.$inject = ['$scope', '$location', 'LoginService'];

function AppCtrl($scope, $location, LoginService) {
    var vm = $scope;
    vm.showDashboardLink = false;
    vm.userName = LoginService.userName || '';
    vm.loginStatus = false;


    vm.isHomeRoute = function () {
        if ($location.path() === '/') {
            vm.showDashboardLink = true;
            return true;
        } else {
            vm.showDashboardLink = false;
            return false;
        }
    };

    vm.$on('$locationChangeSuccess', function () {
        if (vm.isHomeRoute()) {
            console.log('Reached Home');
            vm.showDashboardLink = true;
        }
        else {
            vm.showDashboardLink = false;
        }
    });

    vm.goToHome = function () {
        console.log('Going To Home');
        $location.path('/');
    };

    vm.redirectToLogin = function () {
        console.log('going to login!');
        $location.path('/login');
    };
    // implement all other features after implemeting login
    console.log("Constructed");
}