define([
], function () {
    var rootUrl = "/_layouts/15/BTS.SP.KPI/";
    var layoutUrl = rootUrl + "views/htdm/";
    var controlUrl = rootUrl + "controllers/htdm/";
    var states = [
        {
            name: 'dmchucvu',
            url: '/dmchucvu',
            parent: 'layout',
            abstract: false,
            views: {
                'viewMain@root': {
                    templateUrl: layoutUrl + "dmchucvu/index.html",
                    controller: "ListItemOnPage_dmchucvu_ctrl as ctrl"
                }
            },
            moduleUrl: controlUrl + "dmchucvu_ctrl.js"
        },
        {
            name: 'dmChiTieuBaoCao',
            url: '/dmChiTieuBaoCao',
            parent: 'layout',
            abstract: false,
            views: {
                'viewMain@root': {
                    templateUrl: layoutUrl + "dmChiTieuBaoCao/index.html",
                    controller: "ListItemOnPage_ChiTieuBaoCao_ctrl as ctrl"
                }
            },
            moduleUrl: controlUrl + "dmchitieubaocao_ctrl.js"
        }
       
    ];
    return states;
});