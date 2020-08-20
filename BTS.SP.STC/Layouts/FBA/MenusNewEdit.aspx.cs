using System;
using System.Configuration;
using System.Data;
using BTS.SP.STC.Code.FBA.Provider;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
namespace BTS.SP.STC.Layouts.FBA
{
    public partial class MenusNewEdit : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string paraMenuId = this.Request.QueryString["MenuId"];
            if (paraMenuId != "0" && !Page.IsPostBack)
            {
                DataTable dataTable = FbaMenus.GetOne(paraMenuId);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow r = dataTable.Rows[0];
                    txtMenuId.Text = r["MenuId"].ToString();
                    txtMenuId.ReadOnly = true;
                    txtMenuIdParent.Text = r["MenuIdParent"].ToString();
                    txtMenuName.Text = r["MenuName"].ToString();
                    txtPath.Text = r["Path"].ToString();
                    txtDescription.Text = r["Description"].ToString();
                    if (r["Status"].ToString() == "1") isStatus.Checked = true;
                }
            }
        }
        protected void OnSubmit(object sender, EventArgs e)
        {
            string paraMenuId = this.Request.QueryString["MenuId"];
            string Statuspara = "0";
            if (isStatus.Checked) Statuspara = "0";
            if (paraMenuId != "0")
            {
                FbaMenus.UpdateOne(txtMenuId.Text.Trim(), txtMenuIdParent.Text.Trim(), txtMenuName.Text.Trim(),txtPath.Text.Trim(), txtDescription.Text.Trim(), Statuspara);
            }
            else
            {
                FbaMenus.CreateOne(txtMenuId.Text.Trim(), txtMenuIdParent.Text.Trim(), txtMenuName.Text.Trim(), txtPath.Text.Trim(), txtDescription.Text.Trim(), Statuspara);
            }
            SPUtility.Redirect("FBA/MenusDisp.aspx", SPRedirectFlags.RelativeToLayoutsPage | SPRedirectFlags.UseSource, this.Context);
        }

        protected void OnDelete(object sender, EventArgs e)
        {
            string paraMenuId = this.Request.QueryString["MenuId"];
            FbaMenus.DeleteOne(txtMenuId.Text.Trim());
            SPUtility.Redirect("FBA/MenusDisp.aspx", SPRedirectFlags.RelativeToLayoutsPage | SPRedirectFlags.UseSource, this.Context);
        }
    }
}
