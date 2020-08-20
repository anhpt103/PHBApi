using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Au.PHF
{
    [Table("PHF_AU_NGUOIDUNG")]
    public class PHF_AU_NGUOIDUNG : DataInfoEntityPHF
    {
        [Column("USERNAME")]
        [StringLength(30)]
        [Required]
        public string USERNAME { get; set; }

        [Column("PASSWORD")]
        [StringLength(500)]
        [Description("Mật khẩu")]
        [Required]
        public string PASSWORD { get; set; }

        [Column("FULLNAME")]
        [StringLength(500)]
        [Description("Họ tên")]
        [Required]
        public string FULLNAME { get; set; }

        [Column("EMAIL")]
        [StringLength(500)]
        [Description("Email")]
        public string EMAIL { get; set; }

        [Column("PHONE")]
        [StringLength(20)]
        [Description("SĐT")]
        public string PHONE { get; set; }

        [Column("TRANG_THAI")]
        [Description("Trạng thái sử dụng (1: Đang sử dụng; 0: Không sử dụng) ")]
        public int TRANG_THAI { get; set; }

        [Column("MAPHONGBAN")]
        [StringLength(500)]
        [Description("DANH SÁCH MÃ PHÒNG BAN")]
        public string MAPHONGBAN { get; set; }

        [Column("ISADMIN")]
        public int? IsAdmin { get; set; }

        [Column("ISFIRSTLOGIN")]
        public int? IsFirstLogin { get; set; }

        [Column("NUMBERWRONGLOGIN")]
        public int? NumberWrongLogin { get; set; }

        [Column("LASTTIMEWRONGLOGIN")]
        public DateTime? LastTimeWrongLogin { get; set; }

        [Column("ISLOCK")]
        public int? IsLock { get; set; }

        [Column("TIMELOCK")]
        public DateTime? TimeLock { get; set; }

        [Column("LASTTIMECHANGEPASSWORD")]
        public DateTime? LastTimeChangePassword { get; set; }
    }
}
