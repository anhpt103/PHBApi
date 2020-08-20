using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_CAUHINHBAOCAO")]
    public class PHF_CAUHINHBAOCAO : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(200)]
        [Description("Mã báo cáo")]
        public string MABAOCAO { get; set; }

        [Column("NGUOILAP")]
        [StringLength(100)]
        [Description("Người lập")]
        public string NGUOILAP { get; set; }

        [Column("NGAYLAP")]
        [Description("Ngày lập")]
        public DateTime NGAYLAP { get; set; }

        [Column("FILEDINHKEM")]
        [StringLength(1000)]
        [Description("File đính kèm")]
        public string FILEDINHKEM { get; set; }

        [Column("TUNGAY")]
        [Description("Từ ngày")]
        public DateTime TUNGAY { get; set; }

        [Column("DENNGAY")]
        [Description("Đến ngày")]
        public DateTime DENNGAY { get; set; }

        [Column("NAM")]
        [Description("Năm")]
        public int NAM { get; set; }

        [Column("QUY")]
        [Description("Quý")]
        [StringLength(50)]
        public string QUY { get; set; }

        [Column("TENQUY")]
        [Description("Tên quý")]
        [StringLength(50)]
        public string TENQUY { get; set; }

        [Column("NOIDUNG")]
        [Description("Nội dung")]
        [StringLength(1500)]
        public string NOIDUNG { get; set; }

        [Column("MAPHONGBAN")]
        [StringLength(50)]
        [Description("Mã phòng ban")]
        public string MAPHONGBAN { get; set; }

        [Column("THOIGIAN")]
        [Description("Thời gian nhận file DD-MM-YYYY HH:MM:SS")]
        [StringLength(30)]
        public string THOIGIAN { get; set; }

        [Column("URL")]
        [Description("URL")]
        [StringLength(250)]
        public string URL { get; set; }

        [Column("TRANG_THAI")]
        [Description("Trạng thái")]
        public Nullable<int> TRANG_THAI { get; set; }
    }
}
