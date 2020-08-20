using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BTS.SP.API.ENTITY.Models.Nv.PHF.XLKN
{
    [Table("PHF_PL03_XLKN_TEMPLATE")]
    public class PHF_PL03_XLKN_TEMPLATE : DataInfoEntityPHF
    {
        [Column("STT")]
        [StringLength(50)]
        [Description("Số thứ tự")]
        public string STT { get; set; }

        [Column("STT_TIEUDE")]
        [Description("Số thứ tự tiêu đề")]
        [StringLength(50)]
        public string STT_TIEUDE { get; set; }

        [Column("STT_CHA")]
        [StringLength(50)]
        [Description("Số thứ tự cha")]
        public string STT_CHA { get; set; }

        [Column("DONVI_DUOC_THANHTRA")]
        [StringLength(100)]
        [Description("Đơn vị được thanh tra, kiểm tra")]
        public string DONVI_DUOC_THANHTRA { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }
    }
}
