using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107
{
    [Table("BC04_BCTC_TT107_TEMPLATE")]
    public class BC04_BCTC_TT107_TEMPLATE : BaseEntity
    {
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

        [Column("PHAN")]
        public int? PHAN { get; set; }

        [Column("SAP_XEP")]
        public int? SAP_XEP { get; set; }

        [Column("IS_BOLD")]
        public int? IS_BOLD { get; set; }

        //Dùng để làm động cột
        [Column("STT_CHITIEU")]
        public int? STT_CHITIEU { get; set; }
    }
}
