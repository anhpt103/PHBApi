require.config({
    base: '/',
    paths: {
        'angular': 'lib/angular.min',
        'uiRouter': 'lib/angular-ui-router.min',
        'ocLazyLoad': 'lib/ocLazyLoad.require',
        'bootstrap': 'lib/ui-bootstrap-tpls-2.5.0.min',
        'ui-bootstrap': 'lib/ui-bootstrap-tpls-1.3.3',
        'jquery': 'lib/jquery.min',
        'chart-js': 'lib/Chart.min',
        'angular-chart': 'lib/angular-chart.min',
        'angularStorage': 'lib/angular-local-storage.min',
        'ngRoute': 'https://ajax.googleapis.com/ajax/libs/angularjs/1.2.28//angular-route.min'
    },
    shim: {
       
        'bootstrap': ['jquery'],
        'angular': {
            deps: ['jquery'],
            exports: 'angular'
        },
        'ngRoute': ['angular'],
        'ui-bootstrap': ['angular'],
        'angularStorage': ['angular'],
        'app': ['angular'],
        'chart-js': ['angular'],
        'angular-chart': ['chart-js']
    }
});
require(['angular', 'app'], function (angular) {
    'use strict';
    angular.bootstrap(document.body, ['mainApp']);
});

