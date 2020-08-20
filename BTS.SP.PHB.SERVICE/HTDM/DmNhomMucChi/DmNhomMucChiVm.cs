using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.HTDM.DmNhomMucChi
{
    public class DmNhomMucChiVm
    {
        public class ViewModel
        {
            public string MA_NHOMMC { get; set; }
            public string TEN_NHOMMC { get; set; }
            public string MO_TA { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA_NHOMMC { get; set; }
            public string TEN_NHOMMC { get; set; }
            public string MO_TA { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_NHOMMC = summary;
                TEN_NHOMMC = summary;
                MO_TA = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_NHOMMUCCHI();

                if (!string.IsNullOrEmpty(this.MA_NHOMMC))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_NHOMMC),
                        Value = this.MA_NHOMMC,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_NHOMMC))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_NHOMMC),
                        Value = this.TEN_NHOMMC,
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
                get { return ClassHelper.GetPropertyName(() => new PHB_DM_NHOMMUCCHI().MA_NHOMMC); }
            }
        }
    }
}
