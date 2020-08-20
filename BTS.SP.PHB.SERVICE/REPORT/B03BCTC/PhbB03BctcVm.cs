using BTS.SP.PHB.ENTITY.Rp.B03BBCTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.B03BCTC
{
    public class PhbB03BctcVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }
            public List<PHB_B03BBCTC_DETAIL> DETAIL { get; set; }
        }

        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public List<PHB_B03BBCTC_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_B03BBCTC_DETAIL> LstAdd { get; set; }
            public List<PHB_B03BBCTC_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }

    }
}
