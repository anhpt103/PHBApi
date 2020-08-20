using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_TT188_PL02")]
    public class PHF_TT188_PL02 : DataInfoEntityPHF
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

        [Column("KETQUA_NAMTRUOC")]
        public decimal? KETQUA_NAMTRUOC { get; set; }

        [Column("KEHOACH_NAM")]
        public decimal? KEHOACH_NAM { get; set; }

        [Column("KETQUA_NAM")]
        public decimal? KETQUA_NAM { get; set; }

        [Column("DOICHIEU_NAMTRUOC")]
        public decimal? DOICHIEU_NAMTRUOC { get; set; }

        [Column("DOICHIEU_KEHOACH")]
        public decimal? DOICHIEU_KEHOACH { get; set; }

        [Column("GHICHU")]
        [StringLength(300)]
        public string GHICHU { get; set; }
    }
}
