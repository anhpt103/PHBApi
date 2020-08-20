using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.BIEU04_TT61
{
    public class PHB_BIEU04_TT61_DETAILS: DataInfoEntity
    {
        [Required]
        [Description("Guid ID trong  PHB_BIEU04_TT61")]
        [StringLength(50)]
        public string PHB_BIEU04_TT61_REFID { get; set; }

        [Description("STT")]
        public int STT { get; set; }

        [Description("Số thứ tự chỉ tiêu")]
        [StringLength(50)]
        public string STT_CHI_TIEU { get; set; }

        [Description("Nội dung")]
        [StringLength(1000)]
        public string TEN_CHI_TIEU { get; set; }

        [Description("Số liệu báo cáo quyết toán")]
        public double SO_BAO_CAO_QT { get; set; }

        [Description("Số liệu quyết toán được duyệt")]
        public double SO_QT_DUOC_DUYET { get; set; }

        [Description("Quỹ lương")]
        public double QUY_LUONG { get; set; }

        [Description("Mua sắm, sửa chữa")]
        public double MUA_SUA { get; set; }

        [Description("Trích lập các quỹ")]
        public double TRICH_LAP_QUY { get; set; }
    }
}
