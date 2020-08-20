<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitNewEdit.aspx.cs" Inherits="BTS.SP.STC.Layouts.FBA.UnitNewEdit" DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
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
     <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-Emailtext">
        <wssuc:InputFormSection runat="server" Title="UnitId">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="UnitId">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="UnitId" class="ms-input" Columns="40" maxlength="255" ID="txtUnitId" Direction="LeftToRight" Runat="server" />
			        <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidatorUnitId" ControlToValidate="txtUnitId" Display="Dynamic" Runat="server"/>
			        <asp:Label ID="lblMessage" runat="server" Text="UnitId Existence Msg" ForeColor="red" Visible="false"></asp:Label>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        
          <wssuc:InputFormSection runat="server" Title="UnitIdParent">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="UnitIdParent">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="UnitIdParent" class="ms-input" Columns="40" maxlength="255" ID="txtUnitIdParent" Direction="LeftToRight" Runat="server" />
			        <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidatorUnitIdParent" ControlToValidate="txtUnitIdParent" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        
        
           <wssuc:InputFormSection runat="server" Title="Unit Name">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Unit Name">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="UnitName" class="ms-input" Columns="40" maxlength="255" ID="txtUnitName" Direction="LeftToRight" Runat="server" />
			        <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidatortxtUnitName" ControlToValidate="txtUnitName" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        
        
         <wssuc:InputFormSection runat="server" Title="Phone">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Phone">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="Phone" class="ms-input" Columns="40" maxlength="255" ID="txtPhone" Direction="LeftToRight" Runat="server" />
			        <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator1" ControlToValidate="txtPhone" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        
        
         <wssuc:InputFormSection runat="server" Title="Email">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Email">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="Email" class="ms-input" Columns="40" maxlength="255" ID="txtEmail" Direction="LeftToRight" Runat="server" />
			        <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator2" ControlToValidate="txtEmail" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        
           <wssuc:InputFormSection runat="server" Title="Address">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Address">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="Address" class="ms-input" Columns="40" maxlength="255" ID="txtAddress" Direction="LeftToRight" Runat="server" />
			        <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator3" ControlToValidate="txtAddress" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
         
           <wssuc:InputFormSection runat="server" Title="Description">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Description">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="Description" class="ms-input" Columns="40" maxlength="255" ID="txtDescription" Direction="LeftToRight" Runat="server" />
			        <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator4" ControlToValidate="txtDescription" Display="Dynamic" Runat="server"/>
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
    <a href="UnitDisp.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral3" Text="New, Edit" EncodeMethod="HtmlEncode" runat="server" /></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow2" runat="server" />
    <SharePoint:EncodedLiteral ID="TitleArea" Text="Add New, Edit" EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
