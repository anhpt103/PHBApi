define(['angular', 'controllers/auth/auth-controller'], function (angular) {
    var app = angular.module('headerModule', ['authModule', 'configModule']);
    var layoutUrl = "/_layouts/15/BTS.SP.STC.PHD/";
    app.directive('tree', function () {
        return {
            restrict: "E",
            replace: true,
            scope: {
                tree: '='
            },
            templateUrl: layoutUrl + 'utils/tree/template-ul.html'
        };
    });
    app.directive('leaf', function ($compile) {
        return {
            restrict: "E",
            replace: true,
            scope: {
                leaf: "="
            },
            templateUrl: layoutUrl + 'utils/tree/template-li.html',
            link: function (scope, element, attrs) {
                if (angular.isArray(scope.leaf.Children) && scope.leaf.Children.length > 0) {
                    element.append("<tree tree='leaf.Children'></tree>");
                    element.addClass('dropdown-submenu');
                    $compile(element.contents())(scope);
                } else {
                    //element.bind('click', function () {
                    //    alert("You have clicked on " + scope.leaf.name);
                    //});
                }
            }
        };
    });

    app.service('HeaderService', ['$http', 'configService', function ($http, configService) {
        var menuApi = configService.rootUrlWebApi + "/Dm/DM_MENU";
        var result = {
            getMenu: function (ph) {
                return $http.get(menuApi + '/GetMenuByPH/' + ph);
            }
        };
        return result;
    }]);
    app.controller('HeaderCtrl', ['$scope', 'configService', '$state', 'accountService', '$log', 'userService', 'HeaderService',
    function ($scope, configService, $state, accountService, $log, userService, HeaderService) {

        function treeify(list, idAttr, parentAttr, childrenAttr) {
            if (!idAttr) idAttr = 'Value';
            if (!parentAttr) parentAttr = 'Parent';
            if (!childrenAttr) childrenAttr = 'Children';

            var lookup = {};
            var result = {};
            result[childrenAttr] = [];

            list.forEach(function (obj) {
                lookup[obj[idAttr]] = obj;
                obj[childrenAttr] = [];
            });

            list.forEach(function (obj) {
                if (obj[parentAttr] != null) {
                    lookup[obj[parentAttr]][childrenAttr].push(obj);
                } else {
                    result[childrenAttr].push(obj);
                }
            });

            return result;
        };
        function loadUser() {
            $scope.currentUser = userService.GetCurrentUser();
            if (!$scope.currentUser) {
                $state.go('login');
            }
            HeaderService.getMenu('A').then(function (response) {
                if (response && response.data && response.data.length > 0) {
                    $scope.lstMenu = angular.copy(response.data);
                    $scope.treeMenu = treeify($scope.lstMenu);
                } else {
                    console.log(response);
                }
            }, function (response) {
                console.log(response);
            });

        }

        loadUser();

        $scope.logOut = function () {
            accountService.logout();
        };
    }]);

    return app;
});