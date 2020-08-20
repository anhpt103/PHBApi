using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.B03BCQT_BII1;

namespace BTS.SP.PHB.SERVICE.Models.B03BCQT_BII1
{
    public class B03BCQT_BII1Vm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_B03BCQT_BII1_DETAIL> DETAIL_PHI { get; set; }

            public List<PHB_B03BCQT_BII1_DETAIL> DETAIL_LEPHI { get; set; }
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
            public string TEN_QHNS { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public List<PHB_B03BCQT_BII1_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_B03BCQT_BII1_DETAIL> LstAdd { get; set; }
            public List<PHB_B03BCQT_BII1_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
