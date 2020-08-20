// 'use strict';
define(['angular'], function (angular) {

    var app = angular.module('authModule', ['configModule']);

    app.service('userService', ['localStorageService', '$q', function (localStorageService, $q) {
        console.log('aaaa');	
    var authServiceFactory = {};
    
    var  _SetCurrentUser=function(user,PHANHE)
    {
        var authorizationData = "stcPH" + PHANHE + ".authorizationData";
        localStorageService.set(authorizationData, user);            
    }

		
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
		
		var authData = localStorageService.get('FedAuth');
		
        _authentication.isAuth = false;
        _authentication.userName = "";
        _authentication.useRefreshTokens = false;

    };
	
	var _login = function (loginData) {
        console.log(loginData);
        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        if (loginData.useRefreshTokens) {
            data = data + "&client_id=" + ngAuthSettings.clientId;
        }

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            if (loginData.useRefreshTokens) {
                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: response.refresh_token, useRefreshTokens: true });
            }
            else {
                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: "", useRefreshTokens: false });
            }
            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;
            _authentication.useRefreshTokens = loginData.useRefreshTokens;

            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

	};

    

	var _refreshToken = function () {
        var deferred = $q.defer();

        var authData = localStorageService.get('authorizationData');

        if (authData) {

            if (authData.useRefreshTokens) {

                var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + ngAuthSettings.clientId;

                localStorageService.remove('authorizationData');

                $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                    localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true });

                    deferred.resolve(response);

                }).error(function (err, status) {
                    _logOut();
                    deferred.reject(err);
                });
            }
        }

        return deferred.promise;
    };
	
	authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.SetCurrentUser=_SetCurrentUser;
	
    return authServiceFactory;
    }]);

    app.service('accountService', ['configService', '$http', '$q', 'localStorageService', '$state', 'userService', function (configService, $http, $q, localStorageService, $state, userService) {
        console.log(configService.apiServiceBaseUriPHB);    
        var result = {
            login: function (user) {
                console.log(user);
                var obj = { 'username': user.username, 'password': user.password, 'grant_type': 'password', PHANHE: user.PHANHE };
                if (user.PHANHE == 'A') {
                    urltoken = configService.apiServiceBaseUriPHA;
                } else if (user.PHANHE == 'B') {
                    urltoken = configService.apiServiceBaseUriPHB;
                } else if (user.PHANHE == 'C') {
                    urltoken = configService.apiServiceBaseUriPHC;
                }
                var url = configService.rootUrlWeb + "/PH" + obj.PHANHE;
               
                Object.toparams = function ObjectsToParams(obj) {
                    var p = [];
                    for (var key in obj) {
                        p.push(key + '=' + encodeURIComponent(obj[key]));
                    }
                    return p.join('&');
                }
                var defer = $q.defer();
                $http({ method: 'post', url: urltoken  + "/token", data: Object.toparams(obj), headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (response) {
                    console.log(response);
                    if (response && response.status === 200 && response.data) {
                        userService.SetCurrentUser(response.data, user.PHANHE);                                                                
                        window.open(url);                                          
                    }
                    defer.resolve(response);
                }, function (response) {
                    defer.reject(response);
                });
                return defer.promise;
            },
            logout: function () {
                localStorageService.cookie.clearAll();
                $state.go('login');
            }
        };
        return result;
    }]);

    app.controller('loginCrtl', ['$scope', '$location', '$http', 'localStorageService', 'accountService', '$state', function ($scope, $location, $http, localStorageService, accountService, $state) {
        $scope.user = { username: '', password: '', cookie: false, grant_type: 'password' };
        console.log($scope.user);
        var config = {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        };
        $scope.login = function () {
            $scope.msg = null;
            accountService.login($scope.user).then(function (response) {               
            }, function (response) {
                if (response && response.data) {
                    if (response.data.error) {
                        $scope.msg = response.data.error_description;
                    }
                }
                $scope.user = { username: '', password: '', cookie: false, grant_type: 'password' };
                $scope.focusUsername = true;
            });
        };
    }]);
    
	return app;
});
