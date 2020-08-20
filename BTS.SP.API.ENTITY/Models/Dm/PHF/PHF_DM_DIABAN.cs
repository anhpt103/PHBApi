using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Dm.PHF
{
    [Table("PHF_DM_DIABAN")]

    public class PHF_DM_DIABAN : DataInfoEntityPHF
    {
        [Column("MA_DIABAN")]
        [StringLength(20)]
        public string MA_DIABAN { get; set; }

        [Column("MA_DIABAN_CHA")]
        [StringLength(20)]
        public string MA_DIABAN_CHA { get; set; }

        [Column("TEN_DIABAN")]
        [StringLength(300)]
        public string TEN_DIABAN { get; set; }

        [Column("CAP")]
        public int CAP { get; set; }

        [Column("TRANGTHAI")]
        [Description("Trạng thái")]
        public Nullable<int> TRANGTHAI { get; set; }
    }
}
