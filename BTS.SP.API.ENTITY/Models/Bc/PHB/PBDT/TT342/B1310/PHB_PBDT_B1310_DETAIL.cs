﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.PBDT.B1310
{
    [Table("PHB_PBDT_B1310_DETAIL")]
    public class PHB_PBDT_B1310_DETAIL : DataInfoEntity
    {
        [Column("PHB_PBDT_B1310_REFID")]
        [Required]
        [StringLength(50)]
        public string PHB_PBDT_B1310_REFID { get; set; }

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

        // năm trước
        public decimal? UOC_THUC_HIEN { get; set; }

        // năm hiện hành
        public decimal? NAMHH_SO_DOI_TUONG { get; set; }
        public decimal? NAMHH_DU_TOAN { get; set; }
        public decimal? NAMHH_UOC_THUC_HIEN { get; set; }

        // năm kế hoạch
        public decimal? NAMKH_SO_DOI_TUONG { get; set; }
        public decimal? NAMKH_MUC_TRO_CAP { get; set; }
        public decimal? NAMKH_DU_TOAN { get; set; }


        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Column("IS_OPTIONAL")]
        public int IS_OPTIONAL { get; set; }
    }
}
