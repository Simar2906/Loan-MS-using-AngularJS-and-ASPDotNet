app.service('LoginService', ['$resource', 'jwtHelper', function ($resource, jwtHelper) {
    this.userName = 'DefaultUser';
    this.setToken = function (token) {
        sessionStorage.setItem('token', token);
        this.updateUserData();
    }
    this.getUserData = function () {
        var token = sessionStorage.getItem('token');
        if (token == null) {
            return null;
        }
        else {
            var decoded = jwtHelper.decodeToken(token);
            console.log(decoded);
            return decoded;
        }
    }
    this.updateUserData = function () {
        var token = sessionStorage.getItem('token');
        console.log('Login Found in session');
        if (token != null) {
            var decoded = jwtHelper.decodeToken(token);
            this.userName = decoded.name;
        }
    };

    this.apiResource = $resource('/api/validation/ValidateUser', {}, {
        validateUser: {
            method: 'POST'
        }
    });
    this.updateUserData();
}]);
