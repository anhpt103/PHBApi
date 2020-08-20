using BTS.SP.PHB.ENTITY.Rp.B03_TT90;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.B03_TT90
{
    public class B03_TT90Vm
    {
        public class ContentData
        {
            public ContentData()
            {
                lstDetail = new List<PHB_B03_TT90_DETAIL>();
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
            public List<PHB_B03_TT90_DETAIL> lstDetail { get; set; }
        }

        public class DetailVm
        {
            public string PHB_B03_TT90_REFID { get; set; }
            public string MA_CHI_TIEU { get; set; }
            public string MA_CHI_TIEU_CHA { get; set; }
            public string MA_SO { get; set; }
            public string TEN_CHI_TIEU { get; set; }
            public string STT_CHI_TIEU { get; set; }
            public int SAP_XEP { get; set; }
            public int INDAM { get; set; }
            public int INNGHIENG { get; set; }
            public decimal DU_TOAN_NAM { get; set; }
            public decimal UOC_THUC_HIEN { get; set; }
            public decimal UOC_THUC_HIEN_DU_TOAN { get; set; }
            public decimal UOC_THUC_HIEN_CUNG_KY { get; set; }
            public bool isAss { get; set; }
        }
    }
}
