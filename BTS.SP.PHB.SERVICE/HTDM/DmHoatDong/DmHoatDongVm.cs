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

namespace BTS.SP.PHB.SERVICE.HTDM.DmHoatDong
{
    public class DmHoatDongVm
    {
        public class ViewModel
        {
            public string TEN { get; set; }
            public string MO_TA { get; set; }
        }
        public class Search : IDataSearch
        {
            public string TEN { get; set; }
            public string MO_TA { get; set; }

            public void LoadGeneralParam(string summary)
            {
                TEN = summary;
                MO_TA = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_HOATDONG();

                if (!string.IsNullOrEmpty(this.TEN))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN),
                        Value = this.TEN,
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
                get { return ClassHelper.GetPropertyName(() => new PHB_DM_HOATDONG().TEN); }
            }
        }
    }
}
