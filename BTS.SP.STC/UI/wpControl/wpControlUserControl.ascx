<%@ Assembly Name="BTS.SP.STC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=35f62861b1453f6d" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wpControlUserControl.ascx.cs" Inherits="BTS.SP.STC.UI.wpControl.wpControlUserControl" %>

<link href="<%=SPContext.Current.Web.Url %>/_layouts/15/stc/css/loadingw.css" rel="stylesheet" />
<link href="<%=SPContext.Current.Web.Url %>/_layouts/15/stc/css/style.css" rel="stylesheet" />
<link href="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/css/kendo.common.min.css" rel="stylesheet" />
<link href="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/css/kendo.blueopal.min.css" rel="stylesheet" />
<link href="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/css/angular-ui-tree.min.css" rel="stylesheet" />
<link href="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/css/dx.common.css" rel="stylesheet" />
<link href="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/css/dx.light.css" rel="stylesheet" />
<link href="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/css/ng-tags-input.min.css" rel="stylesheet" />

<link href="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/css/angular-ui-tree.min.css" rel="stylesheet" />
<link href="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/css/ejthemes/ej.widgets.core.bootstrap.min.css" rel="stylesheet" />
<link href="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/css/ejthemes/bootstrap-theme/ej.web.all.min.css" rel="stylesheet" />
<link href="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/css/ejthemes/responsive-css/ej.responsive.css" rel="stylesheet" />
<link href="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/css/ejthemes/ribbon-css/ej.icons.css" rel="stylesheet" />
<link href="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/css/angular-multi-select-tree-0.1.0.css" rel="stylesheet" />
<script type="text/javascript" src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/dx/jquery-1.12.3.min.js"></script>


<script>
    document.write('<base href="' + document.location.pathname + '" />')
</script>
<style type="text/css">
    .container-fluid.menu {
        background-color: #af1515;
    }

    .dropdown-submenu > .dropdown-menu {
        top: 0;
        left: 100%;
        margin-top: -6px;
        margin-left: -1px;
        -webkit-border-radius: 0 6px 6px 6px;
        -moz-border-radius: 0 6px 6px;
        border-radius: 0 6px 6px 6px;
    }

    .dropdown-submenu:hover > .dropdown-menu {
        display: block;
    }

    .container-fluid.menu a:hover {
        background-color: antiquewhite;
    }

    .dropdown-submenu > a:after {
        display: block;
        content: " ";
        float: right;
        width: 0;
        height: 0;
        border-color: transparent;
        border-style: solid;
        border-width: 5px 0 5px 5px;
        border-left-color: #ccc;
        margin-top: 5px;
        margin-right: -10px;
    }

    .dropdown-submenu:hover > a:after {
        border-left-color: #fff;
    }

    .dropdown-submenu.pull-left {
        float: none;
    }

        .dropdown-submenu.pull-left > .dropdown-menu {
            left: -100%;
            margin-left: 10px;
            -webkit-border-radius: 6px 0 6px 6px;
            -moz-border-radius: 6px 0 6px 6px;
            border-radius: 6px 0 6px 6px;
        }

    .navbar {
        min-height: 38px !important;
        border: 0px !important;
    }
</style>

