using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.BIEU01B;

namespace BTS.SP.PHB.SERVICE.Models.BIEU01B
{
    public class BIEU01BVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_BIEU01B_DETAIL> DETAIL { get; set; }
        }

        public class DetailModel
        {
            public class Item: PHB_BIEU01B_DETAIL
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
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public List<PHB_BIEU01B_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_BIEU01B_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU01B_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
