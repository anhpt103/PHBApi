using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.C_B03X;

namespace BTS.SP.PHB.SERVICE.Models.C_B03X
{
    public class C_B03XVm
    {
        public class ViewModel
        {
            public string REFID { get; set; }
            public List<PHB_C_B03X_DETAIL> DETAIL { get; set; }
        }

        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public List<PHB_C_B03X_DETAIL> DETAIL { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_C_B03X_DETAIL> LstAdd { get; set; }
            public List<PHB_C_B03X_DETAIL> LstEdit { get; set; }
            public List<int> LstDelete { get; set; }
        }

        public class ContentData
        {
            public ContentData()
            {
                lstDetail = new List<PHB_C_B03X_DETAIL>();
            }
            public List<PHB_C_B03X_DETAIL> lstDetail { get; set; }
        }
    }
}
