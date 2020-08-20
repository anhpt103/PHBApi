using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_QD_GIAOTHUCHIEN_THANHTRA")]
    public class PHF_QD_GIAOTHUCHIEN_THANHTRA : DataInfoEntityPHF
    {
        [Column("SO_DUTHAO")]
        [StringLength(50)]
        public string SO_DUTHAO { get; set; }

        [Column("NOIDUNG_DUTHAO")]
        [StringLength(500)]
        public string NOIDUNG_DUTHAO { get; set; }

        [Column("NGAY_DUTHAO")]
        public DateTime? NGAY_DUTHAO { get; set; }

        [Required]
        [Column("SO_QUYETDINH")]
        [StringLength(50)]
        public string SO_QUYETDINH { get; set; }

        [Column("NAM_THANHTRA")]
        public int NAM_THANHTRA { get; set; }

        [Column("NGAY_QUYETDINH")]
        public DateTime NGAY_QUYETDINH { get; set; }

        [Column("QD_GIAOTHUCHIEN_THANHTRA")]
        [StringLength(50)]
        public string QD_GIAOTHUCHIEN_THANHTRA { get; set; }

        [Column("DOT_THANHTRA")]
        [StringLength(100)]
        public string DOT_THANHTRA { get; set; }

        [Column("THONGTIN_NGUOIKY")]
        public string THONGTIN_NGUOIKY { get; set; }

        [Column("FILE_DINHKEM")]
        [StringLength(1000)]
        public string FILE_DINHKEM { get; set; }

        [Column("MA_PHONGBAN")]
        [Description("Mã phòng ban")]
        [StringLength(50)]
        public string MA_PHONGBAN { get; set; }

        [Column("LOAI")]
        [StringLength(50)]
        public string LOAI { get; set; }
    }
}
