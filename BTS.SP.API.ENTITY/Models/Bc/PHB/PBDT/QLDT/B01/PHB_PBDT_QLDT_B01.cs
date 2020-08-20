using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.PBDT.QLDT
{
    [Table("PHB_PBDT_QLDT_B01")]
    public class PHB_PBDT_QLDT_B01 : DataInfoEntity
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

        [Column("MA_DBHC")]
        [StringLength(150)]
        public string MA_DBHC { get; set; }

        [Column("NAM")]
        public int NAM { get; set; }

        [Column("THANG")]
        public int THANG { get; set; }
    }
}
