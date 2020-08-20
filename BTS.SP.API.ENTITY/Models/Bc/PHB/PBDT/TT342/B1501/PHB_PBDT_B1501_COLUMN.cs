using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.PBDT.B1501
{
    [Table("PHB_PBDT_B1501_COLUMN")]
    public class PHB_PBDT_B1501_COLUMN : DataInfoEntity
    {
        public int STT { get; set; }

        [StringLength(50)]
        public string COLUMN_REFID { get; set; }

        [StringLength(500)]
        public string TITLE { get; set; }

        [StringLength(5)]
        public string MA_SO { get; set; }

        [StringLength(5)]
        public string MA_CHA { get; set; }
    }
}
