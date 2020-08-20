using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.B01BDG_TT137
{
    public class PHB_B01BDG_TT137_TEMPLATE : DataInfoEntity
    {
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

        [Description("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Description("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Description("PHAN")]
        public int PHAN { get; set; }

        [Description("LOAI")]
        public int LOAI { get; set; }

        [Description("Mã cha")]
        [StringLength(20)]
        public string MA_CHA { get; set; }

    }
}
