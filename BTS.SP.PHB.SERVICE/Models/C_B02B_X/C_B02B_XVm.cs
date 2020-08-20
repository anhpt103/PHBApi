using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.C_B02B_X;

namespace BTS.SP.PHB.SERVICE.Models.C_B02B_X
{
    public class C_B02B_XVm
    {
        //public class ViewModel
        //{
        //    public string REFID { get; set; }
        //    public string TINH { get; set; }
        //    public string HUYEN { get; set; }
        //    public string XA { get; set; }
        //    public int NAM_BC { get; set; }

        //    public List<PHB_C_B02B_X_DETAIL> DETAIL { get; set; }
        //}
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_C_B02B_X_DETAIL> DETAIL { get; set; }
        }
        public class DetailModel
        {
            public string REFID { get; set; }
            public class Item : PHB_C_B02B_X_DETAIL
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
            public string MA_QHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }

            public string TINH { get; set; }
            public string HUYEN { get; set; }
            public string XA { get; set; }

            public List<PHB_C_B02B_X_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }

            public List<PHB_C_B02B_X_DETAIL> LstEdit { get; set; }
        }
    }
}
