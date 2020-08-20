define(['angular'], function () {
    var app = angular.module('tempDataModule', []);

    app.factory('tempDataService',['CacheFactory',function (CacheFactory) {
        var profileCache;
        if (!CacheFactory.get('profileCache')) {
            profileCache = CacheFactory('profileCache');
        }

        profileCache.put('statusInt', [
            {
                Text: 'Sử dụng',
                Value:1
            },
            {
                Text: 'Không sử dụng',
                Value: 0
            }
        ]);
        profileCache.put('statusStr', [
            {
                Text: 'Sử dụng',
                Value: 'A'
            },
            {
                Text: 'Không sử dụng',
                Value: 'I'
            }
        ]);

        var result = {
            dateFormat: 'dd/MM/yyyy',
            delegateEvent: function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
            }
        };

        result.tempData=function(name) {
            return profileCache.get(name);
        }

        return result;
    }
    ]);
    return app;
});
