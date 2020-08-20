<%@ Assembly Name="BTS.SP.STC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=35f62861b1453f6d" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wpControl_PHCUserControl.ascx.cs" Inherits="BTS.SP.STC.UI.wpControl_PHC.wpControl_PHCUserControl" %>
<!DOCTYPE html>
<html>
<head>
    <title></title>
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/styles/css/bootstrap.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/styles/css/font-awesome.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/styles/fonts/fontawesome-webfont.woff>" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/styles/fonts/glyphicons-halflings-regular.woff2>" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/styles/css/ng-table.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/utils/ng-notify/ng-notify.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/utils/loading-bar/loading-bar.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/styles/css/kendo.common.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/styles/css/kendo.blueopal.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/styles/css/style.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/styles/css/vudq-common-style.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/utils/ui-grid/ui-grid.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/utils/ui-grid/ui-grid-theme-sky.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/utils/ui-select/select.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/utils/ui-select/select2.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/utils/ui-select/selectize.default.css" rel="stylesheet" />
    <%--<link href="<%=SPContext.Current.Web.Url%>/_layouts/15/BTS.SP.STC.PHC/styles/css/dx.common.css" rel="stylesheet" />
    <link href="<%=SPContext.Current.Web.Url%>/_layouts/15/BTS.SP.STC.PHC/styles/css/dx.light.css" rel="stylesheet" />
    <link href="<%=SPContext.Current.Web.Url%>/_layouts/15/BTS.SP.STC.PHC/styles/css/ejthemes/ej.widgets.core.bootstrap.min.css" rel="stylesheet" />
    <link href="<%=SPContext.Current.Web.Url%>/_layouts/15/BTS.SP.STC.PHC/styles/css/ejthemes/bootstrap-theme/ej.web.all.min.css" rel="stylesheet" />
    <link href="<%=SPContext.Current.Web.Url%>/_layouts/15/BTS.SP.STC.PHC/styles/css/ejthemes/responsive-css/ej.responsive.css" rel="stylesheet" />
    <link href="<%=SPContext.Current.Web.Url%>/_layouts/15/BTS.SP.STC.PHC/styles/css/ejthemes/ribbon-css/ej.icons.css" rel="stylesheet" />--%>
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
        #loading-bar .bar {
            position: fixed !important;
            top: 5px !important;
        }
    </style>
</head>
<body>
     <div ui-view="viewRoot" id="mainApp" style="padding-top: 50px;"></div>
     <script type="text/javascript"
           
          data-main="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString()%>/main.js"
          src="<%= SPContext.Current.Web.Url +"/"+System.Configuration.ConfigurationManager.AppSettings["pathLayout_PHC"].ToString() %>/lib/require.js"
          charset="UTF8">

     </script>
</body>
</html>


