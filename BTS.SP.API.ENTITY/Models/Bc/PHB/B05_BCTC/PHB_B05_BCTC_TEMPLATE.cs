using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.B05_BCTC
{
    [Table("PHB_B05_BCTC_TEMPLATE")]
    public class PHB_B05_BCTC_TEMPLATE : DataInfoEntity
    {
        [Column("STT_SAP_XEP")]           
        public int STT_SAP_XEP { get; set; }


        // dùng để phân biệt các tab 
        [Column("MA_TABLE")]
        [StringLength(20)]
        public string MA_TABLE { get; set; }


        [Column("MA_CHI_TIEU")]
        [StringLength(100)]
        public string MA_CHI_TIEU { get; set; }

        [Column("MA_CHI_TIEU_CHA")]
        [StringLength(100)]
        public string MA_CHI_TIEU_CHA { get; set; }

        [Column("CHI_TIEU")]
        [StringLength(1000)]
        public string CHI_TIEU { get; set; }

        [Column("MA_SO")]
        [StringLength(50)]
        public string MA_SO { get; set; }

        [Column("IS_BOLD")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        public int IS_ITALIC { get; set; }


    }
}
