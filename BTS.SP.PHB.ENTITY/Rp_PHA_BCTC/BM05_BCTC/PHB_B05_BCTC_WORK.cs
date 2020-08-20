using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BM05_BCTC
{
    [Table("PHB_B05_BCTC_WORK")]
    public class PHB_B05_BCTC_WORK : BaseEntity
    {
        [Required]
        [Description("Guid ID trong  PHB_B05_BCTC")]
        [StringLength(100)]
        public string PHB_B05_BCTC_REFID { get; set; }

        [Column("DON_VI")]
        [StringLength(2000)]
        public string DON_VI { get; set; }

        [Column("QD_TL_SO")]
        [StringLength(20)]
        public string QD_TL_SO { get; set; }

        [Column("NGAY")]
        [StringLength(10)]
        public string NGAY { get; set; }

        [Column("THANG")]
        [StringLength(10)]
        public string THANG { get; set; }

        [Column("NAM")]
        [StringLength(10)]
        public string NAM { get; set; }

        [Column("CO_QUAN_CAP")]
        [StringLength(1000)]
        public string CO_QUAN_CAP { get; set; }

        [Column("THUOC_DV")]
        [StringLength(1000)]
        public string THUOC_DV { get; set; }

        [Column("LOAI_HINH_DV")]
        [StringLength(1000)]
        public string LOAI_HINH_DV { get; set; }

        [Column("CHU_TC")]
        [StringLength(1000)]
        public string CHU_TC { get; set; }

        [Column("NV_CHINH_DV")]
        [StringLength(1000)]
        public string NV_CHINH_DV { get; set; }

        [Column("TT_THUYETMINH")]
        [StringLength(1000)]
        public string TT_THUYETMINH { get; set; }
    }
}
