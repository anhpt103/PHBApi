using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1
{
    [Table("PHB_F01_02_P1_TEMPLATE")]
    public class PHB_F01_02_P1_TEMPLATE : BaseEntity
    {
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

        [Column("MA_LOAI")]
        [StringLength(10)]
        public string MA_LOAI { get; set; }

        [Column("MA_KHOAN")]
        [StringLength(10)]
        public string MA_KHOAN { get; set; }

        [Column("NN_LK")]
        [StringLength(10)]
        public string NN_LK { get; set; }

        [Column("IS_BOLD")]
        public int? IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int? IS_ITALIC { get; set; }
    }
}
