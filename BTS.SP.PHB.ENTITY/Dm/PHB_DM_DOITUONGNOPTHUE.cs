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
    [Table("TCS_DM_DOITUONGNOPTHUE")]
    public class PHB_DM_DOITUONGNOPTHUE : BaseEntity
    {
        [Column("MA")]
        [Required]
        [StringLength(20)]
        public string MA { get; set; }

        [Column("TEN")]
        [StringLength(500)]
        public string TEN { get; set; }

        [Column("CQTC_MA")]
        [StringLength(20)]
        [Description("Mã cơ quan tài chính")]
        public string CQTC_MA { get; set; }

        [Column("DIACHI")]
        [StringLength(500)]
        [Description("Địa chỉ")]
        public string DIACHI { get; set; }

        [Column("CAP")]
        [Description("Cấp")]
        public int CAP { get; set; }

        [Column("CHUONG")]
        [StringLength(3)]
        [Description("Chương")]
        public string CHUONG { get; set; }

        [Column("LOAI")]
        [StringLength(3)]
        [Description("Loại")]
        public string LOAI { get; set; }

        [Column("KHOAN")]
        [StringLength(3)]
        [Description("Khoản")]
        public string KHOAN { get; set; }

        [Column("TRANG_THAI")]
        [StringLength(1)]
        [DefaultValue("A")]
        public string TRANG_THAI { get; set; }
    }
}
