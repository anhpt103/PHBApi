using System;
using System.Web.Security;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web;
using BTS.SP.STC.Code.FBA.Provider;

namespace BTS.SP.STC.Layouts.FBA
{
    /// <summary>
    /// Code behind for RolesNew.aspx
    /// </summary>
    public partial class RoleNew : LayoutsPageBase
    {

        protected override bool RequireSiteAdministrator
        {
            get { return true; }
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            // add the role to the membership provider
            if (!FbaRoles.BaseRoleProvider().RoleExists(txtRole.Text))
            {
                try
                {
                    FbaRoles.BaseRoleProvider().CreateRole(txtRole.Text);
                    // redirect to roles list
                    SPUtility.Redirect("FBA/RolesDisp.aspx", SPRedirectFlags.RelativeToLayoutsPage | SPRedirectFlags.UseSource, this.Context);
                }
                catch
                {
                   
                }
            }
            else
            {
                lblMessage.Visible = true;

            }
        }
    }
}
