using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.DmBaoCao
{
    public class ExportXmlViewModel
    {
        public int Nam { get; set; }
        public PhienBan PhienBan { get; set; }
        public string TenBC { get; set; }
        public string NguoiLapBC { get; set; }
        public DateTime NgayKy { get; set; }
        public string NguoiKy { get; set; }
        public string NguoiKiemSoat { get; set; }
    }

    public enum PhienBan
    {
        Ver_001 = 1
    }
}
