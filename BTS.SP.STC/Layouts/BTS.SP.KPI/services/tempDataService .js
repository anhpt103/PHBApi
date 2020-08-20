define(['angular'], function () {
    var app = angular.module('tempDataModule', []);

    app.factory('tempDataService', ['CacheFactory', function (CacheFactory) {
        var dataCache;
        if (!CacheFactory.get('dataCache')) {
            dataCache = CacheFactory('dataCache');         
            dataCache.put('lstlogin', [
                { PhanHe: 'A', Url: 'http://localhost:55099', subSite: 'http://duy-pc/PHA/'},
                { PhanHe: 'B', Url: 'http://localhost:53414', subSite: 'http://duy-pc/PHB/'},

            ]);
            
        }
        var result = {
            dateFormat: 'dd/MM/yyyy',
            delegateEvent: function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
            }
        };

        result.tempData = function (name) {
            return dataCache.get(name);
        }
        result.putTempData = function (module, data) {
            dataCache.put(module, data);
        }
        return result;
    }
    ]);
    return app;
});
