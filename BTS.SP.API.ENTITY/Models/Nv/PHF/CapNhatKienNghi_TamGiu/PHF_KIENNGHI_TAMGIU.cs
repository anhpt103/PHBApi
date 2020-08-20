using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.CapNhatKienNghi_TamGiu
{
    [Table("PHF_KIENNGHI_TAMGIU")]
    public class PHF_KIENNGHI_TAMGIU : DataInfoEntityPHF
    {
        [Column("MAPHONGBAN")]
        [StringLength(50)]
        [Description("Mã phòng ban")]
        public string MAPHONGBAN { get; set; }

        [Column("MA_DOITUONG")]
        [StringLength(50)]
        [Description("Mã đối tượng")]
        public string MA_DOITUONG { get; set; }

        [Column("MA_DOITUONG_CHA")]
        [StringLength(50)]
        [Description("Mã đối tượng cha")]
        public string MA_DOITUONG_CHA { get; set; }

        [Column("NAMKETLUAN")]
        [Description("Năm kết luận")]
        public int? NAMKETLUAN { get; set; }

        [Column("NAM")]
        [Description("Năm")]
        public int? NAM { get; set; }

        [Column("THANG")]
        [Description("Tháng")]
        public int? THANG { get; set; }

        [DefaultValue(0)]
        [Column("NSNN_THUEGTGT")]
        [Description("Nộp ngân sách nhà nước thuế GTGT ")]
        public decimal? NSNN_THUEGTGT { get; set; }

        [DefaultValue(0)]
        [Column("NSNN_THUETNDN")]
        [Description("Nộp ngân sách nhà nước thuế TNDN ")]
        public decimal? NSNN_THUETNDN { get; set; }


        [DefaultValue(0)]
        [Column("NSNN_THUEXNK")]
        [Description("Nộp ngân sách nhà nước thuế XNK ")]
        public decimal? NSNN_THUEXNK { get; set; }


        [DefaultValue(0)]
        [Column("NSNN_THUETN")]
        [Description("Nộp ngân sách nhà nước thuế TN ")]
        public decimal? NSNN_THUETN { get; set; }


        [DefaultValue(0)]
        [Column("NSNN_THUEKHAC")]
        [Description("Nộp ngân sách nhà nước thuế Khác ")]
        public decimal? NSNN_THUEKHAC { get; set; }


        [DefaultValue(0)]
        [Column("NSNN_KHOANKHAC")]
        [Description("Nộp ngân sách nhà nước Khoản khác ")]
        public decimal? NSNN_KHOANKHAC { get; set; }


        [DefaultValue(0)]
        [Column("NSNN")]
        [Description("Nộp ngân sách nhà nước ")]
        public decimal? NSNN { get; set; }


        [DefaultValue(0)]
        [Column("GHITHUCHI")]
        [Description("Ghi thu ghi chi ")]
        public decimal? GHITHUCHI { get; set; }


        [DefaultValue(0)]
        [Column("GIAMDUTOAN")]
        [Description("Giảm dự toán ")]
        public decimal? GIAMDUTOAN { get; set; }


        [DefaultValue(0)]
        [Column("GIAMQUYETTOAN")]
        [Description("Giảm Quyết toán ")]
        public decimal? GIAMQUYETTOAN { get; set; }


        [DefaultValue(0)]
        [Column("THUVECP")]
        [Description("Thu về cổ phần hóa ")]
        public decimal? THUVECP { get; set; }


        [DefaultValue(0)]
        [Column("KIENNGHI_KHAC")]
        [Description("Kiến nghị khác")]
        public decimal? KIENNGHI_KHAC { get; set; }


        [DefaultValue(0)]
        [Column("GHITHUCHI_THUCHIEN")]
        [Description("Ghi thu ghi chi thực hiện ")]
        public decimal? GHITHUCHI_THUCHIEN { get; set; }


        [DefaultValue(0)]
        [Column("GIAMDUTOAN_THUCHIEN")]
        [Description("Giảm dự toán thực hiện")]
        public decimal? GIAMDUTOAN_THUCHIEN { get; set; }


        [DefaultValue(0)]
        [Column("GIAMQUYETTOAN_THUCHIEN")]
        [Description("Giảm Quyết toán thực hiện")]
        public decimal? GIAMQUYETTOAN_THUCHIEN { get; set; }


        [DefaultValue(0)]
        [Column("THUVECP_THUCHIEN")]
        [Description("Thu về cổ phần hóa thực hiện")]
        public decimal? THUVECP_THUCHIEN { get; set; }


        [DefaultValue(0)]
        [Column("KIENNGHI_KHAC_THUCHIEN")]
        [Description("Kiến nghị khác thực hiện")]
        public decimal? KIENNGHI_KHAC_THUCHIEN { get; set; }


        [DefaultValue(0)]
        [Column("NSNN_THUEGTGT_THUCHIEN")]
        [Description("Nộp ngân sách nhà nước thuế GTGT thực hiện")]
        public decimal? NSNN_THUEGTGT_THUCHIEN { get; set; }

        [DefaultValue(0)]
        [Column("NSNN_THUETNDN_THUCHIEN")]
        [Description("Nộp ngân sách nhà nước thuế TNDN thực hiện")]
        public decimal? NSNN_THUETNDN_THUCHIEN { get; set; }


        [DefaultValue(0)]
        [Column("NSNN_THUEXNK_THUCHIEN")]
        [Description("Nộp ngân sách nhà nước thuế XNK thực hiện")]
        public decimal? NSNN_THUEXNK_THUCHIEN { get; set; }


        [DefaultValue(0)]
        [Column("NSNN_THUETN_THUCHIEN")]
        [Description("Nộp ngân sách nhà nước thuế TN thực hiện")]
        public decimal? NSNN_THUETN_THUCHIEN { get; set; }


        [DefaultValue(0)]
        [Column("NSNN_THUEKHAC_THUCHIEN")]
        [Description("Nộp ngân sách nhà nước thuế Khác thực hiện")]
        public decimal? NSNN_THUEKHAC_THUCHIEN { get; set; }


        [DefaultValue(0)]
        [Column("NSNN_KHOANKHAC_THUCHIEN")]
        [Description("Nộp ngân sách nhà nước Khoản khác thực hiện")]
        public decimal? NSNN_KHOANKHAC_THUCHIEN { get; set; }


        [DefaultValue(0)]
        [Column("NSNN_THUCHIEN")]
        [Description("Nộp ngân sách nhà nước thực hiện")]
        public decimal? NSNN_THUCHIEN { get; set; }


        [DefaultValue(0)]
        [Column("TONGSO")]
        [Description("Tổng số ")]
        public decimal? TONGSO { get; set; }

        [DefaultValue(0)]
        [Column("TONGSO_THUCHIEN")]
        [Description("Tổng số thực hiện")]
        public decimal? TONGSO_THUCHIEN { get; set; }

        [Column("SO_KETLUAN_THANHTRA")]
        [Description("Số kết luận thanh tra")]
        [StringLength(100)]
        public string SO_KETLUAN_THANHTRA { get; set; }

        [Column("NGAY_KETLUAN_THANHTRA")]
        [Description("Ngày kết luận thanh tra")]
        public DateTime? NGAY_KETLUAN_THANHTRA { get; set; }

        [Column("MASOTHUE")]
        [StringLength(80)]
        public string MASOTHUE { get; set; }

        [Column("SO_QUYETDINH_THU")]
        [StringLength(80)]
        public string SO_QUYETDINH_THU { get; set; }

        [Column("NGAY_QUYETDINH_THU")]
        public DateTime? NGAY_QUYETDINH_THU { get; set; }

        [DefaultValue(0)]
        [Column("GIATRI_QUYETDINH_THU")]
        public decimal? GIATRI_QUYETDINH_THU { get; set; }

        [Column("MA_NDKT")]
        [StringLength(70)]
        public string MA_NDKT { get; set; }

        [Column("MA_CHUONG")]
        [StringLength(80)]
        public string MA_CHUONG { get; set; }

        [Column("COQUAN_QUANLYTHU")]
        [StringLength(150)]
        public string COQUAN_QUANLYTHU { get; set; }

        [Column("KHOBAC")]
        [StringLength(150)]
        public string KHOBAC { get; set; }

        [Column("SO_CHUNGTU")]
        [StringLength(70)]
        public string SO_CHUNGTU { get; set; }

        [Column("NGAY_CHUNGTU")]
        public DateTime? NGAY_CHUNGTU { get; set; }

        [Column("NOP_TKTG")]
        public decimal? NOP_TKTG { get; set; }

        [Column("NGAY_NOP_NSNN")]
        public DateTime? NGAY_NOP_NSNN { get; set; }

        [Column("NGAY_XULY_NOP_NSNN")]
        public DateTime? NGAY_XULY_NOP_NSNN { get; set; }

        [Column("SO_CHUNGTU_NOP_NSNN")]
        [StringLength(70)]
        public string SO_CHUNGTU_NOP_NSNN { get; set; }

        [Column("SAPXEP")]
        public int? SAPXEP { get; set; }

        [Column("LOAI_KIEN_NGHI")]
        [Description("Loại kiến nghị dành cho kiến nghị không số")]
        [StringLength(70)]
        public string LOAI_KIEN_NGHI { get; set; }

        [Column("ND_KIEN_NGHI")]
        [Description("Nội dung kiến nghị dành cho kiến nghị không số")]
        [StringLength(4000)]
        public string ND_KIEN_NGHI { get; set; }

        [Column("ND_THUC_HIEN")]
        [Description("Nội dung thực hiện dành cho kiến nghị không số")]
        [StringLength(2000)]
        public string ND_THUC_HIEN { get; set; }

        [Column("NGUOI_KIENNGHI")]
        [Description("Người kiến nghị")]
        [StringLength(50)]
        public string NGUOI_KIENNGHI { get; set; }

        [Column("NGAY_KIENNGHI")]
        [Description("Ngày kiến nghị")]
        public DateTime? NGAY_KIENNGHI { get; set; }

        [Column("GHICHU_KIENNGHI")]
        [Description("Ghi chú kiến nghị")]
        [StringLength(2000)]
        public string GHICHU_KIENNGHI { get; set; }

        [Column("NGUOI_XULY")]
        [Description("Người xử lý kiến nghị")]
        [StringLength(50)]
        public string NGUOI_XULY { get; set; }

        [Column("NGAY_XULY")]
        [Description("Ngày xử lý kiến nghị")]
        public DateTime? NGAY_XULY { get; set; }

    }
}
