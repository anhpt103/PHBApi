using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BIEU02_TONGHOP_TCDN")]
    public class PHF_BIEU02_TONGHOP_TCDN : DataInfoEntityPHF
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

        [Column("DOITUONG_THANHTRA")]
        [StringLength(1000)]
        [Description("Đối tượng thanh tra (Chi tiết đếnTên dự án/gói thầu)")]
        public string DOITUONG_THANHTRA { get; set; }

        [Column("QUYETDINH_THANHTRA")]
        [StringLength(1000)]
        [Description("Quyết định thanh tra (số… ngày/tháng/năm)")]
        public string QUYETDINH_THANHTRA { get; set; }

        [Column("KETLUAN_THANHTRA")]
        [StringLength(1000)]
        [Description("Kết luận thanh tra (số … ngày/tháng/năm)")]
        public string KETLUAN_THANHTRA { get; set; }

        [Column("MASO_DUAN")]
        [StringLength(1000)]
        [Description("Mã số dự án")]
        public string MASO_DUAN { get; set; }

        [Column("KIENNGHI_THUHOI")]
        [StringLength(1000)]
        [Description("Kiến nghị thu hồi về thuế, phí lệ phí")]
        public string KIENNGHI_THUHOI { get; set; }

        [Column("TONGMUC_TONGSO")]
        [StringLength(1000)]
        [Description("Kiến nghị giảm trừ tổng mức - Tổng số")]
        public string TONGMUC_TONGSO { get; set; }

        [Column("TONGMUC_DONGIA")]
        [StringLength(1000)]
        [Description("Kiến nghị giảm trừ tổng mức - Sai phạm về đơn giá")]
        public string TONGMUC_DONGIA { get; set; }

        [Column("TONGMUC_KHOILUONG")]
        [StringLength(1000)]
        [Description("Kiến nghị giảm trừ tổng mức - Sai phạm về khối lượng")]
        public string TONGMUC_KHOILUONG { get; set; }

        [Column("TONGMUC_KHAC")]
        [StringLength(1000)]
        [Description("Kiến nghị giảm trừ tổng mức - Sai phạm khác")]
        public string TONGMUC_KHAC { get; set; }

        [Column("DUTOAN_TONGSO")]
        [StringLength(1000)]
        [Description("Kiến nghị giảm trừ giá trị dự toán - Tổng số")]
        public string DUTOAN_TONGSO { get; set; }

        [Column("DUTOAN_DONGIA")]
        [StringLength(1000)]
        [Description("Kiến nghị giảm trừ giá trị dự toán - Sai phạm về đơn giá")]
        public string DUTOAN_DONGIA { get; set; }

        [Column("DUTOAN_KHOILUONG")]
        [StringLength(1000)]
        [Description("Kiến nghị giảm trừ giá trị dự toán - Sai phạm về khối lượng")]
        public string DUTOAN_KHOILUONG { get; set; }

        [Column("DUTOAN_KHAC")]
        [StringLength(1000)]
        [Description("Kiến nghị giảm trừ giá trị dự toán - Sai phạm khác")]
        public string DUTOAN_KHAC { get; set; }

        [Column("THANHTOAN_TONGSO")]
        [StringLength(1000)]
        [Description("Kiến nghị giảm trừ giá trị nghiệm thu thanh toán - Tổng số")]
        public string THANHTOAN_TONGSO { get; set; }

        [Column("THANHTOAN_DONGIA")]
        [StringLength(1000)]
        [Description("Kiến nghị giảm trừ giá trị nghiệm thu thanh toán - Sai phạm về đơn giá")]
        public string THANHTOAN_DONGIA { get; set; }

        [Column("THANHTOAN_KHOILUONG")]
        [StringLength(1000)]
        [Description("Kiến nghị giảm trừ giá trị nghiệm thu thanh toán - Sai phạm về khối lượng")]
        public string THANHTOAN_KHOILUONG { get; set; }

        [Column("THANHTOAN_KHAC")]
        [StringLength(1000)]
        [Description("Kiến nghị giảm trừ giá trị nghiệm thu thanh toán - Sai phạm khác")]
        public string THANHTOAN_KHAC { get; set; }

        [Column("KIENNGHI_SOTIEN")]
        [StringLength(1000)]
        [Description("Kiến nghị thu hồi NSNN - Số tiền")]
        public string KIENNGHI_SOTIEN { get; set; }

        [Column("KIENNGHI_NGUYENNHAN")]
        [StringLength(1000)]
        [Description("Kiến nghị thu hồi NSNN - Nguyên nhân")]
        public string KIENNGHI_NGUYENNHAN { get; set; }

        [Column("KIENNGHI_KHAC")]
        [StringLength(1000)]
        [Description("Kiến nghị khác")]
        public string KIENNGHI_KHAC { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }
    }
}
