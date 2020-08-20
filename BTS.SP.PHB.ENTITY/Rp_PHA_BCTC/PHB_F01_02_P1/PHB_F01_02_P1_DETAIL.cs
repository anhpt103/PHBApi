using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1
{
    [Table("PHB_F01_02_P1_DETAIL")]
    public class PHB_F01_02_P1_DETAIL : BaseEntity
    {
        [Column("PHB_F01_02_P1_REFID")]
        [Required]
        [Description("RefID Guid ID trong  PHB_F01_02_P1")]
        [StringLength(50)]
        public string PHB_F01_02_P1_REFID { get; set; }

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

        [Column("MA_LOAI")]
        [StringLength(10)]
        public string MA_LOAI { get; set; }

        [Column("MA_KHOAN")]
        [StringLength(10)]
        public string MA_KHOAN { get; set; }

        [Column("NN_LK")]
        [StringLength(10)]
        public string NN_LK { get; set; }

        [Column("GIA_TRI")]
        public decimal GIA_TRI { get; set; }

        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }

        [Column("STT")]
        [StringLength(5)]
        public string STT { get; set; }

        [Column("MA_NGUON")]
        [StringLength(10)]
        public string MA_NGUON { get; set; }
    }
}