<div ng-app="mainApp" id="mainApp">
    <!--Navigation-->
    <div class="container-fluid top-hd" style="height: 53px; background-color: #fffaf0;">
        <div class="container">
            <div class="container">
                <img src="<%=SPContext.Current.Web.Url %>/_layouts/15/stc/imgs/logo.png" style="height: 40px; margin: 7px 10px 7px 0px; float: left;" />
                <span class="brand" style="float: left; line-height: 40px; margin-top: 7px; font-size: 18px; font-weight: bold; color: brown;">PHẦN MỀM QUẢN LÝ TÀI CHÍNH CÔNG</span>
            </div>
        </div>
    </div>
    <div class="collapse navbar-collapse" ng-controller="loadMenuController">
        <!--app.js-->
        <nav class="navbar navbar-inverse">
            <%--<div class="col-sm-1" style="margin: 3px;" ng-show="currentUser">
                <div class="input-group">
                    <input type="text" id="CHANGEPH" class="form-control" ng-model="changePH"></input>
                    <div class="input-group-btn">
                        <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">
                            <span class="caret"></span>
                        </button>
                        <ul id="dataList" class="dropdown-menu">
                            <li data-value="A" data-selected="true"><a>PHÂN HỆ A</a></li>
                            <li data-value="B"><a>PHÂN HỆ B</a></li>
                            <li data-value="C"><a>PHÂN HỆ C</a></li>
                        </ul>
                    </div>
                </div>
            </div>--%>
            <div class="container menu">
                <div class="navbar-header" ng-if="currentUser" style="height: 38px;">
                    <%--<ul class="nav navbar-nav">
                        <li dropdown ng-repeat="data in dataMenu">
                            <a href="#" dropdown-toggle>{{data.TENMENU}}<b ng-if="data.CHILDREN && data.CHILDREN.length>0" class='caret'></b></a>
                            <tree ng-if="data.CHILDREN && data.CHILDREN.length>0" tree='data.CHILDREN'></tree>
                        </li>
                    </ul>--%>
                    <div class="navbar-custom-menu" style="position: fixed; right: 0px; margin: 5px 170px;">
                        <ul class="nav navbar-nav">
                            <li class="dropdown user user-menu">
                                <a class="dropdown-toggle" data-toggle="dropdown" aria-expanded="true">
                                    <div data-toggle="tooltip" data-placement="left" title="Thông tin tài khoản">
                                        <span class="glyphicon glyphicon-user"></span>&nbsp;
                                    </div>
                                </a>
                                <ul class="dropdown-menu" style="margin: -7px -96px; font-size: 13px; padding: 13px 19px; color: #333;">
                                    <!-- User image -->
                                    <li class="user-header">
                                        <p style="float: left;">
                                            Tài khoản: {{currentUser.userName}}<br />
                                        </p>
                                        <p style="float: left;">
                                            Tên đầy đủ: {{currentUser.fullName}}<br />
                                        </p>
                                        <p style="float: left;">
                                            Hộp thư: {{currentUser.email}}<br />
                                        </p>
                                        <p style="float: left;">
                                            Số điện thoại: {{currentUser.phone}}<br />
                                        </p>
                                        <p style="float: left;">
                                            Chức vụ: {{currentUser.chucVu}}<br />
                                        </p>
                                        <p style="float: left;">
                                            Tên tài khoản: {{currentUser.donViLap}}<br />
                                        </p>
                                        <p style="float: left;">
                                            Tài khoản kho bạc: {{currentUser.maTaiKhoanKhoBac}}<br />
                                        </p>
                                    </li>
                                    <li class="user-footer">
                                        <div class="pull-left">
                                        </div>

                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="pull-right" ng-controller="logOutController">
                    <a class="btn btn-default" style="margin: 2px 44px; position: fixed; right: 0px;" ng-click="logOut()"><span class="glyphicon glyphicon-off"></span>&nbsp;Đăng xuất</a>
                </div>
            </div>
        </nav>
    </div>
    <!--End Nav-->
    <div id="page-wrapper" class="container">
        <div ui-view></div>
        <div data-sp-flash></div>
        <div style="margin-top: 30px;">
            <a style="font-weight: bold;" href="../pha">- PHA Phân hệ khai thác báo cáo.</a>
        </div>
    </div>
</div>

<!--ExcelViewer-->
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/excelviewer/jsrender.min.js" type="text/javascript"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/excelviewer/jquery.validate.min.js" type="text/javascript"></script>
<!--End excel-->
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/jsCommon.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-cookies.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-ui-router.min.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-resource.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-sanitize.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-locale_en.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/ui-bootstrap-tpls-1.3.3.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-filter.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-base64.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-local-storage.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-ui-tree.min.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-ui-tree.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/masks.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-confirm.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-confirm.min.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-ui.min.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/dx/jquery-ui.min.js"></script>
<%--<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/dx/dx.all.js"></script>--%>

<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/kendo.all.min.js"></script>

<!--ExcelViewer-->
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/excelviewer/ej.web.all.min.js" type="text/javascript"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/excelviewer/ej.widget.angular.min.js" type="text/javascript"></script>
<!--End excel-->




