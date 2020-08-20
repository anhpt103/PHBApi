'use strict';
define([
    'jquery',  // đây như kiểu dữ liệu
    'angular',
    'states/htdm',
    'states/dashboard',
    'ui-bootstrap',
    'uiRouter',
	'ngRoute',
    'ocLazyLoad',
    'services/configService',
	'angularStorage',
    'services/interceptorService',
    
    'chart-js',
    'angular-chart'
], function (jquery, angular, state_htdm, state_dashboard) {
    'use strict';
        var app = angular.module('mainApp', ['ngRoute', 'ui.router', 'oc.lazyLoad', 'LocalStorageModule', 'InterceptorModule',  'chart.js']);

    app.config(function (localStorageServiceProvider) {
        localStorageServiceProvider.setStorageType('cookie').setPrefix('');
    });

    app.config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push('interceptorService');
    }]);

    // app.config(['$qProvider', function($qProvider) {
    //     $qProvider.errorOnUnhandledRejections(false);
    // }]);

    app.config(function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {
        var lazyDeferred;
        // Set the app module as loaded
        $ocLazyLoadProvider.config({ loadedModules: ['angular', 'app'], asyncLoader: require });
        var layoutUrl = "/_layouts/15/BTS.SP.KPI/";
        $urlRouterProvider.otherwise("/home");

        $stateProvider.state('root', {
            abstract: true,
            views: { 'viewRoot': { templateUrl: layoutUrl + "views/layouts/root.html" } },
            resolve: {
                loadModule: ['$ocLazyLoad', '$q', function ($ocLazyLoad, $q) {
                    var deferred = $q.defer();
                    require(['controllers/layouts/layout-controller'],
                        function (layoutModule) {//url c?a module
                            deferred.resolve();
                            $ocLazyLoad.inject(layoutModule.name);
                        });
                    return deferred.promise;
                }]
            }

        });

        //$stateProvider.state('noheader', {
        //    abstract: true,
        //    views: { 'viewRootNoHeader': { templateUrl: layoutUrl + "views/layouts/rootnoheader.html" } },
        //    resolve: {
        //        loadModule: ['$ocLazyLoad', '$q', function ($ocLazyLoad, $q) {
        //            var deferred = $q.defer();
        //            require(['controllers/layouts/layout-controller'],
        //                function (layoutModule) {//url c?a module
        //                    deferred.resolve();
        //                    $ocLazyLoad.inject(layoutModule.name);
        //                });
        //            return deferred.promise;
        //        }]
        //    }

        //});

        $stateProvider.state('layout', {
            parent: 'root',
            abstract: true,
            views: { 'viewHeader': { templateUrl: layoutUrl + "views/layouts/header.html", controller: "HeaderCtrl as ctrl" } },
            resolve: {
                loadModule: ['$ocLazyLoad', '$q', function ($ocLazyLoad, $q) {
                    var deferred = $q.defer();
                    require(['controllers/layouts/header-controller'],
                        function (headerModule) {
                            deferred.resolve();
                            $ocLazyLoad.inject(headerModule.name);
                        });
                    return deferred.promise;
                }
                ]

            }
        });

        $stateProvider.state('home', {
            url: "/home",
            parent: 'layout',
            abstract: false,
            views: {
                'viewMain@root': {
                    templateUrl: layoutUrl + "views/layouts/home.html",
                    controller: "homeCtrl as ctrl"
                }
            },
           
            resolve: {
                loadModule: ['$ocLazyLoad', '$q', function ($ocLazyLoad, $q) {
                    var deferred = $q.defer();
                    require(['controllers/layouts/home-controller'],
                        function (homeModule) {
                            deferred.resolve();
                            $ocLazyLoad.inject(homeModule.name);
                        });
                    return deferred.promise;
                }]
            }

        });
        $stateProvider.state('login', {
            url: "/login",
            abstract: false,
            views: {
                'viewRoot': {
                    templateUrl: layoutUrl + "views/auth/login.html?t=" + (new Date()),
                    controller: "loginCrtl as ctrl"

                }
            },
            resolve: {
                loadModule: ['$ocLazyLoad', '$q', function ($ocLazyLoad, $q) {
                    var deferred = $q.defer();
                    require(['controllers/auth/auth-controller'],
                        function (layoutModule) {//url c?a module
                            deferred.resolve();
                            $ocLazyLoad.inject(layoutModule.name);
                        });
                    return deferred.promise;
                }]
            }
        });

        var lststate = [];
        lststate = lststate.concat(state_dashboard).concat(state_htdm);
        angular.forEach(lststate, function (state) {
            $stateProvider.state(state.name, {
                url: state.url,
                parent: state.parent,
                abstract: state.abstract,
                views: state.views,
                resolve: {
                    loadModule: ['$ocLazyLoad', '$q', function ($ocLazyLoad, $q) {
                        var deferred = $q.defer();

                        require([state.moduleUrl], function (module) {
                            deferred.resolve();
                            $ocLazyLoad.inject(module.name);
                        });
                        return deferred.promise;
                    }]
                }
            });
        });

        //$locationProvider.html5Mode({
        //       enabled: true,
        //       requireBase: false
        //   });
    });
    return app;
});
