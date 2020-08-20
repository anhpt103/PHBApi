using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_TT129_PL03")]
    public class PHF_TT129_PL03 : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(50)]
        public string MABAOCAO { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("TEN_FILE")]
        [StringLength(100)]
        public string TEN_FILE { get; set; }

        [Column("MADONG")]
        [StringLength(50)]
        public string MADONG { get; set; }

        [Column("DIEM_TUDANHGIA")]
        public decimal? DIEM_TUDANHGIA { get; set; }
  
    }
}
