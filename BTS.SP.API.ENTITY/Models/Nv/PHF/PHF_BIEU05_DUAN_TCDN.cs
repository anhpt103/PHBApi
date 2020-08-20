using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BIEU05_DUAN_TCDN")]
    public class PHF_BIEU05_DUAN_TCDN : DataInfoEntityPHF
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

        [Column("MA_FILE")]
        [StringLength(200)]
        [Description("Mã file Template")]
        public string MA_FILE { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("TEN_DONVI")]
        [StringLength(1000)]
        [Description("Tên đơn vị")]
        public string TEN_DONVI { get; set; }

        [Column("TONGMUC_DAUTU")]
        [StringLength(1000)]
        [Description("Tổng mức đầu tư")]
        public string TONGMUC_DAUTU { get; set; }

        [Column("NGAYBATDAU_THUCHIEN_DA")]
        [Description("Ngày bắt đầu thực hiện dự án")]
        public DateTime? NGAYBATDAU_THUCHIEN_DA { get; set; }

        [Column("THUCHIEN_NAM")]
        [StringLength(1000)]
        [Description("Thực hiện năm")]
        public string THUCHIEN_NAM { get; set; }

        [Column("LUYKE_GIATRI_THUCHIEN")]
        [StringLength(1000)]
        [Description("Lũy kế giá trị thực hiện")]
        public string LUYKE_GIATRI_THUCHIEN { get; set; }

        [Column("GHICHU")]
        [StringLength(1000)]
        [Description("Ghi chú")]
        public string GHICHU { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }
    }
}
