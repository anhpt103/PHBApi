using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_TT188_PL01")]
    public class PHF_TT188_PL01 : DataInfoEntityPHF
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

        [Column("NOIDUNGCT")]
        [StringLength(2000)]
        public string NOIDUNGCT { get; set; }
    }
}
