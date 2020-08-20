<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>




<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="BTS.SP.STC.Layouts.FBA.UserEdit"
    DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="PageTitle" Text="EditUser_Title"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
   	<a href="../../settings.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="pagetitle" EncodeMethod="HtmlEncode"/></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow1" runat="server" />
    <a href="UsersDisp.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral2" Text="UserMgmt_Title"
        EncodeMethod="HtmlEncode" runat="server" /></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow2" runat="server" />
    <SharePoint:EncodedLiteral ID="TitleArea" Text="EditUser"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content5" contentplaceholderid="PlaceHolderTitleBreadcrumb" runat="server">
  <SharePoint:UIVersionedContent ID="UIVersionedContent1" UIVersion="3" runat="server"><ContentTemplate>
	<asp:SiteMapPath
		SiteMapProvider="SPXmlContentMapProvider"
		id="ContentMap"
		SkipLinkText=""
		NodeStyle-CssClass="ms-sitemapdirectional"
		RootNodeStyle-CssClass="s4-die"
		PathSeparator="&#160;&gt; "
		PathSeparatorStyle-CssClass = "s4-bcsep"
		runat="server" />
  </ContentTemplate></SharePoint:UIVersionedContent>
  <SharePoint:UIVersionedContent ID="UIVersionedContent2" UIVersion="4" runat="server"><ContentTemplate>
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
		SkipLinkText="" />
  </ContentTemplate></SharePoint:UIVersionedContent>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-descriptiontext">
        <wssuc:InputFormSection runat="server" Title="User Name">
            <template_inputformcontrols>
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="red"></asp:Label>
		    <wssuc:InputFormControl runat="server" LabelText="User Name">
			    <Template_Control>
			    <SharePoint:InputFormTextBox Title="Username" Enabled="false" ReadOnly="true" class="ms-input" Columns="40" maxlength="255" ID="txtUsername" Direction="LeftToRight" Runat="server" />
			    <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator1" ControlToValidate="txtUsername" Display="Dynamic" Runat="server"/>
			    
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" Title="Full Name">
            <template_inputformcontrols>
	        <wssuc:InputFormControl runat="server" LabelText="Full Name">
		        <Template_Control>
		            <SharePoint:InputFormTextBox Title="Full Name" class="ms-input" Columns="40" maxlength="255" ID="txtFullName" Direction="LeftToRight" Runat="server" />
		            <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator2" ControlToValidate="txtFullName" Display="Dynamic" Runat="server"/>
		        </Template_Control>
	        </wssuc:InputFormControl>
        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" Title="Email">
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server" LabelText="Email">
			    <Template_Control>
			    <SharePoint:InputFormTextBox Title="Email" class="ms-input" Columns="40" maxlength="255" ID="txtEmail" Direction="LeftToRight" Runat="server" />
			    <SharePoint:InputFormRegularExpressionValidator ID="InputFormRegExpressionFieldValidator1"  ControlToValidate="txtEmail" Display="Dynamic" runat="server" ValidationExpression=".+\@.+" ErrorMessage="Enter a valid email address."/>
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" id="CategorySection1" Title="Active">
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server">
			    <Template_Control>
				    <asp:CheckBox ID="isActive" ToolTip="Active" Text="Active" runat="server"/>
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" Title="Account Locked">
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server">
			    <Template_Control>
				    <asp:CheckBox ID="isLocked" ToolTip="Account Locked" Text="Account Locked" runat="server"/>
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" id="GroupSection" Title=" Group">
            <template_inputformcontrols>
	        <wssuc:InputFormControl runat="server" LabelText=" Group">
		        <Template_Control>
			        <SharePoint:InputFormCheckBoxList ID="groupList" CssClass="ms-RadioText" ToolTip=" Group" runat="server"/>
		        </Template_Control>
	        </wssuc:InputFormControl>
        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" id="RolesSection" Title=" Group">
            <template_inputformcontrols>
	        <wssuc:InputFormControl runat="server" LabelText="Group">
		        <Template_Control>
			        <SharePoint:InputFormCheckBoxList ID="rolesList" CssClass="ms-RadioText" ToolTip="Group" runat="server"/>
		        </Template_Control>
	        </wssuc:InputFormControl>
        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:ButtonSection runat="server">
            <template_buttons>
            <asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth" OnClick="OnDeleteUser" Text="Delete" id="BtnDelete" accesskey="D"/>
	        <asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth" OnClick="OnResetPassword" Text="ResetPassword" id="BtnReset" accesskey="R"/>
    	    <asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth" OnClick="OnSubmit" Text="Save" id="BtnOk" accesskey="S"/>	    
	    </template_buttons>
        </wssuc:ButtonSection>
    </table>
</asp:Content>
