﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107
{
    [Table("BC04_BCTC_TT107")]
    public class BC04_BCTC_TT107 : BaseEntity
    {
        [Column("REFID")]
        [Required]
        [StringLength(50)]
        public string REFID { get; set; }

        [Column("TRANG_THAI")]
        [Required]
        [Description("1: Đã duyệt | 0:chưa duyệt")]
        public int TRANG_THAI { get; set; }

        [Column("TRANG_THAI_GUI")]
        [Required]
        [Description("1: Đã gửi | 0:chưa gửi | 2:bị trả lại ")]
        public int TRANG_THAI_GUI { get; set; }

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

        [Column("TEN_DONVI")]
        [StringLength(250)]
        public string TEN_DONVI { get; set; }

        [Column("DON_VI_DT")]
        [StringLength(150)]
        public string DON_VI_DT { get; set; }

        [Column("CAP_DU_TOAN")]
        [StringLength(150)]
        public string CAP_DU_TOAN { get; set; }

        [Column("NAM")]
        [Description("1: Đã duyệt | 0:chưa duyệt")]
        public int NAM { get; set; }

        [Column("KY_BC")]
        [Required]
        [Description("0:Năm  | 101:Quý 1 | 102:Quý 2 | 103:Quý 3 | 104:Quý 4 | 201:6 tháng đầu năm | 202 : 6 tháng cuối năm")]
        public int KY_BC { get; set; }

        [Column("SO_QD_THANHLAP")]
        [StringLength(100)]
        public string SO_QD_THANHLAP { get; set; }

        [Column("NGAY_QD_THANHLAP")]
        public DateTime? NGAY_QD_THANHLAP { get; set; }

        [Column("TEN_DONVI_CAPTREN")]
        [StringLength(250)]
        public string TEN_DONVI_CAPTREN { get; set; }

        [Column("THUOC_DONVI_CAP1")]
        public bool THUOC_DONVI_CAP1 { get; set; }

        [Column("MA_LOAIHINH")]
        [StringLength(100)]
        public string MA_LOAIHINH { get; set; }

        [Column("TEN_LOAIHINH")]
        [StringLength(100)]
        public string TEN_LOAIHINH { get; set; }

        [Column("SO_QD_GIAO")]
        [StringLength(100)]
        public string SO_QD_GIAO { get; set; }

        [Column("NGAY_QD_GIAO")]
        public DateTime? NGAY_QD_GIAO { get; set; }

        [Column("CHUCNANG_NHIEMVU")]
        [StringLength(300)]
        public string CHUCNANG_NHIEMVU { get; set; }

        [Column("BCTC_PHEDUYET")]
        [StringLength(100)]
        public string BCTC_PHEDUYET { get; set; }

        [Column("BCTC_NGAY_PHEDUYET")]
        public DateTime? BCTC_NGAY_PHEDUYET { get; set; }
    }
}
