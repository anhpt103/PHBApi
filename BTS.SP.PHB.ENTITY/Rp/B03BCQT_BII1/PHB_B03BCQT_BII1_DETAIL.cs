using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.B03BCQT_BII1
{
    [Table("PHB_B03BCQT_BII1_DETAIL")]
    public class PHB_B03BCQT_BII1_DETAIL : BaseEntity
    {
        [Required]
        [Description("Guid ID trong PHB_B03BCQT_BII1")]
        [StringLength(50)]
        public string PHB_B03BCQT_BII1_REFID { get; set; }

        [Required]
        [Description("Loại chỉ tiêu: 1 PHÍ, 2 LỆ PHÍ")]
        public int LOAI { get; set; }

        [Description("Mã mục, tiểu mục")]
        [StringLength(4)]
        [Required]
        public string MA_NOIDUNGKT { get; set; }

        [Description("Số thứ tự chỉ tiêu: 1.1,1.2,...")]
        [StringLength(50)]
        public string STT_CHI_TIEU { get; set; }

        [Description("Tên mục, tiểu mục")]
        [StringLength(255)]
        public string TEN_NOIDUNGKT { get; set; }

        [Column("SAP_XEP")]
        [Description("Sắp xếp")]
        public int SAP_XEP { get; set; }

        [Description("Tổng tiền thu")]
        public double TONG_THU { get; set; }

        [Description("Tiền nộp NSNN")]
        public double TIEN_NSNN { get; set; }

        [Description("Tiền khấu trừ, để lại")]
        public double TIEN_KHAUTRU { get; set; }

        [Description("Ghi chú")]
        [StringLength(200)]
        public string GHI_CHU { get; set; }

    }
}
