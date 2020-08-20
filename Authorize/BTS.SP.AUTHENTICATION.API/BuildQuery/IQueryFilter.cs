﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Types;

namespace BTS.SP.AUTHENTICATION.API.BuildQuery
{
    public interface IQueryFilter
    {
        dynamic PropertyAndValue { set; }

        string Field { get; set; }

        PropertyInfo Property { get; set; }

        bool ValueAsOtherField { get; set; }

        dynamic Value { get; set; }

        FilterMethod Method { get; set; }

        bool IsAlwaysCheckNotNull { get; set; }

        IQueryFilterParam QueryStringParams { get; set; }

        List<IQueryFilter> SubFilters { get; set; }

        bool IsCompleteQuery { get; }

        bool MergeFilter(IQueryFilter filter);

        string Build();

        T ConvertTo<T>(bool keepSub = false) where T : IQueryFilter;
        void MapTo<T>(T filter, bool keepSub = false) where T : IQueryFilter;
    }
}