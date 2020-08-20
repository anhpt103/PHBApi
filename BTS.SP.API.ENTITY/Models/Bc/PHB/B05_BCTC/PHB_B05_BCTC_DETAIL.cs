using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.B05_BCTC
{
    [Table("PHB_B05_BCTC_DETAIL")]
    public class PHB_B05_BCTC_DETAIL : DataInfoEntity
    {
        [Column("PHB_B05_BCTC_REFID")]
        [Required]
        [Description("Guid ID trong  PHB_B05_BCTC")]
        [StringLength(100)]
        public string PHB_B05_BCTC_REFID { get; set; }

        [Column("STT")]
        [Description("Số thứ tự chỉ tiêu")]
      
        public int STT { get; set; }
       
        [Column("MA_TABLE")]
        [Description("chỉ tiêu của tab và table nào")]
        [StringLength(50)]
        public string MA_TABLE { get; set; }
        [Column("MA_CHI_TIEU_CHA")]
        [StringLength(100)]
        public string MA_CHI_TIEU_CHA { get; set; }

        [Column("MA_SO")]
        [StringLength(50)]
        public string MA_SO { get; set; }


        [Column("MA_CHI_TIEU")]
        [Description("Mã chỉ tiêu")]
        [StringLength(50)]
        public string MA_CHI_TIEU { get; set; }


        [Column("TEN_CHI_TIEU")]
        [Description("Tên chỉ tiêu")]
        [StringLength(1000)]
        public string TEN_CHI_TIEU { get; set; }

        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }

        [Column("THUYET_MINH")]
        [Description("Thuyết Minh")]
        [StringLength(1000)]
        public string THUYET_MINH { get; set; }

        [Description("Cột tiền thứ 1 trong table")]
        public double COT_1 { get; set; }

        [Description("Cột tiền thứ 2 trong table")]
        public double COT_2 { get; set; }

      
    }
}
