<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserResetPassword.aspx.cs" Inherits="BTS.SP.STC.Layouts.FBA.UserResetPassword"
    DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="PageTitle" Text=" ResetPasswordUser"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
   	<a href="../../settings.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="pagetitle" EncodeMethod="HtmlEncode"/></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow1" runat="server" />
    <a href="UsersDisp.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral2" Text="UserMgmt"
        EncodeMethod="HtmlEncode" runat="server" /></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow2" runat="server" />
    <SharePoint:EncodedLiteral ID="TitleArea" Text=" Reset Password User"
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
    <script language="javascript">
        function confirmResetPassword() {
            if (Page_ClientValidate()) {
                return confirm("<SharePoint:EncodedLiteral runat='server' text='Reset Password Confirm' EncodeMethod='EcmaScriptStringLiteralEncode'/>");
            }
            else {
                return false;
            }

        }
        function ResetAutoPasswordOnClick()
        {
	        if (browseris.ie4up || browseris.nav6up)
	        {
		        document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(txtNewPassword.ClientID),Response.Output);%>).disabled = true;
		        ValidatorEnable(document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(InputFormRequiredFieldValidatorNewPassword.ClientID),Response.Output);%>),false);
                document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(chkSendEmail.ClientID),Response.Output);%>).disabled = true;
	        }
        }
        function ResetSelectPasswordOnClick()
        {
	        if (browseris.ie4up || browseris.nav6up)
	        {
		        document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(txtNewPassword.ClientID),Response.Output);%>).disabled = false;
		        document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(InputFormRequiredFieldValidatorNewPassword.ClientID),Response.Output);%>).enabled = true;
                document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(chkSendEmail.ClientID),Response.Output);%>).disabled = false;
	        }
        }

        function InitResetPassword() {
            if (document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(resetAutoPassword.ClientID),Response.Output);%>).checked) {
                ResetAutoPasswordOnClick();
            }
            else {
                ResetSelectPasswordOnClick();
            }
        }

        _spBodyOnLoadFunctionNames.push("InitResetPassword");

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-descriptiontext">
        <wssuc:InputFormSection runat="server" Title="Reset Password">
            <Template_Description>
				    <SharePoint:EncodedLiteral ID="EncodedLiteral3" runat="server" text="Reset Password Desc" EncodeMethod='HtmlEncode'/>
	       </Template_Description>
            <Template_InputFormControls>
            <asp:Label ID="resetPasswordMsg" runat="server" Text=""></asp:Label>
			<SharePoint:InputFormRadioButton id="resetAutoPassword"
				GroupName="resetPassword"
				runat="server"
				onclick="javascript:ResetAutoPasswordOnClick()"
				title="Reset Auto Password"
				LabelText="Reset Auto Password" />
			<SharePoint:InputFormRadioButton id="resetSelectPassword"
				GroupName="resetPassword"
				runat="server" 
				onclick="javascript:ResetSelectPasswordOnClick()"
				title="Reset Select Password"
				LabelText="Reset Select Password">
					<table border="0" width="100%" cellspacing="0" cellpadding="0">
						<wssuc:InputFormControl ID="InputFormControl1"  runat="server" LabelText="New Password">
							<Template_Control>
								<SharePoint:InputFormTextBox title="NewPassword" class="ms-input" ID="txtNewPassword" Columns="35" Runat="server" MaxLength="512" Direction="LeftToRight" TextMode="Password" autocomplete="off" />
                                <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidatorNewPassword" ControlToValidate="txtNewPassword" Display="Dynamic" Runat="server"/>
                                <asp:Label ID="lblNewPasswordError" runat="server" Text="" ForeColor="red" ></asp:Label>
							</Template_Control>
						</wssuc:InputFormControl>
                        <wssuc:InputFormControl ID="InputFormControl3" runat="server">
			                <Template_Control>
                                <asp:CheckBox Title="Reset Password Send Email" ID="chkSendEmail" runat="server" Text="Reset Password Send Email" />			       
			                </Template_Control>
		                </wssuc:InputFormControl>
					</table>
			</SharePoint:InputFormRadioButton>
	   </Template_InputFormControls>
        </wssuc:InputFormSection>
        <wssuc:ButtonSection runat="server">
            <template_buttons>
		<asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth" OnClick="OnResetPassword" OnClientClick="if (!confirmResetPassword()) return false;" Text="Reset Password" id="BtnResetPassword" accesskey="R"/>
		</template_buttons>
        </wssuc:ButtonSection>
    </table>
</asp:Content>