using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.QuyTrinhTTGia
{
    [Table("PHF_BM06_TONGHOP_PHANTICH")]
    public class PHF_BM06_TONGHOP_PHANTICH : DataInfoEntityPHF
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

        [Column("DONVI_TINH")]
        [Description("Đơn vị tính")]
        [StringLength(50)]
        public string DONVI_TINH { get; set; }

        [Column("COT4")]
        [Description("Cột 4")]
        public decimal? COT4 { get; set; }

        [Column("COT5")]
        [Description("Cột 5")]
        public decimal? COT5 { get; set; }

        [Column("COT6")]
        [Description("Cột 6")]
        public decimal? COT6 { get; set; }

        [Column("COT7")]
        [Description("Cột 7")]
        public decimal? COT7 { get; set; }

        [Column("COT10")]
        [Description("Cột 10")]
        [StringLength(300)]
        public string COT10 { get; set; }
    }
}
