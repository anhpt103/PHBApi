using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHA_BCTC.B03A_BCTC
{
    [Table("PHB_B03A_BCTC_DETAIL")]
    public class PHB_B03A_BCTC_DETAIL : DataInfoEntity
    {
        [Column("PHB_B03A_BCTC_REFID")]
        [Required]
        [Description("RefID Guid ID trong  PHB_B03A_BCTC")]
        [StringLength(50)]
        public string PHB_B03A_BCTC_REFID { get; set; }

        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }

        [Column("STT")]
        [StringLength(5)]
        public string STT { get; set; }

        [Column("CHI_TIEU")]
        [Required]
        [StringLength(250)]
        public string CHI_TIEU { get; set; }

        [Column("MA_SO")]
        [StringLength(250)]
        public string MA_SO { get; set; }

        [Column("MA_SO_CHA")]
        [StringLength(250)]
        public string MA_SO_CHA { get; set; }

        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Column("THUYET_MINH")]
        [StringLength(250)]
        public string THUYET_MINH { get; set; }

        [Column("SO_CUOI_NAM")]
        public decimal? SO_CUOI_NAM { get; set; }

        [Column("SO_DAU_NAM")]
        public decimal? SO_DAU_NAM { get; set; }

    }
}
