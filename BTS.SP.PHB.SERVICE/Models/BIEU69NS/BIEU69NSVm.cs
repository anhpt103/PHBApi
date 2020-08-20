using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.BIEU69NS;

namespace BTS.SP.PHB.SERVICE.Models.BIEU69NS
{
    public class BIEU69NSVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_BIEU69NS_DETAIL> DETAIL { get; set; }
        }
        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public List<PHB_BIEU69NS_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_BIEU69NS_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU69NS_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
