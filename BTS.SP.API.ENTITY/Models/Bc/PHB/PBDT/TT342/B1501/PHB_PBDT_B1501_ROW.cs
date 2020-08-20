using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.PBDT.B1501
{
    [Table("PHB_PBDT_B1501_ROW")]
    public class PHB_PBDT_B1501_ROW : DataInfoEntity
    {
        [Column("PHB_PBDT_B1501_REFID")]
        [Required]
        [StringLength(50)]
        public string PHB_PBDT_B1501_REFID { get; set; }

        [StringLength(50)]
        public string ROW_REFID { get; set; }

        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }

        [Column("STT")]
        [StringLength(5)]
        public string STT { get; set; }

        [Column("MA_SO")]
        [StringLength(5)]
        public string MA_SO { get; set; }

        [Column("MA_CHA")]
        [StringLength(5)]
        public string MA_CHA { get; set; }

        [Column("CHI_TIEU")]
        [Required]
        [StringLength(1000)]
        public string CHI_TIEU { get; set; }

        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Column("IS_OPTIONAL")]
        public int IS_OPTIONAL { get; set; }
    }
}
