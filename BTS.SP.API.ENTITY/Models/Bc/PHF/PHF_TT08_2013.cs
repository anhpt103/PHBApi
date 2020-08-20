using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_TT08_2013")]
    public class PHF_TT08_2013 : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(50)]
        public string MABAOCAO { get; set; }

        [Column("TENBAOCAO")]
        [StringLength(500)]
        [Description("Tên báo cáo")]
        public string TENBAOCAO { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("TEN_FILE")]
        [StringLength(100)]
        public string TEN_FILE { get; set; }

        [Column("GIMFILE")]
        [StringLength(500)]
        [Description("FILE ĐÍNH KÈM")]
        public string GIMFILE { get; set; }

        [Column("URLFILE")]
        [Description("URLFILE")]
        [StringLength(250)]
        public string URLFILE { get; set; }

        [Column("TUNGAY")]
        [Description("Từ ngày")]
        public DateTime TUNGAY { get; set; }

        [Column("DENNGAY")]
        [Description("Đến ngày")]
        public DateTime DENNGAY { get; set; }

        [Column("NAM")]
        [Description("Năm")]
        [StringLength(10)]
        public string NAM { get; set; }

        [Column("QUY")]
        [Description("Quý")]
        [StringLength(50)]
        public string QUY { get; set; }

        [Column("TENQUY")]
        [Description("Tên quý")]
        [StringLength(50)]
        public string TENQUY { get; set; }

        [Column("MAPHONGBAN")]
        [StringLength(50)]
        [Description("Mã phòng ban")]
        public string MAPHONGBAN { get; set; }

        [Column("THOIGIAN")]
        [Description("Thời gian nhận file DD-MM-YYYY HH:MM:SS")]
        [StringLength(30)]
        public string THOIGIAN { get; set; }
    }
}
