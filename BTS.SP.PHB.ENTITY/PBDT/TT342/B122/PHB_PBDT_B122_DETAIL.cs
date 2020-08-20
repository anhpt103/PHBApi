using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.PBDT.B122
{
    [Table("PHB_PBDT_B122_DETAIL")]
    public class PHB_PBDT_B122_DETAIL : BaseEntity
    {
        [Column("PHB_PBDT_B122_REFID")]
        [Required]
        [StringLength(50)]
        public string PHB_PBDT_B122_REFID { get; set; }

        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }

        [Column("STT")]
        [StringLength(5)]
        public string STT { get; set; }

        [Column("CHI_TIEU")]
        [Required]
        [StringLength(1000)]
        public string CHI_TIEU { get; set; }

        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }
        [Column("NAM_THUC_HIEN")]
        public decimal? NAM_THUC_HIEN { get; set; }

        [Column("NAM_HIEN_HANH_DU_TOAN")]
        public decimal? NAM_HIEN_HANH_DU_TOAN { get; set; }

        [Column("NAM_HIEN_HANH_UOC_THUC_HIEN")]
        public decimal? NAM_HIEN_HANH_UOC_THUC_HIEN { get; set; }

        [Column("NAM_KE_HOACH")]
        public decimal? NAM_KE_HOACH { get; set; }

        [Column("IS_OPTIONAL")]
        public int IS_OPTIONAL { get; set; }
    }
}
