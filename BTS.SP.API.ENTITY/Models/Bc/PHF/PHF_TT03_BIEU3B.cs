using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_TT03_BIEU3B")]
    public class PHF_TT03_BIEU3B : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(200)]
        [Description("Mã báo cáo")]
        public string MABAOCAO { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("TT")]
        [StringLength(50)]
        [Description("TT")]
        public string TT { get; set; }

        [Column("TENVU")]
        [StringLength(1000)]
        [Description("Tên vụ")]
        public string TENVU { get; set; }

        [Column("TENCOQUSN")]
        [StringLength(1000)]
        [Description("Tên cơ quan")]
        public string TENCOQUSN { get; set; }

        [Column("COQUANTHULY")]
        [StringLength(1000)]
        [Description("Cơ quan thụ lý")]
        public string COQUANTHULY { get; set; }

        [Column("TOMTAT")]
        [StringLength(1000)]
        [Description("Tóm tắt nội dung vụ việc")]
        public string TOMTAT { get; set; }
    }
}
