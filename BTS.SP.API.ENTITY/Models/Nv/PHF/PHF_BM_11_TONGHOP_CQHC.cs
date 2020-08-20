using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BM_11_TONGHOP_CQHC")]
    public class PHF_BM_11_TONGHOP_CQHC: DataInfoEntityPHF
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

        [Column("SO_TOKHAI")]
        [StringLength(1000)]
        [Description("Số tờ khai")]
        public string SO_TOKHAI { get; set; }

        [Column("NGAY_DANGKY")]
        [Description("Ngày đăng ký")]
        public DateTime NGAY_DANGKY { get; set; }

        [Column("LOAIHINH")]
        [StringLength(1000)]
        [Description("Loại hình")]
        public string LOAIHINH { get; set; }

        [Column("TEN_CONGTY")]
        [StringLength(1000)]
        [Description("Tên Cty")]
        public string TEN_CONGTY { get; set; }

        [Column("TEN_HANGKHAIBAO")]
        [StringLength(1000)]
        [Description("Tên hàng khai báo")]
        public string TEN_HANGKHAIBAO { get; set; }

        [Column("MABIEU_THUENK")]
        [StringLength(1000)]
        [Description("Mã Biểu thuế NK")]
        public string MABIEU_THUENK { get; set; }

        [Column("TRIGIA_TINHTHUE")]
        [Description("Trị giá tính thuế (cơ quan hải quan theo dõi)")]
        public decimal TRIGIA_TINHTHUE { get; set; }

        [Column("DANGKB_MAHS")]
        [StringLength(1000)]
        [Description("Đang khai báo Mã HS")]
        public string DANGKB_MAHS { get; set; }

        [Column("DANGKB_THUESUAT_THUENK")]
        [Description("Đang khai báo thuế suất thuế NK ")]
        public decimal DANGKB_THUESUAT_THUENK { get; set; }

        [Column("DANGKB_THUESUAT_THUEVAT")]
        [Description("Đang khai báo thuế suất thuế VAT")]
        public decimal DANGKB_THUESUAT_THUEVAT { get; set; }

        [Column("DEXUATDC_MAHS")]
        [StringLength(1000)]
        [Description("Đề xuất điều chỉnh mã HS ")]
        public string DEXUATDC_MAHS { get; set; }

        [Column("DEXUATDC_THUESUAT_THUENK")]
        [Description("Đề xuất điều chỉnh thuế suất thuế NK")]
        public decimal DEXUATDC_THUESUAT_THUENK { get; set; }

        [Column("DEXUATDC_THUESUAT_THUEVAT")]
        [Description("Đề xuất điều chỉnh thuế suất thuế VAT")]
        public decimal DEXUATDC_THUESUAT_THUEVAT { get; set; }

        [Column("RASOATTANGTHU_THUENK")]
        [Description("Rà soát tăng thu thuế NK")]
        public decimal RASOATTANGTHU_THUENK { get; set; }

        [Column("RASOATTANGTHU_THUEGTGT")]
        [Description("Rà soát tăng thu thuế GTGT")]
        public decimal RASOATTANGTHU_THUEGTGT { get; set; }

    }
}
