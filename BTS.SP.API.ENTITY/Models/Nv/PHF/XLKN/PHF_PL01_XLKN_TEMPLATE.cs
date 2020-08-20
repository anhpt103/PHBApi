using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BTS.SP.API.ENTITY.Models.Nv.PHF.XLKN
{
    [Table("PHF_PL01_XLKN_TEMPLATE")]
    public class PHF_PL01_XLKN_TEMPLATE : DataInfoEntityPHF
    {
        [Column("STT")]
        [Description("Số thứ tự")]
        [StringLength(200)]
        public string STT { get; set; }

        [Column("STT_TIEUDE")]
        [Description("Số thứ tự tiêu đề")]
        [StringLength(5)]
        public string STT_TIEUDE { get; set; }

        [Column("STT_CHA")]
        [Description("Số thứ tự cha")]
        [StringLength(200)]
        public string STT_CHA { get; set; }

        [Column("TEN_DOITUONG")]
        [StringLength(500)]
        [Description("Tên đối tượng")]
        public string TEN_DOITUONG { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }


    }
}
