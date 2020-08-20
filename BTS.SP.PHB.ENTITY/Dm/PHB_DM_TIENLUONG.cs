using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Dm
{
    [Table("PHB_DM_TIENLUONG")]
    public class PHB_DM_TIENLUONG : BaseEntity
    {
        [Column("MA_TIEN_LUONG")]
        [StringLength(15)]
        [Required]
        public string MA_TIEN_LUONG { get; set; }

        [Column("MUC_LUONG_TT")]
        [Description("Mức lương tối thiểu")]
        public decimal? MUC_LUONG_TT { get; set; }

        [Column("GIAM_TRU")]
        [Description("Giảm trừ")]
        public decimal? GIAM_TRU { get; set; }

        [Column("BHXH_CQD")]
        [Description("BHXH cơ quan đóng")]
        public decimal? BHXH_CQD { get; set; }

        [Column("BHYT_CQD")]
        [Description("BHYT cơ quan đóng")]
        public decimal? BHYT_CQD { get; set; }

        [Column("BHTN_CQD")]
        [Description("BHTN cơ quan đóng")]
        public decimal? BHTN_CQD { get; set; }

        [Column("KP_CD_CQD")]
        [Description("Kinh phí công đoàn cơ quan đóng")]
        public decimal? KP_CD_CQD { get; set; }

        [Column("BHXH_NLDD")]
        [Description("BHXH người lao động đóng")]
        public decimal? BHXH_NLDD { get; set; }

        [Column("BHYT_NLDD")]
        [Description("BHYT người lao động đóng")]
        public decimal? BHYT_NLDD { get; set; }

        [Column("BHTN_NLDD")]
        [Description("BHTN người lao động đóng")]
        public decimal? BHTN_NLDD { get; set; }

        [Column("KP_CD_NLDD")]
        [Description("Kinh phí công đoàn người lao động đóng")]
        public decimal? KP_CD_NLDD { get; set; }

        [Column("TRANG_THAI")]
        [Required]
        [StringLength(1)]
        public string TRANG_THAI { get; set; }
    }
}
