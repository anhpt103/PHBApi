using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.BIEU3BP1;

namespace BTS.SP.PHB.SERVICE.Models.BIEU3BP1
{
    public class BIEU3BP1Vm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_BIEU3BP1_DETAIL> DETAIL { get; set; }
        }

        public class DetailModel
        {
            public class Item: PHB_BIEU3BP1_DETAIL
            {
                public int INDAM { get; set; }
                public int INNGHIENG { get; set; }
                public int LOAI { get; set; }
                public int CAP { get; set; }
                public string CONG_THUC { get; set; }
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
            public List<PHB_BIEU3BP1_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_BIEU3BP1_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU3BP1_DETAIL> LstEdit { get; set; }
            public List<string> LstKhoanDelete { get; set; }
            public List<string> LstLoaiDelete { get; set; }
        }
    }
}
