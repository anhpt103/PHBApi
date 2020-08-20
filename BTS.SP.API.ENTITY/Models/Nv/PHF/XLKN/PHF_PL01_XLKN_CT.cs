using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.XLKN
{
    [Table("PHF_PL01_XLKN_CT")]
    public class PHF_PL01_XLKN_CT : DataInfoEntityPHF
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

        [Column("TEN_DOITUONG")]
        [StringLength(500)]
        [Description("Tên đối tượng")]
        public string TEN_DOITUONG { get; set; }

        [Column("TONG_CUOC_THANHTRA")]
        [StringLength(500)]
        [Description("Tổng số cuốc thanh tra")]
        public string TONG_CUOC_THANHTRA { get; set; }

        [Column("TONG_DOAN_THANHTRA")]
        [StringLength(500)]
        [Description("Tổng số đoàn thanh tra")]
        public string TONG_DOAN_THANHTRA { get; set; }

        [Column("TD_DANGTHUCHIEN")]
        [StringLength(500)]
        [Description("Tiến độ đang thực hiện")]
        public string TD_DANGTHUCHIEN { get; set; }


        [Column("TD_DANGDUTHAO_KL")]
        [StringLength(500)]
        [Description("Tiến độ Đang dự thảo KL")]
        public string TD_DANGDUTHAO_KL { get; set; }


        [Column("TD_DACONGBODUTHAOKL")]
        [StringLength(500)]
        [Description("Tiến độ đã công bố dự thảo KL")]
        public string TD_DACONGBODUTHAOKL { get; set; }

        [Column("TD_DANGTRINHLANHDAOBO")]
        [StringLength(500)]
        [Description("Tiến độ Đang trình Lãnh đạo Bộ, Lãnh đạo TT Bộ")]
        public string TD_DANGTRINHLANHDAOBO { get; set; }

        [Column("DALUHANH_KL")]
        [StringLength(500)]
        [Description("Đã lưu hành kết luận")]
        public string DALUHANH_KL { get; set; }

        [Column("GHI_CHU")]
        [StringLength(500)]
        [Description("Ghi chú")]
        public string GHI_CHU { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }

    }
}
