using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHA
{
    [Table("PHA_BAOCAO_DAUTU_DETAIL")]
    public class PHA_BAOCAO_DAUTU_DETAIL : DataInfoEntity
    {
        [Column("REFID")]
        [Required]
        [StringLength(50)]
        public string REFID { get; set; }

        [Column("STT")]
        [StringLength(5)]
        public string STT { get; set; }

        [Column("NOIDUNG")]
        [StringLength(250)]
        public string NOIDUNG { get; set; }

        [Column("DIADIEM_MO_TK")]
        [StringLength(250)]
        public string DIADIEM_MO_TK { get; set; }

        public decimal SEGMENT_5 { get; set; }
        public decimal SEGMENT_6 { get; set; }
        public decimal SEGMENT_7 { get; set; }
        public decimal SEGMENT_8 { get; set; }
        public decimal SEGMENT_9 { get; set; }
        public decimal SEGMENT_10 { get; set; }
        public decimal SEGMENT_11 { get; set; }
        public decimal SEGMENT_12 { get; set; }
        public decimal SEGMENT_13 { get; set; }
        public decimal SEGMENT_14 { get; set; }
    }
}
