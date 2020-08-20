using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.SERVICE.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.BuildQuery
{
    public interface IQueryOrder
    {
        string Field { get; set; }

        PropertyInfo Property { get; set; }

        OrderMethod Method { get; set; }

        string Build();

        T ConvertTo<T>() where T : IQueryOrder;
        void MapTo<T>(T order) where T : IQueryOrder;
    }
}
