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
    [Table("PHF_GIAMSAT_DOAN_TT_CT")]
    public class PHF_GIAMSAT_DOAN_TT_CT : DataInfoEntityPHF
    {
        [Column("MAQD")]
        [StringLength(50)]
        [Description("Mã quyết định")]
        public string MAQD { get; set; }

        [Column("MA_PK")]
        [StringLength(50)]
        public string MA_PK { get; set; }

        [Column("FILEDINHKEM")]
        [StringLength(500)]
        [Description("File đính kèm")]
        public string FILEDINHKEM { get; set; }

        [Column("URL")]
        [StringLength(250)]
        [Description("Đường dẫn để lưu tệp upload")]
        public string URL { get; set; }

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
    }
}
