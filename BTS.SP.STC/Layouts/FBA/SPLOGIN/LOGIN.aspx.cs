using System;
using System.Diagnostics;
using System.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.IdentityModel;
using Microsoft.SharePoint.WebControls;
using System.Web.Security;
using BTS.SP.STC.Code.FBA.Provider;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Web.Services;
using BTS.SP.STC.Code.FBA;

namespace BTS.SP.STC.Layouts.FBA.SPLOGIN
{
    public partial class LOGIN : System.Web.UI.Page
    {
        private string _userName
        {
            get { return ViewState["vs_username"].ToString(); }
            set { ViewState["vs_username"] = value; }
        }

        private string _password
        {
            get { return ViewState["vs_password"].ToString(); }
            set { ViewState["vs_password"] = value; }
        }

        private bool _remember
        {
            get { return Convert.ToBoolean(ViewState["vs_remember"].ToString()); }
            set { ViewState["vs_remember"] = value; }
        }

        [WebMethod(EnableSession = true)]
        private void SignInUser()
        {
            //var Member = (MembershipProvider)FbaMembers.BaseMembershipProvider(); //Member.Name = "FBAMembershipProvider";
            //var role = (RoleProvider)FbaRoles.BaseRoleProvider();
            //if (!Member.ValidateUser(_userName, _password)) return;
            //SecurityToken token1 = SPSecurityContext.SecurityTokenForFormsAuthentication
            //                     (new Uri(SPContext.Current.Web.Url),
            //                                Member.Name,
            //                                 role.Name,
            //                                 _userName,
            //                                 _password,
            //                                 SPFormsAuthenticationOption.SmartClient);


            SecurityToken token = SPSecurityContext.SecurityTokenForContext
                                             (new Uri(SPContext.Current.Web.Url));
            //,
            //                                 "FBAMembershipProvider",
            //                                 "admin",
            //                                 "admin",
            //                                 "admin",
            //                                 SPFormsAuthenticationOption.SmartClient);

            //SPFederationAuthenticationModule.Current.SetPrincipalAndWriteSessionToken(token1);
            SPFederationAuthenticationModule.Current.SetPrincipalAndWriteSessionToken(token);

            Response.Redirect(Request.QueryString["Source"]);

            //call API get Token
            //string token_Api = APIFunction.GetToken(_userName, _password);
            //if (!string.IsNullOrEmpty(token_Api))
            //{
            //
            //    //HttpCookie myCookie = new HttpCookie("ss.authorizationData");
            //    DateTime now = DateTime.Now;
            //    //myCookie.Value = token_Api;
            //    //myCookie.Expires = now.AddHours(9999); // For a cookie to effectively never expire
            //    //// Add the cookie.
            //    //Response.Cookies.Add(myCookie);
            //
            //    HttpCookie myCookie2 = new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["url_Api"].ToString()+".authorizationData");
            //    myCookie2.Value = token_Api;
            //    myCookie2.Expires = now.AddHours(9999); // For a cookie to effectively never expire
            //    // Add the cookie.
            //    Response.Cookies.Add(myCookie2);
            //
            //}
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            _userName = UserName.Text.Trim();
            _password = Password.Text;
            _remember = RememberMe.Checked;

            SignInUser();

            //if (string.IsNullOrEmpty(_userName))
            //{
            //    FailureTextLogin.Text = "Nhập Tên Đăng Nhập";
            //    return;
            //}

            //if (string.IsNullOrEmpty(_password))
            //{
            //    FailureTextLogin.Text = "Nhập Mật Khẩu";
            //    return;
            //}

            //var mem = (MembershipProvider)FbaMembers.BaseMembershipProvider();
            //var temp = !mem.ValidateUser(_userName, _password);
            //if (!mem.ValidateUser(_userName, _password))
            //{
            //    var currentMemberShip = mem.GetUser(_userName, false);
            //    if (currentMemberShip.IsLockedOut)
            //    {
            //        FailureTextLogin.Text = "Tài khoản của bạn đã bị khóa do nhập sai mật khẩu";
            //        return;

            //    }
            //    else
            //    {
            //        FailureTextLogin.Text = "Sai Tên Đăng Nhập Hoặc Mât Khẩu";
            //        return;

            //    }
            //}

            //var user = (MembershipUser)mem.GetUser(_userName, false);
            //if (user != null)
            //{
            //    //if (user.IsPasswordChanged)
            //    //{
            //    //    phPart1.Visible = false;
            //    //    phPart2.Visible = true;
            //    //}
            //    //else
            //    //{
            //    SignInUser();
            //    //}
            //}

        }
        //protected void ChangePasswordBtn_Click(object sender, EventArgs e)
        //{

