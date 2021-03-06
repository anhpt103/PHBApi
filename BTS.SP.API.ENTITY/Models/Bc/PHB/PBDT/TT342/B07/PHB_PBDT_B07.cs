﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.PBDT.B07
{
    [Table("PHB_PBDT_B07")]
    public class PHB_PBDT_B07 : DataInfoEntity
    {
        [Column("REFID")]
        [Required]
        [StringLength(50)]
        public string REFID { get; set; }

        [Column("TRANG_THAI")]
        [Required]
        public int TRANG_THAI { get; set; }

        [Column("NGAY_TAO")]
        public DateTime? NGAY_TAO { get; set; }

        [Column("NGUOI_TAO")]
        [StringLength(150)]
        public string NGUOI_TAO { get; set; }

        [Column("NGAY_SUA")]
        public DateTime? NGAY_SUA { get; set; }

        [Column("NGUOI_SUA")]
        [StringLength(150)]
        public string NGUOI_SUA { get; set; }

        [Column("MA_DONVI")]
        [StringLength(150)]
        public string MA_DONVI { get; set; }

        [Column("DON_VI_DT")]
        [StringLength(150)]
        public string DON_VI_DT { get; set; }

        [Column("CAP_DU_TOAN")]
        [StringLength(150)]
        public string CAP_DU_TOAN { get; set; }

        [Column("NAM_THUC_HIEN")]
        public int NAM_THUC_HIEN { get; set; }

        [Column("NAM_HIEN_HANH")]
        public int NAM_HIEN_HANH { get; set; }

        [Column("NAM_KE_HOACH")]
        public int NAM_KE_HOACH { get; set; }
        [Column("CHUONG")]
        [StringLength(150)]
        public string CHUONG { get; set; }

    }
}
