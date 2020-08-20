using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.XLKN
{
    [Table("PHF_PL04_XLKN_CT")]
    public class PHF_PL04_XLKN_CT : DataInfoEntityPHF
    {
        [Column("MA_FILE")]
        [StringLength(100)]
        [Description("Mã báo cáo")]
        public string MA_FILE { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("STT")]
        [Description("Số thứ tự")]
        [StringLength(200)]
        public string STT { get; set; }

        [Column("STT_TIEUDE")]
        [Description("Số thứ tự tiêu đề")]
        [StringLength(5)]
        public string STT_TIEUDE { get; set; }

        [Column("STT_CHA")]
        [Description("Số thứ tự cha")]
        [StringLength(200)]
        public string STT_CHA { get; set; }
        [Column("DONVI_DUOC_THANHTRA")]
        [StringLength(100)]
        [Description("Đơn vị được thanh tra, kiểm tra")]
        public string DONVI_DUOC_THANHTRA { get; set; }

        [Column("VBCC_SO")]
        [StringLength(100)]
        [Description("Văn bản cơ chế số")]
        public string VBCC_SO { get; set; }

        [Column("VBCC_NGAY")]
        [Description("Văn bản cơ chế ngày")]
        public DateTime? VBCC_NGAY { get; set; }

        [Column("VBCC_NOIDUNG")]
        [StringLength(500)]
        [Description("Văn bản cơ chế nội dung")]
        public string VBCC_NOIDUNG { get; set; }

        [Column("NOIDUNG_CC_MOI")]
        [StringLength(500)]
        [Description("Nội dung cơ chế mới")]
        public string NOIDUNG_CC_MOI { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }

    }
}
