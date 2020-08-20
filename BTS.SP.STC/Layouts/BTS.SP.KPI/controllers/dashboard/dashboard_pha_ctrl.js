define(['angular', 'ui-bootstrap', 'controllers/htdm/dmdbhc'], function () {
    'use strict';


    var app = angular.module('dashboard_pha_Module', ['dmdbhc_Module', 'ui.bootstrap']);

    app.service('dashboardphaService', ['$http', 'configService', function ($http, configService) {
        var ServiceUrl = configService.rootUrlWebApi + '/dashboard/pha';
       
        var result = {
            GetMaHuyen: function (madiabancha) {            
                return $http.get(ServiceUrl + '/GetMaHuyen/' + madiabancha);
            },
            Get_TH_TC_TOANTINH: function (data) {
                return $http.post(ServiceUrl + '/Get_TH_TC_TOANTINH', data);
            },
            Get_TONG_THU: function (data) {
                return $http.post(ServiceUrl + '/Get_TONG_THU', data);
            },
            Get_TONG_THU_NSDP: function (data) {
                return $http.post(ServiceUrl + '/Get_TONG_THU_NSDP', data);
            },          
            GetDatabyMaDiaBanCha: function (data) {
                return $http.post(ServiceUrl + '/GetDatabyMaDiaBanCha', data);
            }, 
            GetDataLop1: function (data) {
                return $http.post(ServiceUrl + '/GetDataLop1', data);
            },
            GetTenDiaBan: function (madiaban) {
                return $http.get(ServiceUrl + '/GetTenDiaBan/' + madiaban);
            },
            GETDATA_CHI_TX_DT: function (data) {
                return $http.post(ServiceUrl + '/GETDATA_CHI_TX_DT', data);
            },      
            GETDATA_T_NSNN: function (data) {
                return $http.post(ServiceUrl + '/GETDATA_T_NSNN', data);
            },
            GETDATA_C_NSDP: function (data) {
                return $http.post(ServiceUrl + '/GETDATA_C_NSDP', data);
            },
        };
        return result;
    }]);



    app.controller('dashboard_pha_ctrl', ['dashboardphaService', '$scope', 'configService', 'userService', 'dmdbhc_Service', '$uibModal', function (dashboardphaService, $scope, configService, userService, dmdbhc_Service, $uibModal) {
        var currentTime = new Date();
        $scope.currentYear = currentTime.getFullYear();
        $scope.listNAM = [];
        $scope.fromTHANG = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
        $scope.toTHANG = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
        $scope.target = { DONVITINH: 1000000, LOAIQT: 'T', NAM: $scope.currentYear, FROM_THANG: 1, TO_THANG: 12, DIA_BAN: '', TEN_DIABAN:''};
        for (var i = $scope.currentYear - 4; i <= $scope.currentYear + 2; i++) {
            $scope.listNAM.push(i);
        }
        var lstUrl = window.location.href.split('/');
        if (lstUrl[3] == 'langson') {          
            $scope.target.DIA_BAN = '20';
            console.log('$scope.target.DIA_BAN', $scope.target.DIA_BAN);
        } else {
            $scope.target.DIA_BAN = '15';
        }
        $scope.tongThuNSNN = 0;
        $scope.tongThuNSDP = 0;
        $scope.tongChi = 0;
        $scope.MA_DBHC = [];

       
       

        $scope.barColor4 = ['#803690', '#00ADF9', '#FDB45C'];
        $scope.barColor3 = ['#DCDCDC', '#46BFBD', '#FDB45C'];
        //options biểu đồ hình cột có số hiện trên thanh dữ liệu
        $scope.options = {              
            barThickness: 100,
            legend: { display: true },
            scales: {
                yAxes: [
                  {
                      id: 'y-axis',
                      min: 0,
                      ticks: {
                          beginAtZero: false,
                          callback: function (value) {
                              return (value + '').replace(/(\d)(?=(\d{3})+$)/g, '$1,');
                          }
                        },
                  }
                ],             
            },
            animation: {
                duration: 1,
                onComplete: function () {                
                    var chartInstance = this.chart,                      
                    ctx = chartInstance.ctx;                    
                    ctx.textAlign = 'center';
                    ctx.textBaseline = 'bottom';
                    ctx.fillStyle = '#000000';
                   
                    this.data.datasets.forEach(function (dataset, i) {
                        var meta = chartInstance.controller.getDatasetMeta(i);
                        meta.data.forEach(function (bar, index) {

                            var data = dataset.data[index];
                            
                            ctx.fillText((data + '').replace(/(\d)(?=(\d{3})+$)/g, '$1,'), bar._model.x, bar._model.y - 5);
                           
                        });
                    });
                }
            },
        };

        //options biểu đồ hình cột không có số hiện trên thanh dữ liệu
        $scope.optionnonnumber = {
            
            legend: { display: true },
            scales: {
                yAxes: [
                    {
                        id: 'y-axis',
                        min: 0,
                        ticks: {
                            beginAtZero: false,
                            callback: function (value) {
                                return (value + '').replace(/(\d)(?=(\d{3})+$)/g, '$1,');
                            }
                        },
                    }
                ],
            },          
        };

        //options biểu đồ hình tròn
        $scope.optioneChart = {
            colors: ['#803690', '#00ADF9', '#DCDCDC', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'],            
            plugins: {
                datalabels: {
                    formatter: (value, ctx) => {
                        let sum = 0;
                        let dataArr = ctx.chart.data.datasets[0].data;
                        dataArr.map(data => {
                            sum += data;
                        });
                        let percentage = (value * 100 / sum).toFixed(2) + "%";
                        return percentage;
                    },
                    color: '#0000',
                }
            }
        };     

        

        
        

        // check trùng
        function loaibophantutrung(arr) {
            let isExist = (arr, x) => arr.indexOf(x) > -1;
            let ans = [];

            arr.forEach(element => {
                if (!isExist(ans, element)) ans.push(element);
            });

            return ans;
        }

      


        function getTenDiaBan()
        {
            dashboardphaService.GetTenDiaBan($scope.target.DIA_BAN).then(function (response) {
                if (response && response.data) {
                    $scope.target.TEN_DIABAN = response.data.teN_DBHC;
                } else {
                    console.log(response);
                }
            }, function (response) {
                console.log(response);
            });
        }
       



        function LoadMaDiaBan() {
            dashboardphaService.GetMaHuyen($scope.target.DIA_BAN).then(function (response) {
              
                if (response && response.data && response.status == 200) {    
                    $scope.MA_DBHC = response.data.data;                                       
                } else {
                    console.log(response);
                }
            }, function (response) {
                console.log(response);
            });
        }
        LoadMaDiaBan();

        // .....

        //Lấy tổng thu và tông chi
        function LoadKPI() {
            var data = {
                MA_DIABAN: $scope.target.DIA_BAN,
                NAM: $scope.target.NAM,
                TU_THANG: $scope.target.FROM_THANG,
                DEN_THANG: $scope.target.TO_THANG,
            }

            dashboardphaService.Get_TH_TC_TOANTINH(data).then(function (response) {          
                for (var i = 0 ; i < response.data.length; i++) {
                    if (response.data[i].stt == 1) {
                        $scope.tongThuNSNN += response.data[i].tongtien;
                    }
                    if (response.data[i].stt == 2) {
                        $scope.tongThuNSDP += response.data[i].tongtien;
                    }
                    if (response.data[i].stt == 3) {
                        $scope.tongChi += response.data[i].tongtien;                       
                    }                
                }
            });
        }
        LoadKPI();

        //Biểu đồ so sánh dữ liệu từ KPI
        function LoadTongThuTongChi() {
            $scope.datatenDiaBan = [];
            //So sánh thu chi
            $scope.dataBieu1cot1 = [];
            $scope.dataBieu1cot2 = [];
            $scope.dataBieu1cot3 = [];
            $scope.dataBieu1 = [];
            $scope.seriesBieu1 = [];
            $scope.labelBieu1 = [];
            //So sánh thu NSNN
            $scope.dataBieu2 = [];
            $scope.labelBieu2 = [];
            //So sánh thu NSDP
            $scope.dataBieu3 = [];
            $scope.labelBieu3 = [];
            //So sánh chi
            $scope.dataBieu4 = [];
            $scope.labelBieu4 = [];
            var data = {
                MA_DIABAN: $scope.target.DIA_BAN,
                NAM: $scope.target.NAM,
                TU_THANG: $scope.target.FROM_THANG,
                DEN_THANG: $scope.target.TO_THANG,
            }
            dashboardphaService.GetDatabyMaDiaBanCha(data).then(function (response) {
                if (response && response.data && response.data.length > 0) {                       
                    for (var i = 0 ; i < response.data.length; i++) {
                        $scope.datatenDiaBan.push(response.data[i].teN_DIABAN);
                        $scope.labelBieu1 = loaibophantutrung($scope.datatenDiaBan);
                        $scope.labelBieu2 = loaibophantutrung($scope.datatenDiaBan);
                        $scope.labelBieu3 = loaibophantutrung($scope.datatenDiaBan);
                        $scope.labelBieu4 = loaibophantutrung($scope.datatenDiaBan);
                    }
                    for (var i = 0; i < $scope.labelBieu1.length; i++) {
                        $scope.tongthunndiaban = 0;
                        $scope.tongthudpdiaban = 0;
                        $scope.tongchidiaban = 0;
                        for (var j = 0 ; j < response.data.length; j++) {
                            if (response.data[j].stt == 1 && response.data[j].teN_DIABAN == $scope.labelBieu1[i]) {
                                $scope.tongthunndiaban += response.data[j].tongtien;
                            }
                            if (response.data[j].stt == 2 && response.data[j].teN_DIABAN == $scope.labelBieu1[i]) {
                                $scope.tongthudpdiaban += response.data[j].tongtien;
                            }
                            if (response.data[j].stt == 3 && response.data[j].teN_DIABAN == $scope.labelBieu1[i]) {
                               
                                $scope.tongchidiaban += response.data[j].tongtien;
                            }
                        }
                        $scope.dataBieu2.push($scope.tongthunndiaban);
                        $scope.dataBieu3.push($scope.tongthudpdiaban);
                        $scope.dataBieu4.push($scope.tongchidiaban);
                        $scope.dataBieu1cot1.push($scope.tongthunndiaban);
                        $scope.dataBieu1cot2.push($scope.tongthudpdiaban);
                        $scope.dataBieu1cot3.push($scope.tongchidiaban);
                    }                  
                    $scope.seriesBieu1.push('Tổng Thu NSNN','Tổng Thu NSDP','Tổng Chi');
                    $scope.dataBieu1.push($scope.dataBieu1cot1) ;
                    $scope.dataBieu1.push($scope.dataBieu1cot2);
                    $scope.dataBieu1.push($scope.dataBieu1cot3);
                    
                } else {
                    console.log(response);
                }
            }, function (response) {
                console.log(response);
            });
        }
        LoadTongThuTongChi();


        //Biểu đồ dữ liêu lấy chi thường xuyên và chi đầu từ
        function load_TX_DT() {
            //So sánh chi
            
            $scope.dataBieu5 = [];
            $scope.labelBieu5 = ['Chi Thường Xuyên', 'Chi Đầu Tư'];
            $scope.seriesBieu20 = ['Quyết Toán'];
            var data = {
                MA_DIABAN: $scope.target.DIA_BAN,
                NAM: $scope.target.NAM,
                TU_THANG: $scope.target.FROM_THANG,
                DEN_THANG: $scope.target.TO_THANG,
            }
            dashboardphaService.GETDATA_CHI_TX_DT(data).then(function (response) {
                if (response && response.data && response.data.length > 0) {
                    $scope.TX = 0;
                    $scope.DT = 0;
                    for (var i = 0; i < response.data.length; i++) {
                        
                        $scope.TX += response.data[i].tx;
                        $scope.DT += response.data[i].dt;
                    }
                    $scope.dataBieu5.push($scope.TX);
                    $scope.dataBieu5.push($scope.DT);
                }
            })
        }
        load_TX_DT();

        function load_T_NSNN() {
            //So sánh chi
            $scope.dataBieu6 = [];
            $scope.dataBieu6cot1= [];
            $scope.dataBieu6cot2 = [];
           
            $scope.seriesBieu6 = ['Thu Hưởng Phân Cấp 100%', 'Thu Hưởng Phân Cấp'];
            $scope.labelBieu6 = ['Trung ương', 'Tỉnh', 'Huyện','Xã'];
            var data = {
                MA_DIABAN: $scope.target.DIA_BAN,
                NAM: $scope.target.NAM,
                TU_THANG: $scope.target.FROM_THANG,
                DEN_THANG: $scope.target.TO_THANG,
            }
            dashboardphaService.GETDATA_T_NSNN(data).then(function (response) {    
                console.log(response);
                if (response && response.data && response.data.length > 0) {
                    $scope.T_TW_100 = 0;
                    $scope.T_TW_PC = 0;
                    $scope.T_T_100 = 0;
                    $scope.T_T_PC = 0;
                    $scope.T_H_100 = 0;
                    $scope.T_H_PC = 0;
                    $scope.T_X_100 = 0;
                    $scope.T_X_PC = 0;
                    for (var i = 0; i < response.data.length; i++) {
                        $scope.T_TW_100 += response.data[i].t_TW_100;
                        $scope.T_TW_PC += response.data[i].t_TW_PC;
                        $scope.T_T_100 += response.data[i].t_T_100;
                        $scope.T_T_PC += response.data[i].t_T_PC;
                        $scope.T_H_100 += response.data[i].t_H_100;
                        $scope.T_H_PC += response.data[i].t_H_PC;
                        $scope.T_X_100 += response.data[i].t_X_100;
                        $scope.T_X_PC += response.data[i].t_X_PC;   
                    }
                    $scope.dataBieu6cot1.push($scope.T_TW_100);
                    $scope.dataBieu6cot1.push($scope.T_T_100);
                    $scope.dataBieu6cot1.push($scope.T_H_100);
                    $scope.dataBieu6cot1.push($scope.T_X_100);
                    $scope.dataBieu6cot2.push($scope.T_TW_PC);
                    $scope.dataBieu6cot2.push($scope.T_T_PC);
                    $scope.dataBieu6cot2.push($scope.T_H_PC);
                    $scope.dataBieu6cot2.push($scope.T_X_PC);
                    $scope.dataBieu6.push($scope.dataBieu6cot1);
                    $scope.dataBieu6.push($scope.dataBieu6cot2);
                   
                }
            })
        }
        load_T_NSNN();


        function load_C_NSDP() {
            $scope.dataBieu7 = [];
            $scope.dataBieu7cot1 = [];
            $scope.dataBieu7cot2 = [];
            $scope.labelBieu7 = ['Tỉnh', 'Huyện', 'Xã'];
            $scope.seriesBieu7 = ['Tổng chi cân đối', 'Chi trả nợ gốc'];
            var data = {
                MA_DIABAN: $scope.target.DIA_BAN,
                NAM: $scope.target.NAM,
                TU_THANG: $scope.target.FROM_THANG,
                DEN_THANG: $scope.target.TO_THANG,
            }
            dashboardphaService.GETDATA_C_NSDP(data).then(function (response) {
                if (response && response.data ) {
                    $scope.T_CCD = 0;
                    $scope.T_CTN = 0;
                    $scope.H_CCD = 0;
                    $scope.H_CTN = 0;
                    $scope.X_CCD = 0;
                    $scope.X_CTN = 0;                  
                    for (var i = 0; i < response.data.length; i++) {
                        $scope.T_CCD += response.data[i].t_CCD;
                        $scope.T_CTN += response.data[i].t_CTN;
                        $scope.H_CCD += response.data[i].h_CCD;
                        $scope.H_CTN += response.data[i].h_CTN;
                        $scope.X_CCD += response.data[i].x_CCD;
                        $scope.X_CTN += response.data[i].x_CTN;                      
                    }
                    $scope.dataBieu7cot1.push($scope.T_CCD);
                    $scope.dataBieu7cot1.push($scope.H_CCD);
                    $scope.dataBieu7cot1.push($scope.X_CCD);
                    $scope.dataBieu7cot2.push($scope.T_CTN);
                    $scope.dataBieu7cot2.push($scope.H_CTN);
                    $scope.dataBieu7cot2.push($scope.X_CTN);
                    $scope.dataBieu7.push($scope.dataBieu7cot1);
                    $scope.dataBieu7.push($scope.dataBieu7cot2);
                    console.log($scope.dataBieu7);
                }
            });
        } 
        load_C_NSDP()

        //Cập nhật dữ liệu
        $scope.Updatedata = function () {
            $scope.tongThuNSNN = 0;
            $scope.tongThuNSDP = 0;
            $scope.tongChi = 0; 
            getTenDiaBan();
            LoadKPI();
            LoadTongThuTongChi();
            load_TX_DT();
            load_T_NSNN();
            load_C_NSDP();
        }


        $scope.LoadExcel = function () {
            var modalInstance = $uibModal.open({
                windowClass: 'app-modal-window',
                backdrop: 'static',
                templateUrl: configService.buildUrl('dashboard/pha/detail', 'excel'),
                controller: 'excel_Ctrl',
                resolve: {
                    targetData: function () {
                        return $scope.target;
                    }
                }
            });
        }

        $scope.DetailTTNSNN = function () {
            var modalInstance = $uibModal.open({
                windowClass: 'app-modal-window',
                backdrop: 'static',
                templateUrl: configService.buildUrl('dashboard/pha/detail', 'tongthunsnn'),
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
                templateUrl: configService.buildUrl('dashboard/pha/detail', 'tongthunsdp'),
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
                templateUrl: configService.buildUrl('dashboard/pha/detail', 'tongchi'),
                controller: 'tongchi_Ctrl',
                resolve: {
                    targetData: function () {
                        return $scope.target;
                    }
                }
            });
        }
    }]);

    app.controller('tongthunsnn_Ctrl', ['dashboardphaService', '$scope', 'configService', 'userService', 'dmdbhc_Service', '$uibModal', 'targetData', '$uibModalInstance', function (dashboardphaService, $scope, configService, userService, dmdbhc_Service, $uibModal, targetData, $uibModalInstance) {
        $scope.target = angular.copy(targetData);
        console.log($scope.target);
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


        $scope.barColor4 = ['#803690', '#00ADF9', '#FDB45C'];
        $scope.barColor3 = ['#FDB45C', '#46BFBD', '#FDB45C'];
        $scope.options = {
            colors: ['#803690', '#00ADF9', '#FDB45C'],
            legend: { display: false },
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
            },
          
        };

        // check trùng
        function loaibophantutrung(arr) {
            let isExist = (arr, x) => arr.indexOf(x) > -1;
            let ans = [];

            arr.forEach(element => {
                if (!isExist(ans, element)) ans.push(element);
            });

            return ans;
        }


        function LoadDetailTongThu() {
            $scope.datatenDiaBan = [];
            var data = {
                MA_DIABAN: targetData.DIA_BAN,
                NAM: targetData.NAM,
                TU_THANG: targetData.FROM_THANG,
                DEN_THANG: targetData.TO_THANG,
            }
            dashboardphaService.Get_TONG_THU(data).then(function (response) {
               
                $scope.result = response.data;
                // Lấy theo năm
                for (var i = 0 ; i < $scope.result.length; i++) {
                    if ($scope.result[i].nam == targetData.NAM && $scope.result[i].mA_DIABAN == $scope.target.DIA_BAN) {
                        $scope.dataBieu1.push($scope.result[i]);
                        $scope.dataBieu2cot1.push($scope.result[i].tongtien);
                       
                    }
                    if ($scope.result[i].nam == targetData.NAM - 1 && $scope.result[i].mA_DIABAN == $scope.target.DIA_BAN) {
                        $scope.dataBieu2cot2.push($scope.result[i].tongtien);
                        $scope.dataBieu3.push($scope.result[i]);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 2 && $scope.result[i].mA_DIABAN == $scope.target.DIA_BAN) {
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
                for (var i = 0 ; i < response.data.length; i++) {
                    if ($scope.result[i].mA_DIABAN != $scope.target.DIA_BAN)
                    $scope.datatenDiaBan.push(response.data[i].teN_DIABAN);
                    $scope.labelBieu6 = loaibophantutrung($scope.datatenDiaBan);
                }
                for (var i = 0; i < $scope.labelBieu6.length; i++) {
                    $scope.tongthunamdau = 0;
                    $scope.tongthunamsau = 0;
                    $scope.tongthunamcuoi = 0;
                    for (var j = 0 ; j < $scope.result.length; j++) {
                        if ($scope.result[j].nam == targetData.NAM && $scope.result[j].teN_DIABAN == $scope.labelBieu6[i]) {
                            $scope.tongthunamdau += $scope.result[j].tongtien;
                        }
                        if ($scope.result[j].nam == targetData.NAM - 1 && $scope.result[j].teN_DIABAN == $scope.labelBieu6[i]) {
                            $scope.tongthunamsau += $scope.result[j].tongtien;
                           
                        }
                        if ($scope.result[j].nam == targetData.NAM - 2 && $scope.result[j].teN_DIABAN == $scope.labelBieu6[i]) {
                            $scope.tongthunamcuoi += $scope.result[j].tongtien;
                            
                        }
                    }
                    $scope.dataBieu6cot1.push($scope.tongthunamdau);
                    $scope.dataBieu6cot2.push($scope.tongthunamsau);
                    $scope.dataBieu6cot3.push($scope.tongthunamcuoi);
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

    app.controller('tongthunsdp_Ctrl', ['dashboardphaService', '$scope', 'configService', 'userService', 'dmdbhc_Service', '$uibModal', 'targetData', '$uibModalInstance', function (dashboardphaService, $scope, configService, userService, dmdbhc_Service, $uibModal, targetData, $uibModalInstance) {
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


        $scope.barColor4 = ['#803690', '#00ADF9', '#FDB45C'];
        $scope.barColor3 = ['#FDB45C', '#46BFBD', '#FDB45C'];

        // check trùng
        function loaibophantutrung(arr) {
            let isExist = (arr, x) => arr.indexOf(x) > -1;
            let ans = [];

            arr.forEach(element => {
                if (!isExist(ans, element)) ans.push(element);
            });

            return ans;
        }

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
            $scope.datatenDiaBan = [];
            var data = {
                MA_DIABAN: targetData.DIA_BAN,
                NAM: targetData.NAM,
                TU_THANG: targetData.FROM_THANG,
                DEN_THANG: targetData.TO_THANG,
            }
            dashboardphaService.Get_TONG_THU_NSDP(data).then(function (response) {
               
                $scope.result = response.data;
                // Lấy theo năm
                for (var i = 0 ; i < $scope.result.length; i++) {
                    if ($scope.result[i].nam == targetData.NAM && $scope.result[i].mA_DIABAN == $scope.target.DIA_BAN) {
                        $scope.dataBieu1.push($scope.result[i]);
                        $scope.dataBieu2cot1.push($scope.result[i].tongtien);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 1 && $scope.result[i].mA_DIABAN == $scope.target.DIA_BAN) {
                        $scope.dataBieu2cot2.push($scope.result[i].tongtien);
                        $scope.dataBieu3.push($scope.result[i]);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 2 && $scope.result[i].mA_DIABAN == $scope.target.DIA_BAN) {
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

                for (var i = 0 ; i < response.data.length; i++) {
                    if ($scope.result[i].mA_DIABAN != $scope.target.DIA_BAN)
                        $scope.datatenDiaBan.push(response.data[i].teN_DIABAN);
                    $scope.labelBieu6 = loaibophantutrung($scope.datatenDiaBan);
                }
                for (var i = 0; i < $scope.labelBieu6.length; i++) {
                    $scope.tongthunamdau = 0;
                    $scope.tongthunamsau = 0;
                    $scope.tongthunamcuoi = 0;
                    for (var j = 0 ; j < $scope.result.length; j++) {
                        if ($scope.result[j].nam == targetData.NAM && $scope.result[j].teN_DIABAN == $scope.labelBieu6[i]) {
                            $scope.tongthunamdau += $scope.result[j].tongtien;
                        }
                        if ($scope.result[j].nam == targetData.NAM - 1 && $scope.result[j].teN_DIABAN == $scope.labelBieu6[i]) {
                            $scope.tongthunamsau += $scope.result[j].tongtien;

                        }
                        if ($scope.result[j].nam == targetData.NAM - 2 && $scope.result[j].teN_DIABAN == $scope.labelBieu6[i]) {
                            $scope.tongthunamcuoi += $scope.result[j].tongtien;

                        }
                    }
                    $scope.dataBieu6cot1.push($scope.tongthunamdau);
                    $scope.dataBieu6cot2.push($scope.tongthunamsau);
                    $scope.dataBieu6cot3.push($scope.tongthunamcuoi);
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

    app.controller('tongchi_Ctrl', ['dashboardphaService', '$scope', 'configService', 'userService', 'dmdbhc_Service', '$uibModal', 'targetData', '$uibModalInstance', function (dashboardphaService, $scope, configService, userService, dmdbhc_Service, $uibModal, targetData, $uibModalInstance) {
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


        $scope.barColor4 = ['#800000', '#D2691E', '#FDB45C'];
        $scope.barColor3 = ['#FDB45C', '#46BFBD', '#FDB45C'];
        $scope.options = {
            legend: { display: true },
            scales: {
                yAxes: [
                  {
                      id: 'y-axis',
                      min: 20,
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
        

        // check trùng
        function loaibophantutrung(arr) {
            let isExist = (arr, x) => arr.indexOf(x) > -1;
            let ans = [];

            arr.forEach(element => {
                if (!isExist(ans, element)) ans.push(element);
            });

            return ans;
        }
        function LoadDetailTongThu() {
            $scope.datatenDiaBan = [];
            var data = {
                MA_DIABAN: targetData.DIA_BAN,
                NAM: targetData.NAM,
                TU_THANG: targetData.FROM_THANG,
                DEN_THANG: targetData.TO_THANG,
            }
            dashboardphaService.Get_TONG_THU_NSDP(data).then(function (response) {
              
                $scope.result = response.data;
                // Lấy theo năm
                for (var i = 0 ; i < $scope.result.length; i++) {
                    if ($scope.result[i].nam == targetData.NAM && $scope.result[i].mA_DIABAN == $scope.target.DIA_BAN) {
                        $scope.dataBieu1.push($scope.result[i]);
                        $scope.dataBieu2cot1.push($scope.result[i].tongtien);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 1 && $scope.result[i].mA_DIABAN == $scope.target.DIA_BAN) {
                        $scope.dataBieu2cot2.push($scope.result[i].tongtien);
                        $scope.dataBieu3.push($scope.result[i]);
                    }
                    if ($scope.result[i].nam == targetData.NAM - 2 && $scope.result[i].mA_DIABAN == $scope.target.DIA_BAN) {
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

                for (var i = 0 ; i < response.data.length; i++) {
                    if ($scope.result[i].mA_DIABAN != $scope.target.DIA_BAN)
                        $scope.datatenDiaBan.push(response.data[i].teN_DIABAN);
                    $scope.labelBieu6 = loaibophantutrung($scope.datatenDiaBan);
                }
                for (var i = 0; i < $scope.labelBieu6.length; i++) {
                    $scope.tongthunamdau = 0;
                    $scope.tongthunamsau = 0;
                    $scope.tongthunamcuoi = 0;
                    for (var j = 0 ; j < $scope.result.length; j++) {
                        if ($scope.result[j].nam == targetData.NAM && $scope.result[j].teN_DIABAN == $scope.labelBieu6[i]) {
                            $scope.tongthunamdau += $scope.result[j].tongtien;
                        }
                        if ($scope.result[j].nam == targetData.NAM - 1 && $scope.result[j].teN_DIABAN == $scope.labelBieu6[i]) {
                            $scope.tongthunamsau += $scope.result[j].tongtien;

                        }
                        if ($scope.result[j].nam == targetData.NAM - 2 && $scope.result[j].teN_DIABAN == $scope.labelBieu6[i]) {
                            $scope.tongthunamcuoi += $scope.result[j].tongtien;

                        }
                    }
                    $scope.dataBieu6cot1.push($scope.tongthunamdau);
                    $scope.dataBieu6cot2.push($scope.tongthunamsau);
                    $scope.dataBieu6cot3.push($scope.tongthunamcuoi);
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


    app.controller('excel_Ctrl', ['dashboardphaService', '$scope', 'configService', 'userService', 'dmdbhc_Service', '$uibModal', 'targetData', '$uibModalInstance', function (dashboardphaService, $scope, configService, userService, dmdbhc_Service, $uibModal, targetData, $uibModalInstance) {
        $scope.target = angular.copy(targetData);
        $scope.result = []
        $scope.config = angular.copy(configService);

        // check trùng
        function loaibophantutrung(arr) {
            let isExist = (arr, x) => arr.indexOf(x) > -1;
            let ans = [];

            arr.forEach(element => {
                if (!isExist(ans, element)) ans.push(element);
            });

            return ans;
        }



       

        function LoadDataLop1() {
            $scope.MACHITIEUtemp = [];
            $scope.MACHITIEU = [];
            var data = {
                MA_DIABAN: $scope.target.DIA_BAN,
                NAM: $scope.target.NAM,
                TU_THANG: $scope.target.FROM_THANG,
                DEN_THANG: $scope.target.TO_THANG,
            }
            dashboardphaService.GetDataLop1(data).then(function (response) {
                if (response.status == 200 && response.data && response.data.length > 0) {
                    $scope.data = response.data;
                    }else {
                    console.log(response);
                }
            }, function (response) {
                console.log(response);
            });
        }

        LoadDataLop1();

      

        $scope.cancel = function () {
            $uibModalInstance.close();
        };




    }]);
    return app;
});

