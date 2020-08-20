using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107
{
    [Table("BC04_BCTC_TT107_DETAILS")]
    public class BC04_BCTC_TT107_DETAILS : BaseEntity
    {
        [Column("BC04_BCTC_TT107_REFID")]
        [Required]
        [Description("RefID Guid ID trong  BC04_BCTC_TT107")]
        [StringLength(50)]
        public string BC04_BCTC_TT107_REFID { get; set; }

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

        [Column("AMOUNT1")]
        public decimal AMOUNT1 { get; set; }

        [Column("AMOUNT2")]
        public decimal AMOUNT2 { get; set; }

        [Column("AMOUNT3")]
        public decimal AMOUNT3 { get; set; }

        [Column("AMOUNT4")]
        public decimal AMOUNT4 { get; set; }

        [Column("AMOUNT5")]
        public decimal AMOUNT5 { get; set; }

        [Column("AMOUNT6")]
        public decimal AMOUNT6 { get; set; }

        [Column("AMOUNT7")]
        public decimal AMOUNT7 { get; set; }

        [Column("AMOUNT8")]
        public decimal AMOUNT8 { get; set; }

        [Column("AMOUNT9")]
        public decimal AMOUNT9 { get; set; }

        [Column("AMOUNT10")]
        public decimal AMOUNT10 { get; set; }

        [Column("AMOUNT11")]
        public decimal AMOUNT11 { get; set; }

        [Column("AMOUNT12")]
        public decimal AMOUNT12 { get; set; }

        [Column("PHAN")]
        public int PHAN { get; set; }
    }
}
