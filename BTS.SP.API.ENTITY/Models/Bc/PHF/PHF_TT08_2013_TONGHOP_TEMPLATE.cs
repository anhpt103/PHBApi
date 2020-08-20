using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_TT08_2013_TONGHOP_TEMPLATE")]
    public class PHF_TT08_2013_TONGHOP_TEMPLATE : DataInfoEntityPHF
    {

        [Column("STT")]
        [StringLength(10)]
        public string STT { get; set; }

        [Column("SAPXEP")]
        public int SAPXEP { get; set; }

        [Column("TEN_DONVI")]
        [StringLength(500)]
        public string TEN_DONVI { get; set; }

        [Column("MADONG")]
        [StringLength(50)]
        public string MADONG { get; set; }

        [Column("INDAM")]
        public Nullable<int> INDAM { get; set; }

        [Column("INNGHIENG")]
        public Nullable<int> INNGHIENG { get; set; }

    }
}
