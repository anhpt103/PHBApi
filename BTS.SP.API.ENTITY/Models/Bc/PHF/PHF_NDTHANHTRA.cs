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
    [Table("PHF_NDTHANHTRA")]
    public class PHF_NDTHANHTRA : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(200)]
        [Description("Mã báo cáo")]
        public string MABAOCAO { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("TEN_FILE")]
        [Description("Tên file")]
        [StringLength(250)]
        public string TEN_FILE { get; set; }

        [Column("TENBAOCAO")]
        [StringLength(2000)]
        [Description("Tên báo cáo")]
        public string TENBAOCAO { get; set; }

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

        [Column("USER")]
        [StringLength(2000)]
        [Description("Tài khoản người dùng")]
        public string USER { get; set; }

        [Column("MAPHONGBAN")]
        [StringLength(50)]
        [Description("Mã phòng ban")]
        public string MAPHONGBAN { get; set; }

        [Column("THOIGIAN")]
        [Description("Thời gian nhận file DD-MM-YYYY HH:MM:SS")]
        [StringLength(30)]
        public string THOIGIAN { get; set; }

        [Column("TEN_BIEUMAU")]
        [Description("Tên biểu mẫu")]
        [StringLength(200)]
        public string TEN_BIEUMAU { get; set; }

        [Column("TIEUDE_BIEUMAU")]
        [Description("Tiêu đề biểu mẫu")]
        [StringLength(500)]
        public string TIEUDE_BIEUMAU { get; set; }

        [Column("GIMFILE")]
        [StringLength(500)]
        [Description("FILE ĐÍNH KÈM")]
        public string GIMFILE { get; set; }

        [Column("URLFILE")]
        [Description("URLFILE")]
        [StringLength(250)]
        public string URLFILE { get; set; }
    }
}
