using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.BIEU4A;
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;

namespace BTS.SP.PHB.SERVICE.Models.BIEU70NS
{
    public class BIEU70NSVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_BIEU70NS_DETAIL> DETAIL { get; set; }
        }
        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public List<PHB_BIEU70NS_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_BIEU70NS_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU70NS_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
