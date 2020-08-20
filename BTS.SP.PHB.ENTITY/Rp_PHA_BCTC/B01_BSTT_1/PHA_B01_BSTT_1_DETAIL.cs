using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B01_BSTT_1
{
    [Table("PHA_B01_BSTT_1_DETAIL")]
    public class PHA_B01_BSTT_1_DETAIL : BaseEntity
    {
        [Column("PHA_B01_BSTT_1_REFID")]
        [Required]
        [Description("RefID Guid ID trong  PHA_B01_BSTT_1_REFID")]
        [StringLength(50)]
        public string PHA_B01_BSTT_1_REFID { get; set; }

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

        [Column("TONG_SO")]
        public decimal? TONG_SO { get; set; }

        [Column("TRONG_DVKTTG_1")]
        public decimal? TRONG_DVKTTG_1 { get; set; }

        [Column("TRONG_DVKTTG_2")]
        public decimal? TRONG_DVKTTG_2 { get; set; }

        [Column("TRONG_DVDT_CAP1")]
        public decimal? TRONG_DVDT_CAP1 { get; set; }

        [Column("NGOAI_DVDT_CAP1_CUNGTINH")]
        public decimal? NGOAI_DVDT_CAP1_CUNGTINH { get; set; }

        [Column("NGOAI_DVDT_CAP1_KHACTINH")]
        public decimal? NGOAI_DVDT_CAP1_KHACTINH { get; set; }

        [Column("NGOAI_NHA_NUOC")]
        public decimal? NGOAI_NHA_NUOC { get; set; }
    }
}
