using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.B04_TT90
{
    [Table("PHB_B04_TT90")]
    public class PHB_B04_TT90 : BaseEntity
    {
        [Column("REFID")]
        [Required]
        [StringLength(50)]
        public string REFID { get; set; }

        [Column("MA_QHNS")]
        [StringLength(20)]
        public string MA_QHNS { get; set; }

        [Column("TEN_QHNS")]
        [StringLength(200)]
        public string TEN_QHNS { get; set; }

        [Column("NAM_BC")]
        [Required]
        public int NAM_BC { get; set; }

        [Column("KY_BC")]
        public int KY_BC { get; set; }

        [Column("MA_CHUONG")]
        public string MA_CHUONG { get; set; }

        [Column("TRANG_THAI")]
        [Required]
        [Description("1: Đã duyệt | 0:chưa duyệt")]
        public int TRANG_THAI { get; set; }

        [Column("TRANG_THAI_GUI")]
        [Required]
        [Description("1: Đã gửi | 0:chưa gửi | 2:bị trả lại ")]
        public int TRANG_THAI_GUI { get; set; }

        [Column("NGAY_TAO")]
        [Required]
        public DateTime NGAY_TAO { get; set; }

        [Column("NGUOI_TAO")]
        [StringLength(150)]
        [Required]
        public string NGUOI_TAO { get; set; }

        [Column("NGAY_SUA")]
        public DateTime? NGAY_SUA { get; set; }

        [Column("NGUOI_SUA")]
        [StringLength(150)]
        public string NGUOI_SUA { get; set; }
    }
}
