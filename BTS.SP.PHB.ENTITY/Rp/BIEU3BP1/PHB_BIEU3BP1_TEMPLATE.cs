﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.BIEU3BP1
{
    public class PHB_BIEU3BP1_TEMPLATE:BaseEntity
    {
        [StringLength(50)]
        public string STT_CHI_TIEU { get; set; }

        [StringLength(50)]
        [Required]
        public string MA_CHI_TIEU { get; set; }

        [StringLength(255)]
        public string TEN_CHI_TIEU { get; set; }

        [StringLength(500)]
        public string CONG_THUC { get; set; }

        [Description("0 : Không có chi tiết | 1: Là chi tiết")]
        public int LOAI { get; set; }

        [Description("1: Phần I | 2: Phần 2,...")]
        public int PHAN { get; set; }

        [Description("0: Không in đậm, 1: In đậm")]
        public int INDAM { get; set; }

        [Description("0: Không in nghiêng, 1: In nghiêng")]
        public int INNGHIENG { get; set; }
        [Description("Cấp chỉ tiêu")]
        public int CAP { get; set; }
    }
}
