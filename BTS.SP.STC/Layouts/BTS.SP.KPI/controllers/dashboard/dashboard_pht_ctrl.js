define(['angular', 'ui-bootstrap', 'controllers/htdm/dmdbhc'], function () {
    'use strict';


    var app = angular.module('dashboard_pht_Module', ['dmdbhc_Module', 'ui.bootstrap']);

    app.service('dashboardphtService', ['$http', 'configService', function ($http, configService) {
        var ServiceUrl = configService.rootUrlWebApi + '/dashboard/pha';
        var result = {
            GetMaHuyen: function () {
                return $http.get(ServiceUrl + '/GetMaHuyen');
            },
            Get_TH_TC_TOANTINH: function (data) {
                return $http.post(ServiceUrl + '/Get_TH_TC_TOANTINH', data);
            },
            Get_TONG_THU: function (data) {
                return $http.post(ServiceUrl + '/Get_TONG_THU', data);
            },
            Get_TONG_THU_NSDP: function (data) {
                return $http.post(ServiceUrl + '/Get_TONG_THU_NSDP', data);
            }
        };
        return result;
    }]);



    app.controller('dashboard_pht_ctrl', ['dashboardphtService', '$scope', 'configService', 'userService', 'dmdbhc_Service', '$uibModal', function (dashboardphtService, $scope, configService, userService, dmdbhc_Service, $uibModal) {
        var currentTime = new Date();
        $scope.currentYear = currentTime.getFullYear();
        $scope.listNAM = [];
        $scope.fromTHANG = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
        $scope.toTHANG = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
        $scope.target = { DONVITINH: 1000000, LOAIQT: 'T', NAM: $scope.currentYear, FROM_THANG: 1, TO_THANG: 12, DIA_BAN: 15 };
        for (var i = $scope.currentYear - 4; i <= $scope.currentYear + 2; i++) {
            $scope.listNAM.push(i);
        }
        $scope.tongThuNSNN = 0;
        $scope.tongThuNSDP = 0;
        $scope.tongChi = 0;
        $scope.MA_DBHC = [];



        function LoadMaDiaBan() {
            dashboardphtService.GetMaHuyen().then(function (response) {
                if (response && response.data && response.data.length > 0) {
                    $scope.MA_DBHC = response.data;



                } else {
                    console.log(response);
                }
            }, function (response) {
                console.log(response);
            });

        }
        LoadMaDiaBan();

        // .....


        function LoadKPI() {
            var data = {
                MA_DIABAN: $scope.target.DIA_BAN,
                NAM: $scope.currentYear,
                TU_THANG: $scope.target.FROM_THANG,
                DEN_THANG: $scope.target.TO_THANG,
            }

            dashboardphtService.Get_TH_TC_TOANTINH(data).then(function (response) {
                console.log(response.data);
                for (var i = 0 ; i < response.data.length; i++) {
                    if (response.data[i].stt == 1) {
                        $scope.tongThuNSNN += response.data[i].tongtien
                        console.log($scope.tongThuNSNN);
                    }
                    if (response.data[i].stt == 2) {
                        $scope.tongThuNSDP += response.data[i].tongtien
                        console.log($scope.tongThuNSDP);
                    }
                    if (response.data[i].stt == 3) {
                        $scope.tongChi += response.data[i].tongtien
                        console.log($scope.tongChi);
                    }


                }
            });
        }
        LoadKPI();

        $scope.Updatedata = function () {
            $scope.tongThuNSNN = 0;
            $scope.tongThuNSDP = 0;
            $scope.tongChi = 0;
            LoadKPI();
        }

        $scope.DetailTTNSNN = function () {
            var modalInstance = $uibModal.open({
                windowClass: 'app-modal-window',
                backdrop: 'static',
                templateUrl: configService.buildUrl('dashboard/pht/detail', 'tongthunsnn'),
                controller: 'tongthunsnn_Ctrl',
                resolve: {
                    targetData: function () {
                        return $scope.target;
                    }
                }
            });
        }

        $scope.DetailTTNSDP = function () {
            var modalInstance = $uibModal.open({
                windowClass: 'app-modal-window',
                backdrop: 'static',
                templateUrl: configService.buildUrl('dashboard/pht/detail', 'tongthunsdp'),
                controller: 'tongthunsdp_Ctrl',
                resolve: {
                    targetData: function () {
                        return $scope.target;
                    }
                }
            });
        }

        $scope.DetailTC = function () {
            var modalInstance = $uibModal.open({
                windowClass: 'app-modal-window',
                backdrop: 'static',
                templateUrl: configService.buildUrl('dashboard/pht/detail', 'tongchi'),
                controller: 'tongchi_Ctrl',
                resolve: {
                    targetData: function () {
                        return $scope.target;
                    }
                }
            });
        }



    }]);

    app.controller('tongthunsnn_Ctrl', ['dashboardphtService', '$scope', 'configService', 'userService', 'dmdbhc_Service', '$uibModal', 'targetData', '$uibModalInstance', function (dashboardphtService, $scope, configService, userService, dmdbhc_Service, $uibModal, targetData, $uibModalInstance) {
        $scope.target = angular.copy(targetData);
        $scope.result = []
        $scope.config = angular.copy(configService);

        //Biều đồ lấy theo năm
        $scope.labelBieu1 = [];
        $scope.dataBieu1 = [];
        $scope.labelBieu2 = [];
        $scope.seriesBieu2 = [];
        $scope.dataBieu2 = [];
        $scope.dataBieu2cot1 = [];
        $scope.dataBieu2cot2 = [];
        $scope.dataBieu2cot3 = [];
        $scope.dataBieu3 = [];
        $scope.dataBieu4 = [];


        //Biểu đồ lấy theo huyện
        $scope.dataBieu5 = [];
        $scope.labelBieu6 = [];
        $scope.dataBieu6 = [];
        $scope.dataBieu6cot1 = [];
        $scope.dataBieu6cot2 = [];
        $scope.dataBieu6cot3 = [];
        $scope.dataBieu7 = [];
        $scope.dataBieu8 = [];


        $scope.barColor4 = ['#3CBA9F', '#e8c3b9'];
        $scope.options = {
            legend: { display: true },
            scales: {
                yAxes: [
                  {
                      id: 'y-axis',
                      min: 0,
                      ticks: {
                          beginAtZero: true,
                          callback: function (value) {
                              return (value + '').replace(/(\d)(?=(\d{3})+$)/g, '$1,');
                          }
                      }
                  }
                ]
            }

        };
        function LoadDetailTongThu() {
            var data = {
                MA_DIABAN: targetData.DIA_BAN,
                NAM: targetData.NAM,
                TU_THANG: targetData.FROM_THANG,
                DEN_THANG: targetData.TO_THANG,
            }
            dashboardphtService.Get_TONG_THU(data).then(function (response) {
                console.log(response);
                $scope.result = response.data;
                // Lấy theo năm
                for (var i = 0 ; i < $scope.result.length; i++) {
                    if ($scope.result[i].nam == targetData.NAM && $scope.result[i].mA_DIABAN == 15) {
                        $scope.dataBieu1.push($scope.result[i]);
                        $scope.dataBieu2cot1.push($scope.result[i].tongtien);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 1 && $scope.result[i].mA_DIABAN == 15) {
                        $scope.dataBieu2cot2.push($scope.result[i].tongtien);
                        $scope.dataBieu3.push($scope.result[i]);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 2 && $scope.result[i].mA_DIABAN == 15) {
                        $scope.dataBieu2cot3.push($scope.result[i].tongtien);
                        $scope.dataBieu4.push($scope.result[i]);
                    }
                }
                $scope.dataBieu2.push($scope.dataBieu2cot3);
                $scope.dataBieu2.push($scope.dataBieu2cot2);
                $scope.dataBieu2.push($scope.dataBieu2cot1);
                $scope.labelBieu2.push("Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12");
                $scope.seriesBieu2.push(targetData.NAM - 2, targetData.NAM - 1, targetData.NAM);
                // end lấy theo năm
                // Lấy theo huyện					

                for (var i = 0 ; i < $scope.result.length; i++) {
                    if ($scope.result[i].nam == targetData.NAM && $scope.result[i].mA_DIABAN != 15) {
                        $scope.labelBieu6.push($scope.result[i].mA_DIABAN);
                        $scope.dataBieu6cot1.push($scope.result[i].tongtien);
                        $scope.dataBieu5.push($scope.result[i]);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 1 && $scope.result[i].mA_DIABAN != 15) {
                        $scope.dataBieu6cot2.push($scope.result[i].tongtien);
                        $scope.dataBieu7.push($scope.result[i]);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 2 && $scope.result[i].mA_DIABAN != 15) {
                        $scope.dataBieu6cot3.push($scope.result[i].tongtien);
                        $scope.dataBieu8.push($scope.result[i]);
                    }
                }
                $scope.dataBieu6.push($scope.dataBieu6cot3);
                $scope.dataBieu6.push($scope.dataBieu6cot2);
                $scope.dataBieu6.push($scope.dataBieu6cot1);

                //end Lấy theo huyện
            });
        }
        LoadDetailTongThu();

        $scope.cancel = function () {
            $uibModalInstance.close();
        };




    }]);

    app.controller('tongthunsdp_Ctrl', ['dashboardphtService', '$scope', 'configService', 'userService', 'dmdbhc_Service', '$uibModal', 'targetData', '$uibModalInstance', function (dashboardphtService, $scope, configService, userService, dmdbhc_Service, $uibModal, targetData, $uibModalInstance) {
        $scope.target = angular.copy(targetData);
        $scope.result = []
        $scope.config = angular.copy(configService);

        //Biều đồ lấy theo năm
        $scope.labelBieu1 = [];
        $scope.dataBieu1 = [];
        $scope.labelBieu2 = [];
        $scope.seriesBieu2 = [];
        $scope.dataBieu2 = [];
        $scope.dataBieu2cot1 = [];
        $scope.dataBieu2cot2 = [];
        $scope.dataBieu2cot3 = [];
        $scope.dataBieu3 = [];
        $scope.dataBieu4 = [];


        //Biểu đồ lấy theo huyện
        $scope.dataBieu5 = [];
        $scope.labelBieu6 = [];
        $scope.dataBieu6 = [];
        $scope.dataBieu6cot1 = [];
        $scope.dataBieu6cot2 = [];
        $scope.dataBieu6cot3 = [];
        $scope.dataBieu7 = [];
        $scope.dataBieu8 = [];


        $scope.barColor4 = ['#3CBA9F', '#e8c3b9'];
        $scope.options = {
            legend: { display: true },
            scales: {
                yAxes: [
                  {
                      id: 'y-axis',
                      min: 0,
                      ticks: {
                          beginAtZero: true,
                          callback: function (value) {
                              return (value + '').replace(/(\d)(?=(\d{3})+$)/g, '$1,');
                          }
                      }
                  }
                ]
            }

        };
        function LoadDetailTongThu() {
            var data = {
                MA_DIABAN: targetData.DIA_BAN,
                NAM: targetData.NAM,
                TU_THANG: targetData.FROM_THANG,
                DEN_THANG: targetData.TO_THANG,
            }
            dashboardphtService.Get_TONG_THU_NSDP(data).then(function (response) {
                console.log(response);
                $scope.result = response.data;
                // Lấy theo năm
                for (var i = 0 ; i < $scope.result.length; i++) {
                    if ($scope.result[i].nam == targetData.NAM && $scope.result[i].mA_DIABAN == 15) {
                        $scope.dataBieu1.push($scope.result[i]);
                        $scope.dataBieu2cot1.push($scope.result[i].tongtien);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 1 && $scope.result[i].mA_DIABAN == 15) {
                        $scope.dataBieu2cot2.push($scope.result[i].tongtien);
                        $scope.dataBieu3.push($scope.result[i]);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 2 && $scope.result[i].mA_DIABAN == 15) {
                        $scope.dataBieu2cot3.push($scope.result[i].tongtien);
                        $scope.dataBieu4.push($scope.result[i]);
                    }
                }
                $scope.dataBieu2.push($scope.dataBieu2cot3);
                $scope.dataBieu2.push($scope.dataBieu2cot2);
                $scope.dataBieu2.push($scope.dataBieu2cot1);
                $scope.labelBieu2.push("Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12");
                $scope.seriesBieu2.push(targetData.NAM - 2, targetData.NAM - 1, targetData.NAM);
                // end lấy theo năm
                // Lấy theo huyện					

                for (var i = 0 ; i < $scope.result.length; i++) {
                    if ($scope.result[i].nam == targetData.NAM && $scope.result[i].mA_DIABAN != 15) {
                        $scope.labelBieu6.push($scope.result[i].mA_DIABAN);
                        $scope.dataBieu6cot1.push($scope.result[i].tongtien);
                        $scope.dataBieu5.push($scope.result[i]);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 1 && $scope.result[i].mA_DIABAN != 15) {
                        $scope.dataBieu6cot2.push($scope.result[i].tongtien);
                        $scope.dataBieu7.push($scope.result[i]);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 2 && $scope.result[i].mA_DIABAN != 15) {
                        $scope.dataBieu6cot3.push($scope.result[i].tongtien);
                        $scope.dataBieu8.push($scope.result[i]);
                    }
                }
                $scope.dataBieu6.push($scope.dataBieu6cot3);
                $scope.dataBieu6.push($scope.dataBieu6cot2);
                $scope.dataBieu6.push($scope.dataBieu6cot1);

                //end Lấy theo huyện
            });
        }
        LoadDetailTongThu();

        $scope.cancel = function () {
            $uibModalInstance.close();
        };




    }]);

    app.controller('tongchi_Ctrl', ['dashboardphtService', '$scope', 'configService', 'userService', 'dmdbhc_Service', '$uibModal', 'targetData', '$uibModalInstance', function (dashboardphtService, $scope, configService, userService, dmdbhc_Service, $uibModal, targetData, $uibModalInstance) {
        $scope.target = angular.copy(targetData);
        $scope.result = []
        $scope.config = angular.copy(configService);

        //Biều đồ lấy theo năm
        $scope.labelBieu1 = [];
        $scope.dataBieu1 = [];
        $scope.labelBieu2 = [];
        $scope.seriesBieu2 = [];
        $scope.dataBieu2 = [];
        $scope.dataBieu2cot1 = [];
        $scope.dataBieu2cot2 = [];
        $scope.dataBieu2cot3 = [];
        $scope.dataBieu3 = [];
        $scope.dataBieu4 = [];


        //Biểu đồ lấy theo huyện
        $scope.dataBieu5 = [];
        $scope.labelBieu6 = [];
        $scope.dataBieu6 = [];
        $scope.dataBieu6cot1 = [];
        $scope.dataBieu6cot2 = [];
        $scope.dataBieu6cot3 = [];
        $scope.dataBieu7 = [];
        $scope.dataBieu8 = [];


        $scope.barColor4 = ['#3CBA9F', '#e8c3b9'];
        $scope.options = {
            legend: { display: true },
            scales: {
                yAxes: [
                  {
                      id: 'y-axis',
                      min: 0,
                      ticks: {
                          beginAtZero: true,
                          callback: function (value) {
                              return (value + '').replace(/(\d)(?=(\d{3})+$)/g, '$1,');
                          }
                      }
                  }
                ]
            }

        };
        function LoadDetailTongThu() {
            var data = {
                MA_DIABAN: targetData.DIA_BAN,
                NAM: targetData.NAM,
                TU_THANG: targetData.FROM_THANG,
                DEN_THANG: targetData.TO_THANG,
            }
            dashboardphtService.Get_TONG_THU_NSDP(data).then(function (response) {
                console.log(response);
                $scope.result = response.data;
                // Lấy theo năm
                for (var i = 0 ; i < $scope.result.length; i++) {
                    if ($scope.result[i].nam == targetData.NAM && $scope.result[i].mA_DIABAN == 15) {
                        $scope.dataBieu1.push($scope.result[i]);
                        $scope.dataBieu2cot1.push($scope.result[i].tongtien);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 1 && $scope.result[i].mA_DIABAN == 15) {
                        $scope.dataBieu2cot2.push($scope.result[i].tongtien);
                        $scope.dataBieu3.push($scope.result[i]);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 2 && $scope.result[i].mA_DIABAN == 15) {
                        $scope.dataBieu2cot3.push($scope.result[i].tongtien);
                        $scope.dataBieu4.push($scope.result[i]);
                    }
                }
                $scope.dataBieu2.push($scope.dataBieu2cot3);
                $scope.dataBieu2.push($scope.dataBieu2cot2);
                $scope.dataBieu2.push($scope.dataBieu2cot1);
                $scope.labelBieu2.push("Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12");
                $scope.seriesBieu2.push(targetData.NAM - 2, targetData.NAM - 1, targetData.NAM);
                // end lấy theo năm
                // Lấy theo huyện					

                for (var i = 0 ; i < $scope.result.length; i++) {
                    if ($scope.result[i].nam == targetData.NAM && $scope.result[i].mA_DIABAN != 15) {
                        $scope.labelBieu6.push($scope.result[i].mA_DIABAN);
                        $scope.dataBieu6cot1.push($scope.result[i].tongtien);
                        $scope.dataBieu5.push($scope.result[i]);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 1 && $scope.result[i].mA_DIABAN != 15) {
                        $scope.dataBieu6cot2.push($scope.result[i].tongtien);
                        $scope.dataBieu7.push($scope.result[i]);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 2 && $scope.result[i].mA_DIABAN != 15) {
                        $scope.dataBieu6cot3.push($scope.result[i].tongtien);
                        $scope.dataBieu8.push($scope.result[i]);
                    }
                }
                $scope.dataBieu6.push($scope.dataBieu6cot3);
                $scope.dataBieu6.push($scope.dataBieu6cot2);
                $scope.dataBieu6.push($scope.dataBieu6cot1);

                //end Lấy theo huyện
            });
        }
        LoadDetailTongThu();

        $scope.cancel = function () {
            $uibModalInstance.close();
        };




    }]);



    return app;
});

