using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.HTDM.DmNoiDungKt
{
    public class DmNoiDungKtVm
    {
        public class ViewModel
        {
            public string MA { get; set; }
            public string TEN { get; set; }
            public string MA_CHA { get; set; }
            public string MA_NHOMMC { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA { get; set; }
            public string TEN { get; set; }
            public string MA_CHA { get; set; }
            public string MA_NHOMMC { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_NHOMMC = summary;
                MA = summary;
                TEN = summary;
                MA_CHA = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_NOIDUNGKT();

                if (!string.IsNullOrEmpty(this.MA_NHOMMC))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_NHOMMC),
                        Value = this.MA_NHOMMC,
                        Method = FilterMethod.Like
                    });
                }
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
                return result;
            }

            public List<IQueryFilter> GetQuickFilters()
            {
                return null;
            }

            public string DefaultOrder
            {
                get { return ClassHelper.GetPropertyName(() => new PHB_DM_NOIDUNGKT().MA); }
            }
        }
    }
}
