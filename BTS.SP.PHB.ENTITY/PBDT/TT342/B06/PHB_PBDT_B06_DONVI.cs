using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.PBDT.B06
{
    [Table("PHB_PBDT_B06_DONVI")]
    public class PHB_PBDT_B06_DONVI : BaseEntity
    {
        public int STT { get; set; }

        [Column("PHB_PBDT_B06_REFID")]
        [Required]
        [StringLength(50)]
        public string PHB_PBDT_B06_REFID { get; set; }

        [Column("DONVI_REFID")]
        [Required]
        [StringLength(50)]
        public string DONVI_REFID { get; set; }

        [Column("TEN_DON_VI")]
        [StringLength(50)]
        public string TEN_DON_VI { get; set; }
    }
}
