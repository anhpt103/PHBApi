using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.HTDM.DmTSCD
{
    public class DmTSCDVm
    {
        public class ViewModel
        {
            public string MA_TSCD { get; set; }
            public string TEN_TSCD { get; set; }
            public string MO_TA { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA_TSCD { get; set; }
            public string TEN_TSCD { get; set; }
            public string MO_TA { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_TSCD = summary;
                TEN_TSCD = summary;
                MO_TA = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_TSCD();

                if (!string.IsNullOrEmpty(this.MA_TSCD))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_TSCD),
                        Value = this.MA_TSCD,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_TSCD))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_TSCD),
                        Value = this.TEN_TSCD,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.MO_TA))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MO_TA),
                        Value = this.MO_TA,
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
                get { return ClassHelper.GetPropertyName(() => new PHB_DM_TSCD().MA_TSCD); }
            }
        }
    }
}
