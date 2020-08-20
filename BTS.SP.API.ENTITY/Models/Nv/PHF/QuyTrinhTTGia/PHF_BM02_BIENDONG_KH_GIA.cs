using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.QuyTrinhTTGia
{
    [Table("PHF_BM02_BIENDONG_KH_GIA")]
    public class PHF_BM02_BIENDONG_KH_GIA : DataInfoEntityPHF
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

        [Column("TEN_SANPHAM")]
        [StringLength(500)]
        [Description("Tên sản phẩm")]
        public string TEN_SANPHAM { get; set; }

        [Column("DIEN_GIAI")]
        [StringLength(500)]
        [Description("Diễn giải")]
        public string DIEN_GIAI { get; set; }
    }
}
