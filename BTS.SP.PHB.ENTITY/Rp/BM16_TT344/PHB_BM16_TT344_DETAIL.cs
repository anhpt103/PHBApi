using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.BM16_TT344
{
    [Table("PHB_BM16_TT344_DETAIL")]
    public class PHB_BM16_TT344_DETAIL : BaseEntity
    {
        [Column("PHB_BM16_TT344_REFID")]
        [Required]
        [StringLength(50)]
        public string PHB_BM16_TT344_REFID { get; set; }

        [Column("CHUONG")]
        [StringLength(50)]
        [Required]
        public string CHUONG { get; set; }

        [Column("LOAI")]
        [StringLength(50)]
        [Required]
        public string LOAI { get; set; }

        [Column("KHOAN")]
        [StringLength(50)]
        [Required]
        public string KHOAN { get; set; }

        [Column("MUC")]
        [StringLength(50)]
        [Required]
        public string MUC { get; set; }

        [Column("TIEUMUC")]
        [StringLength(50)]
        [Required]
        public string TIEUMUC { get; set; }

        [Column("SCTTU")]
        [StringLength(50)]
        public string SCTTU { get; set; }

        [Column("SOTIENTU")]
        [Required]
        public decimal SOTIENTU { get; set; }

        [Column("SOTIENTT")]
        [Required]
        public decimal SOTIENTT { get; set; }

        [Column("MA_KBNN")]
        [StringLength(50)]
        public string MA_KBNN { get; set; }

    }
}
