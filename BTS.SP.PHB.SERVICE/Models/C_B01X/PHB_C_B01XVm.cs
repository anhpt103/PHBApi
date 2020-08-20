using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.C_B01X;

namespace BTS.SP.PHB.SERVICE.Models.C_B01X
{
    public class PHB_C_B01XVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_C_B01X_DETAIL> DETAIL_TRONGBANG { get; set; }

            public List<PHB_C_B01X_DETAIL> DETAIL_NGOAIBANG { get; set; }
        }
        public class DetailModel
        {
            public int LOAI { get; set; }
            public string MA_NOIDUNGKT { get; set; }
            public string STT_CHI_TIEU { get; set; }
            public string TEN_NOIDUNGKT { get; set; }
            public double TONG_THU { get; set; }
            public double TIEN_NSNN { get; set; }
            public double TIEN_KHAUTRU { get; set; }
        }
        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_DBHC  { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public int MA_BAO_CAO { get; set; }
            public int MA_DBHC_CHA { get; set; }
            public List<PHB_C_B01X_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_C_B01X_DETAIL> LstAdd { get; set; }
            public List<PHB_C_B01X_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
