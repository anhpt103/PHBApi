using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.C_B02AX;

namespace BTS.SP.PHB.SERVICE.Models.C_B02A_X
{
    public class C_B02A_XVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_C_B02AX_DETAIL> DETAIL { get; set; }
        }

        public class DetailModel
        {
            public string REFID { get; set; }
            public class Item : PHB_C_B02AX_DETAIL
            {
                public string MA_CHUONG { get; set; }
                public string MA_QHNS { get; set; }

                public int NAM_BC { get; set; }
                public int KY_BC { get; set; }
                public int TRANG_THAI { get; set; }
                public string MA_DBHC { get; set; }
                public string MA_DBHC_CHA { get; set; }
                public string TEN_QHNS { get; set; }
            }
            public List<Item> DETAIL { get; set; }
        }
        public class InsertModel
        {
            public string MA_CHUONG { get; set; }
            public string MA_QHNS { get; set; }

            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public int TRANG_THAI { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public string TEN_QHNS { get; set; }
            public List<PHB_C_B02AX_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_C_B02AX_DETAIL> LstAdd { get; set; }
            public List<PHB_C_B02AX_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
