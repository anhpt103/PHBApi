using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BM_08_KIENNGHI_CQHC")]
    public class PHF_BM_08_KIENNGHI_CQHC : DataInfoEntityPHF
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

        [Column("TEN_DOANHNGHIEP")]
        [StringLength(1000)]
        [Description("Tên doanh nghiệp")]
        public string TEN_DOANHNGHIEP { get; set; }

        [Column("MASO_THUE")]
        [StringLength(500)]
        [Description("Mã số thuế")]
        public string MASO_THUE { get; set; }

        [Column("TRUYTHU_THUE_TNDN")]
        [Description("Truy thu, nộp bổ sung NSNN - Thuế TNDN")]
        public decimal? TRUYTHU_THUE_TNDN { get; set; }

        [Column("TRUYTHU_THUE_GTGT")]
        [Description("Truy thu, nộp bổ sung NSNN - Thuế GTGT")]
        public decimal? TRUYTHU_THUE_GTGT { get; set; }

        [Column("TRUYTHU_THUKHAC")]
        [Description("Truy thu, nộp bổ sung NSNN - Thu khác")]
        public decimal? TRUYTHU_THUKHAC { get; set; }

        [Column("TRUYTHU_CONG")]
        [Description("Truy thu, nộp bổ sung NSNN - Cộng")]
        public decimal? TRUYTHU_CONG { get; set; }

        [Column("KIENNGHI_GIAMLO")]
        [Description("Kiến nghị khác - Giảm lỗ")]
        public decimal? KIENNGHI_GIAMLO { get; set; }

        [Column("KIENNGHI_GIAMKHAU")]
        [Description("Kiến nghị khác - Giảm khấu trừ thuế GTGT")]
        public decimal? KIENNGHI_GIAMKHAU { get; set; }

        [Column("KIENNGHI_CONG")]
        [Description("Kiến nghị khác - Cộng")]
        public decimal? KIENNGHI_CONG { get; set; }

        [Column("DANOP_NSNN")]
        [Description("Đã nộp vào NSNN")]
        public decimal? DANOP_NSNN { get; set; }

        [Column("SOTHUE_NSNN")]
        [Description("Số thuế còn phải nộp vào NSNN")]
        public decimal? SOTHUE_NSNN { get; set; }

        [Column("GHI_CHU")]
        [Description("Ghi chú")]
        [StringLength(1000)]
        public string GHI_CHU { get; set; }

    }
}
