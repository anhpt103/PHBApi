using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Text;
using System.Web;
using BTS.SP.STC.Code.FBA;
using Microsoft.SharePoint.Utilities;
using BTS.SP.STC.Code.FBA.Provider;
namespace BTS.SP.STC.Layouts.FBA
{
    /// <summary>
    /// Code behind for RolesDisp.aspx
    /// </summary>
    public partial class RolesDisp : LayoutsPageBase
    {

        protected override void OnInit(System.EventArgs e)
        {
            // display error if membership provider not configured
            if (!FbaSites.IsProviderConfigured())
            {
                lblMessage.Text = "MembershipNotConfigured";
                RoleGrid.Visible = false;
                ToolBarPlaceHolder.Visible = false;
                onetidNavNodesTB.Visible = false;
            }

            // ModifiedBySolvion
            // bhi - 19.12.2011
            // Show status when roles a not enabled
            else
            {
                MembershipSettings settings = new MembershipSettings(SPContext.Current.Web);
                if (!settings.EnableRoles)
                {
                    string startupScriptName = "RolesNotEnabledInfo";
                    if (!Page.ClientScript.IsStartupScriptRegistered(startupScriptName))
                    {
                        StringBuilder script = new StringBuilder();
                        script.AppendLine("ExecuteOrDelayUntilScriptLoaded(showRoleStatus, 'SP.js')");
                        script.AppendLine("function showRoleStatus() {");
                        script.AppendLine("var roleStatusID = SP.UI.Status.addStatus('Information : ', 'Roles are not enabled. You can enable roles in the <a href=\"/_layouts/BTS19.QLG.APP/FBA/Management/FBASiteConfiguration.aspx\">FBA Site Configuration</a>.', true);");
                        script.AppendLine("SP.UI.Status.setStatusPriColor(roleStatusID, \"yellow\");");
                        script.AppendLine("}");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), startupScriptName, script.ToString(), true);
                    }
                }
            }
            // EndModifiedBySolvion

            base.OnInit(e);
        }

        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);
            string source = SPHttpUtility.UrlKeyValueEncode(SPUtility.OriginalServerRelativeRequestUrl);
            (idNewNavNode as ToolBarButton).NavigateUrl = "UserNew.aspx?Source=" + source;
        }

        protected override bool RequireSiteAdministrator
        {
            get { return true; }
        }
    }
}
