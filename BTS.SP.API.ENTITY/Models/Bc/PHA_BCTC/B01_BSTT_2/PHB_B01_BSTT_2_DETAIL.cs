using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHA_BCTC.PHB_B01_BSTT_2
{
    [Table("PHB_B01_BSTT_2_DETAIL")]
    public class PHB_B01_BSTT_2_DETAIL : DataInfoEntity
    {
        [Column("PHB_B01_BSTT_2_REFID")]
        [Required]
        [Description("RefID Guid ID trong  PHB_B01_BSTT_2_BCTC")]
        [StringLength(50)]
        public string PHB_B01_BSTT_2_REFID { get; set; }

        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }

        [Column("STT")]
        [StringLength(5)]
        public string STT { get; set; }

        [Column("CHI_TIEU")]
        [Required]
        [StringLength(250)]
        public string CHI_TIEU { get; set; }

        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Column("NAM_NAY")]
        public decimal? NAM_NAY { get; set; }
    }
}
