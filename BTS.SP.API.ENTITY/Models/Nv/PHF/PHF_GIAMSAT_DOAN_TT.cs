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
    [Table("PHF_GIAMSAT_DOAN_TT")]
    public class PHF_GIAMSAT_DOAN_TT : DataInfoEntityPHF
    {
        [Column("MAQD")]
        [StringLength(50)]
        [Description("Mã quyết định")]
        public string MAQD { get; set; }

        [Column("MA_PK")]
        [StringLength(50)]
        public string MA_PK { get; set; }

        [Column("TENQD")]
        [StringLength(250)]
        [Description("Tên quyết định")]
        public string TENQD { get; set; }

        [Column("NAM")]
        [Description("Năm")]
        public int NAM { get; set; }

        [Column("MAPHONGBAN")]
        [StringLength(50)]
        [Description("Mã phòng ban")]
        public string MAPHONGBAN { get; set; }

        [Column("MADOITUONG")]
        [StringLength(50)]
        [Description("Mã đối tượng")]
        public string MADOITUONG { get; set; }

        [Column("FILEDINHKEM")]
        [StringLength(500)]
        [Description("File đính kèm")]
        public string FILEDINHKEM { get; set; }

        [Column("URL")]
        [StringLength(250)]
        [Description("Đường dẫn để lưu tệp upload")]
        public string URL { get; set; }

        [Column("FILEDINHKEM_2")]
        [StringLength(1000)]
        [Description("File đính kèm")]
        public string FILEDINHKEM_2 { get; set; }

        [Column("URL_2")]
        [Description("URL_2")]
        [StringLength(250)]
        public string URL_2 { get; set; }

        [Column("FILEDINHKEM_3")]
        [StringLength(1000)]
        [Description("File đính kèm")]
        public string FILEDINHKEM_3 { get; set; }

        [Column("URL_3")]
        [Description("URL_3")]
        [StringLength(250)]
        public string URL_3 { get; set; }

        [Column("NGAYLAP")]
        [Description("Ngày lập báo cáo")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "yyyy/MM/dd", ApplyFormatInEditMode = true)]
        public DateTime? NGAYLAP { get; set; }

        [Column("NGAYQDGS")]
        [Description("Ngày quyết định giám sát")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "yyyy/MM/dd", ApplyFormatInEditMode = true)]
        public DateTime? NGAYQDGS { get; set; }

        [Column("THOIGIAN_CAPNHAT")]
        [Description("Thời gian cập nhật văn bản DD-MM-YYYY HH:MM:SS")]
        [StringLength(30)]
        public string THOIGIAN_CAPNHAT { get; set; }

        [Column("TOTRUONG")]
        [StringLength(250)]
        [Description("Tổ trưởng")]
        public string TOTRUONG { get; set; }

        [Column("THANHVIEN")]
        [StringLength(500)]
        [Description("Thành viên")]
        public string THANHVIEN { get; set; }

        [Column("NGAY_TRIENKHAI")]
        [Description("Ngày triển khai")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "yyyy/MM/dd", ApplyFormatInEditMode = true)]
        public DateTime? NGAY_TRIENKHAI { get; set; }

        [Column("NGAY_KETTHUC")]
        [Description("Ngày kết thúc")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "yyyy/MM/dd", ApplyFormatInEditMode = true)]
        public DateTime? NGAY_KETTHUC { get; set; }
    }
}
