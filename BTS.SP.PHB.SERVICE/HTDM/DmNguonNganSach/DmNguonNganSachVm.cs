using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.HTDM.DmNguonNganSach
{
    public class DmNguonNganSachVm
    {
        public class ViewModel
        {
            public string MA_NGUONNS { get; set; }
            public string TEN_NGUONNS { get; set; }
            public string MA_CHA { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA_NGUONNS { get; set; }
            public string TEN_NGUONNS { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_NGUONNS = summary;
                TEN_NGUONNS = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_NGUONNGANSACH();

                if (!string.IsNullOrEmpty(this.MA_NGUONNS))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_NGUONNS),
                        Value = this.MA_NGUONNS,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_NGUONNS))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_NGUONNS),
                        Value = this.TEN_NGUONNS,
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
                get { return ClassHelper.GetPropertyName(() => new PHB_DM_NGUONNGANSACH().MA_NGUONNS); }
            }
        }
    }
}
