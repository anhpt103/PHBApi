using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.B03_TT90
{
    [Table("PHB_B03_TT90_TEMPLATE")]
    public class PHB_B03_TT90_TEMPLATE : BaseEntity
    {
        [Column("STT_CHI_TIEU")]
        [Description("STT chỉ tiêu báo cáo")]
        [StringLength(15)]
        public string STT_CHI_TIEU { get; set; }

        [Required]
        [Column("MA_CHI_TIEU")]
        [Description("Mã chỉ tiêu báo cáo")]
        [StringLength(15)]
        public string MA_CHI_TIEU { get; set; }

        [Required]
        [Column("MA_CHI_TIEU_CHA")]
        [Description("Mã chỉ tiêu cha báo cáo")]
        [StringLength(15)]
        public string MA_CHI_TIEU_CHA { get; set; }

        [Required]
        [Column("TEN_CHI_TIEU")]
        [Description("Tên chỉ tiêu báo cáo")]
        [StringLength(250)]
        public string TEN_CHI_TIEU { get; set; }

        [Column("SAPXEP")]
        public int? SAPXEP { get; set; }

        [Column("INDAM")]
        public int? INDAM { get; set; }

        [Column("INNGHIENG")]
        public int? INNGHIENG { get; set; }
    }
}
