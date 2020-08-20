using BTS.SP.PHB.ENTITY.Rp.PL01_TT137;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.PL01_TT137
{
    public class PL01_TT137ViewModel
    {
        public class AddModel
        {
            public PHB_PL01_TT137 data { get; set; }
            public PHB_PL01_TT137_DETAIL dataDetail { get; set; }
        }

        public class EditModel
        {
            public string MA_BAO_CAO { get; set; }
            public PHB_PL01_TT137 data { get; set; }
            public PHB_PL01_TT137_DETAIL dataDetail { get; set; }
        }

    }
    public class PL01_TT137ReportViewModel
    {
        //param
        public string LST_DVQHNS { get; set; }
        public int DONVI_TIEN { get; set; }
        public string NAM_BC { get; set; }
        public string KY_BC { get; set; }
        public string USERNAME { get; set; }
        public int LOAIBC { get; set; }
        public int CHITIET { get; set; }
        public string DSDVQHNS { get; set; }
        public string P_MA_DBHC { get; set; }
        //
        public int TongSoThu { get; set; }
        public int TongSoTienPhaiNop { get; set; }
        public int TongSoDuocKhauTru { get; set; }
        public string MA_QHNS { get; set; }
        public int SoDuKinhPhiNamTruoc { get; set; }
        public int DuToanDuocGiaoTrongNam { get; set; }
        public int KinhPhiThucNhanTrongNam { get; set; }
        public int KinhPhiQuyetToan { get; set; }
        public int KinhPhiGiamTrongNam { get; set; }
        public int SoDuKinhPhiNamSau { get; set; }
        public int KinhPhiDaNhan { get; set; }
        public int DuToanConDu { get; set; }
        public int KetQuaChenhLech { get; set; }
        public int KinhPhiTietKiem { get; set; }
        public int TrichLapCacQuy { get; set; }
        public int KinhPhiCaiCachTienLuong { get; set; }
    }
}
