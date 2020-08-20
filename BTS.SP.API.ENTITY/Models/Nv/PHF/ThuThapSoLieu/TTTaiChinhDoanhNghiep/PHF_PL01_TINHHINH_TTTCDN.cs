using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.ThuThapSoLieu.TTTaiChinhDoanhNghiep
{
    [Table("PHF_PL01_TINHHINH_TTTCDN")]
    public class PHF_PL01_TINHHINH_TTTCDN : DataInfoEntityPHF
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

        [Column("DONVI")]
        [StringLength(500)]
        [Description("Đơn vị")]
        public string DONVI { get; set; }

        [Column("TAISAN_CODINH_DAUNAM")]
        [Description("Tài sản cố định đầu năm")]
        public decimal? TAISAN_CODINH_DAUNAM { get; set; }

        [Column("TAISAN_CODINH_CUOINAM")]
        [Description("Tài sản cố định cuối năm")]
        public decimal? TAISAN_CODINH_CUOINAM { get; set; }

        [Column("DAUTU_NGANHAN")]
        [Description("Đầu tư tài chính - ngắn hạn")]
        public decimal? DAUTU_NGANHAN { get; set; }

        [Column("DAUTU_DAIHAN")]
        [Description("Đầu tư tài chính - dài hạn")]
        public decimal? DAUTU_DAIHAN { get; set; }

        [Column("NOPHAITHU_NGANHAN")]
        [Description("Nợ phải thu - ngắn hạn")]
        public decimal? NOPHAITHU_NGANHAN { get; set; }

        [Column("NOPHAITHU_DAIHAN")]
        [Description("Nợ phải thu - dài hạn")]
        public decimal? NOPHAITHU_DAIHAN { get; set; }

        [Column("HANG_TONKHO")]
        [Description("Hàng tồn kho")]
        public decimal? HANG_TONKHO { get; set; }

        [Column("CPXDCBDD")]
        [Description("CPXDCBDD")]
        public decimal? CPXDCBDD { get; set; }

        [Column("CACKHOAN_KHAC")]
        [Description("Các khoản khác")]
        public decimal? CACKHOAN_KHAC { get; set; }

        [Column("TONGCONG")]
        [Description("Tổng cộng")]
        public decimal? TONGCONG { get; set; }
    }
}
