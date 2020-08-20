using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_TIENDO_THUCHIEN_KH_DINHKEM")]
    public class PHF_TIENDO_THUCHIEN_KH_DINHKEM : DataInfoEntityPHF
    {
        [Required]
        [Column("MA_PHIEU")]
        [StringLength(50)]
        public string MA_PHIEU { get; set; }

        [Column("MA_DOITUONG")]
        [StringLength(500)]
        public string MA_DOITUONG { get; set; }

        [Column("NAM_THANHTRA")]
        public int NAM_THANHTRA { get; set; }

        [Column("LOAI_FILE")]
        [StringLength(200)]
        public string LOAI_FILE { get; set; }

        [Column("FILE_PATH")]
        [StringLength(500)]
        public string FILE_PATH { get; set; }
    }
}
