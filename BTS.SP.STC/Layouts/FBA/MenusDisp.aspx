<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenusDisp.aspx.cs" Inherits="BTS.SP.STC.Layouts.FBA.MenusDisp" DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Register TagPrefix="FBA" Namespace="BTS.SP.STC.Code.FBA.Data" Assembly="BTS.SP.STC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=35f62861b1453f6d" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>


<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div style="padding-top: 4px;padding-bottom: 4px;">
            <wssuc:ToolBar ID="onetidNavNodesTB" runat="server">
                <Template_Buttons>
                    <wssuc:ToolBarButton runat="server" Text="New" ID="idNewNavNode" ToolTip="New Menus"
                        NavigateUrl="MenusNewEdit.aspx?MenuId=0" ImageUrl="/_layouts/images/newitem.gif" AccessKey="U" />
                </Template_Buttons>
            </wssuc:ToolBar>
        </div>
    

     <SharePoint:MenuTemplate ID="menusMenu" runat="server">
           <SharePoint:MenuItemTemplate ID="EditMenu" runat="server" Text="Edit" ImageUrl="/_layouts/images/edititem.gif"
                                     ClientOnClickNavigateUrl="MenusNewEdit.aspx?MenuId=%MenuId%" Title="MenuId">
        </SharePoint:MenuItemTemplate>
    </SharePoint:MenuTemplate>

    <FBA:FBADataSource runat="server" ID="MenusDataSource" ViewName="FBAMenuView" />
    <SharePoint:SPGridView ID="MenusGrid" runat="server" DataSourceID="MenusDataSource"
        AutoGenerateColumns="false" AllowPaging="true" PageSize="3" AllowSorting="true">
        <Columns>
            <SharePoint:SPMenuField HeaderText="MenuId" TextFields="MenuId" MenuTemplateId="menusMenu"
                NavigateUrlFields="MenuId" NavigateUrlFormat="MenusNewEdit.aspx?MenuId={0}" TokenNameAndValueFields="MenuId=MenuId"
                SortExpression="MenuId" />
            <SharePoint:SPBoundField HeaderText="MenuName" DataField="MenuName" SortExpression="MenuName" />
            <SharePoint:SPBoundField HeaderText="Path" DataField="Path" SortExpression="Path" />
            <SharePoint:SPBoundField HeaderText="Description" DataField="Description" SortExpression="Description" />
            <SharePoint:SPBoundField HeaderText="Status" DataField="Status" SortExpression="Status" />
        </Columns>
    </SharePoint:SPGridView>
    <SharePoint:SPGridViewPager ID="SPGridViewPagerMenus" GridViewId="MenusGrid" runat="server" />
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Menus Management
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
 <a href="../../settings.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral2" runat="server" text="<%$Resources:wss,settings_pagetitle%>" EncodeMethod="HtmlEncode"/></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow1" runat="server" />
    <a href="MenusDisp.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral3" Text="Menus Management" EncodeMethod="HtmlEncode" runat="server" /></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow2" runat="server" />
    <SharePoint:EncodedLiteral ID="TitleArea" Text="List" EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
