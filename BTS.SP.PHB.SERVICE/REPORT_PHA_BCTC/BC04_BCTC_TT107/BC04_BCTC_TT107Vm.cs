using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.BC04_BCTC_TT107
{
    public class BC04_BCTC_TT107Vm
    {
        public class ContentData
        {
            public ContentData()
            {
                lstDetail = new List<ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107.BC04_BCTC_TT107>();
            }
            public string REFID { get; set; }
            public int TRANG_THAI { get; set; }
            public int TRANG_THAI_GUI { get; set; }
            public DateTime? NGAY_TAO { get; set; }
            public string NGUOI_TAO { get; set; }
            public DateTime? NGAY_SUA { get; set; }
            public string NGUOI_SUA { get; set; }
            public string MA_DONVI { get; set; }
            public string MA_QHNS_QL { get; set; }
            public string TEN_DONVI { get; set; }
            public string DON_VI_DT { get; set; }
            public string CAP_DU_TOAN { get; set; }
            public int NAM { get; set; }
            public int KY_BC { get; set; }
            public string SO_QD_THANHLAP { get; set; }
            public DateTime? NGAY_QD_THANHLAP { get; set; }
            public string TEN_DONVI_CAPTREN { get; set; }
            public bool THUOC_DONVI_CAP1 { get; set; }
            public string MA_LOAIHINH { get; set; }
            public string TEN_LOAIHINH { get; set; }
            public string SO_QD_GIAO { get; set; }
            public DateTime? NGAY_QD_GIAO { get; set; }
            public string CHUCNANG_NHIEMVU { get; set; }
            public string BCTC_PHEDUYET { get; set; }
            public DateTime? BCTC_NGAY_PHEDUYET { get; set; }
            public List<ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107.BC04_BCTC_TT107> lstDetail { get; set; }
        }
    }
}
