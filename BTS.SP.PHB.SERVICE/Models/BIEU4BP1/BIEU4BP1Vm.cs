using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.BIEU4BP1;

namespace BTS.SP.PHB.SERVICE.Models.BIEU4BP1
{
    public class BIEU4BP1Vm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_BIEU4BP1_DETAIL> DETAIL { get; set; }
        }
        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public List<PHB_BIEU4BP1_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_BIEU4BP1_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU4BP1_DETAIL> LstEdit { get; set; }
            public List<string> LstKhoanDelete { get; set; }
            public List<string> LstLoaiDelete { get; set; }
        }
    }
}
