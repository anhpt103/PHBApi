using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_GIAMSAT_DOAN_TT_DINHKEM")]
    public class PHF_GIAMSAT_DOAN_TT_DINHKEM : DataInfoEntityPHF
    {
        [Column("MA_DOITUONG")]
        [StringLength(50)]
        public string MA_DOITUONG { get; set; }

        [Column("MA_DONVI")]
        [StringLength(50)]
        public string MA_DONVI { get; set; }

        [Column("NAM_THANHTRA")]
        public int NAM_THANHTRA { get; set; }

        [Column("FILE_PATH")]
        [StringLength(500)]
        public string FILE_PATH { get; set; }
    }
}
