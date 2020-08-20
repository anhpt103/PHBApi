
<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenusNewEdit.aspx.cs" Inherits="BTS.SP.STC.Layouts.FBA.MenusNewEdit" DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script language="javascript">
        try {
            document.getElementById("BtnCancel").focus();
        }
        catch (e) { }

        function confirmDelete() {
            return confirm("<SharePoint:EncodedLiteral runat='server' text='Delete ConfirmText' EncodeMethod='EcmaScriptStringLiteralEncode'/>");
        }
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-descriptiontext">
        <wssuc:InputFormSection runat="server" Title="MenuId">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="MenuId">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="MenuId" class="ms-input" Columns="40" maxlength="255" ID="txtMenuId" Direction="LeftToRight" Runat="server" />
			        <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidatorMenuId" ControlToValidate="txtMenuId" Display="Dynamic" Runat="server"/>
			        <asp:Label ID="lblMessage" runat="server" Text="MenuId Existence Msg" ForeColor="red" Visible="false"></asp:Label>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        
          <wssuc:InputFormSection runat="server" Title="MenuIdParent">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="MenuIdParent">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="MenuIdParent" class="ms-input" Columns="40" maxlength="255" ID="txtMenuIdParent" Direction="LeftToRight" Runat="server" />
			        <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidatorMenuIdParent" ControlToValidate="txtMenuIdParent" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        
        
           <wssuc:InputFormSection runat="server" Title="Menu Name">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Menu Name">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="MenuName" class="ms-input" Columns="40" maxlength="255" ID="txtMenuName" Direction="LeftToRight" Runat="server" />
			        <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidatortxtMenuName" ControlToValidate="txtMenuName" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        
        
         <wssuc:InputFormSection runat="server" Title="Menu Path">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Menu Path">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="Path" class="ms-input" Columns="40" maxlength="255" ID="txtPath" Direction="LeftToRight" Runat="server" />
			        <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator1" ControlToValidate="txtPath" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        
        
         <wssuc:InputFormSection runat="server" Title="Description">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Description">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="Description" class="ms-input" Columns="40" maxlength="255" ID="txtDescription" Direction="LeftToRight" Runat="server" />
			        <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator2" ControlToValidate="txtDescription" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        
           <wssuc:InputFormSection runat="server" Title="Status">
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server">
			    <Template_Control>
				    <asp:CheckBox ID="isStatus" ToolTip="Status" Text="Status" runat="server"/>
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>

        <wssuc:ButtonSection runat="server">
            <template_buttons>
		        <asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth" OnClick="OnSubmit" Text="<%$Resources:wss,multipages_okbutton_text%>" id="BtnOk" accesskey="<%$Resources:wss,okbutton_accesskey%>"/>
                <asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth" OnClick="OnDelete" OnClientClick="if (!confirmDelete()) return false;" Text="Delete" id="BtnDelete" accesskey="D"/>
		    </template_buttons>
        </wssuc:ButtonSection>
    </table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
New, Edit 
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
 <a href="../../settings.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral2" runat="server" text="<%$Resources:wss,settings_pagetitle%>" EncodeMethod="HtmlEncode"/></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow1" runat="server" />
    <a href="RolesDisp.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral3" Text="Add New, Edit" EncodeMethod="HtmlEncode" runat="server" /></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow2" runat="server" />
    <SharePoint:EncodedLiteral ID="TitleArea" Text="New, Edit" EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>