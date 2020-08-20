using System;
using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.C_B04X;
namespace BTS.SP.PHB.SERVICE.Models.C_B04X
{
    public class C_B04XVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_QHNS { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public int TRANG_THAI { get; set; }
            public DateTime NGAY_TAO { get; set; }
            public string NGUOI_TAO { get; set; }
            public DateTime? NGAY_SUA { get; set; }
            public string NGUOI_SUA { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public string TEN_QHNS { get; set; }
            public Nullable<decimal> DIEN_TICH { get; set; }
            public Nullable<decimal> DIEN_TICH_DAT { get; set; }
            public Nullable<decimal> DANSO { get; set; }
            public string NGANH_NGHE { get; set; }
            public string MUCTIEU_NHIEMVU { get; set; }
            public string DANH_GIA { get; set; }
            public string NGUYEN_NHAN { get; set; }
            public string KHACH_QUAN { get; set; }
            public string CHU_QUAN { get; set; }
            public string DENGHI_KIENXUAT { get; set; }
            public List<PHB_C_B04X_DETAIL> DataDetails { get; set; }
            public List<PHB_C_B04X_DETAIL> DETAIL { get; set; }
            public List<PHB_C_B04X_DETAIL_TSCD> DataDetailTSCD { get; set; }
        }

        public class DetailModel
        {
            public string REFID { get; set; }
            public class Item : PHB_C_B04X_DETAIL
            {
                public string MA_CHUONG { get; set; }
                public string MA_QHNS { get; set; }

                public int NAM_BC { get; set; }
                public int KY_BC { get; set; }
                public int TRANG_THAI { get; set; }
                public string MA_DBHC { get; set; }
                public string MA_DBHC_CHA { get; set; }
                public string TEN_QHNS { get; set; }
            }
            public List<Item> DETAIL { get; set; }
        }
        public class InsertModel
        {
            public string REFID { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_QHNS { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public int TRANG_THAI { get; set; }
            public DateTime NGAY_TAO { get; set; }
            public string NGUOI_TAO { get; set; }
            public DateTime? NGAY_SUA { get; set; }
            public string NGUOI_SUA { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public string TEN_QHNS { get; set; }
            public Nullable<decimal> DIEN_TICH { get; set; }
            public Nullable<decimal> DIEN_TICH_DAT { get; set; }
            public Nullable<decimal> DANSO { get; set; }
            public string NGANH_NGHE { get; set; }
            public string MUCTIEU_NHIEMVU { get; set; }
            public string DANH_GIA { get; set; }
            public string NGUYEN_NHAN { get; set; }
            public string KHACH_QUAN { get; set; }
            public string CHU_QUAN { get; set; }
            public string DENGHI_KIENXUAT { get; set; }
            public List<PHB_C_B04X_DETAIL> DataDetails { get; set; }
            public List<PHB_C_B04X_DETAIL_TSCD> DataDetailTSCD { get; set; }
        }
        public class EditModel
        {
            public string REFID { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_QHNS { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public int TRANG_THAI { get; set; }
            public DateTime NGAY_TAO { get; set; }
            public string NGUOI_TAO { get; set; }
            public DateTime? NGAY_SUA { get; set; }
            public string NGUOI_SUA { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public string TEN_QHNS { get; set; }
            public Nullable<decimal> DIEN_TICH { get; set; }
            public Nullable<decimal> DIEN_TICH_DAT { get; set; }
            public Nullable<decimal> DANSO { get; set; }
            public string NGANH_NGHE { get; set; }
            public string MUCTIEU_NHIEMVU { get; set; }
            public string DANH_GIA { get; set; }
            public string NGUYEN_NHAN { get; set; }
            public string KHACH_QUAN { get; set; }
            public string CHU_QUAN { get; set; }
            public string DENGHI_KIENXUAT { get; set; }
            public List<PHB_C_B04X_DETAIL> DataDetails { get; set; }
            public List<PHB_C_B04X_DETAIL_TSCD> DataDetailTSCD { get; set; }
        }
    }
}
