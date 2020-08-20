using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.PHB_BM14TT134
{
    [Table("KEKHAICHUNGTUDETAIL")]
    public class KEKHAICHUNGTUDETAIL : BaseEntity
    {
        [Column("KEKHAICHUNGTUREFID")]
        [Required]
        [StringLength(50)]
        public string KEKHAICHUNGTUREFID { get; set; }


        [Column("SO_CTU")]
        [StringLength(50)]
        public string SO_CTU { get; set; }

        [Column("NGAY_THANG")]
        [Required]
        public DateTime NGAY_THANG { get; set; }

        [Column("NOIDUNG")]
        [Required]
        public string NOIDUNG { get; set; }

        [Column("SO_TIEN")]
        [Required]
        public decimal SO_TIEN { get; set; }
    }
}
