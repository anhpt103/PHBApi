﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;

namespace BTS.SP.PHB.ENTITY.Rp.BIEU70NS
{ 
    public class PHB_BIEU70NS_TEMPLATE:BaseEntity
    {
        [Description("Số thứ tự chỉ tiêu")]
        [StringLength(50)]
        public string STT_CHI_TIEU { get; set; }


        [Description("Mã chỉ tiêu")]
        [StringLength(50)]
        [Required]
        public string MA_CHI_TIEU { get; set; }

        [Description("Tên chỉ tiêu")]
        [StringLength(1000)]
        public string TEN_CHI_TIEU { get; set; }
    }
}
