using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Au.PHF
{
    [Table("PHF_NHATKY_HETHONG")]
    public class PHF_NHATKY_HETHONG : DataInfoEntityPHF
    {
        [Column("USER_NAME")]
        [StringLength(50)]
        [Required]
        public string USER_NAME { get; set; }

        [Column("ACTION")]
        [Description("Hoạt động lưu log menu")]
        [StringLength(50)]
        public string ACTION { get; set; }

        [Column("TIME_USED")]
        [Description("Thời gian người dùng sử dụng")]
        public DateTime? TIME_USED { get; set; }
    }
}
