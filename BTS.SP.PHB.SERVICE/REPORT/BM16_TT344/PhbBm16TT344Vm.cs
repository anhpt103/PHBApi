using BTS.SP.PHB.ENTITY.Rp.BM16_TT344;
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

namespace BTS.SP.PHB.SERVICE.REPORT.BM16_TT344
{
    public class PhbBm16TT344Vm
    {
        public class Search : IDataSearch
        {
            public string MA_BAOCAO_TU { get; set; }
            public string NGUOI_TAO { get; set; }
            public string NGAY_TAO { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_BAOCAO_TU = summary;
                NGUOI_TAO = summary;
                NGAY_TAO = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_BM16_TT344();

                if (!string.IsNullOrEmpty(this.MA_BAOCAO_TU))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_BAOCAO_TU),
                        Value = this.MA_BAOCAO_TU,
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
                get { return ClassHelper.GetPropertyName(() => new PHB_BM16_TT344().MA_BAOCAO_TU); }
            }
        }
    }
}
