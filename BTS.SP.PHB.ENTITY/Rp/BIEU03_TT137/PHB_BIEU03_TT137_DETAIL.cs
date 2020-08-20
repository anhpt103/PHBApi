using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.BIEU03_TT137
{
    [Table("PHB_BIEU03_TT137_DETAIL")]
    public class PHB_BIEU03_TT137_DETAIL : BaseEntity
    {
        [Column("PHB_BIEU03_TT137_REFID")]
        [Required]
        [StringLength(50)]
        public string PHB_BIEU03_TT137_REFID { get; set; }

        [Column("STT_SAPXEP")]
        public int STT_SAPXEP { get; set; }

        [Column("STT")]
        [StringLength(5)]
        public string STT { get; set; }

        [Column("MA_SO")]
        [StringLength(5)]
        public string MA_SO { get; set; }

        [Column("MA_CHA")]
        [StringLength(5)]
        public string MA_CHA { get; set; }

        [Column("CHI_TIEU")]
        [Required]
        [StringLength(1000)]
        public string CHI_TIEU { get; set; }

        [Column("DT_NT")]
        public decimal? DT_NT { get; set; }

        [Column("DT_GNN")]
        public decimal? DT_GNN { get; set; }

        [Column("DT_SDNN")]
        public double? DT_SDNN { get; set; }

        [Column("QT_NN")]
        public decimal? QT_NN { get; set; }

        [Column("DG_TUYETDOI")]
        public double? DG_TUYETDOI { get; set; }

        [Column("DG_TUONGDOI")]
        public double? DG_TUONGDOI { get; set; }

        [Column("DSD_TUYETDOI")]
        public double? DSD_TUYETDOI { get; set; }

        [Column("DSD_TUONGDOI")]
        public double? DSD_TUONGDOI { get; set; }

        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Column("IS_OPTIONAL")]
        public int IS_OPTIONAL { get; set; }
    }
}
