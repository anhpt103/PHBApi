
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.B01B_TT137
{
    public class PHB_B01B_TT137_DETAIL : DataInfoEntity
    {
        [Required]
        [Description("Guid ID trong  PHB_B01B_TT137")]
        [StringLength(50)]
        public string PHB_B01B_TT137_REFID { get; set; }

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

        [Description("Số tiền dự toán")]
        public double? SO_DOI_CHIEU { get; set; }

        [Description("Số tiền báo cáo")]
        public double? THUC_HIEN { get; set; }

        [Description("Số tiền chênh lệch")]
        public double? CHENH_LECH { get; set; }

        [Description("Mã cha")]
        [StringLength(20)]
        public string MA_CHA { get; set; }

        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Column("IS_OPTIONAL")]
        public int IS_OPTIONAL { get; set; }
    }
}
