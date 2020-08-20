'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {
	var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var authServiceFactory = {};
	
	 var _authentication = {
        isAuth: false,
        userName: "",
        useRefreshTokens: false
    };

    var _externalAuthData = {
        provider: "",
        userName: "",
        externalAccessToken: ""
    };
	
	 var _logOut = function () {
        localStorageService.remove('authorizationData');
        _authentication.isAuth = false;
        _authentication.userName = "";
        _authentication.useRefreshTokens = false;

    };
	
	
}]);