using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Auth;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.HTDM.DmCtMucTieu
{
    public class DmCtMucTieuVm
    {
        public class ViewModel
        {
            public string MA { get; set; }
            public string TEN { get; set; }
            public string TEN_RUTGON { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA { get; set; }
            public string TEN { get; set; }
            public string TEN_RUTGON { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA = summary;
                TEN = summary;
                TEN_RUTGON = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new DM_CTMUCTIEU();

                if (!string.IsNullOrEmpty(this.MA))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA),
                        Value = this.MA,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN),
                        Value = this.TEN,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_RUTGON))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_RUTGON),
                        Value = this.TEN_RUTGON,
                        Method = FilterMethod.Like
                    });
                }
                return result;
            }

            public List<IQueryFilter> GetQuickFilters()
            {
                return null;
            }

            public string DefaultOrder
            {
                get { return ClassHelper.GetPropertyName(() => new DM_CTMUCTIEU().MA); }
            }
        }
    }
}