        //    var mem = (MembershipProvider)Utils.BaseMembershipProvider();
        //    if (!mem.ValidateUser(_userName, CurrentPassword.Text))
        //    {
        //        FailureTextPasswordChange.Text = "Unable to change your password. Please make sure your current password is correct, and then try again.";
        //        return;
        //    }

        //    if (NewPassword.Text.Length < mem.MinRequiredPasswordLength)
        //    {
        //        FailureTextPasswordChange.Text =
        //            String.Format("Your new password is too short. Minimum length {0} characters.",
        //                          mem.MinRequiredPasswordLength);
        //        return;
        //    }

        //    int num = 0;
        //    for (int i = 0; i < NewPassword.Text.Length; i++)
        //    {
        //        if (!char.IsLetterOrDigit(NewPassword.Text, i))
        //        {
        //            num++;
        //        }
        //    }

        //    if (num < mem.MinRequiredNonAlphanumericCharacters)
        //    {
        //        FailureTextPasswordChange.Text =
        //            String.Format("Your password requires {0} or more non-alphanumeric characters.", mem.MinRequiredNonAlphanumericCharacters);
        //        return;
        //    }

        //    Match match = Regex.Match(NewPassword.Text, mem.PasswordStrengthRegularExpression);
        //    if (!match.Success)
        //    {
        //        FailureTextPasswordChange.Text =
        //            String.Format("Password must be at least {0} characters, containing upper, lowercase and numeric.",
        //                          mem.MinRequiredPasswordLength);
        //        return;
        //    }

        //    var user = (MembershipUser)mem.GetUser(_userName, false);
        //    if (user != null)
        //    {
        //        try
        //        {
        //            bool result = user.ChangePassword(CurrentPassword.Text, NewPassword.Text);
        //            if (!result)
        //            {
        //                FailureTextPasswordChange.Text = "Unable to change your password.";
        //                return;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            FailureTextPasswordChange.Text = ex.Message;
        //            return;
        //        }

        //        FailureTextLogin.Text = "Successfully changed your password. Please login again with your new password.";
        //        phPart1.Visible = true;
        //        phPart2.Visible = false;
        //    }
        //}
        //protected void Cancel_Click(object sender, EventArgs e)
        //{
        //    //Clear everything. Reset login page.
        //    _userName = "";
        //    _password = "";
        //    _remember = false;
        //    phPart1.Visible = true;
        //    phPart2.Visible = false;
        //    FailureTextLogin.Text = "Option to change password was cancelled. Unable to log you in unless password is changed.";
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            var Member = (MembershipProvider)FbaMembers.BaseMembershipProvider(); //Member.Name = "FBAMembershipProvider";
            var role = (RoleProvider)FbaRoles.BaseRoleProvider();
            //if (!Member.ValidateUser(_userName, _password)) return;
            SecurityToken token = SPSecurityContext.SecurityTokenForFormsAuthentication
                                 (new Uri(SPContext.Current.Web.Url),
                                            Member.Name,
                                             role.Name,
                                             "admin",
                                             "Admin@123",
                                             SPFormsAuthenticationOption.SmartClient);
            SPFederationAuthenticationModule.Current.SetPrincipalAndWriteSessionToken(token);

            Response.Redirect(Request.QueryString["Source"]);


        }


    }
}
