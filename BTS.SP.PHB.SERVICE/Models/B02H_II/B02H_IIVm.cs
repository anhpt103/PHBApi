using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.B02H_II;

namespace BTS.SP.PHB.SERVICE.Models.B02H_II
{
    public class B02H_IIVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_B02H_II_DETAIL> DETAIL { get; set; }
        }
        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public List<PHB_B02H_II_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_B02H_II_DETAIL> LstAdd { get; set; }
            public List<PHB_B02H_II_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
