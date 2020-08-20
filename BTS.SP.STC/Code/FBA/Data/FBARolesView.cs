using System.Collections;
using System.Data;
using System.Web.UI;
using BTS.SP.STC.Code.FBA.Provider;
namespace BTS.SP.STC.Code.FBA.Data
{
    /// <summary>
    ///     Data source for the User Management role display view. Gets all FBA roles.
    /// </summary>
    internal class FBARolesView : DataSourceView
    {
        public FBARolesView(IDataSource owner, string viewName) : base(owner, viewName)
        {
        }

        protected override IEnumerable ExecuteSelect(DataSourceSelectArguments selectArgs)
        {
            // only continue if a membership provider has been configured
            if (!FbaRoles.IsProviderConfigured())
                return null;

            // get roles and build data table
            var dataTable = new DataTable();
            var roles = FbaRoles.BaseRoleProvider().GetAllRoles();
            dataTable.Columns.Add("Role");
            dataTable.Columns.Add("UsersInRole");

            // add users in role counts
            for (var i = 0; i < roles.Length; i++)
            {
                var row = dataTable.NewRow();
                row["Role"] = roles[i];
                row["UsersInRole"] = FbaRoles.BaseRoleProvider().GetUsersInRole(roles[i]).Length;
                dataTable.Rows.Add(row);
            }

            dataTable.AcceptChanges();
            var dataView = new DataView(dataTable);

            // sort if a sort expression available
            if (selectArgs.SortExpression != string.Empty) dataView.Sort = selectArgs.SortExpression;

            // return as a DataList
            return dataView;
        }
    }
}