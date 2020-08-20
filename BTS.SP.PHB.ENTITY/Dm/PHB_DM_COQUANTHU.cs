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
    [Table("TCS_DM_COQUANTHU")]
    public class PHB_DM_COQUANTHU: BaseEntity
    {
        [Column("MA")]
        [StringLength(20)]
        [Required]
        public string MA { get; set; }

        [Column("TEN")]
        [StringLength(500)]
        public string TEN { get; set; }

        [Column("TRANG_THAI")]
        [StringLength(1)]
        [DefaultValue("A")]
        public string TRANG_THAI { get; set; }
    }
}
