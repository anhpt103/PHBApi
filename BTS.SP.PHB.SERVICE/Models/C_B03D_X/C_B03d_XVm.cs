using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.C_B03D_X;

namespace BTS.SP.PHB.SERVICE.Models.C_B03D_X
{
    public class C_B03d_XVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }
            public int NAM_BC { get; set; }
            public List<PHB_C_B03D_X_DETAIL> DETAIL { get; set; }

        }
        public class DetailModel
        {
            public string REFID { get; set; }
            public class Item : PHB_C_B03D_X_DETAIL
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
        public class ViewModelDTO
        {
            public ViewModelDTO()
            {
                DETAIL = new List<PHB_C_B03D_X_DETAIL_DTO>();
            }
            public string NGUOI_SUA { get; set; }
            public string REFID { get; set; }
            public int NAM_BC { get; set; }
            public List<PHB_C_B03D_X_DETAIL_DTO> DETAIL { get; set; }

        }

        public class PHB_C_B03D_X_DETAIL_DTO
        {
            public int ID { get; set; }
            public string PHB_C_B03D_X_REFID { get; set; }
            public string MACHITIEU { get; set; }
            public string MACHA { get; set; }
            public Nullable<int> CAP { get; set; }
            public string TENCHITIEU { get; set; }
            public string STTCHITIEU { get; set; }
            public double DUTOANNAM { get; set; }
            public double QUYETTOANNAM { get; set; }
            public Nullable<int> ISCHILD { get; set; }
            public int INDAM { get; set; }

        }


        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public List<PHB_C_B03D_X_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_C_B03D_X_DETAIL> LstAdd { get; set; }
            public List<PHB_C_B03D_X_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
