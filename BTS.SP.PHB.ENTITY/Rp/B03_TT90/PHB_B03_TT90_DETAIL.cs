using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.B03_TT90
{
    [Table("PHB_B03_TT90_DETAIL")]
    public class PHB_B03_TT90_DETAIL : BaseEntity
    {
        [Column("PHB_B03_TT90_REFID")]
        [Required]
        [Description("Guid ID trong  PHB_B03_TT90_REFID")]
        [StringLength(50)]
        public string PHB_B03_TT90_REFID { get; set; }

        [Column("MA_CHI_TIEU")]
        [StringLength(15)]
        public string MA_CHI_TIEU { get; set; }

        [Column("MA_CHI_TIEU_CHA")]
        [StringLength(15)]
        public string MA_CHI_TIEU_CHA { get; set; }

        [Column("MA_SO")]
        [Description("Mã số chỉ tiêu báo cáo")]
        [StringLength(15)]
        public string MA_SO { get; set; }

        [Column("TEN_CHI_TIEU")]
        [Description("Tên chỉ tiêu báo cáo")]
        [StringLength(250)]
        public string TEN_CHI_TIEU { get; set; }

        [Column("STT_CHI_TIEU")]
        [Description("STT chỉ tiêu báo cáo")]
        [StringLength(15)]
        public string STT_CHI_TIEU { get; set; }

        [Column("SAP_XEP")]
        [Description("Sắp xếp")]
        [Required]
        public int SAP_XEP { get; set; }

        [Column("INDAM")]
        public int INDAM { get; set; }

        [Column("INNGHIENG")]
        public int INNGHIENG { get; set; }

        [Column("DU_TOAN_NAM")]
        public decimal DU_TOAN_NAM { get; set; }

        [Column("UOC_THUC_HIEN")]
        public decimal UOC_THUC_HIEN { get; set; }

        [Column("UOC_THUC_HIEN_DU_TOAN")]
        public decimal UOC_THUC_HIEN_DU_TOAN { get; set; }

        [Column("UOC_THUC_HIEN_CUNG_KY")]
        public decimal UOC_THUC_HIEN_CUNG_KY { get; set; }
    }
}
