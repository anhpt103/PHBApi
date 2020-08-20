using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.PBDT.QLDT
{
    [Table("PHB_PBDT_QLDT_B03_DETAIL")]
    public class PHB_PBDT_QLDT_B03_DETAIL : BaseEntity
    {
        [Column("PHB_PBDT_QLDT_B03_REFID")]
        [Required]
        [StringLength(50)]
        public string PHB_PBDT_QLDT_B03_REFID { get; set; }

        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }

        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Column("IS_OPTIONAL")]
        public int IS_OPTIONAL { get; set; }


        [StringLength(5)]
        public string STT { get; set; }

        [StringLength(500)]
        public string HO_TEN { get; set; }

        [StringLength(500)]
        public string CHUC_DANH { get; set; }

        public decimal? HE_SO_LUONG { get; set; }
        public decimal? PC_KV { get; set; }
        public decimal? PC_CV { get; set; }
        public decimal? PC_CONGVU { get; set; }
        public decimal? PC_LOAIXA { get; set; }
        public decimal? PC_KN { get; set; }
        public decimal? PC_LAUNAM { get; set; }
        public decimal? PC_THUHUT { get; set; }
        public decimal? TONG_HESO { get; set; }

        public decimal? TONG_CONG { get; set; }

        public decimal? LOAI_TRU_BHXH { get; set; }
        public decimal? LOAI_TRU_BHYT { get; set; }
        public decimal? LOAI_TRU_CONG { get; set; }

        public decimal? TONG_CONG2 { get; set; }
    }
}
