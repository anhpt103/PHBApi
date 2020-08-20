<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>



<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserNew.aspx.cs" Inherits="BTS.SP.STC.Layouts.FBA.UserNew"
    DynamicMasterPageFile="~masterurl/default.master" %>
    
<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="PageTitle" Text="NewUser_Title"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
   	<a href="../../settings.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="<%$Resources:wss,settings_pagetitle%>" EncodeMethod="HtmlEncode"/></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow1" runat="server" />
    <a href="UsersDisp.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral2" Text="UserMgmt_Title"
        EncodeMethod="HtmlEncode" runat="server" /></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow2" runat="server" />
    <SharePoint:EncodedLiteral ID="TitleArea" Text="NewUser_Title"
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
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-descriptiontext">
        <!-- User Name -->
        <wssuc:InputFormSection runat="server" Title="Tên đăng nhập">
            <template_inputformcontrols>
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="red"></asp:Label>
		        <wssuc:InputFormControl runat="server" LabelText="Tên đăng nhập:">
			        <Template_Control>
			            
			            <SharePoint:InputFormTextBox Title="UserName" class="ms-input" Columns="40" maxlength="255" ID="txtUsername" Direction="LeftToRight" Runat="server" autocomplete="off" />
			            <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator1" ControlToValidate="txtUsername" Display="Dynamic" Runat="server"/>			            
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Full Name -->
        <wssuc:InputFormSection runat="server" Title="Tên đầy đủ">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Tên đầy đủ">
			        <Template_Control>
			            <SharePoint:InputFormTextBox Title="FullName" class="ms-input" Columns="40" maxlength="255" ID="txtFullName" Direction="LeftToRight" Runat="server" autocomplete="off" />
			            <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator2" ControlToValidate="txtFullName" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Password -->
        <wssuc:InputFormSection runat="server" Title="Mật khẩu">
            <template_inputformcontrols>
                <asp:Label ID="lblPasswordMessage" runat="server" Text="" ForeColor="red"></asp:Label>
		        <wssuc:InputFormControl runat="server" LabelText="Mật khẩu">
			        <Template_Control>
			            <SharePoint:InputFormTextBox Title="password" ToolTip="Password" class="ms-input" Columns="40" textmode="Password" maxlength="255" ID="txtPassword" Direction="LeftToRight" Runat="server" autocomplete="off" />
			            <SharePoint:InputFormRequiredFieldValidator ID="txtPasswordValidator1" ControlToValidate="txtPassword" Display="Dynamic" Runat="server"/>
			            <SharePoint:InputFormTextBox Title="confirm" ToolTip="ConfirmPassword" class="ms-input" Columns="40" textmode="Password" maxlength="255" ID="txtConfirm" Runat="server" autocomplete="off" />
			            <SharePoint:InputFormRequiredFieldValidator ID="txtConfirmValidator1" ControlToValidate="txtConfirm" Display="Dynamic" Runat="server"/>
			            <SharePoint:InputFormCompareValidator ID="InputFormCompareValidatorPassword" SetFocusOnError="true"  ControlToValidate="txtConfirm" ControlToCompare="txtPassword" Type="String" Display="Dynamic" Operator="Equal" ErrorMessage="Password and confirmation do not match." runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Security Question -->
       <%-- <wssuc:InputFormSection id="QuestionSection" runat="server" Title="Security Question">
            <template_inputformcontrols>
                <asp:Label ID="lblQuestionMessage" runat="server" Text="" ForeColor="red"></asp:Label>
		        <wssuc:InputFormControl runat="server" LabelText="Security Question">
			        <Template_Control>
			            <SharePoint:InputFormTextBox Title="Security Question" class="ms-input" Columns="40" maxlength="255" ID="txtQuestion" Direction="LeftToRight" Runat="server" autocomplete="off" />
			            <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator3" ControlToValidate="txtQuestion" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Security Answer -->
        <wssuc:InputFormSection id="AnswerSection" runat="server" Title="Security Answer">
            <template_inputformcontrols>
                <asp:Label ID="lblAnswerMessage" runat="server" Text="" ForeColor="red"></asp:Label>
		        <wssuc:InputFormControl runat="server" LabelText="Security Answer">
			        <Template_Control>
			            <SharePoint:InputFormTextBox Title="Security Answer" class="ms-input" Columns="40" maxlength="255" ID="txtAnswer" Direction="LeftToRight" Runat="server" autocomplete="off" />
			            <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator4" ControlToValidate="txtAnswer" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>--%>
        <!-- Email Address -->
       <%-- <wssuc:InputFormSection runat="server" Title="Email">
            <template_inputformcontrols>
                <asp:Label ID="lblEmailMessage" runat="server" Text="" ForeColor="red"></asp:Label>
		        <wssuc:InputFormControl runat="server" LabelText="Email">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="Email" class="ms-input" Columns="40" maxlength="255" ID="txtEmail" Direction="LeftToRight" Runat="server" autocomplete="off" />
			        <SharePoint:InputFormRegularExpressionValidator ID="InputFormRegExpressionFieldValidator1"  ControlToValidate="txtEmail" Display="Dynamic" runat="server" ValidationExpression=".+\@.+" ErrorMessage="Enter a valid email address."/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Is Active -->--%>
        <wssuc:InputFormSection runat="server" id="ActiveSection" Title="Kích hoạt">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server">
			        <Template_Control>
				        <asp:CheckBox ID="isActive" Checked="true" ToolTip="Active" Text="Active" runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Groups -->
        <wssuc:InputFormSection runat="server" id="GroupSection" Title="Ứng dụng">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Ứng dụng">
			        <Template_Control>
				        <SharePoint:InputFormCheckBoxList ID="groupList" CssClass="ms-RadioText" ToolTip="Group" runat="server" />
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Roles -->
      <%--  <wssuc:InputFormSection runat="server" id="RolesSection" Title="Group">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Group">
			        <Template_Control>
				        <SharePoint:InputFormCheckBoxList ID="rolesList" CssClass="ms-RadioText" ToolTip="Group" runat="server" />
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>--%>
        <!-- Email -->
       <%-- <wssuc:InputFormSection runat="server" id="EmailSection" Title="SendEmail">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server">
			        <Template_Control>
			            <SharePoint:InputFormCheckBox id="emailUser" runat="server" Checked="False" LabelText="SendEmail" ToggleChildren="true">
				            <SharePoint:EncodedLiteral ID="EncodedLiteral3" runat="server" text="Subject Email" EncodeMethod='HtmlEncode'/>
					        <br>
					        <Template_Control>
					            <SharePoint:InputFormTextBox Title="Subject Email" class="ms-input" ID="txtEmailSubject" Columns="40" Runat="server" MaxLength="255" />
					            <SharePoint:InputFormRequiredFieldValidator id="ReqValEmailSubject" runat="server" BreakBefore=true BreakAfter=true EnableClientScript="false" ControlToValidate="txtEmailSubject"/>
					        </Template_Control>
					        <br>
					        <SharePoint:EncodedLiteral ID="EncodedLiteral4" runat="server" text="Body" EncodeMethod='HtmlEncode'/>
					        <br>
					        <Template_Control>
					            <SharePoint:InputFormTextBox Title="Body Title" class="ms-input" ID="txtEmailBody" Runat="server" TextMode="MultiLine" Columns="40" Rows="8" Cols=64 MaxLength=512 />
					        </Template_Control>
			            </SharePoint:InputFormCheckBox>
			         </Template_Control>
			    </wssuc:InputFormControl>   
		    </template_inputformcontrols>
        </wssuc:InputFormSection>--%>
        <!-- Buttons -->
        <wssuc:ButtonSection runat="server">
            <template_buttons>
		        <asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth" OnClick="OnSubmit" Text="Save" id="BtnOk" accesskey="S"/>
		    </template_buttons>
        </wssuc:ButtonSection>
    </table>
</asp:Content>
