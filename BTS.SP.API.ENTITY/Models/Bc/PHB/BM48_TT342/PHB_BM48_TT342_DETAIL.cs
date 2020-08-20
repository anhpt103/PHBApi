
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.BIEU70NS
{
    public class PHB_BM48_TT342_DETAIL : DataInfoEntity
    {
        [Required]
        [Description("Guid ID trong  PHB_BM48_TT342")]
        [StringLength(50)]
        public string PHB_BM48_TT342_REFID { get; set; }

        [Description("STT")]
        public int STT { get; set; }


        [Description("Số thứ tự chỉ tiêu")]
        [StringLength(50)]
        public string STT_CHI_TIEU { get; set; }

        [Description("Mã chỉ tiêu")]
        [StringLength(50)]
        public string MA_CHI_TIEU { get; set; }

        [Description("Tên chỉ tiêu")]
        [StringLength(1000)]
        public string TEN_CHI_TIEU { get; set; }

        [Description("Số tiền năm trước, năm liền kề")]
        public double TONG_SO { get; set; }

    }
}
