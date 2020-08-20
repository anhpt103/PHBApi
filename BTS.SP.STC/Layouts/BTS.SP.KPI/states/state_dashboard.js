define([
], function () {
    var rootUrl = "/_layouts/15/BTS.SP.KPI/";
    var layoutUrl = rootUrl + "views/htdm/";
    var controlUrl = rootUrl + "controllers/htdm/";
    var states = [
        {
            name: 'dashboard',
            url: '/dashboard',
            parent: 'layout',
            abstract: false,
            views: {
                'viewMain@root': {
                    templateUrl: layoutUrl + "dmchucvu/index.html",
                    //controller: "ListItemOnPage_dmchucvu_ctrl as ctrl"
                }
            },
            moduleUrl: controlUrl + "dashboard_ctrl.js"
        }
    ];
    return states;
});