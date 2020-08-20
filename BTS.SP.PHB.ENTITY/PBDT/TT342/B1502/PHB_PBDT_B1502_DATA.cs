using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.PBDT.B1502
{
    [Table("PHB_PBDT_B1502_DATA")]
    public class PHB_PBDT_B1502_DATA : BaseEntity
    {
        [StringLength(50)]
        public string ROW_REFID { get; set; }

        [StringLength(50)]
        public string COLUMN_REFID { get; set; }

        public decimal? DATA { get; set; }
    }
}
