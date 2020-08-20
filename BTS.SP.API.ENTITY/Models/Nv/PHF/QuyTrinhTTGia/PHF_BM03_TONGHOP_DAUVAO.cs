using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.QuyTrinhTTGia
{
    [Table("PHF_BM03_TONGHOP_DAUVAO")]
    public class PHF_BM03_TONGHOP_DAUVAO : DataInfoEntityPHF
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

        [Column("SANLUONG_NHAPKHO")]
        [Description("Sản lượng nhập kho")]
        public int? SANLUONG_NHAPKHO { get; set; }

        [Column("GIACIF_GIATRI")]
        [Description("Giá CIF - Giá trị")]
        public decimal? GIACIF_GIATRI { get; set; }

        [Column("GIACIF_TYLE")]
        [Description("Giá CIF - Tỷ lệ")]
        public decimal? GIACIF_TYLE { get; set; }

        [Column("THUE_NHAPKHAU_GIATRI")]
        [Description("Thuế nhập khẩu - Giá trị")]
        public decimal? THUE_NHAPKHAU_GIATRI { get; set; }

        [Column("THUE_NHAPKHAU_TYLE")]
        [Description("Thuế nhập khẩu - Tỷ lệ")]
        public decimal? THUE_NHAPKHAU_TYLE { get; set; }

        [Column("THUE_TTDB_GIATRI")]
        [Description("Thuế TTDB - Giá trị")]
        public decimal? THUE_TTDB_GIATRI { get; set; }

        [Column("THUE_TTDB_TYLE")]
        [Description("Thuế TTDB - Tỷ lệ")]
        public decimal? THUE_TTDB_TYLE { get; set; }

        [Column("MUAHANG_VANCHUYEN_GIATRI")]
        [Description("Mua hàng - Vẩn chuyển - Giá trị")]
        public decimal? MUAHANG_VANCHUYEN_GIATRI { get; set; }

        [Column("MUAHANG_VANCHUYEN_TYLE")]
        [Description("Mua hàng - Vẩn chuyển - Tỷ lệ")]
        public decimal? MUAHANG_VANCHUYEN_TYLE { get; set; }

        [Column("MUAHANG_KHAC_GIATRI")]
        [Description("Mua hàng - khác - Giá trị")]
        public decimal? MUAHANG_KHAC_GIATRI { get; set; }

        [Column("MUAHANG_KHAC_TYLE")]
        [Description("Mua hàng - khác - Tỷ lệ")]
        public decimal? MUAHANG_KHAC_TYLE { get; set; }
    }
}
