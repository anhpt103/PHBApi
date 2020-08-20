using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_DS_FILE_DOITUONG")]
    public class PHF_DS_FILE_DOITUONG : DataInfoEntityPHF
    {
        [Column("MA_DOITUONG")]
        [StringLength(50)]
        public string MA_DOITUONG { get; set; }

        [Column("TEN_NGHIEPVU")]
        [StringLength(500)]
        public string TEN_NGHIEPVU { get; set; }

        [Column("FILE_DINHKEM")]
        [StringLength(1000)]
        public string FILE_DINHKEM { get; set; }

        [Column("SO_FILE")]
        [StringLength(500)]
        public string SO_FILE { get; set; }

        [Column("NGAY_XUAT_FILE")]
        public DateTime? NGAY_XUAT_FILE { get; set; }
    }
}
