using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.PHB_BM14_TT134
{
    [Table("KEKHAICHUNGTU")]
    public class KEKHAICHUNGTU : DataInfoEntity
    {
        [Column("REFID")]
        [Required]
        [StringLength(50)] 
        public string REFID { get; set; }

        [Column("MA_XA")]
        [StringLength(50)]
        public string MA_XA { get; set; }

        [Column("MA_KTC")]
        [StringLength(50)]
        public string MA_KTC { get; set; }
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

        [Column("TONG_TIEN")]
        public decimal TONG_TIEN { get; set; }
    }
}
