using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_TT188_PL01_TEMPLATE")]
    public class PHF_TT188_PL01_TEMPLATE : DataInfoEntityPHF
    {

        [Column("STT")]
        [StringLength(50)]
        public string STT { get; set; }

        [Column("SAPXEP")]
        public int SAPXEP { get; set; }

        [Column("NOIDUNG")]
        [StringLength(2000)]
        public string NOIDUNG { get; set; }

        [Column("MADONG")]
        [StringLength(50)]
        public string MADONG { get; set; }

        [Column("INDAM")]
        public Nullable<int> INDAM { get; set; }

        [Column("INNGHIENG")]
        public Nullable<int> INNGHIENG { get; set; }

    }
}
