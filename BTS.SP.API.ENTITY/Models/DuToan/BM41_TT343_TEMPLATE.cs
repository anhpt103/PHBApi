using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.DuToan
{
    [Table("BM41_TT343_TEMPLATE")]
    public class BM41_TT343_TEMPLATE : DataInfoEntity
    {
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

        [Column("MA_CHITIEU_CHA")]
        [StringLength(20)]
        public string MA_CHITIEU_CHA { get; set; }

    }
}
