using System;
using System.Data;
using BTS.SP.STC.Code.FBA.Provider;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace BTS.SP.STC.Layouts.FBA
{
    public partial class UnitNewEdit : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string paraUnitId = this.Request.QueryString["UnitId"];
            if (paraUnitId != "0" && !Page.IsPostBack)
            {
                DataTable dataTable = FBAUnit.GetOne(paraUnitId);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow r = dataTable.Rows[0];
                    txtUnitId.Text = r["UnitId"].ToString();
                    txtUnitId.ReadOnly = true;
                    txtUnitIdParent.Text = r["UnitIdParent"].ToString();
                    txtUnitName.Text = r["UnitName"].ToString();
                    txtPhone.Text = r["Phone"].ToString();
                    txtEmail.Text = r["Email"].ToString();
                    txtAddress.Text = r["Address"].ToString();
                    txtDescription.Text = r["Description"].ToString();
                    
                }
            }
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            string paraUnitId = this.Request.QueryString["UnitId"];
            if (paraUnitId != "0")
            {
                FBAUnit.UpdateOne(txtUnitId.Text.Trim(), txtUnitIdParent.Text.Trim(), txtUnitName.Text.Trim(), txtPhone.Text.Trim(), txtEmail.Text.Trim(), txtAddress.Text.Trim(), txtDescription.Text.Trim());
            }
            else
            {
                FBAUnit.CreateOne(txtUnitId.Text.Trim(), txtUnitIdParent.Text.Trim(), txtUnitName.Text.Trim(), txtPhone.Text.Trim(), txtEmail.Text.Trim(), txtAddress.Text.Trim(), txtDescription.Text.Trim());
            }
            SPUtility.Redirect("FBA/UnitDisp.aspx", SPRedirectFlags.RelativeToLayoutsPage | SPRedirectFlags.UseSource, this.Context);
        }

        protected void OnDelete(object sender, EventArgs e)
        {
            string paraUnitId = this.Request.QueryString["UnitId"];
            FBAUnit.DeleteOne(txtUnitId.Text.Trim());
            SPUtility.Redirect("FBA/UnitDisp.aspx", SPRedirectFlags.RelativeToLayoutsPage | SPRedirectFlags.UseSource, this.Context);
        }
    }
}
