using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Dm.PHB
{
    [Table("PHB_DM_DVSDNS")]
    public class PHB_DM_DVSDNS : DataInfoEntity
    {
        [Column("MA_DVSDNS")]
        [Required]
        [StringLength(50)]
        public string MA_DVSDNS { get; set; }

        [Column("TEN_DVSDNS")]
        [Required]
        [StringLength(200)]
        public string TEN_DVSDNS { get; set; }

        [Column("MA_DVSDNS_CHA")]
        [StringLength(15)]
        public string MA_DVSDNS_CHA { get; set; }

        [Column("MA_CHUONG")]
        [StringLength(15)]
        public string MA_CHUONG { get; set; }

        [Column("CAP_DU_TOAN")]
        public int CAP_DU_TOAN { get; set; }

        //[Column("TRANG_THAI")]
        //[Required]
        //[Description("A: Sử dụng | I:Không sử dụng")]
        //[StringLength(1)]
        //public string TRANG_THAI { get; set; }
    }
}
