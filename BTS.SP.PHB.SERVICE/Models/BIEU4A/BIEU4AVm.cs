using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.BIEU4A;

namespace BTS.SP.PHB.SERVICE.Models.BIEU4A
{
    public class BIEU4AVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_BIEU4A_DETAIL> DETAIL { get; set; }
        }
        public class DetailModel
        {
            public class Item: PHB_BIEU4A_DETAIL
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
            public List<PHB_BIEU4A_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_BIEU4A_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU4A_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
