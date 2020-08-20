using System.Collections;
using System.Data;
using System.Web.UI;
using BTS.SP.STC.Code.FBA.Provider;
namespace BTS.SP.STC.Code.FBA.Data
{
    internal class FBAMenuView : DataSourceView
    {
        public FBAMenuView(IDataSource owner, string viewName) : base(owner, viewName)
        {
        }
        protected override IEnumerable ExecuteSelect(DataSourceSelectArguments selectArgs)
        {
            // only continue if a membership provider has been configured
            DataTable menus = FbaMenus.GetAll();
            //var dataView = new DataView(menus);
            // sort if a sort expression available
            //if (selectArgs.SortExpression != string.Empty) dataView.Sort = selectArgs.SortExpression;
            // return as a DataList
            return menus.DefaultView;
        }
    }
}
