using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU11TT344;

namespace BTS.SP.PHB.SERVICE.Models.BIEU11TT344
{
    public class BIEU11TT344Vm
    {
        public class ViewModel
        {
            public string REFID { get; set; }
            public string TINH { get; set; }
            public string HUYEN { get; set; }
            public string XA { get; set; }
            public int NAM_BC { get; set; }
            public List<PHB_BIEU11TT344_DETAIL> DETAIL { get; set; }

        }
        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public string THUYETMINH_1 { get; set; }
            public string THUYETMINH_2 { get; set; }
            public string THUYETMINH_3 { get; set; }
            public List<PHB_BIEU11TT344_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public string THUYETMINH_1 { get; set; }
            public string THUYETMINH_2 { get; set; }
            public string THUYETMINH_3 { get; set; }
            public List<PHB_BIEU11TT344_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU11TT344_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
