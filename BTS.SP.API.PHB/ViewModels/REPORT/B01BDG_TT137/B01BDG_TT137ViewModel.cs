using BTS.SP.PHB.ENTITY.Rp.B01BDG_TT137;
using System;
using System.Collections.Generic;

namespace BTS.SP.API.PHB.ViewModels.REPORT.B01BDG_TT137
{
    public class B01BDG_TT137ViewModel
    {
        public class AddModel
        {
            public PHB_B01BDG_TT137 data { get; set; }
            public List<PHB_B01BDG_TT137_DETAIL> datadetail { get; set; }
        }

        public class ContentData
        {
            public ContentData()
            {
                details = new List<PHB_B01BDG_TT137_DETAIL>();
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
            public List<PHB_B01BDG_TT137_DETAIL> details { get; set; }
        }

    }
}