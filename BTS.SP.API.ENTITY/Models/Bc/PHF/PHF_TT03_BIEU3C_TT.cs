﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_TT03_BIEU3C_TT")]
    public class PHF_TT03_BIEU3C_TT : DataInfoEntityPHF
    {
        [Column("STT")]
        [Description("Số thứ tự")]
        public int STT { get; set; }

        [Column("STT_TIEUDE")]
        [Description("Số thứ tự tiêu đề")]
        [StringLength(5)]
        public string STT_TIEUDE { get; set; }

        [Column("STT_CHA")]
        [Description("Số thứ tự cha")]
        public int STT_CHA { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("MABAOCAO")]
        [StringLength(200)]
        [Description("Mã báo cáo")]
        public string MABAOCAO { get; set; }

        [Column("DONVI")]
        [StringLength(500)]
        [Description("Đơn vị công tác")]
        public string DONVI { get; set; }

        [Column("NOIDUNG")]
        [StringLength(1000)]
        [Description("Nội dung")]
        public string NOIDUNG { get; set; }

        [Column("GHICHU")]
        [StringLength(1000)]
        [Description("Ghi chú")]
        public string GHICHU { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int? IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int? IS_ITALIC { get; set; }
    }
}
