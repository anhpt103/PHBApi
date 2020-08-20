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
    [Table("PHF_TIENDO_TUAN")]
    public class PHF_TIENDO_TUAN : DataInfoEntityPHF
    {
        [Required]
        [Column("MA_BAOCAO")]
        [StringLength(50)]
        public string MA_BAOCAO { get; set; }

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

        [Column("MA_PHONGBAN")]
        [Description("Mã phòng ban")]
        [StringLength(50)]
        public string MA_PHONGBAN { get; set; }

        [Column("NOIDUNG")]
        [Description("Nội dung")]
        [StringLength(500)]
        public string NOIDUNG { get; set; }

        [Column("KETQUA")]
        [Description("Kết quả")]
        [StringLength(500)]
        public string KETQUA { get; set; }

        [Column("KHOKHAN")]
        [Description("Khó khăn")]
        [StringLength(500)]
        public string KHOKHAN { get; set; }

        [Column("NOI_LAMVIEC")]
        [Description("Nơi làm việc")]
        [StringLength(500)]
        public string NOI_LAMVIEC { get; set; }

        [Column("TUAN")]
        [Description("Tuần")]
        public int? TUAN { get; set; }

        [Column("NAM")]
        [Description("Năm")]
        public int NAM { get; set; }

        [Column("NGUOILAP")]
        [StringLength(200)]
        [Description("Người lập báo cáo")]
        public string NGUOILAP { get; set; }

        [Column("NGAYLAP")]
        [Description("Ngày lập báo cáo")]
        public DateTime? NGAYLAP { get; set; }

        [Column("QUY")]
        [Description("Mã quý")]
        public int? QUY { get; set; }

        [Column("TENQUY")]
        [Description("Tên quý")]
        [StringLength(50)]
        public string TENQUY { get; set; }
    }
}
