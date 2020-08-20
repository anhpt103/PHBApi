using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.TOOLS.BuildQuery;

namespace BTS.SP.TOOLS
{
    public interface IDataSearch
    {
        string DefaultOrder { get; }
        void LoadGeneralParam(string summary);
        List<IQueryFilter> GetFilters();
        List<IQueryFilter> GetQuickFilters();
    }
}
