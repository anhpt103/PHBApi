<%@ Assembly Name="BTS.SP.STC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=35f62861b1453f6d" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wpControl_PHAUserControl.ascx.cs" Inherits="BTS.SP.STC.UI.wpControl_PHA.wpControl_PHAUserControl" %>
<!DOCTYPE html>
<%--<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderMain">




</asp:Content>--%>
<html>
<head>
    <title></title>
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/utils/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/utils/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/styles/css/ng-table.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/utils/ng-notify/ng-notify.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/utils/toaster/toaster.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/utils/loading-bar/loading-bar.min.css" rel="stylesheet" />
    <%--<link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/utils/js-xlsx/jszip.js" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/utils/js-xlsx/xlsx.full.min.js" />--%>
<%--    <link href="<%= SPContext.Current.Web.Url%>/_layouts/15/BTS.SP.STC.PHA/utils/telerik/styles/kendo.common.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url%>/_layouts/15/BTS.SP.STC.PHA/utils/telerik/styles/kendo.blueopal.min.css" rel="stylesheet" />--%>

    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/utils/kendo/styles/kendo.common.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/utils/kendo/styles/kendo.rtl.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/utils/kendo/styles/kendo.default.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/utils/kendo/styles/kendo.mobile.all.min.css" rel="stylesheet" />
    <script src="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/lib/xlsx/xlsx.full.min.js"></script>
    <script src="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/lib/xlsx/jszip.js"></script>
    <script src="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/lib/xlsx/xlsx.js"></script>
    <script src="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/lib/grid-ui/ui-grid.min"></script>
    <script src="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/lib/dataTables.min"></script>
    <script src="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/lib/xlsx/FileSaver.js"></script>
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/styles/css/style.css" rel="stylesheet" />
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
    <div ui-view="viewRoot" id="mainApp"></div>
    <script type="text/javascript" data-main="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/main.js" src="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/lib/require.js" charset="UTF8"></script>
</body>
</html>


<!--Mapper-->

<%--<script type="text/javascript" src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/cvi_tip_lib.js"></script>
<script type="text/javascript" src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/maputil.js"></script>--%>
<%--<script type="text/javascript" src="<%=SPContext.Current.Web.Url%>/_layouts/15/stc/js/mapper.js"></script>--%>
