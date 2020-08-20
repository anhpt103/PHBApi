﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.PBDT.B1309
{
    [Table("PHB_PBDT_B1309_DETAIL")]
    public class PHB_PBDT_B1309_DETAIL : BaseEntity
    {
        [Column("PHB_PBDT_B1309_REFID")]
        [Required]
        [StringLength(50)]
        public string PHB_PBDT_B1309_REFID { get; set; }

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


        [StringLength(500)]
        public string QD_PHE_DUYET { get; set; }

        [StringLength(500)]
        public string THOIGIAN_THUCHIEN { get; set; }

        public decimal? TONG_KINH_PHI { get; set; }
        public decimal? LUY_KE { get; set; }
        public decimal? DU_TOAN { get; set; }


        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Column("IS_OPTIONAL")]
        public int IS_OPTIONAL { get; set; }
    }
}
