using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.DuToan
{
    [Table("PHA_NDT_CTMT_CHUONG_TEMP")]
    public class PHA_NDT_CTMT_CHUONG_TEMP : DataInfoEntity
    {
        [Column("TEN")]
        [StringLength(500)]
        public string TEN { get; set; }

        [Column("MA")]
        [StringLength(50)]
        public string MA { get; set; }

        [Column("STT")]
        [StringLength(10)]
        public string STT { get; set; }

        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }

        [Column("LOAI_CTMT")]
        [StringLength(20)]
        public string LOAI_CTMT { get; set; }
    }
}
