using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU12TT344;

namespace BTS.SP.PHB.SERVICE.Models.BIEU12TT344
{
    public class BIEU12TT344Vm
    {
        public class ViewModel
        {
            public string REFID { get; set; }
            public string TINH { get; set; }
            public string HUYEN { get; set; }
            public string XA { get; set; }
            public int NAM_BC { get; set; }
            public List<PHB_BIEU12TT344_DETAIL> DETAIL { get; set; }

        }
        public class InsertModel
        {
            public string REFID { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_QHNS { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public int TRANG_THAI { get; set; }
            public DateTime NGAY_TAO { get; set; }
            public string NGUOI_TAO { get; set; }
            public DateTime? NGAY_SUA { get; set; }
            public string NGUOI_SUA { get; set; }
            public string TINH { get; set; }
            public string HUYEN { get; set; }
            public string XA { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public List<PHB_BIEU12TT344_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_BIEU12TT344_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU12TT344_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
