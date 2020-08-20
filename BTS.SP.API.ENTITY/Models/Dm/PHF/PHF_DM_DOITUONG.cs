using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Dm.PHF
{
    [Table("PHF_DM_DOITUONG")]
    public class PHF_DM_DOITUONG : DataInfoEntityPHF
    {
        [Required]
        [Column("MA_DOITUONG")]
        [StringLength(50)]
        [Description("Mã đối tượng")]
        public string MA_DOITUONG { get; set; }

        [Column("MA_DOITUONG_CHA")]
        [StringLength(50)]
        [Description("Mã đối tượng cha")]
        public string MA_DOITUONG_CHA { get; set; }

        [Column("MA_DIABAN")]
        [StringLength(50)]
        [Description("Mã địa bàn")]
        public string MA_DIABAN { get; set; }

        [Required]
        [Column("TEN_DOITUONG")]
        [StringLength(500)]
        [Description("Tên đối tượng")]
        public string TEN_DOITUONG { get; set; }

        [Required]
        [Column("NAM")]
        [Description("Năm")]
        public int NAM { get; set; }

        [Column("MA_DVQHNS")]
        [StringLength(50)]
        [Description("Mã đơn vị quan hệ ngân sách")]
        public string MA_DVQHNS { get; set; }

        [Column("TRANG_THAI")]
        [Description("Trạng thái")]
        public Nullable<int> TRANG_THAI { get; set; }

        [Column("MA_LINHVUC")]
        [StringLength(50)]
        [Description("Lĩnh vực thanh tra")]
        public string MA_LINHVUC { get; set; }

        [Column("DIA_CHI")]
        [Description("Địa chỉ")]
        [StringLength(500)]
        public string DIA_CHI { get; set; }

        [Column("TEN_NGUOI_DAIDIEN")]
        [Description("Tên người đại diện")]
        [StringLength(500)]
        public string TEN_NGUOI_DAIDIEN { get; set; }

        [Column("SDT")]
        [Description("Số điện thoại")]
        [StringLength(50)]
        public string SDT { get; set; }

        [Column("MASO_THUE")]
        [Description("Mã số thuế")]
        [StringLength(50)]
        public string MASO_THUE { get; set; }

        [Column("DONVI_CHUQUAN")]
        [Description("Đơn vị chủ quản")]
        [StringLength(500)]
        public string DONVI_CHUQUAN { get; set; }

        [Column("TAIKHOAN_SO")]
        [Description("Tài khoản số")]
        [StringLength(50)]
        public string TAIKHOAN_SO { get; set; }

        [Column("DIACHI_GIAODICH")]
        [Description("Địa chỉ giao dịch")]
        [StringLength(500)]
        public string DIACHI_GIAODICH { get; set; }

        [Column("MA_NDKT")]
        [Description("Mã NDKT")]
        [StringLength(50)]
        public string MA_NDKT { get; set; }

        [Column("MA_CHUONG")]
        [Description("Mã chương")]
        [StringLength(50)]
        public string MA_CHUONG { get; set; }

        [Column("MA_DUAN")]
        [Description("Mã dự án")]
        [StringLength(50)]
        public string MA_DUAN { get; set; }

        [Column("TEN_DUAN")]
        [Description("Tên dự án")]
        [StringLength(500)]
        public string TEN_DUAN { get; set; }

        [Column("NHOM_DUAN")]
        [Description("Nhóm dự án")]
        [StringLength(500)]
        public string NHOM_DUAN { get; set; }

        [Column("CAP_QD_DAUTU")]
        [Description("Cấp quyết định đầu tư")]
        [StringLength(500)]
        public string CAP_QD_DAUTU { get; set; }

        [Column("CHU_DAUTU")]
        [Description("Chủ đầu tư")]
        [StringLength(500)]
        public string CHU_DAUTU { get; set; }

        [Column("DIADIEM_THUCHIEN_DA")]
        [Description("Địa điểm thực hiện dự án")]
        [StringLength(500)]
        public string DIADIEM_THUCHIEN_DA { get; set; }

        [Column("THOIGIAN_KHOICONG_HOANTHANH")]
        [Description("Thời gian khởi công hoàng thành")]
        [StringLength(500)]
        public string THOIGIAN_KHOICONG_HOANTHANH { get; set; }

        [Column("NGUON_VON")]
        [Description("Nguồn vốn")]
        [StringLength(500)]
        public string NGUON_VON { get; set; }

        [Column("TONGMUC_DAUTU")]
        [Description("Tổng mức đầu tư")]
        [StringLength(500)]
        public string TONGMUC_DAUTU { get; set; }

        [Column("GIATRI_HOPDONG")]
        [Description("Giá trị hợp đồng")]
        [StringLength(500)]
        public string GIATRI_HOPDONG { get; set; }

        [Column("KEHOACH_VON")]
        [Description("Kế hoạch vốn")]
        [StringLength(500)]
        public string KEHOACH_VON { get; set; }

        [Column("KHOILUONG_THUCHIEN")]
        [Description("Khối lượng thực hiện")]
        [StringLength(500)]
        public string KHOILUONG_THUCHIEN { get; set; }

        [Column("GIATRI_THANHTOAN")]
        [Description("Giá trị thanh toán")]
        [StringLength(500)]
        public string GIATRI_THANHTOAN { get; set; }

        [Column("MHTC_DDHD")]
        [Description("MÔ HÌNH TỔ CHỨC VÀ ĐẶC ĐIỂM HOẠT ĐỘNG")]
        [StringLength(5000)]
        public string MHTC_DDHD { get; set; }

        [Column("COCHE_QL_TC")]
        [Description("VỀ CƠ CHẾ QUẢN LÝ ĐIỀU HÀNH VÀ CƠ CHẾ TÀI CHÍNH ")]
        [StringLength(5000)]
        public string COCHE_QL_TC { get; set; }
    }
}
