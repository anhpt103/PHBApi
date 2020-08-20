using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_TT188_PL03_TEMPLATE")]
    public class PHF_TT188_PL03_TEMPLATE : DataInfoEntityPHF
    {
        [Column("STT")]
        [StringLength(10)]
        public string STT { get; set; }

        [Column("SAPXEP")]
        public int SAPXEP { get; set; }

        [Column("NOIDUNG")]
        [StringLength(500)]
        public string NOIDUNG { get; set; }

        [Column("DONVITINH")]
        [StringLength(50)]
        public string DONVITINH { get; set; }

        [Column("MADONG")]
        [StringLength(50)]
        public string MADONG { get; set; }

        [Column("INDAM")]
        public Nullable<int> INDAM { get; set; }

        [Column("INNGHIENG")]
        public Nullable<int> INNGHIENG { get; set; }
    }
}
