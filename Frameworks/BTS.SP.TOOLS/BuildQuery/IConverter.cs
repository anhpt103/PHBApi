using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.TOOLS.BuildQuery
{
    public interface IConverter
    {
        string MapTo(dynamic value);
    }
}
