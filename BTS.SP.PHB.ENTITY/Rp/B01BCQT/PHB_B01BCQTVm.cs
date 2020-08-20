using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.B01BCQT
{
    public class PHB_B01BCQTVm
    {
        public class BaoCaoDetail
        {
            public BaoCaoDetail()
            {
                lstCOL = new List<Dictionary<string, decimal?>>();
            }
            public string MA_SO { get; set; }
            public string TEN_CHI_TIEU { get; set; }
            public string STT_CHI_TIEU { get; set; }
            public int SAP_XEP { get; set; }
            public decimal? TONG_SO { get; set; }
            public List<Dictionary<string, decimal?>> lstCOL { get; set; }
        }



        //

        public class LoaiKhoanItem
        {
            public LoaiKhoanItem()
            {
                KhoanItem = new List<string>();
            }
            public string LoaiItem { get; set; }
            public List<string> KhoanItem { get; set; }
        }
        public class DataRes<T>
        {
            public DataRes()
            {
                Body = new List<T>();
                Header = new List<LoaiKhoanItem>();
            }
            public List<T> Body { get; set; }
            public List<LoaiKhoanItem> Header { get; set; }
            public int NAM_BC { get; set; }
        }
        public class InputPara
        {
            public string REFID { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }

        }
        public class EditModel
        {
            public EditModel()
            {
                LstAdd = new List<PHB_B01BCQT_DETAIL>();
                LstEdit = new List<PHB_B01BCQT_DETAIL>();
            }
            public string REFID { get; set; }
            public List<PHB_B01BCQT_DETAIL> LstAdd { get; set; }
            public List<PHB_B01BCQT_DETAIL> LstEdit { get; set; }
            public List<string> LstKhoanDelete { get; set; }
            public List<string> LstLoaiDelete { get; set; }
        }

        public class ContentData
        {
            public ContentData()
            {
                lstDetail = new List<PHB_B01BCQT_DETAIL>();
            }
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public string MA_QHNS_CHA { get; set; }
            public string MA_CHUONG { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public string MA_LOAI { get; set; }
            public string MA_KHOAN { get; set; }
            public List<PHB_B01BCQT_DETAIL> lstDetail { get; set; }
        }

        public class ColObject : IEquatable<ColObject>
        {
            public ColObject(string _khoan, decimal _GiaTri)
            {
                KHOAN = _khoan;
                GIA_TRI = _GiaTri;
            }

            public string KHOAN { get; set; }
            public decimal GIA_TRI { get; set; }

            public bool Equals(ColObject other)
            {
                return !ReferenceEquals(null, other) && KHOAN.Equals(other);
            }
            public override int GetHashCode()
            {
                return KHOAN.GetHashCode();
            }
        }
        public class SumPara
        {
            public string MADBHC { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
            public string LOAI_BC { get; set; }
            public string DSDVQHNS { get; set; }
            public string MA_DBHC { get; set; }
        }

        public class SumDetail
        {
            public SumDetail()
            {
                lstCOL = new List<ColObject>();
            }
            public string MA_SO { get; set; }
            public string TEN_CHI_TIEU { get; set; }
            public string STT_CHI_TIEU { get; set; }
            public int SAP_XEP { get; set; }
            public int IS_BOLD { get; set; }
            public int IS_ITALIC { get; set; }
            public decimal? TONG_SO { get; set; }
            public List<ColObject> lstCOL { get; set; }
        }
    }
}
