using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_TT03_BIEU2B")]
    public class PHF_TT03_BIEU2B : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(200)]
        public string MABAOCAO { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("DONVI")]
        [StringLength(50)]
        public string DONVI { get; set; }

        [Column("COT1")]
        public decimal? COT1 { get; set; }

        [Column("COT2")]
        public decimal? COT2 { get; set; }

        [Column("COT3")]
        public decimal? COT3 { get; set; }

        [Column("COT4")]
        public decimal? COT4 { get; set; }

        [Column("COT5")]
        public decimal? COT5 { get; set; }

        [Column("COT6")]
        public decimal? COT6 { get; set; }

        [Column("COT7")]
        public decimal? COT7 { get; set; }

        [Column("COT8")]
        public decimal? COT8 { get; set; }

        [Column("COT9")]
        public decimal? COT9 { get; set; }

        [Column("COT10")]
        public decimal? COT10 { get; set; }

        [Column("COT11")]
        public decimal? COT11 { get; set; }

        [Column("COT12")]
        public decimal? COT12 { get; set; }

        [Column("COT13")]
        public decimal? COT13 { get; set; }

        [Column("COT14")]
        public decimal? COT14 { get; set; }

        [Column("COT15")]
        public decimal? COT15 { get; set; }

        [Column("COT16")]
        public decimal? COT16 { get; set; }

        [Column("COT17")]
        public decimal? COT17 { get; set; }

        [Column("COT18")]
        public decimal? COT18 { get; set; }

        [Column("COT19")]
        public decimal? COT19 { get; set; }

        [Column("COT20")]
        public decimal? COT20 { get; set; }

        [Column("COT21")]
        public decimal? COT21 { get; set; }

        [Column("COT22")]
        public decimal? COT22 { get; set; }

        [Column("COT23")]

        public decimal? COT23 { get; set; }

        [Column("COT24")]
        public decimal? COT24 { get; set; }

        [Column("COT25")]
        public decimal? COT25 { get; set; }

        [Column("COT26")]
        public decimal? COT26 { get; set; }

        [Column("COT27")]
        public decimal? COT27 { get; set; }

        [Column("COT28")]
        public decimal? COT28 { get; set; }

        [Column("COT29")]
        public decimal? COT29 { get; set; }

        [Column("COT30")]
        public decimal? COT30 { get; set; }

        [Column("COT31")]
        public decimal? COT31 { get; set; }

        [Column("COT32")]
        [StringLength(1000)]
        [Description("Ghi chú")]
        public string COT32 { get; set; }

    }
}
