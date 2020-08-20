using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU07TT344;

namespace BTS.SP.PHB.SERVICE.Models.BIEU_07TT344
{
    public class BIEU07TT344Vm
    {
        public class ViewModel
        {
            public string REFID { get; set; }
            public string TINH { get; set; }
            public string HUYEN { get; set; }
            public string XA { get; set; }
            public int NAM_BC { get; set; }

            public double TONGTHU { get; set; }
            public double THU_XAHUONG100 { get; set; }
            public double THU_CHIATYLE { get; set; }
            public double THU_BOSUNG { get; set; }
            public double THU_BOSUNGCANDOINS { get; set; }
            public double THU_BOSUNGCOMUCTIEU { get; set; }
            public double THU_KETDUNSNAMTRUOC { get; set; }
            public double THU_VIENTRO { get; set; }
            public double THU_CHUYENNGUON { get; set; }

            public double TONGCHI { get; set; }
            public double CHI_DAUTUPT { get; set; }
            public double CHI_THUONGXUYEN { get; set; }
            public double CHI_CHUYENNGUON { get; set; }
            public double CHI_NOPTRANS { get; set; }

            public double KETDUNS { get; set; }

        }
        public class InsertModel
        {
            public string MA_QHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public string TEN_QHNS { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }

            public string TINH { get; set; }
            public string HUYEN { get; set; }
            public string XA { get; set; }

            public double TONGTHU { get; set; }
            public double THU_XAHUONG100 { get; set; }
            public double THU_CHIATYLE { get; set; }
            public double THU_BOSUNG { get; set; }
            public double THU_BOSUNGCANDOINS { get; set; }
            public double THU_BOSUNGCOMUCTIEU { get; set; }
            public double THU_KETDUNSNAMTRUOC { get; set; }
            public double THU_VIENTRO { get; set; }
            public double THU_CHUYENNGUON { get; set; }

            public double TONGCHI { get; set; }
            public double CHI_DAUTUPT { get; set; }
            public double CHI_THUONGXUYEN { get; set; }
            public double CHI_CHUYENNGUON { get; set; }
            public double CHI_NOPTRANS { get; set; }

            public double KETDUNS { get; set; }
        }

        public class EditModel
        {
            public string REFID { get; set; }

            public double TONGTHU { get; set; }
            public double THU_XAHUONG100 { get; set; }
            public double THU_CHIATYLE { get; set; }
            public double THU_BOSUNG { get; set; }
            public double THU_BOSUNGCANDOINS { get; set; }
            public double THU_BOSUNGCOMUCTIEU { get; set; }
            public double THU_KETDUNSNAMTRUOC { get; set; }
            public double THU_VIENTRO { get; set; }
            public double THU_CHUYENNGUON { get; set; }

            public double TONGCHI { get; set; }
            public double CHI_DAUTUPT { get; set; }
            public double CHI_THUONGXUYEN { get; set; }
            public double CHI_CHUYENNGUON { get; set; }
            public double CHI_NOPTRANS { get; set; }

            public double KETDUNS { get; set; }
        }
    }
}
