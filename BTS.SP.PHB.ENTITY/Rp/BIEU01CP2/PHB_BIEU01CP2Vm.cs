using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.BIEU01CP2
{
    public class PHB_BIEU01CP2Vm
    {

        public class BaoCaoDetail
        {
            public BaoCaoDetail()
            {
                lstCOL = new List<ColObject>();
            }
            public string MA_SO { get; set; }
            public string TEN_CHI_TIEU { get; set; }
            public string STT_CHI_TIEU { get; set; }
            public int SAP_XEP { get; set; }
            public decimal? TONG_SOBC { get; set; }
            public decimal? TONG_SODUYET { get; set; }
            public decimal? TONG_CHENHLECH { get; set; }
            public List<ColObject> lstCOL { get; set; }
        }


        public class LoaiKhoanItem
        {
            public LoaiKhoanItem()
            {
                KhoanItem = new List<string>();
            }
            public string LoaiItem { get; set; }
            public List<string> KhoanItem { get; set; }
        }
        public class DataRes
        {
            public DataRes()
            {
                Body = new List<BaoCaoDetail>();
                Header = new List<LoaiKhoanItem>();
            }
            public List<BaoCaoDetail> Body { get; set; }
            public List<LoaiKhoanItem> Header { get; set; }
            public int NAM_BC { get; set; }
        }
        public class TemplateReport
        {
            public string MA_SO { get; set; }
            public string TEN_CHI_TIEU { get; set; }
            public string STT_CHI_TIEU { get; set; }
            public int SAP_XEP { get; set; }
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

        public class ColObject : IEquatable<ColObject>
        {
            public ColObject(string _khoan, decimal _SoBC, decimal _SoDuyet, decimal _ChenhLech)
            {
                KHOAN = _khoan;
                SO_BC = _SoBC;
                SO_DUYET = _SoDuyet;
                CHENH_LECH = _ChenhLech;
            }

            public string KHOAN { get; set; }
            public decimal SO_BC { get; set; }
            public decimal SO_DUYET { get; set; }
            public decimal CHENH_LECH { get; set; }

            public bool Equals(ColObject other)
            {
                return !ReferenceEquals(null, other) && KHOAN.Equals(other);
            }            
            public override int GetHashCode()
            {
                return KHOAN.GetHashCode();
            }
        }


    }
}
