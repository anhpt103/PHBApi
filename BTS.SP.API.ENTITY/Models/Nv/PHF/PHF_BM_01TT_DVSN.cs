using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BM_01TT_DVSN")]
    public class PHF_BM_01TT_DVSN : DataInfoEntityPHF
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

        [Column("NOIDUNG_CHI")]
        [StringLength(1000)]
        [Description("Nội dung chi")]
        public string NOIDUNG_CHI { get; set; }

        [Column("THUCTHI_NAM")]
        [Description("Thực Thi năm")]
        public decimal? THUCTHI_NAM { get; set; }

        [Column("QUYETTOAN_CHI_NAM")]
        [Description("Quyết toán chi năm")]
        public decimal? QUYETTOAN_CHI_NAM { get; set; }

        [Column("THUCTHI_DUOCGIAO")]
        [Description("Thực thi được giao")]
        public decimal? THUCTHI_DUOCGIAO { get; set; }

        [Column("GHICHU")]
        [StringLength(2000)]
        [Description("Ghi chú")]
        public string GHICHU { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }
    }
}
