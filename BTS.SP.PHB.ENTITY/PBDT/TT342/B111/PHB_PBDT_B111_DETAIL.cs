using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.PBDT.B111
{
    [Table("PHB_PBDT_B111_DETAIL")]
    public class PHB_PBDT_B111_DETAIL : BaseEntity
    {
        [Column("PHB_PBDT_B111_REFID")]
        [Required]
        [StringLength(50)]
        public string PHB_PBDT_B111_REFID { get; set; }

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


        [Column("NAMHH_DT_TONG_SO")]
        public decimal? NAMHH_DT_TONG_SO { get; set; }

        [Column("NAMHH_DT_CHI_DTPT")]
        public decimal? NAMHH_DT_CHI_DTPT { get; set; }

        [Column("NAMHH_DT_CHI_THUONGXUYEN")]
        public decimal? NAMHH_DT_CHI_THUONGXUYEN { get; set; }


        [Column("NAMHH_UOCTH_TONG_SO")]
        public decimal? NAMHH_UOCTH_TONG_SO { get; set; }

        [Column("NAMHH_UOCTH_CHI_DTPT")]
        public decimal? NAMHH_UOCTH_CHI_DTPT { get; set; }

        [Column("NAMHH_UOCTH_CHI_THUONGXUYEN")]
        public decimal? NAMHH_UOCTH_CHI_THUONGXUYEN { get; set; }


        [Column("NAMKH_DT_TONG_SO")]
        public decimal? NAMKH_DT_TONG_SO { get; set; }

        [Column("NAMKH_DT_CHI_DTPT")]
        public decimal? NAMKH_DT_CHI_DTPT { get; set; }

        [Column("NAMKH_DT_CHI_THUONG_XUYEN")]
        public decimal? NAMKH_DT_CHI_THUONG_XUYEN { get; set; }


        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Column("IS_OPTIONAL")]
        public int IS_OPTIONAL { get; set; }
    }
}
