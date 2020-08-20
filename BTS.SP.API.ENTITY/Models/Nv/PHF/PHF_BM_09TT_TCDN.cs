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
    [Table("PHF_BM_09TT_TCDN")]
    public class PHF_BM_09TT_TCDN :DataInfoEntityPHF
    {
        [Column("STT")]
        [Description("Số thứ tự")]
        public int? STT { get; set; }

        [Column("STT_TIEUDE")]
        [Description("Số thứ tự tiêu đề")]
        [StringLength(5)]
        public string STT_TIEUDE { get; set; }

        [Column("STT_CHA")]
        [Description("Số thứ tự cha")]
        public int? STT_CHA { get; set; }

        [Column("MA_FILE")]
        [StringLength(200)]
        [Description("Mã file Template")]
        public string MA_FILE { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }

        [Column("TEN_DONVI")]
        [StringLength(200)]
        [Description("Tên đơn vị")]
        public string TEN_DONVI { get; set; }

        [Column("DTVTN_HACHTOANTHIEU")]
        [StringLength(200)]
        [Description("Doanh thu và thu nhập khác-Hạch toán thiếu")]
        public string DTVTN_HACHTOANTHIEU { get; set; }

        [Column("DTVTN_TANGKHONGDUNG")]
        [StringLength(200)]
        [Description("Doanh thu và thu nhập khác-Tăng không đúng")]
        public string DTVTN_TANGKHONGDUNG { get; set; }

        [Column("CP_HACHTOANTHIEU")]
        [StringLength(200)]
        [Description("Chi phí-Hạch toán thiếu")]
        public string CP_HACHTOANTHIEU { get; set; }

        [Column("CP_TANGKHONGDUNG")]
        [StringLength(200)]
        [Description("Chi phí-Tăng không đúng")]
        public string CP_TANGKHONGDUNG { get; set; }

        [Column("LNTH_HACHTOANTHIEU")]
        [StringLength(200)]
        [Description("Lợi nhuận thực hiện-Hạch toán thiếu")]
        public string LNTH_HACHTOANTHIEU { get; set; }

        [Column("LNTH_TANGKHONGDUNG")]
        [StringLength(200)]
        [Description("Lợi nhuận thực hiện-Tăng không đúng")]
        public string LNTH_TANGKHONGDUNG { get; set; }
    }
}
