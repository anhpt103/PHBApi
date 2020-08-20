using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.HTDM.DmTaiKhoan
{
    public class DmTaiKhoanVm
    {
        public class ViewModel
        {
            public string MA_TAI_KHOAN { get; set; }
            public string TEN_TAI_KHOAN { get; set; }
            public string MA_CHA { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA_TAI_KHOAN { get; set; }
            public string TEN_TAI_KHOAN { get; set; }
            public string MA_CHA { get; set; }

            public void LoadGeneralParam(string summary)
            {
                MA_TAI_KHOAN = summary;
                TEN_TAI_KHOAN = summary;
                MA_CHA = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_TAIKHOAN();

                if (!string.IsNullOrEmpty(this.MA_CHA))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_CHA),
                        Value = this.MA_CHA,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.MA_TAI_KHOAN))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.MA_TAI_KHOAN),
                        Value = this.MA_TAI_KHOAN,
                        Method = FilterMethod.Like
                    });
                }
                if (!string.IsNullOrEmpty(this.TEN_TAI_KHOAN))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN_TAI_KHOAN),
                        Value = this.TEN_TAI_KHOAN,
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
                get { return ClassHelper.GetPropertyName(() => new PHB_DM_TAIKHOAN().MA_TAI_KHOAN); }
            }
        }
    }
}
