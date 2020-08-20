define([
], function () {
    var layoutUrl = "/_layouts/15/BTS.SP.STC.PHD/views/htdm/";
    var controlUrl = "/_layouts/15/BTS.SP.STC.PHD/controllers/htdm/";
    var states = [
        {
            name: 'dmChiTieuBaoCao',
            url: '/dm_chitieu_baocao',
            parent: 'layout',
            abstract: false,
            views: {
                'viewMain@root': {
                    templateUrl: layoutUrl + "dm_chitieu_baocao/index.html",
                    controller: "dm_chitieu_baocao_ViewCtrl as ctrl"
                }
            },
            moduleUrl: controlUrl + "dm_chitieu_baocao_controller.js"
        },
    ];
    return states;
});