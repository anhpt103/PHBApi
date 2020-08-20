using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp
{
    public class ReportRqModel
    {
        public string MA_CHUONG { get; set; }
        public int NAM_BC { get; set; }
        public int KY_BC { get; set; }
        public int LOAI_BC { get; set; }
        public bool CHI_TIET { get; set; }
        public List<string> DSDVQHNS { get; set; }
        public bool TONG_HOP_BAOCAO { get; set; }
        public List<string> TEN_DSDVQHNS { get; set; }

        public List<string> changeList { get; set; }
        public string newName { get; set; }
        public int PHAN { get; set; }
        public int LOAI { get; set; }
        public int CAP { get; set; }
        public string MA_CHI_TIEU_CHA { get; set; }
        public string isPHAN { get; set; }

        public string MA_DBHC { get; set; }
        public string MA_DBHC_CHA { get; set; }
        //public List<string> MADBHC { get; set; }
        //public List<string> MADBHC_CHA { get; set; }
    }
    public class ReportRqModelBack
    {
        public string MA_CHUONG { get; set; }
        public int NAM_BC { get; set; }
        public int KY_BC { get; set; }
        public int LOAI_BC { get; set; }
        public bool CHI_TIET { get; set; }
        public List<string> DSDVQHNS { get; set; }
        public bool TONG_HOP_BAOCAO { get; set; }
        public List<string> TEN_DSDVQHNS { get; set; }

        public List<string> changeList { get; set; }
        public string newName { get; set; }

        public int PHAN { get; set; }
        public int LOAI { get; set; }
        public int CAP { get; set; }
        public string MA_CHI_TIEU_CHA { get; set; }
        public string TEN_CHI_TIEU { get; set; }
        public string NOI_DUNG { get; set; }
        public string NOIDUNG { get; set; }
        public string NOI_DUNG_THU { get; set; }
        public string NOI_DUNG_CHI { get; set; }

        public string TENCHITIEU { get; set; }

        public string CHI_TIEU { get; set; }
        public string TEN_CHITIEU { get; set; }


        public string isPHAN { get; set; }

        
    }
    public class SumPara
    {
        public string DSDVQHNS { get; set; }
        public string NAM_BC { get; set; }
    }
}