<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/ng-file-upload-shim.min.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/ng-file-upload.min.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-file-upload.min.js"></script>
<!--Angular multiselect input-->
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-multi-select-tree-0.1.0.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/angular-multi-select-tree-0.1.0.tpl.js"></script>
<!--END Angular multiselect input-->
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/ng-tags-input.min.js"></script>

<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/directives.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/filters.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/handlers.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/services.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/configService.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/controllers.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmModule.js"></script>

<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvModule.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/nv_phcModule.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/sys/sysModule.js"></script>

<%--format input currency--%>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/init/formatCurrency.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/utils/dynamic-number.min.js"></script>
<%--end format input currency--%>

<!--Hệ thống-->
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/sys/sysUserController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/sys/sysChucNangController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/sys/sysTuDienController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/sys/sysDVQHNSController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/sys/sysTongHopDLController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/sys/sysThamSoHeThongController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/sys/sysTraCuuController.js"></script>


<!--Danh mục-->
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmCapController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmNguonNSNNController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmNhomController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmTKTNController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmLoaiController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmChuongController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmNGANHKTController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmChiTieuBaoCaoController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmMauBaoCaoThuController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmMucController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmTieuMucController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmMauBaoCaoChiController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmQuyController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmDuAnController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmTaiKhoanController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmLoaiHinhController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmDanhMucController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmNguonVonController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmLoaiDuToan.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmLoaiCTController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmCTMucTieuController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmDTThanhToanController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmTinhChatNguonController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmTKKhoBacController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmTieuNhomController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmMenuController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmDBHCController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmCTMTQGController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmTSCDController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmVatTuController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmLoaiTSCDController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmLoaiPhiController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmPHCNghiepVuController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmChiTieuThuTheoNDKTController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmChiTieuChiTheoNDKTController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmPhongBanController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmDuTuXDCBController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/dm/dmChiTieuCotController.js"></script>
<!--End Danh mục-->
<!--nghiệp vụ-->
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoMau53Controller.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoMau56Controller.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoMau57Controller.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoMau68Controller.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoMau70Controller.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoMau55Controller.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBAOCAO_ADD_COLController.js"></script>

<%--<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/testReportController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvbaoCaoB202Controller.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoB303_MLNSController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoB201Controller.js"></script>

<%--<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoMLNSController.js"></script>--%>
<%--<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoDuToanChiController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoPL08B05Controller.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoPL08B04Controller.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoB301Controller.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoPL06Controller.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoHCSNController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoNSController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoDTXDController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoNS_DuToanController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv/nvBaoCaoNS_DTController.js"></script>
--%>
<!--Nghiệp vụ phân hệ C Kế toán xã-->
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/nv_phcNhapSoDuController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/nv_phcDoiChieuSoLieuController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/NvNhapCTKeToan/nv_phcNhapCTKeToanController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/NvNhapCTKeToan/nv_phcNhapCTPhieuThuController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/NvNhapCTKeToan/nv_phcNhapCTPhieuChiController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/NvNhapCTKeToan/nv_phcNhapCTGiayBaoNoController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/NvNhapCTKeToan/nv_phcNhapCTGiayBaoCoController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/NvNhapCTKeToan/nv_phcNhapCTPhieuNhapKhoController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/NvNhapCTKeToan/nv_phcNhapCTPhieuXuatKhoController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/NvNhapCTKeToan/nv_phcNhapCTGhiTangCCDCController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/NvNhapCTKeToan/nv_phcNhapCTGhiGiamCCDController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/NvNhapCTKeToan/nv_phcNhapCTKhacController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/nv_phcDuToanChiTheoNDKTController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/nv_phcDuToanThuTheoNDKTController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/nv_phcBienLaiThuController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/nv_phcDuToanThuTheoMLNSController.js"></script>
<script src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/app/modules/nv_phc/nv_phcDuToanChiTheoMLNSController.js"></script>
<!--END nghiệp vụ phân hệ C Kế toán xã-->

<!--End nghiệp vụ-->
<script src="<%= SPContext.Current.Web.Url %>/_layouts/15/stc/app/app.js"></script>

<!--Mapper-->

        <script type="text/javascript" src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/cvi_tip_lib.js"></script>
        <script type="text/javascript" src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/maputil.js"></script>
        <%--<script type="text/javascript" src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/mapper.js"></script>--%>
