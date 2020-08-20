using BTS.SP.AUTHENTICATION.API.BuildQuery;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Implimentations;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Types;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Helper;
using BTS.SP.AUTHENTICATION.API.Services;
using System.Collections.Generic;

namespace BTS.SP.AUTHENTICATION.API.ServiceFunc.SysChucNang
{
    public class SysChucNangVm
    {
        public class Search : IDataSearch
        {
            public string MACHA { get; set; }
            public string MACHUCNANG { get; set; }
            public string TENCHUCNANG { get; set; }

            public string DefaultOrder
            {
                get
                {
                    return ClassHelper.GetPropertyName(() => new SYS_CHUCNANG().SOTHUTU);
                }
            }
            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new SYS_CHUCNANG();

                if (!string.IsNullOrEmpty(this.MACHA))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MACHA),
                        Value = this.MACHA,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.MACHUCNANG))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MACHUCNANG),
                        Value = this.MACHUCNANG,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TENCHUCNANG))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TENCHUCNANG),
                        Value = this.TENCHUCNANG,
                        Method = FilterMethod.Like
                    });
                }

                return result;
            }

            public List<IQueryFilter> GetQuickFilters()
            {
                return null;
            }

            public void LoadGeneralParam(string summary)
            {
                MACHA = summary;
                MACHUCNANG = summary;
                TENCHUCNANG = summary;
            }
        }
    }
}
