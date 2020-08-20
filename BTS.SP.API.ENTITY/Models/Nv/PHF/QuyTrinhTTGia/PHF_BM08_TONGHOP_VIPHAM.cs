using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.QuyTrinhTTGia
{
    [Table("PHF_BM08_TONGHOP_VIPHAM")]
    public class PHF_BM08_TONGHOP_VIPHAM : DataInfoEntityPHF
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

        [Column("NOIDUNG_VIPHAM")]
        [StringLength(500)]
        [Description("Nội dung vi phạm")]
        public string NOIDUNG_VIPHAM { get; set; }

        [Column("SOTIEN_PHAT")]
        [Description("Số tiền phạt")]
        public decimal? SOTIEN_PHAT { get; set; }

        [Column("BIENPHAP_KHACPHUC")]
        [StringLength(500)]
        [Description("Biện pháp khắc phục")]
        public string BIENPHAP_KHACPHUC { get; set; }

        [Column("GHICHU")]
        [StringLength(500)]
        [Description("Ghi Chú")]
        public string GHICHU { get; set; }

    }
}
