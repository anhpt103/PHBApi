
<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsersDisp.aspx.cs" Inherits="BTS.SP.STC.Layouts.FBA.UsersDisp" DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Assembly="BTS.SP.STC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=35f62861b1453f6d" Namespace="BTS.SP.STC.Code.FBA.Data" TagPrefix="FBA" %>

<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="PageTitle" Text="User Management"
                               EncodeMethod="HtmlEncode" runat="server"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
             runat="server">
    <a href="../../settings.aspx">
        <SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="User Management" EncodeMethod="HtmlEncode"/>
    </a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow1" runat="server"/>
    <SharePoint:EncodedLiteral ID="TitleArea" Text="User Management"
                               EncodeMethod="HtmlEncode" runat="server"/>
</asp:Content>
<asp:Content ID="Content5" contentplaceholderid="PlaceHolderTitleBreadcrumb" runat="server">
    <SharePoint:UIVersionedContent ID="UIVersionedContent1" UIVersion="3" runat="server">
        <ContentTemplate>
            <asp:SiteMapPath
                SiteMapProvider="SPXmlContentMapProvider"
                id="ContentMap"
                SkipLinkText=""
                NodeStyle-CssClass="ms-sitemapdirectional"
                RootNodeStyle-CssClass="s4-die"
                PathSeparator="&#160;&gt; "
                PathSeparatorStyle-CssClass="s4-bcsep"
                runat="server"/>
        </ContentTemplate>
    </SharePoint:UIVersionedContent>
    <SharePoint:UIVersionedContent ID="UIVersionedContent2" UIVersion="4" runat="server">
        <ContentTemplate>
            <SharePoint:ListSiteMapPath
                runat="server"
                SiteMapProviders="SPSiteMapProvider,SPXmlContentMapProvider"
                RenderCurrentNodeAsLink="false"
                PathSeparator=""
                CssClass="s4-breadcrumb"
                NodeStyle-CssClass="s4-breadcrumbNode"
                CurrentNodeStyle-CssClass="s4-breadcrumbCurrentNode"
                RootNodeStyle-CssClass="s4-breadcrumbRootNode"
                HideInteriorRootNodes="true"
                SkipLinkText=""/>
        </ContentTemplate>
    </SharePoint:UIVersionedContent>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <asp:PlaceHolder ID="ToolBarPlaceHolder" runat="server">
        <SharePoint:EncodedLiteral ID="DescArea" Text="User Management"
                                   EncodeMethod="HtmlEncode" runat="server"/>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:MenuTemplate ID="UserMenu" runat="server">
        <SharePoint:MenuItemTemplate ID="Edit" runat="server" Text="Edit" ImageUrl="/_layouts/images/edititem.gif"
                                     ClientOnClickNavigateUrl="UserEdit.aspx?UserName=%USERNAME%" Title="Edit">
        </SharePoint:MenuItemTemplate>
        <SharePoint:MenuItemTemplate ID="ResetPassword" runat="server" Text="ResetPassword" ImageUrl="/_layouts/images/restore.gif"
                                     ClientOnClickNavigateUrl="UserResetPassword.aspx?UserName=%USERNAME%" Title="Delete">
        </SharePoint:MenuItemTemplate>
        <SharePoint:MenuItemTemplate ID="Delete" runat="server" Text="Delete" ImageUrl="/_layouts/images/delete.gif"
                                     ClientOnClickNavigateUrl="UserDelete.aspx?UserName=%USERNAME%" Title="Delete">
        </SharePoint:MenuItemTemplate>
    </SharePoint:MenuTemplate>
    <div style="padding-bottom: 4px; padding-top: 4px;">
        <wssuc:ToolBar ID="onetidNavNodesTB" runat="server">
            <Template_Buttons>
                <wssuc:ToolBarButton runat="server" Text=" New User" ID="idNewNavNode" ToolTip=" New User"
                                     NavigateUrl="UserNew.aspx" ImageUrl="/_layouts/images/newitem.gif" AccessKey="U"/>
            </Template_Buttons>
        </wssuc:ToolBar>
    </div>
    <div id="SearchControls" runat="server">
        <asp:Label runat="server" ID="lblSearch" Text="Search"/>
        <asp:TextBox ID="SearchText" runat="server"></asp:TextBox>
        <asp:Button ID="Search"
                    runat="server" Text="Search" OnClick="Search_Click"/>
    </div>
    <FBA:FBADataSource runat="server" ID="UserDataSource" ViewName="FBAUsersView"/>
    <SharePoint:SPGridView ID="MemberGrid" runat="server" DataSourceID="UserDataSource"
                           AutoGenerateColumns="false" AllowPaging="true" PageSize="20" AllowSorting="true">
        <Columns>
            <SharePoint:SPMenuField HeaderText="UserName" TextFields="Name" MenuTemplateId="UserMenu"
                                    NavigateUrlFields="Name" NavigateUrlFormat="UserEdit.aspx?UserName={0}" TokenNameAndValueFields="USERNAME=Name"
                                    SortExpression="Name"/>
            <SharePoint:SPBoundField DataField="Email" HeaderText="Email" SortExpression="Email">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Title" HeaderText="Title" SortExpression="Title">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Active" HeaderText="Active" SortExpression="Active">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Locked" HeaderText="Locked" SortExpression="Locked">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="LastLogin" HeaderText="LastLogin" SortExpression="LastLogin">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="IsInSharePoint" HeaderText="IsInSharePoint" SortExpression="IsInSharePoint">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Modified" HeaderText="Modified" SortExpression="Modified">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Created" HeaderText="Created" SortExpression="Created">
            </SharePoint:SPBoundField>
        </Columns>
    </SharePoint:SPGridView>
    <SharePoint:SPGridViewPager ID="SPGridViewPager1" GridViewId="MemberGrid" runat="server"/>
    <p>
        <asp:Label runat="server" ID="lblMessage" ForeColor="Red"/>
    </p>
</asp:Content>