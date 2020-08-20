using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_XD_KH_THANHTRA_BO_CHITIET")]
    public class PHF_XD_KH_THANHTRA_BO_CHITIET : DataInfoEntityPHF
    {
        [Column("MA_PHIEU")]
        [StringLength(50)]
        public string MA_PHIEU { get; set; }

        [Column("MA_DOITUONG")]
        [StringLength(50)]
        public string MA_DOITUONG { get; set; }

        [Column("MA_DOITUONG_CHA")]
        [StringLength(50)]
        public string MA_DOITUONG_CHA { get; set; }

        [Column("TEN_DOITUONG")]
        [StringLength(500)]
        public string TEN_DOITUONG { get; set; }

        [Column("PHONG_CHUTRI")]
        [StringLength(50)]
        public string PHONG_CHUTRI { get; set; }

        [Column("LY_DO")]
        [StringLength(1000)]
        public string LY_DO { get; set; }

        [Column("LOAI_DOITONG")]
        [StringLength(50)]
        public string LOAI_DOITONG { get; set; }

    }
}
