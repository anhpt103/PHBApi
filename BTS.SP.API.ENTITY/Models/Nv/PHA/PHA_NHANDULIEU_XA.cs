using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHA
{
    [Table("PHA_NHANDULIEU_XA")]
    public class PHA_NHANDULIEU_XA : DataInfoEntity
    {
        [Column("LOAI_BC")]
        public int LOAI_BC { get; set; }

        [Column("TRANGTHAI")]
        public int TRANGTHAI { get; set; }

        [Column("MA_BAOCAO")]
        [StringLength(50)]
        public string MA_BAOCAO { get; set; }

        [Column("NAM_BC")]
        [StringLength(10)]
        public string NAM_BC { get; set; }

        [Column("THANG_BC")]
        [StringLength(10)]
        public string THANG_BC { get; set; }

        [Column("DONVI")]
        [StringLength(200)]
        public string DONVI { get; set; }

        [Column("TEN_DONVI")]
        [StringLength(200)]
        public string TEN_DONVI { get; set; }

        [Column("MA_DBHC")]
        [StringLength(200)]
        public string MA_DBHC { get; set; }

        [Column("TEN_DBHC")]
        [StringLength(200)]
        public string TEN_DBHC { get; set; }

        [Column("MA_DBHC_NHAP")]
        [StringLength(200)]
        public string MA_DBHC_NHAP { get; set; }

        [Column("TEN_DBHC_NHAP")]
        [StringLength(200)]
        public string TEN_DBHC_NHAP { get; set; }

        [Column("NGAY_TAO")]
        public DateTime? NGAY_TAO { get; set; }

        [Column("NGUOI_TAO")]
        [StringLength(200)]
        public string NGUOI_TAO { get; set; }
    }
}
