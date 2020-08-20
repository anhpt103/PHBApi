using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.PBDT.B1311
{
    [Table("PHB_PBDT_B1311_DETAIL")]
    public class PHB_PBDT_B1311_DETAIL : DataInfoEntity
    {
        [Column("PHB_PBDT_B1311_REFID")]
        [Required]
        [StringLength(50)]
        public string PHB_PBDT_B1311_REFID { get; set; }

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

        // đối tượng
        public int? DOITUONG_NAMTRUOC { get; set; }
        public int? DOITUONG_NAMHH { get; set; }
        public int? DOITUONG_NAMKH { get; set; }
        public int? DOITUONG_TANG_GIAM { get; set; }
        public double? DOITUONG_TY_LE { get; set; }

        // số tiền
        public decimal? SOTIEN_NAMTRUOC { get; set; }
        public decimal? SOTIEN_NAMHH_DT { get; set; }
        public decimal? SOTIEN_NAMHH_UOCTHUCHIEN { get; set; }
        public decimal? SOTIEN_NAMKH { get; set; }
        public decimal? SOTIEN_TANG_GIAM { get; set; }
        public double? SOTIEN_TY_LE { get; set; }

        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Column("IS_OPTIONAL")]
        public int IS_OPTIONAL { get; set; }
    }
}
