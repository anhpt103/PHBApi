using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.Models.PL03
{
    public class PL03Vm
    {

        public class ViewModel
        {
            public string REFID { get; set; }

            public List<PL03DTO> DETAIL { get; set; }
        }

        public class PL03DTO
        {
            public string SAPXEP { get; set; }
            public string CT { get; set; }
            public int CAP { get; set; }
            public int INDAM { get; set; }
            public int INGHIENG { get; set; }
            public string HIENTHI { get; set; }
            public string TRANGTHAI { get; set; }
            public int LOAI_CHITIEU { get; set; }
            public string MA_CHITIEU { get; set; }
            public string MA_CHITIEU_2 { get; set; }
            public string TEN_CHITIEU { get; set; }
            public double QT { get; set; }
            public double DT { get; set; }
            public string STT { get; set; }

        }

    }
}
