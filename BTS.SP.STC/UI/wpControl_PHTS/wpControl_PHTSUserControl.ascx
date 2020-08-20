<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wpControl_PHTSUserControl.ascx.cs" Inherits="BTS.SP.STC.UI.wpControl_PHTS.wpControl_PHTSUserControl" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Hệ thống quản lý tài sản</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <link rel="shortcut icon" type="image/png" href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/favicon1.ico" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/skin-win8-n/ui.fancytree.min.css" />
    <link type="text/css" rel="stylesheet" href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/grid.css" />
    <link type="text/css" href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/jquery-confirm.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/site.css" />
    <link type="text/css" rel="stylesheet" href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/custom.css" />

    <script type="text/javascript">
        var version = new Date().getTime();
        var currentYear = new Date().getFullYear();
    </script>
    <script>
        console.log('document.location.pathname', document.location.pathname);
        document.write('<base href="' + document.location.pathname + '" />')
    </script>

    <script type="text/html" id="myCustomTemplate">
        <span class="qms-val-panel" data-bind="visible: field.isModified() && !field.isValid(),attr: { class:  (field.isModified() && !field.isValid()) ? 'validationMessage '+ 'validationMessage_' + VM.activePage().id() + '_' + VM.activePage().parentPage.id():'' }"><span data-bind="validationMessage: field"></span><span class="hidden-validate" data-bind="click: function(data, event){ field.isModified(false)}"><i class="fa fa-times" aria-hidden="true"></i></span></span>
    </script>
    <script type="text/javascript" src="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Scripts/require.js" data-main="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/start.js"></script>
</head>
<body class="hold-transition skin-blue-light sidebar-mini login-page qlts-162" id="bodyqlts">
    <div data-bind="page: {id: 'login', title: 'Đăng Nhập', guard: annonymousOnly, sourceOnShow: 'app/HeThong/Login/162.html',  withOnShow: requireVM('162LoginVM')}, visible: !loggedIn">
    </div>
    <div data-bind="page: {id: 'forgotpass', title: 'Đăng Nhập', guard: annonymousOnly, sourceOnShow: 'app/HeThong/Login/forgotPass.html',  withOnShow: requireVM('forgotPassVM')}, visible: !loggedIn">
    </div>
    <div data-bind="page: {id: 'qlts', title: 'Đăng Nhập', guard: annonymousOnly, sourceOnShow: 'app/HeThong/Login/login.html',  withOnShow: requireVM('userLoginVM')}, visible: !loggedIn">
    </div>
    <div data-bind="page: {id: 'register',  title: 'Đăng Ký', guard: annonymousOnly, scrollToTop: true, sourceOnShow: 'app/HeThong/Login/register.html', withOnShow: requireVM('userRegisterVM'), visible: !loggedIn}">
    </div>

    <!-- /.register-box -->

    <div data-bind="visible: loggedIn" style="display: none">
        <div class="wrapper">
            <header class="main-header">
                <!-- Logo -->
                <div style="position: absolute;left: 0px; top: 0px;">
                    <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button" style="color: #fff;font-size: 18px">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </a>
                </div>
                <a href="#!/DashBoard" class="logo" style="background:brown">
                    <!-- mini logo for sidebar mini 50x50 pixels -->
                    <span class="logo-mini">
