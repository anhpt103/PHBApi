using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.DuToan
{
    [Table("PHA_NHAPDUTOAN_DIABAN")]
    public class PHA_NHAPDUTOAN_DIABAN : DataInfoEntity
    {
        [Column("NAM")]
        public int NAM { get; set; }

        [Column("LOAI_CHITIEU")]
        public int LOAI_CHITIEU { get; set; }

        [Column("MA_BAOCAO")]
        [StringLength(20)]
        public string MA_BAOCAO { get; set; }

        [Column("MA_CHITIEU")]
        [StringLength(50)]
        public string MA_CHITIEU { get; set; }

        [Column("MA_DBHC")]
        [StringLength(20)]
        public string MA_DBHC { get; set; }

        [Column("GIA_TRI")]
        public decimal? GIA_TRI { get; set; }

        [Column("TRANG_THAI")]
        [StringLength(10)]
        public string TRANG_THAI { get; set; }

        [Column("USER_NHAP")]
        [StringLength(50)]
        public string USER_NHAP { get; set; }

        [Column("NGAY_NHAP")]
        public DateTime? NGAY_NHAP { get; set; }

        [Column("USER_SUA")]
        [StringLength(50)]
        public string USER_SUA { get; set; }

        [Column("NGAY_SUA")]
        public DateTime? NGAY_SUA { get; set; }
    }
}
