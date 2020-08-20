using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Dm
{
    [Table("PHB_DM_CANBO")]
    public class PHB_DM_CANBO : BaseEntity
    {
        [Column("MA_CAN_BO")]
        [StringLength(15)]
        [Required]
        public string MA_CAN_BO { get; set; }

        [Column("MA_NGACH_LUONG")]
        [Required]
        [StringLength(15)]
        public string MA_NGACH_LUONG { get; set; }

        [Column("TEN_CAN_BO")]
        [Required]
        [StringLength(250)]
        public string TEN_CAN_BO { get; set; }

        [Column("DIA_CHI")]
        [Required]
        [StringLength(250)]
        public string DIA_CHI { get; set; }

        [Column("CHUC_VU")]
        [Required]
        [StringLength(100)]
        public string CHUC_VU { get; set; }

        [Column("PHONG_BAN")]
        [Required]
        [StringLength(100)]
        public string PHONG_BAN { get; set; }

        [Column("HE_SO_LUONG")]
        [Description("Hệ số lương")]
        public decimal? HE_SO_LUONG { get; set; }

        [Column("GIAM_TRU")]
        [Description("Giảm trừ")]
        public decimal? GIAM_TRU { get; set; }

        [Column("DTCQ")]
        public decimal? DTCQ { get; set; }

        [Column("DTNR")]
        public decimal? DTNR { get; set; }

        [Column("DTDD")]
        public decimal? DTDD { get; set; }

        [Column("EMAIL")]
        [StringLength(100)]
        public string EMAIL { get; set; }

        [Column("GIOI_TINH")]
        [StringLength(5)]
        public string GIOI_TINH { get; set; }

        [Column("SO_CMND")]
        public int SO_CMND { get; set; }

        [Column("NGAY_CAP")]
        public Nullable<DateTime> NGAY_CAP { get; set; }

        [Column("NOI_CAP")]
        [StringLength(50)]
        public string NOI_CAP { get; set; }

        [Column("MA_SO_THUE")]
        public int MA_SO_THUE { get; set; }

        [Column("SO_TK_CANHAN")]
        public int SO_TK_CANHAN { get; set; }

        [Column("NH_MO_TK")]
        [StringLength(100)]
        public string NH_MO_TK { get; set; }

        [Column("TRANG_THAI")]
        [Required]
        [StringLength(1)]
        public string TRANG_THAI { get; set; }
    }
}
