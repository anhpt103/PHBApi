using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BIEU02_TONGHOP_GIAINGAN")]
    public class PHF_BIEU02_TONGHOP_GIAINGAN : DataInfoEntityPHF
    {
        [Column("STT")]
        [Description("Số thứ tự")]
        public int STT { get; set; }

        [Column("STT_TIEUDE")]
        [Description("Số thứ tự tiêu đề")]
        [StringLength(5)]
        public string STT_TIEUDE { get; set; }

        [Column("STT_CHA")]
        [Description("Số thứ tự cha")]
        public int STT_CHA { get; set; }

        [Column("MA_FILE")]
        [StringLength(200)]
        [Description("Mã file Template")]
        public string MA_FILE { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("NGUON_KINHPHI")]
        [StringLength(500)]
        public string NGUON_KINHPHI { get; set; }

        [Column("KEHOACHGN_TONG")]
        public decimal? GIAINGAN_TONG { get; set; }

        [Column("KEHOACHGN_NAM")]
        public decimal? GIAINGAN_NAM { get; set; }

        [Column("THUCHIENGN_TONG")]
        public decimal? THUCHIENGN_TONG { get; set; }

        [Column("THUCHIENGN_NAM")]
        public decimal? THUCHIENGN_NAM { get; set; }

        [Column("TYLEGN_TONG")]
        public decimal? TYLEGN_TONG { get; set; }

        [Column("TYLEGN_NAM")]
        public decimal? TYLEGN_NAM { get; set; }

        [Column("GHICHU")]
        [StringLength(500)]
        public string GHICHU { get; set; }

    }
}
