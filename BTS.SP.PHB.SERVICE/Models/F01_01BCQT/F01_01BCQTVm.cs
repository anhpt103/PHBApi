using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.F01_01BCQT;

namespace BTS.SP.PHB.SERVICE.Models.F01_01BCQT
{
    public class F01_01BCQTVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_F01_01BCQT_DETAIL> DETAIL { get; set; }
        }
        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public string TEN_QHNS { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public List<PHB_F01_01BCQT_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_F01_01BCQT_DETAIL> LstAdd { get; set; }
            public List<PHB_F01_01BCQT_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
