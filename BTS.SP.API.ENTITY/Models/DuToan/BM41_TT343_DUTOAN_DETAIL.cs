using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.DuToan
{
    [Table("BM41_TT343_DUTOAN_DETAIL")]
    public class BM41_TT343_DUTOAN_DETAIL : DataInfoEntity
    {
        [Column("REFID_BM41_TT343")]
        [Required]
        [StringLength(50)]
        public string REFID_BM41_TT343 { get; set; }
        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }
        [Column("STT")]
        [StringLength(20)]
        public string STT { get; set; }
        [Column("TEN_CHITIEU")]
        [StringLength(500)]
        public string TEN_CHITIEU { get; set; }
        [Column("MA_CHITIEU")]
        [StringLength(20)]
        public string MA_CHITIEU { get; set; }  
        [Column("TW")]
        public decimal? TW { get; set; }
        [Column("TINH")]
        public decimal? TINH { get; set; }
        [Column("DIABAN")]
        public decimal? DIABAN { get; set; }
        [Column("PHUONG")]
        public decimal? PHUONG { get; set; }
        [Column("XA")]
        public decimal? XA { get; set; }
        [Column("MA_DIABAN")]
        [StringLength(10)]
        public string MA_DIABAN { get; set; }
        [Column("MA_DIABAN_CHA")]
        [StringLength(10)]
        public string MA_DIABAN_CHA { get; set; }
        [Column("TEN_DIABAN")]
        [StringLength(100)]
        public string TEN_DIABAN { get; set; }

    }
}
