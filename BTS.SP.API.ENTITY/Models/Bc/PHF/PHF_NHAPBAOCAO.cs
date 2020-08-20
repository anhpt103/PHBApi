using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_NHAPBAOCAO")]
    public class PHF_NHAPBAOCAO : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(200)]
        [Description("Mã báo cáo")]
        public string MABAOCAO { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã báo cáo cha")]
        public string MA_FILE_PK { get; set; }

        [Column("HANBAOCAO")]
        [Description("Hạn báo cáo")]
        public DateTime HANBAOCAO { get; set; }

        [Column("SOBAOCAO")]
        [Description("Số báo cáo")]
        [StringLength(50)]
        public string SOBAOCAO { get; set; }

        [Column("CHUCVU_NGUOILAP")]
        [StringLength(200)]
        [Description("Chức vụ người lập báo cáo")]
        public string CHUCVU_NGUOILAP { get; set; }

        [Column("CHUCVU_NGUOIKY")]
        [StringLength(200)]
        [Description("Chức vụ người ký báo cáo")]
        public string CHUCVU_NGUOIKY { get; set; }

        [Column("NGUOIKY")]
        [StringLength(200)]
        [Description("Người ký báo cáo")]
        public string NGUOIKY { get; set; }

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

        [Column("NGUOILAP")]
        [Description("Người lập báo cáo")]
        [StringLength(200)]
        public string NGUOILAP { get; set; }

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

        [Column("PHONE")]
        [StringLength(20)]
        [Description("SĐT")]
        public string PHONE { get; set; }
    }
}
