
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.TTTCQuy
{
    [Table("PHF_BM_FILE_TTTCQuy")]
    public class PHF_BM_FILE_TTTCQuy : DataInfoEntityPHF
    {
        [Column("MA_FILE")]
        [StringLength(200)]
        [Description("Mã file Template")]
        public string MA_FILE { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("TEN_FILE")]
        [Description("Tên file")]
        [StringLength(250)]
        public string TEN_FILE { get; set; }

        [Column("NAM")]
        [Description("Năm")]
        [StringLength(6)]
        public string NAM { get; set; }

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


    }
}
