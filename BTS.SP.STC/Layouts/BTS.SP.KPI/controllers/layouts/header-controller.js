
define(['angular'], function (angular) {
    var app = angular.module('headerModule', ['configModule']);

    app.service('HeaderService', ['$http', 'configService', function ($http, configService) {
        var menuApi = configService.rootUrlWebApi + '/Layout';
       
        var result = {
            GetMenus: function () { return $http.get(menuApi + '/GetMenus'); },
           
        };
        return result;
    }]);

    app.controller('HeaderCtrl', function ($scope, $http, HeaderService) {
        $scope.SiteMenu = [];        
        function loadMenu() {
            HeaderService.GetMenus().then(function (response) {
				
                if (response && response.data && response.data.length > 0) {
                    $scope.SiteMenu = angular.copy(response.data);
					console.log($scope.SiteMenu);
					console.log(response);
                } else {
                    console.log(response);
                }
            }, function (response) {
                console.log(response);
            });

        }

        //loadMenu();

        

    });
    return app;
});


