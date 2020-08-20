using System;
using System.Collections;
using System.Web.UI;

namespace BTS.SP.STC.Code.FBA.Data
{
    /// <summary>
    ///     Provides data sources for User and Role display pages
    /// </summary>
    public class FBADataSource : DataSourceControl
    {
        private DataSourceView _view;

        public string ViewName { get; set; }

        public string SearchText
        {
            get
            {
                var s = (string) ViewState["SearchText"];
                return s != null ? s : string.Empty;
            }

            set { ViewState["SearchText"] = value; }
        }

        public bool ResetCache { get; set; }

        /// <summary>
        ///     return a strongly typed view for the current data source control
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        protected override DataSourceView GetView(string viewName)
        {
            // only retrieve a view if a membership provider can be found
            if (_view == null)
                try
                {
                    if (ViewName == "FBAUsersView")
                        _view = new FBAUsersView(this, viewName);
                    else if (ViewName == "FBARolesView")
                        _view = new FBARolesView(this, viewName);
                    else if (ViewName == "FBAMenuView")
                        _view = new FBAMenuView(this, viewName);
                    else if (ViewName == "FBAUnitView")
                        _view = new FBAUnitView(this, viewName);
                }
                catch (Exception ex)
                {
                   // Utils.LogError(ex, true);
                }

            return _view;
        }

        /// <summary>
        ///     return a collection of available views
        /// </summary>
        /// <returns></returns>
        protected override ICollection GetViewNames()
        {
            var views = new ArrayList(4);
            views.Add("FBAUsersView");
            views.Add("FBARolesView");
            views.Add("FBAMenuView");
            views.Add("FBAUnitView");
            return views;
        }
    }
}