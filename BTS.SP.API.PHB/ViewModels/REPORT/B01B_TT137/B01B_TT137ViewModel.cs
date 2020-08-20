using BTS.SP.PHB.ENTITY.Rp.B01B_TT137;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.REPORT.B01B_TT137
{
    public class B01B_TT137ViewModel
    {
        public class AddModel
        {
            public PHB_B01B_TT137 data { get; set; }
            public List<PHB_B01B_TT137_DETAIL> datadetail { get; set; }
        }

        public class ContentData
        {
            public ContentData()
            {
                details = new List<PHB_B01B_TT137_DETAIL>();
            }
            public string REFID { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public int TRANG_THAI { get; set; }
            public DateTime NGAY_TAO { get; set; }
            public string NGUOI_TAO { get; set; }
            public List<PHB_B01B_TT137_DETAIL> details { get; set; }
        }

        public class Template
        {
            public int ID { get; set; }
            public int STT { get; set; }
            public string STT_CHI_TIEU { get; set; }
            public string MA_CHI_TIEU { get; set; }
            public string TEN_CHI_TIEU { get; set; }
            public int IS_BOLD { get; set; }
            public int IS_ITALIC { get; set; }
            public int PHAN { get; set; }
            public int LOAI { get; set; }
            public string MA_CHA { get; set; }
            public double? THUC_HIEN { get; set; }
            public double? SO_DOI_CHIEU { get; set; }
            public double? CHENH_LECH { get; set; }
        }

    }
}