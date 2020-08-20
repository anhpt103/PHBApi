using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.B02_TT137
{
    [Table("PHB_B02_TT137")]
    public class PHB_B02_TT137: DataInfoEntity
    {
        [Column("REFID")]
        [Required]
        [StringLength(50)]
        public string REFID { get; set; }

        [Column("TRANG_THAI")]
        [Required]
        [Description("1: Đã duyệt | 0:chưa duyệt")]
        public int TRANG_THAI { get; set; }

        [Column("TRANG_THAI_GUI")]
        [Required]
        [Description("1: Đã gửi | 0:chưa gửi | 2:bị trả lại ")]
        public int TRANG_THAI_GUI { get; set; }

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

        [Column("MA_QHNS")]
        [StringLength(150)]
        public string MA_QHNS { get; set; }

        [Column("TEN_QHNS")]
        [StringLength(150)]
        public string TEN_QHNS { get; set; }

        [Column("DON_VI_DT")]
        [StringLength(150)]
        public string DON_VI_DT { get; set; }

        [Column("CAP_DU_TOAN")]
        [StringLength(150)]
        public string CAP_DU_TOAN { get; set; }

        [Column("MA_CHUONG")]
        public string MA_CHUONG { get; set; }
        [Column("NAM_BC")]
        public int NAM_BC { get; set; }
        [Column("KY_BC")]
        public int KY_BC { get; set; }
    }
}