<%--                        <img src="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/images/logo.png" style="height: 50px; float: left; padding-left: 5px;" />--%>
                    </span>
                    <!-- logo for regular state and mobile devices -->
                        <div class="container-fluid top-hd" style="background-color: brown ;font-family:'Times New Roman', Times, serif;font-style: normal;padding : 0px !important">
                            <div class="container">
                                <div class="container" style="margin: 0 20px;">
                                    <img src="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/images/logo.png" style="height: 40px; margin: 7px 10px 7px 0px; float: left;" />
                                    <span class="brand" style="float: left; line-height: 40px; margin-top: 7px; font-size: 18px; font-weight: bold; color: white;">KHO TÀI SẢN</span>
                                </div>
                            </div>
                        </div>
                </a>


                <!-- Header Navbar: style can be found in header.less -->
                <nav class="navbar navbar-static-top" style="background-color: brown !important" role="navigation">
                    <!-- Sidebar toggle button-->
                    <!--<a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
                        <span class="sr-only">Toggle navigation</span>
                    </a>-->
                    <!-- Navbar Left Menu -->
                    <!-- Navbar Right Menu -->

                    <div class="navbar-custom-menu">
                        <ul class="nav navbar-nav">
                            <li class="dropdown messages-menu tendonvi" style="padding-top: 11px; min-width: 200px; text-align: right; ">
                                <span class="selectdonvi" style="max-width:300px; min-width:150px">
                                    <select id="select-donvi" class="form-control select2 select-donvi" style="width: 100%;">
                                        <option selected="selected">Văn phòng chính phủ</option>
                                        <option>Thành phố Hà Nội</option>
                                        <option>Tỉnh Sóc Trăng</option>
                                        <option>Tỉnh Hải Dương</option>
                                        <option>Tỉnh Tiền Giang</option>
                                        <option>Bộ Y tế</option>
                                        <option>Bộ Tài Nguyên và Môi trường</option>
                                    </select>
                                </span>
                            </li>
                            <!--<li class="donvitonghop" >
        <a style="padding-left: 3px;" data-bind="text: '- ' + VM.tenDonViCha()">Bộ Tài chính</a>
    </li>-->
                            <!-- Messages: style can be found in dropdown.less-->
                            <li class="dropdown messages-menu" style="display:none">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                    <i class="fa fa-envelope-o"></i>
                                    <span class="label label-success">4</span>
                                </a>
                                <ul class="dropdown-menu">
                                    <li class="header">Bạn có 4 tin nhắn</li>
                                    <li>
                                        <!-- inner menu: contains the actual data -->
                                        <ul class="menu">
                                            <li>
                                                <!-- start message -->
                                                <a href="#">
                                                    <div class="pull-left">
                                                        <img class="img-circle" alt="User Image" />
                                                    </div>
                                                    <h4>
                                                        Văn phòng chính phủ
                                                        <small><i class="fa fa-clock-o"></i> 5 phút</small>
                                                    </h4>
                                                    <p>Nhập số dư ban đầu là đất</p>
                                                </a>
                                            </li><!-- end message -->
                                            <li>
                                                <a href="#">
                                                    <div class="pull-left">
                                                        <img class="img-circle" alt="user image" />
                                                    </div>
                                                    <h4>
                                                        Admin- Bộ Tài chính
                                                        <small><i class="fa fa-clock-o"></i> 2 giờ</small>
                                                    </h4>
                                                    <p>Tăng nguyên giá tài sản hữu hình khác</p>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#">
                                                    <div class="pull-left">
                                                        <img class="img-circle" alt="user image" />
                                                    </div>
                                                    <h4>
                                                        Admin- Bộ Tài chính
                                                        <small><i class="fa fa-clock-o"></i> Hôm nay</small>
                                                    </h4>
                                                    <p>Tăng nguyên giá tài sản là nhà</p>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#">
                                                    <div class="pull-left">
                                                        <img class="img-circle" alt="user image" />
                                                    </div>
                                                    <h4>
                                                        Admin- Bộ Tài chính
                                                        <small><i class="fa fa-clock-o"></i> Hôm qua</small>
                                                    </h4>
                                                    <p>Giảm nguyên giá tài sản là cây lâu năm, súc vật</p>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#">
                                                    <div class="pull-left">
                                                        <img class="img-circle" alt="user image" />
                                                    </div>
                                                    <h4>
                                                        Admin- Bộ Tài chính
                                                        <small><i class="fa fa-clock-o"></i> 2 ngày</small>
                                                    </h4>
                                                    <p>Thay đổi thông tin tài sản là đất</p>
                                                </a>
                                            </li>
                                        </ul>
                                    </li>
                                    <li class="footer"><a href="#">Xem tất cả tin nhắn</a></li>
                                </ul>
                            </li>
                            <!-- Notifications: style can be found in dropdown.less -->
                            <li class="dropdown notifications-menu">
                                <a href="#" data-bind="attr: { href: VM.currentUrl2() + 'ThongBao'}" class="dropdown-toggle" style=" font-size: 20px;">
                                    <i class="fa fa-bell-o"></i>
                                    <span class="label label-warning" data-bind="text: VM.totalThongBao()"></span>
                                </a>

                            </li>
                            <li class="dropdown notifications-menu">
                                <a href="#" data-target="#SystemNotification" data-toggle="modal" class="dropdown-toggle" style=" font-size: 20px;">
                                    <i class="fa fa-envelope-o"></i>
                                    <span class="label label-danger" data-bind="text: VM.totalThongBaoDonVi()"></span>
                                </a>

                            </li>
                            <!-- Tasks: style can be found in dropdown.less -->
                            <!-- User Account: style can be found in dropdown.less -->
                            <li class="dropdown user user-menu">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown" style="height: 55px;
    padding-right: 5px;">
                                    <div id="uphoto">
                                        <%--<img id="imgAvata" data-bind="attr: { src: svc.getAvatar(myAvatar()) }" class="user-image" alt="User Image" />--%>
                                        <span id="userId" class="hidden-Indentity" data-bind="text: myIdentity"></span>
                                    </div>
                                </a>
                                <ul class="dropdown-menu">
                                    <!-- User image -->
                                    <li class="user-header">
                                        <%--<img data-bind="attr: { src: svc.getAvatar(myAvatar()) }" class="img-circle" alt="User Image" />--%>
                                        <p>
                                            Quản trị hệ thống
                                            <small>Quản lý tài sản- Bộ Tài Chính</small>
                                        </p>
                                    </li>
                                    <!-- Menu Body -->
                                    <!-- Menu Footer-->
                                    <li class="user-footer">
                                        <div class="pull-left">
                                            <a href="#!/Profile" class="btn btn-default btn-flat">Cá nhân</a>
                                        </div>
                                        <div class="pull-right">
                                            <a id="logout" data-bind="click: logout" href="#" class="btn btn-default btn-flat">Thoát</a>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                            <!-- Control Sidebar Toggle Button -->
                            <!--<li style="display:none">
        <a href="#" data-toggle="control-sidebar"><i class="fa fa-gears"></i></a>
    </li>-->
                        </ul>
                    </div>
                </nav>
            </header>
            <!-- Left side column. contains the logo and sidebar -->
            <aside class="main-sidebar">
                <!-- sidebar: style can be found in sidebar.less -->
                <section class="sidebar">
                    <!-- /.search form -->
                    <!-- sidebar menu: : style can be found in sidebar.less -->
                    <ul class="sidebar-menu">

                        <!-- ko foreach: ko.utils.arrayFilter($__page__.children(), function(item){ return item.val('isMenu') && item.id() != 'DashBoard'; })-->
                        <!-- ko if: $root.isAuthorized($data) -->
                        <!-- ko if: children().length > 0 -->
                        <!-- Messages: style can be found in dropdown.less-->
                        <li class="treeview" data-bind="css: {active: isVisible}">
                            <!-- ko if: ko.utils.arrayFilter(children(), function(item){ return item.val('isMenu'); }).length > 0 -->
                            <a href="#" data-toggle="dropdown" aria-expanded="false">

                                <i data-bind="attr: { class: val('icon') }"></i>
                                <span><!-- ko text: val('title') --><!--/ko--></span>
                                <i class="fa fa-angle-right pull-right"></i>
                            </a>
                            <ul class="treeview-menu" data-bind="foreach: ko.utils.arrayFilter(children(), function(item){ return item.val('isMenu'); })">

                                <!-- ko if: $root.isAuthorized($data) -->
                                <!-- ko if: ko.utils.arrayFilter(children(), function(item){ return item.val('isMenu'); }).length == 0 -->
                                <!-- ko if: val('isSeparator') -->
                                <li class="header">
                                    <!-- ko text: val('title') --><!--/ko-->
                                </li>
                                <!-- /ko -->
                                <!-- ko ifnot: val('isSeparator') -->
                                <li data-bind="css: {active: isVisible}">
                                    <a data-bind="'page-href': $data"><i class="fa fa-angle-double-right"></i> <!-- ko text: val('title') --><!--/ko--></a>
                                </li>
                                <!-- /ko -->
                                <!-- /ko -->
                                <!-- ko if: ko.utils.arrayFilter(children(), function(item){ return item.val('isMenu'); }).length > 0 -->
                                <li data-bind="css: {active: isVisible}">
                                    <a data-bind="'page-href': ko.utils.arrayFilter(children(), function (item) {
    return item.val('isMenu');
})[0]">
                                        <i class="fa fa-angle-double-right"></i> <span><!-- ko text: val('title') --><!--/ko--></span>
                                        <i class="fa fa-angle-right pull-right"></i>
                                    </a>
                                    <ul class="treeview-menu" data-bind="foreach: ko.utils.arrayFilter(children(), function(item){ return item.val('isMenu'); })">
                                        <!-- ko if: $root.isAuthorized($data) -->
                                        <li data-bind="css: {active: isVisible}">
                                            <a data-bind="'page-href': $data"> <i class="fa fa-circle" style="font-size: 8px; padding-top: 3px; color:#ccc"></i> <!-- ko text: val('title') --><!--/ko--></a>

                                        </li>
                                        <!-- /ko -->
                                    </ul>

                                </li>
                                <!-- /ko-->
                                <!-- /ko -->
                            </ul>
                            <!-- /ko-->
                            <!-- ko ifnot: ko.utils.arrayFilter(children(), function(item){ return item.val('isMenu'); }).length > 0 -->
                            <a data-bind="'page-href': $data"><i data-bind="attr: { class: val('icon')}"></i> <span><!-- ko text: val('title') --><!--/ko--></span></a>
                            <!-- /ko -->
                        </li>
                        <!-- /ko -->
                        <!-- ko if: children().length == 0-->
                        <li class="treeview" data-bind="css: {active: isVisible}">
                            <a data-bind="'page-href': $data"><i data-bind="attr: { class: val('icon')}"></i> <span><!-- ko text: val('title') --><!--/ko--></span></a>
                        </li>
                        <!-- /ko -->
                        <!-- /ko -->
                        <!-- /ko -->
                    </ul>
                </section>
                <!-- /.sidebar -->
            </aside>
            <!-- Content Wrapper. Contains page content -->
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <!--<section class="content-header" data-bind="visible: customUI.menuHeader()" style="padding-top:5px;">
                    <ol class="breadcrumb" style="    margin-bottom: -10px;">
                        <li data-bind="visible: customUI.menuHeader()" style=" margin-top: -5px; ">
                            <div class="selectdonvi" style="max-width: 700px; min-width: 250px; text-align: right;">
                                <select id="select-donvi" class="form-control select2 select-donvi" style="width: 100%;">
                                    <option selected="selected">Văn phòng chính phủ</option>
                                    <option>Thành phố Hà Nội</option>
                                    <option>Tỉnh Sóc Trăng</option>
                                    <option>Tỉnh Hải Dương</option>
                                    <option>Tỉnh Tiền Giang</option>
                                    <option>Bộ Y tế</option>
                                    <option>Bộ Tài Nguyên và Môi trường</option>
                                </select>
                            </div>
                        </li>
                        <li class="donvitonghop" data-bind="visible: customUI.menuHeader()">
                            <a data-bind="text: '- ' + VM.myDonViChaCap1()">Viện hàn lâm khoa học Việt Nam</a>
                        </li>
                    </ol>
                </section>-->
                <section class="content-header" style="padding-top:10px">
                    <ol class="breadcrumb" style="padding-left: 5px;">
                        <li><a href="#!/DashBoard"><i class="fa fa-home"></i> Trang chủ</a></li>
                        <!-- ko foreach: activePageChildren -->
                        <!-- ko if: id() != 'create' && id() != 'Form' &&  id() != 'edit' && id() != 'detail' && !id().startsWith("create") && !id().startsWith("edit") && !id().startsWith("detail") && !id().startsWith("ChiTiet") && !id().startsWith("Sua") && !id().startsWith("ThemMoi")  -->
                        <li><a data-bind="'page-href': $data, css: {active: route.length == 0}" class="normal"><!-- ko text: titleOrId() --><!--/ko--></a></li>
                        <!-- /ko -->
                        <!-- /ko -->
                    </ol>
                    <h1 id="TitlePage" data-bind="visible: false">
                        <i class="fa fa-dashboard"></i>
                        <!-- ko text: currentHeader --><!-- /ko -->

                    </h1>

                </section>
                <!-- Main content -->
                <section class="content" id="menu" >

                    <div title="DashBoard" data-bind="page: { id: 'DashBoard', role: 'start', guard: isAuthorized, title: 'Thông tin đơn vị', sourceOnShow: '/_layouts/15/BTS.SP.STC.PHT/app/HeThong/DashBoard/dashboard.html', withOnShow: requireVM('dashBoardVM') }">
                    </div>
                    <div data-bind="page: { id: 'Profile', guard: isAuthorized, title: 'Trang cá nhân', sourceOnShow: '/_layouts/15/BTS.SP.STC.PHT/app/HeThong/Profile/index.html', withOnShow: requireVM('profileVM') }">
                    </div>
                </section><!-- /.content -->
            </div><!-- /.content-wrapper -->
            <div class="clearfix"></div>
            <!-- Control Sidebar -->
            <aside class="control-sidebar control-sidebar-light" style="display:none">
                <!-- Create the tabs -->
                <ul class="nav nav-tabs nav-justified control-sidebar-tabs">
                    <li><a href="#control-sidebar-home-tab" data-toggle="tab"><i class="fa fa-home"></i></a></li>
                    <li class="active"><a href="#control-sidebar-settings-tab" data-toggle="tab"><i class="fa fa-gears"></i></a></li>
                </ul>
                <!-- Tab panes -->
                <div class="tab-content">
                    <!-- Home tab content -->
                    <div class="tab-pane" id="control-sidebar-home-tab">
                        <h3 class="control-sidebar-heading">Recent Activity</h3>

                        <h3 class="control-sidebar-heading">Tasks Progress</h3>

                    </div><!-- /.tab-pane -->
                    <div class="tab-pane active" id="control-sidebar-settings-tab">
                        <h3 class="control-sidebar-heading">Giao diện</h3>
                        <div class="form-group">
                            <select class="form-control" data-bind="value:customUI.selectTemplate">
                                <option value="qlts" selected>Quản lý tài sản</option>
                                <option value="qlts-162">Quản lý tài sản - 162</option>
                            </select>
                        </div><!-- /.form-group -->

                    </div><!-- /.tab-pane -->
                    <div class="tab-pane" id="control-sidebar-home-tab">

                    </div>
                </div>
            </aside>
            <!-- Control Sidebar -->
            <!-- /.control-sidebar -->
            <!-- /.control-sidebar -->
            <!-- Add the sidebar's background. This div must be placed
             immediately after the control sidebar -->
            <div class='control-sidebar-bg'></div>
        </div><!-- ./wrapper -->
        <footer class="main-footer" style="margin-left:0px;background-color: #fffaf0 !important">
                <div class="container-fluid top-hd" style="height: 53px; background-color: #fffaf0;">
                    <div class="container">
                        <div class="container">
                            <img src="<%= SPContext.Current.Web.Url %>/_layouts/15/stc/imgs/LOGOBT.png" style="height: 40px; margin: 7px 10px 7px 0px; float: left;" />
                            <span class="brand" style="float: left; line-height: 40px; margin-top: 7px; font-size: 14px; font-weight: bold; color: brown;">B&T HiTech Solution Co,Ltd Copyright © 2018</span>
                        </div>
                    </div>
                </div>
        </footer>

    </div>
    <!--<div class="chat-bot-container" data-bind="visible: loggedIn" style="display:none">
        <a class="pulse-button" id="floating-button" href="#!/QuanLyTaiSan/DanhSachTaiSan" style="display: block; background: #337ab7d6;">
            <i class="fa fa-home" style="    padding-top: 5px;"></i>
        </a>
        <button class="pulse-button" id="floating-button" data-target="#SystemNotification" data-toggle="modal" style="display: block; background: #dd4b39bf;">
            <i class="fa fa-envelope-o"></i>
            <span class="label label-info" data-bind="text: VM.totalThongBaoDonVi()"></span>
        </button>
        <button class="pulse-button" id="floating-button" style="display: block; background-color: #00a65ad1;">
            <i class="fa fa-bell-o"></i>
            <span class="label label-warning" data-bind="text: VM.totalThongBao()"></span>
        </button>
    </div>-->

    <div id="loadingIcon" style="position: fixed; width: 100%; height: 100%; z-index: 9999; display: none; background:rgba(255,255,255,0.5);">
        <div class="overlay" style="position: fixed; top: 50%; left: 50%;  z-index: 1000; font-size:24px">
            <img class="loadingImg" src="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHT/Content/images/Load.gif" alt="loading" style="position: absolute; top: 50%; left: 52%;  width: 60px; height:60px; z-index: 1000;" />
        </div>
    </div>
    <!-- /.modal-content -->
    <div id="SystemNotification" class="modal" role="dialog">
        <div class="modal-dialog" style="    z-index:9999">
            <div class="modal-content">
                <div class="modal-header modal-header-warning">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title"><i class="fa fa-envelope-o"></i> Thông báo hệ thống</h4>
                </div>
                <div class="modal-body">
                    <!-- ko foreach: VM.thongBaoDonVi()-->
                    <div class="row margin-bottom-lg margin-top">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <h4 class="" style="margin-bottom: 5px; border-bottom: 1px solid #eee;padding-bottom: 5px;" data-bind="text: TieuDe">More Icons</h4>
                            <p data-bind="html: NoiDung">
                                Get 893 icons right now with FA Free, plus another 1,283 icons with Pro, which also gets you another 46 icon category packs as we finish them!
                            </p>
                            <span style="font-size:11px; font-style:italic; color: #808080">Thông báo từ: <!--ko text: TenDonViThongBao--><!--/ko--></span>
                            <span style="font-size:11px; font-style:italic; color: #808080; float:right">Áp dụng từ <!--ko text: toDateString(NgayBatDau)--><!--/ko--> đến <!--ko text: toDateString(NgayKetThuc)--><!--/ko-->!</span>
                        </div>
                    </div>
                    <!--/ko-->
                </div>
                <div class="modal-footer">
                    <!--<button type="button" id="btn-ok-modal" class="btn btn-outline">Đồng ý</button>-->
                    <button type="button" id="btn-close-modal" class="btn btn-default" data-dismiss="modal">Đóng</button>
                </div>
            </div>
        </div>
    </div>
    <div id="modelNotification" class="modal modal-primary" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title">Primary Modal</h4>
                </div>
                <div class="modal-body">
                    <p>One fine body…</p>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btn-ok-modal" class="btn btn-outline">Đồng ý</button>
                    <button type="button" id="btn-close-modal" class="btn btn-outline pull-right" data-dismiss="modal">Đóng</button>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
