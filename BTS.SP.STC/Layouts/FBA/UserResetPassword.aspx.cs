using System;
using System.Web.Security;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Web;
using System.Globalization;
using BTS.SP.STC.Code.FBA.Provider;

namespace BTS.SP.STC.Layouts.FBA
{
    /// <summary>
    /// Code behind for UserDelete.aspx
    /// </summary>
    public partial class UserResetPassword : LayoutsPageBase
    {

        protected override bool RequireSiteAdministrator
        {
            get { return true; }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.CheckRights();
        
            // display error confirmation message
            string userName = Request.QueryString["USERNAME"];
            if (string.IsNullOrEmpty(userName))
            {
                SPUtility.TransferToErrorPage("UserNotFound");
                return;
            }

            if (FbaMembers.BaseMembershipProvider().RequiresQuestionAndAnswer || !FbaMembers.BaseMembershipProvider().EnablePasswordReset)
            {
                SPUtility.TransferToErrorPage("ResetPasswordUnavailable");
                return;
            }

            resetPasswordMsg.Text = string.Format("ResetPasswordMsg", userName);

            lblNewPasswordError.Text = "";

            if (!this.Page.IsPostBack)
            {
                resetAutoPassword.Checked = true;
                resetSelectPassword.Checked = false;
                chkSendEmail.Checked = true;
                
            }

        }

        protected void OnResetPassword(object sender, EventArgs e)
        {
            string username = Request.QueryString["USERNAME"];

            bool sendEmail = true;

            string newPassword = null;

            if (resetSelectPassword.Checked)
            {
                newPassword = txtNewPassword.Text;
                sendEmail = chkSendEmail.Checked;
            }

            try
            {
                FbaMembers.ResetUserPassword(username, newPassword, sendEmail, Web);
            }
            catch (ArgumentException ex)
            {
                lblNewPasswordError.Text = ex.Message;
                return;
            }
            catch 
            {
                SPUtility.TransferToErrorPage("UnexpectedError");
                return;
            }

            SPUtility.Redirect("/_layouts/FBA/UsersDisp.aspx", SPRedirectFlags.RelativeToLayoutsPage | SPRedirectFlags.UseSource, this.Context);
        }

    }
}
