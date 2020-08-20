<%@ Assembly Name="BTS.SP.STC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=35f62861b1453f6d" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wpControl_PHDUserControl.ascx.cs" Inherits="BTS.SP.STC.UI.wpControl_PHD.wpControl_PHDUserControl" %>
<!DOCTYPE html>
<html>
<head>
    <title></title>
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHD/styles/css/bootstrap.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHD/styles/css/font-awesome.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHD/styles/css/ng-table.min.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHD/utils/ng-notify/ng-notify.min.css" rel="stylesheet" />
    <link href="<%=SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHD/styles/css/style.css" rel="stylesheet" />
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHD/utils/loading-bar/loading-bar.min.css" rel="stylesheet" />
</head>
<body>
     <div ui-view="viewRoot" id="mainApp"></div>
     <script type="text/javascript" data-main="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHD/main.js" src="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHD/lib/require.js" charset="UTF8"></script>
</body>
</html>
