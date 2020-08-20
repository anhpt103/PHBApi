using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.F01_02BCQT_PII;

namespace BTS.SP.PHB.SERVICE.Models.F01_02BCQT_PII
{
    public class F01_02BCQT_PIIVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_F01_02BCQT_PII_DETAIL> DETAIL { get; set; }
        }
        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public List<PHB_F01_02BCQT_PII_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; } 
            public List<PHB_F01_02BCQT_PII_DETAIL> LstAdd { get; set; }
            public List<PHB_F01_02BCQT_PII_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
