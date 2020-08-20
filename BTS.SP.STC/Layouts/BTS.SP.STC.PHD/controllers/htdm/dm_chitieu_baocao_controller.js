// 'use strict';
define(['angular'
], function (angular) {
    var app = angular.module('dm_chitieu_baocao_Module', []);
    app.controller('dm_chitieu_baocao_ViewCtrl', ['$scope', '$location', '$http', 'appSettings', function ($scope, $location, $http, appSettings) {
        console.log(appSettings);
    }]);
    return app;
});


