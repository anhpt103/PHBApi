using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.XLKN
{
    [Table("PHF_PL02_XLKN_CT")]
    public class PHF_PL02_XLKN_CT : DataInfoEntityPHF
    {
        [Column("MA_FILE")]
        [StringLength(100)]
        [Description("Mã báo cáo")]
        public string MA_FILE { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("QUY")]
        [StringLength(50)]
        [Description("Quý")]
        public string QUY { get; set; }

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
        [Description("Đơn vị được thanh tra, kiểm tra")]
        public string DONVI_DUOC_THANHTRA { get; set; }

        [Column("KETLUAN_THANHTRA")]
        [StringLength(200)]
        [Description("kết luận thanh tra")]
        public string KETLUAN_THANHTRA { get; set; }

        [Column("TONG_CONG")]
        [Description("Tổng cộng")]
        public Nullable<decimal> TONG_CONG { get; set; }

        [Column("NSNN_CONG")]
        [Description("NSNN cộng")]
        public Nullable<decimal> NSNN_CONG { get; set; }

        [Column("NSNN_THUTHUE")]
        [Description("NSNN thu thuế")]
        public Nullable<decimal> NSNN_THUTHUE { get; set; }

        [Column("NSNN_LEPHI")]
        [Description("NSNN lệ phí")]
        public Nullable<decimal> NSNN_LEPHI { get; set; }

        [Column("NSNN_PHAT_VIPHAMHC")]
        [Description("NSNN phạt vi phạm hành chính")]
        public Nullable<decimal> NSNN_PHAT_VIPHAMHC { get; set; }

        [Column("NSNN_CHI_KHONG_DUNG_CHE_DO")]
        public Nullable<decimal> NSNN_CHI_KHONG_DUNG_CHE_DO { get; set; }

        [Column("NSNN_KHAC")]
        public Nullable<decimal> NSNN_KHAC { get; set; }

        [Column("GTDT_CONG")]
        public Nullable<decimal> GTDT_CONG { get; set; }

        [Column("GTDT_TDDT_KODUNG")]
        public Nullable<decimal> GTDT_TDDT_KODUNG { get; set; }

        [Column("GTDT_KHAC")]
        public Nullable<decimal> GTDT_KHAC { get; set; }

        [Column("GTQT_CONG")]
        public Nullable<decimal> GTQT_CONG { get; set; }

        [Column("GTQT_KODUNG_CHEDO")]
        public Nullable<decimal> GTQT_KODUNG_CHEDO { get; set; }

        [Column("GTQT_NGHIEMTHU_KODUNG")]
        public Nullable<decimal> GTQT_NGHIEMTHU_KODUNG { get; set; }

        [Column("GTQT_KHAC")]
        public Nullable<decimal> GTQT_KHAC { get; set; }

        [Column("KNTCK_CONG")]
        public Nullable<decimal> KNTCK_CONG { get; set; }

        [Column("KNTCK_XULY_HACHTOAN")]
        public Nullable<decimal> KNTCK_XULY_HACHTOAN { get; set; }

        [Column("KNTCK_GIAM_GT_CONGTRINH")]
        public Nullable<decimal> KNTCK_GIAM_GT_CONGTRINH { get; set; }

        [Column("KNTCK_KHAC")]
        public Nullable<decimal> KNTCK_KHAC { get; set; }

        [Column("GHI_CHU")]
        [StringLength(500)]
        public string GHI_CHU { get; set; }

        [Column("QD_TT_SO_NGAY")]
        [StringLength(200)]
        public string QD_TT_SO_NGAY { get; set; }

        [Column("TRUONG_DOAN_TT")]
        [StringLength(200)]
        public string TRUONG_DOAN_TT { get; set; }

        [Column("TV_DOAN_TT")]
        [StringLength(500)]
        public string TV_DOAN_TT { get; set; }

        [Column("DONVI_DUOC_TT")]
        [StringLength(500)]
        public string DONVI_DUOC_TT { get; set; }

        [Column("QUY_III_TK")]
        [StringLength(500)]
        public string QUY_III_TK { get; set; }

        [Column("QUY_III_KL")]
        [StringLength(500)]
        public string QUY_III_KL { get; set; }

        [Column("CHIN_THANG_TK")]
        [StringLength(500)]
        public string CHIN_THANG_TK { get; set; }

        [Column("CHIN_THANG_KL")]
        [StringLength(500)]
        public string CHIN_THANG_KL { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }
    }
}
