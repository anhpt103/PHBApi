﻿define(['angular'], function () {
    var app = angular.module('configModule', []);
    app.factory('configService', function () {
        var hostname = window.location.hostname;
        var port = window.location.port;
        var rootUrl = 'http://' + hostname + ':' + port;
        var lstUrl = window.location.href.split('/');
        var rootUrlApi = 'http://localhost:51056'
        //var rootUrlApi = 'http://api.btsoftvn.com:8383/' + lstUrl[3] + "/kpi";
        var PHA = 'http://localhost:55099';
        var PHB = 'http://localhost:53414';
        var PHC = 'http://localhost:1155'

        //site SharePonit
      

        if (!port) {
            rootUrl = 'http://' + hostname;
        }
        var result = {
            rootUrlWeb: rootUrl,
            rootUrlWebApi: rootUrlApi + '/api',
            apiServiceBaseUri: rootUrlApi,
            apiServiceBaseUriPHA: PHA,
            apiServiceBaseUriPHB: PHB,
            apiServiceBaseUriPHC: PHC,
            dateFormat: 'dd/MM/yyyy',
            delegateEvent: function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
            }
        };
        result.buildUrl = function (folder, file) {
            return this.rootUrlWeb + "/_layouts/15/BTS.SP.KPI/views/" + folder + "/" + file + ".html";
        };
        result.pageDefault = {
            totalItems: 0,
            itemsPerPage: 10,
            currentPage: 1,
            pageSize: 5,
            totalPage: 5,
            maxSize: 5
        };
        result.paramDefault = {
            isAdvance: false,
            AdvanceData: {},
            PageSize: 5,
            Page: 5,
            OrderBy: '',
            OrderType: 'ASC',
            Keyword: '',
            Summary: '',
            advanceData: {},

        };
        result.filterDefault = {
            summary: '',
            isAdvance: false,
            advanceData: {},
            orderBy: '',
            orderType: 'ASC',
        };
        var label = {
            lblMessage: 'Thông báo',
            lblNotifications: 'Thông báo',
            lblindex: '',
            lblDetails: 'Thông tin',
            lblEdit: 'Cập nhập',
            lblCreate: 'Thêm',
            lbl: '',
            btnCreate: 'Thêm mới',
            btnImport: 'Cập nhật từ tệp excel',
            btnEdit: 'Sửa',
            btnDelete: 'Xóa',
            btnRemove: 'Xóa',
            btnActive: 'Active',
            btnInactive: 'Inactive',
            btnToggle: 'Toggle',
            btnSaveAndKeep: 'Lưu và giữ lại',
            btnSaveAndPrint: 'Lưu và in phiếu',

            btnSearch: 'Tìm kiếm',
            btnRefresh: 'Làm mới',
            btnBack: 'Quay lại',
            btnClear: 'Xóa tất cả',
            btnCancel: 'Hủy',

            btnSave: 'Lưu lại',
            btnSubmit: 'Lưu',

            btnLogOn: 'Đăng nhập',
            btnLogOff: 'Đăng xuất',
            btnChangePassword: 'Đổi mật khẩu',

            btnSendMessage: 'Gửi tin nhắn',
            btnSendNotification: 'Gửi thông báo',
            btnNotifications: 'Thông báo',

            btnDisconnect: 'Hủy kết nối',
            btnDisconnectSession: 'Hủy kết nối',
            btnDisconnectAccount: 'Hủy mọi kết nối',

            btnUpload: 'Upload',
            btnUploadAll: 'Upload tất cả',
            btnFileCancel: 'Hủy',
            btnFileCancelAll: 'Hủy tất cả',
            btnFileRemove: 'Xóa',
            btnFileRemoveAll: 'Xóa tất cả',
            btn: '',

            btnImportExcel: 'Import từ file excel',
            btnExportExcel: 'Xuất ra file excel',

            btnCall: 'Call',
            btnChart: 'Biểu đồ',
            btnData: 'Số liệu',
            btnPrint: 'In phiếu',
            btnExit: 'Thoát',
            btnExportPDF: 'Kết xuất file PDF',
            btnExport: 'Kết xuất',

            btnPrintList: 'In DS',
            btnPrintDetailList: 'In DS chi tiết',
            btnSend: 'DS duyệt',
            btnApproval: 'Duyệt',
            btnComplete: 'Hoàn thành',
            btnAddInfo: 'Bổ sung'
        };

        result.label = label;

        return result;
    }
    );
    return app;
});
