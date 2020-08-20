using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  BTS.SP.AUTHENTICATION.API.BuildQuery;

namespace  BTS.SP.AUTHENTICATION.API.Services
{
    public interface IDataSearch
    {
        string DefaultOrder { get; }
        void LoadGeneralParam(string summary);
        //void LoadGeneralParamReport(string summary,int loai_bc);
        List<IQueryFilter> GetFilters();

        List<IQueryFilter> GetQuickFilters();
    }

    public interface IDataSearchReport
    {
        string DefaultOrder { get; }
        void LoadGeneralParam(string summary, int loai_bc);
        List<IQueryFilter> GetFilters();

        List<IQueryFilter> GetQuickFilters();
    }
    
}
