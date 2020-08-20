using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B04_BCTC
{
    [Table("PHA_B04_BCTC_DETAIL")]
    public class PHA_B04_BCTC_DETAIL : BaseEntity
    {
        [Column("PHA_B04_BCTC_REFID")]
        [Required]
        [StringLength(50)]
        public string PHA_B04_BCTC_REFID { get; set; }

        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }

        [Column("STT")]
        [StringLength(5)]
        public string STT { get; set; }

        [Column("CHI_TIEU")]
        [Required]
        [StringLength(250)]
        public string CHI_TIEU { get; set; }

        [Column("MA_SO")]
        [StringLength(250)]
        public string MA_SO { get; set; }

        [Column("MA_SO_CHA")]
        [StringLength(250)]
        public string MA_SO_CHA { get; set; }

        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Column("SO_CUOI_NAM")]
        public decimal? SO_CUOI_NAM { get; set; }

        [Column("SO_DAU_NAM")]
        public decimal? SO_DAU_NAM { get; set; }

        public decimal? TONG_CONG { get; set; }

        public decimal? TSCD_HUU_HINH { get; set; }

        public decimal? TSCD_VO_HINH { get; set; }

        public decimal? NGUON_VON_KD { get; set; }

        public decimal? CHENH_LECH_TY_GIA { get; set; }

        public decimal? THANG_DU_LUY_KE { get; set; }

        public decimal? CAC_QUY { get; set; }

        public decimal? CAI_CACH_TIEN_LUON { get; set; }

        public decimal? KHAC { get; set; }

        public decimal? CONG { get; set; }

        public decimal? NAM_NAY { get; set; }

        public decimal? NAM_TRUOC { get; set; }
    }
}
