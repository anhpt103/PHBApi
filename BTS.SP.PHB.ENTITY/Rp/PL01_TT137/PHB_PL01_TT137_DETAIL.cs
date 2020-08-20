using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.PL01_TT137
{
    [Table("PHB_PL01_TT137_DETAIL")]
    public class PHB_PL01_TT137_DETAIL : BaseEntity
    {
        [Required]
        [Description("Guid ID trong  PHB_PL01_TT137")]
        [StringLength(50)]
        public string PHB_PL01_TT137_REFID { get; set; }

        [Column("CANBO_I")]
        [StringLength(500)]
        public string CANBO_I { get; set; }

        [Column("CANBO_II")]
        [StringLength(500)]
        public string CANBO_II { get; set; }

        [Column("CHUCVU_I")]
        [StringLength(500)]
        public string CHUCVU_I { get; set; }

        [Column("CHUCVU_II")]
        [StringLength(500)]
        public string CHUCVU_II { get; set; }

        [Column("QT_NGANSACH_NAM")]
        [StringLength(500)]
        public string QT_NGANSACH_NAM { get; set; }

        [Column("QT_KHONGVON")]
        [StringLength(500)]
        public string QT_KHONGVON { get; set; }

        [Column("DT_GIAO_DAUNAM")]
        public double DT_GIAO_DAUNAM { get; set; }

        [Column("DT_BOSUNG_TRONGNAM")]
        public double DT_BOSUNG_TRONGNAM { get; set; }

        [Column("TSKP_PHAINOP_NSNN")]
        public double TSKP_PHAINOP_NSNN { get; set; }

        [Column("TSKP_DANOP_NSNN")]
        public double TSKP_DANOP_NSNN { get; set; }

        [Column("TSKP_CONPHAINOP_NSNN")]
        public double TSKP_CONPHAINOP_NSNN { get; set; }

        [Column("THUYET_MINH")]
        [StringLength(2000)]
        public string THUYET_MINH { get; set; }

        [Column("NHAN_XET")]
        [StringLength(2000)]
        public string NHAN_XET { get; set; }

        [Column("KIEN_NGHI")]
        [StringLength(2000)]
        public string KIEN_NGHI { get; set; }

        [Column("DONVI_CHA")]
        [StringLength(255)]
        public string DONVI_CHA { get; set; }

        [Column("DIA_CHI")]
        [StringLength(500)]
        public string DIA_CHI { get; set; }
    }
}
