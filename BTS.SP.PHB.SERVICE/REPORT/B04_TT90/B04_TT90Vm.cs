using BTS.SP.PHB.ENTITY.Rp.B04_TT90;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.B04_TT90
{
    public class B04_TT90Vm
    {
        public class ContentData
        {
            public ContentData()
            {
                lstDetail = new List<PHB_B04_TT90_DETAIL>();
            }
            public string REFID { get; set; }
            public string MA_QHNS { get; set; }
            public string TEN_DVQHNS { get; set; }
            public int NAM_BC { get; set; }
            public int TRANG_THAI { get; set; }
            public DateTime NGAY_TAO { get; set; }
            public string NGUOI_TAO { get; set; }
            public DateTime? NGAY_SUA { get; set; }
            public string NGUOI_SUA { get; set; }
            public List<PHB_B04_TT90_DETAIL> lstDetail { get; set; }
        }

        public class DetailVm
        {
            public string PHB_B04_TT90_REFID { get; set; }
            public string MA_CHI_TIEU { get; set; }
            public string MA_CHI_TIEU_CHA { get; set; }
            public string MA_SO { get; set; }
            public string TEN_CHI_TIEU { get; set; }
            public string STT_CHI_TIEU { get; set; }
            public int SAP_XEP { get; set; }
            public int INDAM { get; set; }
            public int INNGHIENG { get; set; }
            public decimal TONGSOLIEU_BCQT { get; set; }
            public decimal TONGSOLIEU_QT_DUOCDUYET { get; set; }
            public decimal CHENH_LECH { get; set; }
            public decimal SOQUYETTOAN_DUOCDUYET { get; set; }
            public bool isAdd { get; set; }
        }
    }
}
