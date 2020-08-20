using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHC.SERVICE.TienIch
{
    public class KetXuatQTChiVm
    {
        public class DTO
        {
            public string TEN_CHUONG { get; set; }
            public string TEN_TIEUMUC { get; set; }
            public string TEN_NGANHKT { get; set; }

            public string CHUONG { get; set; }
            public string TIEUMUC { get; set; }
            public string LOAIKHOAN { get; set; }

            public decimal SOTIEN { get; set; }
        }
        public class DTOB02B
        {

            public string MACHITIEU { get; set; }
            public string TENCHITIEU { get; set; }
            public string SAPXEP { get; set; }
            public decimal LUYKE { get; set; }
            public decimal DUTOANNAM { get; set; }
            public decimal THANG { get; set; }
        }
        public class InputParam
        {
            public string P_CONGTHUC { get; set; }
            public string LOAIBAOCAO { get; set; }

            public DateTime TUNGAY_HL { get; set; }
            public DateTime DENNGAY_HL { get; set; }
        }

    }
}
