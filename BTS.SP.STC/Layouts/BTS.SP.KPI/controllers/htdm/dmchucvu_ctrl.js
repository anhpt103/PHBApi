/*  
* Người tạo : Nguyễn Sĩ Thiệu
*   Last modify:
* View: BTS.SP.STC.PHC/views/htdm/dmtktn.ctrl
* Sevices: Dm/PHC/dmtktn
* Class sevices: DM_TKTNSercive  , DM_TKTNVm
* Entity: Models/Dm/PHC/DM_TKTN
* Menu: Danh muc -> 2.19 Danh mục tài khoản tự nhiên
*/
define(['ui-bootstrap'], function () {
    'use strict';
    var app = angular.module('dmchucvu_Module', ['ui-bootstrap']);
    app.factory('dmchucvu_Service', ['$http', 'configService', function ($http, configService) {
        var serviceUrl = configService.rootUrlWebApi + '/DM/DM_CHUCVU';
        var result = {
            Select_Page: function (data) {
                return $http.post(serviceUrl + '/Select_Page', data);
            },
            addNew: function (data) {
                return $http.post(serviceUrl + '/AddNew', data);
            },
            update: function (params) {
                return $http.put(serviceUrl + '/Update/' + params.ID, params);
            },
            deleteItem: function (params) {
                return $http.put(serviceUrl + '/DeleteItem/' + params.ID, params);
            },
            Export: function (data) {
                return $http.post(serviceUrl + '/Export', data, { responseType: 'arraybuffer' });
            },
            getAllChucVu: function () {
                return $http.get(serviceUrl + '/getAllChucVu');
            }
        }
        return result;
    }]);
    /* controller list */
    app.controller('ListItemOnPage_CHUCVU_ctrl', ['$scope', '$location', '$http', 'configService', 'dmchucvu_Service', 'tempDataService', '$filter', '$uibModal', '$log', 'ngNotify', 'securityService',
        function ($scope, $location, $http, configService, service, tempDataService, $filter, $uibModal, $log, ngNotify, securityService) {
            $scope.config = angular.copy(configService);
            $scope.paged = angular.copy(configService.pageDefault);
            $scope.filtered = angular.copy(configService.paramDefault);

            $scope.tempData = tempDataService.tempData;
            $scope.accessList = {};
            function filterData() {
                if ($scope.accessList.View) {
                    $scope.isLoading = true;
                    var postdata = { paged: $scope.paged, filtered: $scope.filtered };
                    service.Select_Page(postdata).then(function (successRes) {
                        console.log(successRes);
                        if (successRes && successRes.status === 200 && successRes.data && successRes.data.Status) {
                            $scope.isLoading = false;
                            $scope.data = successRes.data.Data.Data;
                            angular.extend($scope.paged, successRes.data.Data);
                        }
                    }, function (errorRes) {
                        console.log(errorRes);
                    });
                }
            };
            filterData();

            function loadAccessList() {
                securityService.getAccessList('pha_dmchucvu').then(function (successRes) {
                    if (successRes && successRes.status == 200) {
                        $scope.accessList = successRes.data;
                        if (!$scope.accessList.View) {
                            //ngNotify.set("Không có quyền truy cập !", { duration: 3000, type: 'error' });
                            window.location.href = "#!/AccessDenied";
                        } else {
                            filterData();
                        }
                    } else {
                        //ngNotify.set("Không có quyền truy cập !", { duration: 3000, type: 'error' });
                        window.location.href = "#!/AccessDenied";
                    }
                }, function (errorRes) {
                    console.log(errorRes);
                    //ngNotify.set("Không có quyền truy cập !", { duration: 3000, type: 'error' });
                    $scope.accessList = null;
                    window.location.href = "#!/AccessDenied";
                });
            }

            loadAccessList();

            $scope.displayHepler = function (module, value) {
                var data = $filter('filter')($scope.tempData(module), { Value: value }, true);
                if (data.length === 1) {
                    return data[0].Text;
                } else {
                    return value;
                }
            }

            /* Function Select page */

            $scope.setPage = function (pageNo) {
                $scope.paged.currentPage = pageNo;
                filterData();
            };
            $scope.sortType = 'MA_CHUCVU';
            $scope.sortReverse = false;
            $scope.doSearch = function () {
                $scope.paged.currentPage = 1;
                filterData();
            };
            $scope.pageChanged = function () {
                filterData();
            };
            $scope.goHome = function () {
                window.location.href = "#!/home";
            };
            $scope.refresh = function () {
                $scope.setPage($scope.paged.currentPage);
            };
            /* Function add New Item */
            $scope.addNew = function () {
                var modalInstance = $uibModal.open({
                    backdrop: 'static',
                    size: 'md',
                    templateUrl: configService.buildUrl('htdm/dmchucvu', 'addNew'),
                    controller: 'addNew_CHUCVU_ctrl',
                    resolve: {}
                });
                modalInstance.result.then(function (updatedData) {
                    $scope.refresh();
                }, function () {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            };

            /* Function Edit Item */
            $scope.edit = function (target) {
                var modalInstance = $uibModal.open({
                    backdrop: 'static',
                    templateUrl: configService.buildUrl('htdm/dmchucvu', 'edit'),
                    controller: 'Edit_CHUCVU_ctrl',
                    resolve: {
                        targetData: function () {
                            return target;
                        }
                    }
                });
                modalInstance.result.then(function (updatedData) {
                    $scope.refresh();
                }, function () {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            };

            /* Function Delete Item */
            $scope.deleteItem = function (target) {
                var modalInstance = $uibModal.open({
                    backdrop: 'static',
                    templateUrl: configService.buildUrl('htdm/dmchucvu', 'delete'),
                    controller: 'Delete_CHUCVU_ctrl',
                    resolve: {
                        targetData: function () {
                            return target;
                        }
                    }
                });
                modalInstance.result.then(function (updatedData) {
                    $scope.refresh();
                }, function () {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            };
            /* End Function Select page */

        }]);

    /* controller addNew */
    app.controller('addNew_CHUCVU_ctrl', ['$scope', '$uibModalInstance', '$location', '$http', 'configService', 'dmchucvu_Service', 'tempDataService', '$filter', '$uibModal', '$log', 'ngNotify',
        function ($scope, $uibModalInstance, $location, $http, configService, service, tempDataService, $filter, $uibModal, $log, ngNotify) {
            $scope.config = angular.copy(configService);
            $scope.tempData = tempDataService.tempData;
            $scope.statusStr = tempDataService.tempData('statusStr');
            $scope.target = { TRANG_THAI: '' };
            $scope.target.TRANG_THAI = $scope.statusStr[0].Value;
            $scope.isLoading = false;
            $scope.title = function () { return 'Thêm mới'; };
            $scope.addNew = function () {
                service.addNew($scope.target).then(function (successRes) {
                    if (successRes && successRes.status === 200 && successRes.data.Status) {
                        ngNotify.set("Thêm mới thành công !", { duration: 3000, type: 'success' });
                        $uibModalInstance.close($scope.target);
                    } else {
                        console.log('addNew successRes', successRes);
                        ngNotify.set(successRes.data.Message, { duration: 3000, type: 'error' });
                    }
                },
                    function (errorRes) {
                        console.log('errorRes', errorRes);
                    });
            };

            $scope.cancel = function () {
                $uibModalInstance.close();
            };

        }]);

    /* controller Edit */
    app.controller('Edit_CHUCVU_ctrl', ['$scope', '$uibModalInstance', '$location', '$http', 'configService', 'dmchucvu_Service', 'tempDataService', '$filter', '$uibModal', '$log', 'targetData', 'ngNotify',
        function ($scope, $uibModalInstance, $location, $http, configService, service, tempDataService, $filter, $uibModal, $log, targetData, ngNotify) {
            $scope.config = angular.copy(configService);
            $scope.tempData = tempDataService.tempData;
            $scope.statusStr = tempDataService.tempData('statusStr');
            $scope.target = { TRANG_THAI: '' };
            $scope.target.TRANG_THAI = $scope.statusStr[0].Value;
            $scope.targetData = angular.copy(targetData);
            $scope.target = targetData;
            $scope.isLoading = false;
            $scope.title = function () { return 'Cập nhật thông tin'; };
            $scope.save = function () {
                service.update($scope.target).then(function (successRes) {
                    if (successRes && successRes.status === 200) {
                        ngNotify.set("Sửa thành công !", { duration: 3000, type: 'success' });
                        $uibModalInstance.close($scope.target);
                    } else {
                        console.log('update successRes', successRes);
                        ngNotify.set(successRes.data.Message, { duration: 3000, type: 'error' });
                    }
                },
                    function (errorRes) {
                        console.log('errorRes', errorRes);
                    });
            };
            $scope.cancel = function () {
                $uibModalInstance.close();
            };

        }]);

    /* controller delete */
    app.controller('Delete_CHUCVU_ctrl', ['$scope', '$uibModalInstance', '$location', '$http', 'configService', 'dmchucvu_Service', 'tempDataService', '$filter', '$uibModal', '$log', 'targetData', 'ngNotify',
        function ($scope, $uibModalInstance, $location, $http, configService, service, tempDataService, $filter, $uibModal, $log, targetData, ngNotify) {
            $scope.config = angular.copy(configService);
            $scope.targetData = angular.copy(targetData);
            $scope.target = targetData;
            $scope.isLoading = false;
            $scope.title = function () { return 'Xoá thành phần'; };
            $scope.save = function () {
                service.deleteItem($scope.target).then(function (successRes) {
                    if (successRes && successRes.status === 200) {
                        ngNotify.set("Xóa thành công !", { duration: 3000, type: 'success' });
                        $uibModalInstance.close($scope.target);
                    } else {
                        console.log('deleteItem successRes ', successRes);
                        ngNotify.set(successRes.data.Message, { duration: 3000, type: 'error' });
                    }
                },
                    function (errorRes) {
                        console.log('errorRes', errorRes);
                    });
            };
            $scope.cancel = function () {
                $uibModalInstance.close();
            };

        }]);
    return app;
});

