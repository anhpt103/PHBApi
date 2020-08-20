using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU08TT344;

namespace BTS.SP.PHB.SERVICE.Models.BIEU_08TT344
{
    public class BIEU08TT344Vm
    {
        public class ViewModel
        {
            public string REFID { get; set; }
            public string TINH { get; set; }
            public string HUYEN { get; set; }
            public string XA { get; set; }
            public int NAM_BC { get; set; }

            public List<PHB_BIEU08TT344_DETAIL> DETAIL { get; set; } 

            //public int SAPXEP { get; set; }
            //public string NOIDUNG { get; set; }
            //public string MA_CHITIEU { get; set; }

            //public double DUTOAN_NSNN { get; set; }
            //public double DUTOAN_NSX { get; set; }

            //public double QUYETTOAN_NSNN { get; set; }
            //public double QUYETTOAN_NSX { get; set; }

            //public double SOSANH_NSNN { get; set; }
            //public double SOSANH_NSX { get; set; }

        }
        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public string TEN_QHNS { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }

            public string TINH { get; set; }
            public string HUYEN { get; set; }
            public string XA { get; set; }

            public List<PHB_BIEU08TT344_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }

            public List<PHB_BIEU08TT344_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU08TT344_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }
    }
}
