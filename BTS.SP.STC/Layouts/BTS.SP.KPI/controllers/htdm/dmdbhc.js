
define([], function () {
    'use strict';
    var app = angular.module('dmdbhc_Module', []);
    app.factory('dmdbhc_Service', ['$http', 'configService', function ($http, configService) {
        var serviceUrl = configService.rootUrlWebApi + '/DM/DM_DBHC';
        var result = {
            GetMaHuyen: function (data) {
                return $http.post(serviceUrl + '/GetMaHuyen');
            },            
        }
        return result;
    }]);    
    return app;
});

