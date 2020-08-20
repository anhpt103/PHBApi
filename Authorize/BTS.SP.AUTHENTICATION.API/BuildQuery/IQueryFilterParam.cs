using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.AUTHENTICATION.API.BuildQuery
{
    public interface IQueryFilterParam
    {
        int Count { get; set; }
        List<object> Params { get; set; }

        string GetNextParam(object param = null);
    }
}
