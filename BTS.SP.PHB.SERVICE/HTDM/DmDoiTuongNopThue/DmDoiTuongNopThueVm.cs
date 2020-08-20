using BTS.SP.PHB.ENTITY.Dm;
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

namespace BTS.SP.PHB.SERVICE.HTDM.DmDoiTuongNopThue
{
    public class DmDoiTuongNopThueVm
    {
        public class Search : IDataSearch
        {
            public string MA { get; set; }
            public string TEN { get; set; }
            public string CQTC_MA { get; set; }
            public string DIACHI { get; set; }
            public int CAP { get; set; }
            public string CHUONG { get; set; }
            public string LOAI { get; set; }
            public string KHOAN { get; set; }
            public string TRANG_THAI { get; set; }

            public string DefaultOrder
            {
                get
                {
                    return ClassHelper.GetPropertyName(() => new PHB_DM_COQUANTHU().MA);
                }
            }
            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_COQUANTHU();
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

            public void LoadGeneralParam(string summary)
            {
                MA = summary;
                TEN = summary;
                CQTC_MA = summary;
                DIACHI = summary;
                CHUONG = summary;
                LOAI = summary;
                KHOAN = summary;
            }
        }
    }
}
