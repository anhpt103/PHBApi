using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_B01_BSTT_2
{   
        [Table("PHB_B01_BSTT_2_TEMPLATE")]
        public class PHB_B01_BSTT_2_TEMPLATE : BaseEntity
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

            [Column("IS_BOLD")]
            public int IS_BOLD { get; set; }

            [Column("IS_ITALIC")]
            public int IS_ITALIC { get; set; }

        public string XML_PARENT_FIELD_NAME { get; set; }

    }
}
