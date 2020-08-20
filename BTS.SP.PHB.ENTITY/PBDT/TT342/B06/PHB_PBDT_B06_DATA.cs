using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.PBDT.B06
{
    [Table("PHB_PBDT_B06_DATA")]
    public class PHB_PBDT_B06_DATA : BaseEntity
    {

        [Column("DETAIL_REFID")]
        [Required]
        [StringLength(50)]
        public string DETAIL_REFID { get; set; }

        [Column("DONVI_REFID")]
        [Required]
        [StringLength(50)]
        public string DONVI_REFID { get; set; }

        public decimal? UOC_THUC_HIEN { get; set; }
        public decimal? DU_TOAN { get; set; }

    }
}
