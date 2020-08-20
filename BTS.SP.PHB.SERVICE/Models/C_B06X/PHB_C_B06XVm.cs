using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Rp.C_B06X;

namespace BTS.SP.PHB.SERVICE.Models.C_B06X
{
    public class PHB_C_B06XVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PHB_C_B06X_DETAIL> DETAIL_QC { get; set; }

            public List<PHB_C_B06X_DETAIL> DETAIL_HDSN { get; set; }
            public List<PHB_C_B06X_DETAIL> DETAIL_HDK { get; set; }
            public int NAM_BC { get; set; }
            public List<PHB_C_B06X_DETAIL> DETAIL { get; set; }
        }

        public class DetailModel
        {
            public string REFID { get; set; }
            public class Item : PHB_C_B06X_DETAIL
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
        //public class DetailModel
        //{
        //    public int LOAI { get; set; }
        //    public string MA_CHITIEU { get; set; }
        //    public string STT_CHI_TIEU { get; set; }
        //    public string TEN_CHITIEU { get; set; }
        //    public double TONG_THU { get; set; }
        //    public double TIEN_NSNN { get; set; }
        //    public double TIEN_KHAUTRU { get; set; }
        //}
        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_DBHC  { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public List<PHB_C_B06X_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_C_B06X_DETAIL> LstAdd { get; set; }
            public List<PHB_C_B06X_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
