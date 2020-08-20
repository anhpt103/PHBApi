
define(['angular', 'controllers/auth/auth-controller'], function (angular) {
    var app = angular.module('homeModule', ['configModule'])

    app.controller('homeCtrl', ['$scope', '$location', 'accountService', function ($scope, $location, accountService) {



        $scope.login = function (item) {
            var url = '';
            if (item == 'F') {
                url = 'http://14.160.26.174:5858';
            } else if (item == 'D') {
                url = 'http://ift.edu.vn';
            }
            else {

                var lstUrl = window.location.href.split('/');


                url = lstUrl[0] + lstUrl[1] + '/' + lstUrl[3] + '/ph' + item.toLowerCase();


            }
            console.log(url);
            window.open(url);
        };
    }]);
    return app;
});

