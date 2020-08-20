using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B04_BCTC
{
    public class PHA_B04_BCTCVm
    {
        public class ContentData
        {
            public ContentData()
            {
                lstDetail = new List<PHA_B04_BCTC_DETAIL>();
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
            public string DON_VI_DT { get; set; }
            public string CAP_DU_TOAN { get; set; }
            public int NAM { get; set; }
            public List<PHA_B04_BCTC_DETAIL> lstDetail { get; set; }
        }
    }
}
