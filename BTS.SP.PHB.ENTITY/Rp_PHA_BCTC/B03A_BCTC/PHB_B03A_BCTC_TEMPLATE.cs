using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B03A_BCTC
{   
        [Table("PHB_B03A_BCTC_TEMPLATE")]
        public class PHB_B03A_BCTC_TEMPLATE : BaseEntity
    {
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

    }
}
