﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.F022HP1
{
    [Table("PHB_F022HP1_TEMPLATE")]
    public class PHB_F022HP1_TEMPLATE : DataInfoEntity
    {
        [Column("MA_TEMPLATE")]
        [Required]
        [StringLength(15)]
        public string MA_TEMPLATE { get; set; }

        [Column("MA_CHI_TIEU")]
        [Required]
        [Description("Mã chỉ tiêu báo cáo")]
        [StringLength(15)]
        public string MA_CHI_TIEU { get; set; }

        [Column("TEN_CHI_TIEU")]
        [Required]
        [Description("Tên chỉ tiêu báo cáo")]
        [StringLength(250)]
        public string TEN_CHI_TIEU { get; set; }

        [Column("STT_CHI_TIEU")]
        [Description("STT chỉ tiêu báo cáo")]
        [Required]
        [StringLength(15)]
        public string STT_CHI_TIEU { get; set; }

        [Column("MA_SO")]
        [Description("Mã số chỉ tiêu")]
        [Required]
        [StringLength(15)]
        public string MA_SO { get; set; }

        [Column("TRANG_THAI")]
        [Required]
        public int TRANG_THAI { get; set; }

        [Column("CONG_THUC")]
        [StringLength(250)]
        public string CONG_THUC { get; set; } 
    }
}
