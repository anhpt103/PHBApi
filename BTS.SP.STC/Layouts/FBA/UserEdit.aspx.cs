using System;
using System.Web.Security;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web.UI.WebControls;
using System.Web;
using BTS.SP.STC.Code.FBA.Provider;
using BTS.SP.STC.Code.FBA;
namespace BTS.SP.STC.Layouts.FBA
{
    /// <summary>
    /// Code behind for UserEdit.aspx
    /// </summary>
    public partial class UserEdit : LayoutsPageBase
    {
        private bool _showRoles;

        protected override bool RequireSiteAdministrator
        {
            get { return true; }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.CheckRights();

            // init
            _showRoles = (new MembershipSettings(SPContext.Current.Web)).EnableRoles;

            // get user info
            string userName = this.Request.QueryString["USERNAME"];
            SPUser spuser = null;
            try
            {
                spuser = this.Web.AllUsers[FbaMembers.EncodeUsername(userName)];
            }
            catch
            {
                
            }
            MembershipUser user = FbaMembers.BaseMembershipProvider().GetUser(userName,false);

            if (user != null)
            {
                if (!Page.IsPostBack)
                {
                    // load user props
                    if (spuser != null)
                    {
                        txtEmail.Text = spuser.Email;
                        txtFullName.Text = spuser.Name;
                    }
                    else
                    {
                        txtEmail.Text = user.Email;
                        txtFullName.Text = user.UserName;
                    }
                    txtUsername.Text = user.UserName;
                    isActive.Checked = user.IsApproved;
                    isLocked.Checked = user.IsLockedOut;
                    isLocked.Enabled = user.IsLockedOut;

                    // if roles activated display roles
                    if (_showRoles)
                    {
                        RolesSection.Visible = true;
                        GroupSection.Visible = false;

                        try
                        {
                            // load roles
                            string[] roles = FbaRoles.BaseRoleProvider().GetAllRoles();
                            rolesList.DataSource = roles;
                            rolesList.DataBind();

                            // select roles associated with the user
                            for (int i = 0; i < roles.Length; i++)
                            {
                                ListItem item = rolesList.Items.FindByText(roles[i].ToString());
                                if (item != null) item.Selected = FbaRoles.BaseRoleProvider().IsUserInRole(user.UserName, roles[i].ToString());
                            }
                        }
                        catch
                        {
                        }
                    }
                    // otherwise display groups
                    else 
                    {
                        GroupSection.Visible = true;
                        RolesSection.Visible = false;

                        try
                        {
                            // load groups
                            groupList.DataSource = this.Web.SiteGroups;
                            groupList.DataBind();

                            if (spuser != null)
                            {
                                // select groups associated with the user
                                foreach (SPGroup group in spuser.Groups)
                                {
                                    ListItem item = groupList.Items.FindByText(group.Name);
                                    if (item != null) item.Selected = true;
                                }
                            }
                        }
                        catch 
                        {
                           
                        }
                    }
                }
            }
            else
            {
                SPUtility.TransferToErrorPage("UserNotFound");
            }
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            // get user info
            string userName = this.Request.QueryString["USERNAME"];
            SPUser spuser = null;
            // This could be done with EnsureUsers, which won't throw an exception if the user hasn't logged on to the site.
            try
            {
                spuser = this.Web.AllUsers[FbaMembers.EncodeUsername(userName)];
            }
            catch
            {

            }
            MembershipUser user = FbaMembers.BaseMembershipProvider().GetUser(userName,false);
            
            // check user exists
            if (user != null)
            {
                try
                {
                    // TODO: If we want the Email to be used for the user account, we need to delete the user and create a new one with the new email address.
                    // This will mean we need to iterate over the groups that the user is a member of, in all site collections in all web apps, and add the new user
                    // to those groups.  In the meantime, we allow the email to be changed, but this won't update the account username.

                    // update membership provider info
                    user.Email = txtEmail.Text;
                    user.IsApproved = isActive.Checked;

                    //Unlock Account
                    if (user.IsLockedOut && !isLocked.Checked)
                    {
                        user.UnlockUser();
                    }
                    try
                    {
                        FbaMembers.BaseMembershipProvider().UpdateUser(user);
                    }
                    catch (System.Configuration.Provider.ProviderException ex)
                    {
                        lblMessage.Text = ex.Message;
                        return;
                    }

                    // if roles enabled add/remove user to selected role(s)
                    if (_showRoles)
                    {
                        for (int i = 0; i < rolesList.Items.Count; i++)
                        {
                            if (rolesList.Items[i].Selected)
                            {
                                if (!FbaRoles.BaseRoleProvider().IsUserInRole(user.UserName, rolesList.Items[i].Value))
                                    FbaRoles.BaseRoleProvider().AddUsersToRoles(new string[] {user.UserName}, new string[] {rolesList.Items[i].Value});
                            }
                            else
                            {
                                if (FbaRoles.BaseRoleProvider().IsUserInRole(user.UserName, rolesList.Items[i].Value))
                                    FbaRoles.BaseRoleProvider().RemoveUsersFromRoles(new string[] {user.UserName}, new string[] {rolesList.Items[i].Value});
                            }
                        }
                    }
                    // or add/remove user to selected group(s)
                    else
                    {
                        for (int i = 0; i < groupList.Items.Count; i++)
                        {
                            string groupName = groupList.Items[i].Value;

                            // determine whether user is in group
                            bool userInGroup = false;

                            if (spuser != null)
                            {
                                foreach (SPGroup group in spuser.Groups)
                                {
                                    if (group.Name == groupName)
                                    {
                                        userInGroup = true;
                                        break;
                                    }
                                }
                            }

                            // if selected add user to group
                            if (groupList.Items[i].Selected)
                            {
                                // only add if not already in group
                                if (!userInGroup)
                                {
                                    //Add the user to SharePoint if they're not already a SharePoint user
                                    if (spuser == null)
                                    {
                                        try
                                        {
                                            spuser = this.Web.EnsureUser(FbaMembers.EncodeUsername(userName));
                                        }
                                        catch 
                                        {
                                            lblMessage.Text = "ErrorAddingToSharePoint";
                                            return;
                                        }
                                    }
                                    this.Web.SiteGroups[groupName].AddUser(spuser);
                                }
                            }
                            // else remove user from group
                            else
                            {
                                // only attempt remove if actually in the group
                                if (userInGroup)
                                    this.Web.SiteGroups[groupName].RemoveUser(spuser);
                            }
                        }
                    }

                    // update sharepoint user info
                    if (spuser != null)
                    {
                        spuser.Email = txtEmail.Text;
                        spuser.Name = txtFullName.Text;
                        spuser.Update();
                    }
                    SPUtility.Redirect("FBA/UsersDisp.aspx", SPRedirectFlags.RelativeToLayoutsPage | SPRedirectFlags.UseSource, this.Context);
                    
                }
                catch
                {
                }
            }
            else
            {
                SPUtility.TransferToErrorPage("UserNotFound");
            }
        }

        protected void OnResetPassword(object sender, EventArgs e)
        {
            SPUtility.Redirect(string.Format("~/_layouts/FBA/UserResetPassword.aspx?UserName={0}&Source={1}", this.Request.QueryString["USERNAME"], SPHttpUtility.UrlKeyValueEncode(SPUtility.OriginalServerRelativeRequestUrl)), SPRedirectFlags.RelativeToLayoutsPage, this.Context);
        }

        protected void OnDeleteUser(object sender, EventArgs e)
        {
            SPUtility.Redirect(string.Format("FBA/UserDelete.aspx?UserName={0}&Source={1}", this.Request.QueryString["USERNAME"], SPHttpUtility.UrlKeyValueEncode(SPUtility.OriginalServerRelativeRequestUrl)), SPRedirectFlags.RelativeToLayoutsPage, this.Context);
        }
    }
}
