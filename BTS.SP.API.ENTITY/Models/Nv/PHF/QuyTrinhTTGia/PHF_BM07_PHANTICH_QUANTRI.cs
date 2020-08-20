using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.QuyTrinhTTGia
{
    [Table("PHF_BM07_PHANTICH_QUANTRI")]
    public class PHF_BM07_PHANTICH_QUANTRI : DataInfoEntityPHF
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

        [Column("DIENGIAI")]
        [StringLength(500)]
        [Description("Diễn giải")]
        public string DIENGIAI { get; set; }

        [Column("DONVI_TINH")]
        [Description("Đơn vị tính")]
        public int? DONVI_TINH { get; set; }

        [Column("TITLE_BIENDONGGIA_NGAY")]
        [Description("Title Biến động giá - Ngày")]
        public string TITLE_BIENDONGGIA_NGAY { get; set; }

        [Column("BIENDONGGIA_NGAY")]
        [Description("Biến động giá - Ngày")]
        public decimal? BIENDONGGIA_NGAY { get; set; }

        [Column("TRUNGGIAN_SANXUAT")]
        [Description("Trung gian - Định mức sản xuất")]
        public decimal? TRUNGGIAN_SANXUAT { get; set; }

        [Column("SANPHAM_TRUNGGIAN_DINHMUC")]
        [Description("Sản phẩm - Trung gian - Định mức sản xuất")]
        public decimal? SANPHAM_TRUNGGIAN_DINHMUC { get; set; }

        [Column("SANPHAM_SANXUAT_DINHMUC")]
        [Description("Sản phẩm - Trung gian - Định mức sản xuất")]
        public decimal? SANPHAM_SANXUAT_DINHMUC { get; set; }

        [Column("MUCBIENDONG_TYLE")]
        [Description("Mức biến động trên 1 đơn vị")]
        public decimal? MUCBIENDONG_TYLE { get; set; }
    }
}
