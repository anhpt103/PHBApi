'use strict';
define(['angular','controllers/auth/auth-controller'], function () {

    var app = angular.module('InterceptorModule', ['authModule','ui.router']);

    app.factory('interceptorService', ['$q', '$injector','$location','localStorageService','userService','$state', function ($q, $injector,$location,localStorageService,userService,$state) {
        var interceptorServiceFactory = {};
		
        var _request = function (config) {
            config.headers = config.headers || {};
            var authData = localStorageService.get('authorizationData');
            console.log(authData);
            if (authData) {

                if (request.url.indexOf(configService.apiServiceBaseUri) >= 0) {
                    request.headers.Authorization = 'Bearer ' + currentUser.access_token;
                }
            }
			return config;
        }
		
		
		var _responseError = function (rejection) {
        if (rejection.status === 401) {			
            var authData = localStorageService.get('authorizationData');			
            if (authData) {				
                if (authData.useRefreshTokens) {
                    //$location.path('/refresh');
                    return $q.reject(rejection);
                }
            }
            userService.logOut();		
			$state.go('login');         
        }
        return $q.reject(rejection);
    }
      
	interceptorServiceFactory.request = _request;
	interceptorServiceFactory.responseError = _responseError;
	return interceptorServiceFactory;
    }]);

	
    return app;
});