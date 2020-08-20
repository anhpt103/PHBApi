
using BTS.SP.PHB.ENTITY.Rp.BM14_TT144;
using BTS.SP.PHB.ENTITY.Rp.PHB_BM14TT134;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.BM14_TT134
{
    public class kekhaichungtuVm
    {
        public class Search : IDataSearch
        {
            public string MA_KTC { get; set; }
            public string NGUOI_TAO { get; set; }
            public string NGAY_TAO { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_KTC = summary;
                NGUOI_TAO = summary;
                NGAY_TAO = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new KEKHAICHUNGTU();

                if (!string.IsNullOrEmpty(this.MA_KTC))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_KTC),
                        Value = this.MA_KTC,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.NGUOI_TAO))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.NGUOI_TAO),
                        Value = this.NGUOI_TAO,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.NGAY_TAO))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.NGAY_TAO),
                        Value = this.NGAY_TAO,
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
                get { return ClassHelper.GetPropertyName(() => new KEKHAICHUNGTU().MA_KTC); }
            }
        }
    }
}
    
