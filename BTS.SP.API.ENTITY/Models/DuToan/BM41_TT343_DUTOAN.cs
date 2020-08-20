using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.DuToan
{
    [Table("BM41_TT343_DUTOAN")]
    public class BM41_TT343_DUTOAN : DataInfoEntity
    {
        [Column("MABAOCAO")]
        [Required]
        [StringLength(50)]
        public string MABAOCAO { get; set; }
        [Column("REFID")]
        [Required]
        [StringLength(50)]
        public string REFID { get; set; }
        [Column("NGAY_TAO")]
        public DateTime? NGAY_TAO { get; set; }
        [Column("NGUOI_TAO")]
        [StringLength(150)]
        public string NGUOI_TAO { get; set; }
        [Column("NGAY_SUA")]
        public DateTime? NGAY_SUA { get; set; }
        [Column("NGUOI_SUA")]
        [StringLength(150)]
        public string NGUOI_SUA { get; set; }
        [Column("MA_DIABAN")]
        [StringLength(10)]
        public string MA_DIABAN { get; set; }
        [Column("NAM")]
        [StringLength(5)]
        public string NAM { get; set; }
    }
}
