using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Types;

namespace BTS.SP.PHB.SERVICE.HTDM.DmLoaiKhoan
{
    public class DmLoaiKhoanVm
    {
        public class KhoanItem
        {
            public string MA_KHOAN { get; set; }
            public string TEN_KHOAN { get; set; }
            public string MA_LOAI { get; set; }
        }
        public class LoaiItem
        {   
            public string MA_LOAI { get; set; }
            public string TEN_LOAI { get; set; }
        }

        public class ResponseData
        {
            public ResponseData()
            {
                lstLoai = new List<LoaiItem>();
                lstKhoan = new List<KhoanItem>();
            }
            public List<LoaiItem> lstLoai { get; set; }
            public List<KhoanItem> lstKhoan { get; set; }

        }
        public class ViewModel
        {
            public string MA { get; set; }
            public string TEN { get; set; }
        }
        public class Search : IDataSearch
        {
            public string MA { get; set; }
            public string TEN { get; set; }

            public void LoadGeneralParam(string summary)
            {
                TEN = summary;
                MA = summary;
            }

            public List<IQueryFilter> GetFilters()
            {
                var result = new List<IQueryFilter>();
                var refObj = new PHB_DM_LOAIKHOAN();

                if (!string.IsNullOrEmpty(this.TEN))
                {
                    result.Add(new QueryFilterLinQ
                    {
                        Property = ClassHelper.GetProperty(() => refObj.TEN),
                        Value = this.TEN,
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
                return result;
            }

            public List<IQueryFilter> GetQuickFilters()
            {
                return null;
            }

            public string DefaultOrder
            {
                get { return ClassHelper.GetPropertyName(() => new PHB_DM_LOAIKHOAN().MA); }
            }
        }
    }
}
