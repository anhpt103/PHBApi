using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_THEODOI_CANBO_CT")]
    public class PHF_THEODOI_CANBO_CT : DataInfoEntityPHF
    {
        [Column("MA_PHIEU")]
        [StringLength(50)]
        public string MA_PHIEU { get; set; }

        [Column("TEN_CANBO")]
        [StringLength(500)]
        public string MA_CANBO { get; set; }
        
    }
}
