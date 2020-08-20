using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BM_09_THONGKE_CQHC")]
    public class PHF_BM_09_THONGKE_CQHC: DataInfoEntityPHF
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

        [Column("MASO_THUE")]
        [StringLength(500)]
        [Description("Mã số thuế")]
        public string MASO_THUE { get; set; }

        [Column("CQ_THUE_QUANLY")]
        [StringLength(1000)]
        [Description("Cơ quan thuế quản lý")]
        public string CQ_THUE_QUANLY { get; set; }

        [Column("NOIDUNG_NGHIVAN")]
        [StringLength(1000)]
        [Description("Nội dung nghi vấn")]
        public string NOIDUNG_NGHIVAN { get; set; }


    }
}
