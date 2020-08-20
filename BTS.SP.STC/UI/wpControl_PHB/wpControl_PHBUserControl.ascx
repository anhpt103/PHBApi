<%@ Assembly Name="BTS.SP.STC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=35f62861b1453f6d" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wpControl_PHBUserControl.ascx.cs" Inherits="BTS.SP.STC.UI.wpControl_PHB.wpControl_PHBUserControl" %>

<!DOCTYPE html>
<html>
<head>
    <title></title>
    <link href="<%=SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/utils/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="<%=SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/utils/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/css/ng-table.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/utils/ng-notify/ng-notify.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/utils/toaster/toaster.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/utils/loading-bar/loading-bar.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/utils/angular-confirm/angular-confirm.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/utils/ui-grid/ui-grid.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/utils/telerik/styles/kendo.common.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/utils/telerik/styles/kendo.blueopal.min.css" rel="stylesheet" />

    <%--<link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/utils/devExpress/css/dx.common.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/utils/devExpress/css/dx.light.css" rel="stylesheet" />--%>

    <link href="<%=SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/css/style.css" rel="stylesheet" />
    <script type="text/javascript">
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
    <div ui-view="viewRoot" id="mainApp">
    </div>
    <script type="text/javascript" data-main="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/main.js" src="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHB/lib/require.js" charset="UTF8"></script>
</body>
</html>
