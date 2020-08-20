using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.CapNhatKienNghi_TamGiu
{
    [Table("PHF_KIENNGHI_KHONGSO")]
    public class PHF_KIENNGHI_KHONGSO : DataInfoEntityPHF
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

        [Column("SO_KETLUAN_THANHTRA")]
        [Description("Số kết luận thanh tra")]
        [StringLength(100)]
        public string SO_KETLUAN_THANHTRA { get; set; }

        [Column("NGAY_KETLUAN_THANHTRA")]
        [Description("Ngày kết luận thanh tra")]
        public DateTime? NGAY_KETLUAN_THANHTRA { get; set; }

        [Column("NGUOI_KIENNGHI")]
        [Description("Người kiến nghị")]
        [StringLength(50)]
        public string NGUOI_KIENNGHI { get; set; }

        [Column("NGAY_KIENNGHI")]
        [Description("Ngày kiến nghị")]
        public DateTime? NGAY_KIENNGHI { get; set; }

        [Column("NOIDUNG_KIENNGHI")]
        [Description("Nội dung kiến nghị")]
        [StringLength(2000)]
        public string NOIDUNG_KIENNGHI { get; set; }

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

        [Column("NOIDUNG_THUCHIEN")]
        [Description("Nội dung thực hiện kiến nghị")]
        [StringLength(2000)]
        public string NOIDUNG_THUCHIEN { get; set; }

        [Column("GHICHU_THUCHIEN")]
        [Description("Ghi chú thực hiện")]
        [StringLength(2000)]
        public string GHICHU_THUCHIEN { get; set; }

    }
}
