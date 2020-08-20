using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_TT08_2013_TONGHOP")]
    public class PHF_TT08_2013_TONGHOP : DataInfoEntityPHF
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

        [Column("SONGUOI_PHAIKEKHAI")]
        public int? SONGUOI_PHAIKEKHAI { get; set; }

        [Column("SONGUOI_DAKEKHAI")]
        public int? SONGUOI_DAKEKHAI { get; set; }

        [Column("SONGUOI_CONGKHAI_NIEMYET")]
        public int? SONGUOI_CONGKHAI_NIEMYET { get; set; }

        [Column("SONGUOI_CONGKHAI_TOCHUCHOP")]
        public int? SONGUOI_CONGKHAI_TOCHUCHOP { get; set; }

        [Column("SONGUOI_DUOCXACMINH")]
        public int? SONGUOI_DUOCXACMINH { get; set; }

        [Column("SONGUOI_COKETLUAN")]
        public int? SONGUOI_COKETLUAN { get; set; }

        [Column("SONGUOI_XLKL_KHONGTT")]
        public int? SONGUOI_XLKL_KHONGTT { get; set; }

        [Column("SONGUOI_XLKL")]
        public int? SONGUOI_XLKL { get; set; }

        [Column("SONGUOI_XLTN")]
        public int? SONGUOI_XLTN { get; set; }

        [Column("GHICHU")]
        [StringLength(300)]
        public string GHICHU { get; set; }
    }
}
