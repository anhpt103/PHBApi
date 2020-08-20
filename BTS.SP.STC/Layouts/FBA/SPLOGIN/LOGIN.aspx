<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LOGIN.aspx.cs" Inherits="BTS.SP.STC.Layouts.FBA.SPLOGIN.LOGIN" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
    <head>
          <!-- Custom styles for this template -->
    <link href="<%= SPContext.Current.Web.Url %>/_layouts/15/BTS.SP.STC.PHA/utils/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/_layouts/15/FBA/SPLOGIN/DesignFiles/signin.css" rel="stylesheet" />
     </head>
    <body>
        <div class="row">               
            <div class="account-wall">
                <h1 class="text-center login-title"><b><font style="color: #e61818;font-size: 25px;">KHO DỮ LIỆU TÀI CHÍNH ĐỊA PHƯƠNG</font></b></h1>             
                <form runat="server" style="background-color: rgba(255, 255, 255, 0.6);" class="form-signin">
                <img class="profile-img" src="/_layouts/15/FBA/SPLOGIN/DesignFiles/QuocHuyLogo.png" alt="" >
             <%--<h1 class="h3 mb-3 font-weight-normal" style="padding-left: 70px;">Phần mềm tài chính</h1>--%>               
                    <asp:TextBox ID="UserName" autocomplete="off" runat="server" class="form-control" placeholder="Tên đăng nhập" ng-model="username" ></asp:TextBox>    
                   <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="Tên đăng nhập không được để trống!" ToolTip="User Name is required." ValidationGroup="customLoginMain"></asp:RequiredFieldValidator>

                <asp:TextBox id="Password" TextMode="Password" autocomplete="off" runat="server" class="form-control" placeholder="Mật khẩu" ng-model="password" />
                 <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Chưa nhập mật khẩu!" ToolTip="Password is required." ValidationGroup="customLoginMain"></asp:RequiredFieldValidator>

            <div style="text-align: center; color: red;">
                <asp:Literal ID="FailureTextLogin" runat="server" EnableViewState="False"></asp:Literal>
            </div>
          
           <div style="padding-left: 175px;">
                 <asp:CheckBox id="RememberMe" Text="Nhớ đăng nhập" runat="server" autopostback="false" />
            </div>   
           
            
                <p ng-click="start(username,password)">
                    <asp:Button ID="LoginButton" class="btn btn-lg btn-primary btn-block" type="submit" runat="server" Text="Đăng nhập" OnClick="btnSubmit_OnClick" ValidationGroup="SignInContent" style="background-color: brown;"/>
                </p>
           
            
            <br/>
                 <div style="padding-left: 140;""><a ng-click="start('SystemAccount',password)" href="/_windows/default.aspx?ReturnUrl=%2f_layouts%2f15%2fAuthenticate.aspx%3fSource%3d%252F&Source=%2F">Dành cho nhà phát triển</a></div>
           <%-- <p class="mt-5 mb-3 text-muted">B&T High Tech Solution © 2019</p>--%>
           
       </form>
            </div> 
            
        </div>        
    </body>
</html>
