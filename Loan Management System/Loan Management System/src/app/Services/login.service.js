app.service('LoginService', ['$resource', 'jwtHelper', function ($resource, jwtHelper) {
    this.userName = 'DefaultUser';
    this.setToken = function (token) {
        sessionStorage.setItem('token', token);
        this.updateUserData();
    }
    this.removeToken = function () {
        sessionStorage.removeItem('token');
        this.userName = 'DefaultUser';
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
        if (token != null) {
            console.log('Login Found in session', token);
            var decoded = jwtHelper.decodeToken(token);
            this.userName = decoded.name;
        }
    };

    this.apiResource = $resource('/api/validation/ValidateUser', {}, {
        validateUser: {
            url: '/api/validation/ValidateUser',
            method: 'POST'
        },
        createUser: {
            url: '/api/validation/CreateUser',
            method: 'POST'
        },
        deleteUser: {
            url: '/api/validation/DeleteUser',
            method: 'DELETE',
            params: { userId: '@userId' }
        }
    });
    this.updateUserData();
}]);
