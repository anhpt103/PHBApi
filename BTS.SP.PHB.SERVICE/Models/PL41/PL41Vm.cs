using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.PL41;

namespace BTS.SP.PHB.SERVICE.Models.PL41
{
    public class PL41Vm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_PL41_DETAIL> DETAIL { get; set; }
        }

        public class LstDetail
        {
            public LstDetail ()
            {
                dataDetail = new List<PHB_PL41_DETAIL>();
            }
            public string DON_VI { get; set; }
            public List<PHB_PL41_DETAIL> dataDetail { get; set; }
        }


        public class DetailModel
        {
            public class Item: PHB_PL41_DETAIL
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
            public List<PHB_PL41_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_PL41_DETAIL> LstAdd { get; set; }
            public List<PHB_PL41_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
