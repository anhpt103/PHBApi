using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BIEU04_TAICHINH_TCDN")]
    public class PHF_BIEU04_TAICHINH_TCDN : DataInfoEntityPHF
    {
        [Column("STT")]
        [Description("Số thứ tự")]
        public int STT { get; set; }

        [Column("STT_TIEUDE")]
        [Description("Số thứ tự tiêu đề")]
        [StringLength(5)]
        public string STT_TIEUDE { get; set; }

        [Column("STT_CHA")]
        [Description("Số thứ tự cha")]
        public int STT_CHA { get; set; }

        [Column("MA_FILE")]
        [StringLength(200)]
        [Description("Mã file Template")]
        public string MA_FILE { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("TEN_DONVI")]
        [StringLength(1000)]
        [Description("Tên đơn vị")]
        public string TEN_DONVI { get; set; }

        [Column("NO_PHAITRA")]
        [StringLength(1000)]
        [Description("Nợ phải trả")]
        public string NO_PHAITRA { get; set; }

        [Column("NGUONVON_CHUSOHUU")]
        [StringLength(1000)]
        [Description("Nguồn vốn chủ sở hữu")]
        public string NGUONVON_CHUSOHUU { get; set; }

        [Column("DOANHTHU")]
        [StringLength(1000)]
        [Description("Doanh thu")]
        public string DOANHTHU { get; set; }

        [Column("LOINHUAN_TRUOCTHUE")]
        [StringLength(1000)]
        [Description("Lợi nhuận trước thuế")]
        public string LOINHUAN_TRUOCTHUE { get; set; }

        [Column("VON_DIEULE")]
        [StringLength(1000)]
        [Description("Vốn điều lệ")]
        public string VON_DIEULE { get; set; }

        [Column("VONGOP_TCT")]
        [StringLength(1000)]
        [Description("Vốn góp của TCT")]
        public string VONGOP_TCT { get; set; }

        [Column("TYLE_VONGOP")]
        [StringLength(1000)]
        [Description("Tỷ lệ vốn góp")]
        public string TYLE_VONGOP { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }
    }
}
