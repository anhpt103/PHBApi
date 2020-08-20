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
    [Table("TCS_DM_TYLEDIEUTIET")]
    public class PHB_DM_TYLEDIEUTIET: BaseEntity
    {
        [Column("MA")]
        [Required]
        [StringLength(20)]
        public string MA { get; set; }

        [Column("TYLE_DIEUTIET")]
        [StringLength(30)]
        [Description("Tỷ lệ điều tiết")]
        public string TYLE_DIEUTIET { get; set; }

        [Column("TRANG_THAI")]
        [StringLength(1)]
        [DefaultValue("A")]
        public string TRANG_THAI { get; set; }
    }
}
