using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHA
{
    [Table("PHA_NHANDULIEU_XA_DETAIL")]
    public class PHA_NHANDULIEU_XA_DETAIL : DataInfoEntity
    {
        [Column("MA_BAOCAOPK")]
        [StringLength(50)]
        public string MA_BAOCAOPK { get; set; }

        [Column("MA_BAOCAO")]
        [StringLength(50)]
        public string MA_BAOCAO { get; set; }

        [Column("MAQHNS")]
        [StringLength(10)]
        public string MAQHNS { get; set; }

        [Column("MA_TKTN")]
        [StringLength(10)]
        public string MA_TKTN { get; set; }

        [Column("CHUONG")]
        [StringLength(10)]
        public string CHUONG { get; set; }

        [Column("MACTMT")]
        [StringLength(10)]
        public string MACTMT { get; set; }

        [Column("KHOAN")]
        [StringLength(10)]
        public string KHOAN { get; set; }

        [Column("TIEUMUC")]
        [StringLength(10)]
        public string TIEUMUC { get; set; }

        [Column("MANV")]
        [StringLength(10)]
        public string MANV { get; set; }

        [Column("SOTIEN")]
        public decimal SOTIEN { get; set; }

        [Column("LOAI")]
        [StringLength(10)]
        public string LOAI { get; set; }

        [Column("MUC")]
        [StringLength(10)]
        public string MUC { get; set; }

        [Column("NHOM")]
        [StringLength(10)]
        public string NHOM { get; set; }

        [Column("TIEUNHOM")]
        [StringLength(10)]
        public string TIEUNHOM { get; set; }

        [Column("MA_KHOBAC")]
        [StringLength(10)]
        public string MA_KHOBAC { get; set; }

        [Column("MA_CAPNGANSACH")]
        [StringLength(10)]
        public string MA_CAPNGANSACH { get; set; }

        [Column("MA_DBHC")]
        [StringLength(10)]
        public string MA_DBHC { get; set; }

        [Column("DIENGIAI")]
        [StringLength(1000)]
        public string DIENGIAI { get; set; }

    }
}
