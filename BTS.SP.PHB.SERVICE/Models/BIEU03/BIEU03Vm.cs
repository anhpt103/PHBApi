using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.BIEU03;

namespace BTS.SP.PHB.SERVICE.Models.BIEU03
{
    public class BIEU03Vm
    {
        public class ViewModel
        {
            public string REFID { get; set; }
            public List<PHB_BIEU03_DETAIL> DETAIL { get; set; }
            public string THUYETMINH_1 { get; set; }
            public string THUYETMINH_2 { get; set; }
            public string THUYETMINH_3 { get; set; }
        }

        public class DetailModel
        {
            public class Item : PHB_BIEU03_DETAIL
            {
                public string MA_QHNS { get; set; }

                public string TEN_QHNS { get; set; }
                public int INDAM { get; set; }
                public int INNGHIENG { get; set; }
            }
            public string REFID { get; set; }

            public List<Item> DETAIL { get; set; }
            public string THUYETMINH_1 { get; set; }
            public string THUYETMINH_2 { get; set; }
            public string THUYETMINH_3 { get; set; }
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
            public List<PHB_BIEU03_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public string THUYETMINH_1 { get; set; }
            public string THUYETMINH_2 { get; set; }
            public string THUYETMINH_3 { get; set; }
            public List<PHB_BIEU03_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU03_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
