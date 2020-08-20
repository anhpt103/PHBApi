using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Au
{
    [Table("LOG_SIGNIN")]
    public class LogSignin : DataInfoEntity
    {
        [Required]
        [Column("USERNAME")]
        [StringLength(50)]
        public string Username { get; set; }

        [Column("DIACHIMAY")]
        [StringLength(100)]
        public string DiaChiMay { get; set; }

        [Column("THOIGIANTRUYCAP")]
        public DateTime ThoiGianTruyCap { get; set; }

        [Column("CHUCNANG")]
        [StringLength(500)]
        public string ChucNang { get; set; }

        [Column("CHI_TIET")]
        [StringLength(1000)]
        public string ChiTiet { get; set; }

        [Column("GHI_CHU")]
        [StringLength(1000)]
        public string GhiChu { get; set; }

        [Column("DBHC")]
        [StringLength(100)]
        public string DBHC { get; set; }

        [Column("TEN_DBHC")]
        [StringLength(500)]
        public string Ten_DBHC { get; set; }

        [Column("DBHC_CHA")]
        [StringLength(100)]
        public string DBHC_Cha { get; set; }
    }
}
