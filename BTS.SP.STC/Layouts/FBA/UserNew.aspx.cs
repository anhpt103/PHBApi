using System;
using System.Web.Security;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Administration;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web;
using System.Collections.Generic;
using BTS.SP.STC.Code.FBA.Provider;
using BTS.SP.STC.Code.FBA;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace BTS.SP.STC.Layouts.FBA
{
    /// <summary>
    /// Code behind for UserNew.aspx
    /// </summary>
    public partial class UserNew : LayoutsPageBase
    {

        protected override bool RequireSiteAdministrator
        {
            get { return true; }
        }

        protected override void OnLoad(EventArgs e)
        {
            //MenusProvider _MenusProvider = new MenusProvider();
            //try { 
            //List<aspnet_Menus> _ListMenus = _MenusProvider.GetMenus("ec87d327-18e8-43a2-b1c6-0edc89d2de25");
            //    foreach (aspnet_Menus item in _ListMenus)
            //    {
            //        Response.Write("men=" + item.MenuName);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Response.Write("men=" + ex.Message);
            //}
           
            this.CheckRights();

            bool _showRoles = (new MembershipSettings(SPContext.Current.Web)).EnableRoles;

            //ReqValEmailSubject.Enabled = emailUser.Checked;

            if (!Page.IsPostBack)
            {
                try
                {
                    // if roles activated display roles
                    if (_showRoles)
                    {
                        //RolesSection.Visible = true;
                        //GroupSection.Visible = false;

                        //// load roles
                        //rolesList.DataSource = FbaRoles.BaseRoleProvider().GetAllRoles();
                        //rolesList.DataBind();
                    }
                    // otherwise display groups
                    else
                    {
                        GroupSection.Visible = true;
                        //RolesSection.Visible = false;

                        // load groups
                        var a = this.Web.SiteGroups;
                        groupList.DataSource = this.Web.SiteGroups;
                        groupList.DataBind();
                    }

                    // Display Question and answer if required by provider
                    //if (FbaMembers.BaseMembershipProvider().RequiresQuestionAndAnswer)
                    //{
                    //    QuestionSection.Visible = true;
                    //    AnswerSection.Visible = true;
                    //}
                    //else
                    //{
                    //    QuestionSection.Visible = false;
                    //    AnswerSection.Visible = false;
                    //}
                }
                catch 
                {
                   
                }
            }
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            // ModifiedBySolvion
            // bhi - 09.01.2012
            // Reset message labels
            //lblMessage.Text = lblAnswerMessage.Text = lblEmailMessage.Text = lblPasswordMessage.Text = lblQuestionMessage.Text = "";
            // EndModifiedBySolvion

            bool _showRoles = (new MembershipSettings(SPContext.Current.Web)).EnableRoles;

            // check to see if username already in use
            MembershipUser user = FbaMembers.BaseMembershipProvider().GetUser(txtUsername.Text,false);
            
            if (user == null)
            {
                try
                {
                    // get site reference             
                    string provider = FbaMembers.GetMembershipProvider(this.Site);

                    // create FBA database user
                    MembershipCreateStatus createStatus;

                    if (FbaMembers.BaseMembershipProvider().RequiresQuestionAndAnswer)
                    {
                        user = FbaMembers.BaseMembershipProvider().CreateUser(txtUsername.Text, txtPassword.Text, "", "", "", isActive.Checked, null, out createStatus);
                    }
                    else
                    {
                        user = FbaMembers.BaseMembershipProvider().CreateUser(txtUsername.Text, txtPassword.Text, "", null, null, isActive.Checked, null, out createStatus);
                    }


                    if (createStatus != MembershipCreateStatus.Success)
                    {
                        SetErrorMessage(createStatus);
                        return;
                    }

                    if (user == null)
                    {
                        lblMessage.Text = "UnknownError";
                        return;
                    }

                    using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                    {

                    }

                    bool groupAdded = false;

                    if (_showRoles)
                    {
                        //for (int i = 0; i < rolesList.Items.Count; i++)
                        //{
                        //    if (rolesList.Items[i].Selected)
                        //    {
                        //        FbaRoles.BaseRoleProvider().AddUsersToRoles(new string[] {user.UserName}, new string[] {rolesList.Items[i].Value});
                        //    }
                        //}

                        // add user to SharePoint whether a role was selected or not
                        AddUserToSite(FbaMembers.EncodeUsername(user.UserName), user.Email, txtFullName.Text);
                    }
                    else
                    {
                        // add user to each group that was selected
                        for (int i = 0; i < groupList.Items.Count; i++)
                        {
                            if (groupList.Items[i].Selected)
                            {
                                // add user to group
                                SPGroup group = this.Web.SiteGroups[groupList.Items[i].Value];
                                group.AddUser(
                                    FbaMembers.EncodeUsername(user.UserName),
                                    user.Email,
                                    txtFullName.Text,
                                    "");

                                // update
                                group.Update();
                                groupAdded = true;
                            }
                        }

                        // if no group selected, add to site with no permissions
                        if (!groupAdded)
                        {
                            AddUserToSite(FbaMembers.EncodeUsername(user.UserName), user.Email, txtFullName.Text);
                        }
                    }

                    // Email User
                    //if ((emailUser.Checked == true))
                    //{
                    //    //InputFormTextBox txtEmailSubject = (InputFormTextBox)emailUser.FindControl("txtEmailSubject");
                    //    //InputFormTextBox txtEmailBody = (InputFormTextBox)emailUser.FindControl("txtEmailBody");
                    //    //if ((!string.IsNullOrEmpty(txtEmailSubject.Text)) && (!string.IsNullOrEmpty(txtEmailBody.Text)))
                    //    //    Email.SendEmail(this.Web, user.Email, txtEmailSubject.Text, txtEmailBody.Text);
                    //}
                    SPUtility.Redirect("FBA/UsersDisp.aspx", SPRedirectFlags.RelativeToLayoutsPage | SPRedirectFlags.UseSource, this.Context);


                }
                catch
                {
                    
                }
            }
            else
            {
                lblMessage.Text = "DuplicateUserName";
            }
        }

        protected void SetErrorMessage(MembershipCreateStatus status)
        {
             switch (status)
             {
                 case MembershipCreateStatus.DuplicateUserName:
                    lblMessage.Text = "DuplicateUserName";
                    break;

                case MembershipCreateStatus.DuplicateEmail:
                    //lblEmailMessage.Text ="DuplicateEmail";
                    break;

                case MembershipCreateStatus.InvalidPassword:
                    string message = "";
                    if (string.IsNullOrEmpty(FbaMembers.BaseMembershipProvider().PasswordStrengthRegularExpression))
                    {
                        message = string.Format("InvalidPasswordChars", FbaMembers.BaseMembershipProvider().MinRequiredPasswordLength, FbaMembers.BaseMembershipProvider().MinRequiredNonAlphanumericCharacters);
                    }
                    else
                    {
                        message = string.Format("InvalidPasswordCharsRegex", FbaMembers.BaseMembershipProvider().MinRequiredPasswordLength, FbaMembers.BaseMembershipProvider().MinRequiredNonAlphanumericCharacters, FbaMembers.BaseMembershipProvider().PasswordStrengthRegularExpression);
                    }
                    //LocalizedString.GetGlobalString("FBAPackWebPages", "InvalidPassword")
                    // TODO: use resource files
                    lblPasswordMessage.Text = message;
                    break;

                case MembershipCreateStatus.InvalidEmail:
                    //lblEmailMessage.Text = "InvalidEmail";
                    break;

                case MembershipCreateStatus.InvalidAnswer:
                    //lblAnswerMessage.Text = "InvalidAnswer";
                    break;

                case MembershipCreateStatus.InvalidQuestion:
                    //lblQuestionMessage.Text = "InvalidQuestion";
                    break;

                case MembershipCreateStatus.InvalidUserName:
                    lblMessage.Text = "InvalidUserName";
                    break;

                case MembershipCreateStatus.ProviderError:
                    lblMessage.Text = "ProviderError";
                    break;

                case MembershipCreateStatus.UserRejected:
                    lblMessage.Text = "UserRejected";
                    break;

                default:
                    lblMessage.Text = "UnknownError";
                    break;
            }
        }

        /// <summary>
        /// Adds a user to the SharePoint (in no particular group)
        /// </summary>
        /// <param name="login"></param>
        /// <param name="email"></param>
        /// <param name="fullname"></param>
        private void AddUserToSite(string login, string email, string fullname)
        {
            this.Web.AllUsers.Add(
                login,
                email,
                fullname,
                "");
        }
    }
}
