using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.TOOLS.BuildQuery.Result
{
    public class FilterObj : FilterObj<object>
    {
    }

    public class FilterObj<T>
    {
        public string Summary { get; set; }
        public bool IsAdvance { get; set; }
        public string OrderBy { get; set; }
        public string OrderType { get; set; }
        public T AdvanceData { get; set; }
    }
}
