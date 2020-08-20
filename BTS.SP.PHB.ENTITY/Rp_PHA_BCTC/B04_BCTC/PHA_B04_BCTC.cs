using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B04_BCTC
{
    [Table("PHA_B04_BCTC")]
    public class PHA_B04_BCTC : BaseEntity
    {
        [Column("REFID")]
        [Required]
        [StringLength(50)]
        public string REFID { get; set; }

        [Column("TRANG_THAI")]
        [Required]
        [Description("1: Đã duyệt | 0:chưa duyệt")]
        public int TRANG_THAI { get; set; }

        [Column("NGAY_TAO")]
        public DateTime? NGAY_TAO { get; set; }

        [Column("NGUOI_TAO")]
        [StringLength(150)]
        public string NGUOI_TAO { get; set; }

        [Column("NGAY_SUA")]
        public DateTime? NGAY_SUA { get; set; }

        [Column("NGUOI_SUA")]
        [StringLength(150)]
        public string NGUOI_SUA { get; set; }

        [Column("MA_DONVI")]
        [StringLength(150)]
        public string MA_DONVI { get; set; }

        [Column("DON_VI_DT")]
        [StringLength(150)]
        public string DON_VI_DT { get; set; }

        [Column("CAP_DU_TOAN")]
        [StringLength(150)]
        public string CAP_DU_TOAN { get; set; }

        [Column("NAM")]
        [Description("1: Đã duyệt | 0:chưa duyệt")]
        public int NAM { get; set; }

        [Column("KY_BC")]
        [Required]
        [Description("0:Năm  | 101:Quý 1 | 102:Quý 2 | 103:Quý 3 | 104:Quý 4 | 201:6 tháng đầu năm | 202 : 6 tháng cuối năm")]
        public int KY_BC { get; set; }
    }
}
