using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU01A;

namespace BTS.SP.PHB.SERVICE.Models.BIEU01A
{
    public class BIEU01AVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_BIEU01A_DETAIL> DETAIL { get; set; }

        }
       
        public class DetaiRplModel
        {
            public class Item : PHB_BIEU01A_DETAIL
            {
                public string MA_QHNS { get; set; }
                public string TEN_QHNS { get; set; }
                public double TONG_THU { get; set; }
                public double TIEN_NSNN { get; set; }
                public double TIEN_KHAUTRU { get; set; }
            }
            public string REFID { get; set; }
            public List<Item> DETAIL { get; set; }
        }

        public class DetailModel
        {
            public class Item: PHB_BIEU01A_DETAIL
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
            public List<PHB_BIEU01A_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_BIEU01A_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU01A_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
