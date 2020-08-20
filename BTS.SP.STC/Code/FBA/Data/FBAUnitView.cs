using System.Collections;
using System.Data;
using System.Web.UI;
using BTS.SP.STC.Code.FBA.Provider;
namespace BTS.SP.STC.Code.FBA.Data
{
    internal class FBAUnitView : DataSourceView
    {
        public FBAUnitView(IDataSource owner, string viewName) : base(owner, viewName)
        {
        }
        protected override IEnumerable ExecuteSelect(DataSourceSelectArguments selectArgs)
        {
            // only continue if a membership provider has been configured
            DataTable data = FBAUnit.GetAll();
            //var dataView = new DataView(menus);
            // sort if a sort expression available
            //if (selectArgs.SortExpression != string.Empty) dataView.Sort = selectArgs.SortExpression;
            // return as a DataList
            return data.DefaultView;
        }
    }
}
