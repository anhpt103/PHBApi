
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.B01A_TT137
{
    public class PHB_B01A_TT137_DETAIL : BaseEntity
    {
        [Required]
        [Description("Guid ID trong  PHB_B01A_TT137")]
        [StringLength(50)]
        public string PHB_B01A_TT137_REFID { get; set; }
        [Description("STT")]
        public int STT { get; set; }
        [Description("Số thứ tự chỉ tiêu")]
        [StringLength(50)]
        public string STT_CHI_TIEU { get; set; }
        [Description("Mã chỉ tiêu")]
        [StringLength(50)]
        public string MA_CHI_TIEU { get; set; }
        [Description("Tên chỉ tiêu")]
        [StringLength(1000)]
        public string TEN_CHI_TIEU { get; set; }
        [Description("Số tiền dự toán")]
        public double DU_TOAN { get; set; }
        public double DT_SXD { get; set; }
        public double DT_CL { get; set; }
        [Description("Số tiền năm trước, năm liền kề")]
        public double THUC_HIEN { get; set; }
        public double TH_SXD { get; set; }
        public double TH_CL { get; set; }
    }
}
