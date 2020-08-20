using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.HTDM.DmLoaiNganSach
{
    public class DmLoaiNganSachVm
    {
        public class ViewModel
        {
            public string MA_LOAINS { get; set; }
            public string TEN_LOAINS { get; set; }
            public string MO_TA { get; set; }
            public string TEN_MORONG { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA_LOAINS { get; set; }
            public string TEN_LOAINS { get; set; }
            public string MO_TA { get; set; }
            public string TEN_MORONG { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_LOAINS = summary;
                TEN_LOAINS = summary;
                MO_TA = summary;
                TEN_MORONG = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_LOAINGANSACH();

                if (!string.IsNullOrEmpty(this.MA_LOAINS))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_LOAINS),
                        Value = this.MA_LOAINS,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_LOAINS))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_LOAINS),
                        Value = this.TEN_LOAINS,
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
                if (!string.IsNullOrEmpty(this.TEN_MORONG))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_MORONG),
                        Value = this.TEN_MORONG,
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
                get { return ClassHelper.GetPropertyName(() => new PHB_DM_LOAINGANSACH().MA_LOAINS); }
            }
        }
    }
}
