define([
], function () {
    var rootUrl = "/_layouts/15/BTS.SP.KPI/";
    var layoutUrl = rootUrl + "views/dashboard/";
    var controlUrl = rootUrl + "controllers/dashboard/";
    var states = [
        {
            name: 'dashboard',
            url: '/dashboard',
            parent: 'layout',
            abstract: false,
            views: {
                'viewMain@root': {
                    templateUrl: layoutUrl + "index.html",
                    //controller: "ListItemOnPage_dmchucvu_ctrl as ctrl"
                }
            },
            moduleUrl: controlUrl + "dashboard_ctrl.js"
        },
        {
            name: 'dashboard_pha',
            url: '/dashboard_pha',
            parent: 'layout',
            abstract: false,
            views: {
                'viewMain@root': {
                    templateUrl: layoutUrl + "pha/index.html",
                    controller: "dashboard_pha_ctrl as ctrl"
                }
            },
            moduleUrl: controlUrl + "dashboard_pha_ctrl.js"
        }
        ,
        {
            name: 'dashboard_pha_list',
            url: '/dashboard_pha_list',
            parent: 'layout',
            abstract: false,
            views: {
                'viewMain@root': {
                    templateUrl: layoutUrl + "pha/list.html",
                    controller: "dashboard_pha_list_ctrl as ctrl"
                }
            },
            moduleUrl: controlUrl + "dashboard_pha_list_ctrl.js"
        },
         {
             name: 'dashboard_pht',
             url: '/dashboard_pht',
             parent: 'layout',
             abstract: false,
             views: {
                 'viewMain@root': {
                     templateUrl: layoutUrl + "pht/index.html",
                     controller: "dashboard_pht_ctrl as ctrl"
                 }
             },
             moduleUrl: controlUrl + "dashboard_pht_ctrl.js"
         }
    ];
    return states;
});