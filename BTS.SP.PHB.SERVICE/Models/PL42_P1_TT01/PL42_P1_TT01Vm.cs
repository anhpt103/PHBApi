using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.PHB.PL42_P1_TT01;

namespace BTS.SP.PHB.SERVICE.Models.PL42_P1_TT01
{
    public class PL42_P1_TT01Vm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_PL42_P1_TT01_DETAIL> DETAIL { get; set; }
        }
        public class DetailModel
        {
            public class Item : PHB_PL42_P1_TT01_DETAIL
            {
                public string MA_QHNS { get; set; }
                public string TEN_QHNS { get; set; }
            }
            public string REFID { get; set; }
            public List<Item> DETAIL { get; set; }
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
            public List<PHB_PL42_P1_TT01_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_PL42_P1_TT01_DETAIL> LstAdd { get; set; }
            public List<PHB_PL42_P1_TT01_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
