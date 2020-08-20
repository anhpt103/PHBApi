using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.C_B04X
{
    public class PHB_C_B04X_DETAIL_TSCD : BaseEntity
    {
        [Column("PHB_C_B04X_REFID")]
        [Required]
        [Description("Guid ID trong PHB_C_B04X")]
        [StringLength(50)]
        public string PHB_C_B04X_REFID { get; set; }

        [StringLength(50)]
        public string STT { get; set; }

        [Column("CHI_TIEU")]
        [Description("Chỉ tiêu")]
        [Required]
        [StringLength(50)]
        public string CHI_TIEU { get; set; }

        [Column("CHI_TIEU_OLD")]
        [Description("Chỉ tiêu cũ")]
        [StringLength(50)]
        public string CHI_TIEU_OLD { get; set; }

        [StringLength(50)]
        public string DON_VI_TINH { get; set; }

        public int? DAUNAM_SL { get; set; }
        public decimal? DAUNAM_NG { get; set; }

        public int? TANG_SL { get; set; }
        public decimal? TANG_NG { get; set; }

        public int? GIAM_SL { get; set; }
        public decimal? GIAM_NG { get; set; }

        public int? CUOINAM_SL { get; set; }
        public decimal? CUOINAM_NG { get; set; }
    }
}
