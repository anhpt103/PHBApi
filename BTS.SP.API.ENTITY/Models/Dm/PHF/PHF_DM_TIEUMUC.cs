using BTS.SP.API.ENTITY.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Dm.PHF
{
    [Table("PHF_DM_TIEUMUC")]
    public class PHF_DM_TIEUMUC : DataInfoEntityPHF
    {
        [Column("MA_TIEUMUC")]
        [StringLength(50)]
        [Description("Mã tiểu mục")]
        [ExportExcel("Mã tiểu mục")]
        public string MA_TIEUMUC { get; set; }

        [Column("NGAY_HL")]
        [Description("Ngày hiệu lực")]
        public DateTime NGAY_HL { get; set; }

        [Column("NGAY_HET_HL")]
        [Description("Ngày hết hiệu lực")]
        public Nullable<DateTime> NGAY_HET_HL { get; set; }

        [Column("TEN_TIEUMUC")]
        [StringLength(500)]
        [Description("Tên tiêu mục")]
        [ExportExcel("Tên tiểu mục")]
        public string TEN_TIEUMUC { get; set; }

        [Column("TRANG_THAI")]
        [Description("Trạng thái sử dụng (A: Đang sử dụng; I: Không sử dụng)")]
        [StringLength(50)]
        public string TRANG_THAI { get; set; }

        [Column("MA_MUC")]
        [StringLength(50)]
        [Description("Mã mục")]
        [ExportExcel("Mã mục")]
        public string MA_MUC { get; set; }

        [Column("GHI_CHU")]
        [StringLength(500)]
        [Description("Ghi chú")]
        public string GHI_CHU { get; set; }

        [Column("USER_NAME")]
        [StringLength(50)]
        [Description("Người tạo")]
        public string USER_NAME { get; set; }

        [Column("NGAY_PS")]
        [Description("Ngày khởi tạo")]
        public Nullable<DateTime> NGAY_PS { get; set; }

        [Column("NGAY_SD")]
        [Description("Ngày cập nhật cuối cùng")]
        public Nullable<DateTime> NGAY_SD { get; set; }

        [Column("LOAI")]
        [StringLength(500)]
        [Description("Loại")]
        public string LOAI { get; set; }

        [Column("LOAI_KHOANTHU")]
        [StringLength(500)]
        [Description("Loại khoản thu")]
        public string LOAI_KHOANTHU { get; set; }
    }
}
