using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BM_07_TINHHINH_CQHC")]
    public class PHF_BM_07_TINHHINH_CQHC : DataInfoEntityPHF
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

        [Column("CHI_TIEU")]
        [StringLength(1000)]
        [Description("Chỉ tiêu")]
        public string CHI_TIEU { get; set; }

        [Column("THUCHIEN_NAM")]
        [Description("Thực hiện năm")]
        public decimal? THUCHIEN_NAM { get; set; }

        [Column("DUTOAN_PHAPLENH")]
        [Description("Dự toán pháp lệnh")]
        public decimal? DUTOAN_PHAPLENH { get; set; }

        [Column("THUCHIEN")]
        [Description("Thực hiện")]
        public decimal? THUCHIEN { get; set; }

        [Column("THUCHIEN_SO_PHAPLENH")]
        [Description("Thực hiện so với dự toán pháp lệnh")]
        public decimal? THUCHIEN_SO_PHAPLENH { get; set; }

        [Column("THUCHIEN_SO_NAM")]
        [Description("Thực hiện so với năm")]
        public decimal? THUCHIEN_SO_NAM { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }
    }
}
