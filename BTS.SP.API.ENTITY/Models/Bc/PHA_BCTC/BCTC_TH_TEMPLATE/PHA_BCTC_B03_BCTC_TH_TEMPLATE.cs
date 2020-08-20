using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHA_BCTC.BCTC_TH_TEMPLATE
{
    [Table("PHA_BCTC_B03_BCTC_TH_TEMPLATE")]
    public class PHA_BCTC_B03_BCTC_TH_TEMPLATE : DataInfoEntity
    {
        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }

        [Column("STT")]
        [StringLength(1000)]
        public string STT { get; set; }

        [Column("CHI_TIEU")]
        [Required]
        [StringLength(1000)]
        public string CHI_TIEU { get; set; }

        [Column("MA_SO")]
        [StringLength(1000)]
        public string MA_SO { get; set; }

        [Column("MA_SO_CHA")]
        [StringLength(1000)]
        public string MA_SO_CHA { get; set; }

        [Column("THUYET_MINH")]
        [StringLength(1000)]
        public string THUYET_MINH { get; set; }

        [Column("NAM_NAY")]
        public decimal NAM_NAY { get; set; }

        [Column("NAM_TRUOC")]
        public decimal NAM_TRUOC { get; set; }

        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Column("MA_NAM_NAY")]
        [StringLength(1000)]
        public string MA_NAM_NAY { get; set; }

        [Column("XML_PARENT_FIELD_NAME")]
        [StringLength(1000)]
        public string XML_PARENT_FIELD_NAME { get; set; }
    }
}
