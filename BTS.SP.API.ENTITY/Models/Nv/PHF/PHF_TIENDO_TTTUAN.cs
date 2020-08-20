using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_TIENDO_TTTUAN")]
    public class PHF_TIENDO_TTTUAN : DataInfoEntityPHF
    {
        [Required]
        [Column("MA_PHIEU")]
        [StringLength(50)]
        public string MA_PHIEU { get; set; }

        [Column("MA_DOITUONG")]
        [Description("Mã đối tượng")]
        [StringLength(50)]
        public string MA_DOITUONG { get; set; }

        [Column("TUNGAY")]
        [Description("Từ ngày")]
        public DateTime? TUNGAY { get; set; }

        [Column("DENNGAY")]
        [Description("Đến ngày")]
        public DateTime? DENNGAY { get; set; }

        [Column("MAPHONGBAN")]
        [Description("Mã phòng ban")]
        [StringLength(50)]
        public string MAPHONGBAN { get; set; }

        [Column("NOIDUNG")]
        [Description("Nội dung")]
        [StringLength(500)]
        public string NOIDUNG { get; set; }

        [Column("NAM")]
        [Description("Năm")]
        public int NAM { get; set; }
    }
}
