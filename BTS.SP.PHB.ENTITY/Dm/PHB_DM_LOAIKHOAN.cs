using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.PHB.ENTITY.Dm
{
    [Table("PHB_DM_LOAIKHOAN")]
    public class PHB_DM_LOAIKHOAN:BaseEntity
    {
        [Column("MA")]
        [Required]
        [StringLength(15)]
        public string MA { get; set; }

        [Column("TEN")]
        [Required]
        [StringLength(250)]
        public string TEN { get; set; }

        [Column("MA_CHA")]
        [StringLength(15)]
        public string MA_CHA { get; set; }

        [Column("CAP")]
        public int CAP { get; set; }

        [Column("LACHITIET")]
        public int LACHITIET { get; set; }

        [Column("TRANG_THAI")]
        [StringLength(1)]
        [Required]
        public string TRANG_THAI { get; set; }
    }
}
