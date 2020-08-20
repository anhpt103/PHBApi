﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Dm
{
    [Table("DM_MAUBAOCAO")]
    public class DM_MAUBAOCAO : DataInfoEntity
    {
        [Column("MA_BAOCAO")]
        [StringLength(10)]
        [Description("Mã báo cáo")]
        public string MA_BAOCAO { get; set; }

        [Column("CAP_BAOCAO")]
        [StringLength(10)]
        [Description("Cấp báo cáo")]
        public string CAP_BAOCAO { get; set; }

        [Column("MA_LOAIBAOCAO")]
        [StringLength(100)]
        [Description("Mã loại báo cáo")]
        public string MA_LOAIBAOCAO { get; set; }

        [Column("LOAI_BAOCAO")]
        [StringLength(100)]
        [Description("Loại báo cáo")]
        public string LOAI_BAOCAO { get; set; }

        [Column("TEN_BAOCAO")]
        [StringLength(100)]
        [Description("Tên báo cáo")]
        public string TEN_BAOCAO { get; set; }

        [Column("NGAY_TAO")]
        [Description("Ngày tạo")]
        public DateTime NGAY_TAO { get; set; }


        [Column("DINH_KEM")]
        [StringLength(100)]
        [Description("Đính kèm")]
        public string DINH_KEM { get; set; }
    }
}