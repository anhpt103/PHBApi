using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.BIEU2B
{
    [Table("PHB_BIEU2B_TEMPLATE")]
    public class PHB_BIEU2B_TEMPLATE:BaseEntity
    {
        [Description("Số thứ tự chỉ tiêu")]
        [StringLength(50)]
        public string STT_CHI_TIEU { get; set; }


        [Description("Mã chỉ tiêu")]
        [StringLength(50)]
        public string MA_CHI_TIEU { get; set; }

        [Description("Tên chỉ tiêu")]
        [StringLength(255)]
        public string TEN_CHI_TIEU { get; set; }

        [Description("Công thức")]
        [StringLength(255)]
        public string CONG_THUC { get; set; }

        [Description("Cấp")]
        [Required]
        public int CAP { get; set; }

        [Description("Loại")]
        public int LOAI { get; set; }

        [Description("PHAN")]
        public int PHAN { get; set; }
    }
}
