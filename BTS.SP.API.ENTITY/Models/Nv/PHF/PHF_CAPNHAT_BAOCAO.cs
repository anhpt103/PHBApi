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
    [Table("PHF_CAPNHAT_BAOCAO")]
    public class PHF_CAPNHAT_BAOCAO : DataInfoEntityPHF
    {
        [Required]
        [Column("MA_BAOCAO")]
        [StringLength(50)]
        public string MA_BAOCAO { get; set; }

        [Column("MA_PHIEU")]
        [StringLength(50)]
        public string MA_PHIEU { get; set; }

        [Column("MA_DOITUONG")]
        [Description("Mã đối tượng")]
        [StringLength(50)]
        public string MA_DOITUONG { get; set; }

        [Column("LOAI_BAOCAO")]
        [Description("Loại báo cáo")]
        [StringLength(50)]
        public string LOAI_BAOCAO { get; set; }

        [Column("TUNGAY")]
        [Description("Từ ngày")]
        public DateTime? TUNGAY { get; set; }

        [Column("DENNGAY")]
        [Description("Đến ngày")]
        public DateTime? DENNGAY { get; set; }

        [Column("MAPHONG")]
        [Description("Mã phòng")]
        [StringLength(50)]
        public string MAPHONG { get; set; }

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

        [Column("DUKIEN")]
        [Description("Dự kiến")]
        [StringLength(500)]
        public string DUKIEN { get; set; }

        [Column("DIADIEM")]
        [Description("Địa điểm")]
        [StringLength(300)]
        public string DIADIEM { get; set; }

        [Column("TUAN")]
        [Description("Tuần")]
        public int? TUAN { get; set; }

    }
}
