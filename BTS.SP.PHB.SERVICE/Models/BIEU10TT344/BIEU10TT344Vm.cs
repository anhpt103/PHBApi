using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU10TT344;

namespace BTS.SP.PHB.SERVICE.Models.BIEU10TT344
{
    public class BIEU10TT344Vm
    {
        public class ViewModel
        {
            public string REFID { get; set; }
            public List<PHB_BIEU10TT344_DETAIL>  DETAIL { get; set; }
        }
        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public List<PHB_BIEU10TT344_DETAIL> DETAIL { get; set; }
        }
        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_BIEU10TT344_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU10TT344_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
