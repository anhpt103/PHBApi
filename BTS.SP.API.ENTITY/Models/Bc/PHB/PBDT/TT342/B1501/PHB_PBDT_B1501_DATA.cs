using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.PBDT.B1501
{
    [Table("PHB_PBDT_B1501_DATA")]
    public class PHB_PBDT_B1501_DATA : DataInfoEntity
    {
        [StringLength(50)]
        public string ROW_REFID { get; set; }

        [StringLength(50)]
        public string COLUMN_REFID { get; set; }

        public decimal? DATA { get; set; }
    }
}
