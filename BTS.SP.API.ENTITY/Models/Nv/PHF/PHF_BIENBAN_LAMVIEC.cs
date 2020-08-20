using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BIENBAN_LAMVIEC")]
    public class PHF_BIENBAN_LAMVIEC : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(200)]
        [Description("Mã báo cáo")]
        public string MABAOCAO { get; set; }

        [Column("TENBAOCAO")]
        [StringLength(500)]
        [Description("Tên báo cáo")]
        public string TENBAOCAO { get; set; }

        [Column("NAM")]
        [Description("Năm")]
        [StringLength(50)]
        public string NAM { get; set; }

        [Column("DINHKEMFILE")]
        [StringLength(200)]
        [Description("File đính kèm")]
        public string DINHKEMFILE { get; set; }

        [Column("MAPHONGBAN")]
        [StringLength(50)]
        [Description("Mã phòng ban")]
        public string MAPHONGBAN { get; set; }

        [Column("MADOITUONG")]
        [StringLength(50)]
        [Description("Mã đối tượng")]
        public string MADOITUONG { get; set; }

        [Column("LINHVUC")]
        [StringLength(50)]
        [Description("Lĩnh vực")]
        public string LINHVUC { get; set; }

        [Column("NOIDUNG")]
        [StringLength(2000)]
        [Description("Nội dung")]
        public string NOIDUNG { get; set; }

        [Column("NGUOILAP")]
        [StringLength(200)]
        [Description("Người lập báo cáo")]
        public string NGUOILAP { get; set; }

        [Column("NGAYLAP")]
        [Description("Ngày lập báo cáo")]
        public DateTime? NGAYLAP { get; set; }

        [Column("URL")]
        [StringLength(250)]
        [Description("Đường dẫn để lưu tệp upload")]
        public string URL { get; set; }
    }
}
