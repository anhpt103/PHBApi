using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHA
{
    [Table("PHA_BAOCAO_DAUTU")]
    public class PHA_BAOCAO_DAUTU : DataInfoEntity
    {
        [Column("REFID")]
        [Required]
        [StringLength(50)]
        public string REFID { get; set; }

        [Column("MA_DVQHNS")]
        [StringLength(10)]
        public string MA_DVQHNS { get; set; }

        [Column("MA_DBHC")]
        [StringLength(255)]
        public string MA_DBHC { get; set; }

        [Column("TEN_DBHC")]
        [StringLength(255)]
        public string TEN_DBHC { get; set; }

        [Column("MA_BAOCAO")]
        [StringLength(20)]
        [Required]
        public string MA_BAOCAO { get; set; }

        [Column("TEN_DVQHNS")]
        [StringLength(255)]
        public string TEN_DVQHNS { get; set; }

        [Column("NAM")]
        [Required]
        public int NAM { get; set; }
    }
}
