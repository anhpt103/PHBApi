using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Sys
{
    [Table("PHB_SYS_LOG_CHUCNANG")]
    public class PHB_SYS_LOG_CHUCNANG : BaseEntity
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
