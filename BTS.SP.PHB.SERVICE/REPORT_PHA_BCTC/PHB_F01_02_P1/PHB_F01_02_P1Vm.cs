using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.PHB_F01_02_P1
{
    public class PHB_F01_02_P1Vm
    {
        public class ContentData
        {
            public ContentData()
            {
                lstDetail = new List<ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1.PHB_F01_02_P1>();
            }
            public string REFID { get; set; }
            public int TRANG_THAI { get; set; }
            public int TRANG_THAI_GUI { get; set; }
            public DateTime? NGAY_TAO { get; set; }
            public string NGUOI_TAO { get; set; }
            public DateTime? NGAY_SUA { get; set; }
            public string NGUOI_SUA { get; set; }
            public string MA_DONVI { get; set; }
            public string TEN_DONVI { get; set; }
            public string MA_QHNS_QL { get; set; }
            public string DON_VI_DT { get; set; }
            public string CAP_DU_TOAN { get; set; }
            public int NAM { get; set; }
            public int KY_BC { get; set; }
            public List<ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1.PHB_F01_02_P1> lstDetail { get; set; }
        }

    }
}
